using PropertyManger.Entities;
using PropertyManger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManger.Areas.Dashboard.ViewModels
{
    public class AccommodationsListingModel
    {
        public IEnumerable<Accommodation> Accommodations { get; set; }

        public int? AccommodationPackageID { get; set; }
        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }
        public string SearchTerm { get; set; }


        public Pager Pager { get; set; }

    }

    public class AccommodationActionModel
    {
        public int ID { get; set; }

        public int AccommodationPackageID { get; set; }
        public AccommodationPackage AccommodationPackage { get; set;}

        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }
    }
}