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

        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to the FindMusic!");

            var startupService = new StartupService();
            var configuration = startupService.ConfigureSettings();

            var services = startupService.Configure();

            services.AddDbContext<FindMusicContext>((serviceProvider, options) =>
            {
                var connectionString = configuration.GetConnectionString("Storage");
                options.UseSqlite(connectionString);
            });

            var provider = startupService.BuildProvider(services);

            _findMusic = provider.Resolve<FindMusic>();

            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            await _findMusic.Run();
        }
    }
}
