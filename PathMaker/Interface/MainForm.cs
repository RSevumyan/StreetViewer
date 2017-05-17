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
        private GMapForm gMapForm;

        public MainForm()
        {
            InitializeComponent();
            controller = new Controller();
            setToolTipProperties(toolTip1);
            setToolTipProperties(toolTip2);
            orderInput.Value = controller.Parameters.Order;
            radiusUpDown.Value = controller.Parameters.Radius;
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
                if (gMapForm.Markers.Count > 1 && gMapForm.Markers[0].IsVisible)
                {
                    IList<PointLatLng> points = new List<PointLatLng>();
                    for (int i = 0; i < gMapForm.Markers.Count - 1; i++)
                    {
                        string start = getStringOfLocation(gMapForm.Markers[i].Position);
                        string end = getStringOfLocation(gMapForm.Markers[i + 1].Position);
                        IList<PointLatLng> partOfPoints = getListOfPoinLatLng(controller.getDirection(start, end));
                        foreach (PointLatLng point in partOfPoints)
                        {
                            points.Add(point);
                        }
                    }
                    gMapForm.drawRoute(points);
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
                    gMapForm.drawRoute(getListOfPoinLatLng(direction));
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
        // allDirectionsButtonEvents
        //
        private void allDirectionsButton_Click(object sender, EventArgs e)
        {
            if (gMapForm.Markers[0].IsVisible)
            {
                try
                {
                    string lat = coordinateToString(gMapForm.Markers[0].Position.Lat);
                    string lng = coordinateToString(gMapForm.Markers[0].Position.Lng);
                    List<List<Location>> listOfDirections = controller.getAllDirectionsOfArea(lat, lng);

                    List<IList<PointLatLng>> listOfPoints = new List<IList<PointLatLng>>();
                    foreach (List<Location> direction in listOfDirections)
                    {
                        IList<PointLatLng> localPoints = getListOfPoinLatLng(direction);
                        listOfPoints.Add(localPoints);
                    }
                    gMapForm.drawListOfRoutes(listOfPoints);
                    if (gMapForm.ListOfRoutes.Count > 0)
                    {
                        streetViewsRequestButton.Enabled = true;
                    }
                }
                catch (WebException we)
                {
                    resultLabel.Text = WEB_ERROR_MESSAGE;
                }
            }
        }

        // 
        // streetViewsRequestButton events
        //
        private void streetViewsRequestButton_Click(object sender, EventArgs e)
        {
            List<List<Location>> list = new List<List<Location>>();
            streetVewsFolderDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                if (streetVewsFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    if (gMap.Overlays[0].Routes[0].Points.Count > 0)
                    {
                        List<Location> points = getListOfLocation(gMap.Overlays[0].Routes[0].Points);
                        list.Add(points);
                    }
                   
                    string path = streetVewsFolderDialog.SelectedPath + "\\";

                    foreach (GMapRoute route in gMapForm.ListOfRoutes)
                    {
                        list.Add(getListOfLocation(route.Points));
                    }
                    downloader = controller.getStreetViews(list, path);
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
            if (orderInput.Value != 0)
            {
                controller.Parameters.Order = (int)orderInput.Value;
            }

            if (radiusUpDown.Value != 0)
            {
                controller.Parameters.Radius = (int)radiusUpDown.Value;
            }
        }

        // 
        // gMap events
        // 
        private void gMap_Load(object sender, EventArgs e)
        {
            gMapForm = new GMapForm(gMap);
        }

        private void gMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            gMapForm.mouseDoubleClick(e);
        }

        private void gMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            gMapForm.onMarkerClick(item);
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
                        gMapForm.addMarkerByKeyUp(position, true);
                        gMapForm.Route.Points.Clear();
                        this.streetViewsRequestButton.Enabled = false;
                        gMapForm.calculateZoomAndPosition();
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
                        gMapForm.addMarkerByKeyUp(position, false);
                        gMapForm.Route.Points.Clear();
                        this.streetViewsRequestButton.Enabled = false;
                        gMapForm.calculateZoomAndPosition();
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
        // common
        //
        private void setToolTipProperties(System.Windows.Forms.ToolTip toolTip)
        {
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
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

        private List<Location> getListOfLocation(IList<PointLatLng> points)
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