using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OWP.Startup))]
namespace OWP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
