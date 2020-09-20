using Identity.Models.Response;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IIdentityService
    {
        Task<RegistrationResponse> RegisterAsync(string email, string password);
    }
}