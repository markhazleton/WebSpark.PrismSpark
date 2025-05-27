namespace WebSpark.PrismSpark;

/// <summary>
/// Interface for syntax highlighters that can process text and generate highlighted output.
/// </summary>
public interface IHighlighter
{
    /// <summary>
    /// Highlights the specified text using the provided grammar rules.
    /// </summary>
    /// <param name="text">The text to highlight.</param>
    /// <param name="grammar">The grammar rules to apply for highlighting.</param>
    /// <param name="language">The programming language being highlighted.</param>
    /// <returns>The highlighted text, typically as HTML.</returns>
    string Highlight(string text, Grammar grammar, string language);
}
