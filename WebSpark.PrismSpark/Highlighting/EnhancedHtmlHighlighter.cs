using System.Text;

namespace WebSpark.PrismSpark.Highlighting;

/// <summary>
/// Enhanced HTML highlighter with plugin support and advanced features
/// </summary>
public class EnhancedHtmlHighlighter : IHighlighter
{
    private readonly HighlightOptions _defaultOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnhancedHtmlHighlighter"/> class with optional default highlighting options.
    /// </summary>
    /// <param name="defaultOptions">The default highlighting options to use when none are specified. If null, default options will be created.</param>
    public EnhancedHtmlHighlighter(HighlightOptions? defaultOptions = null)
    {
        _defaultOptions = defaultOptions ?? new HighlightOptions();
    }

    /// <summary>
    /// Highlights the specified code using the provided grammar and language, applying default highlighting options.
    /// </summary>
    /// <param name="text">The source code text to highlight.</param>
    /// <param name="grammar">The grammar definition to use for tokenization.</param>
    /// <param name="language">The programming language identifier for the code.</param>
    /// <returns>A string containing the highlighted HTML markup.</returns>
    public string Highlight(string text, Grammar grammar, string language)
    {
        return Highlight(text, grammar, language, _defaultOptions);
    }

    /// <summary>
    /// Highlights the specified code using the provided grammar, language, and custom highlighting options.
    /// This method provides full control over the highlighting process including plugin execution,
    /// custom CSS classes, line numbering, and container wrapping.
    /// </summary>
    /// <param name="text">The source code text to highlight. This will be tokenized according to the grammar rules.</param>
    /// <param name="grammar">The grammar definition containing the tokenization rules for the specified language.</param>
    /// <param name="language">The programming language identifier, used for CSS class generation and plugin selection.</param>
    /// <param name="options">Custom highlighting options that control output formatting, CSS classes, and container generation.</param>
    /// <returns>A string containing the highlighted HTML markup with proper CSS classes and optional container wrapping.</returns>
    /// <exception cref="ArgumentNullException">Thrown when text, grammar, language, or options is null.</exception>
    public string Highlight(string text, Grammar grammar, string language, HighlightOptions options)
    {
        // Create highlight context
        var context = new HighlightContext
        {
            Code = text,
            Language = language,
            Grammar = grammar,
            ShowLineNumbers = options.ShowLineNumbers,
            HighlightedLines = new HashSet<int>(options.HighlightedLines),
            ClassPrefix = options.ClassPrefix,
            ClassMappings = new Dictionary<string, string>(options.ClassMappings),
            Classes = new List<string>(options.CssClasses),
            Attributes = new Dictionary<string, string>(options.Attributes)
        };

        // Run before-tokenize hooks
        var hookArgs = new HookEventArgs
        {
            Language = language,
            Grammar = grammar,
            Code = text,
            Context = context
        };
        Hooks.Run(HookNames.BeforeTokenize, hookArgs);

        if (hookArgs.Cancel) return string.Empty;

        // Update context from hooks
        text = hookArgs.Code;
        grammar = hookArgs.Grammar;

        // Tokenize
        var tokens = Prism.Tokenize(text, grammar);

        // Process tokens with plugins
        PluginManager.ProcessTokens(tokens, language, context);

        // Run after-tokenize hooks
        hookArgs.Tokens = tokens;
        Hooks.Run(HookNames.AfterTokenize, hookArgs);

        if (hookArgs.Cancel) return string.Empty;

        // Generate HTML
        var html = Stringify(hookArgs.Tokens, context, language);

        // Process output with plugins
        html = PluginManager.ProcessOutput(html, language, context);

        // Wrap in container if needed
        if (options.WrapInContainer)
        {
            html = WrapInContainer(html, context, language, options);
        }

        return html;
    }

    private static string Stringify(Token[] tokens, HighlightContext context, string language)
    {
        if (!tokens.Any()) return string.Empty;

        var htmlSb = new StringBuilder();

        foreach (var token in tokens)
        {
            if (token is StringToken stringToken)
            {
                var content = Encode(stringToken.Content);

                if (!stringToken.IsMatchedToken())
                {
                    htmlSb.Append(content);
                    continue;
                }

                const string tag = "span";
                var type = token.Type;
                var classes = BuildClasses(type, token.Alias, context);
                var attributes = BuildAttributes(token, context);

                var html = $"<{tag} class=\"{string.Join(" ", classes)}\"{attributes}>{content}</{tag}>";
                htmlSb.Append(html);
                continue;
            }

            if (token is StreamToken streamToken)
            {
                htmlSb.Append(Stringify(streamToken.Content, context, language));
            }
        }

        return htmlSb.ToString();
    }

    private static List<string> BuildClasses(string? type, string[] alias, HighlightContext context)
    {
        var classes = new List<string> { "token" };

        if (!string.IsNullOrEmpty(type))
        {
            var finalType = ApplyClassMapping(type, context);
            classes.Add(finalType);
        }

        foreach (var aliasClass in alias)
        {
            var finalAlias = ApplyClassMapping(aliasClass, context);
            classes.Add(finalAlias);
        }

        // Apply prefix
        if (!string.IsNullOrEmpty(context.ClassPrefix))
        {
            classes = classes.Select(c => context.ClassPrefix + c).ToList();
        }

        return classes;
    }

