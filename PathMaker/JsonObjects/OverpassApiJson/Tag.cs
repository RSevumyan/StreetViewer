using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StreetViewer.JsonObjects.OverpassApiJson
{
    [DataContract]
    public class Tag
    {
        private string foot;
        [DataContract(Name = "foot")]
        public string Foot
        {
            get { return foot; }
            set { foot = value; }
        }

        private string highway;
        [DataContract(Name = "highway")]
        public string Highway
        {
            get { return highway; }
            set { highway = value; }
        }

        private string lanes;
        [DataContract(Name = "lanes")]
        public string Lanes
        {
            get { return lanes; }
            set { lanes = value; }
        }

        private string lit;
        [DataContract(Name = "lit")]
        public string Lit
        {
            get { return lit; }
            set { lit = value; }
        }

        private string name;
        [DataContract(Name = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string surface;
        [DataContract(Name = "surface")]
        public string Surface
        {
            get { return surface; }
            set { surface = value; }
        }

        private string wikidata;
        [DataContract(Name = "wikidata")]
        public string Wikidata
        {
            get { return wikidata; }
            set { wikidata = value; }
        }

        private string wikipedia;
        [DataContract(Name = "wikipedia")]
        public string Wikipedia
        {
            get { return wikipedia; }
            set { wikipedia = value; }
        }

        private string cladr_003A_code;
        [DataContract(Name = "cladr_003A_code")]
        public string Cladr_003A_code
        {
            get { return cladr_003A_code; }
            set { cladr_003A_code = value; }
        }

        private string cladr_003A_name;
        [DataContract(Name = "cladr_003A_name")]
        public string Cladr_003A_name
        {
            get { return cladr_003A_name; }
            set { cladr_003A_name = value; }
        }

        private string cladr_003A_suffix;
        [DataContract(Name = "cladr_003A_suffix")]
        public string Cladr_003A_suffix
        {
            get { return cladr_003A_suffix; }
            set { cladr_003A_suffix = value; }
        }

        private string omkum_003A_code;
        [DataContract(Name = "omkum_003A_code")]
        public string Omkum_003A_code
        {
            get { return omkum_003A_code; }
            set { omkum_003A_code = value; }
        }

        private string sorting_name;
        [DataContract(Name = "sorting_name")]
        public string Sorting_name
        {
            get { return sorting_name; }
            set { sorting_name = value; }
        }
    }
}
