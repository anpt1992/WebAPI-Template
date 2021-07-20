using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Template.Contracts.V2.Requests.User
{
    public class UserLoginRequest
    {       
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
