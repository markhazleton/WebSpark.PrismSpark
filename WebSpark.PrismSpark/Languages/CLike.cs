using System.Text.RegularExpressions;

namespace WebSpark.PrismSpark.Languages;

// From https://github.com/PrismJS/prism/blob/master/components/prism-clike.js

/// <summary>
/// C-like programming language grammar definition, serving as a base for C, C++, Java, and other C-family languages.
/// </summary>
public class CLike : IGrammarDefinition
{
    /// <summary>
    /// Defines the C-like language grammar with common syntax highlighting rules shared by C-family programming languages.
    /// </summary>
    /// <returns>A Grammar object containing basic C-family language syntax rules.</returns>
    public Grammar Define()
    {
        return new Grammar
        {
            ["comment"] = new GrammarToken[]
            {
                new(@"(^|[^\\])\/\*[\s\S]*?(?:\*\/|$)", true, true),
                new(@"(^|[^\\:])\/\/.*", true, true),
            },
            ["string"] = new GrammarToken[]
            {
                new(@"([""'])(?:\\(?:\r\n|[\s\S])|(?!\1)[^\\\r\n])*\1", greedy: true),
            },
            ["class-name"] = new GrammarToken[]
            {
                new(
                    new Regex(
                        @"(\b(?:class|extends|implements|instanceof|interface|new|trait)\s+|\bcatch\s+\()[\w.\\]+",
                        RegexOptions.Compiled | RegexOptions.IgnoreCase),
                    true,
                    inside: new Grammar
                    {
                        ["punctuation"] = new GrammarToken[]
                        {
                            new(@"[.\\]")
                        }
                    }),
            },
            ["keyword"] = new GrammarToken[]
            {
                new(
                    @"\b(?:break|catch|continue|do|else|finally|for|function|if|in|instanceof|new|null|return|throw|try|while)\b"),
            },
            ["boolean"] = new GrammarToken[]
            {
                new(@"\b(?:false|true)\b")
            },
            ["function"] = new GrammarToken[]
            {
                new(@"\b\w+(?=\()"),
            },
            ["number"] = new GrammarToken[]
            {
                new(new Regex(@"\b0x[\da-f]+\b|(?:\b\d+(?:\.\d*)?|\B\.\d+)(?:e[+-]?\d+)?",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase)),
            },
            ["operator"] = new GrammarToken[]
            {
                new(@"[<>]=?|[!=]=?=?|--?|\+\+?|&&?|\|\|?|[?*/~^%]")
            },
            ["punctuation"] = new GrammarToken[]
            {
                new(@"[{}[\];(),.:]")
            }
        };
    }
}
