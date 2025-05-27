namespace WebSpark.PrismSpark;

/// <summary>
/// Interface for defining grammar rules for a programming language.
/// </summary>
public interface IGrammarDefinition
{
    /// <summary>
    /// Defines and returns the grammar rules for a programming language.
    /// </summary>
    /// <returns>A Grammar object containing the language's syntax rules.</returns>
    Grammar Define();
}
