using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShope.CoreAPIApp.Models
{
    //[Table("OrderItem")]
    public class OrderItem //: EntityData
    {
        [Key]
        public string Id { get; set; }
        public string OrderId { get; set; }
        //[ForeignKey("Product")]
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        //public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
