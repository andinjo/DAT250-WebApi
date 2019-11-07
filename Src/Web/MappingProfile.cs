using AutoMapper;
using Models;
using Web.Requests;
using Web.Responses;

namespace Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateForum, Forum>();
            CreateMap<Forum, ForumResponse>();
        }
    }
}
