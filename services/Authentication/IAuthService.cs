using System.Threading.Tasks;
using DesignIntentDesktop.Models;
using DesignIntentDesktop.Models.Authentication;

namespace DesignIntentDesktop.services.Authentication
{
    public interface IAuthService
    {
        Task<AuthResponse> Login();
    }
}