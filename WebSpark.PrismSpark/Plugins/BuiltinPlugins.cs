using System.Text.RegularExpressions;

namespace WebSpark.PrismSpark.Plugins;

/// <summary>
/// Custom Class plugin - allows customizing CSS class names
/// Equivalent to PrismJS custom-class plugin
/// </summary>
public class CustomClassPlugin : PluginBase
{
    /// <summary>
    /// Gets the unique identifier for this plugin.
    /// </summary>
    public override string Id => "custom-class";

    /// <summary>
    /// Gets the display name for this plugin.
    /// </summary>
    public override string Name => "Custom Class";

    private string? _prefix;
    private Dictionary<string, string> _mappings = new();
    private Func<string, string, string>? _mapper;

    /// <summary>
    /// Set a prefix for all CSS classes
    /// </summary>
    /// <param name="prefix">Prefix to add</param>
    public void SetPrefix(string prefix)
    {
        _prefix = prefix;
    }

    /// <summary>
    /// Set class mappings
    /// </summary>
    /// <param name="mappings">Dictionary of original class to new class</param>
    public void SetMappings(Dictionary<string, string> mappings)
    {
        _mappings = mappings ?? new Dictionary<string, string>();
    }

    /// <summary>
    /// Set a custom mapping function
    /// </summary>
    /// <param name="mapper">Function that takes (className, language) and returns mapped class</param>
    public void SetMapper(Func<string, string, string> mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Processes the HTML output and applies custom class mappings.
    /// </summary>
    /// <param name="html">The HTML content to process.</param>
    /// <param name="language">The programming language being highlighted.</param>
    /// <param name="context">The highlighting context.</param>
    /// <returns>The processed HTML with custom class mappings applied.</returns>
    public override string ProcessOutput(string html, string language, HighlightContext context)
    {
        if (string.IsNullOrEmpty(html)) return html;

        // Apply custom class mappings
        var classPattern = @"class=""([^""]*token[^""]*)""";
        return Regex.Replace(html, classPattern, match =>
        {
            var classes = match.Groups[1].Value;
            var modifiedClasses = ProcessClasses(classes, language);
            return $"class=\"{modifiedClasses}\"";
        });
    }

    private string ProcessClasses(string classes, string language)
    {
        var classList = classes.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var result = new List<string>();

        foreach (var cls in classList)
        {
            var processedClass = cls;

            // Apply custom mapper first
            if (_mapper != null)
            {
                processedClass = _mapper(processedClass, language);
            }
            // Apply dictionary mappings
            else if (_mappings.TryGetValue(cls, out var mappedClass))
            {
                processedClass = mappedClass;
            }

            // Apply prefix
            if (!string.IsNullOrEmpty(_prefix))
            {
                processedClass = _prefix + processedClass;
            }

            result.Add(processedClass);
        }

        return string.Join(" ", result);
    }
}

/// <summary>
/// Line Numbers plugin - adds line numbers to code blocks
/// Equivalent to PrismJS line-numbers plugin
/// </summary>
public class LineNumbersPlugin : PluginBase
{
    /// <summary>
    /// Gets the unique identifier for this plugin.
    /// </summary>
    public override string Id => "line-numbers";

    /// <summary>
    /// Gets the display name for this plugin.
    /// </summary>
    public override string Name => "Line Numbers";

    /// <summary>
    /// Processes the HTML output and adds line numbers if enabled.
    /// </summary>
    /// <param name="html">The HTML content to process.</param>
    /// <param name="language">The programming language being highlighted.</param>
    /// <param name="context">The highlighting context.</param>
    /// <returns>The processed HTML with line numbers added.</returns>
    public override string ProcessOutput(string html, string language, HighlightContext context)
    {
        if (!context.ShowLineNumbers) return html;

        var lines = html.Split(new[] { '\n', '\r' }, StringSplitOptions.None);
        var numberedLines = new List<string>();

        for (int i = 0; i < lines.Length; i++)
        {
            var lineNumber = i + 1;
            var isHighlighted = context.HighlightedLines.Contains(lineNumber);
            var lineClass = isHighlighted ? "line-numbers-row highlighted" : "line-numbers-row";

            numberedLines.Add($"<span class=\"{lineClass}\" data-line=\"{lineNumber}\">{lines[i]}</span>");
        }

        return string.Join("\n", numberedLines);
    }
}

/// <summary>
/// Line Highlight plugin - highlights specific lines
/// Equivalent to PrismJS line-highlight plugin
/// </summary>
public class LineHighlightPlugin : PluginBase
{
    /// <summary>
    /// Gets the unique identifier for this plugin.
    /// </summary>
    public override string Id => "line-highlight";

    /// <summary>
    /// Gets the display name for this plugin.
    /// </summary>
    public override string Name => "Line Highlight";

