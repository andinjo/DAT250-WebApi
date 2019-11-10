using System.Threading.Tasks;
using FakeItEasy;
using Models.Business;
using Models.Requests;
using Xunit;

namespace Services.Test.PostServiceTest
{
    public class CreateTest : BasePostTest
    {
        [Fact]
        public async Task Create_PostIsCreated_ReturnsPostWithExpectedValues()
        {
            var create = new CreatePost();
            var post = new Post { Content = "Content"};
            var forum = new Forum { Id = 1 };
            A.CallTo(() => User.Exists()).Returns(true);
            A.CallTo(() => ForumService.Read(forum.Id)).Returns(forum);
            A.CallTo(() => Mapper.Map<Post>(create)).Returns(post);
            A.CallTo(() => User.Id()).Returns("User Id");

            var result = await PostService.Create(1, create);

            Assert.Equal("User Id", result.UserId);
            A.CallTo(() => PostRepository.Create(A<Post>.That.Matches(p =>
                p.Content == "Content" &&
                p.UserId == "User Id" &&
                p.Forum.Id == forum.Id
            ))).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Create_UserNotAuthenticated_ReturnsNull()
        {
            A.CallTo(() => User.Exists()).Returns(false);
            A.CallTo(() => ForumService.Read(1)).Returns(new Forum());

            var post = await PostService.Create(1, new CreatePost());

            Assert.Null(post);
        }
    }
}