    private static string ApplyClassMapping(string className, HighlightContext context)
    {
        return context.ClassMappings.TryGetValue(className, out var mapped) ? mapped : className;
    }

    private static string BuildAttributes(Token token, HighlightContext context)
    {
        var attributes = new StringBuilder();

        // Add any token-specific attributes from context
        if (context.Attributes.Any())
        {
            foreach (var attr in context.Attributes)
            {
                attributes.Append($" {attr.Key}=\"{Encode(attr.Value)}\"");
            }
        }

        return attributes.ToString();
    }

    private static string WrapInContainer(string html, HighlightContext context, string language, HighlightOptions options)
    {
        var containerTag = options.ContainerTag;
        var classes = new List<string> { $"language-{language}" };
        classes.AddRange(context.Classes);

        var classAttr = classes.Any() ? $" class=\"{string.Join(" ", classes)}\"" : string.Empty;
        var dataLangAttr = $" data-language=\"{language}\"";

        var attributes = new StringBuilder();
        attributes.Append(classAttr);
        attributes.Append(dataLangAttr);

        foreach (var attr in context.Attributes)
        {
            attributes.Append($" {attr.Key}=\"{Encode(attr.Value)}\"");
        }

        return $"<{containerTag}{attributes}>{html}</{containerTag}>";
    }

    private static string Encode(string content)
    {
        return content.Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&#39;")
            .Replace("\u00a0", " ");
    }
}

/// <summary>
/// Configuration options for controlling HTML syntax highlighting output, including line numbering,
/// CSS class customization, container wrapping, and additional HTML attributes.
/// </summary>
public class HighlightOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether line numbers should be displayed alongside the highlighted code.
    /// When enabled, line numbers will be generated and included in the output markup.
    /// </summary>
    /// <value>
    /// <c>true</c> if line numbers should be shown; otherwise, <c>false</c>. The default is <c>false</c>.
    /// </value>
    public bool ShowLineNumbers { get; set; } = false;

    /// <summary>
    /// Gets or sets the collection of line numbers (1-based) that should be visually highlighted
    /// with special CSS classes to draw attention to specific lines of code.
    /// </summary>
    /// <value>
    /// A list of integers representing the line numbers to highlight. Empty by default.
    /// </value>
    public List<int> HighlightedLines { get; set; } = new();

    /// <summary>
    /// Gets or sets an optional prefix that will be prepended to all generated CSS class names.
    /// This is useful for avoiding CSS conflicts when multiple highlighting libraries are used on the same page.
    /// </summary>
    /// <value>
    /// The CSS class prefix string, or <c>null</c> if no prefix should be applied. Default is <c>null</c>.
    /// </value>
    /// <example>
    /// Setting ClassPrefix to "prism-" will generate classes like "prism-token", "prism-keyword", etc.
    /// </example>
    public string? ClassPrefix { get; set; }

    /// <summary>
    /// Gets or sets a dictionary that maps default CSS class names to custom class names,
    /// allowing for complete customization of the generated CSS classes.
    /// </summary>
    /// <value>
    /// A dictionary where keys are the original class names and values are the replacement class names. Empty by default.
    /// </value>
    /// <example>
    /// Mapping "keyword" to "my-keyword" will replace all instances of the "keyword" class with "my-keyword".
    /// </example>
    public Dictionary<string, string> ClassMappings { get; set; } = new();

    /// <summary>
    /// Gets or sets additional CSS classes that will be applied to the container element
    /// when <see cref="WrapInContainer"/> is enabled.
    /// </summary>
    /// <value>
    /// A list of CSS class names to add to the container. Empty by default.
    /// </value>
    public List<string> CssClasses { get; set; } = new();

    /// <summary>
    /// Gets or sets additional HTML attributes that will be applied to the container element
    /// when <see cref="WrapInContainer"/> is enabled.
    /// </summary>
    /// <value>
    /// A dictionary of attribute names and values to add to the container. Empty by default.
    /// </value>
    /// <example>
    /// Adding {"id", "my-code-block"} will add id="my-code-block" to the container element.
    /// </example>
    public Dictionary<string, string> Attributes { get; set; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether the highlighted output should be wrapped
    /// in a container element with appropriate CSS classes and attributes.
    /// </summary>
    /// <value>
    /// <c>true</c> if the output should be wrapped in a container; otherwise, <c>false</c>. The default is <c>true</c>.
    /// </value>
    public bool WrapInContainer { get; set; } = true;

    /// <summary>
    /// Gets or sets the HTML tag name to use for the container element when <see cref="WrapInContainer"/> is enabled.
    /// Common values include "code", "pre", "div", or any other valid HTML element name.
    /// </summary>
    /// <value>
    /// The HTML tag name for the container element. The default is "code".
    /// </value>
    public string ContainerTag { get; set; } = "code";
}
