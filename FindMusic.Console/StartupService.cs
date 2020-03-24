using System;
using System.IO;
using FindMusic.Core;
using FindMusic.Entity.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FindMusic.Console
{
    public class StartupService
    {
        public IConfigurationRoot ConfigureSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            return builder.Build();
        }

        public IServiceCollection Configure()
        {
            var services = new ServiceCollection();

            services.AddTransient<FindMusic>();

            services.AddSingleton<IDbContextFactory, DbContextFactory>();
            services.ConfigureCore();

            return services;
        }

        public IServiceProvider BuildProvider(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }
    }
}
