namespace Middlewares.Middleware.Logging
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path}");

            var start = DateTime.UtcNow;
            await _next(context);
            var end = DateTime.UtcNow;

            _logger.LogInformation($"Request completed in {(end - start).TotalMilliseconds} ms with status {context.Response.StatusCode}");
        }
    }
}
