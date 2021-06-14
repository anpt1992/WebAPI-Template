using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Template.Contracts.V1.Responses
{
    public class PagedResponse<T>
    {
        public PagedResponse()
        {
        }
        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;
        }       
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string NextPage { get; set; }
        public string PreviosPage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
