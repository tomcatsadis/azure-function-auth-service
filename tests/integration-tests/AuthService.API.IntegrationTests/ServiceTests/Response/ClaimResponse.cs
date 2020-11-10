using Newtonsoft.Json;

namespace AuthService.API.IntegrationTests.Response
{
    public class ClaimResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
