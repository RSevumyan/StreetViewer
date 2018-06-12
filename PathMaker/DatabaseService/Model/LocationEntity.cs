using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Common;
using PathFinder.StreetViewing.JsonObjects.OverpassApiJson;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder.DatabaseService.Model
{
    public class LocationEntity
    {
        public LocationEntity()
        {
            ImagePacks = new HashSet<ImagePack>();
        }

        public LocationEntity(double lat, double lng) : this()
        {
            Lat = lat;
            Lng = lng;
        }

        public LocationEntity(Element element) : this()
        {
            OverpassId = element.Id;
            Lat = element.Lat;
            Lng = element.Lon;
        }

        public LocationEntity(Location location) : this()
        {
            Lat = location.Lat;
            Lng = location.Lng;
        }

        public int Id { get; set; }

        public long OverpassId { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public virtual HashSet<PolylineChunk> PolylineChunks { get; set; }

        public virtual HashSet<ImagePack> ImagePacks { get; set; }

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
