using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DatabaseService.Model
{
    public class OrderedLocationEntity
    {
        public OrderedLocationEntity() { }
        public OrderedLocationEntity(int order, LocationEntity location)
        {
            Order = order;
            LocationEntity = location;
        }

        public int Id { get; set; }

        public int Order { get; set; }

        public virtual LocationEntity LocationEntity{get;set;}

    }
}
