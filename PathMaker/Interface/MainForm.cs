using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;

using StreetViewer.Service;
using StreetViewer.Core;
using StreetViewer.JsonObjects.GoogleApiJson.Common;

using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using GMap.NET.ObjectModel;

namespace StreetViewer.Interface
{
    delegate void StringArgReturningVoidDelegate(string text);
    delegate void BoolArgReturningVoidDelegate(bool availability);

    public partial class MainForm : Form
    {
        private const string ERROR_MESSAGE = "Введены некорректные данные";
        private const string WEB_ERROR_MESSAGE = "Ошибка соединения";
        private const string STREET1_TOOLTIP_MESSAGE = "Введите начальную улицу";
        private const string STREET2_TOOLTIP_MESSAGE = "Введите конечную улицу";
        private const string RESULTLABEL_STREETVIEWS_DOWNLOADING = "Идет загрузка панорам";
        private const string RESULTLABEL_STREETVIEWS_SUCCESS = "Панорамы успешно загружены";

        private Controller controller;
        private Downloader downloader;
        private ObservableCollection<GMapMarker> markers;
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
            try
            {
                if (markers.Count > 1 && markers[0].IsVisible)
                {
                    IList<PointLatLng> points = new List<PointLatLng>();
                    for (int i = 0; i < markers.Count - 1; i++)
                    {
                        string start = getStringOfLocation(markers[i].Position);
                        string end = getStringOfLocation(markers[i + 1].Position);
                        IList<PointLatLng> partOfPoints = getListOfPoinLatLng(controller.getDirection(start, end));
                        foreach (PointLatLng point in partOfPoints)
                        {
                            points.Add(point);
                        }
                    }
                    drawRoute(points);
                    if (downloader == null)
                    {
                        this.streetViewsRequestButton.Enabled = true;
                    }
                }
                else if (!string.IsNullOrEmpty(startStreet.Text) && !string.IsNullOrEmpty(endStreet.Text))
                {
                    KeyEventArgs keyEventArgs = new KeyEventArgs(Keys.Enter);
                    startStreet_KeyUp(sender, keyEventArgs);
                    endStreet_KeyUp(sender, keyEventArgs);
                    IList<Location> direction = controller.getDirection(startStreet.Text, endStreet.Text);
                    drawRoute(getListOfPoinLatLng(direction));
                    if (downloader == null)
                    {
                        this.streetViewsRequestButton.Enabled = true;
                    }
                }
                else
                {
                    resultLabel.Text = ERROR_MESSAGE;
                }
            }
            catch (WebException ex)
            {
                resultLabel.Text = WEB_ERROR_MESSAGE;
            }
        }


