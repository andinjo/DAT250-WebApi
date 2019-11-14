﻿using System;
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
                    opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Forum, ForumResponse>(MemberList.Destination);

            CreateMap<CreatePost, Post>(MemberList.Source);
            CreateMap<UpdatePost, Post>(MemberList.Source)
                .ForMember(
                    dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Post, PostResponse>(MemberList.Destination);

            CreateMap<CreateReply, Reply>(MemberList.Source);
            CreateMap<UpdateReply, Reply>(MemberList.Source)
                .ForMember(
                    dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Reply, ReplyResponse>(MemberList.Destination);
        }
    }
}
