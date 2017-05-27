using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreetViewer.Core
{
    class Parameters
    {
        private static Parameters instance;

        private int order;
        private int radius;

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        private Parameters()
        {
            this.order = 1;
            this.radius = 100;
        }

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
