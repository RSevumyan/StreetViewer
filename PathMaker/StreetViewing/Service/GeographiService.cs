using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Common;
using PathFinder.StreetViewing.JsonObjects.OverpassApiJson;
using PathFinder.Core;
using PathFinder.DataBaseService;
using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Direction;
using PathFinder.DatabaseService.Model;

namespace PathFinder.StreetViewing.Service
{
    /// <summary>
    /// Сервис работы с географическими данными.
    /// </summary>
    public class GeographiService
    {
        private const int EARTH_RADIUS = 6371;

        /// <summary>
        /// Декодировать строку полилинии в список координат.
        /// </summary>
        /// <param name="encodedPoints">строка полилинии</param>
        /// <returns>Список координат</returns>
        public List<Location> DecodePolyline(string encodedPoints)
        {
            int orderParam = Parameters.Instance.Order;

            List<Location> locationList = new List<Location>();

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
                Location newLocation = new Location(lat, lng);

                locationList.Add(newLocation);
            }
            return locationList;
        }

        /// <summary>
        /// Получить расстояние между двумя географическими точками.
        /// </summary>
        /// <param name="start">Начальная географическая точка</param>
        /// <param name="end">Конечная географическая точка</param>
        /// <returns>Расстояние</returns>
        public double GetDistanceByCoordinates(LocationEntity start, LocationEntity end)
        {
            double deltaLat = start.Lat - end.Lat;
            double deltaLng = start.Lng - end.Lng;
            double term1 = Math.Pow(Math.Sin(deltaLat / 2), 2);
            double term2 = Math.Cos(start.Lat) * Math.Cos(end.Lat) * Math.Pow(Math.Sin(deltaLng / 2), 2);
            double sigma = 2 * Math.Asin(Math.Sqrt(term1 + term2));
            return EARTH_RADIUS * sigma;
        }

        /// <summary>
        /// Получение участков пути из Geo Json
        /// </summary>
        /// <param name="geoJson">Geo Json</param>
        /// <returns>Список учасков пути</returns>
        public List<PolylineChunk> GetPolylineChunksFromGeoJson(GeoJson geoJson)
        {
            List<PolylineChunk> resultChunks = new List<PolylineChunk>();
            Dictionary<long, Element> ways = new Dictionary<long, Element>();
            Dictionary<long, Element> nodes = new Dictionary<long, Element>();
            Dictionary<string, Road> roads = new Dictionary<string, Road>();
            foreach (Element elem in geoJson.Elements)
            {
                if (elem.Type == "way")
                {
                    ways.Add(elem.Id, elem);
                    if (!roads.ContainsKey(elem.Tags.Name))
                    {
                        roads.Add(elem.Tags.Name, new Road(elem.Tags.Name));
                    }
                }
                else if (elem.Type == "node")
                {
                    nodes.Add(elem.Id, elem);
                }
            }

            foreach (Element way in ways.Values)
            {
                PolylineChunk chunk = new PolylineChunk();
                chunk.OverpassId = way.Id;
                List<OrderedLocationEntity> chunkLocation = new List<OrderedLocationEntity>();
                for (int i = 0; i < way.Nodes.Count() - 1; i++)
                {
                    LocationEntity start = new LocationEntity(nodes[way.Nodes[i]].Lat, nodes[way.Nodes[i]].Lon);
                    chunkLocation.Add(new OrderedLocationEntity(i,start));
                    LocationEntity end = new LocationEntity(nodes[way.Nodes[i + 1]].Lat, nodes[way.Nodes[i + 1]].Lon);
                    //chunkLocation.AddRange(GetIntermediateLocations(start, end));
                    chunkLocation.Add(new OrderedLocationEntity(i+1, start));
                }
                chunk.OrderedLocationEntities = chunkLocation;
                resultChunks.Add(chunk);
            }
            return resultChunks;
        }

        /// <summary>
        /// Получить словарь улиц из Geo Json
        /// </summary>
        /// <param name="geoJson">Geo Json</param>
        /// <returns>Словарь улиц</returns>
        public Dictionary<string, Road> GetRoadsDictionaryFromGeoJson(GeoJson geoJson)
        {
            Dictionary<string, Road> roads = new Dictionary<string, Road>();
            GeographiData geoData = GeographiData.Instance;
            List<Element> ways = new List<Element>();
            foreach (Element elem in geoJson.Elements)
            {
                if (elem.Type == "way" && !string.IsNullOrEmpty(elem.Tags.Name) && !geoData.Chunks.Keys.Contains(elem.Id))
                {
                    ways.Add(elem);
                }
                else if (elem.Type == "node" && !geoData.Locations.Keys.Contains(elem.Id))
                {
                    geoData.Locations.Add(elem.Id, new LocationEntity(elem));
                }
            }

            foreach (Element elem in ways)
            {
                PolylineChunk chunk = CreateChunk(elem);
                geoData.Chunks.Add(chunk.OverpassId, chunk);
                if (geoData.Roads.Keys.Contains(elem.Tags.Name))
                {
                    Road road = geoData.Roads[elem.Tags.Name];
                    AddElementToWay(road.PolylineChunks, chunk);
                    road.IsStreetViewsDownloaded = false;
                    if (!roads.Keys.Contains(road.Name))
                    {
                        roads.Add(road.Name, road);
                    }
                }
                else
                {
                    Road road = new Road(elem.Tags.Name);
                    AddElementToWay(road.PolylineChunks, chunk);
                    roads.Add(road.Name, road);
                    geoData.Roads.Add(road.Name, road);
                }
            }
            return roads;
        }

