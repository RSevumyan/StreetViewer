using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using StreetViewer.JsonObjects.Geocoding;
using StreetViewer.JsonObjects.Direction;

namespace StreetViewer.Service
{
    class RestService
    {
        private const String GOOGLE_API_KEY = "AIzaSyDZvb2R8tCxXLOJQMo7i-38-wzRGmPKKLE";
        private const String GEOCODING_URL_FORMAT = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}";
        private const String DESTINATION_URL_FORMAT = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&mode=driving&key={2}";
        private const String STREET_VIEW_URL_FORMAT = "https://maps.googleapis.com/maps/api/streetview?size=640x640&location={0},{1}&heading={2}&pitch=-0.76&key={3}";

        private const int BYTES_LENGTH = 2048;

        public RestService()
        {

        }

        public GeocodeJsonReply getGeocoding(String place)
        {
            HttpWebRequest reques = (HttpWebRequest)HttpWebRequest.Create(String.Format(GEOCODING_URL_FORMAT, place, GOOGLE_API_KEY));
            HttpWebResponse response = reques.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GeocodeJsonReply));
            object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());

            return objResponse as GeocodeJsonReply;
        }

        public DirectionsStatusJson getDirection(string startStreet, string endStreet)
        {
            HttpWebRequest reques = (HttpWebRequest)HttpWebRequest.Create(String.Format(DESTINATION_URL_FORMAT, startStreet, endStreet, GOOGLE_API_KEY));
            HttpWebResponse response = reques.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(DirectionsStatusJson));
            object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
            return objResponse as DirectionsStatusJson;

        }

        public Stream getStreetViewStream(string lat, string lng, string heading)
        {
            string url = String.Format(STREET_VIEW_URL_FORMAT, lat.Replace(",", "."), lng.Replace(",", "."), heading.Replace(",", "."), GOOGLE_API_KEY);
            HttpWebRequest reques = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = reques.GetResponse() as HttpWebResponse;
            return response.GetResponseStream();
        }
    }
}
