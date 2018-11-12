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
            var uriBuilder = new UriBuilder($"{_api.DefaultEndPoint}")
            {
                Path = $"api/users",
                Query = $"username={userName}"
            };

            //var uri = new Uri($"{_api.DefaultEndPoint}/api/users/{userName}");

            var user = await _api.GetAsync<User>(uriBuilder.Uri.AbsoluteUri);

            return new AuthenticationResponse
            {
                User = user,
                IsAuthenticated = user != null
            };
        }
    }
}
