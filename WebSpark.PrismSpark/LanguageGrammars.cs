using WebSpark.PrismSpark.Languages;

namespace WebSpark.PrismSpark;

/// <summary>
/// Provides static access to language grammar definitions and manages the registry of supported programming languages.
/// </summary>
// TODO: maybe the class could be created by `source-generators`
public static class LanguageGrammars
{
    private static readonly IDictionary<string, Lazy<Grammar>> Definitions;

    static LanguageGrammars()
    {
        Definitions = new Dictionary<string, Lazy<Grammar>>(16);

        AddDefinition<C>("c");
        AddDefinition<CLike>("clike");
        AddDefinition<CSharp>("csharp", "cs", "dotnet");
        AddDefinition<AspNet>("aspnet", "aspx");
        AddDefinition<CSHtml>("cshtml", "razor");
        AddDefinition<Cil>("cil");
        AddDefinition<JavaScript>("javascript", "js");
        AddDefinition<RegExp>("regexp", "regex");
        AddDefinition<Markup>("markup", "html", "mathml", "svg", "xml", "atom", "rss");
        AddDefinition<Sql>("sql");
        AddDefinition<Json>("json", "web-manifest");
        AddDefinition<PowerShell>("powershell", "ps1");
        AddDefinition<Yaml>("yaml", "yml");
        AddDefinition<Css>("css");
        AddDefinition<Lua>("lua");
        AddDefinition<Bash>("bash", "sh", "shell");
        AddDefinition<Batch>("batch", "cmd");
        AddDefinition<Cpp>("cpp", "c++");
        AddDefinition<Go>("go");
        AddDefinition<Rust>("rust");
        AddDefinition<Python>("python", "py");
        AddDefinition<Java>("java");
        AddDefinition<Pug>("pug");
        AddDefinition<Markdown>("markdown", "md");
    }

    /// <summary>
    /// Adds a grammar definition for the specified language aliases.
    /// </summary>
    /// <typeparam name="T">The grammar definition type that implements IGrammarDefinition.</typeparam>
    /// <param name="alias">The language aliases to register for this grammar.</param>
    public static void AddDefinition<T>(params string[] alias) where T : IGrammarDefinition, new()
    {
        var lazyVal = new Lazy<Grammar>(() => new T().Define());
        foreach (var name in alias)
            Definitions.Add(name, lazyVal);
    }

    /// <summary>
    /// Register a grammar directly
    /// </summary>
    /// <param name="name">The language name to register.</param>
    /// <param name="grammar">The grammar definition to register.</param>
    public static void RegisterGrammar(string name, Grammar grammar)
    {
        Definitions[name] = new Lazy<Grammar>(() => grammar);
    }

    /// <summary>
    /// Register additional language grammars
    /// </summary>
    public static void RegisterAdditionalLanguages()
    {
        // Additional languages will be registered here when PopularLanguages.cs is recreated
        // with correct API usage
    }

    /// <summary>
    /// Gets the grammar definition for the specified language.
    /// </summary>
    /// <param name="language">The language name or alias.</param>
    /// <returns>The grammar definition for the language, or an empty grammar if not found.</returns>
    public static Grammar GetGrammar(string language)
    {
        Definitions.TryGetValue(language, out var grammar);
        return grammar?.Value ?? new Grammar();
    }

    /// <summary>Gets the C programming language grammar.</summary>
    public static Grammar C => GetGrammar("c");
    /// <summary>Gets the C++ programming language grammar.</summary>
    public static Grammar Cpp => GetGrammar("cpp");
    /// <summary>Gets the C-like programming language grammar.</summary>
    public static Grammar CLike => GetGrammar("clike");
    /// <summary>Gets the C# programming language grammar.</summary>
    public static Grammar CSharp => GetGrammar("csharp");
    // public static Grammar Cs => CSharp;
    // public static Grammar DotNet => CSharp;
    /// <summary>Gets the ASP.NET grammar.</summary>
    public static Grammar AspNet => GetGrammar("aspnet");
    // public static Grammar Aspx => AspNet;
    /// <summary>Gets the C# HTML (Razor) grammar.</summary>
    public static Grammar CSHtml => GetGrammar("cshtml");
    /// <summary>Gets the Common Intermediate Language (CIL) grammar.</summary>
    public static Grammar Cil => GetGrammar("cil");
    /// <summary>Gets the JavaScript programming language grammar.</summary>
    public static Grammar JavaScript => GetGrammar("javascript");
    // public static Grammar Js => JavaScript;
    /// <summary>Gets the Regular Expression grammar.</summary>
    public static Grammar RegExp => GetGrammar("regexp");
    // public static Grammar Regex => RegExp;
    /// <summary>Gets the Markup language (HTML/XML) grammar.</summary>
    public static Grammar Markup => GetGrammar("markup");
    // public static Grammar Html => Markup;
    // public static Grammar Mathml => Markup;
    // public static Grammar Svg => Markup;
    // public static Grammar Xml => Markup;
    // public static Grammar Atom => Xml;
    // public static Grammar Rss => Xml;
    /// <summary>Gets the SQL database language grammar.</summary>
    public static Grammar Sql => GetGrammar("sql");
    /// <summary>Gets the JSON data format grammar.</summary>
    public static Grammar Json => GetGrammar("json");
    // public static Grammar WebManifest => Json;
    /// <summary>Gets the PowerShell scripting language grammar.</summary>
    public static Grammar PowerShell => GetGrammar("powershell");
    // public static Grammar Ps1 => PowerShell;
    /// <summary>Gets the YAML data serialization language grammar.</summary>
    public static Grammar Yaml => GetGrammar("yaml");
    // public static Grammar Yml => Yaml;
    /// <summary>Gets the CSS stylesheet language grammar.</summary>
    public static Grammar Css => GetGrammar("css");
    /// <summary>Gets the Lua programming language grammar.</summary>
    public static Grammar Lua => GetGrammar("lua");
    /// <summary>Gets the Bash shell scripting language grammar.</summary>
    public static Grammar Bash => GetGrammar("bash");
    /// <summary>Gets the Windows Batch scripting language grammar.</summary>
    public static Grammar Batch => GetGrammar("batch");
    /// <summary>Gets the Go programming language grammar.</summary>
    public static Grammar Go => GetGrammar("go");
    /// <summary>Gets the Rust programming language grammar.</summary>
    public static Grammar Rust => GetGrammar("rust");
    /// <summary>Gets the Python programming language grammar.</summary>
    public static Grammar Python => GetGrammar("python");
    /// <summary>Gets the Java programming language grammar.</summary>
    public static Grammar Java => GetGrammar("java");
    /// <summary>Gets the Pug programming language grammar.</summary>
    public static Grammar Pug => GetGrammar("pug");
    /// <summary>Gets the Markdown language grammar.</summary>
    public static Grammar Markdown => GetGrammar("markdown");
}
