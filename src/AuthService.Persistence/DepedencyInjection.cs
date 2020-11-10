using AuthService.Application.Repositories;
using AuthService.Persistence.Repositories;
using AuthService.Persistence.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AuthService.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            #region Configuration Dependencies

            var configurationSection = services.BuildServiceProvider().GetService<IConfiguration>().GetSection(nameof(DatabaseSettings));

            if (!configurationSection.Exists()) throw new ArgumentNullException($"'{nameof(DatabaseSettings)}' configuration section is required.");

            services.AddSingleton<IDatabaseSettings>(configurationSection.Get<DatabaseSettings>());

            /*
             * check if configuration is valid
             * */
            services.BuildServiceProvider().GetService<IDatabaseSettings>().ValidateAttributes();

            #endregion

            #region Project Dependencies

            services.AddSingleton<IContext, Context>();

            services.AddScoped<IUserRepository, UserRepository>();

            #endregion

            return services;
        }
    }
}
