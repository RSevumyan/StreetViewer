using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PathFinder.StreetViewing.JsonObjects.OverpassApiJson
{
    [DataContract]
    public  class Osm3s
    {
        private string timestamp_osm_base;
        [DataMember(Name = "timestamp_osm_base")]
        public string Timestamp_osm_base
        {
            get { return timestamp_osm_base; }
            set { timestamp_osm_base = value; }
        }

        private string copyright;
        [DataMember(Name = "copyright")]
        public string Copyright
        {
            get { return copyright; }
            set { copyright = value; }
        }
    }
}
