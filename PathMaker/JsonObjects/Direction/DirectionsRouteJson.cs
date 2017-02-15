using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StreetViewer.JsonObjects.Common;
using System.Runtime.Serialization;

namespace StreetViewer.JsonObjects.Direction
{
    [DataContract]
    public class DirectionsRouteJson
    {
        private BoundsJson bounds;
        [DataMember(Name = "bounds")]
        public BoundsJson Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }
        private String copyrights;
        [DataMember(Name = "copyrights")]
        public String Copyrights
        {
            get { return copyrights; }
            set { copyrights = value; }
        }
        private List<DirectionsLegJson> legs;
        [DataMember(Name = "legs")]
        public List<DirectionsLegJson> Legs
        {
            get { return legs; }
            set { legs = value; }
        }
        private DirectionsPolylineJson overviewPolyline;
        [DataMember(Name = "overview_polyline")]
        public DirectionsPolylineJson OverviewPolyline
        {
            get { return overviewPolyline; }
            set { overviewPolyline = value; }
        }
        private String summary;
        [DataMember(Name = "summary")]
        public String Summary
        {
            get { return summary; }
            set { summary = value; }
        }
        private List<String> warnings;
        [DataMember(Name = "warnings")]
        public List<String> Warnings
        {
            get { return warnings; }
            set { warnings = value; }
        }
        private List<int> waypointOrder;
        [DataMember(Name = "waypoint_order")]
        public List<int> WaypointOrder
        {
            get { return waypointOrder; }
            set { waypointOrder = value; }
        }

    }
}
