
namespace WebSpark.PrismSpark.Examples;

/// <summary>
/// Provides comprehensive examples demonstrating the core functionality of PrismSpark,
/// including syntax tokenization, language grammar usage, and token processing capabilities.
/// This class serves as both documentation and a practical demonstration of the library's features.
/// </summary>
public class PrismSparkExamples
{
    /// <summary>
    /// Executes all available PrismSpark examples in sequence, demonstrating various features
    /// including basic tokenization, language-specific grammar processing, and token analysis.
    /// This method initializes PrismSpark and runs a comprehensive suite of demonstrations.
    /// </summary>
    /// <remarks>
    /// The examples include:
    /// <list type="bullet">
    /// <item><description>Basic tokenization of C# code</description></item>
    /// <item><description>Language-specific grammar examples with JavaScript</description></item>
    /// <item><description>Token processing and analysis with Python code</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Run all PrismSpark examples
    /// PrismSparkExamples.RunAllExamples();
    /// </code>
    /// </example>
    public static void RunAllExamples()
    {
        // Initialize PrismSpark
        PrismSpark.Initialize();

        Console.WriteLine("=== PrismSpark Core Examples ===\n");

        // Basic tokenization example
        BasicTokenizationExample();

        // Language grammar examples
        LanguageGrammarExamples();

        // Token processing examples
        TokenProcessingExamples();
    }

    /// <summary>
    /// Demonstrates basic code tokenization using PrismSpark with C# source code.
    /// This example shows how to tokenize a complete C# class including various language constructs
    /// such as using statements, class declarations, methods, string literals, and control flow.
    /// </summary>
    /// <remarks>
    /// This example tokenizes a sample C# "Hello World" program that includes:
    /// <list type="bullet">
    /// <item><description>Using directives</description></item>
    /// <item><description>Class and method declarations</description></item>
    /// <item><description>String literals and interpolation</description></item>
    /// <item><description>Array initialization and foreach loops</description></item>
    /// </list>
    /// The output shows each token with its content and type, demonstrating how PrismSpark
    /// breaks down source code into meaningful components for syntax highlighting.
    /// </remarks>
    private static void BasicTokenizationExample()
    {
        Console.WriteLine("1. Basic Tokenization");
        Console.WriteLine("======================");

        var csharpCode = @"
using System;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine(""Hello, World!"");
        var numbers = new int[] { 1, 2, 3, 4, 5 };

        foreach (var num in numbers)
        {
            Console.WriteLine($""Number: {num}"");
        }
    }
}";

        var grammar = LanguageGrammars.CSharp;
        var tokens = Prism.Tokenize(csharpCode, grammar);

        Console.WriteLine("C# Code tokenized:");
        foreach (var token in tokens)
        {
            if (token is StringToken stringToken)
            {
                Console.WriteLine($"Text: '{stringToken.Content.Replace("\n", "\\n").Replace("\r", "\\r")}'");
            }
            else if (token is StreamToken streamToken)
            {
                Console.WriteLine($"Token: {streamToken.Type} (contains {streamToken.Content.Length} sub-tokens)");
            }
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Demonstrates language-specific grammar processing using JavaScript code as an example.
    /// This method shows how different programming languages are tokenized using their specific
    /// grammar rules, highlighting the differences in language constructs and token types.
    /// </summary>
    /// <remarks>
    /// This example uses a JavaScript implementation of the Fibonacci algorithm to demonstrate:
    /// <list type="bullet">
    /// <item><description>Function declarations and parameters</description></item>
    /// <item><description>Conditional statements and operators</description></item>
    /// <item><description>Recursive function calls</description></item>
    /// <item><description>Console output and method chaining</description></item>
    /// </list>
    /// The output is limited to the first 10 tokens to keep the demonstration concise while
    /// still showing the variety of token types that JavaScript grammar can identify.
    /// </remarks>
    private static void LanguageGrammarExamples()
    {
        Console.WriteLine("2. Language Grammar Examples");
        Console.WriteLine("============================");

        var jsCode = @"
function fibonacci(n) {
    if (n <= 1) return n;
    return fibonacci(n - 1) + fibonacci(n - 2);
}

console.log(fibonacci(10));";

        var jsGrammar = LanguageGrammars.JavaScript;
        var jsTokens = Prism.Tokenize(jsCode, jsGrammar);

        Console.WriteLine("JavaScript tokens:");
        foreach (var token in jsTokens.Take(10)) // Show first 10 tokens
        {
            if (token is StringToken stringToken)
            {
                Console.WriteLine($"Text: '{stringToken.Content.Replace("\n", "\\n").Replace("\r", "\\r")}'");
            }
            else if (token is StreamToken streamToken)
            {
                Console.WriteLine($"Token: {streamToken.Type} (contains {streamToken.Content.Length} sub-tokens)");
            }
        }
        Console.WriteLine("... (showing first 10 tokens only)\n");
    }

    private static void TokenProcessingExamples()
    {
        Console.WriteLine("3. Token Processing Examples");
        Console.WriteLine("============================");

        var pythonCode = @"
import numpy as np

def calculate(x, y):
    """"""Calculate sum""""""
    return x + y

result = calculate(42, 8)
print(f'Result: {result}')";

        var pythonGrammar = LanguageGrammars.Python;
        var tokens = Prism.Tokenize(pythonCode, pythonGrammar);

        // Count tokens by type
        var tokenCounts = new Dictionary<string, int>();
        foreach (var token in tokens)
        {
            if (token is StreamToken streamToken && !string.IsNullOrEmpty(streamToken.Type))
            {
                tokenCounts[streamToken.Type] = tokenCounts.GetValueOrDefault(streamToken.Type, 0) + 1;
            }
        }

        Console.WriteLine("Python token counts:");
        foreach (var kvp in tokenCounts.OrderByDescending(x => x.Value))
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
        Console.WriteLine();

        // Show available language grammars
        Console.WriteLine("Available language grammars:");
        var grammarProperties = typeof(LanguageGrammars).GetProperties()
            .Where(p => p.PropertyType == typeof(Grammar))
            .Select(p => p.Name)
            .OrderBy(name => name);

        foreach (var name in grammarProperties)
        {
            Console.WriteLine($"- {name}");
        }
        Console.WriteLine();
    }
}
