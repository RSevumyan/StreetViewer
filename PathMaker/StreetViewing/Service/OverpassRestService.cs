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
        private const string OVERPASS_WAYS_FROM_AREA_QUERY = "[out:json][timeout:200];way(around:{0},{1},{2})[\"highway\"~\"motorway|trunk|residential|primary|motorway_link|trunk_link|primary_link|secondary|tertiary|unclassified\"];out body;>;out skel;";
        private const string OVERPASS_REQUEST = "http://overpass-api.de//api/interpreter?data={0}";
        
        /// <summary>
        /// Запрос всех путей по области с ответом в виде GeoJson.
        /// </summary>
        /// <param name="lat">Широта точки центра области запроса.</param>
        /// <param name="lng">Долгота точки центра области запроса.</param>
        /// <param name="radius">Радиус, по которому будут запрашиваться пути.</param>
        /// <returns>GeoJson, содержащий результат запроса.</returns>
        public GeoJson getWaysOfArea(double lat, double lng, int radius)
        {
            string query = String.Format(OVERPASS_WAYS_FROM_AREA_QUERY, radius, coordinateToString(lat), coordinateToString(lng));
            string request = String.Format(OVERPASS_REQUEST, query);
            HttpWebRequest reques = (HttpWebRequest)HttpWebRequest.Create(request);
            HttpWebResponse response = reques.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GeoJson));
            object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
            return objResponse as GeoJson;
        }
    }
}
