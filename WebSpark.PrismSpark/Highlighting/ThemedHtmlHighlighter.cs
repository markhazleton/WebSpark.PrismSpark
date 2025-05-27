using WebSpark.PrismSpark.Themes;
using WebSpark.PrismSpark.Utilities;

namespace WebSpark.PrismSpark.Highlighting;

/// <summary>
/// Provides static methods for generating CSS stylesheets for PrismSpark syntax highlighting themes.
/// This class handles the creation of complete CSS that includes base styles, theme-specific token styles,
/// plugin styles, and utility styles for enhanced code presentation.
/// </summary>
public static class CssGenerator
{
    /// <summary>
    /// Generates a complete CSS stylesheet for the specified theme, including all necessary styles
    /// for syntax highlighting, plugins, and visual enhancements.
    /// </summary>
    /// <param name="themeName">The name of the theme to generate CSS for. Must be a valid theme registered with the ThemeManager.</param>
    /// <param name="options">Optional CSS generation options to customize the output. If null, default options will be used.</param>
    /// <returns>A complete CSS stylesheet as a string, or an empty string if the theme is not found.</returns>
    /// <exception cref="ArgumentNullException">Thrown when themeName is null or empty.</exception>
    public static string GenerateThemeCss(string themeName, CssOptions? options = null)
    {
        options ??= new CssOptions();
        var theme = ThemeManager.GetTheme(themeName);
        if (theme == null) return string.Empty;

        var css = new List<string>();

        // Base container styles
        css.Add(GenerateBaseStyles(options));

        // Theme-specific styles
        css.Add(GenerateThemeStyles(theme, options));

        // Plugin styles
        css.Add(GeneratePluginStyles(options));

        // Utility styles
        css.Add(GenerateUtilityStyles(options));

        return string.Join("\n\n", css.Where(c => !string.IsNullOrEmpty(c)));
    }

    /// <summary>
    /// Generate base styles for code containers
    /// </summary>
    private static string GenerateBaseStyles(CssOptions options)
    {
        var selector = options.BaseSelector;
        var css = new List<string>();

        css.Add($@"{selector} {{
    color: #000;
    background: none;
    font-family: {options.FontFamily};
    font-size: {options.FontSize};
    text-align: left;
    white-space: pre;
    word-spacing: normal;
    word-break: normal;
    word-wrap: normal;
    line-height: {options.LineHeight};
    tab-size: {options.TabSize};
    hyphens: none;
}}");

        css.Add($@"{selector}[class*=""language-""],
{selector}[class*=""language-""] {{
    text-align: left;
    white-space: pre;
    word-spacing: normal;
    word-break: normal;
    word-wrap: normal;
    line-height: {options.LineHeight};
    tab-size: {options.TabSize};
    hyphens: none;
}}");

        css.Add($@"pre[class*=""language-""] {{
    position: relative;
    margin: {options.BlockMargin};
    padding: {options.BlockPadding};
    overflow: auto;
    border-radius: {options.BorderRadius};
}}");

        css.Add($@":not(pre) > code[class*=""language-""],
pre[class*=""language-""] {{
    background: #f5f2f0;
}}");

        css.Add($@":not(pre) > code[class*=""language-""] {{
    padding: {options.InlinePadding};
    border-radius: {options.BorderRadius};
    white-space: normal;
}}");

