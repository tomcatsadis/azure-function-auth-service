using Newtonsoft.Json;

namespace AuthService.API.IntegrationTests.Response
{
    public class TokenResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("tokenType")]
        public string TokenType { get; set; }

        [JsonProperty("expiresIn")]
        public double ExpiresIn { get; set; }
    }
}
