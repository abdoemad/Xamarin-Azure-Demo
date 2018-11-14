using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Services.Data.Models
{
    public class Order
    {
        public Guid UserId { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
