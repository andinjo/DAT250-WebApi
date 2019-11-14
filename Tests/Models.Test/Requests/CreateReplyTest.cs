using System;
using System.ComponentModel.DataAnnotations;
using Models.Core;
using Models.Requests;
using Xunit;

namespace Web.Test.Requests
{
    public class CreateReplyTest : ModelBaseTest
    {
        [Fact]
        public void Map_FromCreateToReply_MapsExpectedProperties()
        {
            var create = new CreateReply
            {
                Content = "This is content"
            };

            var reply = Mapper.Map<Reply>(create);

            Assert.Equal(create.Content, reply.Content);
            var justNow = DateTime.Now.AddMilliseconds(-10);
            Assert.True(justNow < reply.CreatedAt, "justNow < reply.CreatedAt");
            Assert.True(justNow < reply.UpdatedAt, "justNow < reply.UpdatedAt");
        }

        [Fact]
        public void Validate_EverythingOk_SucceedsValidation()
        {
            var create = new CreateReply
            {
                Content = "Valid content"
            };

            var context = new ValidationContext(create);
            var result = Validator.TryValidateObject(create, context, null, true);

            Assert.True(result, "Validation of all properties failed");
        }

        [Fact]
        public void Validate_ContentNotSpecified_FailsValidation()
        {
            var create = new CreateReply
            {
                Content = string.Empty
            };

            var context = new ValidationContext(create)
            {
                MemberName = nameof(create.Content)
            };
            var result = Validator.TryValidateProperty(create.Content, context, null);

            Assert.False(result, "Validation of content succeeded");
        }
    }
}
