using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StreetViewer.JsonObjects.GoogleApiJson.Common
{
    [DataContract]
    public class Location
    {

        public Location(double lat, double lng)
        {
            this.Lat = lat;
            this.Lng = lng;
        }

        private double lat;
        [DataMember(Name = "lat")]
        public double Lat
        {
            get { return lat; }
            set { lat = value; }
        }

        private double lng;
        [DataMember(Name = "lng")]
        public double Lng
        {
            get { return lng; }
            set { lng = value; }
        }
    }
}
