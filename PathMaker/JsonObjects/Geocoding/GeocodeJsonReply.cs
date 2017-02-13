using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace StreetViewer.JsonObjects.Geocoding
{
    [DataContract]
    public class GeocodeJsonReply
    {
        private List<JsonAddress> results;
        [DataMember(Name = "results")]
        public List<JsonAddress> Results
        {
            get { return results; }
            set { results = value; }

        }

        private String status;
        [DataMember(Name = "status")]
        public String Status
        {
            get { return status; }
            set { status = value; }
        }

    }
}
