using System.Text.RegularExpressions;

namespace WebSpark.PrismSpark.Languages;

/// <summary>
/// Markdown language grammar definition for PrismSpark.
/// Based on PrismJS prism-markdown.js component.
/// </summary>
public class Markdown : IGrammarDefinition
{
    /// <summary>
    /// Defines the Markdown language grammar for syntax highlighting.
    /// </summary>
    public Grammar Define()
    {
        var grammar = new Grammar();

        // Front matter (YAML between --- delimiters)
        grammar["front-matter-block"] = new[]
        {
            new GrammarToken(
                new Regex(@"^---[\s\S]*?^---$", RegexOptions.Multiline),
                alias: new[] { "front-matter" },
                inside: new Grammar
                {
                    ["punctuation"] = new GrammarToken[] { new(@"^---|---$") }
                })
        };

        // Blockquotes
        grammar["blockquote"] = new[]
        {
            new GrammarToken(
                new Regex(@"^>(?:[\t ]*>)*", RegexOptions.Multiline))
        };

        // Fenced code blocks: ```lang ... ```
        grammar["code"] = new[]
        {
            // Fenced code block
            new GrammarToken(
                new Regex(@"^([ \t]*(?:`{3,}|~{3,}))[\s\S]*?(?:^[ \t]*\1`*[ \t]*$)", RegexOptions.Multiline),
                greedy: true,
                alias: new[] { "keyword" }),
            // Indented code block (4 spaces or 1 tab)
            new GrammarToken(
                new Regex(@"(?:^|\n)(?:(?:    |\t).*(?:\n|$))+", RegexOptions.Compiled))
        };

        // Headings
        grammar["title"] = new[]
        {
            // ATX headings: # Heading
            new GrammarToken(
                new Regex(@"(^\s*)#{1,6}.+", RegexOptions.Multiline),
                lookbehind: true,
                alias: new[] { "important" },
                inside: new Grammar
                {
                    ["punctuation"] = new GrammarToken[] { new(@"^#{1,6}") }
                }),
            // Setext heading underline
            new GrammarToken(
                new Regex(@"(^\s*)(?=.+)(?:\*[ \t]*\*[ \t]*\*[ \t]*[\*\s]*$|-[ \t]*-[ \t]*-[ \t]*[\-\s]*$|=[ \t]*=[ \t]*=[ \t]*[=\s]*$)", RegexOptions.Multiline),
                lookbehind: true,
                alias: new[] { "important" })
        };

        // Horizontal rules
        grammar["hr"] = new[]
        {
            new GrammarToken(
                new Regex(@"(^\s*)([*\-_])(?:[\t ]*\2){2,}(?=\s*$)", RegexOptions.Multiline),
                lookbehind: true,
                alias: new[] { "punctuation" })
        };

        // Lists
        grammar["list"] = new[]
        {
            new GrammarToken(
                new Regex(@"(^\s*)(?:[*+\-]|\d+\.)(?=[\t ].)", RegexOptions.Multiline),
                lookbehind: true,
                alias: new[] { "punctuation" })
        };

        // Tables (GFM)
        grammar["table"] = new[]
        {
            new GrammarToken(
                new Regex(@"(^.*\|.*\n)[ \t]*\|?[ \t]*:?-{3,}:?[ \t]*(?:\|[ \t]*:?-{3,}:?[ \t]*)*\|?(?:\n.*\|.*)*(?:\n|$)", RegexOptions.Multiline),
                lookbehind: true,
                inside: new Grammar
                {
                    ["table-header-row"] = new GrammarToken[]
                    {
                        new(new Regex(@"^.*(?=\n)"),
                            inside: new Grammar
                            {
                                ["table-header"] = new GrammarToken[]
                                {
                                    new(new Regex(@"[^\|]+"), alias: new[] { "important" })
                                },
                                ["punctuation"] = new GrammarToken[] { new(@"\|") }
                            })
                    },
                    ["table-data-rows"] = new GrammarToken[]
                    {
                        new(new Regex(@"(^.*(?:\n|$))[\s\S]+", RegexOptions.Multiline),
                            lookbehind: true,
                            inside: new Grammar
                            {
                                ["table-data"] = new GrammarToken[]
                                {
                                    new(new Regex(@"[^\|]+"))
                                },
                                ["punctuation"] = new GrammarToken[] { new(@"\|") }
                            })
                    },
                    ["punctuation"] = new GrammarToken[] { new(@"\|") }
                })
        };

        // URLs (autolinks)
        grammar["url-reference"] = new[]
        {
            new GrammarToken(
                new Regex(@"!?\[[^\]]+\]:[\t ]+(?:\S+|<(?:\\.|[^>\\])+>)(?:[\t ]+(?:""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'|\((?:\\.|[^)\\])*\)))?"),
                inside: new Grammar
                {
                    ["variable"] = new GrammarToken[]
                    {
                        new(new Regex(@"^(!?\[)[^\]]+"), lookbehind: true)
                    },
                    ["string"] = new GrammarToken[]
                    {
                        new(new Regex(@"(?:""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'|\((?:\\.|[^)\\])*\))$"))
                    },
                    ["punctuation"] = new GrammarToken[]
                    {
                        new(new Regex(@"^[!\[\]:]|[<>]"))
                    },
                    ["url"] = new GrammarToken[]
                    {
                        new(new Regex(@"[\s\S]+"))
                    }
                },
                alias: new[] { "url" })
        };

        // Bold
        grammar["bold"] = new[]
        {
            // **bold** or __bold__
            new GrammarToken(
                new Regex(@"(?:(\*\*|__)(?:(?!\1)[^\\\n]|\\.)*\1)"),
                greedy: true,
                inside: new Grammar
                {
                    ["content"] = new GrammarToken[]
                    {
                        new(new Regex(@"^.[\s\S]+(?=.$)"), inside: new Grammar()) // placeholder
                    },
                    ["punctuation"] = new GrammarToken[] { new(@"^\*\*|^\*\*|\*\*$|^__|__$") }
                })
        };

        // Italic
        grammar["italic"] = new[]
        {
            // *italic* or _italic_
            new GrammarToken(
                new Regex(@"(?:(\*|_)(?:(?!\1)[^\\\n]|\\.)*\1)"),
                greedy: true,
                inside: new Grammar
                {
                    ["content"] = new GrammarToken[]
                    {
                        new(new Regex(@"^.[\s\S]+(?=.$)"), inside: new Grammar()) // placeholder
                    },
                    ["punctuation"] = new GrammarToken[] { new(@"^[*_]|[*_]$") }
                })
        };

        // Strikethrough (GFM)
        grammar["strike"] = new[]
        {
            new GrammarToken(
                new Regex(@"~~(?:(?!~~)[^\\\n]|\\.)+~~"),
                greedy: true,
                inside: new Grammar
                {
                    ["content"] = new GrammarToken[]
                    {
                        new(new Regex(@"^.[\s\S]+(?=.$)"), inside: new Grammar())
                    },
                    ["punctuation"] = new GrammarToken[] { new(@"^~~|~~$") }
                })
        };

        // Inline code
        grammar["code-snippet"] = new[]
        {
            new GrammarToken(
                new Regex(@"(?:`[^`\n]+`|``[^`][\s\S]*?[^`]``)"),
                greedy: true,
                alias: new[] { "code", "keyword" })
        };

        // Links and images
        grammar["url"] = new[]
        {
            // ![alt](url) images
            new GrammarToken(
                new Regex(@"!?\[(?:(?!\])(?:\\.|[^\\\n]))*\](?:\([^\s)]+(?:[\t ]+""(?:\\.|[^""\\])*"")?\)|[ \t]?\[(?:(?!\])(?:\\.|[^\\\n]))*\])"),
                greedy: true,
                inside: new Grammar
                {
                    ["operator"] = new GrammarToken[] { new(@"^!") },
                    ["content"] = new GrammarToken[]
                    {
                        new(new Regex(@"(?<=^\[)[^\]]+(?=\])"), inside: new Grammar())
                    },
                    ["variable"] = new GrammarToken[]
                    {
                        new(new Regex(@"(?<=^\][ \t]?\[)[^\]]+(?=\]$)"))
                    },
                    ["url"] = new GrammarToken[]
                    {
                        new(new Regex(@"(?<=^\]\()[^\s)]+"))
                    },
                    ["string"] = new GrammarToken[]
                    {
                        new(new Regex(@"(?<=^\]\([^\s)]+[\t ]+)""(?:\\.|[^""\\])*""(?=\)$)"))
                    }
                }),
            // Autolinks <url>
            new GrammarToken(
                new Regex(@"<(?:https?|ftp|file):\/\/[^\s>]+>"),
                greedy: true,
                alias: new[] { "url" })
        };

        return grammar;
    }
}
