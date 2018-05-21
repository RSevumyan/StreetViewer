using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PathFinder.Core
{
    /// <summary>
    /// Класс синглтон, содержащий параметры работы приложения.
    /// </summary>
    public class Parameters
    {
        private static Parameters instance;

        /// <summary>
        /// Шаг загрузки панорам, в метрах
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Радиус области, по которой запрашиваются пути.
        /// </summary>
        public int Radius { get; set; }

        public string PluginsPath { get; set; }

        public int PluginNumber { get; set; }

        public string StreetViewsPath { get; set; }

        private Parameters()
        {
            Order = Int32.Parse(ConfigurationManager.AppSettings["order"]);
            Radius = Int32.Parse(ConfigurationManager.AppSettings["radius"]);
            PluginsPath = ConfigurationManager.AppSettings["pluginPath"];
            StreetViewsPath = ConfigurationManager.AppSettings["streetViewsPath"];
        }

        /// <summary>
        /// Экземпляр синглтона.
        /// </summary>
        public static Parameters Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Parameters();
                }
                return instance;
            }
        }
    }
}
