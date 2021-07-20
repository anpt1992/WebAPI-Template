using AutoMapper;
using WebAPI_Template.Contracts;
using WebAPI_Template.Domain;

namespace WebAPI_Template.Mapping
{
    public class RequestToDomainProfile:Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<Contracts.V1.Requests.Queries.PaginationQuery, PaginationFilter>();           
        }        
    }
}
