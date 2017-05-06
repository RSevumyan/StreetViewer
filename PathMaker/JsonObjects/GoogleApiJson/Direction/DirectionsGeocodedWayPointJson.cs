using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StreetViewer.JsonObjects.GoogleApiJson.Direction
{
    [DataContract]
    public class DirectionsGeocodedWayPointJson
    {
        private String geocoderStatus;
        [DataMember(Name = "geocoder_status")]
        public String GeocoderStatus
        {
            get { return geocoderStatus; }
            set { geocoderStatus = value; }
        }
        private String placeId;
        [DataMember(Name = "place_id")]
        public String PlaceId
        {
            get { return placeId; }
            set { placeId = value; }
        }
        private List<String> types;
        [DataMember(Name = "types")]
        public List<String> Types
        {
            get { return types; }
            set { types = value; }
        }
        private bool partailMatch = false;
        [DataMember(Name = "partial_match")]
        public bool PartailMatch
        {
            get { return partailMatch; }
            set { partailMatch = value; }
        }
    }
}
