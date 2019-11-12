using System;
using AutoMapper;
using Models.Core;
using Models.Requests;
using Models.Responses;

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

            CreateMap<CreatePost, Post>(MemberList.Source);
            CreateMap<Post, PostResponse>(MemberList.Destination);
        }
    }
}
