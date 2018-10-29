using EShope.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Services.Data
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
