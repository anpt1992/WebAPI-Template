using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Template.Contracts.V2.Responses
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Name2 { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
