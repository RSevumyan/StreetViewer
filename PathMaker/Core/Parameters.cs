using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreetViewer.Core
{
    class Parameters
    {
        private int order;

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        public Parameters()
        {
            this.order = 1;
        }
    }
}
