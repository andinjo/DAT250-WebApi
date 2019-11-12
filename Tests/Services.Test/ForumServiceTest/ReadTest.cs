using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Xunit;

namespace Services.Test.ForumServiceTest
{
    public class ReadTest : BaseForumTest
    {
        [Fact]
        public async Task Read_FetchesForum()
        {
            var forum = new Forum {Id = 1};
            A.CallTo(() => ForumRepository.Read(1))
                .Returns(forum);

            var response = await ForumService.Read(1);

            Assert.Equal(forum.Id, response.Id);
            A.CallTo(() => ForumRepository.Read(1))
                .MustHaveHappenedOnceExactly();
        }
    }
}
