using AuthService.API.IntegrationTests.Response;
using AuthService.API.IntegrationTests.Util;
using AuthService.Domain.UnitTests.Util;
using AuthService.Persistence.IntegrationTests.Util;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.API.IntegrationTests.User
{
    [Collection(nameof(APITestCollection))]
    public class SignUpByEmailTests
    {
        private readonly APITestFixture _apiFixture;

        private readonly PersistenceTestFixture _persistenceFixture;

        public SignUpByEmailTests(APITestFixture apiFixture, PersistenceTestFixture persistenceFixture)
        {
            _apiFixture = apiFixture;
            _persistenceFixture = persistenceFixture;
        }

        [Fact]
        public async Task SignUpByEmailShouldBeSucceed()
        {
            var dateTimeBeforeSignUp = DateTime.UtcNow;

            /* sign up using service */
            var expectedUser = UserFactory.GenerateUserUsingEmail();
            var requestBodyObject = new SignUpByEmailRequest
            {
                Email = expectedUser.Email,
                Password = UserFactory.DefaultPassword,
                FirstName = expectedUser.FirstName,
                LastName = expectedUser.LastName
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBodyObject), Encoding.UTF8, "application/json");

            var httpResponse = await _apiFixture.Client.PostAsync(_apiFixture.ApplicationSettings.AuthServiceAPISettings.SignUpByEmailUrl, content);

            httpResponse.EnsureSuccessStatusCode();

            var actualUserReponse = JsonConvert.DeserializeObject<UserResponse>(
                await httpResponse.Content.ReadAsStringAsync());

            /* check if the created user response is match with expected user */

            Assert.NotNull(actualUserReponse);
            Assert.NotNull(actualUserReponse.Id);
            Assert.Equal(expectedUser.Email, actualUserReponse.Email);
            Assert.Equal(expectedUser.FirstName, actualUserReponse.FirstName);
            Assert.Equal(expectedUser.LastName, actualUserReponse.LastName);
            Assert.True(expectedUser.SignUpDate > dateTimeBeforeSignUp);

            /* check again if the user stored in database is match with expected user */
            var actualUser = _persistenceFixture.GetUserByEmail(expectedUser.Email);

            Assert.NotNull(actualUser);
            Assert.Equal(expectedUser.Email, actualUser.Email);
            Assert.Equal(expectedUser.FirstName, actualUser.FirstName);
            Assert.Equal(expectedUser.LastName, actualUser.LastName);
            Assert.True(expectedUser.SignUpDate > dateTimeBeforeSignUp);
        }

        public class SignUpByEmailRequest
        {
            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }

            [JsonProperty("firstName")]
            public string FirstName { get; set; }

            [JsonProperty("LastName")]
            public string LastName { get; set; }
        }
    }
}
 