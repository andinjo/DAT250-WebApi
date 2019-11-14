using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Xunit;

namespace Services.Test.PostServiceTest
{
    public class ReadTest : BasePostTest
    {
        [Fact]
        public async Task Read_PostExists_ReturnsPost()
        {
            var post = new Post
            {
                Content = "Content"
            };
            A.CallTo(() => PostRepository.Read(1)).Returns(post);

            var response = await PostService.Read(1);

            Assert.Equal(post.Content, response.Content);
            Assert.Equal(post.CreatedAt, response.CreatedAt);
            Assert.Equal(post.UpdatedAt, response.UpdatedAt);
        }

        [Fact]
        public async Task Read_PostDoesNotExist_ReturnsNull()
        {
            A.CallTo(() => PostRepository.Read(1)).Returns(null as Post);

            var response = await PostService.Read(1);

            Assert.Null(response);
        }
    }
}
