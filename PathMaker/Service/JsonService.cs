using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StreetViewer.JsonObjects.Geocoding;
namespace StreetViewer.Service
{
    class JsonService
    {
        public Location parseGeocode(GeocodeJsonReply json)
        {
            return json.Results[0].Geometry.Location;
        }
    }
}
