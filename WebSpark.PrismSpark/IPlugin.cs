namespace WebSpark.PrismSpark;

/// <summary>
/// Interface for all PrismSpark plugins
/// </summary>
public interface IPlugin
{
    /// <summary>
    /// Unique identifier for the plugin
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Display name of the plugin
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Optional dependencies this plugin requires
    /// </summary>
    string[] Dependencies { get; }

    /// <summary>
    /// Initialize the plugin
    /// </summary>
    void Initialize();

    /// <summary>
    /// Process tokens before highlighting
    /// </summary>
    /// <param name="tokens">Tokens to process</param>
    /// <param name="language">Language being highlighted</param>
    /// <param name="context">Additional context</param>
    void ProcessTokens(Token[] tokens, string language, HighlightContext context);

    /// <summary>
    /// Process the final highlighted output
    /// </summary>
    /// <param name="html">Generated HTML</param>
    /// <param name="language">Language that was highlighted</param>
    /// <param name="context">Additional context</param>
    /// <returns>Modified HTML</returns>
    string ProcessOutput(string html, string language, HighlightContext context);
}

/// <summary>
/// Base implementation of IPlugin
/// </summary>
public abstract class PluginBase : IPlugin
{
    /// <summary>
    /// Gets the unique identifier for the plugin.
    /// </summary>
    public abstract string Id { get; }

    /// <summary>
    /// Gets the display name of the plugin.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Gets the dependencies required by this plugin.
    /// </summary>
    public virtual string[] Dependencies => Array.Empty<string>();

    /// <summary>
    /// Initializes the plugin. Override to provide custom initialization logic.
    /// </summary>
    public virtual void Initialize() { }

    /// <summary>
    /// Processes tokens before highlighting. Override to provide token processing logic.
    /// </summary>
    /// <param name="tokens">The tokens to process.</param>
    /// <param name="language">The programming language being highlighted.</param>
    /// <param name="context">The highlighting context.</param>
    public virtual void ProcessTokens(Token[] tokens, string language, HighlightContext context) { }

    /// <summary>
    /// Processes the final highlighted output. Override to provide output processing logic.
    /// </summary>
    /// <param name="html">The generated HTML content.</param>
    /// <param name="language">The programming language that was highlighted.</param>
    /// <param name="context">The highlighting context.</param>
    /// <returns>The processed HTML content.</returns>
    public virtual string ProcessOutput(string html, string language, HighlightContext context) => html;
}
