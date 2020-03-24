using FindMusic.Utils.Extensions;

namespace FindMusic.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to the FindMusic!");

            var startupService = new StartupService();
            var serviceCollection = startupService.Configure();
            var provider = startupService.BuildProvider(serviceCollection);

            var findMusic = provider.Resolve<FindMusic>();
            findMusic.Run();

            System.Console.ReadLine();
        }
    }
}
