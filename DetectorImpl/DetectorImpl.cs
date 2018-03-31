using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.StreetViewing.Service;
using PathFinder.SignDetection;

namespace DetectorImpl
{
    public class DetectorImpl : IDetector
    {
        IList<byte[]> IDetector.DetectSignFromVideo(byte[] video)
        {
            throw new NotImplementedException();
        }

        IList<byte[]> IDetector.DetectSignFromImage(byte[] image)
        {
            throw new NotImplementedException();
        }

        string IDetector.CategorizeSign(byte[] signImage)
        {
            throw new NotImplementedException();
        }
    }
}
