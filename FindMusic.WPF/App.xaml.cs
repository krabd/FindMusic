using System.Windows;
using FindMusic.Entity;
using FindMusic.Utils.Extensions;
using FindMusic.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FindMusic.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var startupService = new StartupService();
            var configuration = startupService.ConfigureSettings();

            var services = startupService.Configure();

            services.AddDbContext<FindMusicContext>((serviceProvider, options) =>
            {
                var connectionString = configuration.GetConnectionString("Storage");
                options.UseSqlite(connectionString);
            });

            var provider = startupService.BuildProvider(services);

            var findMusic = provider.Resolve<FindMusicViewModel>();
            var window = new MainWindow {DataContext = findMusic};
            Current.MainWindow = window;
            window.Show();

            base.OnStartup(e);
        }
    }
}
