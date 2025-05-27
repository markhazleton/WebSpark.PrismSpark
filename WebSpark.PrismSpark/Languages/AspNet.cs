using System.Text.RegularExpressions;

namespace WebSpark.PrismSpark.Languages;

// From https://github.com/PrismJS/prism/blob/master/components/prism-aspnet.js

/// <summary>
/// Provides syntax highlighting grammar definition for ASP.NET Web Forms.
/// Supports ASP.NET syntax including page directives, server controls, and embedded C# code blocks.
/// </summary>
public class AspNet : IGrammarDefinition
{
    /// <summary>
    /// Defines the grammar rules for ASP.NET Web Forms syntax highlighting.
    /// </summary>
    /// <returns>A Grammar object containing the token definitions for ASP.NET markup and code syntax.</returns>
    public Grammar Define()
    {
        var aspnetGrammar = new Markup().Define();
        var csharpGrammar = new CSharp().Define();

        aspnetGrammar["page-directive"] = new GrammarToken[]
        {
            new(@"<%\s*@.*%>", alias: new[] { "tag" },
                inside: new Grammar
                {
                    ["page-directive"] = new GrammarToken[]
                    {
                        new(new Regex(
                                @"<%\s*@\s*(?:Assembly|Control|Implements|Import|Master(?:Type)?|OutputCache|Page|PreviousPageType|Reference|Register)?|%>",
                                RegexOptions.Compiled | RegexOptions.IgnoreCase),
                            alias: new[] { "tag" })
                    },
                    Reset = aspnetGrammar["tag"][0].Inside
                })
        };
        aspnetGrammar["directive"] = new GrammarToken[]
        {
            new(@"<%.*%>", alias: new[] { "tag" },
                inside: new Grammar
                {
                    ["directive"] = new GrammarToken[]
                    {
                        new(@"<%\s*?[$=%#:]{0,2}|%>", alias: new[] { "tag" })
                    },
                    Reset = csharpGrammar
                })
        };

        // Regexp copied from prism-markup, with a negative look-ahead added
        aspnetGrammar["tag"][0].Pattern =
            new Regex(
                @"<(?!%)\/?[^\s>\/]+(?:\s+[^\s>\/=]+(?:=(?:(""|')(?:\\[\s\S]|(?!\1)[^\\])*\1|[^\s'"">=]+))?)*\s*\/?>",
                RegexOptions.Compiled);

        aspnetGrammar["tag"][0].Inside!["attr-value"][0].Inside!.InsertBefore("punctuation", new Grammar
        {
            ["directive"] = aspnetGrammar["directive"]
        });

        aspnetGrammar.InsertBefore("comment", new Grammar
        {
            ["asp-comment"] = new GrammarToken[]
            {
                new(@"<%--[\s\S]*?--%>", alias: new[] { "asp", "comment" })
            }
        });

        // script runat="server" contains csharp, not javascript
        aspnetGrammar.InsertBefore("tag", new Grammar
        {
            ["asp-script"] = new GrammarToken[]
            {
                new(
                    new Regex(@"(<script(?=.*runat=['""]?server\b)[^>]*>)[\s\S]*?(?=<\/script>)",
                        RegexOptions.Compiled | RegexOptions.IgnoreCase),
                    true,
                    alias: new[] { "asp", "script" },
                    inside: csharpGrammar)
            }
        });

        return aspnetGrammar;
    }
}
