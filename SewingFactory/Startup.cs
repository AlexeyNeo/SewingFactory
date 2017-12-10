using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SewingFactory.Startup))]
namespace SewingFactory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
