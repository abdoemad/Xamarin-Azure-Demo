using EShope.Common;
using EShope.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumnailURL { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ShortDescription => Description.TruncateLongString(70);
        
    }
}
