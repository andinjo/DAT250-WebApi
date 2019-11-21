using System;
using Newtonsoft.Json;

namespace Models.Responses
{
    public class UserResponse
    {
        [JsonProperty(PropertyName = "user_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "nickname")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "picture")]
        public string AvatarUrl { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "last_login")]
        public DateTime LastLogin { get; set; }
    }
}
