using WebSpark.PrismSpark.Themes;

namespace WebSpark.PrismSpark.Demo.Services
{
    /// <summary>
    /// Service interface for managing theme selection
    /// </summary>
    public interface IThemeService
    {
        string GetCurrentPrismTheme();
        string GetCurrentBootstrapTheme();
        string GetCurrentColorMode();
        void SetPrismTheme(string themeName);
        void SetBootstrapTheme(string themeName);
        void SetColorMode(string colorMode);
        void ResetToDefaults();
        IEnumerable<string> GetAvailablePrismThemes();
        string GenerateThemeCss(string? themeName = null);
    }

    /// <summary>
    /// Service for managing theme selection with session persistence
    /// </summary>
    public class ThemeService : IThemeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ThemeService> _logger;

        private const string PrismThemeSessionKey = "PrismTheme";
        private const string BootstrapThemeSessionKey = "BootstrapTheme";
        private const string ColorModeSessionKey = "ColorMode";

        // Default theme values
        private const string DefaultPrismTheme = "yeti";
        private const string DefaultBootstrapTheme = "yeti";
        private const string DefaultColorMode = "light";

        public ThemeService(IHttpContextAccessor httpContextAccessor, ILogger<ThemeService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        private ISession? Session => _httpContextAccessor.HttpContext?.Session;

        public string GetCurrentPrismTheme()
        {
            return Session?.GetString(PrismThemeSessionKey) ?? DefaultPrismTheme;
        }

        public string GetCurrentBootstrapTheme()
        {
            return Session?.GetString(BootstrapThemeSessionKey) ?? DefaultBootstrapTheme;
        }

        public string GetCurrentColorMode()
        {
            return Session?.GetString(ColorModeSessionKey) ?? DefaultColorMode;
        }

        public void SetPrismTheme(string themeName)
        {
            if (string.IsNullOrWhiteSpace(themeName))
                throw new ArgumentException("Theme name cannot be null or empty", nameof(themeName));

            // Validate theme exists
            var theme = ThemeManager.GetTheme(themeName);
            if (theme == null)
                throw new ArgumentException($"Theme '{themeName}' not found", nameof(themeName));

            Session?.SetString(PrismThemeSessionKey, themeName);
            _logger.LogInformation("PrismSpark theme set to {ThemeName}", themeName);
        }

        public void SetBootstrapTheme(string themeName)
        {
            if (string.IsNullOrWhiteSpace(themeName))
                throw new ArgumentException("Theme name cannot be null or empty", nameof(themeName));

            Session?.SetString(BootstrapThemeSessionKey, themeName);
            _logger.LogInformation("Bootstrap theme set to {ThemeName}", themeName);
        }

        public void SetColorMode(string colorMode)
        {
            if (string.IsNullOrWhiteSpace(colorMode))
                throw new ArgumentException("Color mode cannot be null or empty", nameof(colorMode));

            var normalizedColorMode = colorMode.ToLower();
            if (normalizedColorMode != "light" && normalizedColorMode != "dark")
                throw new ArgumentException("Color mode must be 'light' or 'dark'", nameof(colorMode));

            Session?.SetString(ColorModeSessionKey, normalizedColorMode);
            _logger.LogInformation("Color mode set to {ColorMode}", normalizedColorMode);
        }

        public void ResetToDefaults()
        {
            Session?.SetString(PrismThemeSessionKey, DefaultPrismTheme);
            Session?.SetString(BootstrapThemeSessionKey, DefaultBootstrapTheme);
            Session?.SetString(ColorModeSessionKey, DefaultColorMode);
            _logger.LogInformation("Themes reset to defaults");
        }

        public IEnumerable<string> GetAvailablePrismThemes()
        {
            return ThemeManager.GetThemeNames();
        }

        public string GenerateThemeCss(string? themeName = null)
        {
            themeName ??= GetCurrentPrismTheme();
            return ThemeManager.GenerateCss(themeName);
        }
    }
}
