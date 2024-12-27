//using AutoMapper;
using Domain.DataModels;
using Microsoft.Extensions.DependencyInjection;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Register clients
            services.AddSingleton(typeof(IHttpAppClient), typeof(HttpAppClient));

            //register viewmodels
            services.AddTransient<MainViewModel>();
            services.AddSingleton<JournalViewModel>();
            services.AddSingleton<TodoViewModel>();
            services.AddTransient<TodoItemEditViewModel>();
            services.AddSingleton<GameViewModel>();
            services.AddSingleton<ImageViewModel>();
            services.AddTransient<UserSignupViewModel>();
            services.AddTransient<UserLoginViewModel>();
            services.AddSingleton<AdminViewModel>();

            //Register services
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(TodoItemProfile));
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<ITodoItemService, TodoItemService>();

            services.AddSingleton<IViewModelFactory, ViewModelFactory>();

            services.AddSingleton<CreateViewModel<HomeViewModel>>(
                services => { return () => new HomeViewModel(); });

            services.AddSingleton<CreateViewModel<JournalViewModel>>(
                services => { return () => services.GetRequiredService<JournalViewModel>(); });

            services.AddSingleton<CreateViewModel<ImageViewModel>>(
                services => { return () => services.GetRequiredService<ImageViewModel>(); });

            services.AddSingleton<CreateViewModel<GameViewModel>>(
                services => { return () => services.GetRequiredService<GameViewModel>(); });

            services.AddSingleton<CreateViewModel<AdminViewModel>>(
                services => { return () => services.GetRequiredService<AdminViewModel>(); });

            services.AddSingleton<CreateViewModel<TodoViewModel>>(
                services => { return () => services.GetRequiredService<TodoViewModel>(); });
                    //services.GetRequiredService<ITodoItemService>(),
                    //services.GetRequiredService<IWindowService>()
                    //); });

            //Register states
            services.AddSingleton<INavigator, Navigator>();

            //Register views
            services.AddTransient<MainView>();
            services.AddTransient<TodoItemEditView>();
            services.AddTransient<JournalItemEditView>();
            services.AddTransient<UserSignupView>();
            services.AddTransient<UserLoginView>();

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
