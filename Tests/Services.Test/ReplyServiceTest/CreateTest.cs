using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Models.Requests;
using Xunit;

namespace Services.Test.ReplyServiceTest
{
    public class CreateTest : BaseReplyTest
    {
        [Fact]
        public async Task Create_EverythingOk_CreatesReply()
        {
            const string userId = "User id";
            var create = new CreateReply();
            var reply = new Reply {Content = "Content"};
            var post = new Post {Id = 1};
            A.CallTo(() => User.Exists()).Returns(true);
            A.CallTo(() => Mapper.Map<Reply>(create)).Returns(reply);
            A.CallTo(() => User.Id()).Returns(userId);
            A.CallTo(() => PostService.Read(1)).Returns(post);

            await ReplyService.Create(1, create);

            A.CallTo(() => ReplyRepository.Create(A<Reply>.That.Matches(r =>
                r.Content == reply.Content &&
                r.UserId == userId &&
                r.Post.Id == post.Id
            ))).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Create_Unauthenticated_DoesNotCreateReply()
        {
            var create = new CreateReply();
            var reply = new Reply {Content = "Content"};
            var post = new Post {Id = 1};
            A.CallTo(() => Mapper.Map<Reply>(create)).Returns(reply);
            A.CallTo(() => User.Exists()).Returns(false);
            A.CallTo(() => PostService.Read(1)).Returns(post);

            var response = await ReplyService.Create(1, create);

            Assert.Null(response);
            A.CallTo(() => ReplyRepository.Create(A<Reply>.Ignored))
                .MustNotHaveHappened();
        }

        [Fact]
        public async Task Create_PostDoesNotExist_DoesNotCreateReply()
        {
            var create = new CreateReply();
            var reply = new Reply {Content = "Content"};
            const string userId = "User id";
            A.CallTo(() => Mapper.Map<Reply>(create)).Returns(reply);
            A.CallTo(() => User.Exists()).Returns(true);
            A.CallTo(() => PostService.Read(1)).Returns(null as Post);
            A.CallTo(() => User.Id()).Returns(userId);

            var response = await ReplyService.Create(1, create);

            Assert.Null(response);
            A.CallTo(() => ReplyRepository.Create(A<Reply>.Ignored))
                .MustNotHaveHappened();
        }
    }
}
