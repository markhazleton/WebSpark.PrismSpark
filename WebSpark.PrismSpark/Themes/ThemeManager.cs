namespace WebSpark.PrismSpark.Themes;

/// <summary>
/// Represents a theme configuration
/// </summary>
public class Theme
{
    /// <summary>
    /// Gets or sets the name of the theme.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the theme.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the token styles for syntax highlighting elements.
    /// </summary>
    public Dictionary<string, ThemeStyle> TokenStyles { get; set; } = new();

    /// <summary>
    /// Gets or sets the background style for the theme.
    /// </summary>
    public ThemeStyle Background { get; set; } = new();

    /// <summary>
    /// Gets or sets the foreground style for the theme.
    /// </summary>
    public ThemeStyle Foreground { get; set; } = new();

    /// <summary>
    /// Gets or sets custom properties for the theme.
    /// </summary>
    public Dictionary<string, string> CustomProperties { get; set; } = new();
}

/// <summary>
/// Represents styling for a theme element
/// </summary>
public class ThemeStyle
{
    /// <summary>
    /// Gets or sets the text color.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the font weight (e.g., "bold", "normal").
    /// </summary>
    public string? FontWeight { get; set; }

    /// <summary>
    /// Gets or sets the font style (e.g., "italic", "normal").
    /// </summary>
    public string? FontStyle { get; set; }

    /// <summary>
    /// Gets or sets the text decoration (e.g., "underline", "none").
    /// </summary>
    public string? TextDecoration { get; set; }

    /// <summary>
    /// Gets or sets the opacity value.
    /// </summary>
    public string? Opacity { get; set; }

    /// <summary>
    /// Gets or sets custom CSS properties for the style.
    /// </summary>
    public Dictionary<string, string> CustomProperties { get; set; } = new();

    /// <summary>
    /// Converts the theme style to a CSS string.
    /// </summary>
    /// <returns>A CSS string representation of the style properties.</returns>
    public string ToCss()
    {
        var properties = new List<string>();

        if (!string.IsNullOrEmpty(Color))
            properties.Add($"color: {Color}");
        if (!string.IsNullOrEmpty(BackgroundColor))
            properties.Add($"background-color: {BackgroundColor}");
        if (!string.IsNullOrEmpty(FontWeight))
            properties.Add($"font-weight: {FontWeight}");
        if (!string.IsNullOrEmpty(FontStyle))
            properties.Add($"font-style: {FontStyle}");
        if (!string.IsNullOrEmpty(TextDecoration))
            properties.Add($"text-decoration: {TextDecoration}");
        if (!string.IsNullOrEmpty(Opacity))
            properties.Add($"opacity: {Opacity}");

        foreach (var prop in CustomProperties)
        {
            properties.Add($"{prop.Key}: {prop.Value}");
        }

        return string.Join("; ", properties);
    }
}

/// <summary>
/// Manages themes for syntax highlighting
/// </summary>
public static class ThemeManager
{
    private static readonly Dictionary<string, Theme> _themes = new();
    private static string _defaultTheme = "prism";

    static ThemeManager()
    {
        RegisterBuiltinThemes();
    }

    /// <summary>
    /// Register a theme
    /// </summary>
    /// <param name="theme">The theme to register.</param>
    public static void RegisterTheme(Theme theme)
    {
        _themes[theme.Name] = theme;
    }

    /// <summary>
    /// Get a theme by name
    /// </summary>
    /// <param name="name">The name of the theme to retrieve.</param>
    /// <returns>The theme if found, otherwise null.</returns>
    public static Theme? GetTheme(string name)
    {
        return _themes.TryGetValue(name, out var theme) ? theme : null;
    }

    /// <summary>
    /// Get all available theme names
    /// </summary>
    /// <returns>An enumerable collection of theme names.</returns>
    public static IEnumerable<string> GetThemeNames() => _themes.Keys;

    /// <summary>
    /// Set the default theme
    /// </summary>
    /// <param name="themeName">The name of the theme to set as default.</param>
    public static void SetDefaultTheme(string themeName)
    {
        if (_themes.ContainsKey(themeName))
        {
            _defaultTheme = themeName;
        }
    }

    /// <summary>
    /// Get the default theme
    /// </summary>
    /// <returns>The default theme.</returns>
    public static Theme GetDefaultTheme()
    {
        return _themes[_defaultTheme];
    }

