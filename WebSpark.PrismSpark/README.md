# PrismSpark.Core

A comprehensive C#/.NET port of [PrismJS](https://github.com/PrismJS/prism) that provides both tokenization and syntax highlighting capabilities. Transform source code into tokens or generate HTML markup with CSS classes for beautiful syntax highlighting.

[![NuGet Version](https://img.shields.io/nuget/v/PrismSpark.Core?label=NuGet)](https://www.nuget.org/packages/PrismSpark.Core)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

## Overview

PrismSpark.Core is a complete syntax highlighting solution that:

- **Tokenizes source code** using grammar definitions from PrismJS
- **Generates highlighted HTML** with semantic CSS classes
- **Supports 20+ programming languages** with extensible grammar system
- **Provides multiple highlighting modes** from basic to enhanced with line numbers, themes, and custom styling

## Features

- **🎨 Language-agnostic highlighting**: Supports 20+ programming languages with defined grammars
- **🏷️ Semantic HTML output**: Generates clean HTML with meaningful CSS classes
- **🔗 Nested token support**: Handles complex syntax structures with nested tokens
- **🛡️ HTML entity encoding**: Safely encodes special characters for web display
- **⚡ High performance**: Efficient tokenization and HTML generation
- **🔧 Extensible**: Easy to add new languages and customize output
- **📏 Line numbers**: Optional line number display
- **🎯 Line highlighting**: Highlight specific lines
- **🎨 Theming support**: Built-in theme system

## Installation

### Package Manager

```powershell
Install-Package PrismSpark.Core
```

### .NET CLI

```bash
dotnet add package PrismSpark.Core
```

### PackageReference

```xml
<PackageReference Include="PrismSpark.Core" Version="0.1.4" />
```

## Quick Start

### Basic Tokenization

```csharp
using PrismSpark.Core;

var text = @"<p>Hello world!</p>";
var grammar = LanguageGrammars.Markup; // HTML/XML grammar
var tokens = Prism.Tokenize(text, grammar);
```

### HTML Highlighting

```csharp
using PrismSpark.Core;
using PrismSpark.Core.Highlighting;

var text = @"<p>Hello world!</p>";
var grammar = LanguageGrammars.Markup;
var highlighter = new HtmlHighlighter();
var html = highlighter.Highlight(text, grammar, "html");

Console.WriteLine(html);
// Output: <span class="token tag">...</span>Hello world!<span class="token tag">...</span>
```

## Supported Languages

The library supports the following programming languages and formats:

| Language | Aliases | Language | Aliases |
|----------|---------|----------|---------|
| **ASP.NET** | `aspnet`, `aspx` | **Bash** | `bash`, `shell` |
| **Batch** | `batch`, `cmd` | **C** | `c` |
| **C#** | `csharp`, `cs`, `dotnet` | **C++** | `cpp`, `c++` |
| **CIL** | `cil` | **CSS** | `css` |
| **Go** | `go` | **HTML/XML** | `markup`, `html`, `mathml`, `svg`, `xml`, `atom`, `rss` |
| **Java** | `java` | **JavaScript** | `javascript`, `js` |
| **JSON** | `json`, `web-manifest` | **Lua** | `lua` |
| **PowerShell** | `powershell`, `ps1` | **Python** | `python`, `py` |
| **Razor** | `cshtml`, `razor` | **RegExp** | `regexp`, `regex` |
| **Rust** | `rust` | **SQL** | `sql` |
| **YAML** | `yaml`, `yml` | | |

All supported language grammars are available in the [Languages directory](https://github.com/tatwd/prism-sharp/tree/main/PrismSpark.Core/Languages).

## Usage Examples

### Basic HTML Highlighting

```csharp
using PrismSpark.Core;
using PrismSpark.Core.Highlighting;

var htmlCode = @"<div class=""container"">
    <h1>Welcome</h1>
    <p>Hello, <strong>world</strong>!</p>
</div>";

var highlighter = new HtmlHighlighter();
var result = highlighter.Highlight(htmlCode, LanguageGrammars.Markup, "html");
```

### C# Code Highlighting

```csharp
var csharpCode = @"
public class Example 
{
    public string Name { get; set; } = ""Hello"";
    
    public void Greet()
    {
        Console.WriteLine($""Hello, {Name}!"");
    }
} "";

var result = highlighter.Highlight(csharpCode, LanguageGrammars.CSharp, "csharp");
```

### Enhanced Highlighting with Line Numbers

```csharp
using PrismSpark.Core.Highlighting;

var enhancedHighlighter = new EnhancedHtmlHighlighter();
var options = new HighlightOptions
{
    ShowLineNumbers = true,
    HighlightedLines = new[] { 2, 4 }, // Highlight lines 2 and 4
    ClassPrefix = "prism-"
};

var result = enhancedHighlighter.Highlight(csharpCode, LanguageGrammars.CSharp, "csharp", options);
```

### Themed Highlighting

```csharp
using PrismSpark.Core.Highlighting;
using PrismSpark.Core.Themes;

var themedHighlighter = new ThemedHtmlHighlighter();
var result = themedHighlighter.Highlight(
    csharpCode, 
    LanguageGrammars.CSharp, 
    "csharp", 
    PrismThemes.Tomorrow
);
```

### JavaScript Highlighting

```csharp
var jsCode = @"
function greet(name) {
    return `Hello, ${name}!`;
}

const message = greet('World');
console.log(message);";

var result = highlighter.Highlight(jsCode, LanguageGrammars.JavaScript, "javascript");
```

### Python Code Highlighting

```csharp
var pythonCode = @"
def fibonacci(n):
    if n <= 1:
        return n
    return fibonacci(n-1) + fibonacci(n-2)

# Generate first 10 fibonacci numbers
for i in range(10):
    print(f""F({i}) = {fibonacci(i)}"")";

var result = highlighter.Highlight(pythonCode, LanguageGrammars.Python, "python");
```

## Highlighter Types

### HtmlHighlighter

Basic HTML highlighting with clean output:

```csharp
var highlighter = new HtmlHighlighter();
var html = highlighter.Highlight(code, grammar, language);
```

### EnhancedHtmlHighlighter

Advanced highlighting with additional features:

```csharp
var highlighter = new EnhancedHtmlHighlighter();
var options = new HighlightOptions
{
    ShowLineNumbers = true,
    HighlightedLines = new[] { 1, 3, 5 },
    ClassPrefix = "custom-",
    ClassMappings = new Dictionary<string, string>
    {
        ["keyword"] = "kw",
        ["string"] = "str"
    }
};
var html = highlighter.Highlight(code, grammar, language, options);
```

### ThemedHtmlHighlighter

Built-in theme support:

```csharp
var highlighter = new ThemedHtmlHighlighter();
var html = highlighter.Highlight(code, grammar, language, PrismThemes.Dark);
```

## Output Structure

The highlighter generates HTML with the following structure:

```html
<span class="token {type} {alias1} {alias2}">content</span>
```

### CSS Classes

- `token`: Base class for all syntax elements
- `{type}`: The token type (e.g., `keyword`, `string`, `comment`, `punctuation`)
- `{alias}`: Additional aliases for more specific styling

### Common Token Types

| Token Type | Description | Example Elements |
|------------|-------------|------------------|
| `keyword` | Language keywords | `function`, `class`, `if`, `return` |
| `string` | String literals | `"hello"`, `'world'`, `` `template` `` |
| `comment` | Code comments | `// comment`, `/* block */` |
| `punctuation` | Syntax punctuation | `{`, `}`, `;`, `.`, `()` |
| `number` | Numeric literals | `123`, `3.14`, `0xFF` |
| `operator` | Operators | `+`, `-`, `===`, `&&` |
| `function` | Function names | `console.log`, `parseInt` |
| `class-name` | Class/type names | `String`, `MyClass` |
| `directive` | Preprocessor directives | `#include`, `#define` |
| `entity` | HTML entities | `&amp;`, `&#65;` |
| `tag` | HTML/XML tags | `<div>`, `</span>` |

### Example Output

For this C# code:

```csharp
public string Name = "Hello";
```

The output would be:

```html
<span class="token keyword">public</span> <span class="token class-name">string</span> Name <span class="token operator">=</span> <span class="token string">"Hello"</span><span class="token punctuation">;</span>
```

## Styling

The library generates HTML structure with CSS classes. You need to provide CSS styles for visual appearance.

### Option 1: PrismJS CSS Themes

Use existing PrismJS CSS themes:

```html
<!-- Default theme -->
<link href="https://cdnjs.cloudflare.com/ajax/libs/prism/1.29.0/themes/prism.min.css" rel="stylesheet" />

<!-- Dark theme -->
<link href="https://cdnjs.cloudflare.com/ajax/libs/prism/1.29.0/themes/prism-dark.min.css" rel="stylesheet" />

<!-- Line numbers plugin CSS -->
<link href="https://cdnjs.cloudflare.com/ajax/libs/prism/1.29.0/plugins/line-numbers/prism-line-numbers.min.css" rel="stylesheet" />
```

### Option 2: Custom CSS

Create your own styles:

```css
/* Base token styling */
.token.comment { color: #6a737d; font-style: italic; }
.token.keyword { color: #d73a49; font-weight: bold; }
.token.string { color: #032f62; }
.token.number { color: #005cc5; }
.token.operator { color: #d73a49; }
.token.punctuation { color: #24292e; }
.token.function { color: #6f42c1; }
.token.class-name { color: #6f42c1; }

/* Line numbers */
.line-numbers { counter-reset: linenumber; }
.line-numbers .line-number::before {
    counter-increment: linenumber;
    content: counter(linenumber);
}
```

## .NET Web MVC Integration

### Package Installation for MVC Projects

```csharp
// Program.cs - Register services
using PrismSpark.Core;
using PrismSpark.Core.Highlighting;

builder.Services.AddSingleton<IHighlighter, HtmlHighlighter>();
builder.Services.AddSingleton<EnhancedHtmlHighlighter>();
builder.Services.AddSingleton<ThemedHtmlHighlighter>();
```

### Controller Implementation

```csharp
using Microsoft.AspNetCore.Mvc;
using PrismSpark.Core;
using PrismSpark.Core.Highlighting;

public class CodeController : Controller
{
    private readonly IHighlighter _highlighter;

    public CodeController(IHighlighter highlighter)
    {
        _highlighter = highlighter;
    }

    public IActionResult Index()
    {
        var codeExamples = new List<CodeSnippet>
        {
            new() { Code = "console.log('Hello World!');", Language = "javascript", Title = "JavaScript Example" },
            new() { Code = "public class Hello { }", Language = "csharp", Title = "C# Example" }
        };

        var highlightedExamples = codeExamples.Select(example => new CodeSnippet
        {
            Code = _highlighter.Highlight(example.Code, GetGrammar(example.Language), example.Language),
            Language = example.Language,
            Title = example.Title
        }).ToList();

        return View(highlightedExamples);
    }

    private static Grammar GetGrammar(string language) => language switch
    {
        "javascript" => LanguageGrammars.JavaScript,
        "csharp" => LanguageGrammars.CSharp,
        "html" => LanguageGrammars.Markup,
        "css" => LanguageGrammars.Css,
        "python" => LanguageGrammars.Python,
        _ => LanguageGrammars.Markup
    };
}

public class CodeSnippet
{
    public string Code { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}
```

### Razor View Implementation

```html
@model List<CodeSnippet>
@{
    ViewData["Title"] = "Code Examples";
}

<div class="container">
    <h1>@ViewData["Title"]</h1>
    
    @foreach (var example in Model)
    {
        <div class="card mb-4">
            <div class="card-header d-flex justify-content-between align-items-center">
                <h3 class="mb-0">@example.Title</h3>
                <span class="badge badge-primary">@example.Language</span>
            </div>
            <div class="card-body p-0">
                <pre class="language-@example.Language line-numbers"><code>@Html.Raw(example.Code)</code></pre>
            </div>
        </div>
    }
</div>
```

## Advanced Features

### Custom Grammar Definition

```csharp
var customGrammar = new Grammar();
customGrammar.Tokens["keyword"] = new GrammarToken
{
    Pattern = @"\b(?:function|var|let|const)\b",
    Alias = new[] { "reserved" }
};

var tokens = Prism.Tokenize("function test() {}", customGrammar);
```

### Plugin System

```csharp
// Custom plugin implementation
public class CustomPlugin : IPlugin
{
    public void Process(HighlightContext context)
    {
        // Custom processing logic
    }
}

// Register and use plugins
var highlighter = new EnhancedHtmlHighlighter();
PluginManager.RegisterPlugin(new CustomPlugin());
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- [PrismJS](https://github.com/PrismJS/prism) - The original JavaScript library that inspired this port
- All the contributors who have helped improve this project
