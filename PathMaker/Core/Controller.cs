using System.Collections.Generic;
using System.Linq;
using System.Threading;

using PathFinder.StreetViewing.Service;
using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Common;
using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Geocoding;
using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Direction;
using PathFinder.StreetViewing.JsonObjects.OverpassApiJson;
using PathFinder.SignDetection;
using PathFinder.DataBaseService;
using CommonDetectorApi;
using PathFinder.DatabaseService.Model;

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
        private DetectorsManager detectorsManager;
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
            detectorsManager = new DetectorsManager(parameters.PluginsPath);
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
        /// Начать загрузку панорам по заданному списку путей
        /// </summary>
        /// <param name="points">Список путей (список списков координат)</param>
        /// <param name="path">Путь у директории, в которых будут сохраняться снимки панорам</param>
        /// <returns>Объект, который загружает панорамы в отдельном потоке</returns>
        public Downloader GetStreetViews(IList<PolylineChunk> chunks, string path)
        {
            downloader = new Downloader(path, chunks, googleService, dbContext);
            Thread downloadThread = new Thread(downloader.DownloadStreetViews);
            downloadThread.Start();
            return downloader;
        }

        /// <summary>
        /// Начать загрузку панорам по заданному списку улиц.
        /// </summary>
        /// <param name="roads">Список улиц</param>
        /// <param name="path">Путь у директории, в которых будут сохраняться снимки панорам</param>
        /// <returns>Объект, который загружает панорамы в отдельном потоке</returns>
        public Downloader GetStreetViews(List<string> roadsNames, string path)
        {
            Dictionary<string, Road> roadDictionary = GeographiData.Instance.Roads;
            List<Road> roads = roadsNames.Select(x => roadDictionary[x]).ToList();
            downloader = new Downloader(path, roads, googleService, dbContext, geoService);
            Thread downloadThread = new Thread(downloader.DownloadRoadsStreetViews);
            downloadThread.Start();
            return downloader;
        }

        /// <summary>
        /// Начать детектирование объектов на выбранных улицах
        /// </summary>
        /// <param name="roads">Список названий улиц</param>
        /// <param name="detectorsNames">Список названий детекторов, которые будут использоваться для детектирования объектов</param>
        /// <returns></returns>
        public SignDetectionProcessor StartDetection(List<string> roads, List<string> detectorsNames)
        {
            List<IDetector> selectedDetectors = detectorsManager.Detectors.Where(x => detectorsNames.Contains(x.Name)).ToList();
            SignDetectionProcessor processor = new SignDetectionProcessor(roads, selectedDetectors, dbContext);
            Thread bypassThread = new Thread(processor.Start);
            bypassThread.Start();
            return processor;

        }

        /// <summary>
        /// Получить все пути по области.
        /// </summary>
        /// <param name="lat">Широта точки центра области запроса.</param>
        /// <param name="lng">Долгота точки центра области запроса.</param>
        /// <returns>Список путей (списков списков координат)</returns>
        public List<PolylineChunk> GetAllDirectionsOfArea(double lat, double lng)
        {
            List<PolylineChunk> areaChunks = new List<PolylineChunk>();
            GeoJson geoJson = overpassService.GetWaysOfArea(lat, lng, parameters.Radius);
            HashSet<long> chunkIdSet = new HashSet<long>(dbContext.Chunks.Select(ch => ch.OverpassId));
            areaChunks.AddRange(geoService.GetPolylineChunksFromGeoJson(geoJson));
            areaChunks.RemoveAll(ch => chunkIdSet.Contains(ch.OverpassId));
            dbContext.Chunks.AddRange(areaChunks);
            dbContext.SaveChanges();
            return areaChunks;
        }

        /// <summary>
        /// Получить вссе улицы по области
        /// </summary>
        /// <param name="lat">Центральная долгота</param>
        /// <param name="lng">Центральная широта</param>
        /// <returns>Словарь улиц</returns>
        public Dictionary<string, Road> GetAllRoadsOfArea(double lat, double lng)
        {
            GeoJson geoJson = overpassService.GetWaysOfArea(lat, lng, parameters.Radius);
            return GetAllRoadsOfArea(geoJson);
        }

        /// <summary>
        /// Получить вссе улицы по области
        /// </summary>
        /// <param name="firstLat">Широта первой точки</param>
        /// <param name="firstlng">Долгота первой точки</param>
        /// <param name="secondLat">Широта второй точки</param>
        /// <param name="secondLng">Долгота второй точки</param>
        /// <returns>Словарь улиц</returns>
        public Dictionary<string, Road> GetAllRoadsOfArea(double firstLat, double firstlng, double secondLat, double secondLng)
        {
            GeoJson geoJson = overpassService.GetWaysOfArea(firstLat, firstlng, secondLat, secondLng);
            return GetAllRoadsOfArea(geoJson);
        }

        private Dictionary<string, Road> GetAllRoadsOfArea(GeoJson geoJson)
        {
            Dictionary<string, Road> roadsDictionary = new Dictionary<string, Road>();
            roadsDictionary = geoService.GetRoadsDictionaryFromGeoJson(geoJson);
            //dbContext.Roads.AddRange(roadsDictionary.Values);
            //dbContext.SaveChanges();

            foreach(Road road in roadsDictionary.Values)
            {
                if(road.Id == 0)
                {
                    dbContext.Roads.Add(road);
                }
            }
            dbContext.SaveChanges();
            return roadsDictionary;
        }

        /// <summary>
        /// ЗАгрузить из базы все участки путей
        /// </summary>
        /// <returns>Список участков путей</returns>
        public List<PolylineChunk> LoadExistingChunks()
        {
            List<PolylineChunk> chunks = new List<PolylineChunk>();
            chunks.AddRange(dbContext.Chunks);
            return chunks;
        }

        /// <summary>
        /// Загрузить все геоданные из базы
        /// </summary>
        /// <returns>Географические данные</returns>
        public GeographiData LoadGeoData()
        {
            GeographiData geoData = GeographiData.Instance;
            geoData.Locations = dbContext.LocationEntities.ToDictionary(l => l.OverpassId);
            geoData.Chunks = dbContext.Chunks.ToDictionary(ch => ch.OverpassId);
            geoData.Roads = dbContext.Roads.ToDictionary(r => r.Name);
            return geoData;
        }

        /// <summary>
        /// Получить список названий загруженных детекторов
        /// </summary>
        /// <returns>Список названий загруженных детекторов</returns>
        public List<string> GetDetectorNamesList()
        {
            return detectorsManager.Detectors.Select(detector => detector.Name).ToList();
        }

        /// <summary>
        /// Перезагрузить менеджер плагинов
        /// </summary>
        public void ReloadDetectorManager()
        {
            detectorsManager = new DetectorsManager(parameters.PluginsPath);
        }

        public HashSet<DatabaseService.Model.Sign> LoadSigns()
        {
            HashSet<DatabaseService.Model.Sign> signsSet = new HashSet<DatabaseService.Model.Sign>();
            signsSet.Union(dbContext.Signs);
            return signsSet;
        }
    }
}