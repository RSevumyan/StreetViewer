using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using StreetViewer.Service;
using StreetViewer.JsonObjects.Common;
using StreetViewer.JsonObjects.Geocoding;
using StreetViewer.JsonObjects.Direction;



namespace StreetViewer.Core
{
    class Controller
    {
        private Parameters parameters;
        private GoogleRestService restService;
        private GeographiService geoService;

        public Controller()
        {
            restService = new GoogleRestService();
            parameters = new Parameters();
            geoService = new GeographiService();
        }

        public Location getGeocoding(string streetName)
        {
            GeocodeJsonReply reply = restService.getGeocoding(streetName);
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
            DirectionsStatusJson json = restService.getDirection(startStreet, endStreet);

            if ("NOT_FOUND".Equals(json.Status))
            {
                return null;
            }
            else
            {
                return geoService.decodePolyline(json.Routes[0].OverviewPolyline.Points, parameters.Order);
            }
        }

        public Downloader getStreetViews(IList<Location> points, string path)
        {
            Downloader downloader = new Downloader(path, points, restService);
            Thread downloadThread = new Thread(downloader.downloadStreetViews);
            downloadThread.Start();
            return downloader;
        }

        public void setParams(int order)
        {
            parameters.Order = order;
        }
    }
}
