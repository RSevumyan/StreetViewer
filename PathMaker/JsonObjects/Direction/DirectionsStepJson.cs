using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StreetViewer.JsonObjects.Common;
using System.Runtime.Serialization;

namespace StreetViewer.JsonObjects.Direction
{
    [DataContract]
    public class DirectionsStepJson
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
        private Location endLocation;
        [DataMember(Name = "end_location")]
        public Location EndLocation
        {
            get { return endLocation; }
            set { endLocation = value; }
        }
        
        private String htmlInstructions;
        [DataMember(Name = "html_instructions")]
        public String HtmlInstructions
        {
            get { return htmlInstructions; }
            set { htmlInstructions = value; }
        }
        private Location startLocation;
        [DataMember(Name = "start_location")]
        public Location StartLocation
        {
            get { return startLocation; }
            set { startLocation = value; }
        }
        private String travelMode;
        [DataMember(Name = "travel_mode")]
        public String TravelMode
        {
            get { return travelMode; }
            set { travelMode = value; }
        }
    }
}
