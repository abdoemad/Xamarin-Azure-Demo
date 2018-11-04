using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShope.API.DataObjects
{
    public class Product : EntityData
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ThumnailURL { get; set; }

    }
}