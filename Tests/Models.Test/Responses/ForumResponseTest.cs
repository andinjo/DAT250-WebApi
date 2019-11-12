using System;
using Models.Core;
using Models.Responses;
using Xunit;

namespace Web.Test.Responses
{
    public class ForumResponseTest : ModelBaseTest
    {
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

            var response = Mapper.Map<ForumResponse>(forum);

            Assert.Equal(forum.Id, response.Id);
            Assert.Equal(forum.Description, response.Description);
            Assert.Equal(forum.Title, response.Title);
            Assert.Equal(forum.CreatedAt, response.CreatedAt);
            Assert.Equal(forum.UpdatedAt, response.UpdatedAt);
        }
    }
}
