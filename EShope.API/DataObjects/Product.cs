using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShope.API.DataObjects
{
    public class Product : EntityData
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}