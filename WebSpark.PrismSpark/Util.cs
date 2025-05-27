using System.Text.RegularExpressions;

namespace WebSpark.PrismSpark;

/// <summary>
/// Utility class providing helper methods for string manipulation and regex pattern matching.
/// </summary>
public static class Util
{
    /// <summary>
    /// Extracts a substring from the specified string starting at the given index.
    /// </summary>
    /// <param name="str">The source string to slice.</param>
    /// <param name="startIndex">The zero-based starting index of the substring.</param>
    /// <param name="endIndex">The zero-based ending index of the substring. If null, slices to the end of the string.</param>
    /// <returns>A substring that begins at the specified start index and ends at the specified end index.</returns>
    public static string Slice(string str, int startIndex, int? endIndex = null)
    {
        if (startIndex >= str.Length) return string.Empty;
        if (!endIndex.HasValue || endIndex > str.Length)
            endIndex = str.Length;
        var sliceLength = endIndex.Value - startIndex;
        return str.Substring(startIndex, sliceLength);
    }

    /// <summary>
    /// Matches a regular expression pattern against text at a specified position and handles lookbehind adjustments.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to match.</param>
    /// <param name="pos">The position in the text to start matching from.</param>
    /// <param name="text">The text to match against.</param>
    /// <param name="lookbehind">Whether to adjust the match for Prism lookbehind groups.</param>
    /// <returns>A MyMatch object containing the match results and adjusted position if lookbehind is used.</returns>
    public static MyMatch MatchPattern(Regex pattern, int pos, string text, bool lookbehind)
    {
        var match = pattern.Match(text, pos);
        var myMatch = new MyMatch(match);
        var matchGroups = myMatch.Groups;

        if (myMatch.Success && lookbehind && matchGroups.Length > 1 && matchGroups[1].Length > 0)
        {
            // change the match to remove the text matched by the Prism lookbehind group
            var lookbehindLength = matchGroups[1].Length;
            myMatch.Index += lookbehindLength;
            matchGroups[0] = Slice(matchGroups[0], lookbehindLength);
        }

        return myMatch;
    }

    /// <summary>
    /// Represents a match result from a regular expression operation with additional functionality for Prism syntax highlighting.
    /// </summary>
    public class MyMatch
    {
        /// <summary>
        /// Gets or sets the array of captured groups from the regular expression match.
        /// </summary>
        public string[] Groups { get; set; }

        /// <summary>
        /// Gets or sets the position in the input string where the match starts.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the match was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Initializes a new instance of the MyMatch class from a .NET Match object.
        /// </summary>
        /// <param name="match">The .NET Match object to convert.</param>
        public MyMatch(Match match)
        {
            Success = match.Success;
            Index = match.Index;
            Groups = ParseGroups(match);
        }

        private static string[] ParseGroups(Match match)
        {
            // trim end with `\r`
            return (from Group g in match.Groups select g.Value.TrimEnd('\r')).ToArray();
        }
    }
}
