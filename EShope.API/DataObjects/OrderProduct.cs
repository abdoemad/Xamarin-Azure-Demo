using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShope.API.DataObjects
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}