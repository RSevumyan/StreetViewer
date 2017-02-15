using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StreetViewer.Service;
using StreetViewer.JsonObjects.Common;
using StreetViewer.JsonObjects.Geocoding;
using StreetViewer.JsonObjects.Direction;

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
            //ToDo Сделать получение массива байт или строки в restService.getGeocoding();
            GeocodeJsonReply reply = restService.getGeocoding(streetName);
            return jsonService.parseGeocode(reply);
        }

        public string getDirection(string startStreet, string endStreet)
        {
            DirectionsStatusJson route = restService.getDirection(startStreet, endStreet);
            return jsonService.parseDirection(route);
        }
    }
}
