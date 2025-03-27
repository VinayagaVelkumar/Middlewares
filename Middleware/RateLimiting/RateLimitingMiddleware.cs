namespace Middlewares.Middleware.RateLimiting
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly Dictionary<string, DateTime> _requestTimes = new();
        private readonly ILogger<RateLimitingMiddleware> _logger;
        private readonly int _rateLimitInSeconds;

        public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger, int rateLimitInSeconds = 1)
        {
            _next = next;
            _logger = logger;
            _rateLimitInSeconds = rateLimitInSeconds;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            if (_requestTimes.TryGetValue(ipAddress, out var lastRequest) &&
                (DateTime.UtcNow - lastRequest).TotalSeconds < _rateLimitInSeconds)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }

            _requestTimes[ipAddress] = DateTime.UtcNow;
            await _next(context);
        }
    }

}
