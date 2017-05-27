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

namespace StreetViewer.Interface
{
    internal class GMapForm
    {
        private ObservableCollection<GMapMarker> markers;
        private GMapRoute route;
        private List<GMapRoute> listOfRoutes;
        private GMapControl gMap;

        internal ObservableCollection<GMapMarker> Markers
        {
            get { return markers; }
            set { markers = value; }
        }
        internal GMapRoute Route
        {
            get { return route; }
            set { route = value; }
        }
        internal List<GMapRoute> ListOfRoutes
        {
            get { return listOfRoutes; }
            set { listOfRoutes = value; }
        }

        internal GMapForm(GMapControl gMap)
        {
            this.gMap = gMap;
            gMap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMap.SetPositionByKeywords("Moscow, Russia");
            gMap.ShowCenter = false;
            gMap.DragButton = System.Windows.Forms.MouseButtons.Left;

            GMapOverlay directionOverlay = new GMapOverlay("DirectionOverlay");
            markers = directionOverlay.Markers;
            route = new GMapRoute("Test route");
            route.Stroke = new Pen(Color.Red, 2);
            directionOverlay.Routes.Add(route);
            gMap.Overlays.Add(directionOverlay);

            GMapOverlay allDirectionsOverlay = new GMapOverlay("AllDirectionsOverlay");
            gMap.Overlays.Add(allDirectionsOverlay);

            GMapOverlay circleOverlay = new GMapOverlay("CircleOverlay");
            gMap.Overlays.Add(circleOverlay);

            listOfRoutes = new List<GMapRoute>();
        }

        internal void calculateZoomAndPosition()
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

        internal void drawRoute(IList<PointLatLng> points)
        {
            route.Points.Clear();
            route.Points.AddRange(points);
            gMap.Refresh();
            gMap.Zoom--;
            gMap.Zoom++;
        }

        internal void drawListOfRoutes(List<IList<PointLatLng>> listOfDirections)
        {
            gMap.Overlays[1].Routes.Clear();
            listOfRoutes = new List<GMapRoute>();
            foreach (List<PointLatLng> points in listOfDirections)
            {
                GMapRoute localRoute = new GMapRoute("Test route");
                localRoute.Stroke = new Pen(Color.Red, 2);
                localRoute.Points.AddRange(points);
                listOfRoutes.Add(localRoute);
                gMap.Overlays[1].Routes.Add(localRoute);
            }
            gMap.Refresh();
            gMap.Zoom--;
            gMap.Zoom++;
        }

        internal void addMarkerByKeyUp(PointLatLng position, bool isStartMarker)
        {
            if (markers.Count == 0)
            {
                markers.Add(getStartMarker(position));

                if (!isStartMarker)
                {
                    markers.Add(getEndMarker(position));
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
                    markers.Add(getEndMarker(position));
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

        internal void mouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                route.Clear();
                double lat = gMap.FromLocalToLatLng(e.X, e.Y).Lat;
                double lng = gMap.FromLocalToLatLng(e.X, e.Y).Lng;
                addMarkerByMouseClick(new PointLatLng(lat, lng));
                gMap.Zoom--;
                gMap.Zoom++;
            }
        }

        internal void onMarkerClick(GMapMarker item)
        {
            route.Clear();
            markers.Remove(item);
            List<PointLatLng> oldMarkers = new List<PointLatLng>();
            foreach (GMapMarker marker in markers)
            {
                oldMarkers.Add(marker.Position);
            }
            markers.Clear();
            foreach (PointLatLng position in oldMarkers)
            {
                addMarkerByMouseClick(position);
            }
            gMap.Zoom--;
            gMap.Zoom++;
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================

        private void addMarkerByMouseClick(PointLatLng position)
        {
            if (markers.Count == 0)
            {
                markers.Add(getStartMarker(position));
            }
            else
            {
                if (markers.Count > 1)
                {
                    int count = markers.Count - 1;
                    markers[count].ToolTipText = "Промежуточная точка № " + count;
                }
                markers.Add(getEndMarker(position));
            }
        }

        private GMapMarker getStartMarker(PointLatLng position)
        {
            GMapMarker startMarker = new GMarkerGoogle(position, GMarkerGoogleType.blue);
            startMarker.ToolTipText = "Начальная точка";
            return startMarker;
        }


        private GMapMarker getEndMarker(PointLatLng position)
        {
            GMapMarker endMarker = new GMarkerGoogle(position, GMarkerGoogleType.red);
            endMarker.ToolTipText = "Конечная точка";
            return endMarker;
        }
    }
}