        // 
        // streetViewsRequestButton events
        //
        private void streetViewsRequestButton_Click(object sender, EventArgs e)
        {
            streetVewsFolderDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                if (streetVewsFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    resultLabel.Text = RESULTLABEL_STREETVIEWS_DOWNLOADING;
                    IList<Location> points = getListOfLocation(gMap.Overlays[0].Routes[0].Points);
                    string path = streetVewsFolderDialog.SelectedPath + "\\";

                    if (string.IsNullOrEmpty(startStreet.Text))
                    {
                        path += points[0].Lat + "_" + points[0].Lng + ";";
                    }
                    else
                    {
                        string startPath = Regex.Replace(startStreet.Text, @"[$""#+\\|/<>?{}&%*]", "");
                        startPath = Regex.Replace(startPath, @"\s", "_");
                        path += startPath + ";";
                    }

                    if (string.IsNullOrEmpty(endStreet.Text))
                    {
                        path += points[points.Count - 1].Lat + "_" + points[points.Count - 1].Lng;
                    }
                    else
                    {
                        string endPath = Regex.Replace(endStreet.Text, @"[$""#+\\|/<>?{}&%*]", "");
                        endPath = Regex.Replace(endPath, @"\s", "_");
                        path += endPath;
                    }

                    downloader = controller.getStreetViews(points, path);
                    Thread downloadStatusThread = new Thread(updateStatus);
                    downloadStatusThread.Start();
                    streetViewsRequestButton.Enabled = false;
                }
            }
            catch (WebException ex)
            {
                resultLabel.Text = WEB_ERROR_MESSAGE;
            }
        }


        // 
        // settingsButton events
        //
        private void settingsButton_Click(object sender, EventArgs e)
        {
            if (orderInput.Value != 0 && radiusUpDown.Value != 0)
            {
                controller.setParams((int)orderInput.Value, (int)radiusUpDown.Value);
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
            markers = overlay.Markers;
            route = new GMapRoute("Test route");
            route.Stroke = new Pen(Color.Red, 2);
            overlay.Routes.Add(route);
            gMap.Overlays.Add(overlay);
        }

        private void gMap_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void gMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
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
                try
                {
                    Location geoCode = controller.getGeocoding(startStreet.Text);

                    if (geoCode != null)
                    {
                        PointLatLng position = new PointLatLng(geoCode.Lat, geoCode.Lng);
                        addMarkerByKeyUp(position, true);
                        route.Points.Clear();
                        this.streetViewsRequestButton.Enabled = false;
                        calculateZoomAndPosition();
                    }
                    else
                    {
                        resultLabel.Text = ERROR_MESSAGE;
                    }
                    e.Handled = true;
                }
                catch (WebException ex)
                {
                    resultLabel.Text = WEB_ERROR_MESSAGE;
                }
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
                try
                {
                    Location geoCode = controller.getGeocoding(endStreet.Text);
                    if (geoCode != null)
                    {
                        PointLatLng position = new PointLatLng(geoCode.Lat, geoCode.Lng);
                        addMarkerByKeyUp(position, false);
                        route.Points.Clear();
                        this.streetViewsRequestButton.Enabled = false;
                        calculateZoomAndPosition();
                    }
                    else
                    {
                        resultLabel.Text = ERROR_MESSAGE;
                    }
                    e.Handled = true;
                }
                catch (WebException ex)
                {
                    resultLabel.Text = WEB_ERROR_MESSAGE;
                }
            }
        }


        // 
        // allDirectionsButtonEvents
        //
        private void allDirectionsButton_Click(object sender, EventArgs e)
        {
            //List<List<Location>> listOfDirections = controller.getAllDirectionsOfArea()
            if (markers[0].IsVisible)
            {
                string lat = coordinateToString(markers[0].Position.Lat);
                string lng = coordinateToString(markers[0].Position.Lng);
                List<List<Location>> listOfDirections = controller.getAllDirectionsOfArea(lat, lng);
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

        private void calculateZoomAndPosition()
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

        private void updateStatus()
        {
            while (downloader.Status < 100)
            {
                this.setText("Загружено " + downloader.Status + "%");
                System.Threading.Thread.Sleep(1000);
            }

            this.setText("Загрузка завершена");
            downloader = null;
            this.setAvailability(true);
        }

        private void setText(string text)
        {
            if (this.resultLabel.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(setText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.resultLabel.Text = text;
            }
        }

        private void setAvailability(bool availability)
        {
            if (this.streetViewsRequestButton.InvokeRequired)
            {
                BoolArgReturningVoidDelegate d = new BoolArgReturningVoidDelegate(setAvailability);
                this.Invoke(d, new object[] { availability });
            }
            else
            {
                this.streetViewsRequestButton.Enabled = availability;
            }
        }

        private void addMarkerByKeyUp(PointLatLng position, bool isStartMarker)
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

        private string getStringOfLocation(PointLatLng point)
        {
            return coordinateToString(point.Lat) + "," + coordinateToString(point.Lng);
        }

        private string coordinateToString(double coordinate)
        {
            return coordinate.ToString().Replace(",", ".");
        }
    }
}