using Microsoft.AspNet.Identity.EntityFramework;
using PropertyManger.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Data
{
    public class PropertyManagerContext : IdentityDbContext<PropertyManagerUser>
    {
        public PropertyManagerContext() : base("PMSConnectionString")
        {
        }

        public static PropertyManagerContext Create()
        {
            return new PropertyManagerContext();
        }

        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<AccommodationPackage> AccommodationPackages { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Lease> Leases { get; set; }
        //public DbSet<PropertyManagerUser> AccessDeatails { get; set; }
    }
}
