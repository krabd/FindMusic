using System.Threading.Tasks;
using FindMusic.Entity;
using FindMusic.Utils.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FindMusic.Console
{
    class Program
    {
        private static FindMusic _findMusic;

        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Welcome to the FindMusic!");

            var startupService = new StartupService();
            var services = startupService.Configure();
            var configuration = startupService.ConfigureSettings();

            services.AddDbContext<FindMusicContext>((serviceProvider, options) =>
            {
                var connectionString = configuration.GetConnectionString("Storage");
                options.UseSqlite(connectionString);
            });

            var provider = startupService.BuildProvider(services);

            _findMusic = provider.Resolve<FindMusic>();

            await _findMusic.Run();
        }
    }
}
