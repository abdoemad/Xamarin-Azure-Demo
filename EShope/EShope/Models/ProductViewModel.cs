using EShope.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Models
{
    public class ProductViewModel : ObserverBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumnailURL { get; set; }
        public decimal Price { get; set; }
    }
}
