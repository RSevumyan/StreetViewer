using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;

using StreetViewer.JsonObjects.OverpassApiJson;

namespace StreetViewer.Service
{
    class OverpassRestService
    {
        private const string OVERPASS_WAYS_FROM_AREA_QUERY = "[out:json];way(around:{0},{1},{2})[\"highway\"~\"motorway|trunk|primary|motorway_link|trunk_link|primary_link|secondary|tertiary\"];(._;>;);out;";
        private const string OVERPASS_REQUEST = "http://overpass-api.de//api/interpreter?data={0}";

        public OverpassRestService()
        {
        }

        public GeoJson getWaysOfArea(string radius, string lat, string lng)
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
