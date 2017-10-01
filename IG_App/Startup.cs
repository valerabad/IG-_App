using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IG_App.Startup))]
namespace IG_App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
