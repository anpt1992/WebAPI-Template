using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Template.Contracts.V2.Requests.Post
{
    public class UpdatePostRequest
    {
        public string Name { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
