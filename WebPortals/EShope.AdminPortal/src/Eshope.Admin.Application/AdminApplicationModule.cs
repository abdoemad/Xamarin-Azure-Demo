using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Eshope.Admin
{
    [DependsOn(
        typeof(AdminCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class AdminApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AdminApplicationModule).GetAssembly());
        }
    }
}