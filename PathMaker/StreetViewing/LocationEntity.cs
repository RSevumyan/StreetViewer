using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PathFinder.StreetViewing
{
    public class LocationEntity
    {
        public LocationEntity() { }

        public LocationEntity(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }

        public LocationEntity(Location location)
        {
            Lat = location.Lat;
            Lng = location.Lng;
        }

        public long Id { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string PathToStreetView { get; set; }

        [ForeignKey("PolylineChunk")]
        public int PolylineChunkId { get; set; }

        public virtual PolylineChunk PolylineChunk { get; set; }

        public Location GetGoogleLocation()
        {
            return new Location(Lat, Lng);
        }

        public static List<LocationEntity> ConvertFromGoogleLocations(List<Location> locations)
        {
            return locations.Select(location => new LocationEntity(location)).ToList();
        }

        public bool Equals(LocationEntity obj)
        {
            bool result = false;
            if (this.Id > 0 && obj.Id > 0)
            {
                result = this.Id == obj.Id;
            }
            else
            {
                result = this.Lat == obj.Lat && this.Lng == obj.Lng;
            }
            return result;
        }
    }
}
