using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using PathFinder.StreetViewing.Service;
using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Common;
using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Geocoding;
using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Direction;
using PathFinder.StreetViewing.JsonObjects.OverpassApiJson;
using System.Reflection;
using PathFinder.SignDetection;
using PathFinder.StreetViewing;
using PathFinder.DataBaseService;

namespace PathFinder.Core
{
    /// <summary>
    /// Класс контроллер приложения.
    /// </summary>
    public class Controller
    {
        private GoogleRestService googleService;
        private GeographiService geoService;
        private OverpassRestService overpassService;
        private Downloader downloader;
        private IList<IDetector> detectors;
        private Parameters parameters;
        private PathFinderContext dbContext;

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public Controller()
        {
            googleService = new GoogleRestService();
            geoService = new GeographiService();
            overpassService = new OverpassRestService();
            parameters = Parameters.Instance;
            //ToDo Доделать загрузку плагинов
            //detectors = PluginsController.LoadDetectorPlugons(parameters.PlugnisPath);
            dbContext = new PathFinderContext();
        }

        /// <summary>
        /// Получить координаты по запросу.
        /// </summary>
        /// <param name="streetName">Запрос (адрес или иное географическое название).</param>
        /// <returns>Координаты запроса или null, если результат пустой.</returns>
        public Location GetGeocoding(string streetName)
        {
            GeocodeJsonReply reply = googleService.GetGeocoding(streetName);
            if ("ZERO_RESULTS".Equals(reply.Status))
            {
                return null;
            }
            else
            {
                double lat = reply.Results[0].Geometry.Location.Lat;
                double lng = reply.Results[0].Geometry.Location.Lng;
                return new Location(lat, lng);
            }
        }

        /// <summary>
        /// Получить путь между двумя географическими названиями.
        /// </summary>
        /// <param name="startStreet">
        /// Начальная точка, в формате xxx.xxxx,yyy.yyyy,
        /// или географическое наименование, которое будет началом пути.
        /// </param>
        /// <param name="endStreet">
        /// Конечная точка, в формате xxx.xxxx,yyy.yyyy,
        /// или географическое наименование, которое будет концом пути.
        /// </param>
        /// <returns>Список координат пути или null, если результат пустой.</returns>
        public IList<PolylineChunk> GetDirection(string startStreet, string endStreet)
        {
            DirectionsStatusJson json = googleService.GetDirection(startStreet, endStreet);
            return HandleGoogleDirectionResultJson(json);
        }

        /// <summary>
        /// Получить путь между двумя географическими названиями.
        /// </summary>
        /// <param name="start">
        /// Начальная точка в формате GMap.NET.PointLatLng.
        /// </param>
        /// <param name="end">
        /// Конечная точка, в формате GMap.NET.PointLatLng
        /// </param>
        /// <returns>Список координат пути или null, если результат пустой.</returns>
        public IList<PolylineChunk> GetDirection(GMap.NET.PointLatLng start, GMap.NET.PointLatLng end)
        {
            return GetDirection(start.Lat, start.Lng, end.Lat, end.Lng);
        }

        /// <summary>
        /// Получить путь между двумя географическими названиями.
        /// </summary>
        /// <param name="startLat">
        /// Широта начальной точки
        /// </param>
        /// <param name="startLng">
        /// Долгота начальной точки
        /// </param>
        /// <param name="endLat">
        /// Широта конечной точки
        /// </param>
        /// <param name="endLng">
        /// Долгота конечной точки
        /// </param>
        /// <returns>Список координат пути или null, если результат пустой.</returns>
        public IList<PolylineChunk> GetDirection(double startLat, double startLng, double endLat, double endLng)
        {
            DirectionsStatusJson json = googleService.GetDirection(startLat, startLng, endLat, endLng);
            return HandleGoogleDirectionResultJson(json);
        }

        /// <summary>
        /// Начать загрузку панорам по заданному списку путей.
        /// </summary>
        /// <param name="points">Список путей (список списков координат)</param>
        /// <param name="path">Путь у директории, в которых будут сохраняться снимки панорам.</param>
        /// <returns>Объект, который загружает панорамы в отдельном потоке.</returns>
        public Downloader GetStreetViews(IList<PolylineChunk> chunks, string path)
        {
            downloader = new Downloader(path, chunks, googleService, dbContext);
            Thread downloadThread = new Thread(downloader.DownloadStreetViews);
            downloadThread.Start();
            return downloader;
        }

        /// <summary>
        /// Получить все пути по области.
        /// </summary>
        /// <param name="lat">Широта точки центра области запроса.</param>
        /// <param name="lng">Долгота точки центра области запроса.</param>
        /// <returns>Список путей (списков списков координат)</returns>
        public IList<PolylineChunk> GetAllDirectionsOfArea(double lat, double lng)
        {
            List<PolylineChunk> areaChunks = new List<PolylineChunk>();
            GeoJson geoJson = overpassService.getWaysOfArea(lat, lng, parameters.Radius);
            HashSet<long> chunkIdSet = new HashSet<long>(dbContext.Chunks.Select(ch => ch.OverpassId));
            areaChunks.AddRange(geoService.GetPolylineChunksFromGeoJson(geoJson));
            areaChunks.RemoveAll(ch => chunkIdSet.Contains(ch.OverpassId));
            return areaChunks;
        }

        public List<PolylineChunk> LoadExistingChunks()
        {
            List<PolylineChunk> chunks = new List<PolylineChunk>();
            chunks.AddRange(dbContext.Chunks);
            return chunks;
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

        private IList<PolylineChunk> HandleGoogleDirectionResultJson(DirectionsStatusJson json)
        {
            if ("NOT_FOUND".Equals(json.Status))
            {
                return null;
            }
            else
            {
                List<Location> result = geoService.DecodePolyline(json.Routes[0].OverviewPolyline.Points);
                return PopulatePolylineChunks(LocationEntity.ConvertFromGoogleLocations(result));
            }

        }

        private List<PolylineChunk> PopulatePolylineChunks(List<LocationEntity> locationsList)
        {
            List<PolylineChunk> chunkList = new List<PolylineChunk>();
            for (int i = 1; i < locationsList.Count - 2; i++)
            {
                List<LocationEntity> chunkLocationList = new List<LocationEntity>();

                chunkLocationList.Add(locationsList[i]);
                chunkLocationList.AddRange(geoService.GetIntermediateLocations(locationsList[i], locationsList[i + 1]));
                chunkLocationList.Add(locationsList[i + 1]);
                PolylineChunk chunk = new PolylineChunk(chunkLocationList);
                chunkList.Add(chunk);
            }
            return chunkList;
        }
    }
}
