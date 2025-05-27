using System.Text;

namespace WebSpark.PrismSpark.Highlighting;

/// <summary>
/// Provides HTML syntax highlighting by converting tokenized code into HTML with appropriate CSS classes.
/// </summary>
public class HtmlHighlighter : IHighlighter
{
    /// <summary>
    /// Highlights the specified text using the provided grammar and returns HTML markup.
    /// </summary>
    /// <param name="text">The source code text to highlight.</param>
    /// <param name="grammar">The grammar definition to use for tokenization.</param>
    /// <param name="language">The programming language identifier.</param>
    /// <returns>HTML markup with syntax highlighting CSS classes applied.</returns>
    public string Highlight(string text, Grammar grammar, string language)
    {
        var tokens = Prism.Tokenize(text, grammar);
        return Stringify(tokens);
    }

    private static string Stringify(Token[] tokens)
    {
        if (!tokens.Any()) return string.Empty;

        var htmlSb = new StringBuilder();

        foreach (var token in tokens)
        {
            if (token is StringToken stringToken)
            {
                var content = Encode(stringToken.Content);

                if (!stringToken.IsMatchedToken())
                {
                    htmlSb.Append(content);
                    continue;
                }

                const string tag = "span";
                var type = token.Type;
                // TODO: maybe need check `token.Alias` null or not
                var classes = new[] { "token", type }.Concat(token.Alias);
                // TODO: support attributes
                var html = $"<{tag} class=\"{string.Join(" ", classes)}\">{content}</{tag}>";
                htmlSb.Append(html);
                continue;
            }

            if (token is not StreamToken streamToken) continue;

            htmlSb.Append(Stringify(streamToken.Content));
        }

        return htmlSb.ToString();
    }

    private static string Encode(string content)
    {
        return content.Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace("\u00a0", " ");
    }
}
