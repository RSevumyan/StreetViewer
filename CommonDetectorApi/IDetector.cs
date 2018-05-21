using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDetectorApi
{
    public interface IDetector
    {
        string Name { get; }
        string Id { get; }
        List<Sign> Detect(Bitmap image);
    }
}
