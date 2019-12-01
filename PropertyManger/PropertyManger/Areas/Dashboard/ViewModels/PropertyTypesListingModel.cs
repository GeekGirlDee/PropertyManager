using PropertyManger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManger.Areas.Dashboard.ViewModels
{
    public class PropertyTypesListingModel
    {
        public IEnumerable<PropertyType> PropertyTypes { get; set; }
        public string SearchTerm { get; set; }
    }


    public class PropertyTypesActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}