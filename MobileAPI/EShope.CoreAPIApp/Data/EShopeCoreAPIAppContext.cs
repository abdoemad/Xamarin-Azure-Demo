using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EShope.CoreAPIApp.Models
{
    public class EShopeCoreAPIAppContext : DbContext
    {
        public EShopeCoreAPIAppContext (DbContextOptions<EShopeCoreAPIAppContext> options)
            : base(options)
        {
        }

        public DbSet<EShope.CoreAPIApp.Models.Order> Order { get; set; }
    }
}
