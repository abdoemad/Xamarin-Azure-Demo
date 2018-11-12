using EShope.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Repository
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository() : base("eshopelocaldb.db")
        {
        }
    }
}
