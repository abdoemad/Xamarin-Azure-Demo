using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EShope.API.DataObjects
{
    [Table("Order")]
    public class Order : EntityData
    {
        public Order() {
            OrderItems = new HashSet<OrderItem>();
        }
        //public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CheckoutDateTime { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}