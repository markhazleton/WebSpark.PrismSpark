using System.Text.RegularExpressions;

namespace WebSpark.PrismSpark.Languages;

// From https://github.com/PrismJS/prism/blob/master/components/prism-json.js

/// <summary>
/// JSON (JavaScript Object Notation) data format grammar definition.
/// </summary>
public class Json : IGrammarDefinition
{
    /// <summary>
    /// Defines the JSON data format grammar with syntax highlighting rules for objects, arrays, strings, numbers, and literals.
    /// </summary>
    /// <returns>A Grammar object containing all JSON syntax rules.</returns>
    public Grammar Define()
    {
        // https://www.json.org/json-en.html
        return new Grammar
        {
            ["property"] = new GrammarToken[]
            {
                new(@"(^|[^\\])""(?:\\.|[^\\""\r\n])*""(?=\s*:)", true, true)
            },
            ["string"] = new GrammarToken[]
            {
                new(@"(^|[^\\])""(?:\\.|[^\\""\r\n])*""(?!\s*:)", true, true)
            },
            ["comment"] = new GrammarToken[]
            {
                new(@"\/\/.*|\/\*[\s\S]*?(?:\*\/|$)", greedy: true)
            },
            ["number"] = new GrammarToken[]
            {
                new(new Regex(@"-?\b\d+(?:\.\d+)?(?:e[+-]?\d+)?\b", RegexOptions.Compiled | RegexOptions.IgnoreCase))
            },
            ["punctuation"] = new GrammarToken[] { new(@"[{}[\],]") },
            ["operator"] = new GrammarToken[] { new(@":") },
            ["boolean"] = new GrammarToken[] { new(@"\b(?:false|true)\b") },
            ["null"] = new GrammarToken[]
            {
                new(@"\bnull\b", alias: new[] { "keyword" })
            },
        };
    }
}
