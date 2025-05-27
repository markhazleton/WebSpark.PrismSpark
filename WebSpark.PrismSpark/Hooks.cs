namespace WebSpark.PrismSpark;

/// <summary>
/// Event arguments for hooks
/// </summary>
public class HookEventArgs : EventArgs
{
    /// <summary>
    /// Gets or sets the programming language being processed.
    /// </summary>
    public string Language { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the grammar rules being used.
    /// </summary>
    public Grammar Grammar { get; set; } = new();

    /// <summary>
    /// Gets or sets the tokens generated during processing.
    /// </summary>
    public Token[] Tokens { get; set; } = Array.Empty<Token>();

    /// <summary>
    /// Gets or sets the source code being processed.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the highlighting context.
    /// </summary>
    public HighlightContext Context { get; set; } = new();

    /// <summary>
    /// Gets or sets additional data associated with the hook event.
    /// </summary>
    public Dictionary<string, object> Data { get; set; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether to cancel further processing.
    /// </summary>
    public bool Cancel { get; set; }
}

/// <summary>
/// Hook system for extensibility
/// </summary>
public static class Hooks
{
    private static readonly Dictionary<string, List<Action<HookEventArgs>>> _hooks = new();

    /// <summary>
    /// Register a hook handler
    /// </summary>
    /// <param name="hookName">Name of the hook</param>
    /// <param name="handler">Handler function</param>
    public static void Add(string hookName, Action<HookEventArgs> handler)
    {
        if (!_hooks.ContainsKey(hookName))
        {
            _hooks[hookName] = new List<Action<HookEventArgs>>();
        }
        _hooks[hookName].Add(handler);
    }

    /// <summary>
    /// Remove a hook handler
    /// </summary>
    /// <param name="hookName">Name of the hook</param>
    /// <param name="handler">Handler function to remove</param>
    public static void Remove(string hookName, Action<HookEventArgs> handler)
    {
        if (_hooks.ContainsKey(hookName))
        {
            _hooks[hookName].Remove(handler);
        }
    }

    /// <summary>
    /// Execute all handlers for a hook
    /// </summary>
    /// <param name="hookName">Name of the hook</param>
    /// <param name="args">Event arguments</param>
    public static void Run(string hookName, HookEventArgs args)
    {
        if (!_hooks.ContainsKey(hookName)) return;

        foreach (var handler in _hooks[hookName])
        {
            handler(args);
            if (args.Cancel) break;
        }
    }

    /// <summary>
    /// Get all registered hook names
    /// </summary>
    public static IEnumerable<string> GetHookNames() => _hooks.Keys;

    /// <summary>
    /// Clear all hooks
    /// </summary>
    public static void Clear() => _hooks.Clear();

    /// <summary>
    /// Clear hooks for a specific name
    /// </summary>
    /// <param name="hookName">Name of the hook to clear</param>
    public static void Clear(string hookName) => _hooks.Remove(hookName);
}

/// <summary>
/// Common hook names used by PrismSpark
/// </summary>
public static class HookNames
{
    /// <summary>Hook fired before tokenization begins.</summary>
    public const string BeforeTokenize = "before-tokenize";
    /// <summary>Hook fired after tokenization completes.</summary>
    public const string AfterTokenize = "after-tokenize";
    /// <summary>Hook fired before token insertion.</summary>
    public const string BeforeInsert = "before-insert";
    /// <summary>Hook fired after token insertion.</summary>
    public const string AfterInsert = "after-insert";
    /// <summary>Hook fired before highlighting begins.</summary>
    public const string BeforeHighlight = "before-highlight";
    /// <summary>Hook fired after highlighting completes.</summary>
    public const string AfterHighlight = "after-highlight";
    /// <summary>Hook fired before highlighting an element.</summary>
    public const string BeforeHighlightElement = "before-highlight-element";
    /// <summary>Hook fired after highlighting an element.</summary>
    public const string AfterHighlightElement = "after-highlight-element";
    /// <summary>Hook fired when processing is complete.</summary>
    public const string Complete = "complete";
    /// <summary>Hook fired when wrapping tokens.</summary>
    public const string WrapTokens = "wrap-tokens";
}
