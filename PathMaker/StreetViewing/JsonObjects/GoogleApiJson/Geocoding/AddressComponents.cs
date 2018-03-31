using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Geocoding
{
    [DataContract]
    public class AddressComponents
    {
        private string longName;
        private string shortName;
        private List<string> types;

        [DataMember(Name = "long_name")]
        public string LongName
        {
            get { return longName; }
            set { longName = value; }
        }

        [DataMember(Name = "short_name")]
        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }


        [DataMember(Name = "types")]
        public List<string> Types
        {
            get { return types; }
            set { types = value; }
        }
    }
}
