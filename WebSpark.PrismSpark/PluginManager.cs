namespace WebSpark.PrismSpark;

/// <summary>
/// Manages registration and execution of plugins
/// </summary>
public static class PluginManager
{
    private static readonly Dictionary<string, IPlugin> _plugins = new();
    private static readonly List<IPlugin> _orderedPlugins = new();
    private static bool _initialized = false;

    /// <summary>
    /// Register a plugin
    /// </summary>
    /// <param name="plugin">Plugin to register</param>
    public static void Register(IPlugin plugin)
    {
        if (_plugins.ContainsKey(plugin.Id))
        {
            throw new InvalidOperationException($"Plugin '{plugin.Id}' is already registered");
        }

        _plugins[plugin.Id] = plugin;
        _initialized = false; // Force re-initialization
    }

    /// <summary>
    /// Register a plugin by type
    /// </summary>
    /// <typeparam name="T">Plugin type</typeparam>
    public static void Register<T>() where T : IPlugin, new()
    {
        Register(new T());
    }

    /// <summary>
    /// Get a registered plugin by ID
    /// </summary>
    /// <param name="id">Plugin ID</param>
    /// <returns>Plugin instance or null if not found</returns>
    public static IPlugin? GetPlugin(string id)
    {
        return _plugins.TryGetValue(id, out var plugin) ? plugin : null;
    }

    /// <summary>
    /// Get all registered plugins
    /// </summary>
    /// <returns>Collection of all plugins</returns>
    public static IEnumerable<IPlugin> GetAllPlugins()
    {
        EnsureInitialized();
        return _orderedPlugins.AsReadOnly();
    }

    /// <summary>
    /// Check if a plugin is registered
    /// </summary>
    /// <param name="id">Plugin ID</param>
    /// <returns>True if registered</returns>
    public static bool IsRegistered(string id)
    {
        return _plugins.ContainsKey(id);
    }

    /// <summary>
    /// Initialize all plugins in dependency order
    /// </summary>
    public static void Initialize()
    {
        if (_initialized) return;

        // Sort plugins by dependencies
        _orderedPlugins.Clear();
        var resolved = new HashSet<string>();
        var visiting = new HashSet<string>();

        foreach (var plugin in _plugins.Values)
        {
            ResolveDependencies(plugin, resolved, visiting);
        }

        // Initialize all plugins
        foreach (var plugin in _orderedPlugins)
        {
            plugin.Initialize();
        }

        _initialized = true;
    }

    /// <summary>
    /// Process tokens with all registered plugins
    /// </summary>
    /// <param name="tokens">Tokens to process</param>
    /// <param name="language">Language being highlighted</param>
    /// <param name="context">Highlight context</param>
    public static void ProcessTokens(Token[] tokens, string language, HighlightContext context)
    {
        EnsureInitialized();
        foreach (var plugin in _orderedPlugins)
        {
            plugin.ProcessTokens(tokens, language, context);
        }
    }

    /// <summary>
    /// Process output with all registered plugins
    /// </summary>
    /// <param name="html">HTML output</param>
    /// <param name="language">Language that was highlighted</param>
    /// <param name="context">Highlight context</param>
    /// <returns>Processed HTML</returns>
    public static string ProcessOutput(string html, string language, HighlightContext context)
    {
        EnsureInitialized();
        foreach (var plugin in _orderedPlugins)
        {
            html = plugin.ProcessOutput(html, language, context);
        }
        return html;
    }

    /// <summary>
    /// Clear all registered plugins
    /// </summary>
    public static void Clear()
    {
        _plugins.Clear();
        _orderedPlugins.Clear();
        _initialized = false;
    }

    private static void EnsureInitialized()
    {
        if (!_initialized)
        {
            Initialize();
        }
    }

    private static void ResolveDependencies(IPlugin plugin, HashSet<string> resolved, HashSet<string> visiting)
    {
        if (resolved.Contains(plugin.Id))
            return;

        if (visiting.Contains(plugin.Id))
            throw new InvalidOperationException($"Circular dependency detected involving plugin '{plugin.Id}'");

        visiting.Add(plugin.Id);

        // Resolve dependencies first
        foreach (var depId in plugin.Dependencies)
        {
            if (!_plugins.TryGetValue(depId, out var dependency))
            {
                throw new InvalidOperationException($"Plugin '{plugin.Id}' depends on '{depId}' which is not registered");
            }
            ResolveDependencies(dependency, resolved, visiting);
        }

        visiting.Remove(plugin.Id);
        resolved.Add(plugin.Id);
        _orderedPlugins.Add(plugin);
    }
}
