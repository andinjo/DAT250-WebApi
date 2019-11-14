using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Xunit;

namespace Services.Test.ReplyServiceTest
{
    public class ReadTest : BaseReplyTest
    {
        [Fact]
        public async Task Read_EverythingOk_ReturnsReply()
        {
            var reply = new Reply();
            A.CallTo(() => ReplyRepository.Read(1)).Returns(reply);

            var response = await ReplyService.Read(1);

            Assert.Equal(reply, response);
        }
    }
}
