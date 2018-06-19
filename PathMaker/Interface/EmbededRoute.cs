using GMap.NET;
using GMap.NET.WindowsForms;
using PathFinder.DatabaseService.Model;
using PathFinder.DataBaseService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PathFinder.Interface
{
    internal class EmbededRoute
    {
        private Color color1;
        private Color color2;
        string routeType;
        private GMapOverlay overlay;
        Dictionary<string, HashSet<long>> roadDictionary;
        Dictionary<long, GMapRoute> chunkDictionary;

        internal EmbededRoute(GMapOverlay overlay, Color color1, Color color2, string routeType)
        {
            this.overlay = overlay;
            roadDictionary = new Dictionary<string, HashSet<long>>();
            chunkDictionary = new Dictionary<long, GMapRoute>();
            this.color1 = color1;
            this.color2 = color2;
            this.routeType = routeType;
        }

        internal List<long> GetChunksIds()
        {
            return chunkDictionary.Select(d => d.Key).ToList();
        }

        internal void AddRoad(Road road)
        {
            if (roadDictionary.Keys.Contains(road.Name))
            {
                HashSet<long> routeDictionary = roadDictionary[road.Name];
                foreach (PolylineChunk chunk in road.PolylineChunks)
                {
                    if (!chunkDictionary.Keys.Contains(chunk.OverpassId))
                    {
                        GMapRoute route = GetRouteForChunk(chunk);
                        overlay.Routes.Add(route);
                        chunkDictionary.Add(chunk.OverpassId, route);
                        routeDictionary.Add(chunk.OverpassId);
                    }
                }
            }
            else
            {
                roadDictionary.Add(road.Name, new HashSet<long>());
                foreach (PolylineChunk chunk in road.PolylineChunks)
                {
                    GMapRoute route = GetRouteForChunk(chunk);
                    overlay.Routes.Add(route);
                    chunkDictionary.Add(chunk.OverpassId, route);
                    roadDictionary[road.Name].Add(chunk.OverpassId);
                }
            }
        }

        internal void AddPolylineChunks(string roadName, List<PolylineChunk> chunks)
        {
            if (roadDictionary.Keys.Contains(roadName))
            {
                HashSet<long> routeDictionary = roadDictionary[roadName];
                foreach (PolylineChunk chunk in chunks)
                {
                    if (!chunkDictionary.Keys.Contains(chunk.OverpassId))
                    {
                        GMapRoute route = GetRouteForChunk(chunk);
                        overlay.Routes.Add(route);
                        chunkDictionary.Add(chunk.OverpassId, route);
                        routeDictionary.Add(chunk.OverpassId);
                    }
                }
            }
            else
            {
                roadDictionary.Add(roadName, new HashSet<long>());
                foreach (PolylineChunk chunk in chunks)
                {
                    GMapRoute route = GetRouteForChunk(chunk);
                    overlay.Routes.Add(route);
                    chunkDictionary.Add(chunk.OverpassId, route);
                    roadDictionary[roadName].Add(chunk.OverpassId);
                }
            }
        }

        internal GMapRoute GetRouteForChunk(PolylineChunk chunk)
        {
            GMapRoute route = new GMapRoute(chunk.OverpassId.ToString());
            if("MainRoute".Equals(routeType) && chunk.IsStreetViewsDownloaded)
            {
                route.Stroke = new Pen(color2, 2);
            }
            else if ("MiniRoute".Equals(routeType) && chunk.IsSignDetected)
            {
                route.Stroke = new Pen(color2, 2);
            }
            else
            {
                route.Stroke = new Pen(color1, 2);
            }
            route.Points.AddRange(GetListOfPoinLatLng(chunk.OrderedLocationEntities.OrderBy(i => i.Order).Select(x => x.LocationEntity).ToList()));
            return route;
        }

        internal void RemoveRoad(string name)
        {
            foreach (long id in roadDictionary[name])
            {
                overlay.Routes.Remove(chunkDictionary[id]);
                chunkDictionary.Remove(id);
            }
            roadDictionary.Remove(name);
        }

        internal void HighlightRoadByViewsDownloadStatus(string name)
        {
            GeographiData geoData = GeographiData.Instance;
            foreach (long id in roadDictionary[name])
            {
                if (geoData.Chunks[id].IsStreetViewsDownloaded)
                {
                    chunkDictionary[id].Stroke.Color = Color.DarkRed;
                }
                else
                {
                    chunkDictionary[id].Stroke.Color = Color.Red;
                }
            }
        }

        internal void HighlightRoadBySignDetectedStatus(string name)
        {
            GeographiData geoData = GeographiData.Instance;
            foreach (long id in roadDictionary[name])
            {
                if (geoData.Chunks[id].IsSignDetected)
                {
                    chunkDictionary[id].Stroke.Color = Color.DarkRed;
                }
                else
                {
                    chunkDictionary[id].Stroke.Color = Color.Red;
                }
            }
        }

        internal void DeHighlightRoad(string name, bool completed)
        {
            Color color = completed ? color2 : color1;
            foreach (long id in roadDictionary[name])
            {
                chunkDictionary[id].Stroke.Color = color;
            }
        }

        internal void DeHighlightPolylineChunk(long chunkOverpassId, bool completed)
        {
            Color color = completed ? color2 : color1;
            chunkDictionary[chunkOverpassId].Stroke.Color = color;
        }

        internal void Clear()
        {
            roadDictionary.Clear();
            overlay.Routes.Clear();
        }

        private List<PointLatLng> GetListOfPoinLatLng(List<LocationEntity> locations)
        {
            List<PointLatLng> list = new List<PointLatLng>();
            foreach (LocationEntity location in locations)
            {
                list.Add(new PointLatLng(location.Lat, location.Lng));
            }
            return list;
        }
    }
}
