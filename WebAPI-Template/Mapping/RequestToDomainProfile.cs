using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Template.Contracts.V1.Requests.Queries;
using WebAPI_Template.Domain;

namespace WebAPI_Template.Mapping
{
    public class RequestToDomainProfile:Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }        
    }
}
