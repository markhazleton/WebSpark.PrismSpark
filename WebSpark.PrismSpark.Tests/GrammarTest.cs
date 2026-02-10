using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebSpark.PrismSpark.Tests;

[TestClass]
public class GrammarTest
{
    [TestMethod]
    public void Create_Sorted_Grammar_Ok()
    {
        var grammar = new Grammar
        {
            ["hello"] = new GrammarToken[] { },
        };
        grammar["foo"] = new GrammarToken[] { };
        grammar.InsertBefore("foo", new Grammar
        {
            ["world"] = new GrammarToken[] { },
        });

        var keys = grammar.Select(x => x.Key).ToArray();
        var expected = new[] { "hello", "world", "foo" };
        CollectionAssert.AreEqual(expected, keys);

    }
}
