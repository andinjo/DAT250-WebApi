using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Models.Requests;
using Xunit;

namespace Services.Test.ReplyServiceTest
{
    public class UpdateTest : BaseReplyTest
    {
        [Fact]
        public async Task Update_EverythingOk_UpdatesReply()
        {
            var oldReply = new Reply {Id = 1};
            var update = new UpdateReply();
            var reply = new Reply
            {
                Id = 1,
                Content = "hello"
            };
            A.CallTo(() => ReplyRepository.Read(1)).Returns(oldReply);
            A.CallTo(() => Mapper.Map(update, oldReply)).Returns(reply);
            A.CallTo(() => User.Is(oldReply.UserId)).Returns(true);

            await ReplyService.Update(oldReply.Id, update);

            A.CallTo(() => ReplyRepository.Update(A<Reply>.That.Matches(r =>
                r.Id == reply.Id &&
                r.Content == reply.Content
            ))).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_ReplyNotFound_DoesNotAttemptUpdate()
        {
            var oldReply = new Reply {Id = 1};
            var update = new UpdateReply();
            var reply = new Reply
            {
                Id = 1,
                Content = "hello"
            };
            A.CallTo(() => ReplyRepository.Read(1)).Returns(null as Reply);
            A.CallTo(() => Mapper.Map(update, oldReply)).Returns(reply);
            A.CallTo(() => User.Is(oldReply.UserId)).Returns(true);

            await ReplyService.Update(oldReply.Id, update);

            A.CallTo(() => ReplyRepository.Update(A<Reply>.Ignored))
                .MustNotHaveHappened();
        }

        [Fact]
        public async Task Update_UserDoesNotOwnReply_DoesNotAttemptUpdate()
        {
            var oldReply = new Reply {Id = 1};
            var update = new UpdateReply();
            var reply = new Reply
            {
                Id = 1,
                Content = "hello"
            };

            A.CallTo(() => ReplyRepository.Read(1)).Returns(oldReply);
            A.CallTo(() => Mapper.Map(update, oldReply)).Returns(reply);
            A.CallTo(() => User.Is(oldReply.UserId)).Returns(false);

            await ReplyService.Update(oldReply.Id, update);

            A.CallTo(() => ReplyRepository.Update(A<Reply>.Ignored))
                .MustNotHaveHappened();
        }    }
}
