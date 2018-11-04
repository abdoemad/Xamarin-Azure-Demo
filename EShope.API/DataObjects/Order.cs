using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShope.API.DataObjects
{
    public class Order : EntityData
    {
        //public int Id { get; set; }
        public string UserName { get; set; }

        public List<OrderProduct> OrderItems { get; set; }

    }
}