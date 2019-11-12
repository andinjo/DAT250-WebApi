using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Xunit;

namespace Services.Test.PostServiceTest
{
    public class ListTest : BasePostTest
    {
        [Fact]
        public async Task List_ForumDoesNotExist_ReturnsNull()
        {
            var list = new List<Post> {new Post()};
            A.CallTo(() => PostRepository.List(1)).Returns(list);

            var posts = await PostService.List(1);

            Assert.Equal(posts, list);
        }
    }
}
