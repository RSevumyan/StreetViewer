using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StreetViewer.JsonObjects.GoogleApiJson.Common;
using StreetViewer.JsonObjects.OverpassApiJson;

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

        public List<List<Location>> getAllDirectionsFromGeoJson(GeoJson geoJson)
        {
            Dictionary<long, Element> ways = new Dictionary<long, Element>();
            Dictionary<long, Element> nodes = new Dictionary<long, Element>();
            foreach (Element elem in geoJson.Elements)
            {
                if (elem.Type == "way")
                {
                    ways.Add(elem.Id, elem);
                }
                else if (elem.Type == "node")
                {
                    nodes.Add(elem.Id, elem);
                }
            }

            List<List<long>> combinedWays = combineWays(ways);

            List<List<Location>> formattedWays = new List<List<Location>>();

            foreach (List<long> way in combinedWays)
            {
                List<Location> locations = new List<Location>();
                foreach (long nodeId in way)
                {
                    locations.Add(new Location(nodes[nodeId].Lat, nodes[nodeId].Lon));
                }
                formattedWays.Add(locations);
            }
            return formattedWays;
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

        private List<List<long>> combineWays(Dictionary<long, Element> ways)
        {
            Dictionary<string, List<Element>> preCombinedWays = new Dictionary<string, List<Element>>();
            foreach (Element way in ways.Values)
            {
                if (!String.IsNullOrEmpty(way.Tags.Name))
                {
                    if (preCombinedWays.ContainsKey(way.Tags.Name))
                    {
                        addElementToWay(preCombinedWays[way.Tags.Name], way);
                    }
                    else
                    {
                        preCombinedWays.Add(way.Tags.Name, new List<Element>());
                        addElementToWay(preCombinedWays[way.Tags.Name], way);
                    }
                }
            }

            List<List<long>> list = new List<List<long>>();
            foreach (List<Element> elemList in preCombinedWays.Values)
            {
                int index = 0;
                while (index < elemList.Count - 1)
                {
                    List<long> localList = new List<long>();
                    do
                    {
                        localList.AddRange(elemList[index].Nodes);
                        index++;
                    } while (index < elemList.Count && localList[localList.Count - 1] == elemList[index].Nodes[0]);
                    list.Add(localList);
                }
            }
            return list;
        }

        private void addElementToWay(List<Element> way, Element partWay)
        {
            int prev = way.FindIndex(elem => elem.Nodes[elem.Nodes.Length - 1] == partWay.Nodes[0]);
            int next = way.FindIndex(elem => elem.Nodes[0] == partWay.Nodes[partWay.Nodes.Length - 1]);
            if (prev == -1 && next == -1)
            {
                way.Add(partWay);
            }
            else if (prev != -1 && next == -1)
            {
                way.Insert(prev + 1, partWay);
            }
            else if (prev == -1 && next != -1)
            {
                way.Insert(next, partWay);
            }
            else if (prev != -1 && next != -1)
            {
                if (prev < next)
                {
                    for (int i = way.Count - 1; i >= next; i--)
                    {
                        Element elem = way[way.Count - 1];
                        way.RemoveAt(way.Count - 1);
                        way.Insert(prev + 1, elem);
                    }
                    way.Insert(prev + 1, partWay);
                }
                else if (prev > next)
                {
                    int index = prev;
                    while (index > 0 && way[index].Nodes[0] == way[index - 1].Nodes[way[index - 1].Nodes.Length - 1])
                    {
                        index--;
                    }
                    for (int i = prev; i >= index; i--)
                    {
                        Element elem = way[prev];
                        way.RemoveAt(prev);
                        way.Insert(next, elem);
                    }
                    way.Insert(next + 1, partWay);
                }
            }
        }
    }
}
