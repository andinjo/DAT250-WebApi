using System;
using System.ComponentModel.DataAnnotations;
using Models.Core;
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

        [Fact]
        public void Validate_ValidCreatePost_SuccessfullyValidates()
        {
            var create = new CreatePost {Content = "This is a test"};

            var context = new ValidationContext(create);
            var result = Validator.TryValidateObject(create, context, null, true);

            Assert.True(result);
        }

        [Fact]
        public void Validate_CreatePost_MustHaveContent()
        {
            var create = new CreatePost();
            var context = new ValidationContext(create)
            {
                MemberName = nameof(create.Content)
            };

            var result = Validator.TryValidateProperty(create.Content, context, null);

            Assert.False(result);
        }
    }
}
