using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StreetViewer.Service;
using StreetViewer.JsonObjects.Geocoding;

namespace StreetViewer.Core
{
    class Controller
    {
        private RestService restService;
        private JsonService jsonService;

        public Controller()
        {
            restService = new RestService();
            jsonService = new JsonService();
        }

        public Location getGeocoding(string streetName)
        {
            GeocodeJsonReply reply = restService.GetGeocoding(streetName);
            return jsonService.parseGeocode(reply);
        }

        public List<Location> getDirection(string startStreet, string endStreet)
        {
            return null;
        }
    }
}
