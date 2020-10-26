using System.Linq;
using System.Threading.Tasks;
using Identity.Exceptions;
using Identity.Models.Request;
using Identity.Models.Response;
using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IIdentityService identityService, ILogger<LoginController> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LoginResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            try
            {
                var loginResponse = await _identityService.LoginAsync(request.Email, request.Password);

                if(!loginResponse.Success)
                {
                    return BadRequest(new LoginResponse
                    {
                        Errors = loginResponse.Errors
                    });
                }

                return Ok(new LoginResponse
                {
                    Token = loginResponse.Token,
                    Success = true
                });
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, $"Error authenticating user");

                return BadRequest(new { error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("verifyToken")]
        public ActionResult Validate()
        {
            return Ok();
        }
    }
}
