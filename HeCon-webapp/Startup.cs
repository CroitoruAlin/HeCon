using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HeCon_webapp.Startup))]
namespace HeCon_webapp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
