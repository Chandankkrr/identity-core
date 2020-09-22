using Identity.Models.Response;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IIdentityService
    {
        Task<RegistrationResponse> RegisterAsync(IdentityUser user, string password);

        Task<EmailConfirmationResponse> ConfirmEmailAsync(string email, string token);

        Task<string> GenerateEmailConfirmationToken(IdentityUser user);
    }
}