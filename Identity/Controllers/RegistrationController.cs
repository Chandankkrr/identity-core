using Identity.Models.Response;
using Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public RegistrationController(IIdentityService identityService)
        {
            _identityService = identityService;
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

            var registrationResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if(!registrationResponse.Success)
            {
                return BadRequest(new RegistrationResponse
                {
                    Errors = registrationResponse.Errors
                });
            }

            return Ok(new RegistrationResponse
            {
                Token = registrationResponse.Token
            });
        }

        // GET: api/<RegistrarionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RegistrarionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RegistrarionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RegistrarionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegistrarionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
