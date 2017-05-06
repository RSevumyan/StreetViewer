using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StreetViewer.JsonObjects.OverpassApiJson
{
    [DataContract]
    public class Element
    {
        private string type;
        [DataMember(Name = "type")]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private long id;
        [DataMember(Name = "id")]
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private double lat;
        [DataMember(Name = "lat")]
        public double Lat
        {
            get { return lat; }
            set { lat = value; }
        }

        private double lon;
        [DataMember(Name = "lon")]
        public double Lon
        {
            get { return lon; }
            set { lon = value; }
        }

        private long[] nodes;
        [DataMember(Name = "nodes")]
        public long[] Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }

        private Tag tags;
        [DataMember(Name = "tags")]
        public Tag Tags
        {
            get { return tags; }
            set { tags = value; }
        }
    }
}
