using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Template.Authorization
{
    public class WorkForCompanyRequirement:IAuthorizationRequirement
    {
        public string DomainName { get; }
        public WorkForCompanyRequirement(string domainName)
        {
            DomainName = domainName;
        }
    }
}
