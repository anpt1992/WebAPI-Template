using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Template.Contracts.V1.Responses
{
    public class Response<T>
    {
        public Response(T response)
        {
            Data = response;
        }
        public T Data { get; set; }
    }
}
