using FindMusic.DataAccess.Interfaces;
using FindMusic.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FindMusic.DataAccess
{
    public static class Configurator
    {
        public static IServiceCollection ConfigureDataAccess(this IServiceCollection services)
        {
            services.AddTransient<IMusicRepository, AppleMusicRepository>();
            services.AddTransient<ILocalMusicRepository, LocalMusicRepository>();

            return services;
        }
    }
}
