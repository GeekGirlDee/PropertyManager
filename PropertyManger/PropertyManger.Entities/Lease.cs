using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManger.Entities
{
   public class Lease
    {
        public int ID { get; set; }
        public int AccommodationID { get; set; }
        public Accommodation Accommodation { get; set; }

        public DateTime FromDate { get; set; }

        //number of stay months 
        public int Duration { get; set; }
    }
}
