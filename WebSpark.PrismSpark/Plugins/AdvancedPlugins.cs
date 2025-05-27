namespace WebSpark.PrismSpark.Plugins;

/// <summary>
/// Copy to clipboard plugin
/// </summary>
public class CopyToClipboardPlugin : PluginBase
{
    /// <summary>
    /// Gets the unique identifier for this plugin.
    /// </summary>
    public override string Id => "copy-to-clipboard";

    /// <summary>
    /// Gets the display name for this plugin.
    /// </summary>
    public override string Name => "Copy to Clipboard";

    /// <summary>
    /// Processes the HTML output and adds copy-to-clipboard functionality.
    /// </summary>
    /// <param name="html">The HTML content to process.</param>
    /// <param name="language">The programming language being highlighted.</param>
    /// <param name="context">The highlighting context.</param>
    /// <returns>The processed HTML with copy functionality added.</returns>
    public override string ProcessOutput(string html, string language, HighlightContext context)
    {
        if (string.IsNullOrEmpty(html)) return html;

        context.Classes.Add("copy-to-clipboard");
        context.Attributes["data-copy-to-clipboard"] = "true";
        context.Metadata["copyButton"] = new
        {
            Text = "Copy",
            SuccessText = "Copied!",
            ErrorText = "Copy failed"
        };

        return html;
    }
}

/// <summary>
/// Autolinker plugin - automatically creates links from URLs
/// </summary>
public class AutolinkerPlugin : PluginBase
{
    /// <summary>
    /// Gets the unique identifier for this plugin.
    /// </summary>
    public override string Id => "autolinker";

    /// <summary>
    /// Gets the display name for this plugin.
    /// </summary>
    public override string Name => "Autolinker";

    private static readonly System.Text.RegularExpressions.Regex UrlRegex =
        new(@"https?://[^\s<>""']+", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    /// <summary>
    /// Processes the HTML output and automatically converts URLs to clickable links.
    /// </summary>
    /// <param name="html">The HTML content to process.</param>
    /// <param name="language">The programming language being highlighted.</param>
    /// <param name="context">The highlighting context.</param>
    /// <returns>The processed HTML with URLs converted to links.</returns>
    public override string ProcessOutput(string html, string language, HighlightContext context)
    {
        if (string.IsNullOrEmpty(html)) return html;

        // Only process string and comment tokens
        return UrlRegex.Replace(html, match =>
        {
            var url = match.Value;
            return $"<a href=\"{url}\" class=\"autolink\" target=\"_blank\" rel=\"noopener noreferrer\">{url}</a>";
        });
    }
}

/// <summary>
/// File highlight plugin - highlights specific files based on patterns
/// </summary>
public class FileHighlightPlugin : PluginBase
{
    /// <summary>
    /// Gets the unique identifier for this plugin.
    /// </summary>
    public override string Id => "file-highlight";

    /// <summary>
    /// Gets the display name for this plugin.
    /// </summary>
    public override string Name => "File Highlight";

    /// <summary>
    /// Processes tokens and attempts to determine the language from file extension if available.
    /// </summary>
    /// <param name="tokens">The tokens to process.</param>
    /// <param name="language">The current programming language.</param>
    /// <param name="context">The highlighting context.</param>
    public override void ProcessTokens(Token[] tokens, string language, HighlightContext context)
    {
        // Check if context has file information
        if (context.Metadata.TryGetValue("filename", out var filenameObj) &&
            filenameObj is string filename)
        {
            // Try to determine language from extension
            var extension = System.IO.Path.GetExtension(filename).ToLower();
            var languageFromExtension = extension switch
            {
                ".cs" => "csharp",
                ".js" => "javascript",
                ".ts" => "typescript",
                ".py" => "python",
                ".java" => "java",
                ".cpp" or ".cc" or ".cxx" => "cpp",
                ".c" => "c",
                ".php" => "php",
                ".rb" => "ruby",
                ".go" => "go",
                ".rs" => "rust",
                ".kt" => "kotlin",
                ".swift" => "swift",
                ".html" or ".htm" => "html",
                ".css" => "css",
                ".scss" => "scss",
                ".json" => "json",
                ".xml" => "xml",
                ".yaml" or ".yml" => "yaml",
                ".sql" => "sql",
                _ => language
            };

            context.Language = languageFromExtension;
        }
    }
}

/// <summary>
/// Show language plugin - displays the language name
/// </summary>
public class ShowLanguagePlugin : PluginBase
{
    /// <summary>
    /// Gets the unique identifier for this plugin.
    /// </summary>
    public override string Id => "show-language";

