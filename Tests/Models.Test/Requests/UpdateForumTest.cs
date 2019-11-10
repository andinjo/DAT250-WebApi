using System;
using System.ComponentModel.DataAnnotations;
using Models.Business;
using Models.Requests;
using Xunit;

namespace Web.Test.Requests
{
    public class UpdateForumTest : ModelBaseTest
    {
        [Fact]
        public void MapForum_FromUpdateForum_UpdatesDescriptionAndUpdatedAt()
        {
            var request = new UpdateForum
            {
                Description = "Test"
            };
            var yesterday = DateTime.Today.AddDays(-1);
            var oldForum = new Forum
            {
                Id = 1,
                Title = "Test",
                Description = null,
                CreatedAt = yesterday,
                UpdatedAt = yesterday
            };

            var forum = Mapper.Map(request, oldForum);

            Assert.Equal(1, forum.Id);
            Assert.Equal("Test", forum.Title);
            Assert.Equal(request.Description, forum.Description);
            Assert.Equal(yesterday, forum.CreatedAt);
            Assert.NotEqual(yesterday, forum.UpdatedAt);
        }

        [Fact]
        public void ValidateUpdateForum_NoDescription_ValidationFails()
        {
            var request = new UpdateForum();

            var context = new ValidationContext(request)
            {
                MemberName = nameof(request.Description)
            };
            var result = Validator.TryValidateProperty(request.Description, context, null);

            Assert.False(result);
        }

        [Fact]
        public void ValidateUpdateForum_DescriptionTooLong_ValidationFails()
        {
            var request = new UpdateForum
            {
                Description = new string('a', 1024)
            };

            var context = new ValidationContext(request)
            {
                MemberName = nameof(request.Description)
            };
            var result = Validator.TryValidateProperty(request.Description, context, null);

            Assert.False(result);
        }

        [Fact]
        public void ValidateUpdateForum_ObjectOk_ValidationSucceeds()
        {
            var request = new UpdateForum
            {
                Description = "Description"
            };

            var context = new ValidationContext(request);
            var result = Validator.TryValidateObject(request, context, null, true);

            Assert.True(result);
        }
    }
}
