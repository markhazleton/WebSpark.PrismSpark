using System.Text.RegularExpressions;

namespace WebSpark.PrismSpark.Languages;

/// <summary>
/// PUG template engine grammar definition for PrismSpark.
/// </summary>
public class Pug : IGrammarDefinition
{
    /// <summary>
    /// Defines the PUG language grammar for syntax highlighting.
    /// </summary>
    public Grammar Define()
    {
        var grammar = new Grammar();

        // Comments (single and multiline)
        grammar["comment"] = new[] {
            new GrammarToken(
                new Regex(@"(^([\t ]*))//.*(?:(?:\r?\n|\r)\2[\t ].+)*", RegexOptions.Multiline)
            )
        };

        // Doctype
        grammar["doctype"] = new[] {
            new GrammarToken(
                new Regex(@"((?:^|\n)[\t ]*)doctype(?: .+)?", RegexOptions.Multiline)
            )
        };

        // Keywords
        grammar["keyword"] = new[] {
            new GrammarToken(
                new Regex(@"(^[\t ]*)(?:append|block|extends|include|prepend)\b.+", RegexOptions.Multiline)
            )
        };

        // Mixin declaration
        grammar["mixin-declaration"] = new[] {
            new GrammarToken(
                new Regex(@"(^[\t ]*)mixin .+", RegexOptions.Multiline),
                lookbehind: true
            )
        };

        // Mixin usage
        grammar["mixin-usage"] = new[] {
            new GrammarToken(
                new Regex(@"(^[\t ]*)\+.+", RegexOptions.Multiline),
                lookbehind: true
            )
        };

        // Tag (very basic)
        grammar["tag"] = new[] {
            new GrammarToken(
                new Regex(@"(^[\t ]*)(?!-)[\w\-#.]+", RegexOptions.Multiline),
                lookbehind: true
            )
        };

        // Code block (very basic)
        grammar["code"] = new[] {
            new GrammarToken(
                new Regex(@"(^[\t ]*(?:-|!?=)).+", RegexOptions.Multiline),
                lookbehind: true
            )
        };

        // Punctuation
        grammar["punctuation"] = new[] {
            new GrammarToken(new Regex(@"[.\-!=|]+"))
        };

        return grammar;
    }
}
