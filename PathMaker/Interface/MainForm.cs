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

namespace StreetViewer.Interface
{
    public partial class MainForm : Form
    {
        private const string ERROR_MESSAGE = "Введены некорректные\r\n данные";
        private Controller controller;

        public MainForm()
        {
            InitializeComponent();
            controller = new Controller();
        }

        private void requestButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(streetTextBox.Text))
            {
                resultLabel.Text = ERROR_MESSAGE;
            }
            else
            {
                string result = controller.getGeocoding(streetTextBox.Text);
                if (string.IsNullOrEmpty(result))
                {
                    resultLabel.Text = ERROR_MESSAGE;
                }
                else
                {
                    resultLabel.Text = result;
                }
            }
        }

        private void requestButton2_Click(object sender, EventArgs e)
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
            gMap.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMap.SetPositionByKeywords("Moscow, Russia");
        }
    }
}
