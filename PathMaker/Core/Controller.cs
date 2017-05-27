using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using StreetViewer.Service;
using StreetViewer.JsonObjects.GoogleApiJson.Common;
using StreetViewer.JsonObjects.GoogleApiJson.Geocoding;
using StreetViewer.JsonObjects.GoogleApiJson.Direction;
using StreetViewer.JsonObjects.OverpassApiJson;



namespace StreetViewer.Core
{
    /// <summary>
    /// Класс контроллер приложения.
    /// </summary>
    public class Controller
    {
        private GoogleRestService googleService;
        private GeographiService geoService;
        private OverpassRestService overpassService;
        private Parameters parameters;

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public Controller()
        {
            googleService = new GoogleRestService();
            geoService = new GeographiService();
            overpassService = new OverpassRestService();
            parameters = Parameters.Instance;
        }

        /// <summary>
        /// Получить координаты по запросу.
        /// </summary>
        /// <param name="streetName">Запрос (адрес или иное географическое название).</param>
        /// <returns>Координаты запроса или null, если результат пустой.</returns>
        public Location getGeocoding(string streetName)
        {
            GeocodeJsonReply reply = googleService.getGeocoding(streetName);
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
        public IList<Location> getDirection(string startStreet, string endStreet)
        {
            DirectionsStatusJson json = googleService.getDirection(startStreet, endStreet);

            if ("NOT_FOUND".Equals(json.Status))
            {
                return null;
            }
            else
            {
                return geoService.decodePolyline(json.Routes[0].OverviewPolyline.Points);
            }
        }

        /// <summary>
        /// Начать загрузку панорам по заданному списку путей.
        /// </summary>
        /// <param name="points">Список путей (список списков координат)</param>
        /// <param name="path">Путь у директории, в которых будут сохраняться снимки панорам.</param>
        /// <returns>Объект, который загружает панорамы в отдельном потоке.</returns>
        public Downloader getStreetViews(List<List<Location>> points, string path)
        {
            Downloader downloader = new Downloader(path, points, googleService);
            Thread downloadThread = new Thread(downloader.downloadStreetViews);
            downloadThread.Start();
            return downloader;
        }

        /// <summary>
        /// Получить все пути по области.
        /// </summary>
        /// <param name="lat">Широта точки центра области запроса.</param>
        /// <param name="lng">Долгота точки центра области запроса.</param>
        /// <returns>Список путей (списков списков координат)</returns>
        public List<List<Location>> getAllDirectionsOfArea(string lat, string lng)
        {
            GeoJson geoJson = overpassService.getWaysOfArea(lat, lng, parameters.Radius);
            return geoService.getAllDirectionsFromGeoJson(geoJson);
        }
    }
}
