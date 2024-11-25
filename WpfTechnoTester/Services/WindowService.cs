using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void ShowNewUserSignup()
        {
            // Resolve the NewWindow from the service provider
            var window = _serviceProvider.GetRequiredService<UserSignup>();

            // Optionally set properties or initialize if needed

            window.Show(); // Open the window
        }
    }
}
