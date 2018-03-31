using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Direction
{
    [DataContract]
    public class DirectionsPolylineJson
    {
        private String points;
        [DataMember(Name = "points")]
        public String Points
        {
            get { return points; }
            set { points = value; }
        }
    }
}
