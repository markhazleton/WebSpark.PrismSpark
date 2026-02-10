# PrismSpark

A modern, high-performance C#/.NET port of [PrismJS](https://github.com/PrismJS/prism) for advanced syntax highlighting, theming, and extensibility.

[![.NET](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/download)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

---

## Features

- **Tokenization & Highlighting** for 24 languages (C#, JavaScript, Python, Markdown, Pug, and more)
- **Plugin System**: Line numbers, copy-to-clipboard, toolbar, and more
- **Theme System**: Built-in and custom themes, CSS generation
- **Hooks & Extensibility**: Event-driven customization
- **Advanced Options**: Line highlighting, custom CSS classes, context/metadata
- **Performance**: Async processing, caching, and efficient rendering
- **.NET 10.0 LTS Support**: Fully compatible with the latest .NET Long Term Support release
- **Comprehensive Test Suite**: 52 MSTest tests covering grammars, highlighting, tokenization, and integration

---

## Requirements

- **.NET 10.0** or later (Long Term Support)
- Compatible with .NET 9.0 and earlier versions

---

## Getting Started

### Installation

```pwsh
# Install core library
Install-Package WebSpark.PrismSpark
# Install HTML highlighting support
Install-Package PrismSpark.Highlighting.HTML
```

### Basic Usage

```csharp
using WebSpark.PrismSpark;
using WebSpark.PrismSpark.Highlighting;

var code = @"<p>Hello world!</p>";
var grammar = LanguageGrammars.Html;
var highlighter = new HtmlHighlighter();
var html = highlighter.Highlight(code, grammar, "html");
```

---

## .NET Web MVC Integration

### Service Registration

```csharp
// Program.cs
builder.Services.AddSingleton<IHighlighter, HtmlHighlighter>();
builder.Services.AddSingleton<EnhancedHtmlHighlighter>();
builder.Services.AddSingleton<ThemedHtmlHighlighter>();
```

### Controller Example

```csharp
public class CodeController : Controller
{
    private readonly IHighlighter _highlighter;
    public CodeController(IHighlighter highlighter) => _highlighter = highlighter;
    public IActionResult Index()
    {
        var code = "public class Hello { }";
        var html = _highlighter.Highlight(code, LanguageGrammars.CSharp, "csharp");
        return View((object)html);
    }
}
```

### Razor View Example

```html
@model string
<pre class="language-csharp line-numbers"><code>@Html.Raw(Model)</code></pre>
```

---

## Advanced Usage

### Enhanced Highlighting

```csharp
var options = new HighlightOptions
{
    ShowLineNumbers = true,
    HighlightedLines = new[] { 2, 4 },
    ClassPrefix = "prism-"
};
var html = new EnhancedHtmlHighlighter().Highlight(code, LanguageGrammars.CSharp, "csharp", options);
```

### Themed Highlighting & CSS

```csharp
var themed = new ThemedHtmlHighlighter("dark");
var html = themed.Highlight(code, LanguageGrammars.Python, "python");
var css = CssGenerator.GenerateThemeCss("dark");
```

### Plugin System

```csharp
public class MyPlugin : IPlugin
{
    public void Process(HighlightContext ctx) { /* ... */ }
}
PluginManager.RegisterPlugin(new MyPlugin());
```

### Hooks

```csharp
Hooks.Add(HookNames.AfterHighlight, args => { /* post-processing */ });
```

### Custom Theme

```csharp
var theme = new Theme
{
    Name = "my-theme",
    Background = new ThemeStyle { BackgroundColor = "#222" },
    Foreground = new ThemeStyle { Color = "#eee" },
    TokenStyles = new Dictionary<string, ThemeStyle>
    {
        ["keyword"] = new() { Color = "#ff0", FontWeight = "bold" },
        ["string"] = new() { Color = "#0f0" }
    }
};
ThemeManager.RegisterTheme(theme);
```

---

## Supported Languages (24 Grammars)

| Language     | Aliases                              |
|--------------|--------------------------------------|
| C            | c                                    |
| C-like       | clike                                |
| C#           | csharp, cs, dotnet                   |
| C++          | cpp, c++                             |
| ASP.NET      | aspnet, aspx                         |
| Razor        | cshtml, razor                        |
| CIL          | cil                                  |
| JavaScript   | javascript, js                       |
| RegExp       | regexp, regex                        |
| HTML/XML     | markup, html, xml, svg, mathml, atom, rss |
| CSS          | css                                  |
| SQL          | sql                                  |
| JSON         | json, web-manifest                   |
| Python       | python, py                           |
| Java         | java                                 |
| Bash         | bash, shell                          |
| Batch        | batch, cmd                           |
| PowerShell   | powershell, ps1                      |
| YAML         | yaml, yml                            |
| Go           | go                                   |
| Rust         | rust                                 |
| Lua          | lua                                  |
| Pug          | pug                                  |
| Markdown     | markdown, md                         |

---

## Interactive Demo Pages

The demo web application includes several interactive pages:

- **Demo** - Server-side syntax highlighting showcase for multiple languages
- **Live Editor** - Interactive code editor with real-time highlighting, validation, and formatting
- **PUG Demo** - Side-by-side raw vs. highlighted PUG template code
- **Markdown Demo** - Interactive markdown editor with PrismSpark syntax highlighting and Markdig HTML rendering

---

## Testing

The project includes a comprehensive MSTest test suite with 52 tests:

- **GrammarTest** - Grammar creation, sorting, and token insertion
- **HtmlHighlighterTest** - HTML entity and C language highlighting
- **LanguageTokenizeTest** - Tokenization across multiple languages
- **PrismTest** - Core Prism engine functionality
- **IntegrationTests** - End-to-end highlighting workflows
- **UtilTest** - Utility function validation

Run the tests:

```bash
dotnet test
```

---

## Performance & Best Practices

- Use async APIs for large files
- Enable caching for repeated highlights
- Use plugin and theme systems for extensibility
- For large code, paginate or lazy-load in the UI

---

## Troubleshooting

- **Unknown Language**: Fallback to `LanguageGrammars.Markup`
- **Performance**: Use async and caching
- **Styling**: Use PrismJS CSS or generate with `CssGenerator`

---

## References

- [PrismJS](https://github.com/PrismJS/prism)
- [PrismSharp Languages](https://github.com/tatwd/prism-sharp/tree/main/WebSpark.PrismSpark/Languages)

---

## License

MIT
