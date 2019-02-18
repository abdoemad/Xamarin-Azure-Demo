using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Eshope.Admin.EntityFrameworkCore
{
    [DependsOn(
        typeof(AdminCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class AdminEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AdminEntityFrameworkCoreModule).GetAssembly());
        }
    }
}