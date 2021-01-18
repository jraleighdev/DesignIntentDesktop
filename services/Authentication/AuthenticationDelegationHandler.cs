using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace DesignIntentDesktop.services.Authentication
{
    public class AuthenticationDelegationHandler : DelegatingHandler
    {
        private IAuthService _authService;
        
        public AuthenticationDelegationHandler(IAuthService authService)
        {
            _authService = authService;
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _authService.Login();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
            {
                token = await _authService.Login();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                response = await base.SendAsync(request, cancellationToken);
            }

            return response;
        }
    }
}