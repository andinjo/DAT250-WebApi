using System;
using Models.Business;
using Models.Responses;
using Xunit;

namespace Web.Test.Responses
{
    public class PostResponseTest : ModelBaseTest
    {
        [Fact]
        public void Map_FromPostToPostResponse_MapsExpectedValues()
        {
            var post = new Post
            {
                Id = 1,
                Content = "Content",
                CreatedAt = DateTime.Today.AddDays(-1),
                UpdatedAt = DateTime.Today
            };

            var response = Mapper.Map<PostResponse>(post);

            Assert.Equal(post.Id, response.Id);
            Assert.Equal(post.Content, response.Content);
            Assert.Equal(post.CreatedAt, response.CreatedAt);
            Assert.Equal(post.UpdatedAt, response.UpdatedAt);
        }
    }
}
