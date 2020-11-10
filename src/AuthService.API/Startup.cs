using TomcatSadis.Security.AccessTokenHandler;
using AuthService.API.Settings;
using AuthService.Application.UseCases;
using AuthService.EventPublisher;
using AuthService.Persistence;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using AccessTokenSettings = AuthService.Domain.Settings.AccessTokenSettings;
using IAccessTokenSettings = AuthService.Domain.Settings.IAccessTokenSettings;

[assembly: FunctionsStartup(typeof(AuthService.API.Startup))]
namespace AuthService.API
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            #region Configuration Dependencies

            builder.Services.AddSingleton<IApplicationSettings>(sp =>
                sp.GetService<IConfiguration>().Get<ApplicationSettings>());

            if (!builder.Services.BuildServiceProvider().GetService<IConfiguration>().GetSection(nameof(AccessTokenSettings)).Exists())
                throw new ArgumentNullException($"'{nameof(AccessTokenSettings)}' configuration section is required.");

            builder.Services.AddSingleton<IAccessTokenSettings>(sp =>
                sp.GetService<IConfiguration>().GetSection(nameof(AccessTokenSettings)).Get<AccessTokenSettings>());

            /*
             * check if configuration is valid
             * */
            var serviceProvider = builder.Services.BuildServiceProvider();
            serviceProvider.GetService<IApplicationSettings>().ValidateAttributes();
            serviceProvider.GetService<IAccessTokenSettings>().ValidateAttributes();

            #endregion

            #region Project Dependencies

            builder.Services.AddScoped<ISignUpUseCase, SignUpUseCase>();

            builder.Services.AddScoped<ISignInUseCase, SignInUseCase>();

            builder.Services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();

            builder.Services.AddScoped<IUserInfoUseCase, UserInfoUseCase>();

            builder.Services.AddSingleton<IAccessTokenProvider, AccessTokenProvider>();

            builder.Services.AddAccessTokenHandler();

            builder.Services.AddPersistence();

            builder.Services.AddEventHubPublisher();

            #endregion
        }
    }
}
