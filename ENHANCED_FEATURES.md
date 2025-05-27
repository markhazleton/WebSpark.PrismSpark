# PrismSharp - Enhanced Features

This document outlines the comprehensive set of features that have been added to PrismSharp to bring it closer to PrismJS functionality.

## 🚀 New Features Added

### 1. Plugin System Architecture

A complete plugin system has been implemented that mirrors PrismJS's extensibility:

#### Core Plugin Infrastructure

- **IPlugin Interface**: Standardized plugin interface with lifecycle management
- **PluginBase Class**: Abstract base class providing common plugin functionality
- **PluginManager**: Centralized plugin registration and execution
- **Dependency Resolution**: Automatic plugin dependency ordering and initialization

#### Built-in Plugins

##### Essential Plugins

- **CustomClassPlugin**: Add custom CSS classes and modify token styling
- **LineNumbersPlugin**: Add line numbers to code blocks
- **LineHighlightPlugin**: Highlight specific lines in code blocks
- **NormalizeWhitespacePlugin**: Format and normalize code whitespace

##### Advanced Plugins

- **CopyToClipboardPlugin**: Add copy-to-clipboard functionality
- **AutolinkerPlugin**: Automatically convert URLs to clickable links
- **FileHighlightPlugin**: Auto-detect language from file patterns
- **ShowLanguagePlugin**: Display the programming language name
- **ToolbarPlugin**: Add customizable toolbar to code blocks
- **CommandLinePlugin**: Enhanced command line and shell highlighting

### 2. Hooks System

Event-driven extensibility system for custom processing:

```csharp
// Register custom hook handlers
Hooks.Add(HookNames.BeforeTokenize, args => {
    // Custom pre-processing
});

Hooks.Add(HookNames.AfterHighlight, args => {
    // Custom post-processing
});
```

**Available Hooks:**

- `before-tokenize`, `after-tokenize`
- `before-highlight`, `after-highlight`
- `before-insert`, `after-insert`
- `wrap-tokens`, `complete`

### 3. Advanced HTML Highlighting

#### EnhancedHtmlHighlighter

Comprehensive highlighting with plugin integration:

```csharp
var highlighter = new EnhancedHtmlHighlighter();
var options = new HighlightOptions
{
    EnableLineNumbers = true,
    HighlightedLines = new[] { 2, 4, 6 },
    CustomCssClasses = new Dictionary<string, string>
    {
        ["keyword"] = "my-keyword-style"
    }
};

var html = highlighter.Highlight(code, "csharp", options);
```

#### ThemedHtmlHighlighter

Theme-aware highlighting with CSS generation:

```csharp
var highlighter = new ThemedHtmlHighlighter("dark");
var htmlPage = highlighter.GenerateHtmlPage(code, "python", "My Code");
```

### 4. Theme System

Complete theme management system:

#### Built-in Themes

- **Prism**: Default PrismJS theme
- **Dark**: Modern dark theme
- **Tomorrow Night**: Popular dark theme
- **Solarized Light**: Popular light theme

#### Theme Features

- Custom theme creation
- CSS generation
- Token-specific styling
- Background and foreground customization

```csharp
// Create custom theme
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

### 5. Extended Language Support

#### Popular Languages Added

- **Rust**: Complete Rust syntax with macros and lifetime annotations
- **Go**: Go language with goroutines and channels
- **Kotlin**: Modern Android development language
- **Swift**: iOS development language
- **YAML**: Configuration file format
- **Markdown**: Documentation format with embedded code blocks

#### Language Features

- TypeScript with type annotations
- PHP with embedded HTML support
- Ruby with comprehensive syntax support
- Language aliases and file extension detection

### 6. Utility Functions

#### String Utilities

```csharp
// HTML escaping/unescaping
var escaped = StringUtils.EscapeHtml(code);

// Code normalization
var normalized = StringUtils.NormalizeWhitespace(code, tabSize: 4);

// Case conversion
var kebab = StringUtils.ToKebabCase("camelCase");
```

#### Grammar Utilities

```csharp
// Clone and extend grammars
var newGrammar = GrammarUtils.CloneGrammar(baseGrammar);
var extended = GrammarUtils.ExtendGrammar(baseGrammar, extension);

