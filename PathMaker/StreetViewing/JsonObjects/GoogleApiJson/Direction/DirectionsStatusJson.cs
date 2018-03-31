using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Direction
{
    [DataContract]
    public class DirectionsStatusJson
    {
        
        private List<DirectionsGeocodedWayPointJson> geocodedWaypoints;
        [DataMember(Name = "geocoded_waypoints")]
        public List<DirectionsGeocodedWayPointJson> GeocodedWaypoints
        {
            get { return geocodedWaypoints; }
            set { geocodedWaypoints = value; }
        }
        private List<DirectionsRouteJson> routes;
        [DataMember(Name = "routes")]
        public List<DirectionsRouteJson> Routes
        {
            get { return routes; }
            set { routes = value; }
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
