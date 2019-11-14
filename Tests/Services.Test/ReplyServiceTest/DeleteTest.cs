using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Xunit;

namespace Services.Test.ReplyServiceTest
{
    public class DeleteTest : BaseReplyTest
    {
        [Fact]
        public async Task Delete_EverythingOk_DeletesReply()
        {
            var reply = new Reply();
            A.CallTo(() => ReplyRepository.Read(1)).Returns(reply);
            A.CallTo(() => User.Is(reply.UserId)).Returns(true);

            await ReplyService.Delete(1);

            A.CallTo(() => ReplyRepository.Delete(reply))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_Unauthorized_DoesNotAttemptToDeleteReply()
        {
            var reply = new Reply();
            A.CallTo(() => ReplyRepository.Read(1)).Returns(reply);
            A.CallTo(() => User.Is(reply.UserId)).Returns(false);

            await ReplyService.Delete(1);

            A.CallTo(() => ReplyRepository.Delete(A<Reply>.Ignored))
                .MustNotHaveHappened();
        }

        [Fact]
        public async Task Delete_ReplyNotFound_DoesNotAttemptToDeleteReply()
        {
            A.CallTo(() => ReplyRepository.Read(1)).Returns(null as Reply);

            await ReplyService.Delete(1);

            A.CallTo(() => ReplyRepository.Delete(A<Reply>.Ignored))
                .MustNotHaveHappened();
        }
    }
}
