using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StreetViewer.JsonObjects.Geocoding
{ 
    [DataContract]
    public class Geometry
    {

        private Location location;
        [DataMember (Name = "location")]
        public Location Location
        {
            get { return location; }
            set { location = value; }
        }

    }
}
