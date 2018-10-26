using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Services.Infra.Models
{
    public class AuthenticationRequest
    {
        public string Password { get; set; }

        public string UserName { get; set; }

    }
}
