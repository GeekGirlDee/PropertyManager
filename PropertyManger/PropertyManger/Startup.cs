using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PropertyManger.Startup))]
namespace PropertyManger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
