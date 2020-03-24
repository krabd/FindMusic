using System;
using FindMusic.Core;
using Microsoft.Extensions.DependencyInjection;

namespace FindMusic.Console
{
    public class StartupService
    {
        public IServiceCollection Configure()
        {
            var services = new ServiceCollection();

            services.AddTransient<FindMusic>();
            services.ConfigureCore();

            return services;
        }

        public IServiceProvider BuildProvider(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }
    }
}
