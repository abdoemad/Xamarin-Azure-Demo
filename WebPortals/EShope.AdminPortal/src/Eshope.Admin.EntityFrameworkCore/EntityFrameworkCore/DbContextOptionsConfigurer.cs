using Microsoft.EntityFrameworkCore;

namespace Eshope.Admin.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<AdminDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for AdminDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}
