using System;
using System.Collections.Generic;
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

        private int order;
        private int radius;
        private string pluginsPath;
        private int pluginNumber;

        /// <summary>
        /// Шаг загрузки панорам, в метрах
        /// </summary>
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        /// <summary>
        /// Радиус области, по которой запрашиваются пути.
        /// </summary>
        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public string PlugnisPath
        {
            get { return pluginsPath; }
            set { pluginsPath = value; }
        }

        public int PluginNumber
        {
            get{ return pluginNumber; }
            set{ pluginNumber = value; }
        }

        private Parameters()
        {
            this.order = 1;
            this.radius = 100;
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
