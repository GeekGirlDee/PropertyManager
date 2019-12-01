using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using PropertyManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Services
{
    public class PMSRolesManager : RoleManager<IdentityRole>
    {
        public PMSRolesManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {
        }

        public static PMSRolesManager Create(IdentityFactoryOptions<PMSRolesManager> options, IOwinContext context)
        {
            return new PMSRolesManager(new RoleStore<IdentityRole>(context.Get<PropertyManagerContext>()));
        }

    }
}
