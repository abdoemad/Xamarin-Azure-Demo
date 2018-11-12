using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Services.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
