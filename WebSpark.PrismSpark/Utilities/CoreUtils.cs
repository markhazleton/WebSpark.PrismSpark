using System.Text;
using System.Text.RegularExpressions;

namespace WebSpark.PrismSpark.Utilities;

/// <summary>
/// String manipulation utilities for PrismSpark
/// </summary>
public static class StringUtils
{
    /// <summary>
    /// Escape HTML characters in a string
    /// </summary>
    public static string EscapeHtml(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        return input
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&#39;");
    }

    /// <summary>
    /// Unescape HTML characters in a string
    /// </summary>
    public static string UnescapeHtml(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        return input
            .Replace("&amp;", "&")
            .Replace("&lt;", "<")
            .Replace("&gt;", ">")
            .Replace("&quot;", "\"")
            .Replace("&#39;", "'");
    }

    /// <summary>
    /// Normalize whitespace in code
    /// </summary>
    public static string NormalizeWhitespace(string code, bool trimLeft = true, bool trimRight = true, int? tabSize = null)
    {
        if (string.IsNullOrEmpty(code)) return code;

        var lines = code.Split('\n');

        // Convert tabs to spaces if tabSize is specified
        if (tabSize.HasValue)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("\t", new string(' ', tabSize.Value));
            }
        }

        // Find minimum indentation (excluding empty lines)
        var minIndent = int.MaxValue;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var indent = 0;
            foreach (var ch in line)
            {
                if (ch == ' ') indent++;
                else if (ch == '\t') indent += tabSize ?? 4;
                else break;
            }
            minIndent = Math.Min(minIndent, indent);
        }

        // Remove common indentation
        if (minIndent > 0 && minIndent != int.MaxValue)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                var removed = 0;
                var newLine = string.Empty;
                foreach (var ch in lines[i])
                {
                    if (removed >= minIndent)
                    {
                        newLine += ch;
                    }
                    else if (ch == ' ')
                    {
                        removed++;
                    }
                    else if (ch == '\t')
                    {
                        removed += tabSize ?? 4;
                    }
                    else
                    {
                        newLine += ch;
                        break;
                    }
                }
                if (newLine.Length > 0)
                {
                    lines[i] = newLine + lines[i].Substring(Math.Min(lines[i].Length, removed));
                }
            }
        }

        var result = string.Join('\n', lines);

        // Trim left and right if requested
        if (trimLeft && trimRight)
            result = result.Trim();
        else if (trimLeft)
            result = result.TrimStart();
        else if (trimRight)
            result = result.TrimEnd();

        return result;
    }

    /// <summary>
    /// Generate a unique ID
    /// </summary>
    public static string GenerateId(string prefix = "prism")
    {
        return $"{prefix}-{Guid.NewGuid():N}";
    }

    /// <summary>
    /// Convert camelCase to kebab-case
    /// </summary>
    public static string ToKebabCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        return Regex.Replace(input, "([a-z])([A-Z])", "$1-$2").ToLower();
    }

    /// <summary>
    /// Convert kebab-case to camelCase
    /// </summary>
    public static string ToCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        var parts = input.Split('-');
        if (parts.Length == 1) return input;

        var result = parts[0].ToLower();
        for (int i = 1; i < parts.Length; i++)
        {
            if (parts[i].Length > 0)
            {
                result += char.ToUpper(parts[i][0]) + parts[i][1..].ToLower();
            }
        }
        return result;
    }

    /// <summary>
    /// Pluralize a word
    /// </summary>
    public static string Pluralize(string word, int count)
    {
        if (count == 1) return word;

        if (word.EndsWith("y") && !IsVowel(word[^2]))
            return word[..^1] + "ies";
        if (word.EndsWith("s") || word.EndsWith("sh") || word.EndsWith("ch") || word.EndsWith("x") || word.EndsWith("z"))
            return word + "es";

        return word + "s";
    }

    /// <summary>
    /// Truncate a string to a maximum length
    /// </summary>
    /// <param name="text">Text to truncate</param>
    /// <param name="maxLength">Maximum length</param>
    /// <param name="ellipsis">Ellipsis to append if truncated</param>
    /// <returns>Truncated string</returns>
    public static string Truncate(string text, int maxLength, string ellipsis = "...")
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
            return text;

        return text.Substring(0, maxLength - ellipsis.Length) + ellipsis;
    }

    private static bool IsVowel(char c) => "aeiouAEIOU".Contains(c);
}

