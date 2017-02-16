using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StreetViewer.Service;
using StreetViewer.JsonObjects.Common;
using StreetViewer.JsonObjects.Geocoding;
using StreetViewer.JsonObjects.Direction;
using System.IO;


namespace StreetViewer.Core
{
    class Controller
    {
        private const string RESULT_DIRECTORY = "results";

        private RestService restService;
        private JsonService jsonService;

        public Controller()
        {
            restService = new RestService();
            jsonService = new JsonService();
        }

        public Location getGeocoding(string streetName)
        {
            //ToDo Сделать получение массива байт или строки в restService.getGeocoding();
            GeocodeJsonReply reply = restService.getGeocoding(streetName);
            return jsonService.parseGeocode(reply);
        }

        public void getDirection(string startStreet, string endStreet)
        {
            DirectionsStatusJson route = restService.getDirection(startStreet, endStreet);

            string polyLine = jsonService.parseDirection(route);
            if (polyLine != null)
            {

                if (!Directory.Exists(RESULT_DIRECTORY))
                {
                    Directory.CreateDirectory(RESULT_DIRECTORY);
                }
                IList<Location> points = decodePolyline(polyLine);
                StreamWriter streamWriter = new StreamWriter(RESULT_DIRECTORY+"\\coordinates.txt");
                for (int i = 0; i < points.Count; i++)
                {
                    Stream viewStream = restService.getStreetViewStream(points[i].Lat.ToString(), points[i].Lng.ToString());
                    FileStream fileStream = File.OpenWrite(RESULT_DIRECTORY+"\\"+ i + ".jpeg");
                    viewStream.CopyTo(fileStream);
                    fileStream.Close();
                    streamWriter.WriteLine(points[i].Lat + ";\t" + points[i].Lng + "\r\n");
                }
                streamWriter.Close();
            }
        }

        private IList<Location> decodePolyline(string encodedPoints)
        {
            IList<Location> locationList = new List<Location>();

            if (string.IsNullOrEmpty(encodedPoints))
                throw new ArgumentNullException("encodedPoints");

            char[] polylineChars = encodedPoints.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            while (index < polylineChars.Length)
            {
                // calculate next latitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = (int)polylineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length)
                    break;

                currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                //calculate next longitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = (int)polylineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length && next5bits >= 32)
                    break;

                currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                double lat = Convert.ToDouble(currentLat) / 1E5;
                double lng = Convert.ToDouble(currentLng) / 1E5;
                locationList.Add(new Location(lat, lng));

            }
            return locationList;
        }
    }
}