    /// <summary>
    /// Processes the HTML output and adds highlighting to specified lines.
    /// </summary>
    /// <param name="html">The HTML content to process.</param>
    /// <param name="language">The programming language being highlighted.</param>
    /// <param name="context">The highlighting context.</param>
    /// <returns>The processed HTML with line highlighting applied.</returns>
    public override string ProcessOutput(string html, string language, HighlightContext context)
    {
        if (!context.HighlightedLines.Any()) return html;

        var lines = html.Split(new[] { '\n', '\r' }, StringSplitOptions.None);
        var result = new List<string>();

        for (int i = 0; i < lines.Length; i++)
        {
            var lineNumber = i + 1;
            var line = lines[i];

            if (context.HighlightedLines.Contains(lineNumber))
            {
                line = $"<span class=\"line-highlight\" data-line=\"{lineNumber}\">{line}</span>";
            }

            result.Add(line);
        }

        return string.Join("\n", result);
    }
}

/// <summary>
/// Normalize Whitespace plugin - normalizes whitespace in code blocks
/// Equivalent to PrismJS normalize-whitespace plugin
/// </summary>
public class NormalizeWhitespacePlugin : PluginBase
{
    /// <summary>
    /// Gets the unique identifier for this plugin.
    /// </summary>
    public override string Id => "normalize-whitespace";

    /// <summary>
    /// Gets the display name for this plugin.
    /// </summary>
    public override string Name => "Normalize Whitespace";

    /// <summary>
    /// Gets or sets whether to remove trailing whitespace from lines.
    /// </summary>
    public bool RemoveTrailing { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to remove common indentation from all lines.
    /// </summary>
    public bool RemoveIndent { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to trim whitespace from the left side of the code block.
    /// </summary>
    public bool LeftTrim { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to trim whitespace from the right side of the code block.
    /// </summary>
    public bool RightTrim { get; set; } = true;

    /// <summary>
    /// Gets or sets the maximum line length before breaking lines.
    /// </summary>
    public int? BreakLines { get; set; }

    /// <summary>
    /// Gets or sets the number of spaces to use for indentation.
    /// </summary>
    public int? Indent { get; set; }

    /// <summary>
    /// Gets or sets whether to remove the initial line feed.
    /// </summary>
    public bool RemoveInitialLineFeed { get; set; } = false;

    /// <summary>
    /// Gets or sets the number of spaces to replace tabs with.
    /// </summary>
    public int? TabsToSpaces { get; set; }

    /// <summary>
    /// Gets or sets the number of spaces to convert to tabs.
    /// </summary>
    public int? SpacesToTabs { get; set; }

    /// <summary>
    /// Processes the tokens and normalizes whitespace in the code.
    /// </summary>
    /// <param name="tokens">The tokens to process.</param>
    /// <param name="language">The programming language being highlighted.</param>
    /// <param name="context">The highlighting context.</param>
    public override void ProcessTokens(Token[] tokens, string language, HighlightContext context)
    {
        if (tokens.Length == 0) return;

        // Process the code in the context
        context.Code = NormalizeCode(context.Code);
    }

    private string NormalizeCode(string code)
    {
        if (string.IsNullOrEmpty(code)) return code;

        var lines = code.Split(new[] { '\r', '\n' }, StringSplitOptions.None);

        // Remove initial line feed
        if (RemoveInitialLineFeed && lines.Length > 0 && string.IsNullOrEmpty(lines[0]))
        {
            lines = lines.Skip(1).ToArray();
        }

        // Convert tabs to spaces or vice versa
        if (TabsToSpaces.HasValue)
        {
            lines = lines.Select(line => line.Replace("\t", new string(' ', TabsToSpaces.Value))).ToArray();
        }
        else if (SpacesToTabs.HasValue)
        {
            var spacesPattern = new string(' ', SpacesToTabs.Value);
            lines = lines.Select(line => line.Replace(spacesPattern, "\t")).ToArray();
        }

        // Remove trailing whitespace
        if (RemoveTrailing)
        {
            lines = lines.Select(line => line.TrimEnd()).ToArray();
        }

        // Remove common indentation
        if (RemoveIndent)
        {
            var minIndent = GetMinIndentation(lines);
            if (minIndent > 0)
            {
                lines = lines.Select(line =>
                    string.IsNullOrWhiteSpace(line) ? line : line.Substring(Math.Min(minIndent, line.Length))
                ).ToArray();
            }
        }

        // Add custom indentation
        if (Indent.HasValue)
        {
            var indentStr = new string(' ', Indent.Value);
            lines = lines.Select(line => string.IsNullOrWhiteSpace(line) ? line : indentStr + line).ToArray();
        }

        // Break long lines
        if (BreakLines.HasValue)
        {
            var brokenLines = new List<string>();
            foreach (var line in lines)
            {
                if (line.Length <= BreakLines.Value)
                {
                    brokenLines.Add(line);
                }
                else
                {
                    // Simple line breaking - could be enhanced
                    for (int i = 0; i < line.Length; i += BreakLines.Value)
                    {
                        brokenLines.Add(line.Substring(i, Math.Min(BreakLines.Value, line.Length - i)));
                    }
                }
            }
            lines = brokenLines.ToArray();
        }

        var result = string.Join(Environment.NewLine, lines);

        // Left and right trim
        if (LeftTrim) result = result.TrimStart();
        if (RightTrim) result = result.TrimEnd();

        return result;
    }

    private int GetMinIndentation(string[] lines)
    {
        var minIndent = int.MaxValue;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var indent = 0;
            foreach (var ch in line)
            {
                if (ch == ' ') indent++;
                else if (ch == '\t') indent += 4; // Assume tab = 4 spaces
                else break;
            }
            minIndent = Math.Min(minIndent, indent);
        }
        return minIndent == int.MaxValue ? 0 : minIndent;
    }
}