/// <summary>
/// Grammar manipulation utilities
/// </summary>
public static class GrammarUtils
{
    /// <summary>
    /// Clone a grammar with deep copy
    /// </summary>
    public static Grammar CloneGrammar(Grammar original)
    {
        var clone = new Grammar();

        foreach (var kvp in original)
        {
            clone[kvp.Key] = CloneGrammarTokens(kvp.Value);
        }

        if (original.Reset != null)
        {
            clone.Reset = CloneGrammar(original.Reset);
        }

        return clone;
    }

    /// <summary>
    /// Clone an array of grammar tokens
    /// </summary>
    public static GrammarToken[] CloneGrammarTokens(GrammarToken[] original)
    {
        var cloned = new GrammarToken[original.Length];
        for (int i = 0; i < original.Length; i++)
        {
            cloned[i] = CloneGrammarToken(original[i]);
        }
        return cloned;
    }

    /// <summary>
    /// Clone a grammar token
    /// </summary>
    public static GrammarToken CloneGrammarToken(GrammarToken original)
    {
        return new GrammarToken(
            original.Pattern,
            original.Lookbehind,
            original.Greedy,
            original.Alias.Length > 0 ? (string[])original.Alias.Clone() : null,
            original.Inside != null ? CloneGrammar(original.Inside) : null
        );
    }

    /// <summary>
    /// Extend a grammar with additional tokens
    /// </summary>
    public static Grammar ExtendGrammar(Grammar baseGrammar, Grammar extension)
    {
        var extended = CloneGrammar(baseGrammar);

        foreach (var kvp in extension)
        {
            extended[kvp.Key] = CloneGrammarTokens(kvp.Value);
        }

        return extended;
    }

    /// <summary>
    /// Insert a token before another token in grammar
    /// </summary>
    public static void InsertBefore(Grammar grammar, string beforeKey, string newKey, GrammarToken[] newTokens)
    {
        var tempGrammar = new Grammar();
        tempGrammar[newKey] = newTokens;
        grammar.InsertBefore(beforeKey, tempGrammar);
    }

    /// <summary>
    /// Get all nested grammars from a grammar
    /// </summary>
    public static IEnumerable<Grammar> GetNestedGrammars(Grammar grammar)
    {
        var grammars = new List<Grammar> { grammar };

        foreach (var kvp in grammar)
        {
            foreach (var token in kvp.Value)
            {
                if (token.Inside != null)
                {
                    grammars.AddRange(GetNestedGrammars(token.Inside));
                }
            }
        }

        return grammars;
    }
}

