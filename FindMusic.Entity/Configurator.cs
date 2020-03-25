using FindMusic.Entity.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace FindMusic.Entity
{
    public static class Configurator
    {
        public static IServiceCollection ConfigureEntity(this IServiceCollection services)
        {
            services.AddSingleton<IDbContextFactory, DbContextFactory>();

            return services;
        }
    }
}
