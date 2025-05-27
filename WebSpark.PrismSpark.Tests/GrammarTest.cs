using System.Linq;
using Xunit;

namespace WebSpark.PrismSpark.Tests;

public class GrammarTest
{
    [Fact]
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
        Assert.Equal(expected, keys);

    }
}
