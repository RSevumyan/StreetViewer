using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using GMap.NET.ObjectModel;
using PathFinder.DatabaseService.Model;

namespace PathFinder.Interface
{
    internal class GMapForm
    {
        private ObservableCollection<GMapMarker> markers;
        private ObservableCollection<GMapMarker> mapMiniMarkers;
        private EmbededRoute mainMapRoute;
        private EmbededRoute miniMapRoute;
        private GMapControl gMap;
        internal GMapControl gMapMini;

        internal ObservableCollection<GMapMarker> Markers
        {
            get { return markers; }
            set { markers = value; }
        }

        internal GMapForm(GMapControl gMap, GMapControl gMapMini)
        {
            this.gMap = gMap;
            gMap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMap.SetPositionByKeywords("Moscow, Russia");
            gMap.ShowCenter = false;
            gMap.DragButton = MouseButtons.Left;

            //Preparing overlay for routes from database
            GMapOverlay historyOverlay = new GMapOverlay("HistoryOverlay");
            mainMapRoute = new EmbededRoute(historyOverlay, Color.Blue, Color.Orange, "MainRoute");
            gMap.Overlays.Add(historyOverlay);

            //Preparing overlay for current session routes
            GMapOverlay currentOverlay = new GMapOverlay("CurrentSessionOverlay");
            markers = currentOverlay.Markers;
            gMap.Overlays.Add(currentOverlay);

            this.gMapMini = gMapMini;
            gMapMini.MapProvider = GoogleMapProvider.Instance;
            gMapMini.SetPositionByKeywords("Moscow, Russia");
            gMapMini.ShowCenter = false;
            gMapMini.DragButton = MouseButtons.Left;

            GMapOverlay gmapMiniOverlay = new GMapOverlay("GmapMiniOverlay");
            mapMiniMarkers = gmapMiniOverlay.Markers;
            miniMapRoute = new EmbededRoute(gmapMiniOverlay, Color.Green, Color.Orange, "MiniRoute");
            gMapMini.Overlays.Add(gmapMiniOverlay);
            
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

        internal EmbededRoute MainMapRoute { get { return mainMapRoute; } }

        internal EmbededRoute MiniMapRoute { get { return miniMapRoute; } }

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
                if(markers.Count > 1)
                {
                    markers.RemoveAt(1);
                }
                double lat = gMap.FromLocalToLatLng(e.X, e.Y).Lat;
                double lng = gMap.FromLocalToLatLng(e.X, e.Y).Lng;
                AddMarkerByMouseClick(markers, new PointLatLng(lat, lng));
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
                AddMarkerByMouseClick(markers, position);
            }
        }

        internal void RefreshGMaps()
        {
            RefreshGMapMain();
            RefreshGMapMini();
        }

        internal void RefreshGMapMain()
        {
            gMap.BeginInvoke((MethodInvoker)(() => gMap.Refresh()));
        }

        internal void RefreshGMapMini()
        {
            gMapMini.BeginInvoke((MethodInvoker)(() => gMapMini.Refresh()));
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

        private void AddMarkerByMouseClick(ObservableCollection<GMapMarker> markers, PointLatLng position)
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
    }
}
