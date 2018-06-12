using System.Data.Entity;
using PathFinder.DatabaseService.Model;
using System.Data.Entity.Infrastructure;

namespace PathFinder.DataBaseService
{
    public class PathFinderContext : DbContext
    {
        public DbSet<Road> Roads { get; set; }

        public DbSet<PolylineChunk> Chunks { get; set; }

        public DbSet<LocationEntity> LocationEntities { get; set; }

        public DbSet<ImagePack> ImagePacks{get;set;}

        public DbSet<StreetView> Images { get; set; }

        public DbSet<Sign> Signs { get; set; }

        public DbSet<OrderedLocationEntity> OrderedLocationEntities { get; set; }
    }
}
