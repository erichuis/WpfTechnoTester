using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using TodoApi.Services;
using WpfTechnoTester.Clients;
using WpfTechnoTester.Services;
using WpfTechnoTester.ViewModels;
using WpfTechnoTester.Views;

namespace WpfTechnoTester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private readonly IHost _host;

        public IServiceProvider? ServiceProvider { get; private set; }

        //public App()
        //{
        //    //_host = Host.CreateDefaultBuilder()
        //    //    .ConfigureServices((context, services) =>
        //    //    {
        //    //        services.AddSingleton(typeof(IHttpAppClient), typeof(HttpAppClient));
        //    //        services.AddTransient<MainWindow>();
        //    //        services.AddTransient<UserSignup>();
        //    //        services.AddTransient<TodoItemViewModel>();
        //    //        services.AddTransient(typeof(IUserService), typeof(UserService));
        //    //        services.AddTransient<UserSignupViewModel>();
        //    //    })
        //    //    .Build();
        //}

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Register clients
            services.AddSingleton(typeof(IHttpAppClient), typeof(HttpAppClient));

            //Register services
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            //Register views
            services.AddTransient<MainWindow>();
            services.AddTransient<UserSignup>();
            services.AddTransient<UserLogin>();

            //register viewmodels
            services.AddTransient<TodoItemViewModel>();
            services.AddTransient<UserSignupViewModel>();
            services.AddTransient<UserLoginViewModel>();

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }
       
        protected override async void OnExit(ExitEventArgs e)
        {
            //await _host.StopAsync();
            //_host.Dispose();
            //ServiceProvider = null;
            base.OnExit(e);
        }
    }

}
