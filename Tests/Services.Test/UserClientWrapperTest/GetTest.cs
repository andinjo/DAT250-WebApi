using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FakeItEasy;
using Models.Core;
using Models.Responses;
using Refit;
using Xunit;

namespace Services.Test.UserClientWrapperTest
{
    public class GetTest : UserClientWrapperBaseTest
    {
        private const string UserId = "user id";

        [Fact]
        public async Task Get_EverythingOk_ReturnsUser()
        {
            var userResponse = new UserResponse();
            var expectedUser = new User
            {
                Email = "test@domain.com",
                Id = UserId
            };
            A.CallTo(() => Client.Get(UserId, AccessToken.Value))
                .Returns(userResponse);
            A.CallTo(() => Mapper.Map<User>(userResponse))
                .Returns(expectedUser);

            var user = await ClientWrapper.Get(UserId);

            Assert.Equal(expectedUser.Email, user.Email);
            Assert.Equal(expectedUser.Id, user.Id);
        }

        [Fact]
        public async Task Get_UserNotFound_ReturnsNull()
        {
            var exception = await ApiException.Create(
                null,
                null,
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                }
            );
            A.CallTo(() => Client.Get(UserId, AccessToken.Value))
                .ThrowsAsync(exception);

            var user = await ClientWrapper.Get(UserId);

            Assert.Null(user);
        }
    }
}