        /// <summary>
        /// Получить промежуточные точки между двумя географическими координатами
        /// </summary>
        /// <param name="start">Начальная координата</param>
        /// <param name="end">Конечная координата</param>
        /// <returns>Список промежуточных координат</returns>
        public List<LocationEntity> GetIntermediateLocations(LocationEntity start, LocationEntity end)
        {
            int orderParam = Parameters.Instance.Order;
            List<LocationEntity> locationList = new List<LocationEntity>();
            double height = (end.Lat - start.Lat) * 100000;
            double width = (end.Lng - start.Lng) * 100000;
            double distance = GetDistanceByCoordinates(start, end);
            locationList.Add(start);

            if (distance > orderParam)
            {
                int order = (int)distance / orderParam;
                double latStep = height / (order + 1) / 100000;
                double lngStep = width / (order + 1) / 100000;

                for (int i = 0; i < order; i++)
                {
                    double newLat = locationList[locationList.Count - 1].Lat + latStep;
                    double newLng = locationList[locationList.Count - 1].Lng + lngStep;
                    locationList.Add(new LocationEntity(newLat, newLng));
                }
            }
            locationList.Remove(start);
            return locationList;
        }

        /// <summary>
        /// Вучислить угол направления по расстоянию по широте и долготе между двумя точками
        /// </summary>
        /// <param name="height">Расстояние по широте между двумя точками</param>
        /// <param name="width">Расстояние по долготе между двумя точками</param>
        /// <returns>Угол направление между двумя точками</returns>
        public double CalculateAngle(double height, double width)
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

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

        private PolylineChunk CreateChunk(Element way)
        {
            GeographiData geoData = GeographiData.Instance;
            PolylineChunk chunk = new PolylineChunk();
            chunk.OverpassId = way.Id;
            chunk.OrderedLocationEntities = new List<OrderedLocationEntity>();
            for(int i = 0; i < way.Nodes.Count(); i++)
            {
                LocationEntity location = geoData.Locations[way.Nodes[i]];
                chunk.OrderedLocationEntities.Add(new OrderedLocationEntity(i, location));

            }
            return chunk;
        }

        private Dictionary<string, List<Element>> CombineWays(Dictionary<long, Element> ways)
        {
            Dictionary<string, List<Element>> preCombinedWays = new Dictionary<string, List<Element>>();
            foreach (Element way in ways.Values)
            {
                if (!String.IsNullOrEmpty(way.Tags.Name))
                {
                    if (preCombinedWays.ContainsKey(way.Tags.Name))
                    {
                        AddElementToWay(preCombinedWays[way.Tags.Name], way);
                    }
                    else
                    {
                        preCombinedWays.Add(way.Tags.Name, new List<Element>());
                        AddElementToWay(preCombinedWays[way.Tags.Name], way);
                    }
                }
            }
            return preCombinedWays;
        }

        private void AddElementToWay(List<PolylineChunk> way, PolylineChunk partWay)
        {
            int prev = way.FindIndex(elem => elem.OrderedLocationEntities[elem.OrderedLocationEntities.Count - 1].LocationEntity.OverpassId == partWay.OrderedLocationEntities[0].LocationEntity.OverpassId);
            int next = way.FindIndex(elem => elem.OrderedLocationEntities[0].LocationEntity.OverpassId == partWay.OrderedLocationEntities[partWay.OrderedLocationEntities.Count - 1].LocationEntity.OverpassId);
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
                        PolylineChunk chunk = way[way.Count - 1];
                        way.RemoveAt(way.Count - 1);
                        way.Insert(prev + 1, chunk);
                    }
                    way.Insert(prev + 1, partWay);
                }
                else if (prev > next)
                {
                    int index = prev;
                    while (index > 0 && way[index].OrderedLocationEntities[0].LocationEntity.OverpassId == way[index - 1].OrderedLocationEntities[way[index - 1].OrderedLocationEntities.Count - 1].LocationEntity.OverpassId)
                    {
                        index--;
                    }
                    for (int i = prev; i >= index; i--)
                    {
                        PolylineChunk elem = way[prev];
                        way.RemoveAt(prev);
                        way.Insert(next, elem);
                    }
                    way.Insert(next + 1, partWay);
                }
            }
        }

        private void AddElementToWay(List<Element> way, Element partWay)
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