        return string.Join("\n\n", css);
    }

    /// <summary>
    /// Generate theme-specific token styles
    /// </summary>
    private static string GenerateThemeStyles(Theme theme, CssOptions options)
    {
        var css = new List<string>();
        var selector = options.BaseSelector;

        // Background and foreground
        if (!string.IsNullOrEmpty(theme.Background.BackgroundColor))
        {
            css.Add($@"pre[class*=""language-""] {{
    background: {theme.Background.BackgroundColor};
}}");
        }

        if (!string.IsNullOrEmpty(theme.Foreground.Color))
        {
            css.Add($@"{selector} {{
    color: {theme.Foreground.Color};
}}");
        }

        // Token styles
        foreach (var tokenStyle in theme.TokenStyles)
        {
            var tokenSelector = $"{selector} .token.{tokenStyle.Key}";
            var style = tokenStyle.Value.ToCss();
            if (!string.IsNullOrEmpty(style))
            {
                css.Add($@"{tokenSelector} {{
    {style};
}}");
            }
        }

        return string.Join("\n\n", css);
    }

    /// <summary>
    /// Generate plugin-specific styles
    /// </summary>
    private static string GeneratePluginStyles(CssOptions options)
    {
        var css = new List<string>();
        var selector = options.BaseSelector;

        // Line numbers plugin
        css.Add($@"pre.line-numbers {{
    position: relative;
    padding-left: 3.8em;
    counter-reset: linenumber;
}}

pre.line-numbers > code {{
    position: relative;
    white-space: inherit;
}}

.line-numbers .line-numbers-rows {{
    position: absolute;
    pointer-events: none;
    top: 0;
    font-size: 100%;
    left: -3.8em;
    width: 3em;
    letter-spacing: -1px;
    border-right: 1px solid #999;
    user-select: none;
}}

.line-numbers-rows > span {{
    pointer-events: none;
    display: block;
    counter-increment: linenumber;
}}

.line-numbers-rows > span:before {{
    content: counter(linenumber);
    color: #999;
    display: block;
    padding-right: 0.8em;
    text-align: right;
}}");

        // Line highlight plugin
        css.Add($@"pre[data-line] {{
    position: relative;
    padding: 1em 0 1em 3em;
}}

.line-highlight {{
    position: absolute;
    left: 0;
    right: 0;
    padding: inherit 0;
    margin-top: 1em;
    background: hsla(24, 20%, 50%, .08);
    background: linear-gradient(to right, hsla(24, 20%, 50%, .1) 70%, hsla(24, 20%, 50%, 0));
    pointer-events: none;
    line-height: inherit;
    white-space: pre;
}}");

        // Copy to clipboard plugin
        css.Add($@".copy-to-clipboard {{
    position: relative;
}}

.copy-to-clipboard-button {{
    position: absolute;
    top: 5px;
    right: 5px;
    padding: 4px 8px;
    border: none;
    border-radius: 3px;
    font-size: 11px;
    font-weight: bold;
    background: rgba(224, 224, 224, 0.2);
    color: #c5c8c6;
    cursor: pointer;
    opacity: 0;
    transition: opacity 0.3s;
}}

.copy-to-clipboard:hover .copy-to-clipboard-button {{
    opacity: 1;
}}

.copy-to-clipboard-button:hover {{
    background: rgba(224, 224, 224, 0.3);
}}");

        // Toolbar plugin
        css.Add($@".toolbar {{
    position: relative;
}}

