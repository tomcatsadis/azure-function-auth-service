using TomcatSadis.Security.AccessTokenHandler;
using TomcatSadis.Security.AccessTokenHandler.Settings;
using AuthService.API.IntegrationTests.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;

namespace AuthService.API.IntegrationTests.Util
{
    public class APITestFixture
    {
        public readonly HttpClient Client;

        public AccessTokenProvider AccessTokenProvider;

        public ApplicationSettings ApplicationSettings;

        public APITestFixture()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            IConfiguration configuration = builder.Build();
            ApplicationSettings = configuration.Get<ApplicationSettings>(c => c.BindNonPublicProperties = true);
            ApplicationSettings.ValidateAttributes();

            Client = new HttpClient
            {
                BaseAddress = new Uri(ApplicationSettings.AuthServiceAPISettings.BaseUrl),
            };
            
            Client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApplicationSettings.AuthServiceAPISettings.OcpApimSubscriptionKey);

            AccessTokenProvider = new AccessTokenProvider(configuration.GetSection(nameof(AccessTokenSettings)).Get<AccessTokenSettings>());
        }

        public AccessTokenResult ValidateToken(string accessToken)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = $"Bearer {accessToken}"; //Set header

            return AccessTokenProvider.ValidateToken(httpContext.Request);
        }

        public int Port { get; } = 7071;
    }
}
