using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Template.Domain;

namespace WebAPI_Template.Contracts.V2.Requests.Post
{
    public class FilterPostRequest : PaginationFilter
    {
        public string Name { get; set; }

    }
}
