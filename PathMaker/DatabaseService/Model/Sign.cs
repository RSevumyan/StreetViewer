using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DatabaseService.Model
{
    public class Sign
    {
        public Sign() { }

        public Sign(StreetView image, string detectorName, CommonDetectorApi.Sign sign)
        {
            ClassName = sign.ClassName;
            X = sign.X;
            Y = sign.Y;
            Height = sign.Height;
            Width = sign.Width;
            Image = image;
            DetectorName = detectorName;
        }

        public int Id { get; set; }

        public string ClassName { get; set; }

        public string DetectorName { get; set; }

        public StreetView Image { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
