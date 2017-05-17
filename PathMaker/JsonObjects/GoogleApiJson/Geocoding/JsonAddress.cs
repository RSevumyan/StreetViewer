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
        private List<AddressComponents> addressComponents;
        private Geometry geometry;

        [DataMember(Name = "address_components")]
        public List<AddressComponents> AddressComponents
        {
            get { return addressComponents; }
            set { addressComponents = value; }
        }

        [DataMember(Name = "geometry")]
        public Geometry Geometry
        {
            get { return geometry; }
            set { geometry = value; }
        }
    }
}
