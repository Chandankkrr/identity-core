using System;

namespace Identity.Models.Response
{
    public class LoginResponse: ServiceResponse
    {
        public string Token { get; set; }
    }
}
