using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinder.SignDetection
{
    public interface IDetector
    {
        IList<byte[]> DetectSignFromVideo(byte[] video);

        IList<byte[]> DetectSignFromImage(byte[] image);

        string CategorizeSign(byte[] signImage);
    }
}
