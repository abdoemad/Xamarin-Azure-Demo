using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Eshope.Admin.Configuration;
using Eshope.Admin.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Eshope.Admin.Web.Startup
{
    [DependsOn(
        typeof(AdminApplicationModule), 
        typeof(AdminEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class AdminWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public AdminWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(AdminConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<AdminNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(AdminApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AdminWebModule).GetAssembly());
        }
    }
}