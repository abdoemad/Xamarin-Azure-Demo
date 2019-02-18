using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eshope.Admin.EntityFrameworkCore
{
    public class AdminDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public AdminDbContext(DbContextOptions<AdminDbContext> options) 
            : base(options)
        {

        }
    }
}
