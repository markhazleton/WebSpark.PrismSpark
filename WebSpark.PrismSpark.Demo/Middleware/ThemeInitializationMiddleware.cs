namespace WebSpark.PrismSpark.Demo.Middleware
{
    /// <summary>
    /// Middleware to ensure default themes are set for new sessions
    /// </summary>
    public class ThemeInitializationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ThemeInitializationMiddleware> _logger;

        public ThemeInitializationMiddleware(RequestDelegate next, ILogger<ThemeInitializationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if this is a new session and initialize default themes
            if (!context.Session.Keys.Contains("PrismTheme"))
            {
                context.Session.SetString("PrismTheme", "yeti");
                context.Session.SetString("BootstrapTheme", "yeti");
                context.Session.SetString("ColorMode", "light");

                _logger.LogDebug("Initialized default themes for new session");
            }

            await _next(context);
        }
    }

    /// <summary>
    /// Extension method to add the theme initialization middleware
    /// </summary>
    public static class ThemeInitializationMiddlewareExtensions
    {
        public static IApplicationBuilder UseThemeInitialization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ThemeInitializationMiddleware>();
        }
    }
}
