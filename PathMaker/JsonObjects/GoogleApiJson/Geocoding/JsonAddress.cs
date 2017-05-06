using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StreetViewer.JsonObjects.GoogleApiJson.Geocoding
{
    [DataContract]
    public class JsonAddress
    {
        private Geometry geometry;
        [DataMember(Name="geometry")]
        public Geometry Geometry
        {
            get { return geometry; }
            set { geometry = value; }
        }

    }
}
