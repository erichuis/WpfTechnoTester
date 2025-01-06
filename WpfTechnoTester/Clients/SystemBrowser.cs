namespace WpfTechnoTester.Clients
{
    using IdentityModel.OidcClient.Browser;
    using System.Diagnostics;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class SystemBrowser : IBrowser
    {
        private readonly int _port;

        public SystemBrowser(int port)
        {
            _port = port;
        }

        public Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            var listener = new HttpListener();
            listener.Prefixes.Add($"http://localhost:{_port}/");
            listener.Start();

            Process.Start(new ProcessStartInfo
            {
                FileName = options.StartUrl,
                UseShellExecute = true
            });

            var context = listener.GetContext(); // Wait for the redirect
            var response = context.Response;
            var responseString = "<html><body>You can now close this window.</body></html>";
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();

            listener.Stop();

            return Task.FromResult(new BrowserResult
            {
                Response = context.Request.Url.ToString(),
                ResultType = BrowserResultType.Success
            });
        }
    }
}
