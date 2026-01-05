using Microsoft.AspNetCore.Mvc;
using WebSpark.PrismSpark.Themes;

namespace WebSpark.PrismSpark.Demo.Controllers
{
    public class ThemeController : Controller
    {
        private readonly ILogger<ThemeController> _logger;
        private const string PrismThemeSessionKey = "PrismTheme";
        private const string BootstrapThemeSessionKey = "BootstrapTheme";

        public ThemeController(ILogger<ThemeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get the current theme settings from session or return defaults
        /// </summary>
        /// <returns>JSON object with current theme settings</returns>
        [HttpGet]
        public IActionResult GetCurrentTheme()
        {
            var prismTheme = HttpContext.Session.GetString(PrismThemeSessionKey) ?? "yeti";
            var bootstrapTheme = HttpContext.Session.GetString(BootstrapThemeSessionKey) ?? "yeti";
            var colorMode = HttpContext.Session.GetString("ColorMode") ?? "light";

            return Json(new
            {
                prismTheme = prismTheme,
                bootstrapTheme = bootstrapTheme,
                colorMode = colorMode
            });
        }

        /// <summary>
        /// Set the PrismSpark syntax highlighting theme
        /// </summary>
        /// <param name="themeName">Name of the theme to set</param>
        /// <returns>JSON response indicating success or failure</returns>
        [HttpPost]
        public IActionResult SetPrismTheme([FromBody] SetThemeRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.ThemeName))
                {
                    return BadRequest(new { success = false, message = "Theme name is required" });
                }

                // Validate that the theme exists
                var theme = ThemeManager.GetTheme(request.ThemeName);
                if (theme == null)
                {
                    return BadRequest(new { success = false, message = $"Theme '{request.ThemeName}' not found" });
                }

                // Store in session
                HttpContext.Session.SetString(PrismThemeSessionKey, request.ThemeName);

                _logger.LogInformation("PrismSpark theme changed to {ThemeName}", request.ThemeName);

                return Json(new { success = true, theme = request.ThemeName });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting PrismSpark theme to {ThemeName}", request.ThemeName);
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        /// <summary>
        /// Set the Bootstrap theme
        /// </summary>
        /// <param name="request">Theme change request</param>
        /// <returns>JSON response indicating success or failure</returns>
        [HttpPost]
        public IActionResult SetBootstrapTheme([FromBody] SetThemeRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.ThemeName))
                {
                    return BadRequest(new { success = false, message = "Theme name is required" });
                }

                // Store in session
                HttpContext.Session.SetString(BootstrapThemeSessionKey, request.ThemeName);

                _logger.LogInformation("Bootstrap theme changed to {ThemeName}", request.ThemeName);

                return Json(new { success = true, theme = request.ThemeName });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting Bootstrap theme to {ThemeName}", request.ThemeName);
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        /// <summary>
        /// Set the color mode (light/dark)
        /// </summary>
        /// <param name="request">Color mode change request</param>
        /// <returns>JSON response indicating success or failure</returns>
        [HttpPost]
        public IActionResult SetColorMode([FromBody] SetColorModeRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.ColorMode))
                {
                    return BadRequest(new { success = false, message = "Color mode is required" });
                }

                if (request.ColorMode.ToLower() != "light" && request.ColorMode.ToLower() != "dark")
                {
                    return BadRequest(new { success = false, message = "Color mode must be 'light' or 'dark'" });
                }

                // Store in session
                HttpContext.Session.SetString("ColorMode", request.ColorMode.ToLower());

                _logger.LogInformation("Color mode changed to {ColorMode}", request.ColorMode);

                return Json(new { success = true, colorMode = request.ColorMode.ToLower() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting color mode to {ColorMode}", request.ColorMode);
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get all available PrismSpark themes
        /// </summary>
        /// <returns>List of available theme names</returns>
        [HttpGet]
        public IActionResult GetAvailableThemes()
        {
            try
            {
                var themes = ThemeManager.GetThemeNames().ToList();
                return Json(themes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving available themes");
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        /// <summary>
        /// Generate CSS for a specific theme
        /// </summary>
        /// <param name="themeName">Name of the theme</param>
        /// <returns>CSS content</returns>
        [HttpGet]
        public IActionResult GetThemeCss(string themeName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(themeName))
                {
                    themeName = HttpContext.Session.GetString(PrismThemeSessionKey) ?? "yeti";
                }

                var css = ThemeManager.GenerateCss(themeName);
                return Content(css, "text/css");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating CSS for theme {ThemeName}", themeName);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Reset themes to default values
        /// </summary>
        /// <returns>JSON response with default theme settings</returns>
        [HttpPost]
        public IActionResult ResetToDefaults()
        {
            try
            {
                HttpContext.Session.SetString(PrismThemeSessionKey, "yeti");
                HttpContext.Session.SetString(BootstrapThemeSessionKey, "yeti");
                HttpContext.Session.SetString("ColorMode", "light");

                _logger.LogInformation("Themes reset to defaults");

                return Json(new
                {
                    success = true,
                    prismTheme = "yeti",
                    bootstrapTheme = "yeti",
                    colorMode = "light"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting themes to defaults");
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
    }

    /// <summary>
    /// Request model for setting theme
    /// </summary>
    public class SetThemeRequest
    {
        public string ThemeName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Request model for setting color mode
    /// </summary>
    public class SetColorModeRequest
    {
        public string ColorMode { get; set; } = string.Empty;
    }
}
