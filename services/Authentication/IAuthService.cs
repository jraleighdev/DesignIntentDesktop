using System.Threading.Tasks;
using DesignIntentDesktop.Models;

namespace DesignIntentDesktop.services.Authentication
{
    public interface IAuthService
    {
        Task<Auth> Login();
    }
}