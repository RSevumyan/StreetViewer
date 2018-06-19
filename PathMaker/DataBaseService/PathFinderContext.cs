using System.Data.Entity;
using PathFinder.DatabaseService.Model;
using System.Data.Entity.Infrastructure;
using System;
using System.IO;

namespace PathFinder.DataBaseService
{
    public class PathFinderContext : DbContext
    {

        public PathFinderContext() : base()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
        }
        public DbSet<Road> Roads { get; set; }

        public DbSet<PolylineChunk> Chunks { get; set; }

        public DbSet<LocationEntity> LocationEntities { get; set; }

        public DbSet<ImagePack> ImagePacks{get;set;}

        public DbSet<StreetView> Images { get; set; }

        public DbSet<Sign> Signs { get; set; }

        public DbSet<OrderedLocationEntity> OrderedLocationEntities { get; set; }
    }
}
