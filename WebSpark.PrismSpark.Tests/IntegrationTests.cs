using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSpark.PrismSpark.Highlighting;

namespace WebSpark.PrismSpark.Tests;

[TestClass]
public class IntegrationTests
{
    [TestMethod]
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
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Length > 0);
        StringAssert.Contains(result, "token");
        StringAssert.Contains(result, "keyword");
    }

    [TestMethod]
    public void ThemedHtmlHighlighter_GenerateHtmlPage_CreatesCompleteHtml()
    {
        // Arrange
        var highlighter = new ThemedHtmlHighlighter("prism");
        var code = "console.log('Hello, World!');";

        // Act
        var result = highlighter.GenerateHtmlPage(code, "javascript", "Test Page");

        // Assert
        Assert.IsNotNull(result);
        StringAssert.Contains(result, "<!DOCTYPE html>");
        StringAssert.Contains(result, "<title>Test Page</title>");
        StringAssert.Contains(result, "<style>");
        StringAssert.Contains(result, "Hello, World!");
    }


    [TestMethod]
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
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Length > 0);
    }
}
