namespace AuthService.API.IntegrationTests.Settings
{
    public sealed class ApplicationSettings
    {
        public AuthServiceAPISettings AuthServiceAPISettings { get; set; }

        public void ValidateAttributes()
        {
            AuthServiceAPISettings.ValidateAttributes();
        }
    }
}
