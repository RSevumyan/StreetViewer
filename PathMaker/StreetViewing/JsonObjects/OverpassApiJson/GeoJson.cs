using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PathFinder.StreetViewing.JsonObjects.OverpassApiJson
{
    [DataContract]
    public class GeoJson
    {
        private double version;
        [DataMember(Name = "version")]
        public double Version
        {
            get { return version; }
            set { version = value; }
        }

        private string generator;
        [DataMember(Name = "generator")]
        public string Generator
        {
            get { return generator; }
            set { generator = value; }
        }

        public Osm3s osm3s;
        [DataMember(Name = "osm3s")]
        public Osm3s Osm3s
        {
            get { return osm3s; }
            set { osm3s = value; }
        }

        private Element[] elements;
        [DataMember(Name = "elements")]
        public Element[] Elements
        {
            get { return elements; }
            set { elements = value; }
        }
    }
}
