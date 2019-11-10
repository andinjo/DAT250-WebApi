using System.Threading.Tasks;
using FakeItEasy;
using Models.Business;
using Xunit;

namespace Services.Test.ForumServiceTest
{
    public class DeleteTest : BaseForumTest
    {
        [Fact]
        public async Task DeleteForum_ForumDoesNotExist_DoesNotCallDelete()
        {
            A.CallTo(() => ForumRepository.Read(1)).Returns(null as Forum);

            await ForumService.Delete(1);

            A.CallTo(() => ForumRepository.Delete(A<Forum>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task DeleteForum_UserDoesNotOwnForum_DoesNotDelete()
        {
            var forum = new Forum {UserId = "Id"};
            A.CallTo(() => ForumRepository.Read(1)).Returns(forum);
            A.CallTo(() => User.Is(forum.UserId)).Returns(false);

            await ForumService.Delete(1);

            A.CallTo(() => ForumRepository.Delete(A<Forum>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateForum_UserIsAuthorized_ReturnsUpdatedForum()
        {
            var forum = new Forum
            {
                Id = 1,
                UserId = "User id",
            };
            A.CallTo(() => ForumRepository.Read(1)).Returns(forum);
            A.CallTo(() => User.Is(forum.UserId)).Returns(true);

            await ForumService.Delete(1);

            A.CallTo(() => ForumRepository.Delete(A<Forum>.That.Matches(f =>
                f.Id == forum.Id &&
                f.UserId == forum.UserId
            ))).MustHaveHappenedOnceExactly();
        }
    }
}
