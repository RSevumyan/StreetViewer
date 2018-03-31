using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PathFinder.SignDetection
{
    public static class PluginsController
    {
        public static List<IDetector> LoadDetectorPlugons(string path)
        {
            ICollection<Type> pluginTypes = LoadPluginsTypes(path);
            List<IDetector> plugins = new List<IDetector>(pluginTypes.Count);
            foreach (Type type in pluginTypes)
            {
                IDetector plugin = (IDetector)Activator.CreateInstance(type);
                plugins.Add(plugin);
            }
            return plugins;

        }

        private static ICollection<Type> LoadPluginsTypes(string pluginsPath)
        {
            string[] dllFileNames = null;
            dllFileNames = Directory.GetFiles(pluginsPath, "*.dll");

            ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
            foreach (string dllFile in dllFileNames)
            {
                AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                Assembly assembly = Assembly.Load(an);
                assemblies.Add(assembly);
            }

            Type pluginType = typeof(IDetector);
            ICollection<Type> pluginTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly != null)
                {
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                            continue;
                        }
                        else
                        {
                            if (type.GetInterface(pluginType.FullName) != null)
                            {
                                pluginTypes.Add(type);
                            }
                        }
                    }
                }
            }
            return pluginTypes;
        }
    }
}
