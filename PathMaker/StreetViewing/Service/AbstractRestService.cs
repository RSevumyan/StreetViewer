using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinder.StreetViewing.Service
{
    public abstract class AbstractRestService
    {
        protected string getStringOfLocation(double lat, double lng)
        {
            return coordinateToString(lat) + "," + coordinateToString(lng);
        }

        protected string coordinateToString(double coordinate)
        {
            return coordinate.ToString().Replace(",", ".");
        }
    }
}
