using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Services.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailURL { get; set; }
        public decimal Price { get; set; } 
    }
}
