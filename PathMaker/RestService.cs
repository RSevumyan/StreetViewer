using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;

namespace PathMaker
{
    class RestService
    {

        private const String GOOGLE_API_KEY = "AIzaSyDZvb2R8tCxXLOJQMo7i-38-wzRGmPKKLE";
        private const String GEOCODING_URL_FORMAT = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}";
        private const String DESTINATION_URL_FORMAT = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&mode=driving&key={2}";
        private const String STREET_VIEW_URL_FORMAT = "https://maps.googleapis.com/maps/api/streetview?size=600x300&location={0},{1}&heading=151.78&pitch=-0.76&key={2}";

        private const int BYTES_LENGTH = 2048;

        public RestService()
        {

        }
        public GeocodeJsonReply GetGeocoding(String place)
        {
            HttpWebRequest reques = (HttpWebRequest)HttpWebRequest.Create(String.Format(GEOCODING_URL_FORMAT,place, GOOGLE_API_KEY));
            HttpWebResponse response = reques.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GeocodeJsonReply));
            object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
            
            return objResponse as GeocodeJsonReply;
        }

        public void WriteToFile(string url){
            HttpWebRequest reques = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = reques.GetResponse() as HttpWebResponse;
            FileStream fs = File.OpenWrite("a.txt");
            response.GetResponseStream().CopyTo(fs);
            fs.Close();
        }
    }
}
