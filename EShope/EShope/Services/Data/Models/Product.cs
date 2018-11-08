using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Services.Data.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailURL { get; set; }
        public string[] PicturesURLs { get; set; }
        public decimal Price { get; set; }
        [JsonProperty(PropertyName = "stockQuantity")]
        public int AvailableQuantity { get; set; }
    }
}
