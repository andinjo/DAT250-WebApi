using System;
using Models.Core;
using Models.Requests;
using Xunit;

namespace Web.Test.Requests
{
    public class UpdateReplyTest : ModelBaseTest
    {
        [Fact]
        public void Map_UpdateToReply_MapsExpectedProperties()
        {
            var yesterday = DateTime.Today.AddDays(-1);
            var update = new UpdateReply
            {
                Content = "Updated content"
            };
            var oldReply = new Reply
            {
                Content = "Old reply",
                UpdatedAt = yesterday
            };

            var reply = Mapper.Map(update, oldReply);

            Assert.Equal(update.Content, reply.Content);
            Assert.True(yesterday < reply.UpdatedAt, "updated at was not updated");
        }
    }
}
