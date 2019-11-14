using System;
using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Models.Requests;
using Xunit;

namespace Services.Test.PostServiceTest
{
    public class UpdateTest : BasePostTest
    {
        [Fact]
        public async Task Update_EverythingOk_UpdatesPost()
        {
            var oldPost = new Post();
            var update = new UpdatePost();
            var post = new Post {Content = "Content"};
            const string userId = "User id";
            A.CallTo(() => User.Is(post.UserId)).Returns(true);
            A.CallTo(() => User.Id()).Returns(userId);
            A.CallTo(() => PostRepository.Read(1)).Returns(oldPost);
            A.CallTo(() => Mapper.Map(update, oldPost))
                .Returns(post);

            await PostService.Update(1, update);

            A.CallTo(() => PostRepository.Update(A<Post>.That.Matches(p =>
                p.Content == post.Content &&
                p.UserId == userId
            ))).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_PostIsNotFound_DoesNotUpdatePost()
        {
            var oldPost = new Post();
            var update = new UpdatePost();
            var post = new Post {Content = "Content"};
            const string userId = "User id";
            A.CallTo(() => User.Is(post.UserId)).Returns(true);
            A.CallTo(() => User.Id()).Returns(userId);
            A.CallTo(() => PostRepository.Read(1)).Returns(null as Post);
            A.CallTo(() => Mapper.Map(update, oldPost))
                .Returns(post);

            var newPost = await PostService.Update(1, update);

            A.CallTo(() => PostRepository.Update(A<Post>.That.Matches(p =>
                p.Content == post.Content &&
                p.UserId == userId
            ))).MustNotHaveHappened();
            Assert.Null(newPost);
        }

        [Fact]
        public async Task Update_Unauthorized_DoesNotUpdatePost()
        {
            var oldPost = new Post();
            var update = new UpdatePost();
            var post = new Post {Content = "Content"};
            const string userId = "User id";
            A.CallTo(() => User.Is(post.UserId)).Returns(false);
            A.CallTo(() => User.Id()).Returns(userId);
            A.CallTo(() => PostRepository.Read(1)).Returns(oldPost);
            A.CallTo(() => Mapper.Map(update, oldPost))
                .Returns(post);

            var newPost = await PostService.Update(1, update);

            A.CallTo(() => PostRepository.Update(A<Post>.That.Matches(p =>
                p.Content == post.Content &&
                p.UserId == userId
            ))).MustNotHaveHappened();
            Assert.Null(newPost);
        }
    }
}
