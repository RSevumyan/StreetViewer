using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using StreetViewer.Service;
using StreetViewer.JsonObjects.GoogleApiJson.Common;
using StreetViewer.JsonObjects.GoogleApiJson.Geocoding;
using StreetViewer.JsonObjects.GoogleApiJson.Direction;
using StreetViewer.JsonObjects.OverpassApiJson;



namespace StreetViewer.Core
{
    class Controller
    {
        private Parameters parameters;
        private GoogleRestService googleService;
        private GeographiService geoService;
        private OverpassRestService overpassService;

        internal Parameters Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public Controller()
        {
            parameters = new Parameters();
            googleService = new GoogleRestService();
            geoService = new GeographiService();
            overpassService = new OverpassRestService();
        }

        public Location getGeocoding(string streetName)
        {
            GeocodeJsonReply reply = googleService.getGeocoding(streetName);
            if ("ZERO_RESULTS".Equals(reply.Status))
            {
                return null;
            }
            else
            {
                double lat = reply.Results[0].Geometry.Location.Lat;
                double lng = reply.Results[0].Geometry.Location.Lng;
                return new Location(lat, lng);
            }
        }

        public IList<Location> getDirection(string startStreet, string endStreet)
        {
            DirectionsStatusJson json = googleService.getDirection(startStreet, endStreet);

            if ("NOT_FOUND".Equals(json.Status))
            {
                return null;
            }
            else
            {
                return geoService.decodePolyline(json.Routes[0].OverviewPolyline.Points, parameters.Order);
            }
        }

        public Downloader getStreetViews(List<List<Location>> points, string path)
        {
            Downloader downloader = new Downloader(path, points, googleService);
            Thread downloadThread = new Thread(downloader.downloadStreetViews);
            downloadThread.Start();
            return downloader;
        }

        public List<List<Location>> getAllDirectionsOfArea(string lat, string lng)
        {
            GeoJson geoJson = overpassService.getWaysOfArea(parameters.Radius.ToString(), lat, lng);
            return geoService.getAllDirectionsFromGeoJson(geoJson);
        }
    }
}
