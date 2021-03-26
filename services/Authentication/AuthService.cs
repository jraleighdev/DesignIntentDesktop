using System.Net.Http;
using System.Threading.Tasks;
using DesignIntentDesktop.Models;
using DesignIntentDesktop.Models.Authentication;
using DesignIntentDesktop.Models.Response;
using DesignIntentDesktop.services.General;
using Microsoft.Extensions.Options;

namespace DesignIntentDesktop.services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;

        private readonly AppSettings _settings;

        public AuthService(HttpClient client, IOptions<AppSettings> settings)
        {
            _client = client;
            _settings = settings.Value;
        }
        
        public async Task<AuthResponse> Login()
        {
            var auth = await GetToken();

            return auth;
        }

        private async Task<AuthResponse> GetToken()
        {
            using (var request = await _client.PostAsJsonAsync("TokenAuth/Authenticate", new LoginDto{UserNameOrEmailAddress = _settings.UserName, Password = _settings.Password}))
            {
                if (request.IsSuccessStatusCode)
                {
                    var result = await request.Content.ReadAsAsync<AbpResponse<AuthResponse>>();
                    return result.Result;
                }
            }

            return null;
        }
    }
}