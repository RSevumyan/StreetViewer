using CommonDetectorApi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.StreetViewing
{
    public class BypassResult
    {

        public BypassResult()
        {
            DetectorsResult = new Dictionary<string, List<Sign>>();
        }

        public Bitmap Image { get; set;}
        public Dictionary<String, List<Sign>> DetectorsResult {get; set;}
    }
}
