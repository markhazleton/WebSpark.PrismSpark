namespace WebSpark.PrismSpark;

/// <summary>
/// Represents an abstract base class for tokens used in syntax highlighting.
/// </summary>
public abstract class Token
{
    /// <summary>
    /// Gets the type of the token.
    /// </summary>
    public string? Type { get; }

    /// <summary>
    /// Gets the array of aliases associated with this token.
    /// </summary>
    public string[] Alias { get; }    /// <summary>
                                      /// Copy of the full string this token was created from
                                      /// </summary>
    protected int Length { get; }

    /// <summary>
    /// Initializes a new instance of the Token class.
    /// </summary>
    /// <param name="type">The type of the token.</param>
    /// <param name="alias">An array of aliases for the token.</param>
    /// <param name="matchedStr">The matched string that created this token.</param>
    protected Token(string? type, string[]? alias, string? matchedStr)
    {
        Type = type;
        Alias = alias ?? Array.Empty<string>();
        Length = matchedStr?.Length ?? 0;
    }

    /// <summary>
    /// Determines whether this token was matched against a string.
    /// </summary>
    /// <returns>True if the token was matched (Length > 0), otherwise false.</returns>
    public bool IsMatchedToken()
    {
        return Length > 0;
    }

    /// <summary>
    /// Return `Length` if current token is matched
    /// </summary>
    /// <returns></returns>
    public abstract int GetLength();

}

/// <summary>
/// Represents a token that contains string content.
/// </summary>
public class StringToken : Token
{
    /// <summary>
    /// Gets the string content of this token.
    /// </summary>
    public string Content { get; }

    /// <summary>
    /// Initializes a new instance of the StringToken class.
    /// </summary>
    /// <param name="content">The string content of the token.</param>
    /// <param name="type">The type of the token.</param>
    /// <param name="alias">An array of aliases for the token.</param>
    /// <param name="matchedStr">The matched string that created this token.</param>
    public StringToken(string content,
        string? type = null,
        string[]? alias = null,
        string? matchedStr = null) : base(type, alias, matchedStr)
    {
        Content = content;
    }

    /// <summary>
    /// Gets the length of the token. Returns the matched length if the token was matched, otherwise returns the content length.
    /// </summary>
    /// <returns>The length of the token.</returns>
    public override int GetLength()
    {
        return IsMatchedToken() ? Length : Content.Length;
    }
}

/// <summary>
/// Represents a token that contains an array of other tokens.
/// </summary>
public class StreamToken : Token
{
    /// <summary>
    /// Gets the array of tokens that make up the content of this stream token.
    /// </summary>
    public Token[] Content { get; }

    /// <summary>
    /// Initializes a new instance of the StreamToken class.
    /// </summary>
    /// <param name="content">The array of tokens that make up this stream token.</param>
    /// <param name="type">The type of the token.</param>
    /// <param name="alias">An array of aliases for the token.</param>
    /// <param name="matchedStr">The matched string that created this token.</param>
    public StreamToken(Token[] content,
        string? type = null,
        string[]? alias = null,
        string? matchedStr = null) : base(type, alias, matchedStr)
    {
        Content = content;
    }

    /// <summary>
    /// Gets the length of the token. Returns the matched length if the token was matched, otherwise returns the content array length.
    /// </summary>
    /// <returns>The length of the token.</returns>
    public override int GetLength()
    {
        return IsMatchedToken() ? Length : Content.Length;
    }
}
