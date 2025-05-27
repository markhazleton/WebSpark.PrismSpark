using System.Text.RegularExpressions;

namespace WebSpark.PrismSpark;

/// <summary>
/// The expansion of a simple `RegExp` literal to support additional properties.
/// </summary>
public class GrammarToken
{
    /// <summary>
    /// The regular expression of the token.
    /// </summary>
    public Regex Pattern { get; set; }

    /// <summary>
    /// Gets a value indicating whether lookbehind is enabled for this token.
    /// </summary>
    public bool Lookbehind { get; }

    /// <summary>
    /// Gets a value indicating whether greedy matching is enabled for this token.
    /// </summary>
    public bool Greedy { get; }

    /// <summary>
    /// Gets the array of aliases for this token.
    /// </summary>
    public string[] Alias { get; }
    /// <summary>
    /// Gets or sets the inner grammar for nested token parsing.
    /// </summary>
    public Grammar? Inside { get; set; }

    /// <summary>
    /// Initializes a new instance of the GrammarToken class with a string pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern as a string.</param>
    /// <param name="lookbehind">Whether lookbehind is enabled.</param>
    /// <param name="greedy">Whether greedy matching is enabled.</param>
    /// <param name="alias">Array of aliases for the token.</param>
    /// <param name="inside">Inner grammar for nested parsing.</param>
    public GrammarToken(string pattern,
        bool lookbehind = false,
        bool greedy = false,
        string[]? alias = null,
        Grammar? inside = null) : this(new Regex(pattern, RegexOptions.Compiled), lookbehind, greedy, alias, inside)
    {
    }

    /// <summary>
    /// Initializes a new instance of the GrammarToken class with a Regex pattern.
    /// </summary>
    /// <param name="pattern">The compiled regular expression pattern.</param>
    /// <param name="lookbehind">Whether lookbehind is enabled.</param>
    /// <param name="greedy">Whether greedy matching is enabled.</param>
    /// <param name="alias">Array of aliases for the token.</param>
    /// <param name="inside">Inner grammar for nested parsing.</param>
    public GrammarToken(Regex pattern,
        bool lookbehind = false,
        bool greedy = false,
        string[]? alias = null,
        Grammar? inside = null)
    {
        Pattern = pattern;
        Lookbehind = lookbehind;
        Greedy = greedy;
        Alias = alias ?? Array.Empty<string>();
        Inside = inside;
    }

}
