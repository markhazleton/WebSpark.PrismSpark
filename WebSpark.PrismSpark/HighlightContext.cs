namespace WebSpark.PrismSpark;

/// <summary>
/// Context information passed through the highlighting process
/// </summary>
public class HighlightContext
{
    /// <summary>
    /// The original source code being highlighted
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// The language being highlighted
    /// </summary>
    public string Language { get; set; } = string.Empty;

    /// <summary>
    /// The grammar being used
    /// </summary>
    public Grammar Grammar { get; set; } = new();

    /// <summary>
    /// Additional metadata
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();

    /// <summary>
    /// CSS classes to add to the root element
    /// </summary>
    public List<string> Classes { get; set; } = new();

    /// <summary>
    /// HTML attributes to add to the root element
    /// </summary>
    public Dictionary<string, string> Attributes { get; set; } = new();

    /// <summary>
    /// Whether line numbers should be included
    /// </summary>
    public bool ShowLineNumbers { get; set; }

    /// <summary>
    /// Lines to highlight (1-based)
    /// </summary>
    public HashSet<int> HighlightedLines { get; set; } = new();

    /// <summary>
    /// Custom prefix for CSS classes
    /// </summary>
    public string? ClassPrefix { get; set; }

    /// <summary>
    /// Custom class mappings
    /// </summary>
    public Dictionary<string, string> ClassMappings { get; set; } = new();
}
