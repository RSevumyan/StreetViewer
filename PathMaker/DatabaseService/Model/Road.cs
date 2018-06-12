using System.Collections.Generic;

namespace PathFinder.DatabaseService.Model
{
    public class Road
    {
        public Road()
        {
            PolylineChunks = new List<PolylineChunk>();
            IsFull = false;
            IsStreetViewsDownloaded = false;
        }

        public Road(string roadName) : this()
        {
            Name = roadName;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<PolylineChunk> PolylineChunks { get; set; }

        public bool IsFull { get; set; }

        public bool IsStreetViewsDownloaded { get; set; }

        public bool IsSignDetected { get; set; }
    }
}
