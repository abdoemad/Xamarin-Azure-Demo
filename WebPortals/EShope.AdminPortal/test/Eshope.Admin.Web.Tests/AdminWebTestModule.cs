using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Eshope.Admin.Web.Startup;
namespace Eshope.Admin.Web.Tests
{
    [DependsOn(
        typeof(AdminWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class AdminWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AdminWebTestModule).GetAssembly());
        }
    }
}