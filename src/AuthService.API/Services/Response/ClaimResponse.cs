
using Newtonsoft.Json;

namespace AuthService.API.Services.Response
{
    public class ClaimResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        public static ClaimResponse Load(Domain.Entities.Claim claim)
        {
            if (claim == null) return null;

            return new ClaimResponse
            {
                Type = claim.Type,
                Value = claim.Value,
            };
        }
    }
}
