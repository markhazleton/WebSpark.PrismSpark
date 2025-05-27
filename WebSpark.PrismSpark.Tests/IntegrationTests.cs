using WebSpark.PrismSpark.Highlighting;
using Xunit;

namespace WebSpark.PrismSpark.Tests;

public class IntegrationTests
{
    [Fact]
    public void ThemedHtmlHighlighter_WithCSharpCode_GeneratesCorrectOutput()
    {
        // Arrange
        var highlighter = new ThemedHtmlHighlighter("prism");
        var code = @"using System;
public class Example
{
    public string Name { get; set; } = ""Hello"";

    public void Greet()
    {
        Console.WriteLine($""Hello, {Name}!"");
    }
}";

        // Act
        var result = highlighter.Highlight(code, "csharp");

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains("token", result);
        Assert.Contains("keyword", result);
    }

    [Fact]
    public void ThemedHtmlHighlighter_GenerateHtmlPage_CreatesCompleteHtml()
    {
        // Arrange
        var highlighter = new ThemedHtmlHighlighter("prism");
        var code = "console.log('Hello, World!');";

        // Act
        var result = highlighter.GenerateHtmlPage(code, "javascript", "Test Page");

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<!DOCTYPE html>", result);
        Assert.Contains("<title>Test Page</title>", result);
        Assert.Contains("<style>", result);
        Assert.Contains("Hello, World!", result);
    }


    [Fact]
    public void EnhancedHtmlHighlighter_WithOptions_AppliesConfiguration()
    {
        // Arrange
        var highlighter = new EnhancedHtmlHighlighter();
        var options = new HighlightOptions
        {
            ShowLineNumbers = true,
            ClassPrefix = "my-"
        };
        var code = "var x = 42;";

        // Act
        var result = highlighter.Highlight(code, LanguageGrammars.JavaScript, "javascript", options);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}
