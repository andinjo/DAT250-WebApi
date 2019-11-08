using System;
using AutoMapper;
using Models;
using Models.Business;
using Models.Response;
using Xunit;

namespace Web.Test.Responses
{
    public class ForumResponseTest
    {
        private readonly IMapper _mapper;

        public ForumResponseTest()
        {
            var mapperConfig = new MapperConfiguration(config =>
                config.AddProfile(typeof(MapperProfile)));

            _mapper = new Mapper(mapperConfig);
        }

        [Fact]
        public void MapToResponse_FromForum_FillsTitle()
        {
            var forum = new Forum
            {
                Id = 1,
                Description = "Description",
                Title = "Title",
                CreatedAt = DateTime.Today,
                UpdatedAt = DateTime.Now
            };

            var response = _mapper.Map<ForumResponse>(forum);

            Assert.Equal(forum.Id, response.Id);
            Assert.Equal(forum.Description, response.Description);
            Assert.Equal(forum.Title, response.Title);
            Assert.Equal(forum.CreatedAt, response.CreatedAt);
            Assert.Equal(forum.UpdatedAt, response.UpdatedAt);
        }
    }
}
