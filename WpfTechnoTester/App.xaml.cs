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

            // Register http clients
            services.AddTransient<OidcLoginService>(); //todo use interface
            services.AddSingleton<IHttpAuthenticationClient, HttpAuthenticationClient>();
            services.AddSingleton(typeof(IHttpTodoItemClient), typeof(HttpTodoItemClient));
            services.AddSingleton<IHttpJournalEntryClient, HttpJournalEntryClient>();
            services.AddSingleton<IHttpUserClient, HttpUserClient>();

            //register viewmodels
            services.AddTransient<MainViewModel>();
            services.AddSingleton<JournalEntriesViewModel>();
            services.AddTransient<JournalEntryEditViewModel>();
            services.AddSingleton<TodoItemsViewModel>();
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
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<ITodoItemService, TodoItemService>();
            services.AddSingleton<IJournalEntryService, JournalEntryService>();

            services.AddSingleton<IViewModelFactory, ViewModelFactory>();

            services.AddSingleton<CreateViewModel<HomeViewModel>>(
                services => { return () => new HomeViewModel(); });

            services.AddSingleton<CreateViewModel<JournalEntriesViewModel>>(
                services => { return () => services.GetRequiredService<JournalEntriesViewModel>(); });

            services.AddSingleton<CreateViewModel<ImageViewModel>>(
                services => { return () => services.GetRequiredService<ImageViewModel>(); });

            services.AddSingleton<CreateViewModel<GameViewModel>>(
                services => { return () => services.GetRequiredService<GameViewModel>(); });

            services.AddSingleton<CreateViewModel<AdminViewModel>>(
                services => { return () => services.GetRequiredService<AdminViewModel>(); });

            services.AddSingleton<CreateViewModel<TodoItemsViewModel>>(
                services => { return () => services.GetRequiredService<TodoItemsViewModel>(); });
            //services.GetRequiredService<ITodoItemService>(),
            //services.GetRequiredService<IWindowService>()
            //); });

            services.AddSingleton<CreateViewModel<JournalEntriesViewModel>>(
                services => { return () => services.GetRequiredService<JournalEntriesViewModel>(); });

            //Register states
            services.AddSingleton<INavigator, Navigator>();

            //Register views
            services.AddTransient<MainView>();
            services.AddTransient<TodoItemEditView>();
            services.AddTransient<JournalEntryEditView>();
            services.AddTransient<UserSignupView>();
            services.AddTransient<UserLoginBrowserView>();

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
