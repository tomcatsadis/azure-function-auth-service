using TomcatSadis.Security.AccessTokenHandler;
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
    public class SignInByEmailTests
    {
        private readonly APITestFixture _apiFixture;

        private readonly PersistenceTestFixture _persistenceFixture;

        public SignInByEmailTests(APITestFixture apiFixture, PersistenceTestFixture persistenceFixture)
        {
            _apiFixture = apiFixture;
            _persistenceFixture = persistenceFixture;
        }

        [Fact]
        public async Task SignInByEmailShouldBeSucceed()
        {
            /* insert data directly */
            var expectedUser = UserFactory.GenerateUserUsingEmail();
            expectedUser = await _persistenceFixture.InjectUser(expectedUser);

            var dateTimeBeforeSignIn = DateTime.UtcNow;

            /* get the data using service */

            var requestBodyObject = new SignInByEmailRequest
            {
                Email = expectedUser.Email,
                Password = UserFactory.DefaultPassword
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBodyObject), Encoding.UTF8, "application/json");
            
            var httpResponse = await _apiFixture.Client.PostAsync(_apiFixture.ApplicationSettings.AuthServiceAPISettings.SignInByEmailUrl, content);

            httpResponse.EnsureSuccessStatusCode();

            var actualTokenReponse = JsonConvert.DeserializeObject<TokenResponse>(
                await httpResponse.Content.ReadAsStringAsync());

            /* check if the actual data from decoded accessToken is equal to expected data */

            Assert.NotNull(actualTokenReponse);
            Assert.NotNull(actualTokenReponse.AccessToken);

            var accessTokenResult = _apiFixture.ValidateToken(actualTokenReponse.AccessToken);

            Assert.Equal(AccessTokenStatus.Valid, accessTokenResult.Status);
            Assert.Equal(expectedUser.Id.ToString(), accessTokenResult.Principal.GetUserId());
            Assert.Equal(expectedUser.FullName, accessTokenResult.Principal.Identity.Name);

            /* get data directly */
            var lastLoginUsingEmail = _persistenceFixture.GetUser(expectedUser.Id)?.LastLoginUsingEmail;

            /* ensure the last login using email datetime is updated */
            Assert.True(lastLoginUsingEmail > dateTimeBeforeSignIn);
        }

        public class SignInByEmailRequest
        {
            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }
        }
    }
}
 