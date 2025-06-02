# PrismSharp - Enhanced Features

This document describes the advanced features and extensibility of PrismSharp, the .NET port of PrismJS.

---

## 🚀 New Features

### Plugin System

- **IPlugin Interface**: Standardized plugin interface
- **PluginManager**: Centralized registration and execution
- **Dependency Resolution**: Automatic plugin ordering
- **Built-in Plugins**: Line numbers, copy-to-clipboard, toolbar, autolinker, file highlight, show language, command line, normalize whitespace, custom class

### Hooks System

Event-driven extensibility for custom processing:

```csharp
Hooks.Add(HookNames.BeforeTokenize, args => { /* pre-processing */ });
Hooks.Add(HookNames.AfterHighlight, args => { /* post-processing */ });
```

Available hooks: `before-tokenize`, `after-tokenize`, `before-highlight`, `after-highlight`, `before-insert`, `after-insert`, `wrap-tokens`, `complete`

### Advanced HTML Highlighting

- **EnhancedHtmlHighlighter**: Plugin integration, line numbers, line highlighting, custom CSS classes
- **ThemedHtmlHighlighter**: Theme-aware highlighting, CSS generation, HTML page generation

### Theme System

- Built-in themes: Prism, Dark, Tomorrow Night, Solarized Light
- Custom theme creation and registration
- CSS generation for themes and plugins

```csharp
var theme = new Theme
{
    Name = "my-theme",
    Background = new ThemeStyle { BackgroundColor = "#1e1e1e" },
    Foreground = new ThemeStyle { Color = "#d4d4d4" },
    TokenStyles = new Dictionary<string, ThemeStyle>
    {
        ["keyword"] = new() { Color = "#569cd6", FontWeight = "bold" },
        ["string"] = new() { Color = "#ce9178" }
    }
};
ThemeManager.RegisterTheme(theme);
```

### Extended Language Support

- Rust, Go, Kotlin, Swift, YAML, Markdown, and more
- TypeScript, PHP, Ruby, and language aliases

### Utility Functions

- **StringUtils**: HTML escaping, normalization, case conversion
- **GrammarUtils**: Clone, extend, insert tokens
- **TokenUtils**: Manipulate and query tokens
- **CodeUtils**: Language detection, line utilities

### CSS Generation System

```csharp
var css = CssGenerator.GenerateThemeCss("dark", new CssOptions
{
    FontFamily = "JetBrains Mono, monospace",
    FontSize = "14px",
    LineHeight = "1.6"
});
```

### Context and Metadata System

```csharp
var context = new HighlightContext
{
    CssClasses = { "line-numbers", "highlight-syntax" },
    Attributes = { ["data-language"] = "csharp" },
    Metadata = { ["filename"] = "Program.cs" }
};
```

---

## 📖 Usage Examples

### Initialization

```csharp
PrismSharp.Initialize(config => {
    config.EnablePlugin("line-numbers")
          .EnablePlugin("copy-to-clipboard")
          .DisablePlugin("autolinker");
});
```

### Complete Example

```csharp
var highlighter = new ThemedHtmlHighlighter("tomorrow-night");
var options = new HighlightOptions
{
    EnableLineNumbers = true,
    HighlightedLines = new[] { 3, 7 },
    NormalizeWhitespace = true,
    TabSize = 4
};
var html = highlighter.Highlight(sourceCode, "typescript", options);
var page = highlighter.GenerateHtmlPage(sourceCode, "typescript", "TypeScript Example");
```

### Plugin Configuration

```csharp
var lineNumbersPlugin = PluginManager.GetPlugin("line-numbers");
lineNumbersPlugin.SetOption("startFrom", 10);
var toolbarPlugin = PluginManager.GetPlugin("toolbar");
toolbarPlugin.SetOption("showCopy", true);
toolbarPlugin.SetOption("showSelectAll", true);
```

---

## 🎯 PrismJS Feature Parity

- [x] Plugin system
- [x] Hooks system
- [x] Line numbers, line highlighting, copy-to-clipboard, autolinker, show language, toolbar, command line, normalize whitespace, file highlight, custom class
- [x] Theme system with CSS generation
- [x] Multiple built-in themes
- [x] Extended language support
- [x] Utility functions
- [x] Context and metadata system
- [x] Advanced HTML generation

### Extensions Beyond PrismJS

- Enhanced theme management
- Complete HTML page generation
- Advanced language detection
- Rich metadata support
- Fluent configuration API
- Integrated utility functions

---

## 🚀 Performance & Extensibility

- Lazy loading of grammars
- Plugin dependency resolution
- Efficient token processing
- Minimal memory overhead
- Custom plugin and theme development
- Hook system for pipeline customization
- Grammar extension and modification
- Token manipulation utilities

PrismSharp is a powerful, extensible syntax highlighting library for .NET, rivaling and extending PrismJS while maintaining .NET performance and type safety.
