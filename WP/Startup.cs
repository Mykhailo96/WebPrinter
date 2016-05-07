using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WP.Startup))]
namespace WP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
