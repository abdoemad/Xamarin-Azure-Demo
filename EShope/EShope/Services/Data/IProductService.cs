using EShope.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShope.Services.Data
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
    }
}
