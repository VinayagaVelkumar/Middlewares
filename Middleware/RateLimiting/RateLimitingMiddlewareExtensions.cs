namespace Middlewares.Middleware.RateLimiting
{
    public static class RateLimitingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder app, int rateLimitInSeconds = 1)
        {
            return app.UseMiddleware<RateLimitingMiddleware>(rateLimitInSeconds);
        }
    }

}
