using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Models.Requests;
using Xunit;

namespace Services.Test.ForumServiceTest
{
    public class CreateTest : BaseForumTest
    {
        private const string UserId = "user id";

        [Fact]
        public async Task Create_UserNotAuthenticated_ReturnsNull()
        {
            A.CallTo(() => UserService.Exists())
                .Returns(false);

            var forum = await ForumService.Create(new CreateForum());

            Assert.Null(forum);
        }

        [Fact]
        public async Task Create_UserIsAuthenticated_CreatesForum()
        {
            var create = new CreateForum();
            var forum = new Forum {Title = "title", Description = "description"};
            A.CallTo(() => UserService.Exists()).Returns(true);
            A.CallTo(() => Mapper.Map<Forum>(create)).Returns(forum);
            A.CallTo(() => UserService.Id()).Returns(UserId);

            await ForumService.Create(create);

            A.CallTo(() => ForumRepository.Create(A<Forum>.That.Matches(f =>
                f.Title == forum.Title &&
                f.Description == forum.Description &&
                f.UserId == UserId
            ))).MustHaveHappenedOnceExactly();
        }
    }
}
