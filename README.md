# PrismSpark

A porting of [PrismJS](https://github.com/PrismJS/prism) to C# or .NET.

All supported language grammars are [here](https://github.com/tatwd/prism-sharp/tree/main/PrismSpark.Core/Languages).

## Getting Started

### Tokenize Code

Install `PrismSpark.Core` package:

```sh
dotnet add package PrismSpark.Core --prerelease
```

Then,

```csharp
using PrismSpark.Core;

var text = @"<p>Hello world!</p>";
var grammar = LanguageGrammars.Html; // or defined yourself
var tokens = Prism.Tokenize(text, grammar);
```

### Highlight Code

Install `PrismSpark.Highlighting.HTML` package:

```sh
dotnet add package PrismSpark.Highlighting.HTML --prerelease
```

Then,

```csharp
using PrismSpark.Core;
using PrismSpark.Highlighting.HTML;

var text = @"<p>Hello world!</p>";
var grammar = LanguageGrammars.Html; // or defined yourself
var highlighter = new HtmlHighlighter();
var html = highlighter.Highlight(text, grammar, "html");
```

_The css styles can customize yourself, or download from [PrismJS](https://prismjs.com/download.html)._

## .NET Web MVC Implementation Guide

This section provides comprehensive instructions for integrating PrismSpark into .NET Web MVC applications for server-side syntax highlighting.

### Package Installation for MVC Projects

1. Install the required packages in your MVC project:

```sh
dotnet add package PrismSpark.Core --prerelease
dotnet add package PrismSpark.Highlighting.HTML --prerelease
```

2. For dependency injection support, add to your `Program.cs` (or `Startup.cs` for older .NET versions):

```csharp
using PrismSpark.Core;
using PrismSpark.Highlighting.HTML;

// Register PrismSpark services
builder.Services.AddSingleton<IHtmlHighlighter, HtmlHighlighter>();
builder.Services.AddSingleton<IThemedHtmlHighlighter, ThemedHtmlHighlighter>();
```

### Controller Implementation

Create a service or use PrismSpark directly in your controllers:

#### Option 1: Direct Usage in Controller

```csharp
using Microsoft.AspNetCore.Mvc;
using PrismSpark.Core;
using PrismSpark.Highlighting.HTML;

public class CodeController : Controller
{
    private readonly IHtmlHighlighter _highlighter;
    
    public CodeController(IHtmlHighlighter highlighter)
    {
        _highlighter = highlighter;
    }
    
    public IActionResult Index()
    {
        var codeSnippets = new List<CodeSnippet>
        {
            new CodeSnippet 
            { 
                Language = "csharp",
                Code = @"public class Example 
                {
                    public string Name { get; set; }
                    public void Display() => Console.WriteLine($""Hello {Name}!"");
                }",
                Title = "C# Class Example"
            },
            new CodeSnippet 
            { 
                Language = "javascript",
                Code = @"function greet(name) {
                    return `Hello, ${name}!`;
                }
                
                const message = greet('World');
                console.log(message);",
                Title = "JavaScript Function Example"
            }
        };
        
        // Highlight each code snippet
        foreach (var snippet in codeSnippets)
        {
            var grammar = GetGrammarForLanguage(snippet.Language);
            snippet.HighlightedCode = _highlighter.Highlight(snippet.Code, grammar, snippet.Language);
        }
        
        return View(codeSnippets);
    }
    
    private Grammar GetGrammarForLanguage(string language)
    {
        return language.ToLower() switch
        {
            "csharp" or "cs" => LanguageGrammars.CSharp,
            "javascript" or "js" => LanguageGrammars.JavaScript,
            "html" => LanguageGrammars.Html,
            "css" => LanguageGrammars.Css,
            "python" => LanguageGrammars.Python,
            "java" => LanguageGrammars.Java,
            "sql" => LanguageGrammars.Sql,
            "json" => LanguageGrammars.Json,
            "xml" => LanguageGrammars.Xml,
            "yaml" => LanguageGrammars.Yaml,
            _ => LanguageGrammars.Markup // fallback
        };
    }
}

public class CodeSnippet
{
    public string Language { get; set; }
    public string Code { get; set; }
    public string HighlightedCode { get; set; }
    public string Title { get; set; }
}
```

#### Option 2: Code Highlighting Service

```csharp
// Services/ICodeHighlightingService.cs
public interface ICodeHighlightingService
{
    string HighlightCode(string code, string language);
    string HighlightCodeWithTheme(string code, string language, string theme = "default");
    Task<string> HighlightCodeAsync(string code, string language);
}

// Services/CodeHighlightingService.cs
using PrismSpark.Core;
using PrismSpark.Highlighting.HTML;

public class CodeHighlightingService : ICodeHighlightingService
{
    private readonly IHtmlHighlighter _htmlHighlighter;
    private readonly IThemedHtmlHighlighter _themedHighlighter;
    
    public CodeHighlightingService(IHtmlHighlighter htmlHighlighter, IThemedHtmlHighlighter themedHighlighter)
    {
        _htmlHighlighter = htmlHighlighter;
        _themedHighlighter = themedHighlighter;
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
            // Log error and return escaped plain text as fallback
            return System.Net.WebUtility.HtmlEncode(code);
        }
    }
    
    public string HighlightCodeWithTheme(string code, string language, string theme = "default")
    {
        try
        {
            var grammar = GetGrammarForLanguage(language);
            return _themedHighlighter.Highlight(code, grammar, language, theme);
        }
        catch (Exception ex)
        {
            // Log error and return escaped plain text as fallback
            return System.Net.WebUtility.HtmlEncode(code);
        }
    }
    
    public Task<string> HighlightCodeAsync(string code, string language)
    {
        return Task.Run(() => HighlightCode(code, language));
    }
    
    private Grammar GetGrammarForLanguage(string language)
    {
        return language?.ToLower() switch
        {
            "csharp" or "cs" or "c#" => LanguageGrammars.CSharp,
            "javascript" or "js" => LanguageGrammars.JavaScript,
            "typescript" or "ts" => LanguageGrammars.TypeScript,
            "html" or "htm" => LanguageGrammars.Html,
            "css" => LanguageGrammars.Css,
            "python" or "py" => LanguageGrammars.Python,
            "java" => LanguageGrammars.Java,
            "sql" => LanguageGrammars.Sql,
            "json" => LanguageGrammars.Json,
            "xml" => LanguageGrammars.Xml,
            "yaml" or "yml" => LanguageGrammars.Yaml,
            "markdown" or "md" => LanguageGrammars.Markdown,
            "bash" or "shell" => LanguageGrammars.Bash,
            "powershell" or "ps1" => LanguageGrammars.PowerShell,
            "php" => LanguageGrammars.Php,
            "ruby" or "rb" => LanguageGrammars.Ruby,
            "go" => LanguageGrammars.Go,
            "rust" or "rs" => LanguageGrammars.Rust,
            "cpp" or "c++" => LanguageGrammars.Cpp,
            "c" => LanguageGrammars.C,
            _ => LanguageGrammars.Markup // fallback for unknown languages
        };
    }
}

// Register in Program.cs
builder.Services.AddScoped<ICodeHighlightingService, CodeHighlightingService>();
```

### View Implementation with Razor

#### Basic View Implementation

```html
@model List<CodeSnippet>
@{
    ViewData["Title"] = "Code Examples";
}

<div class="container">
    <h1>@ViewData["Title"]</h1>
    
    @foreach (var snippet in Model)
    {
        <div class="code-example mb-4">
            <h3>@snippet.Title</h3>
            <div class="code-container">
                <pre class="language-@snippet.Language"><code class="language-@snippet.Language">@Html.Raw(snippet.HighlightedCode)</code></pre>
            </div>
        </div>
    }
</div>
```

#### Advanced View with Copy Button and Line Numbers

```html
@model List<CodeSnippet>
@{
    ViewData["Title"] = "Advanced Code Examples";
}

<div class="container">
    <h1>@ViewData["Title"]</h1>
    
    @foreach (var snippet in Model)
    {
        <div class="code-example mb-4">
            <div class="code-header d-flex justify-content-between align-items-center">
                <h3 class="mb-0">@snippet.Title</h3>
                <div class="code-actions">
                    <span class="language-badge badge badge-secondary">@snippet.Language.ToUpper()</span>
                    <button class="btn btn-sm btn-outline-primary copy-btn" 
                            data-clipboard-target="#code-@snippet.GetHashCode()">
                        <i class="fas fa-copy"></i> Copy
                    </button>
                </div>
            </div>
            <div class="code-container">
                <pre class="language-@snippet.Language line-numbers" data-line-offset="1"><code id="code-@snippet.GetHashCode()" class="language-@snippet.Language">@Html.Raw(snippet.HighlightedCode)</code></pre>
            </div>
        </div>
    }
</div>

@section Scripts {
    <script src="https://cdnjs.cloudflare.com/ajax/libs/clipboard.js/2.0.8/clipboard.min.js"></script>
    <script>
        // Initialize clipboard.js
        var clipboard = new ClipboardJS('.copy-btn');
        
        clipboard.on('success', function(e) {
            // Show success feedback
            var btn = e.trigger;
            var originalText = btn.innerHTML;
            btn.innerHTML = '<i class="fas fa-check"></i> Copied!';
            btn.classList.add('btn-success');
            btn.classList.remove('btn-outline-primary');
            
            setTimeout(function() {
                btn.innerHTML = originalText;
                btn.classList.remove('btn-success');
                btn.classList.add('btn-outline-primary');
            }, 2000);
            
            e.clearSelection();
        });
    </script>
}
```

### CSS Styling Setup and Configuration

#### Option 1: Using PrismJS CDN Themes

Add to your `_Layout.cshtml` in the `<head>` section:

```html
<!-- PrismJS CSS - Choose your preferred theme -->
<!-- Default theme -->
<link href="https://cdnjs.cloudflare.com/ajax/libs/prism/1.24.1/themes/prism.min.css" rel="stylesheet" />

<!-- Or use a dark theme -->
<!-- <link href="https://cdnjs.cloudflare.com/ajax/libs/prism/1.24.1/themes/prism-dark.min.css" rel="stylesheet" /> -->

<!-- Or use other popular themes -->
<!-- <link href="https://cdnjs.cloudflare.com/ajax/libs/prism/1.24.1/themes/prism-tomorrow.min.css" rel="stylesheet" /> -->
<!-- <link href="https://cdnjs.cloudflare.com/ajax/libs/prism/1.24.1/themes/prism-okaidia.min.css" rel="stylesheet" /> -->

<!-- Line numbers plugin CSS -->
<link href="https://cdnjs.cloudflare.com/ajax/libs/prism/1.24.1/plugins/line-numbers/prism-line-numbers.min.css" rel="stylesheet" />
```

#### Option 2: Custom CSS Styling

Create `wwwroot/css/prism-custom.css`:

```css
/* Custom PrismSpark Styling */
.code-example {
    border: 1px solid #e1e5e9;
    border-radius: 0.375rem;
    overflow: hidden;
    margin-bottom: 1.5rem;
    background: #fff;
    box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
}

.code-header {
    background: #f8f9fa;
    padding: 0.75rem 1rem;
    border-bottom: 1px solid #e1e5e9;
}

.code-header h3 {
    color: #495057;
    font-size: 1.1rem;
    font-weight: 600;
}

.language-badge {
    font-size: 0.75rem;
    text-transform: uppercase;
    letter-spacing: 0.5px;
    margin-right: 0.5rem;
}

.copy-btn {
    font-size: 0.8rem;
    padding: 0.25rem 0.5rem;
}

.code-container {
    background: #f8f9fa;
    overflow-x: auto;
}

/* Enhanced code block styling */
pre[class*="language-"] {
    margin: 0;
    padding: 1rem;
    background: #f8f9fa !important;
    font-family: 'Consolas', 'Monaco', 'Courier New', monospace;
    font-size: 0.875rem;
    line-height: 1.5;
    overflow-x: auto;
}

code[class*="language-"] {
    font-family: inherit;
    background: none;
    color: #333;
}

/* Token styling for better readability */
.token.comment,
.token.prolog,
.token.doctype,
.token.cdata {
    color: #6a737d;
    font-style: italic;
}

.token.punctuation {
    color: #24292e;
}

.token.property,
.token.tag,
.token.constant,
.token.symbol,
.token.deleted {
    color: #e36209;
}

.token.boolean,
.token.number {
    color: #005cc5;
}

.token.selector,
.token.attr-name,
.token.string,
.token.char,
.token.builtin,
.token.inserted {
    color: #032f62;
}

.token.operator,
.token.entity,
.token.url,
.language-css .token.string,
.style .token.string,
.token.variable {
    color: #e36209;
}

.token.atrule,
.token.attr-value,
.token.function,
.token.class-name {
    color: #6f42c1;
}

.token.keyword {
    color: #d73a49;
    font-weight: bold;
}

.token.regex,
.token.important {
    color: #e36209;
}

/* Line numbers styling */
.line-numbers .line-numbers-rows {
    border-right: 1px solid #e1e5e9;
    background: #f1f3f4;
    padding: 1rem 0.5rem;
    margin-right: 1rem;
}

.line-numbers-rows > span:before {
    color: #6a737d;
    font-size: 0.8rem;
}

/* Dark theme variant */
@media (prefers-color-scheme: dark) {
    .code-example {
        background: #0d1117;
        border-color: #30363d;
    }
    
    .code-header {
        background: #161b22;
        border-color: #30363d;
    }
    
    .code-header h3 {
        color: #c9d1d9;
    }
    
    pre[class*="language-"] {
        background: #0d1117 !important;
    }
    
    code[class*="language-"] {
        color: #c9d1d9;
    }
    
    .token.comment,
    .token.prolog,
    .token.doctype,
    .token.cdata {
        color: #8b949e;
    }
    
    .token.property,
    .token.tag,
    .token.constant,
    .token.symbol,
    .token.deleted {
        color: #ffa657;
    }
    
    .token.boolean,
    .token.number {
        color: #79c0ff;
    }
    
    .token.selector,
    .token.attr-name,
    .token.string,
    .token.char,
    .token.builtin,
    .token.inserted {
        color: #a5d6ff;
    }
    
    .token.keyword {
        color: #ff7b72;
    }
}

/* Responsive design */
@media (max-width: 768px) {
    .code-header {
        flex-direction: column;
        align-items: flex-start;
    }
    
    .code-actions {
        margin-top: 0.5rem;
    }
    
    pre[class*="language-"] {
        font-size: 0.8rem;
        padding: 0.75rem;
    }
}
```

Add to your `_Layout.cshtml`:

```html
<link rel="stylesheet" href="~/css/prism-custom.css" asp-append-version="true" />
```

### Complete Working Examples

#### Example 1: Blog Post with Code Snippets

```csharp
// Models/BlogPost.cs
public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<CodeSnippet> CodeSnippets { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

// Controllers/BlogController.cs
public class BlogController : Controller
{
    private readonly ICodeHighlightingService _codeService;
    
    public BlogController(ICodeHighlightingService codeService)
    {
        _codeService = codeService;
    }
    
    public IActionResult Post(int id)
    {
        var post = GetBlogPost(id); // Your data access logic
        
        // Highlight code snippets in the blog post
        foreach (var snippet in post.CodeSnippets)
        {
            snippet.HighlightedCode = _codeService.HighlightCode(snippet.Code, snippet.Language);
        }
        
        return View(post);
    }
}
```

#### Example 2: API Documentation with Interactive Examples

```csharp
// Controllers/ApiDocsController.cs
public class ApiDocsController : Controller
{
    private readonly ICodeHighlightingService _codeService;
    
    public ApiDocsController(ICodeHighlightingService codeService)
    {
        _codeService = codeService;
    }
    
    public IActionResult Index()
    {
        var examples = new List<ApiExample>
        {
            new ApiExample
            {
                Title = "Authentication",
                Description = "How to authenticate with our API",
                RequestExample = @"POST /api/auth/login
Content-Type: application/json

{
    ""username"": ""demo@example.com"",
    ""password"": ""securePassword123""
}",
                ResponseExample = @"{
    ""token"": ""eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."",
    ""expires"": ""2024-01-01T12:00:00Z"",
    ""user"": {
        ""id"": 123,
        ""name"": ""Demo User""
    }
}",
                CurlExample = @"curl -X POST https://api.example.com/auth/login \
  -H ""Content-Type: application/json"" \
  -d '{""username"":""demo@example.com"",""password"":""securePassword123""}'",
                CSharpExample = @"var client = new HttpClient();
var request = new
{
    username = ""demo@example.com"",
    password = ""securePassword123""
};

var response = await client.PostAsJsonAsync(""https://api.example.com/auth/login"", request);
var result = await response.Content.ReadFromJsonAsync<AuthResponse>();"
            }
        };
        
        // Highlight all code examples
        foreach (var example in examples)
        {
            example.HighlightedRequest = _codeService.HighlightCode(example.RequestExample, "http");
            example.HighlightedResponse = _codeService.HighlightCode(example.ResponseExample, "json");
            example.HighlightedCurl = _codeService.HighlightCode(example.CurlExample, "bash");
            example.HighlightedCSharp = _codeService.HighlightCode(example.CSharpExample, "csharp");
        }
        
        return View(examples);
    }
}

public class ApiExample
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string RequestExample { get; set; }
    public string ResponseExample { get; set; }
    public string CurlExample { get; set; }
    public string CSharpExample { get; set; }
    public string HighlightedRequest { get; set; }
    public string HighlightedResponse { get; set; }
    public string HighlightedCurl { get; set; }
    public string HighlightedCSharp { get; set; }
}
```

### Performance Considerations and Best Practices

#### 1. Caching Highlighted Code

```csharp
// Services/CachedCodeHighlightingService.cs
public class CachedCodeHighlightingService : ICodeHighlightingService
{
    private readonly ICodeHighlightingService _innerService;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachedCodeHighlightingService> _logger;
    
    public CachedCodeHighlightingService(
        ICodeHighlightingService innerService,
        IMemoryCache cache,
        ILogger<CachedCodeHighlightingService> logger)
    {
        _innerService = innerService;
        _cache = cache;
        _logger = logger;
    }
    
    public string HighlightCode(string code, string language)
    {
        var cacheKey = $"highlight_{language}_{code.GetHashCode()}";
        
        if (_cache.TryGetValue(cacheKey, out string cachedResult))
        {
            _logger.LogDebug("Cache hit for code highlighting: {Language}", language);
            return cachedResult;
        }
        
        var result = _innerService.HighlightCode(code, language);
        
        _cache.Set(cacheKey, result, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
            SlidingExpiration = TimeSpan.FromHours(6),
            Size = result.Length
        });
        
        _logger.LogDebug("Cache miss for code highlighting: {Language}", language);
        return result;
    }
    
    // Implement other interface methods similarly...
}

// Register in Program.cs
builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 100 * 1024 * 1024; // 100MB cache limit
});
builder.Services.AddScoped<CodeHighlightingService>();
builder.Services.Decorate<ICodeHighlightingService, CachedCodeHighlightingService>();
```

#### 2. Async Processing for Large Code Blocks

```csharp
public async Task<IActionResult> LargeCodeFile(int fileId)
{
    var codeFile = await GetCodeFileAsync(fileId);
    
    // Process large files asynchronously
    var highlightedCode = await _codeService.HighlightCodeAsync(codeFile.Content, codeFile.Language);
    
    var model = new CodeFileViewModel
    {
        FileName = codeFile.Name,
        Language = codeFile.Language,
        HighlightedContent = highlightedCode,
        LineCount = codeFile.Content.Split('\n').Length
    };
    
    return View(model);
}
```

#### 3. Configuration Options

```csharp
// appsettings.json
{
  "CodeHighlighting": {
    "EnableCaching": true,
    "CacheExpirationHours": 24,
    "MaxCodeLength": 100000,
    "DefaultTheme": "default",
    "SupportedLanguages": [
      "csharp", "javascript", "typescript", "html", "css", "sql",
      "python", "java", "json", "xml", "yaml", "markdown"
    ]
  }
}

// Configuration model
public class CodeHighlightingOptions
{
    public bool EnableCaching { get; set; } = true;
    public int CacheExpirationHours { get; set; } = 24;
    public int MaxCodeLength { get; set; } = 100000;
    public string DefaultTheme { get; set; } = "default";
    public List<string> SupportedLanguages { get; set; } = new();
}

// Register configuration
builder.Services.Configure<CodeHighlightingOptions>(
    builder.Configuration.GetSection("CodeHighlighting"));
```

### Troubleshooting Common Issues

#### Issue 1: Large Code Files Causing Performance Problems

**Solution**: Implement pagination or lazy loading for large files:

```csharp
public IActionResult ViewLargeFile(int fileId, int page = 1, int linesPerPage = 100)
{
    var codeFile = GetCodeFile(fileId);
    var lines = codeFile.Content.Split('\n');
    
    var startLine = (page - 1) * linesPerPage;
    var endLine = Math.Min(startLine + linesPerPage, lines.Length);
    var pageContent = string.Join('\n', lines.Skip(startLine).Take(linesPerPage));
    
    var highlightedContent = _codeService.HighlightCode(pageContent, codeFile.Language);
    
    var model = new PaginatedCodeViewModel
    {
        Content = highlightedContent,
        CurrentPage = page,
        TotalLines = lines.Length,
        LinesPerPage = linesPerPage,
        StartLineNumber = startLine + 1
    };
    
    return View(model);
}
```

#### Issue 2: Unknown Language Types

**Solution**: Implement fallback handling:

```csharp
private Grammar GetGrammarForLanguage(string language)
{
    var supportedLanguages = new Dictionary<string, Grammar>
    {
        ["csharp"] = LanguageGrammars.CSharp,
        ["javascript"] = LanguageGrammars.JavaScript,
        // ... other mappings
    };
    
    var normalizedLanguage = language?.ToLower().Trim();
    
    if (supportedLanguages.TryGetValue(normalizedLanguage, out var grammar))
    {
        return grammar;
    }
    
    // Try common aliases
    var aliases = new Dictionary<string, string>
    {
        ["cs"] = "csharp",
        ["js"] = "javascript",
        ["ts"] = "typescript",
        ["py"] = "python",
        // ... other aliases
    };
    
    if (aliases.TryGetValue(normalizedLanguage, out var aliasTarget) &&
        supportedLanguages.TryGetValue(aliasTarget, out var aliasGrammar))
    {
        return aliasGrammar;
    }
    
    // Fallback to markup for unknown languages
    return LanguageGrammars.Markup;
}
```

This comprehensive guide should help you successfully integrate PrismSpark into your .NET Web MVC applications with professional-grade syntax highlighting capabilities.

## References

This project is based on [Prism.js](https://github.com/PrismJS/prism) - the lightweight, extensible syntax highlighter built with modern web standards in mind.

- **Official Repository**: <https://github.com/PrismJS/prism>
- **Documentation**: <https://prismjs.com/>
- **Language Definitions**: Refer to the [components directory](https://github.com/PrismJS/prism/tree/master/components) in the official repository for language grammar implementations and tokenization rules
- **Themes**: Download CSS themes from the [official download page](https://prismjs.com/download.html) for styling highlighted code

All language grammars and tokenization logic in PrismSpark are ported from the original JavaScript implementations to maintain consistency and compatibility.
