using Microsoft.AspNet.Identity.EntityFramework;
using PropertyManger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManger.Areas.Dashboard.ViewModels
{
    public class RolesListingModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }

        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class RoleActionModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}