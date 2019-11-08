using System;
using AutoMapper;
using Models.Business;
using Models.Request;
using Models.Response;

namespace Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateForum, Forum>(MemberList.Source);
            CreateMap<UpdateForum, Forum>(MemberList.Source)
                .ForMember(
                    dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.Now)
                );
            CreateMap<Forum, ForumResponse>(MemberList.Destination);
        }
    }
}
