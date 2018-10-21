using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EShope.API.Startup))]

namespace EShope.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}