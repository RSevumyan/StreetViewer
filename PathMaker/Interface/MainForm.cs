using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StreetViewer.Service;
using StreetViewer.JsonObjects.Geocoding;
using StreetViewer.Core;

namespace StreetViewer.Interface
{
    public partial class MainForm : Form
    {
        private Controller controller;
        public MainForm()
        {
            InitializeComponent();
            controller = new Controller();
        }

        private void requestButton_Click(object sender, EventArgs e)
        {  
            Location location = controller.getGeocoding(streetTextBox.Text);
            resultLabel.Text = location.Lat + ";\t" + location.Lng;
        }
    }
}