/// <summary>
/// Token manipulation utilities
/// </summary>
public static class TokenUtils
{
    /// <summary>
    /// Convert tokens to a flat string array
    /// </summary>
    public static string[] TokensToStringArray(Token[] tokens)
    {
        var result = new List<string>();

        foreach (var token in tokens)
        {
            if (token is StringToken stringToken)
            {
                result.Add(stringToken.Content);
            }
            else if (token is StreamToken streamToken)
            {
                result.AddRange(TokensToStringArray(streamToken.Content));
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Get all tokens of a specific type
    /// </summary>
    public static IEnumerable<Token> GetTokensByType(Token[] tokens, string type)
    {
        foreach (var token in tokens)
        {
            if (token.Type == type)
            {
                yield return token;
            }

            if (token is StreamToken streamToken)
            {
                foreach (var nestedToken in GetTokensByType(streamToken.Content, type))
                {
                    yield return nestedToken;
                }
            }
        }
    }

    /// <summary>
    /// Count tokens of a specific type
    /// </summary>
    public static int CountTokensByType(Token[] tokens, string type)
    {
        return GetTokensByType(tokens, type).Count();
    }

    /// <summary>
    /// Replace all tokens of a specific type
    /// </summary>
    public static Token[] ReplaceTokensByType(Token[] tokens, string type, Func<Token, Token> replacer)
    {
        var result = new List<Token>();

        foreach (var token in tokens)
        {
            if (token.Type == type)
            {
                result.Add(replacer(token));
            }
            else if (token is StreamToken streamToken)
            {
                var replacedContent = ReplaceTokensByType(streamToken.Content, type, replacer);
                result.Add(new StreamToken(replacedContent, streamToken.Type, streamToken.Alias));
            }
            else
            {
                result.Add(token);
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Flatten nested tokens into a single array
    /// </summary>
    public static Token[] FlattenTokens(Token[] tokens)
    {
        var result = new List<Token>();

        foreach (var token in tokens)
        {
            if (token is StreamToken streamToken)
            {
                result.AddRange(FlattenTokens(streamToken.Content));
            }
            else
            {
                result.Add(token);
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Get the text content of all tokens combined
    /// </summary>
    public static string GetTokenText(Token[] tokens)
    {
        var text = new StringBuilder();

        foreach (var token in tokens)
        {
            if (token is StringToken stringToken)
            {
                text.Append(stringToken.Content);
            }
            else if (token is StreamToken streamToken)
            {
                text.Append(GetTokenText(streamToken.Content));
            }
        }

        return text.ToString();
    }

    /// <summary>
    /// Wrap tokens with additional information
    /// </summary>
    public static StreamToken WrapTokens(Token[] tokens, string? type, string[]? alias = null)
    {
        return new StreamToken(tokens, type, alias);
    }
}

/// <summary>
/// Code analysis utilities
/// </summary>
public static class CodeUtils
{
    /// <summary>
    /// Count lines in code
    /// </summary>
    public static int CountLines(string code)
    {
        if (string.IsNullOrEmpty(code)) return 0;
        return code.Count(c => c == '\n') + 1;
    }

    /// <summary>
    /// Get line at specific index
    /// </summary>
    public static string GetLine(string code, int lineNumber)
    {
        if (string.IsNullOrEmpty(code)) return string.Empty;

        var lines = code.Split('\n');
        return lineNumber >= 0 && lineNumber < lines.Length ? lines[lineNumber] : string.Empty;
    }

    /// <summary>
    /// Get lines in a range
    /// </summary>
    public static string[] GetLines(string code, int startLine, int endLine)
    {
        if (string.IsNullOrEmpty(code)) return Array.Empty<string>();

        var lines = code.Split('\n');
        var start = Math.Max(0, startLine);
        var end = Math.Min(lines.Length - 1, endLine);

        if (start > end) return Array.Empty<string>();

        var result = new string[end - start + 1];
        Array.Copy(lines, start, result, 0, result.Length);
        return result;
    }

    /// <summary>
    /// Calculate indentation level
    /// </summary>
    public static int GetIndentationLevel(string line, int tabSize = 4)
    {
        if (string.IsNullOrEmpty(line)) return 0;

        var indent = 0;
        foreach (var ch in line)
        {
            if (ch == ' ') indent++;
            else if (ch == '\t') indent += tabSize;
            else break;
        }
        return indent;
    }

    /// <summary>
    /// Detect the primary programming language from code content
    /// </summary>
    public static string DetectLanguage(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) return "text";

        // Simple heuristics based on common patterns
        if (code.Contains("using System") || code.Contains("namespace ") || code.Contains("class ") && code.Contains("{"))
            return "csharp";
        if (code.Contains("function ") || code.Contains("const ") || code.Contains("let ") || code.Contains("var "))
            return "javascript";
        if (code.Contains("def ") || code.Contains("import ") || code.Contains("print("))
            return "python";
        if (code.Contains("public class") || code.Contains("private ") || code.Contains("@Override"))
            return "java";
        if (code.Contains("#include") || code.Contains("int main("))
            return "c";
        if (code.Contains("std::") || code.Contains("#include <iostream>"))
            return "cpp";
        if (code.Contains("<!DOCTYPE") || code.Contains("<html") || code.Contains("<div"))
            return "html";
        if (code.Contains("{") && (code.Contains("color:") || code.Contains("margin:") || code.Contains("padding:")))
            return "css";
        if (code.Contains("SELECT ") || code.Contains("FROM ") || code.Contains("WHERE "))
            return "sql";

        return "text";
    }
}
