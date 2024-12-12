﻿using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfTechnoTester.Clients;
using WpfTechnoTester.Services;
using WpfTechnoTester.State;
using WpfTechnoTester.ViewModels;
using WpfTechnoTester.ViewModels.Factories;
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

            //register viewmodels
            services.AddTransient<MainViewModel>();
            services.AddTransient<JournalViewModel>();
            services.AddTransient<TodoViewModel>();
            services.AddTransient<UserSignupViewModel>();
            services.AddTransient<UserLoginViewModel>();

            //Register services
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IAuthenticator, Authenticator>();

            services.AddSingleton<IViewModelFactory, ViewModelFactory>();

            services.AddSingleton<CreateViewModel<HomeViewModel>>(
                services => { return () => new HomeViewModel(); });

            services.AddSingleton<CreateViewModel<JournalViewModel>>(
                services => { return () => services.GetRequiredService<JournalViewModel>(); });

            services.AddSingleton<CreateViewModel<TodoViewModel>>(
                services => { return () => new TodoViewModel(
                    services.GetRequiredService<ITodoItemService>(),
                    services.GetRequiredService<IWindowService>()
                    ); });

            //Register states
            services.AddSingleton<INavigator, Navigator>();

            //Register views
            services.AddTransient<MainView>();
            services.AddTransient<UserSignup>();
            services.AddTransient<UserLogin>();

            ServiceProvider = services.BuildServiceProvider();

            var mainView = ServiceProvider.GetRequiredService<MainView>();
            mainView.Show();

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
