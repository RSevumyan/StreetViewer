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

        private Controller controller;
        private GMapOverlay markers;
        private GMapMarker startStreetMarker;
        private GMapMarker endStreetMarker;

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

        private void directionRequestButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(startStreet.Text) || string.IsNullOrEmpty(endStreet.Text))
            {
                resultLabel.Text = ERROR_MESSAGE;
            }
            else
            {
                resultLabel.Text = "Загружается...";
                bool result = controller.getDirection(startStreet.Text, endStreet.Text);
                if (result)
                {
                    resultLabel.Text = "Загрузка завершена успешно";
                }
                else
                {
                    resultLabel.Text = ERROR_MESSAGE;
                }
            }
        }

        private void gMap_Load(object sender, EventArgs e)
        {
            gMap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMap.SetPositionByKeywords("Moscow, Russia");
            gMap.ShowCenter = false;
            gMap.DragButton = System.Windows.Forms.MouseButtons.Left;

            markers = new GMapOverlay("markers");
        }

        private void setToolTipProperties(System.Windows.Forms.ToolTip toolTip)
        {
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
        }

        // ==============================================================================================================
        // = startStreet TextBox events
        // ==============================================================================================================

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
                double[] geoCode = controller.getGeocoding(startStreet.Text);
                if (geoCode != null)
                {
                    PointLatLng point = new PointLatLng(geoCode[0], geoCode[1]);
                    gMap.Position = point;

                    startStreetMarker = new GMarkerGoogle(point, GMarkerGoogleType.blue);
                    markers.Markers.Add(startStreetMarker);
                    gMap.Overlays.Add(markers);

                    if (endStreetMarker == null)
                    {
                        gMap.Zoom = 15;
                    }
                    else
                    {
                        calculateZoom(startStreetMarker.Position, endStreetMarker.Position);
                    }
                }
                else
                {
                    resultLabel.Text = ERROR_MESSAGE;
                }
                e.Handled = true;
            }
        }

        // ==============================================================================================================
        // = endStreet TextBox events
        // ==============================================================================================================

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
                double[] geoCode = controller.getGeocoding(endStreet.Text);
                if (geoCode != null)
                {
                    PointLatLng point = new PointLatLng(geoCode[0], geoCode[1]);
                    gMap.Position = point;

                    endStreetMarker = new GMarkerGoogle(point, GMarkerGoogleType.red);
                    markers.Markers.Add(endStreetMarker);
                    gMap.Overlays.Add(markers);

                    if (endStreetMarker == null)
                    {
                        gMap.Zoom = 15;
                    }
                    else
                    {
                        calculateZoom(startStreetMarker.Position, endStreetMarker.Position);
                    }
                }
                else
                {
                    resultLabel.Text = ERROR_MESSAGE;
                }
                e.Handled = true;
            }
        }

        private void calculateZoom(PointLatLng start, PointLatLng end)
        {
            double height = start.Lat - end.Lat;
            double width = start.Lng - end.Lng;
            double distance = Math.Sqrt(Math.Pow(height, 2) + Math.Pow(width, 2));
            gMap.Zoom = 20 - Math.Round(Math.Log(distance * 60 / 0.06, 2));
            gMap.Position = new PointLatLng(start.Lat - height / 2, start.Lng - width / 2);
        }
    }
}
