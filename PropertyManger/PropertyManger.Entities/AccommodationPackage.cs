using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManger.Entities
{
    public class AccommodationPackage
    {
        public int ID { get; set; }
        public int PropertyTypeID { get; set; }
        public virtual PropertyType PropertyType { get; set; }

        public string Name { get; set; }
        public int NoOfRooms { get; set; }
        public decimal FeePerMonth { get; set; }
    }
}

