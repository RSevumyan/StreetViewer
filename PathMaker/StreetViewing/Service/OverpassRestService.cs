using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;

using PathFinder.StreetViewing.JsonObjects.OverpassApiJson;

namespace PathFinder.StreetViewing.Service
{
    /// <summary>
    /// Сервис работы с Overpass Turbo API.
    /// </summary>
    public class OverpassRestService : AbstractRestService
    {
        private const string QUERY_WAYS_IN_CIRCLE = "[out:json][timeout:200];way(around:{0},{1},{2})[\"highway\"~\"motorway|trunk|residential|primary|motorway_link|trunk_link|primary_link|secondary|tertiary|unclassified\"];out body;>;out skel;";
        private const string QUERY_WAYS_IN_SQUARE = "[out:json][timeout:200];way({0},{1},{2},{3})[\"highway\"~\"motorway|trunk|residential|primary|motorway_link|trunk_link|primary_link|secondary|tertiary|unclassified\"];out body;>;out skel;";
        private const string OVERPASS_REQUEST = "http://overpass.openstreetmap.fr//api/interpreter?data={0}";

        /// <summary>
        /// Запрос всех путей по области с ответом в виде GeoJson.
        /// </summary>
        /// <param name="lat">Широта точки центра области запроса.</param>
        /// <param name="lng">Долгота точки центра области запроса.</param>
        /// <param name="radius">Радиус, по которому будут запрашиваться пути.</param>
        /// <returns>GeoJson, содержащий результат запроса.</returns>
        public GeoJson GetWaysOfArea(double lat, double lng, int radius)
        {
            string query = String.Format(QUERY_WAYS_IN_CIRCLE, radius, coordinateToString(lat), coordinateToString(lng));
            return RequestQueryToOverPass(query);
        }

        public GeoJson GetWaysOfArea(double lat1, double lng1, double lat2, double lng2)
        {
            double maxLat = lat1 > lat2 ? lat1 : lat2;
            double minLat = lat1 < lat2 ? lat1 : lat2;
            double maxLng = lng1 > lng2 ? lng1 : lng2;
            double minLng = lng1 < lng2 ? lng1 : lng2;
           
            string query = String.Format(QUERY_WAYS_IN_SQUARE, coordinateToString(minLat), coordinateToString(minLng), coordinateToString(maxLat), coordinateToString(maxLng));
            return RequestQueryToOverPass(query);
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

        private GeoJson RequestQueryToOverPass(string query)
        {
            string request = String.Format(OVERPASS_REQUEST, query);
            HttpWebRequest reques = (HttpWebRequest)HttpWebRequest.Create(request);
            HttpWebResponse response = reques.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GeoJson));
            object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
            return objResponse as GeoJson;
        }
    }
}
