using Abp.Modules;
using Abp.Reflection.Extensions;
using Eshope.Admin.Localization;

namespace Eshope.Admin
{
    public class AdminCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            AdminLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AdminCoreModule).GetAssembly());
        }
    }
}