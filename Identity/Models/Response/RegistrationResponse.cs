using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models.Response
{
    public class RegistrationResponse: ServiceResponse
    {
        public string Token { get; set; }
    }
}
