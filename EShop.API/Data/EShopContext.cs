using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EShop.API.Models
{
    public class EShopContext : DbContext
    {
        public EShopContext (DbContextOptions<EShopContext> options)
            : base(options)
        {
        }

        public DbSet<EShop.API.Models.Product> Product { get; set; }
    }
}
