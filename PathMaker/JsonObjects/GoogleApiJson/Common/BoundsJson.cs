using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StreetViewer.JsonObjects.GoogleApiJson.Common
{
    [DataContract]
    public class BoundsJson
    {

        private Location northEast;
        [DataMember(Name = "northeast")]
        public Location NorthEast
        {
            get { return northEast; }
            set { northEast = value; }
        }

        private Location southWest;
        [DataMember(Name = "southwest")]
        public Location SouthWest
        {
            get { return southWest; }
            set { southWest = value; }
        }
    }
}
