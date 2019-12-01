using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PropertyManger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Services
{
    public class PMSSignInManager: SignInManager<PropertyManagerUser, string>
    {
        public PMSSignInManager(PMSUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(PropertyManagerUser user)
        {
            return user.GenerateUserIdentityAsync((PMSUserManager)UserManager);
        }

        public static PMSSignInManager Create(IdentityFactoryOptions<PMSSignInManager> options, IOwinContext context)
        {
            return new PMSSignInManager(context.GetUserManager<PMSUserManager>(), context.Authentication);
        }
    }
}
