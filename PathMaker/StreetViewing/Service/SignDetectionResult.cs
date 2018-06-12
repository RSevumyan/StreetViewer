using CommonDetectorApi;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathFinder.StreetViewing
{
    public class SignDetectionResult
    {

        public SignDetectionResult()
        {
            DetectorsResult = new Dictionary<string, List<DatabaseService.Model.Sign>>();
        }

        public Bitmap Image { get; set;}

        public Dictionary<String, List<DatabaseService.Model.Sign>> DetectorsResult {get; set;}
    }
}
