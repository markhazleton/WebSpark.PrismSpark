using WebSpark.PrismSpark.Plugins;

namespace WebSpark.PrismSpark;

/// <summary>
/// Main PrismSpark initialization and configuration class
/// </summary>
public static class PrismSpark
{
    private static bool _initialized = false;

    /// <summary>
    /// Initialize PrismSpark with default configuration
    /// </summary>
    public static void Initialize()
    {
        if (_initialized) return;

        // Register additional language grammars
        LanguageGrammars.RegisterAdditionalLanguages();

        // Register built-in plugins
        PluginManager.Register(new CustomClassPlugin());
        PluginManager.Register(new LineNumbersPlugin());
        PluginManager.Register(new LineHighlightPlugin());
        PluginManager.Register(new NormalizeWhitespacePlugin());
        PluginManager.Register(new CopyToClipboardPlugin());
        PluginManager.Register(new AutolinkerPlugin());
        PluginManager.Register(new FileHighlightPlugin());
        PluginManager.Register(new ShowLanguagePlugin());
        PluginManager.Register(new ToolbarPlugin());
        PluginManager.Register(new CommandLinePlugin());

        _initialized = true;
    }

    /// <summary>
    /// Initialize PrismSpark with custom configuration
    /// </summary>
    public static void Initialize(Action<PrismConfiguration> configure)
    {
        if (_initialized) return;

        var config = new PrismConfiguration();
        configure(config);

        // Register additional language grammars if enabled
        if (config.RegisterAdditionalLanguages)
        {
            LanguageGrammars.RegisterAdditionalLanguages();
        }

        // Register built-in plugins based on configuration
        if (config.EnableBuiltinPlugins)
        {
            RegisterBuiltinPlugins(config);
        }

        // Initialize plugins
        _initialized = true;
    }

    /// <summary>
    /// Reset PrismSpark to uninitialized state
    /// </summary>
    public static void Reset()
    {
        PluginManager.Clear();
        Hooks.Clear();
        _initialized = false;
    }

    /// <summary>
    /// Check if PrismSpark is initialized
    /// </summary>
    public static bool IsInitialized => _initialized;

    private static void RegisterBuiltinPlugins(PrismConfiguration config)
    {
        var plugins = new List<IPlugin>();

        if (config.EnabledPlugins.Contains("custom-class"))
            plugins.Add(new CustomClassPlugin());
        if (config.EnabledPlugins.Contains("line-numbers"))
            plugins.Add(new LineNumbersPlugin());
        if (config.EnabledPlugins.Contains("line-highlight"))
            plugins.Add(new LineHighlightPlugin());
        if (config.EnabledPlugins.Contains("normalize-whitespace"))
            plugins.Add(new NormalizeWhitespacePlugin());
        if (config.EnabledPlugins.Contains("copy-to-clipboard"))
            plugins.Add(new CopyToClipboardPlugin());
        if (config.EnabledPlugins.Contains("autolinker"))
            plugins.Add(new AutolinkerPlugin());
        if (config.EnabledPlugins.Contains("file-highlight"))
            plugins.Add(new FileHighlightPlugin());
        if (config.EnabledPlugins.Contains("show-language"))
            plugins.Add(new ShowLanguagePlugin());
        if (config.EnabledPlugins.Contains("toolbar"))
            plugins.Add(new ToolbarPlugin());
        if (config.EnabledPlugins.Contains("command-line"))
            plugins.Add(new CommandLinePlugin());

        foreach (var plugin in plugins)
        {
            PluginManager.Register(plugin);
        }
    }
}

/// <summary>
/// Configuration options for PrismSpark initialization
/// </summary>
public class PrismConfiguration
{
    /// <summary>
    /// Whether to register additional language grammars
    /// </summary>
    public bool RegisterAdditionalLanguages { get; set; } = true;

    /// <summary>
    /// Whether to enable built-in plugins
    /// </summary>
    public bool EnableBuiltinPlugins { get; set; } = true;

    /// <summary>
    /// List of enabled plugin names
    /// </summary>
    public HashSet<string> EnabledPlugins { get; set; } = new()
    {
        "custom-class",
        "line-numbers",
        "line-highlight",
        "normalize-whitespace",
        "copy-to-clipboard",
        "autolinker",
        "file-highlight",
        "show-language",
        "toolbar",
        "command-line"
    };

    /// <summary>
    /// Global plugin configuration
    /// </summary>
    public Dictionary<string, Dictionary<string, object>> PluginOptions { get; set; } = new();

    /// <summary>
    /// Configure options for a specific plugin
    /// </summary>
    public PrismConfiguration ConfigurePlugin(string pluginName, Dictionary<string, object> options)
    {
        PluginOptions[pluginName] = options;
        return this;
    }

    /// <summary>
    /// Enable a plugin
    /// </summary>
    public PrismConfiguration EnablePlugin(string pluginName)
    {
        EnabledPlugins.Add(pluginName);
        return this;
    }

    /// <summary>
    /// Disable a plugin
    /// </summary>
    public PrismConfiguration DisablePlugin(string pluginName)
    {
        EnabledPlugins.Remove(pluginName);
        return this;
    }
}
