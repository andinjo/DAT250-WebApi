using System;
using Models.Core;
using Models.Responses;
using Newtonsoft.Json;
using Xunit;

namespace Web.Test.Responses
{
    public class UserResponseTest : ModelBaseTest
    {
        [Fact]
        public void Map_UserResponseToUser_MapsExpectedProperties()
        {
            var userResponse = new UserResponse
            {
                Email = "email",
                Id = "id",
                Username = "username"
            };

            var user = Mapper.Map<User>(userResponse);

            Assert.Equal(userResponse.Email, user.Email);
            Assert.Equal(userResponse.Id, user.Id);
            Assert.Equal(userResponse.Username, user.Username);
            Assert.Equal(userResponse.AvatarUrl, user.AvatarUrl);
            Assert.Equal(userResponse.CreatedAt, user.CreatedAt);
            Assert.Equal(userResponse.LastLogin, user.LastLogin);
        }

        [Fact]
        public void Deserialize_MapsJsonPropertiesToObjectProperties_ReturnsUserResponse()
        {
            var createdAt = DateTime.Parse("2020-01-01");
            var lastLogin = DateTime.Parse("2020-01-02");
            var expected = new UserResponse
            {
                Email = "email address",
                Id = "user id",
                AvatarUrl = "picture url",
                Username = "nickname",
                CreatedAt = createdAt,
                LastLogin = lastLogin
            };
            var jsonObject = $@"{{
                ""email"": ""{expected.Email}"",
                ""user_id"": ""{expected.Id}"",
                ""picture"": ""{expected.AvatarUrl}"",
                ""nickname"": ""{expected.Username}"",
                ""created_at"": ""{expected.CreatedAt}"",
                ""last_login"": ""{expected.LastLogin}"",
            }}";

            var response = JsonConvert.DeserializeObject<UserResponse>(jsonObject);

            Assert.Equal(response.Email, expected.Email);
            Assert.Equal(response.Id, expected.Id);
            Assert.Equal(response.Username, expected.Username);
            Assert.Equal(response.AvatarUrl, expected.AvatarUrl);
            Assert.Equal(response.CreatedAt, expected.CreatedAt);
            Assert.Equal(response.LastLogin, expected.LastLogin);
        }
    }
}
