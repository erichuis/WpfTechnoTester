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

        public void ShowTodoItemEditViewDialog()
        {
            // Resolve the NewWindow from the service provider
            var window = _serviceProvider.GetRequiredService<TodoItemEditView>();

            // Optionally set properties or initialize if needed
            window.ShowDialog(); // Open the window modal
        }

        public void ShowNewUserSignupDialog()
        {
            // Resolve the NewWindow from the service provider
            var window = _serviceProvider.GetRequiredService<UserSignup>();

            // Optionally set properties or initialize if needed
            window.ShowDialog(); // Open the window modal
        }

        public bool ShowUserLoginDialog()
        {
            // Resolve the NewWindow from the service provider
            var window = _serviceProvider.GetRequiredService<UserLogin>();

            // Optionally set properties or initialize if needed
            var result = window.ShowDialog(); // Open the window modal

            return result ?? false;
        }
    }
}
