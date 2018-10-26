using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EShope.Services.Data.Models;
using EShope.Services.Infra.Models;

namespace EShope.Services.Infra.Imp
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly IAPIConsumer _api;
        public AuthenticationService(IAPIConsumer api)
        {
            _api = api;
        }
        public async Task<AuthenticationResponse> Authenticate(string userName, string password)
        {
            var uri = new Uri($"{_api.DefaultEndPoint}/values");
            
            AuthenticationRequest authenticationRequest = new AuthenticationRequest()
            {
                UserName = userName,
                Password = password
            };

            var res = await _api.PostAsync<AuthenticationRequest, string>(uri.AbsoluteUri, authenticationRequest);

            return new AuthenticationResponse { IsAuthenticated = true };
        }
    }
}
