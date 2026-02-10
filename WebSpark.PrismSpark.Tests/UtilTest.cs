using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebSpark.PrismSpark.Tests;

[TestClass]
public class UtilTest
{
    [TestMethod]
    public void MatchPattern_lookbehind_eq_True_Ok()
    {
        var regex = new Regex(@"see (chapter \d+(\.\d)*)", RegexOptions.IgnoreCase);
        var text = @"For more information, see Chapter 3.4.5.1";
        var match = Util.MatchPattern(regex, 0, text, true);
        Assert.IsNotNull(match);
        Assert.AreEqual(3, match.Groups.Length);
        Assert.AreEqual(".5.1", match.Groups[0]);
        Assert.AreEqual("Chapter 3.4.5.1", match.Groups[1]);
        Assert.AreEqual(".1", match.Groups[2]);
        Assert.AreEqual(37, match.Index);
    }

    [DataTestMethod]
    [DataRow("abc", 0, "abc")]
    [DataRow("abc", 1, "bc")]
    [DataRow("abc", 2, "c")]
    [DataRow("abc", 3, "")]
    [DataRow("", 0, "")]
    public void Slice_without_endIndex_Ok(string str, int startIndex, string expected)
    {
        Assert.AreEqual(expected, Util.Slice(str, startIndex));
    }

    [DataTestMethod]
    [DataRow("abc", 0, 0, "")]
    [DataRow("abc", 1, 2, "b")]
    [DataRow("abc", 2, 3, "c")]
    [DataRow("abc", 0, 5, "abc")]
    [DataRow("abc", 4, 2, "")]
    [DataRow("", 0, 2, "")]
    public void Slice_with_endIndex_Ok(string str, int startIndex, int endIndex, string expected)
    {
        Assert.AreEqual(expected, Util.Slice(str, startIndex, endIndex));
    }
}
