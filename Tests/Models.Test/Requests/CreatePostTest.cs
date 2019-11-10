using System;
using Models.Business;
using Models.Requests;
using Xunit;

namespace Web.Test.Requests
{
    public class CreatePostTest : ModelBaseTest
    {
        [Fact]
        public void Map_FromCreateToPost_MapsAllPropertiesFromSource()
        {
            var create = new CreatePost {Content = "Content"};

            var post = Mapper.Map<Post>(create);

            Assert.Equal(create.Content, post.Content);
            Assert.True(post.CreatedAt > DateTime.Now.AddMilliseconds(-10));
        }
    }
}
