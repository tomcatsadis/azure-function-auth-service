using AuthService.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AuthService.EventPublisher
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEventHubPublisher(this IServiceCollection services)
        {
            #region Configuration Dependencies

            var configurationSection = services.BuildServiceProvider().GetService<IConfiguration>().GetSection(nameof(EventHubPublisherSettings));

            if (!configurationSection.Exists()) throw new ArgumentNullException($"'{nameof(EventHubPublisherSettings)}' configuration section is required.");

            services.AddSingleton<IEventHubPublisherSettings>(configurationSection.Get<EventHubPublisherSettings>());

            /*
             * check if configuration is valid
             * */
            services.BuildServiceProvider().GetService<IEventHubPublisherSettings>().ValidateAttributes();

            #endregion

            #region Project Dependencies

            services.AddScoped<IEventPublisher, EventHubPublisher>();

            #endregion

            return services;
        }
    }
}
