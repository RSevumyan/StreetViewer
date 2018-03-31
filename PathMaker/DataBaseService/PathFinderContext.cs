using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
using PathFinder.StreetViewing;

namespace PathFinder.DataBaseService
{
    public class PathFinderContext : DbContext
    {
        public DbSet<PolylineChunk> Chunks { get; set; }

        public DbSet<LocationEntity> LocationEntities { get; set; }
    }
}
