using System.Collections;

namespace WebSpark.PrismSpark;

/// <summary>
/// Represents a grammar definition for syntax highlighting, containing token patterns and rules.
/// </summary>
public class Grammar : IEnumerable<KeyValuePair<string, GrammarToken[]>>
{
    private int _count;

    private IDictionary<string, OrderVal> GrammarTokenMap { get; }    /// <summary>
                                                                      /// An optional grammar object that will be appended to this grammar.
                                                                      /// </summary>
    public Grammar? Reset { get; set; }

    /// <summary>
    /// Initializes a new instance of the Grammar class.
    /// </summary>
    public Grammar()
    {
        _count = 0;
        GrammarTokenMap = new Dictionary<string, OrderVal>(16);
    }

    /// <summary>
    /// Gets or sets the grammar tokens for the specified key.
    /// </summary>
    /// <param name="key">The token key.</param>
    /// <returns>The array of grammar tokens for the specified key.</returns>
    public GrammarToken[] this[string key]
    {
        get => GrammarTokenMap[key].Val;
        set
        {
            var wrapVal = GrammarTokenMap.TryGetValue(key, out var oldVal)
                ? new OrderVal(value, oldVal.PrevOrder, oldVal.Order)
                : new OrderVal(value, _count, ++_count);
            GrammarTokenMap[key] = wrapVal;
        }
    }
    private int Length => GrammarTokenMap.Count;

    /// <summary>
    /// Removes the grammar token with the specified key.
    /// </summary>
    /// <param name="key">The key of the token to remove.</param>
    public void Remove(string key)
    {
        GrammarTokenMap.Remove(key);
    }

    /// <summary>
    /// Inserts the tokens from another grammar before the specified key.
    /// </summary>
    /// <param name="key">The key before which to insert the grammar.</param>
    /// <param name="grammar">The grammar to insert.</param>
    public void InsertBefore(string key, Grammar grammar)
    {
        var beforeItem = GrammarTokenMap[key];
        var order = beforeItem.Order;
        var prevOrder = beforeItem.PrevOrder;
        var icr = (order - prevOrder) / (grammar.Length + 1);
        var newOrder = prevOrder;

        foreach (var item in grammar)
        {
            // will override exists item
            GrammarTokenMap[item.Key] = new OrderVal(item.Value, newOrder, newOrder += icr);
        }

        beforeItem.PrevOrder = newOrder;
    }

    private class OrderVal
    {
        public GrammarToken[] Val { get; }
        public float Order { get; }
        public float PrevOrder { get; set; }

        public OrderVal(GrammarToken[] val, float prevOrder, float order)
        {
            Val = val;
            PrevOrder = prevOrder;
            Order = order;
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the grammar tokens in order.
    /// </summary>
    /// <returns>An enumerator for the grammar tokens.</returns>
    public IEnumerator<KeyValuePair<string, GrammarToken[]>> GetEnumerator()
    {
        return GrammarTokenMap.OrderBy(kv => kv.Value.Order)
            .Select(kv =>
                new KeyValuePair<string, GrammarToken[]>(kv.Key, kv.Value.Val))
            .GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the grammar tokens.
    /// </summary>
    /// <returns>An enumerator for the grammar tokens.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