    /// <summary>
    /// Generate CSS for a theme
    /// </summary>
    /// <param name="themeName">The name of the theme to generate CSS for.</param>
    /// <param name="selector">The CSS selector to use (default: ".prism").</param>
    /// <returns>The generated CSS string for the theme.</returns>
    public static string GenerateCss(string themeName, string selector = ".prism")
    {
        var theme = GetTheme(themeName);
        if (theme == null) return string.Empty;

        var css = new List<string>();

        // Base styles
        if (theme.Background.Color != null || theme.Background.BackgroundColor != null)
        {
            css.Add($"{selector} {{ {theme.Background.ToCss()} }}");
        }

        if (theme.Foreground.Color != null)
        {
            css.Add($"{selector} {{ color: {theme.Foreground.Color}; }}");
        }

        // Token styles
        foreach (var tokenStyle in theme.TokenStyles)
        {
            var tokenSelector = $"{selector} .token.{tokenStyle.Key}";
            css.Add($"{tokenSelector} {{ {tokenStyle.Value.ToCss()} }}");
        }

        return string.Join("\n", css);
    }

    private static void RegisterBuiltinThemes()
    {
        // Default Prism theme
        RegisterTheme(new Theme
        {
            Name = "prism",
            Description = "Default Prism theme",
            Background = new ThemeStyle { BackgroundColor = "#f5f2f0" },
            Foreground = new ThemeStyle { Color = "#000" },
            TokenStyles = new Dictionary<string, ThemeStyle>
            {
                ["comment"] = new() { Color = "#708090", FontStyle = "italic" },
                ["prolog"] = new() { Color = "#708090", FontStyle = "italic" },
                ["doctype"] = new() { Color = "#708090", FontStyle = "italic" },
                ["cdata"] = new() { Color = "#708090", FontStyle = "italic" },
                ["punctuation"] = new() { Color = "#999" },
                ["namespace"] = new() { Opacity = "0.7" },
                ["property"] = new() { Color = "#905" },
                ["tag"] = new() { Color = "#905" },
                ["boolean"] = new() { Color = "#905" },
                ["number"] = new() { Color = "#905" },
                ["constant"] = new() { Color = "#905" },
                ["symbol"] = new() { Color = "#905" },
                ["deleted"] = new() { Color = "#905" },
                ["selector"] = new() { Color = "#690" },
                ["attr-name"] = new() { Color = "#690" },
                ["string"] = new() { Color = "#690" },
                ["char"] = new() { Color = "#690" },
                ["builtin"] = new() { Color = "#690" },
                ["inserted"] = new() { Color = "#690" },
                ["operator"] = new() { Color = "#9a6e3a" },
                ["entity"] = new() { Color = "#9a6e3a" },
                ["url"] = new() { Color = "#9a6e3a" },
                ["atrule"] = new() { Color = "#07a" },
                ["attr-value"] = new() { Color = "#07a" },
                ["keyword"] = new() { Color = "#07a" },
                ["function"] = new() { Color = "#DD4A68" },
                ["class-name"] = new() { Color = "#DD4A68" },
                ["regex"] = new() { Color = "#e90" },
                ["important"] = new() { Color = "#e90", FontWeight = "bold" },
                ["variable"] = new() { Color = "#e90" }
            }
        });

        // Dark theme
        RegisterTheme(new Theme
        {
            Name = "dark",
            Description = "Dark theme",
            Background = new ThemeStyle { BackgroundColor = "#2d3748" },
            Foreground = new ThemeStyle { Color = "#e2e8f0" },
            TokenStyles = new Dictionary<string, ThemeStyle>
            {
                ["comment"] = new() { Color = "#68d391", FontStyle = "italic" },
                ["prolog"] = new() { Color = "#68d391", FontStyle = "italic" },
                ["doctype"] = new() { Color = "#68d391", FontStyle = "italic" },
                ["cdata"] = new() { Color = "#68d391", FontStyle = "italic" },
                ["punctuation"] = new() { Color = "#a0aec0" },
                ["property"] = new() { Color = "#f56565" },
                ["tag"] = new() { Color = "#f56565" },
                ["boolean"] = new() { Color = "#f56565" },
                ["number"] = new() { Color = "#f56565" },
                ["constant"] = new() { Color = "#f56565" },
                ["symbol"] = new() { Color = "#f56565" },
                ["selector"] = new() { Color = "#48bb78" },
                ["attr-name"] = new() { Color = "#48bb78" },
                ["string"] = new() { Color = "#48bb78" },
                ["char"] = new() { Color = "#48bb78" },
                ["builtin"] = new() { Color = "#48bb78" },
                ["operator"] = new() { Color = "#ed8936" },
                ["entity"] = new() { Color = "#ed8936" },
                ["atrule"] = new() { Color = "#4299e1" },
                ["attr-value"] = new() { Color = "#4299e1" },
                ["keyword"] = new() { Color = "#4299e1" },
                ["function"] = new() { Color = "#9f7aea" },
                ["class-name"] = new() { Color = "#9f7aea" },
                ["regex"] = new() { Color = "#ed8936" },
                ["important"] = new() { Color = "#f56565", FontWeight = "bold" },
                ["variable"] = new() { Color = "#ed8936" }
            }
        });

        // Tomorrow Night theme
        RegisterTheme(new Theme
        {
            Name = "tomorrow-night",
            Description = "Tomorrow Night theme",
            Background = new ThemeStyle { BackgroundColor = "#1d1f21" },
            Foreground = new ThemeStyle { Color = "#c5c8c6" },
            TokenStyles = new Dictionary<string, ThemeStyle>
            {
                ["comment"] = new() { Color = "#969896", FontStyle = "italic" },
                ["prolog"] = new() { Color = "#969896", FontStyle = "italic" },
                ["doctype"] = new() { Color = "#969896", FontStyle = "italic" },
                ["cdata"] = new() { Color = "#969896", FontStyle = "italic" },
                ["punctuation"] = new() { Color = "#c5c8c6" },
                ["property"] = new() { Color = "#cc6666" },
                ["tag"] = new() { Color = "#cc6666" },
                ["boolean"] = new() { Color = "#de935f" },
                ["number"] = new() { Color = "#de935f" },
                ["constant"] = new() { Color = "#de935f" },
                ["symbol"] = new() { Color = "#de935f" },
                ["selector"] = new() { Color = "#b5bd68" },
                ["attr-name"] = new() { Color = "#f0c674" },
                ["string"] = new() { Color = "#b5bd68" },
                ["char"] = new() { Color = "#b5bd68" },
                ["builtin"] = new() { Color = "#8abeb7" },
                ["operator"] = new() { Color = "#8abeb7" },
                ["entity"] = new() { Color = "#8abeb7" },
                ["atrule"] = new() { Color = "#81a2be" },
                ["attr-value"] = new() { Color = "#b5bd68" },
                ["keyword"] = new() { Color = "#b294bb" },
                ["function"] = new() { Color = "#81a2be" },
                ["class-name"] = new() { Color = "#f0c674" },
                ["regex"] = new() { Color = "#8abeb7" },
                ["important"] = new() { Color = "#cc6666", FontWeight = "bold" },
                ["variable"] = new() { Color = "#cc6666" }
            }
        });

        // Solarized Light theme
        RegisterTheme(new Theme
        {
            Name = "solarized-light",
            Description = "Solarized Light theme",
            Background = new ThemeStyle { BackgroundColor = "#fdf6e3" },
            Foreground = new ThemeStyle { Color = "#657b83" },
            TokenStyles = new Dictionary<string, ThemeStyle>
            {
                ["comment"] = new() { Color = "#93a1a1", FontStyle = "italic" },
                ["prolog"] = new() { Color = "#93a1a1", FontStyle = "italic" },
                ["doctype"] = new() { Color = "#93a1a1", FontStyle = "italic" },
                ["cdata"] = new() { Color = "#93a1a1", FontStyle = "italic" },
                ["punctuation"] = new() { Color = "#586e75" },
                ["property"] = new() { Color = "#268bd2" },
                ["tag"] = new() { Color = "#268bd2" },
                ["boolean"] = new() { Color = "#cb4b16" },
                ["number"] = new() { Color = "#cb4b16" },
                ["constant"] = new() { Color = "#cb4b16" },
                ["symbol"] = new() { Color = "#cb4b16" },
                ["selector"] = new() { Color = "#859900" },
                ["attr-name"] = new() { Color = "#b58900" },
                ["string"] = new() { Color = "#2aa198" },
                ["char"] = new() { Color = "#2aa198" },
                ["builtin"] = new() { Color = "#859900" },
                ["operator"] = new() { Color = "#859900" },
                ["entity"] = new() { Color = "#6c71c4" },
                ["atrule"] = new() { Color = "#268bd2" },
                ["attr-value"] = new() { Color = "#2aa198" },
                ["keyword"] = new() { Color = "#859900" },
                ["function"] = new() { Color = "#268bd2" },
                ["class-name"] = new() { Color = "#b58900" },
                ["regex"] = new() { Color = "#dc322f" },
                ["important"] = new() { Color = "#dc322f", FontWeight = "bold" },
                ["variable"] = new() { Color = "#dc322f" }
            }
        });
    }
}
