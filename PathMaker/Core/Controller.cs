using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using StreetViewer.Service;
using StreetViewer.JsonObjects.GoogleApiJson.Common;
using StreetViewer.JsonObjects.GoogleApiJson.Geocoding;
using StreetViewer.JsonObjects.GoogleApiJson.Direction;
using StreetViewer.JsonObjects.OverpassApiJson;



namespace StreetViewer.Core
{
    class Controller
    {
        private Parameters parameters;
        private GoogleRestService googleService;
        private GeographiService geoService;
        private OverpassRestService overpassService;

        public Controller()
        {
            googleService = new GoogleRestService();
            parameters = new Parameters();
            geoService = new GeographiService();
            overpassService = new OverpassRestService();
        }

        public Location getGeocoding(string streetName)
        {
            GeocodeJsonReply reply = googleService.getGeocoding(streetName);
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
            DirectionsStatusJson json = googleService.getDirection(startStreet, endStreet);

            if ("NOT_FOUND".Equals(json.Status))
            {
                return null;
            }
            else
            {
                return geoService.decodePolyline(json.Routes[0].OverviewPolyline.Points, parameters.Order);
            }
        }

        public Downloader getStreetViews(IList<Location> points, string path)
        {
            Downloader downloader = new Downloader(path, points, googleService);
            Thread downloadThread = new Thread(downloader.downloadStreetViews);
            downloadThread.Start();
            return downloader;
        }

        public void setParams(int order, int radius)
        {
            parameters.Order = order;
            parameters.Radius = radius;
        }

        public List<List<Location>> getAllDirectionsOfArea(string lat, string lng)
        {
            GeoJson geoJson = overpassService.getWaysOfArea(parameters.Radius.ToString(), lat, lng);
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
                    while (way[index].Nodes[0] == way[index - 1].Nodes[way[index - 1].Nodes.Length - 1])
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
