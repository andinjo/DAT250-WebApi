using Newtonsoft.Json;

namespace Models.Requests
{
    public class TokenRequest
    {
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }
        [JsonProperty(PropertyName = "client_secret")]
        public string ClientSecret { get; set; }
        [JsonProperty(PropertyName = "audience")]
        public string Audience { get; set; }
        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType { get; set; }
    }
}
