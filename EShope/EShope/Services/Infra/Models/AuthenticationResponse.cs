using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Services.Data.Models
{
    public class AuthenticationResponse : ResponseBase
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
    }
}
