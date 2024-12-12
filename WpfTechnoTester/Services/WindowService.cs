using Microsoft.Extensions.DependencyInjection;
using WpfTechnoTester.Views;

namespace WpfTechnoTester.Services
{
    public class WindowService : IWindowService
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowEditTodoItemView()
        {
            // Resolve the NewWindow from the service provider
            var window = _serviceProvider.GetRequiredService<EditTodoItemView>();

            // Optionally set properties or initialize if needed
            window.Show(); // Open the window
        }

        public void ShowNewUserSignup()
        {
            // Resolve the NewWindow from the service provider
            var window = _serviceProvider.GetRequiredService<UserSignup>();

            // Optionally set properties or initialize if needed
            window.Show(); // Open the window
        }

        public void ShowUserLogin()
        {
            // Resolve the NewWindow from the service provider
            var window = _serviceProvider.GetRequiredService<UserLogin>();

            // Optionally set properties or initialize if needed
            window.Show(); // Open the window
        }
    }
}