.toolbar:before {{
    content: '''';
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    height: 30px;
    background: rgba(224, 224, 224, 0.2);
    border-bottom: 1px solid rgba(224, 224, 224, 0.5);
}}

.toolbar-item {{
    position: absolute;
    top: 0;
    padding: 6px 10px;
    font-size: 11px;
    line-height: 18px;
}}

.toolbar-label {{
    left: 10px;
    color: #999;
    font-weight: bold;
}}

.toolbar-button {{
    right: 10px;
    color: #c5c8c6;
    background: transparent;
    border: none;
    cursor: pointer;
    text-decoration: underline;
}}

.toolbar-button:hover {{
    color: #fff;
}}");

        // Show language plugin
        css.Add($@".show-language:before {{
    content: attr(data-language);
    position: absolute;
    top: 0.3em;
    right: 0.5em;
    padding: 0 0.5em;
    background: rgba(224, 224, 224, 0.2);
    color: #c5c8c6;
    font-size: 10px;
    font-weight: bold;
    text-transform: uppercase;
    border-radius: 0 0 0 0.3em;
}}");

        return string.Join("\n\n", css);
    }

    /// <summary>
    /// Generate utility styles
    /// </summary>
    private static string GenerateUtilityStyles(CssOptions options)
    {
        var css = new List<string>();

        // Selection styles
        css.Add($@"{options.BaseSelector}::-moz-selection,
{options.BaseSelector} ::-moz-selection {{
    background: #b3d4fc;
}}

{options.BaseSelector}::selection,
{options.BaseSelector} ::selection {{
    background: #b3d4fc;
}}");

        // Scrollbar styles
        css.Add($@"pre[class*=""language-""]::-webkit-scrollbar {{
    width: 12px;
}}

pre[class*=""language-""]::-webkit-scrollbar-track {{
    background: rgba(255, 255, 255, 0.1);
}}

pre[class*=""language-""]::-webkit-scrollbar-thumb {{
    background: rgba(255, 255, 255, 0.3);
    border-radius: 6px;
}}

pre[class*=""language-""]::-webkit-scrollbar-thumb:hover {{
    background: rgba(255, 255, 255, 0.5);
}}");

        return string.Join("\n\n", css);
    }
}

/// <summary>
/// Configuration options for controlling CSS generation and styling behavior when creating
/// stylesheets for syntax highlighting themes. These options allow customization of selectors,
/// typography, spacing, and inclusion of various style components.
/// </summary>
public class CssOptions
{
    /// <summary>
    /// Gets or sets the base CSS selector used for targeting code elements in the generated stylesheet.
    /// This selector determines which HTML elements will receive the syntax highlighting styles.
    /// </summary>
    /// <value>
    /// The CSS selector string. Default is "code[class*=\"language-\"], pre[class*=\"language-\"]".
    /// </value>
    public string BaseSelector { get; set; } = "code[class*=\"language-\"], pre[class*=\"language-\"]";

    /// <summary>
    /// Gets or sets the font family stack to use for displaying code text.
    /// Should include monospace fonts for proper code alignment and readability.
    /// </summary>
    /// <value>
    /// A CSS font-family value. Default is "Consolas, Monaco, 'Andale Mono', 'Ubuntu Mono', monospace".
    /// </value>
    public string FontFamily { get; set; } = "Consolas, Monaco, 'Andale Mono', 'Ubuntu Mono', monospace";

    /// <summary>
    /// Gets or sets the font size for code text using CSS length units (em, px, rem, etc.).
    /// </summary>
    /// <value>
    /// A CSS font-size value. Default is "1em".
    /// </value>
    public string FontSize { get; set; } = "1em";

    /// <summary>
    /// Gets or sets the line height for code text to control vertical spacing between lines.
    /// This affects readability and visual density of the code display.
    /// </summary>
    /// <value>
    /// A CSS line-height value (unitless number or length). Default is "1.5".
    /// </value>
    public string LineHeight { get; set; } = "1.5";

    /// <summary>
    /// Gets or sets the tab size for controlling how tab characters are displayed in the code.
    /// This affects indentation rendering and code alignment.
    /// </summary>
    /// <value>
    /// A CSS tab-size value (number or length). Default is "4".
    /// </value>
    public string TabSize { get; set; } = "4";

    /// <summary>
    /// Gets or sets the margin applied to block-level code containers (pre elements).
    /// Controls spacing around code blocks in the document flow.
    /// </summary>
    /// <value>
    /// A CSS margin value. Default is "0.5em 0".
    /// </value>
    public string BlockMargin { get; set; } = "0.5em 0";

    /// <summary>
    /// Gets or sets the padding applied inside block-level code containers.
    /// Controls internal spacing within code blocks for better visual presentation.
    /// </summary>
    /// <value>
    /// A CSS padding value. Default is "1em".
    /// </value>
    public string BlockPadding { get; set; } = "1em";

    /// <summary>
    /// Gets or sets the padding applied to inline code elements.
    /// Controls spacing around small code snippets within text.
    /// </summary>
    /// <value>
    /// A CSS padding value. Default is ".1em".
    /// </value>
    public string InlinePadding { get; set; } = ".1em";

    /// <summary>
    /// Gets or sets the border radius for rounded corners on code containers.
    /// Provides visual polish and modern appearance to code blocks.
    /// </summary>
    /// <value>
    /// A CSS border-radius value. Default is "0.3em".
    /// </value>
    public string BorderRadius { get; set; } = "0.3em";

    /// <summary>
    /// Gets or sets a value indicating whether to include CSS styles for syntax highlighting plugins
    /// such as line numbers, line highlighting, copy-to-clipboard, and toolbar functionality.
    /// </summary>
    /// <value>
    /// <c>true</c> to include plugin styles; otherwise, <c>false</c>. Default is <c>true</c>.
    /// </value>
    public bool IncludePluginStyles { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to include utility CSS styles for enhanced
    /// user experience features like text selection styling and scrollbar customization.
    /// </summary>
    /// <value>
    /// <c>true</c> to include utility styles; otherwise, <c>false</c>. Default is <c>true</c>.
    /// </value>
    public bool IncludeUtilityStyles { get; set; } = true;
}

/// <summary>
/// A theme-aware HTML syntax highlighter that extends the enhanced highlighter with CSS generation capabilities.
/// This class combines syntax highlighting functionality with the ability to generate corresponding CSS styles
/// for complete, self-contained code presentation solutions.
/// </summary>
public class ThemedHtmlHighlighter : EnhancedHtmlHighlighter
{
    /// <summary>
    /// Gets or sets the name of the theme to use for syntax highlighting and CSS generation.
    /// The theme determines the color scheme and styling for different code tokens.
    /// </summary>
    /// <value>
    /// The theme name string. Default is "prism". Must correspond to a theme registered with the ThemeManager.
    /// </value>
    public string ThemeName { get; set; } = "prism";

    /// <summary>
    /// Gets or sets the CSS generation options that control how stylesheets are created for this highlighter.
    /// These options affect typography, spacing, selectors, and feature inclusion in generated CSS.
    /// </summary>
    /// <value>
    /// A <see cref="CssOptions"/> instance containing CSS generation configuration. Never null.
    /// </value>
    public CssOptions CssOptions { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ThemedHtmlHighlighter"/> class with default theme and options.
    /// Uses the "prism" theme and default CSS generation options.
    /// </summary>
    public ThemedHtmlHighlighter() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThemedHtmlHighlighter"/> class with the specified theme.
    /// Uses default CSS generation options with the provided theme name.
    /// </summary>
    /// <param name="themeName">The name of the theme to use for highlighting and CSS generation.</param>
    /// <exception cref="ArgumentNullException">Thrown when themeName is null or empty.</exception>
    public ThemedHtmlHighlighter(string themeName) : base()
    {
        ThemeName = themeName;
    }

    /// <summary>
    /// Generates a complete CSS stylesheet for the current theme using the configured CSS options.
    /// The generated CSS includes base styles, theme-specific token colors, plugin styles, and utility styles.
    /// </summary>
    /// <returns>
    /// A complete CSS stylesheet as a string that can be embedded in HTML or saved to a .css file.
    /// Returns an empty string if the current theme is not found.
    /// </returns>
    public string GenerateCss()
    {
        return CssGenerator.GenerateThemeCss(ThemeName, CssOptions);
    }

    /// <summary>
    /// Highlights the specified source code using the grammar for the given language.
    /// This is a convenience method that automatically retrieves the appropriate grammar
    /// based on the language name and applies the current theme's highlighting.
    /// </summary>
    /// <param name="code">The source code text to highlight. Cannot be null.</param>
    /// <param name="language">The programming language identifier (e.g., "csharp", "javascript", "python").
    /// Must correspond to a language with a registered grammar.</param>
    /// <param name="options">Optional highlighting options to customize the output.
    /// If null, default highlighting options will be used.</param>
    /// <returns>
    /// A string containing the highlighted HTML markup with appropriate CSS classes for the current theme.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when code or language is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when no grammar is found for the specified language.</exception>
    public string Highlight(string code, string language, HighlightOptions? options = null)
    {
        var grammar = LanguageGrammars.GetGrammar(language);
        return base.Highlight(code, grammar, language, options ?? new HighlightOptions());
    }

    /// <summary>
    /// Highlights the specified source code and returns it with embedded CSS styles in a complete HTML fragment.
    /// This method combines syntax highlighting with CSS generation to create a self-contained HTML snippet
    /// that includes all necessary styling within a &lt;style&gt; tag.
    /// </summary>
    /// <param name="code">The source code text to highlight. Cannot be null.</param>
    /// <param name="language">The programming language identifier for grammar selection and CSS class generation.</param>
    /// <param name="options">Optional highlighting options to customize the output behavior and styling.</param>
    /// <returns>
    /// A complete HTML fragment containing both the CSS styles in a &lt;style&gt; tag and the highlighted code markup.
    /// This can be directly embedded into an HTML document without external stylesheets.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when code or language is null or empty.</exception>
    public string HighlightWithEmbeddedCss(string code, string language, HighlightOptions? options = null)
    {
        var highlightedCode = Highlight(code, language, options);
        var css = GenerateCss();

        return $@"<style>
{css}
</style>
{highlightedCode}";
    }

    /// <summary>
    /// Generates a complete, standalone HTML page with syntax-highlighted code, embedded CSS, and professional styling.
    /// This method creates a full HTML document that can be saved as an .html file or displayed in a web browser.
    /// The page includes responsive design, modern typography, and a clean layout suitable for code presentation.
    /// </summary>
    /// <param name="code">The source code text to highlight and display in the page. Cannot be null.</param>
    /// <param name="language">The programming language identifier for syntax highlighting and grammar selection.</param>
    /// <param name="title">The page title to display in the browser tab and page header.
    /// If not provided, defaults to "Code Highlight". HTML characters will be automatically escaped.</param>
    /// <param name="options">Optional highlighting options to customize syntax highlighting behavior and output styling.</param>
    /// <returns>
    /// A complete HTML5 document as a string, including DOCTYPE declaration, head section with embedded CSS,
    /// and a styled body containing the highlighted code within a responsive container layout.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when code or language is null or empty.</exception>
    /// <example>
    /// <code>
    /// var highlighter = new ThemedHtmlHighlighter("tomorrow-night");
    /// var html = highlighter.GenerateHtmlPage("Console.WriteLine(\"Hello World!\");", "csharp", "C# Example");
    /// File.WriteAllText("example.html", html);
    /// </code>
    /// </example>
    public string GenerateHtmlPage(string code, string language, string title = "Code Highlight", HighlightOptions? options = null)
    {
        var highlightedCode = Highlight(code, language, options);
        var css = GenerateCss();
        var escapedTitle = StringUtils.EscapeHtml(title);

        return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>{escapedTitle}</title>
    <style>
        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif;
            margin: 0;
            padding: 20px;
            background-color: #f5f5f5;
        }}
        .container {{
            max-width: 1200px;
            margin: 0 auto;
            background: white;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            overflow: hidden;
        }}
        .header {{
            padding: 20px;
            background: #f8f9fa;
            border-bottom: 1px solid #e9ecef;
        }}
        .content {{
            padding: 0;
        }}
        {css}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>{escapedTitle}</h1>
        </div>
        <div class=""content"">
            {highlightedCode}
        </div>
    </div>
</body>
</html>";
    }
}
