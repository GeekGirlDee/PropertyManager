using PropertyManger.Entities;
using PropertyManger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManger.Areas.Dashboard.ViewModels
{
    public class AccommodationPackagesListingModel
    {
        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<PropertyType> PropertyTypes { get; set; }
        public int? PropertyTypeID { get; set; }

        public Pager Pager { get; set; }
    }

    public class AccommodationPackagesActionModel
    {
        public int ID { get; set; }
        public int PropertyTypeID { get; set; }
        public PropertyType PropertyType { get; set; }

        public string Name { get; set; }
        public int NoOfRooms { get; set; }
        public decimal FeePerMonth { get; set; }

        public IEnumerable<PropertyType> PropertyTypes { get; set; }
    }
}