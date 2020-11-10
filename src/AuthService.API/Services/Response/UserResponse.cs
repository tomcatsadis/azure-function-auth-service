using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AuthService.API.Services.Response
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

        public static UserResponse Load(Domain.Entities.User user)
        {
            if (user == null) return null;

            List<ClaimResponse> claims = null;

            if (user.Claims != null)
            {
                claims = new List<ClaimResponse>(user.Claims.Count);

                user.Claims.ForEach((claim) =>
                {
                    claims.Add(ClaimResponse.Load(claim));
                });
            }

            return new UserResponse
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                IsEmailVerified = user.IsEmailVerified,
                LastLoginUsingEmail = user.LastLoginUsingEmail,
                Phone = user.Phone,
                IsPhoneVerified = user.IsPhoneVerified,
                LastLoginUsingPhone = user.LastLoginUsingPhone,
                SignUpDate = user.SignUpDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Claims = claims
            };
        }
    }
}
