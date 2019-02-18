using System;
using System.Threading.Tasks;
using Abp.TestBase;
using Eshope.Admin.EntityFrameworkCore;
using Eshope.Admin.Tests.TestDatas;

namespace Eshope.Admin.Tests
{
    public class AdminTestBase : AbpIntegratedTestBase<AdminTestModule>
    {
        public AdminTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<AdminDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<AdminDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<AdminDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<AdminDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<AdminDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<AdminDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<AdminDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<AdminDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
