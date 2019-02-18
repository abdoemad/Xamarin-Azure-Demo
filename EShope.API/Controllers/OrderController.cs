using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using EShope.API.DataObjects;
using EShope.API.Models;
using System;
using Microsoft.Azure.Mobile.Server.Config;
using EShope.API.Repository.Base;
using EShope.API.Services;

namespace EShope.API.Controllers
{
    //[RoutePrefix("tables/Order/{userId}")]
    //[MobileAppController]
    public class OrderController :TableController<Order> //: ApiController //
    {
        //IRepository<Order> _orderRepository;
        //public OrderController(IRepository<Order> orderRepository)
        //{
        //    _orderRepository = orderRepository;
        //}
        private AzureServiceBusService serviceBus;

        public OrderController() {
            
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            EShopeMobileServiceContext context = new EShopeMobileServiceContext();
            DomainManager = new EntityDomainManager<Order>(context, Request);
        }

        // GET tables/Order
        //[Route("tables/Order/{userId}")]
        public IQueryable<Order> GetAllOrder([FromUri]Guid userId)
        {
            //return _orderRepository.GetAll().Where(o => o.UserId == userId);
            return Query().Where(o => o.UserId == userId);
        }

        // GET tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Order> GetOrder(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Order> PatchOrder(string id, Delta<Order> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Order
        //[Route("tables/Order/{userId}")]
        public async Task<IHttpActionResult> PostOrder(Order order)
        {
            order.CheckoutDateTime = DateTime.Now;
            order.Id = Guid.NewGuid().ToString();

            order.OrderItems.ToList().ForEach(i =>
            {
                i.Id = Guid.NewGuid().ToString();
            });
            //item.UserId = userId;
            Order current = await InsertAsync(order);
            serviceBus = new AzureServiceBusService();
            await serviceBus.SendMessagesAsync($"New Order {current.Id} by {current.UserId}");
            await serviceBus.Close();
            //return CreatedAtRoute("Tables", new { id = current.Id }, current);
            return Ok(new { id = current.Id });
        }

        // DELETE tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteOrder(string id)
        {
            return DeleteAsync(id);
        }
    }
}
