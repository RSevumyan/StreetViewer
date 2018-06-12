using System.Collections.Generic;

namespace PathFinder.DatabaseService.Model
{
    public class ImagePack
    {
        public ImagePack()
        {
            ImageList = new List<StreetView>();
        }

        public ImagePack(long start, long end) : this()
        {
            StartLocation = start;
            EndLocation = end;
        }

        public int Id { get; set; }

        public long StartLocation { get; set; }

        public long EndLocation { get; set; }

        public virtual List<StreetView> ImageList { get; set; }
    }
}
