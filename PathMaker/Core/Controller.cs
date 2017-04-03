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
        private const int ORDER_PARAM = 50;

        private RestService restService;

        public Controller()
        {
            restService = new RestService();
        }

        public Location getGeocoding(string streetName)
        {
            GeocodeJsonReply reply = restService.getGeocoding(streetName);
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

        public IList<Location> getDirection(string startStreet, string endStreet)
        {
            DirectionsStatusJson json = restService.getDirection(startStreet, endStreet);

            if ("NOT_FOUND".Equals(json.Status))
            {
                return null;
            }
            else
            {
                return decodePolyline(json.Routes[0].OverviewPolyline.Points);
            }
        }

        public void getStreetViews(IList<Location> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                double angle = 0;

                if (i < points.Count - 1)
                {
                    angle = calculateAngle(points[i].Lat - points[i + 1].Lat, points[i].Lng - points[i + 1].Lng);
                }
                else
                {
                    angle = calculateAngle(points[i - 1].Lat - points[i].Lat, points[i - 1].Lng - points[i].Lng);
                }

                Stream viewStream = restService.getStreetViewStream(points[i].Lat.ToString(), points[i].Lng.ToString(), angle.ToString());
                FileStream fileStream = File.OpenWrite(RESULT_DIRECTORY + "\\" + i + ".jpeg");
                viewStream.CopyTo(fileStream);
                fileStream.Close();   
            }
            logDirectionCoordinates(points);
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

        private void logDirectionCoordinates(IList<Location> points)
        {
            createDirectory();
            StreamWriter streamWriter = new StreamWriter(RESULT_DIRECTORY + "\\coordinates.txt");
            for (int i = 0; i < points.Count; i++)
            {
                streamWriter.WriteLine(points[i].Lat + ";\t" + points[i].Lng + "\r\n");
            }
            streamWriter.Close();
        }

        private void createDirectory()
        {
            if (!Directory.Exists(RESULT_DIRECTORY))
            {
                Directory.CreateDirectory(RESULT_DIRECTORY);
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

                if (locationList.Any())
                {
                    double height = (lat - locationList[locationList.Count - 1].Lat) * 100000;
                    double width = (lng - locationList[locationList.Count - 1].Lng) * 100000;
                    double hypotenuse = Math.Sqrt((height * height) + (width * width));


                    if (hypotenuse > ORDER_PARAM)
                    {
                        int order = (int)hypotenuse / ORDER_PARAM;
                        double latStep = height / (order + 1) / 100000;
                        double lngStep = width / (order + 1) / 100000;

                        for (int i = 0; i < order; i++)
                        {
                            double newLat = locationList[locationList.Count - 1].Lat + latStep;
                            double newLng = locationList[locationList.Count - 1].Lng + lngStep;
                            locationList.Add(new Location(newLat, newLng));
                        }
                    }
                }
                locationList.Add(new Location(lat, lng));

            }
            return locationList;
        }

        private double calculateAngle(double height, double width)
        {
            height += height == 0 ? 0.00001 : 0;
            double angle = Math.Atan(width / height) * (180 / Math.PI);

            if (angle < 0)
            {
                angle += 90;
            }

            if (height >= 0 && width < 0)
            {
                angle += 90;
            }
            else if (height > 0 && width >= 0)
            {
                angle += 180;
            }
            else if (height <= 0 && width > 0)
            {
                angle += 270;
            }
            return angle;
        }
    }
}
