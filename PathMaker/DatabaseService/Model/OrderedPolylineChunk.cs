using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DatabaseService.Model
{
    public class OrderedPolylineChunk
    {
        public OrderedPolylineChunk() { }

        public OrderedPolylineChunk(int order, PolylineChunk polylineChunk)
        {
            Order = order;
            PolylineChunk = polylineChunk;
        }

        public int Id { get; set; }

        public int Order { get; set; }

        public virtual PolylineChunk PolylineChunk { get; set; }
    }
}
