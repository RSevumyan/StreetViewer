using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StreetViewer.Service;
using StreetViewer.Core;
using StreetViewer.JsonObjects.Common;

using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;

namespace StreetViewer.Interface
{
    public partial class MainForm : Form
    {
        private const string ERROR_MESSAGE = "Введены некорректные данные";
        private const string STREET1_TOOLTIP_MESSAGE = "Введите начальную улицу";
        private const string STREET2_TOOLTIP_MESSAGE = "Введите конечную улицу";
        private const string RESULTLABEL_TEXT = "TIESTO";
        private const string RESULTLABEL_STREETVIEWS_DOWNLOADING = "Идет загрузка панорам";
        private const string RESULTLABEL_STREETVIEWS_SUCCESS = "Панорамы успешно загружены";

        private Controller controller;

        private GMapMarker startStreetMarker;
        private GMapMarker endStreetMarker;
        private GMapRoute route;

        public MainForm()
        {
            InitializeComponent();
            controller = new Controller();
            setToolTipProperties(toolTip1);
            setToolTipProperties(toolTip2);
        }

        // ==============================================================================================================
        // = Implementation
        // ==============================================================================================================


        // 
        // directionRequestButton events
        // 
        private void directionRequestButton_Click(object sender, EventArgs e)
        {
            if (gMap.Overlays[0].Markers[0].IsVisible == false || gMap.Overlays[0].Markers[1].IsVisible == false)
            {
                KeyEventArgs keyEventArgs = new KeyEventArgs(Keys.Enter);
                startStreet_KeyUp(sender, keyEventArgs);
                endStreet_KeyUp(sender, keyEventArgs);
            }

            if (string.IsNullOrEmpty(startStreet.Text) || string.IsNullOrEmpty(endStreet.Text))
            {
                Location location = controller.getGeocoding(startStreet.Text);

                resultLabel.Text = ERROR_MESSAGE;
            }
            else
            {
                resultLabel.Text = RESULTLABEL_TEXT;
                IList<Location> direction = controller.getDirection(startStreet.Text, endStreet.Text);
                drawRoute(getListOfPoinLatLng(direction));
                this.streetViewsRequestButton.Enabled = true;
            }
        }


        // 
        // streetViewsRequestButton events
        //
        private void streetViewsRequestButton_Click(object sender, EventArgs e)
        {

            if (streetVewsFolderDialog.ShowDialog() == DialogResult.OK)
            {
                resultLabel.Text = RESULTLABEL_STREETVIEWS_DOWNLOADING;
                IList<Location> points = getListOfLocation(gMap.Overlays[0].Routes[0].Points);
                Downloader downloader = controller.getStreetViews(points, streetVewsFolderDialog.SelectedPath);

               /* while (downloader.Status < 100)
                {
                    resultLabel.Text = "Загружено " + downloader.Status + "%";
                    //resultLabel.Refresh();
                    //System.Threading.Thread.Sleep(1000);
                }*/
            }
        }

        // 
        // gMap events
        // 
        private void gMap_Load(object sender, EventArgs e)
        {
            gMap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMap.SetPositionByKeywords("Moscow, Russia");
            gMap.ShowCenter = false;
            gMap.DragButton = System.Windows.Forms.MouseButtons.Left;

            GMapOverlay overlay = new GMapOverlay("overlay");
            startStreetMarker = new GMarkerGoogle(new PointLatLng(0, 0), GMarkerGoogleType.blue);
            startStreetMarker.IsVisible = false;
            startStreetMarker.ToolTipText = "Начальная точка";
            overlay.Markers.Add(startStreetMarker);
            endStreetMarker = new GMarkerGoogle(new PointLatLng(0, 0), GMarkerGoogleType.red);
            endStreetMarker.IsVisible = false;
            endStreetMarker.ToolTipText = "Конечная точка";
            overlay.Markers.Add(endStreetMarker);
            route = new GMapRoute("Test route");
            route.Stroke = new Pen(Color.Red, 2);
            overlay.Routes.Add(route);
            gMap.Overlays.Add(overlay);
        }

        // 
        // startStreet TextBox events
        // 
        private void startStreet_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(startStreet, STREET1_TOOLTIP_MESSAGE);
        }

        private void startStreet_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(startStreet);
        }

        private void startStreet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(startStreet.Text))
            {
                Location geoCode = controller.getGeocoding(startStreet.Text);
                if (geoCode != null)
                {
                    PointLatLng position = new PointLatLng(geoCode.Lat, geoCode.Lng);
                    startStreetMarker.Position = position;
                    startStreetMarker.IsVisible = true;
                    route.Points.Clear();
                    this.streetViewsRequestButton.Enabled = false;

                    if (endStreetMarker.Position.Lat == 0 && endStreetMarker.Position.Lng == 0)
                    {
                        gMap.Position = position;
                        gMap.Zoom = 15;
                    }
                    else
                    {
                        calculateZoomAndPosition(startStreetMarker.Position, endStreetMarker.Position);
                    }
                }
                else
                {
                    resultLabel.Text = ERROR_MESSAGE;
                }
                e.Handled = true;
            }
        }

        //
        // endStreet TextBox events
        //
        private void endStreet_MouseHover(object sender, EventArgs e)
        {
            toolTip2.SetToolTip(endStreet, STREET2_TOOLTIP_MESSAGE);
        }

        private void endStreet_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(endStreet);
        }

        private void endStreet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(endStreet.Text))
            {
                Location geoCode = controller.getGeocoding(endStreet.Text);
                if (geoCode != null)
                {
                    PointLatLng position = new PointLatLng(geoCode.Lat, geoCode.Lng);
                    endStreetMarker.Position = position;
                    endStreetMarker.IsVisible = true;
                    route.Points.Clear();
                    this.streetViewsRequestButton.Enabled = false;

                    if (startStreetMarker.Position.Lat == 0 && startStreetMarker.Position.Lng == 0)
                    {
                        gMap.Position = position;
                        gMap.Zoom = 15;

                    }
                    else
                    {
                        calculateZoomAndPosition(startStreetMarker.Position, endStreetMarker.Position);
                    }
                }
                else
                {
                    resultLabel.Text = ERROR_MESSAGE;
                }
                e.Handled = true;
            }
        }

        // 
        // common
        //
        private void setToolTipProperties(System.Windows.Forms.ToolTip toolTip)
        {
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
        }

        private void calculateZoomAndPosition(PointLatLng start, PointLatLng end)
        {
            double height = start.Lat - end.Lat;
            double width = start.Lng - end.Lng;
            double distance = Math.Sqrt(Math.Pow(height, 2) + Math.Pow(width, 2));
            gMap.Zoom = 18 - Math.Round(Math.Log(distance * 60 / 0.06, 2));
            gMap.Position = new PointLatLng(start.Lat - height / 2, start.Lng - width / 2);
        }

        private void drawRoute(IList<PointLatLng> points)
        {
            route.Points.Clear();
            route.Points.AddRange(points);
            gMap.Refresh();
            gMap.Zoom--;
            gMap.Zoom++;
        }

        private IList<PointLatLng> getListOfPoinLatLng(IList<Location> locations)
        {
            List<PointLatLng> points = new List<PointLatLng>();
            foreach (Location location in locations)
            {
                points.Add(new PointLatLng(location.Lat, location.Lng));
            }
            return points;
        }

        private IList<Location> getListOfLocation(IList<PointLatLng> points)
        {
            List<Location> locations = new List<Location>();
            foreach (PointLatLng point in points)
            {
                locations.Add(new Location(point.Lat, point.Lng));
            }
            return locations;
        }
    }
}