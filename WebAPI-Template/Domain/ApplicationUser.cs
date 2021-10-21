using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI_Template.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
                 
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
       
    }

}
