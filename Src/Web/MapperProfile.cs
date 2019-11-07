using AutoMapper;
using Models;
using Web.Requests;
using Web.Responses;

namespace Web
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateForum, Forum>();
            CreateMap<Forum, ForumResponse>();
        }
    }
}
