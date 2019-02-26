using EShope.Services.Data.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShope.Services.Data
{
    public interface IProductService
    {
        IMobileServiceClient MobileServiceClient { get; }
        Task<List<Product>> GetProductsAsync(bool syncItems = false);
    }
}
