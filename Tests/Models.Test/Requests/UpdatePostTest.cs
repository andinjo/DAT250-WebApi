using System;
using System.ComponentModel.DataAnnotations;
using Models.Core;
using Models.Requests;
using Xunit;

namespace Web.Test.Requests
{
    public class UpdatePostTest : ModelBaseTest
    {
        [Fact]
        public void Map_FromUpdatePostToPost_MapsAllPropertiesFromSourceAndUpdatesUpdatedAt()
        {
            var yesterday = DateTime.Today.AddDays(-1);
            var oldPost = new Post
            {
                Id = 1,
                UserId = "User id",
                Content = "This is an old description",
                CreatedAt = yesterday,
                UpdatedAt = yesterday
            };
            var update = new UpdatePost
            {
                Content = "This is an updated description"
            };

            var post = Mapper.Map(update, oldPost);

            Assert.Equal(oldPost.Id, post.Id);
            Assert.Equal(oldPost.UserId, post.UserId);
            Assert.Equal(update.Content, post.Content);
            Assert.Equal(yesterday, post.CreatedAt);
            Assert.True(yesterday < post.UpdatedAt, "oldPost.UpdatedAt < post.UpdatedAt");
        }

        [Fact]
        public void Validate_ValidUpdatePost_SuccessfullyValidates()
        {
            var update = new UpdatePost {Content = "This is a test"};

            var context = new ValidationContext(update);
            var result = Validator.TryValidateObject(update, context, null, true);

            Assert.True(result);
        }

        [Fact]
        public void Validate_UpdatePost_MustHaveContent()
        {
            var update = new UpdatePost();
            var context = new ValidationContext(update)
            {
                MemberName = nameof(update.Content)
            };

            var result = Validator.TryValidateProperty(update.Content, context, null);

            Assert.False(result);
        }
    }
}
