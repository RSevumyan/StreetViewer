using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StreetViewer.Service;
using StreetViewer.JsonObjects;

namespace StreetViewer.Interface
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RestService service = new RestService();

        private void requestButton_Click(object sender, EventArgs e)
        {
            GeocodeJsonReply reply = service.GetGeocoding(streetTextBox.Text);
            resultLabel.Text = reply.Results[0].Geometry.Location.Lat +";\t"+ reply.Results[0].Geometry.Location.Lng;
        }

    }
}
