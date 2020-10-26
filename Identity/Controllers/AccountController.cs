using Identity.Models.Request;
using Identity.Models.Response;
using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IIdentityService identityService, ILogger<AccountController> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new RegistrationResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
            };

            var registrationResponse = await _identityService.RegisterAsync(user, request.Password);

            if (!registrationResponse.Success)
            {
                return BadRequest(new RegistrationResponse
                {
                    Errors = registrationResponse.Errors
                });
            }

            var emailConfirmationToken = await _identityService.GenerateEmailConfirmationTokenAsync(user);
            var emailConfirmationLink = Url.Action(nameof(VerifyEmail), "Account", new
            {
                email = user.Email,
                token = emailConfirmationToken
            },
                Request.Scheme
            );

            _logger.LogInformation($"{request.Email}, successfully registered, email confirmation link {emailConfirmationLink}");

            return Ok("User registration successful");
        }


        [HttpGet("verifyemail")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail([FromQuery] EmailConfirmationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new EmailConfirmationResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var emailConfirmationResponse = await _identityService.ConfirmEmailAsync(request.Email, request.Token);

            if (!emailConfirmationResponse.Success)
            {
                return BadRequest(emailConfirmationResponse.Errors);
            }

            return Ok(emailConfirmationResponse);
        }
    }
}
