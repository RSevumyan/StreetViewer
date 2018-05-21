using CommonDetectorApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PathFinder.SignDetection
{
    public class DetectorsManager
    {
        private List<IDetector> detectors;

        public DetectorsManager(String pluginPath)
        {
            detectors = new List<IDetector>();
            string[] dllFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (string dllFile in dllFiles)
            {
                Assembly assembly = Assembly.LoadFile(Path.GetFullPath(dllFile));
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IDetector).IsAssignableFrom(type))
                    {
                        detectors.Add((IDetector)assembly.CreateInstance(type.FullName));
                    }
                }
            }
        }

        public List<IDetector> Detectors
        {
            get { return detectors; }
        }
    }
}
