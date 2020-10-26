using Identity.Models.Response;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IIdentityService
    {
        Task<LoginResponse> LoginAsync(string email, string password);

        Task<RegistrationResponse> RegisterAsync(IdentityUser user, string password);

        Task<EmailConfirmationResponse> ConfirmEmailAsync(string email, string token);

        Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user);
    }
}