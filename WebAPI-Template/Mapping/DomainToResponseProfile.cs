using AutoMapper;
using System.Linq;
using WebAPI_Template.Contracts.V1.Responses;
using WebAPI_Template.Domain;

namespace WebAPI_Template.Mapping
{
    public class DomainToResponseProfile:Profile
    {
       public DomainToResponseProfile()
        {
            CreateMap<Post, PostResponse>().ForMember(dest=>dest.Tags,opt => opt.MapFrom(src =>src.Tags.Select(x=>new TagResponse { Name = x.TagName})));
            CreateMap<Tag, TagResponse>();

        }
    }
}