    /// <summary>
    /// Gets the display name for this plugin.
    /// </summary>
    public override string Name => "Show Language";

    /// <summary>
    /// Processes the HTML output and adds language display information.
    /// </summary>
    /// <param name="html">The HTML content to process.</param>
    /// <param name="language">The programming language being highlighted.</param>
    /// <param name="context">The highlighting context.</param>
    /// <returns>The processed HTML with language display information added.</returns>
    public override string ProcessOutput(string html, string language, HighlightContext context)
    {
        if (string.IsNullOrEmpty(language)) return html;

        context.Classes.Add("show-language");
        context.Attributes["data-language"] = language;
        context.Attributes["data-language-position"] = "top-right";

        var displayName = GetLanguageDisplayName(language);
        context.Metadata["languageDisplay"] = $"Language: {displayName}";

        return html;
    }

    private static string GetLanguageDisplayName(string language)
    {
        return language switch
        {
            "csharp" => "C#",
            "javascript" => "JavaScript",
            "typescript" => "TypeScript",
            "cpp" => "C++",
            "objectivec" => "Objective-C",
            _ => char.ToUpper(language[0]) + language[1..].ToLower()
        };
    }
}

/// <summary>
/// Toolbar plugin - provides a toolbar with various actions
/// </summary>
public class ToolbarPlugin : PluginBase
{
    /// <summary>
    /// Gets the unique identifier for this plugin.
    /// </summary>
    public override string Id => "toolbar";

    /// <summary>
    /// Gets the display name for this plugin.
    /// </summary>
    public override string Name => "Toolbar";

    /// <summary>
    /// Processes the HTML output and adds a toolbar with various action items.
    /// </summary>
    /// <param name="html">The HTML content to process.</param>
    /// <param name="language">The programming language being highlighted.</param>
    /// <param name="context">The highlighting context.</param>
    /// <returns>The processed HTML with toolbar functionality added.</returns>
    public override string ProcessOutput(string html, string language, HighlightContext context)
    {
        context.Classes.Add("toolbar");

        var toolbarItems = new List<object>();

        // Add language label
        if (!string.IsNullOrEmpty(language))
        {
            toolbarItems.Add(new
            {
                Type = "label",
                Text = GetLanguageDisplayName(language),
                Class = "toolbar-item toolbar-label"
            });
        }

        // Add copy button
        toolbarItems.Add(new
        {
            Type = "button",
            Text = "Copy",
            Class = "toolbar-item toolbar-button copy-button",
            Action = "copy"
        });

        context.Metadata["toolbar"] = toolbarItems;
        return html;
    }

    private static string GetLanguageDisplayName(string language)
    {
        return language switch
        {
            "csharp" => "C#",
            "javascript" => "JavaScript",
            "typescript" => "TypeScript",
            "cpp" => "C++",
            "objectivec" => "Objective-C",
            _ => char.ToUpper(language[0]) + language[1..].ToLower()
        };
    }
}

/// <summary>
/// Command line plugin - enhances command line highlighting
/// </summary>
public class CommandLinePlugin : PluginBase
{
    /// <summary>
    /// Gets the unique identifier for this plugin.
    /// </summary>
    public override string Id => "command-line";

    /// <summary>
    /// Gets the display name for this plugin.
    /// </summary>
    public override string Name => "Command Line";

    /// <summary>
    /// Processes tokens and adds command line specific styling for supported shell languages.
    /// </summary>
    /// <param name="tokens">The tokens to process.</param>
    /// <param name="language">The programming language being highlighted.</param>
    /// <param name="context">The highlighting context.</param>
    public override void ProcessTokens(Token[] tokens, string language, HighlightContext context)
    {
        // Detect command line languages
        if (language is "bash" or "shell" or "sh" or "powershell" or "cmd")
        {
            context.Classes.Add("command-line");
        }
    }

    /// <summary>
    /// Processes the HTML output and adds command line specific metadata and styling.
    /// </summary>
    /// <param name="html">The HTML content to process.</param>
    /// <param name="language">The programming language being highlighted.</param>
    /// <param name="context">The highlighting context.</param>
    /// <returns>The processed HTML with command line enhancements.</returns>
    public override string ProcessOutput(string html, string language, HighlightContext context)
    {
        if (!context.Classes.Contains("command-line")) return html;

        context.Metadata["commandLine"] = new
        {
            Prompt = "$",
            User = "user",
            Host = "localhost",
            OutputLines = new List<int>()
        };

        return html;
    }
}
