using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EShope.AzureMobileAPI.Startup))]

namespace EShope.AzureMobileAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}