// Insert tokens
GrammarUtils.InsertBefore(grammar, "keyword", "custom", customToken);
```

#### Token Utilities

```csharp
// Token manipulation
var strings = TokenUtils.TokensToStringArray(tokens);
var keywordTokens = TokenUtils.GetTokensByType(tokens, "keyword");
var wrapped = TokenUtils.WrapTokens(tokens, "container");
```

#### Code Analysis

```csharp
// Language detection
var language = CodeUtils.DetectLanguage(code);

// Line utilities
var lineCount = CodeUtils.CountLines(code);
var line = CodeUtils.GetLine(code, 5);
var indent = CodeUtils.GetIndentationLevel(line);
```

### 7. CSS Generation System

Comprehensive CSS generation for themes and plugins:

```csharp
// Generate theme CSS
var css = CssGenerator.GenerateThemeCss("dark", new CssOptions
{
    FontFamily = "JetBrains Mono, monospace",
    FontSize = "14px",
    LineHeight = "1.6"
});
```

### 8. Context and Metadata System

Rich context information for advanced processing:

```csharp
var context = new HighlightContext
{
    CssClasses = { "line-numbers", "highlight-syntax" },
    Attributes = { ["data-language"] = "csharp" },
    Metadata = { ["filename"] = "Program.cs" }
};
```

## 📖 Usage Examples

### Basic Setup

```csharp
using PrismSharp.Core;
using PrismSharp.Highlighting.HTML;

// Initialize PrismSharp with all features
PrismSharp.Initialize();

// Or initialize with custom configuration
PrismSharp.Initialize(config => {
    config.EnablePlugin("line-numbers")
          .EnablePlugin("copy-to-clipboard")
          .DisablePlugin("autolinker");
});
```

### Complete Example

```csharp
// Create themed highlighter
var highlighter = new ThemedHtmlHighlighter("tomorrow-night");

// Configure highlighting options
var options = new HighlightOptions
{
    EnableLineNumbers = true,
    HighlightedLines = new[] { 3, 7 },
    NormalizeWhitespace = true,
    TabSize = 4
};

// Highlight code
var html = highlighter.Highlight(sourceCode, "typescript", options);

// Generate complete HTML page
var page = highlighter.GenerateHtmlPage(
    sourceCode, 
    "typescript", 
    "TypeScript Example"
);
```

### Plugin Configuration

```csharp
// Configure specific plugins
var lineNumbersPlugin = PluginManager.GetPlugin("line-numbers");
lineNumbersPlugin.SetOption("startFrom", 10);

var toolbarPlugin = PluginManager.GetPlugin("toolbar");
toolbarPlugin.SetOption("showCopy", true);
toolbarPlugin.SetOption("showSelectAll", true);
```

## 🎯 PrismJS Feature Parity

This implementation brings PrismSharp to near feature parity with PrismJS:

### ✅ Implemented

- [x] Plugin system architecture
- [x] Hooks system for extensibility
- [x] Line numbers plugin
- [x] Line highlighting plugin
- [x] Copy to clipboard plugin
- [x] Autolinker plugin
- [x] Show language plugin
- [x] Toolbar plugin
- [x] Command line plugin
- [x] Normalize whitespace plugin
- [x] File highlight plugin
- [x] Custom class plugin
- [x] Theme system with CSS generation
- [x] Multiple built-in themes
- [x] Extended language support (20+ languages)
- [x] Utility functions
- [x] Context and metadata system
- [x] Advanced HTML generation

### 🔄 Extensions Beyond PrismJS

- Enhanced theme management system
- Comprehensive CSS generation
- Complete HTML page generation
- Advanced language detection
- Rich metadata support
- Fluent configuration API
- Integrated utility functions

## 🚀 Performance Considerations

The implementation maintains performance while adding extensive features:

- Lazy loading of language grammars
- Plugin dependency resolution
- Efficient token processing
- Minimal memory overhead
- Configurable feature sets

## 🔧 Extensibility

The system is designed for maximum extensibility:

- Custom plugin development
- Hook system for processing pipeline customization
- Custom theme creation
- Grammar extension and modification
- Token manipulation utilities

This comprehensive feature set makes PrismSharp a powerful, extensible syntax highlighting library that rivals and extends the capabilities of PrismJS while maintaining the performance and type safety of C#/.NET.
