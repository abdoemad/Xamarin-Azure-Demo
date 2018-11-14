using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EShope.Services.Data.Models;
using EShope.Services.Infra;

namespace EShope.Services.Data.Imp
{
    public class OrderService : IOrderService
    {
        IAPIConsumer _api;
        public OrderService(IAPIConsumer api)
        {
            _api = api;
        }

        public async Task<string> CheckoutAsync(Order order)
        {
            var uriBuilder = new UriBuilder($"{_api.DefaultEndPoint}")
            {
                Path = "api/orders",
            };

            return await _api.PostAsync<Order, string>(uriBuilder.Uri.AbsoluteUri, order);
        }

        public async Task<List<Order>> GetUserOrdersAsync(Guid userId)
        {
            var uriBuilder = new UriBuilder($"{_api.DefaultEndPoint}")
            {
                Path = "tables/order",
                Query = $"userId={userId}"
            };
            return await _api.GetAsync<List<Order>>(uriBuilder.Uri.AbsoluteUri);
        }
    }
}
