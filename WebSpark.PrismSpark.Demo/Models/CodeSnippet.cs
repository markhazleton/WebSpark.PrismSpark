namespace WebSpark.PrismSpark.Demo.Models;

public class CodeSnippet
{
    public string Code { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string HighlightedCode { get; set; } = string.Empty;
    public bool IsValid { get; set; } = true;
    public string ValidationMessage { get; set; } = string.Empty;
}

public class CodeValidationRequest
{
    public string Code { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
}

public class CodeValidationResponse
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
    public string HighlightedCode { get; set; } = string.Empty;
    public string FormattedCode { get; set; } = string.Empty;
}
