using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WpfTechnoTester.Clients;
using WpfTechnoTester.ViewModels;

namespace WpfTechnoTester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddScoped(typeof(IHttpAppClient), typeof(HttpAppClient));
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<TodoItemViewModel>();
                    services.AddSingleton<UserSignupViewModel>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }

}
