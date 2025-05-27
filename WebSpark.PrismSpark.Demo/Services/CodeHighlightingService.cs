using WebSpark.PrismSpark;
using WebSpark.PrismSpark.Highlighting;

namespace WebSpark.PrismSpark.Demo.Services;

public interface ICodeHighlightingService
{
    string HighlightCode(string code, string language);
    string HighlightCodeWithTheme(string code, string language, string theme = "prism");
    Task<string> HighlightCodeAsync(string code, string language);
    string ValidateAndFormatCode(string code, string language);
}

public class CodeHighlightingService : ICodeHighlightingService
{
    private readonly IHighlighter _htmlHighlighter;
    private readonly ThemedHtmlHighlighter _themedHighlighter;
    private readonly ILogger<CodeHighlightingService> _logger;

    public CodeHighlightingService(
        IHighlighter htmlHighlighter,
        ThemedHtmlHighlighter themedHighlighter,
        ILogger<CodeHighlightingService> logger)
    {
        _htmlHighlighter = htmlHighlighter;
        _themedHighlighter = themedHighlighter;
        _logger = logger;
    }

    public string HighlightCode(string code, string language)
    {
        try
        {
            var grammar = GetGrammarForLanguage(language);
            return _htmlHighlighter.Highlight(code, grammar, language);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during code highlighting for language: {Language}", language);
            return System.Net.WebUtility.HtmlEncode(code);
        }
    }

    public string HighlightCodeWithTheme(string code, string language, string theme = "prism")
    {
        try
        {
            return _themedHighlighter.Highlight(code, language);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during themed code highlighting for language: {Language}", language);
            return System.Net.WebUtility.HtmlEncode(code);
        }
    }

    public Task<string> HighlightCodeAsync(string code, string language)
    {
        return Task.Run(() => HighlightCode(code, language));
    }

    public string ValidateAndFormatCode(string code, string language)
    {
        try
        {
            // Basic validation and formatting
            switch (language?.ToLower())
            {
                case "json":
                    return ValidateAndFormatJson(code);
                case "javascript":
                case "js":
                    return ValidateJavaScript(code);
                case "csharp":
                case "cs":
                    return ValidateCSharp(code);
                default:
                    return HighlightCode(code, language ?? "markup");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during code validation for language: {Language}", language);
            return System.Net.WebUtility.HtmlEncode(code);
        }
    }

    private string ValidateAndFormatJson(string jsonCode)
    {
        try
        {
            // Try to parse and reformat JSON
            var parsed = System.Text.Json.JsonDocument.Parse(jsonCode);
            var formatted = System.Text.Json.JsonSerializer.Serialize(parsed, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            return HighlightCode(formatted, "json");
        }
        catch
        {
            // If parsing fails, return original with highlighting
            return HighlightCode(jsonCode, "json");
        }
    }

    private string ValidateJavaScript(string jsCode)
    {
        // Basic JavaScript validation (could be enhanced with actual JS parser)
        var lines = jsCode.Split('\n');

        // Simple bracket matching
        var openBrackets = jsCode.Count(c => c == '{');
        var closeBrackets = jsCode.Count(c => c == '}');

        return HighlightCode(jsCode, "javascript");
    }

    private string ValidateCSharp(string csharpCode)
    {
        // Basic C# validation
        return HighlightCode(csharpCode, "csharp");
    }

    private Grammar GetGrammarForLanguage(string language)
    {
        return language?.ToLower() switch
        {
            "csharp" or "cs" or "c#" => LanguageGrammars.CSharp,
            "javascript" or "js" => LanguageGrammars.JavaScript,
            "json" => LanguageGrammars.Json,
            "html" or "markup" => LanguageGrammars.Markup,
            "css" => LanguageGrammars.Css,
            "python" or "py" => LanguageGrammars.Python,
            "sql" => LanguageGrammars.Sql,
            "xml" => LanguageGrammars.Markup, // XML is handled by Markup grammar
            "yaml" or "yml" => LanguageGrammars.Yaml,
            "bash" or "shell" => LanguageGrammars.Bash,
            "powershell" or "ps1" => LanguageGrammars.PowerShell,
            _ => LanguageGrammars.Markup // Default fallback
        };
    }
}
