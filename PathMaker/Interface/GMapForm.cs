using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using GMap.NET.ObjectModel;
using PathFinder.StreetViewing;
using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Common;

namespace PathFinder.Interface
{
    internal class GMapForm
    {
        private ObservableCollection<GMapMarker> markers;
        private EmbededRoute dbRoute;
        private EmbededRoute addedRoute;
        private GMapControl gMap;

        internal ObservableCollection<GMapMarker> Markers
        {
            get { return markers; }
            set { markers = value; }
        }

        internal IList<PolylineChunk> AddedChunks
        {
            get { return this.addedRoute.GetChunks(); }
        }

        internal GMapForm(GMapControl gMap)
        {
            this.gMap = gMap;
            gMap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMap.SetPositionByKeywords("Moscow, Russia");
            gMap.ShowCenter = false;
            gMap.DragButton = System.Windows.Forms.MouseButtons.Left;

            //Preparing overlay for routes from database
            GMapOverlay historyOverlay = new GMapOverlay("HistoryOverlay");
            dbRoute = new EmbededRoute(historyOverlay, "Routes in Database", Color.DarkBlue);
            gMap.Overlays.Add(historyOverlay);

            //Preparing overlay for current session routes
            GMapOverlay currentOverlay = new GMapOverlay("CurrentSessionOverlay");
            markers = currentOverlay.Markers;
            addedRoute = new EmbededRoute(currentOverlay, "New routes", Color.Green);
            gMap.Overlays.Add(currentOverlay);
        }

        internal void CalculateZoomAndPosition()
        {
            if (markers.Count == 1)
            {
                gMap.Position = markers[0].Position;
                gMap.Zoom = 15;
            }
            else if (markers.Count > 1)
            {
                PointLatLng start = markers[0].Position;
                PointLatLng end = markers[markers.Count - 1].Position;
                double height = start.Lat - end.Lat;
                double width = start.Lng - end.Lng;
                double distance = Math.Sqrt(Math.Pow(height, 2) + Math.Pow(width, 2));
                gMap.Zoom = 18 - Math.Round(Math.Log(distance * 60 / 0.06, 2));
                gMap.Position = new PointLatLng(start.Lat - height / 2, start.Lng - width / 2);
            }
        }

        internal void DrawRoute(IList<PolylineChunk> routeChunks)
        {
            addedRoute.AddChunks(routeChunks);
            RefreshGMap();
        }

        internal void DrawDbRoute(List<PolylineChunk> routeChunks)
        {
            dbRoute.AddChunks(routeChunks);
            RefreshGMap();
        }

        internal void ShiftToDbRoute(List<PolylineChunk> chunks)
        {
            dbRoute.AddChunks(chunks);
            addedRoute.RemoveChunks(chunks);
            RefreshGMap();
        }

        internal void ClearAddedRoute()
        {
            addedRoute.Clear();
            RefreshGMap();
        }

        internal void AddMarkerByKeyUp(PointLatLng position, bool isStartMarker)
        {
            if (markers.Count == 0)
            {
                markers.Add(GetStartMarker(position));

                if (!isStartMarker)
                {
                    markers.Add(GetEndMarker(position));
                    markers[0].IsVisible = false;
                }
            }
            else if (markers.Count == 1)
            {
                if (isStartMarker)
                {
                    markers[0].Position = position;
                    markers[0].IsVisible = true;
                }
                else
                {
                    markers.Add(GetEndMarker(position));
                }
            }
            else
            {
                if (isStartMarker)
                {
                    markers[0].Position = position;
                    markers[0].IsVisible = true;
                }
                else
                {
                    markers[markers.Count - 1].Position = position;
                }
            }
        }

        internal void MouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                double lat = gMap.FromLocalToLatLng(e.X, e.Y).Lat;
                double lng = gMap.FromLocalToLatLng(e.X, e.Y).Lng;
                AddMarkerByMouseClick(new PointLatLng(lat, lng));
            }
        }

        internal void OnMarkerClick(GMapMarker item)
        {
            markers.Remove(item);
            List<PointLatLng> oldMarkers = new List<PointLatLng>();
            foreach (GMapMarker marker in markers)
            {
                oldMarkers.Add(marker.Position);
            }
            markers.Clear();
            foreach (PointLatLng position in oldMarkers)
            {
                AddMarkerByMouseClick(position);
            }
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

        private void RefreshGMap()
        {
            gMap.BeginInvoke((MethodInvoker)(() => gMap.Refresh()));
        }


        private void AddMarkerByMouseClick(PointLatLng position)
        {
            if (markers.Count == 0)
            {
                markers.Add(GetStartMarker(position));
            }
            else
            {
                if (markers.Count > 1)
                {
                    int count = markers.Count - 1;
                    markers[count].ToolTipText = "Промежуточная точка № " + count;
                }
                markers.Add(GetEndMarker(position));
            }
        }

        private GMapMarker GetStartMarker(PointLatLng position)
        {
            GMapMarker startMarker = new GMarkerGoogle(position, GMarkerGoogleType.blue);
            startMarker.ToolTipText = "Начальная точка";
            return startMarker;
        }


        private GMapMarker GetEndMarker(PointLatLng position)
        {
            GMapMarker endMarker = new GMarkerGoogle(position, GMarkerGoogleType.red);
            endMarker.ToolTipText = "Конечная точка";
            return endMarker;
        }

        internal class EmbededRoute
        {
            private string routeDesk;
            private Color color;
            List<Tuple<PolylineChunk, GMapRoute>> listOfRoadsPair;
            private GMapOverlay overlay;

            internal EmbededRoute(GMapOverlay overlay, string routeDesc, Color color)
            {
                listOfRoadsPair = new List<Tuple<PolylineChunk, GMapRoute>>();
                this.routeDesk = routeDesc;
                this.color = color;
                this.overlay = overlay;
            }

            internal IList<PolylineChunk> GetChunks()
            {
                return listOfRoadsPair.Select(x => x.Item1).ToList();
            }

            internal void AddChunks(IList<PolylineChunk> chunks)
            {
                foreach (PolylineChunk chunk in chunks)
                {
                    GMapRoute route = new GMapRoute(routeDesk);
                    route.Stroke = new Pen(color, 2);
                    route.Points.AddRange(GetListOfPoinLatLng(chunk));
                    overlay.Routes.Add(route);
                    listOfRoadsPair.Add(new Tuple<PolylineChunk, GMapRoute>(chunk, route));
                }
            }

            internal void RemoveChunks(IList<PolylineChunk> chunks)
            {
                foreach (PolylineChunk chunk in chunks)
                {
                    Tuple<PolylineChunk, GMapRoute> tuple = listOfRoadsPair.Find(x => x.Item1.Equals(chunk));
                    overlay.Routes.Remove(tuple.Item2);
                    listOfRoadsPair.Remove(tuple);
                }
            }

            internal void Clear()
            {
                listOfRoadsPair.Clear();
                overlay.Routes.Clear();
            }

            private List<PointLatLng> GetListOfPoinLatLng(PolylineChunk polylineChunks)
            {
                List<PointLatLng> points = new List<PointLatLng>();
                points.AddRange(ConvertToPointLatLng(polylineChunks.LocationEntities));
                return points;
            }

            private List<PointLatLng> ConvertToPointLatLng(List<LocationEntity> locations)
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
}
