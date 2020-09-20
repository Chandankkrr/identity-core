using Identity.Interfaces;
using Identity.Models.Response;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public IdentityService(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<RegistrationResponse> RegisterAsync(string email, string password)
        {
            var userExists = await _userManager.FindByEmailAsync(email);

            if(userExists != null)
            {
                return new RegistrationResponse
                {
                    Errors = new[] { $"User with email {email} already exists" }
                };   
            }

            var user = new IdentityUser
            {
                Email = email,
                UserName = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new RegistrationResponse
                {
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            var token = _tokenService.GenerateToken(user.Id);

            return new RegistrationResponse
            {
                Success = true,
                Token = token
            };
        }
    }
}
