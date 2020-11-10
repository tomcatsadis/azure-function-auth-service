using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AuthService.API.IntegrationTests.Response
{
    public class UserResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("isEmailVerified")]
        public bool IsEmailVerified { get; set; }

        [JsonProperty("lastLoginUsingEmail")]
        public DateTime LastLoginUsingEmail { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("isPhoneVerified")]
        public bool IsPhoneVerified { get; set; }

        [JsonProperty("lastLoginUsingPhone")]
        public DateTime LastLoginUsingPhone { get; set; }

        [JsonProperty("signUpDate")]
        public DateTime SignUpDate { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("claims")]
        public List<ClaimResponse> Claims { get; set; }
    }
}
