using System;
using System.Collections.Generic;

namespace PathFinder.DatabaseService.Model
{
    public class PolylineChunk
    {
        public PolylineChunk()
        {
            OrderedLocationEntities = new List<OrderedLocationEntity>();
            IsStreetViewsDownloaded = false;
            IsSignDetected = false;
        }

        public PolylineChunk(List<OrderedLocationEntity> locationList) : this()
        {
            this.OrderedLocationEntities = locationList;
        }

        public int Id { get; set; }

        public long OverpassId { get; set; }

        public virtual List<OrderedLocationEntity> OrderedLocationEntities { get; set; }

        public virtual HashSet<PolylineChunk> NextChunks { get; set; }

        public bool IsStreetViewsDownloaded { get; set; }

        public bool IsSignDetected { get; set; }
        public int Order { get; set; }

        public bool Equals(PolylineChunk obj)
        {
            bool result = false;
            if (this.Id > 0 && obj.Id > 0)
            {
                result = this.Id == obj.Id;
            }
            else if (this.OverpassId > 0 && obj.OverpassId > 0)
            {
                result = this.OverpassId == obj.OverpassId;
            }
            else if (this.OrderedLocationEntities != null && obj.OrderedLocationEntities != null && this.OrderedLocationEntities.Count == obj.OrderedLocationEntities.Count)
            {
                int count = this.OrderedLocationEntities.Count;
                result = true;
                int index = 0;
                while (result && index < count)
                {
                    result = result && this.OrderedLocationEntities[index].Equals(obj.OrderedLocationEntities[index]);
                    index++;
                }
            }
            return result;
        }
    }
}
