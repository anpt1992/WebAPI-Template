using AutoMapper;
using System.Linq;
using WebAPI_Template.Domain;

namespace WebAPI_Template.Mapping
{
    public class DomainToResponseProfile:Profile
    {
       public DomainToResponseProfile()
        {
            CreateMap<Post, Contracts.V1.Responses.PostResponse>()
                .ForMember(dest=>dest.Tags,opt => opt.MapFrom(src =>src.Tags.Select(x=>new Contracts.V1.Responses.TagResponse { Name = x.TagName})));
            CreateMap<Tag, Contracts.V1.Responses.TagResponse>();

            CreateMap<Post, Contracts.V2.Responses.PostResponse>()
                .ForMember(dest => dest.Name2, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(x => new Contracts.V2.Responses.TagResponse { Name = x.TagName })));
            CreateMap<Tag, Contracts.V2.Responses.TagResponse>();

        }
    }
}
