using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Models.Requests;
using Xunit;

namespace Services.Test.ForumServiceTest
{
    public class UpdateTest : BaseForumTest
    {
        [Fact]
        public async Task UpdateForum_ForumDoesNotExist_ReturnsNull()
        {
            A.CallTo(() => ForumRepository.Read(1)).Returns(null as Forum);

            var response = await ForumService.Update(1, new UpdateForum());

            Assert.Null(response);
        }

        [Fact]
        public async Task UpdateForum_UserDoesNotOwnForum_ReturnsNull()
        {
            var forum = new Forum {UserId = "Id"};
            A.CallTo(() => ForumRepository.Read(1)).Returns(forum);
            A.CallTo(() => User.Is(forum.UserId)).Returns(false);

            var response = await ForumService.Update(1, new UpdateForum());

            Assert.Null(response);
        }

        [Fact]
        public async Task UpdateForum_UserIsAuthorized_ReturnsUpdatedForum()
        {
            var forum = new Forum
            {
                Description = "Description",
                UserId = "User id",
            };
            A.CallTo(() => ForumRepository.Read(1)).Returns(forum);
            A.CallTo(() => User.Is(forum.UserId)).Returns(true);

            var update = await ForumService.Update(1, new UpdateForum());

            A.CallTo(() => ForumRepository.Update(A<Forum>.That.Matches(f =>
                f.Description == forum.Description &&
                f.UserId == forum.UserId
            ))).MustHaveHappenedOnceExactly();
            Assert.Equal(forum, update);
        }
    }
}
