using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using EShope.API.DataObjects;
using EShope.API.Models;
using System;

namespace EShope.API.Controllers
{
    //[RoutePrefix("tables/Order/{userId}")]
    public class OrderController : TableController<Order>
    {
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
        public async Task<IHttpActionResult> PostOrder(Order item)
        {
            //item.UserId = userId;
            Order current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteOrder(string id)
        {
             return DeleteAsync(id);
        }
    }
}
