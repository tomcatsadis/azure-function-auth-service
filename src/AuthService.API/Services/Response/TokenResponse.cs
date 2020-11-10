using Newtonsoft.Json;

namespace AuthService.API.Services.Response
{
    public class TokenResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("tokenType")]
        public string TokenType { get; set; }

        [JsonProperty("expiresIn")]
        public double ExpiresIn { get; set; }

        public static TokenResponse Load(Domain.Entities.Token token)
        {
            if (token == null) return null;

            return new TokenResponse
            {
                AccessToken = token.AccessToken,
                TokenType = token.TokenType,
                ExpiresIn = token.ExpiresIn
            };
        }
    }
}
