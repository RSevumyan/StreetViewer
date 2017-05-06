using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StreetViewer.JsonObjects.GoogleApiJson.Common;
using System.Runtime.Serialization;

namespace StreetViewer.JsonObjects.GoogleApiJson.Direction
{
    [DataContract]
    public class DirectionsLegJson
    {
        private TextValueJson distance;
        [DataMember(Name = "distance")]
        public TextValueJson Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        private TextValueJson duration;
        [DataMember(Name = "duration")]
        public TextValueJson Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        private String endAddress;
        [DataMember(Name = "end_address")]
        public String EndAddress
        {
            get { return endAddress; }
            set { endAddress = value; }
        }
        private Location endLocation;
        [DataMember(Name = "end_location")]
        public Location EndLocation
        {
            get { return endLocation; }
            set { endLocation = value; }
        }
        private String startAddress;
        [DataMember(Name = "start_address")]
        public String StartAddress
        {
            get { return startAddress; }
            set { startAddress = value; }
        }
        private Location startLocation;
        [DataMember(Name = "start_location")]
        public Location StartLocation
        {
            get { return startLocation; }
            set { startLocation = value; }
        }
        private List<DirectionsStepJson> steps;
        [DataMember(Name = "steps")]
        public List<DirectionsStepJson> Steps
        {
            get { return steps; }
            set { steps = value; }
        }
        private List<DirectionsGeocodedWayPointJson> viaWaypoint;
        [DataMember(Name = "via_waypoint")]
        public List<DirectionsGeocodedWayPointJson> ViaWaypoint
        {
            get { return viaWaypoint; }
            set { viaWaypoint = value; }
        }
    }
}
