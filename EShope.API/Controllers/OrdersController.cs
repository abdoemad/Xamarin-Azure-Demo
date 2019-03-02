using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EShope.API.DataObjects;
using EShope.API.Helpers;
using EShope.API.Models;
using EShope.API.Services;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Net.Http.Headers;

namespace EShope.API.Controllers
{
    [MobileAppController]
    public class OrdersController : ApiController
    {
        private EShopeMobileServiceContext db = new EShopeMobileServiceContext();

        // GET: api/Orders
        public IQueryable<Order> GetOrders()
        {
            return db.Orders;//.Include(o => o.User);
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> GetOrder(string id)
        {
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrder(string id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        async Task<NotificationOutcome> SendNotification(string message, string installationId)
        {
            // Get the settings for the server project.
            HttpConfiguration config = this.Configuration;

#if USE_APP_SETTINGS
			var settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

			// Get the Notification Hubs credentials for the Mobile App.
			string notificationHubName = settings.NotificationHubName;
			string notificationHubConnection = settings.Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;
#else
            // The name of the Notification Hub from the overview page.
            string notificationHubName = "xamarinpushnotifhub";
            // Use "DefaultFullSharedAccessSignature" from the portal's Access Policies.
            string notificationHubConnection = "Endpoint=sb://xamarinpushnotifhubnamespace.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=hWvLPIb2207cRtNoy/k4ViGkXxy53M9UI7pZCfurR3g=";
#endif

            // Create a new Notification Hub client.
            var hub = NotificationHubClient.CreateClientFromConnectionString(
                notificationHubConnection,
                notificationHubName,
                // Don't use this in RELEASE builds. The number of devices is limited.
                // If TRUE, the send method will return the devices a message was
                // delivered to.
                enableTestSend: true);

            // Sending the message so that all template registrations that contain "messageParam"
            // will receive the notifications. This includes APNS, GCM, WNS, and MPNS template registrations.
            var templateParams = new Dictionary<string, string>
            {
                ["messageParam"] = message
            };

            // Send the push notification and log the results.

            NotificationOutcome result = null;
            if (string.IsNullOrWhiteSpace(installationId))
            {
                result = await hub.SendTemplateNotificationAsync(templateParams).ConfigureAwait(false);
            }
            else
            {
                result = await hub.SendTemplateNotificationAsync(templateParams, "$InstallationId:{" + installationId + "}").ConfigureAwait(false);
            }


            // Write the success result to the logs.
            config.Services.GetTraceWriter().Info(result.State.ToString());
            return result;
        }
        async Task PushNotificationAsync(Order order) {
            // Get the settings for the server project.
            HttpConfiguration config = this.Configuration;
            MobileAppSettingsDictionary settings =
                this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            // Get the Notification Hubs credentials for the mobile app.
            //"eshopenotifyhub";// settings.NotificationHubName;
            //"Endpoint=sb://eshopenotifyhub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=QqmckPU7pDyRlYchxaVBT7LVjliPUwtUg6IZnexQAZYUWU=";// settings.Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;
            string notificationHubName = settings.NotificationHubName;
            string notificationHubConnection = settings.Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;

            // Create a new Notification Hub client.
            NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString(notificationHubConnection, notificationHubName);

            // Send the message so that all template registrations that contain "messageParam"
            // receive the notifications. This includes APNS, GCM, WNS, and MPNS template registrations.
            Dictionary<string, string> templateParams = new Dictionary<string, string>();
            templateParams["messageParam"] = $"Your order is placed in {order.CheckoutDateTime.ToShortDateString()} with total itmes: {order.OrderItems.Count}";

            try
            {
                // Send the push notification and log the results.
                var result = await hub.SendTemplateNotificationAsync(templateParams);

                // Write the success result to the logs.
                config.Services.GetTraceWriter().Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                // Write the failure result to the logs.
                config.Services.GetTraceWriter()
                    .Error(ex.Message, null, "Push.SendAsync Error");
            }
        }
        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            order.CheckoutDateTime = DateTime.Now;
            order.Id = Guid.NewGuid().ToString();

            order.OrderItems.ToList().ForEach(i =>
            {
                i.Id = Guid.NewGuid().ToString();
            });

            db.Orders.Add(order);

            try
            {
                await db.SaveChangesAsync();
                
                //TODO: Ioc & Interface & Repo
                var savedOrder = await GetOrderFromDB(order.Id);
                AzureServiceBusService serviceBus = new AzureServiceBusService();
                await serviceBus.SendMessagesAsync($"User '{savedOrder.User.Name}' ordered '{savedOrder.OrderItems.Sum(i=>i.Quantity)}' items with total price {savedOrder.OrderItems.Sum(i => i.Quantity * i.Product.Price)} is placed in {savedOrder.CheckoutDateTime}, \r\nDiagnostic data: {Request.Headers.UserAgent} {this.Request.GetOwinContext()?.Request.RemoteIpAddress} {RequestContext.Principal.Identity?.Name}");
                await serviceBus.Close();
                await PushNotificationAsync(savedOrder);
            }
            catch (DbUpdateException ex)
            {
                if (OrderExists(order.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            //return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
            return Ok(order.Id);
        }
        async Task<Order> GetOrderFromDB(string orderId)
        {
            return await ExceptionHelper.TryCatch<Order>(async () =>
            {
                using (var context = new EShopeMobileServiceContext())
                {
                    var order = context.Orders.Include(o => o.User)
                    .Include(o => o.OrderItems).Include(o => o.OrderItems.Select(i => i.Product))
                    .FirstOrDefaultAsync(o => o.Id == orderId);
                    return await order;
                }
            });
        }
        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> DeleteOrder(string id)
        {
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            await db.SaveChangesAsync();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(string id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}