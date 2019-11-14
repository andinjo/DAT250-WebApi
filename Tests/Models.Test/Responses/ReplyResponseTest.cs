using System;
using Models.Core;
using Models.Responses;
using Xunit;

namespace Web.Test.Responses
{
    public class ReplyResponseTest : ModelBaseTest
    {
        [Fact]
        public void Map_ReplyToResponse_MapsExpectedProperties()
        {
            var reply = new Reply
            {
                Id = 1,
                Content = "Test content",
                CreatedAt = DateTime.Today.AddDays(-1),
                UpdatedAt = DateTime.Today,
            };

            var response = Mapper.Map<ReplyResponse>(reply);

            Assert.Equal(reply.Id, response.Id);
            Assert.Equal(reply.Content, response.Content);
            Assert.Equal(reply.CreatedAt, response.CreatedAt);
            Assert.Equal(reply.UpdatedAt, response.UpdatedAt);
        }
    }
}
