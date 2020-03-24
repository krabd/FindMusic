using System.Threading.Tasks;
using FindMusic.Utils.Extensions;

namespace FindMusic.Console
{
    class Program
    {
        private static FindMusic _findMusic;

        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to the FindMusic!");

            var startupService = new StartupService();
            var serviceCollection = startupService.Configure();
            var provider = startupService.BuildProvider(serviceCollection);

            _findMusic = provider.Resolve<FindMusic>();

            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            await _findMusic.Run();
        }
    }
}
