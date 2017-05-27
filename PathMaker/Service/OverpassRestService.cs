using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;

using StreetViewer.JsonObjects.OverpassApiJson;
using StreetViewer.Core;

namespace StreetViewer.Service
{
    /// <summary>
    /// Сервис работы с Overpass Turbo API.
    /// </summary>
    public class OverpassRestService
    {
        private const string OVERPASS_WAYS_FROM_AREA_QUERY = "[out:json];way(around:{0},{1},{2})[\"highway\"~\"motorway|trunk|primary|motorway_link|trunk_link|primary_link|secondary|tertiary|unclassified\"];(._;>;);out;";
        private const string OVERPASS_REQUEST = "http://overpass-api.de//api/interpreter?data={0}";
        
        /// <summary>
        /// Запрос всех путей по области с ответом в виде GeoJson.
        /// </summary>
        /// <param name="lat">Широта точки центра области запроса.</param>
        /// <param name="lng">Долгота точки центра области запроса.</param>
        /// <param name="radius">Радиус, по которому будут запрашиваться пути.</param>
        /// <returns>GeoJson, содержащий результат запроса.</returns>
        public GeoJson getWaysOfArea(string lat, string lng, int radius)
        {
            string query = String.Format(OVERPASS_WAYS_FROM_AREA_QUERY, radius, lat, lng);
            string request = String.Format(OVERPASS_REQUEST, query);
            HttpWebRequest reques = (HttpWebRequest)HttpWebRequest.Create(request);
            HttpWebResponse response = reques.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GeoJson));
            object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
            return objResponse as GeoJson;
        }
    }
}
