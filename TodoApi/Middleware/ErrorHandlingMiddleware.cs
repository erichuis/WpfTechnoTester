namespace TodoApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Call the next middleware

                if (context.Response == null)
                {
                    throw new HttpRequestException(HttpRequestError.Unknown, "Something happened!");
                }
                else if (context.Response.StatusCode == StatusCodes.Status401Unauthorized) 
                {
                    // Log the 401 status
                    _logger.LogWarning("Unauthorized access attempt.");

                    // Modify the response
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        System.Text.Json.JsonSerializer.Serialize(new
                        {
                            Status = 401,
                            Message = "You are not authorized to access this resource."
                        })
                    );
                }
                else if (context.Response.StatusCode == 404)
                {
                    // Handle 404 status code
                    _logger.LogInformation("Resource not found.");
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        System.Text.Json.JsonSerializer.Serialize(new
                        {
                            Status = 404,
                            Message = "The requested resource was not found."
                        })
                    );
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorDetails = new
            {
                Status = 500,
                Message = "An internal server error occurred.",
                Detailed = exception.Message // Optionally include in development only
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500; // Internal Server Error

            return context.Response.WriteAsJsonAsync(errorDetails);
        }
    }

}
