using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EShope.MobileAPIApp.Startup))]

namespace EShope.MobileAPIApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}