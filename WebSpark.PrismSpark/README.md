# PrismSpark.Core

A comprehensive, modern C#/.NET port of [PrismJS](https://github.com/PrismJS/prism) for advanced syntax highlighting, theming, and extensibility.

[![NuGet Version](https://img.shields.io/nuget/v/PrismSpark.Core?label=NuGet)](https://www.nuget.org/packages/PrismSpark.Core)
[![.NET](https://img.shields.io/badge/.NET-10.0-blue)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

---

## Features

- Tokenization & highlighting for 24 languages
- Plugin system: line numbers, copy-to-clipboard, toolbar, and more
- Theme system: built-in and custom themes, CSS generation
- Hooks & extensibility: event-driven customization
- Advanced options: line highlighting, custom CSS classes, context/metadata
- High performance: async processing, caching, efficient rendering
- .NET 10.0 LTS Support: Optimized for the latest .NET Long Term Support release
- Comprehensive MSTest suite: 52 tests covering grammars, tokenization, highlighting, and integration

---

## Requirements

- **.NET 10.0** or later (recommended)
- Compatible with .NET 9.0 and earlier versions

---

## Installation

```pwsh
Install-Package PrismSpark.Core
```

---

## Quick Start

### Basic Tokenization

```csharp
using PrismSpark.Core;

var text = @"<p>Hello world!</p>";
var grammar = LanguageGrammars.Markup;
var tokens = Prism.Tokenize(text, grammar);
```

### HTML Highlighting

```csharp
using PrismSpark.Core;
using PrismSpark.Core.Highlighting;

var code = @"<p>Hello world!</p>";
var grammar = LanguageGrammars.Markup;
var highlighter = new HtmlHighlighter();
var html = highlighter.Highlight(code, grammar, "html");
```

---

## Usage Examples

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

### Themed Highlighting

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

## Output Structure

The highlighter generates HTML like:

```html
<span class="token keyword">public</span> <span class="token class-name">string</span> Name <span class="token operator">=</span> <span class="token string">"Hello"</span><span class="token punctuation">;</span>
```

---

## Styling

Use PrismJS CSS themes or generate your own with `CssGenerator`.

---

## .NET Web MVC Integration

```csharp
// Program.cs
builder.Services.AddSingleton<IHighlighter, HtmlHighlighter>();
builder.Services.AddSingleton<EnhancedHtmlHighlighter>();
builder.Services.AddSingleton<ThemedHtmlHighlighter>();
```

---

## Advanced Features

- Custom grammar definition
- Plugin system and hooks
- Theme system and CSS generation
- Utility functions for string, grammar, and token manipulation
- Context and metadata system for advanced scenarios

---

## Contributing

Contributions are welcome! Please submit a Pull Request or open an issue to discuss changes.

---

## License

MIT
