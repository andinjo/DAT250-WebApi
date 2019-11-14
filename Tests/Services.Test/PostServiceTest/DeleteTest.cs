using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Xunit;

namespace Services.Test.PostServiceTest
{
    public class DeleteTest : BasePostTest
    {
        [Fact]
        public async Task Delete_EverythingOk_DeletesPost()
        {
            var post = new Post{Id = 1};
            A.CallTo(() => PostRepository.Read(post.Id)).Returns(post);
            A.CallTo(() => User.Is(post.UserId)).Returns(true);

            await PostService.Delete(post.Id);

            A.CallTo(() => PostRepository.Delete(post)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_PostNotFound_DoesNotDeletePost()
        {
            var post = new Post{Id = 1};
            A.CallTo(() => PostRepository.Read(post.Id)).Returns(null as Post);
            A.CallTo(() => User.Is(post.UserId)).Returns(true);

            await PostService.Delete(post.Id);

            A.CallTo(() => PostRepository.Delete(post)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Delete_UserDoesNotOwnPost_DoesNotDeletePost()
        {
            var post = new Post{Id = 1};
            A.CallTo(() => PostRepository.Read(post.Id)).Returns(post);
            A.CallTo(() => User.Is(post.UserId)).Returns(false);

            await PostService.Delete(post.Id);

            A.CallTo(() => PostRepository.Delete(post)).MustNotHaveHappened();
        }

    }
}
