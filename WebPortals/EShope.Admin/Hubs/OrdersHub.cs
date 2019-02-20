using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShope.Admin.Hubs
{
    public class OrdersHub : Hub
    {
        public Task SendMessage(string message)
        {
            return Clients.All.SendAsync("NewOrder", message);
        }
    }
}
