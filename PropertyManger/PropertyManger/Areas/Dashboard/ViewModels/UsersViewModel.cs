using Microsoft.AspNet.Identity.EntityFramework;
using PropertyManger.Entities;
using PropertyManger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManger.Areas.Dashboard.ViewModels
{
    public class UsersListingModel
    {         
        public IEnumerable<PropertyManagerUser> Users { get; set; }
        public string RoleID { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string SearchTerm { get; set; }
        
        

        public Pager Pager { get; set; }
    }

    public class UserActionModel
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }

    public class UserRolesModel
    {
        public string UserID { get; set; }
        public IEnumerable<IdentityRole> UserRoles { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
    