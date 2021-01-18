using System.Net.Http;
using System.Threading.Tasks;
using DesignIntentDesktop.Models;
using DesignIntentDesktop.services.General;
using Microsoft.Extensions.Options;

namespace DesignIntentDesktop.services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;

        private readonly string _controller;

        private readonly AppSettings _settings;

        public AuthService(HttpClient client, IOptions<AppSettings> settings)
        {
            _client = client;
            _settings = settings.Value;
            _controller = "Account";
        }
        
        public async Task<Auth> Login()
        {
            var auth = await GetToken();

            return auth;
        }

        private async Task<Auth> GetToken()
        {
            using (var request = await _client.PostAsJsonAsync($"{_controller}/Login", new LoginDto{Email = _settings.UserName, Password = _settings.Password}))
            {
                if (request.IsSuccessStatusCode)
                {
                    return await request.Content.ReadAsAsync<Auth>();
                }
            }

            return null;
        }
    }
}