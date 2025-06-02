# PrismSpark

A modern, high-performance C#/.NET port of [PrismJS](https://github.com/PrismJS/prism) for advanced syntax highlighting, theming, and extensibility.

---

## Features

- **Tokenization & Highlighting** for 20+ languages
- **Plugin System**: Line numbers, copy-to-clipboard, toolbar, and more
- **Theme System**: Built-in and custom themes, CSS generation
- **Hooks & Extensibility**: Event-driven customization
- **Advanced Options**: Line highlighting, custom CSS classes, context/metadata
- **Performance**: Async processing, caching, and efficient rendering

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

## Supported Languages

| Language | Aliases |
|----------|---------|
| C#       | csharp, cs, dotnet |
| JavaScript | javascript, js |
| Python   | python, py |
| HTML/XML | markup, html, xml |
| CSS      | css |
| SQL      | sql |
| Bash     | bash, shell |
| PowerShell | powershell, ps1 |
| ...      | ... |

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
