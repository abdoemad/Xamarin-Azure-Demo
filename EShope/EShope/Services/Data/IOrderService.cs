using EShope.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShope.Services.Data
{
    public interface IOrderService
    {
        Task<string> CheckoutAsync(Order order);

        Task<List<Order>> GetUserOrdersAsync(Guid userId);
    }
}
