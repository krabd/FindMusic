using System;
using System.IO;
using FindMusic.Core;
using FindMusic.Entity.Helpers;
using FindMusic.WPF.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FindMusic.WPF
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

            services.AddTransient<FindMusicViewModel>();

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
