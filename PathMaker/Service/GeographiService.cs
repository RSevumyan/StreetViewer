using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StreetViewer.JsonObjects.Common;

namespace StreetViewer.Service
{
    public class GeographiService
    {
        private const int EARTH_RADIUS = 6371;

        public IList<Location> decodePolyline(string encodedPoints, int orderParam)
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
                    double distance = getDistanceByCoordinates(locationList[locationList.Count - 1], new Location(lat,lng));


                    if (distance > orderParam)
                    {
                        int order = (int)distance / orderParam;
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

        public double getDistanceByCoordinates(Location start, Location end)
        {
            double deltaLat = start.Lat - end.Lat;
            double deltaLng = start.Lng - end.Lng;
            double term1 = Math.Pow(Math.Sin(deltaLat/2),2);
            double term2 = Math.Cos(start.Lat) * Math.Cos(end.Lat) * Math.Pow(Math.Sin(deltaLng / 2), 2);
            double sigma = 2 * Math.Asin(Math.Sqrt(term1+ term2));
            return EARTH_RADIUS * sigma;
        }
    }
}
