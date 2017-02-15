using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StreetViewer.JsonObjects.Geocoding;
using StreetViewer.JsonObjects.Common;
using StreetViewer.JsonObjects.Direction;

namespace StreetViewer.Service
{
    class JsonService
    {
        public Location parseGeocode(GeocodeJsonReply json)
        {
            return json.Results[0].Geometry.Location;
        }

        public string parseDirection(DirectionsStatusJson json)
        {
            return json.Routes[0].OverviewPolyline.Points;
        }
    }
}
