using PathFinder.StreetViewing.JsonObjects.GoogleApiJson.Common;

using System.Collections.Generic;

namespace PathFinder.StreetViewing
{
    public class PolylineChunk
    {
        public PolylineChunk() { }

        public PolylineChunk(List<LocationEntity> locationList)
        {
            this.LocationEntities = locationList;
        }

        public long Id { get; set; }

        public virtual List<LocationEntity> LocationEntities { get; set; }

        public bool Equals(PolylineChunk obj)
        {
            bool result = false;
            if (this.Id > 0 && obj.Id > 0)
            {
                result = this.Id == obj.Id;
            }
            else if (this.LocationEntities != null && obj.LocationEntities != null && this.LocationEntities.Count == obj.LocationEntities.Count)
            {
                int count = this.LocationEntities.Count;
                result = true;
                int index = 0;
                while (result && index < count)
                {
                    result = result && this.LocationEntities[index].Equals(obj.LocationEntities[index]);
                    index++;
                }
            }
            return result;
        }
    }
}
