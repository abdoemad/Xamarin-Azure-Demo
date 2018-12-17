using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using EShope.API.DataObjects;
using EShope.API.Models;
using EShope.API.DTO;
using EShope.API.Models.DomainManagers;
using System;

namespace EShope.API.Controllers
{
    public class ProductController : TableController<Product> //: TableController<ProductDTO>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            EShopeMobileServiceContext context = new EShopeMobileServiceContext();
            DomainManager = new EntityDomainManager<Product>(context, Request);
            //DomainManager = new DefaultMappedEntityDomainManager<ProductDTO, Product>(context, Request/*, Services*/);
            
        }

        [EnableQuery(PageSize = 50)]
        // GET tables/Product
        public IQueryable<Product> GetAllProduct()
        {
            try
            {
                var products = Query().Where(p => p.StockQuantity > 0).ToList();
                return products.AsQueryable();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET tables/Product/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Product> GetProduct(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Product/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Product> PatchProduct(string id, Delta<Product> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Product
        public async Task<IHttpActionResult> PostProduct(Product item)
        {
            Product current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Product/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteProduct(string id)
        {
             return DeleteAsync(id);
        }
    }
}
