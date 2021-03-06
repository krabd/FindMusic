﻿using FindMusic.Core.Interfaces;
using FindMusic.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FindMusic.Core
{
    public static class Configurator
    {
        public static IServiceCollection ConfigureCore(this IServiceCollection services)
        {
            services.AddTransient<IFindMusicService, FindMusicService>();

            return services;
        }
    }
}
