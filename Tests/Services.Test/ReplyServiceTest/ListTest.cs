using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Xunit;

namespace Services.Test.ReplyServiceTest
{
    public class ListTest : BaseReplyTest
    {
        [Fact]
        public async Task List_ReturnsAll()
        {
            var replies = new List<Reply>
            {
                new Reply {Id = 1},
                new Reply {Id = 2}
            };
            A.CallTo(() => ReplyRepository.List(1)).Returns(replies);

            var response = await ReplyService.List(1);

            Assert.Equal(2, response.Count);
        }
    }
}
