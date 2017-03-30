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
        private const string ERROR_MESSAGE = "Введены некорректные данные";

        private Controller controller;

        public MainForm()
        {
            InitializeComponent();
            controller = new Controller();
            setToolTipProperties(toolTip1);
            setToolTipProperties(toolTip2);
        }

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
            gMap.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMap.SetPositionByKeywords("Moscow, Russia");
        }

        private void startStreet_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(startStreet, "Введите начальную улицу");
        }

        private void startStreet_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(startStreet);
        }

        private void endStreet_MouseHover(object sender, EventArgs e)
        {
            toolTip2.SetToolTip(endStreet, "Введите конечную улицу");
        }

        private void endStreet_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(endStreet);
        }

        private void setToolTipProperties(System.Windows.Forms.ToolTip toolTip)
        {
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
        }
    }
}
