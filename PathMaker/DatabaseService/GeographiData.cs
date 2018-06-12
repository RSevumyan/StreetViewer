using PathFinder.DatabaseService.Model;
using System.Collections.Generic;

namespace PathFinder.DataBaseService
{
    public class GeographiData
    {
        private static GeographiData instance;

        public Dictionary<long, LocationEntity> Locations { get; set; }

        public Dictionary<long, PolylineChunk> Chunks { get; set; }

        public Dictionary<string, Road> Roads { get; set; }

        private GeographiData()
        {
            Locations = new Dictionary<long, LocationEntity>();
            Chunks = new Dictionary<long, PolylineChunk>();
            Roads = new Dictionary<string, Road>();
        }

        public static GeographiData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GeographiData();
                }
                return instance;
            }
        }
    }
}
