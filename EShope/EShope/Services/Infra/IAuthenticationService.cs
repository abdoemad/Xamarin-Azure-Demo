using EShope.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShope.Services.Infra
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Authenticate(string userName, string password);
    }
}
