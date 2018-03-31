using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Common;

namespace PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Geocoding
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
