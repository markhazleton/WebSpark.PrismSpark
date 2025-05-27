namespace WebSpark.PrismSpark.Languages;

/// <summary>
/// Additional popular language grammars from PrismJS
/// </summary>
public static class PopularLanguages
{
    /// <summary>
    /// Rust programming language grammar
    /// </summary>
    public static Grammar Rust()
    {
        var grammar = new Grammar();

        // Comments
        grammar["comment"] = new GrammarToken[]
        {
            new GrammarToken(@"//.*|/\*[\s\S]*?\*/", greedy: true)
        };

        // String literals
        grammar["string"] = new GrammarToken[]
        {
            new GrammarToken(@"""(?:[^""\\]|\\.)*""|r#*""(?:[^""]|""(?!#*""))*""#*", greedy: true)
        };

        // Character literals
        grammar["char"] = new GrammarToken[]
        {
            new GrammarToken(@"'(?:[^'\\]|\\(?:[nrt'""\\0]|x[0-9a-fA-F]{2}|u\{[0-9a-fA-F]{1,6}\}))'")
        };

        // Numbers
        grammar["number"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:0x[0-9a-fA-F_]+|0o[0-7_]+|0b[01_]+|\d[\d_]*(?:\.[\d_]*)?(?:[eE][+-]?\d+)?)[iu]?(?:8|16|32|64|128|size)?\b")
        };

        // Keywords
        grammar["keyword"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:as|async|await|break|const|continue|crate|dyn|else|enum|extern|false|fn|for|if|impl|in|let|loop|match|mod|move|mut|pub|ref|return|self|Self|static|struct|super|trait|true|type|union|unsafe|use|where|while)\b")
        };

        // Functions
        grammar["function"] = new GrammarToken[]
        {
            new GrammarToken(@"\b[a-zA-Z_][a-zA-Z0-9_]*\s*(?=\()", lookbehind: true)
        };

        return grammar;
    }

    /// <summary>
    /// TypeScript programming language grammar
    /// </summary>
    public static Grammar TypeScript()
    {
        var grammar = new Grammar();

        // Comments
        grammar["comment"] = new GrammarToken[]
        {
            new GrammarToken(@"//.*|/\*[\s\S]*?\*/", greedy: true)
        };

        // String literals
        grammar["string"] = new GrammarToken[]
        {
            new GrammarToken(@"""(?:[^""\\]|\\.)*""|'(?:[^'\\]|\\.)*'|`(?:[^`\\]|\\.)*`", greedy: true)
        };

        // Numbers
        grammar["number"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:0x[0-9a-fA-F]+|0b[01]+|0o[0-7]+|\d+(?:\.\d+)?(?:[eE][+-]?\d+)?)\b")
        };

        // Keywords
        grammar["keyword"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:abstract|any|as|async|await|boolean|break|case|catch|class|const|constructor|continue|debugger|declare|default|delete|do|else|enum|export|extends|false|finally|for|from|function|get|if|implements|import|in|instanceof|interface|let|module|namespace|never|new|null|number|object|of|package|private|protected|public|readonly|return|set|static|string|super|switch|symbol|this|throw|true|try|type|typeof|undefined|unique|unknown|var|void|while|with|yield)\b")
        };

        // Functions
        grammar["function"] = new GrammarToken[]
        {
            new GrammarToken(@"\b[a-zA-Z_$][a-zA-Z0-9_$]*\s*(?=\()")
        };

        return grammar;
    }

    /// <summary>
    /// Go programming language grammar
    /// </summary>
    public static Grammar Go()
    {
        var grammar = new Grammar();

        // Comments
        grammar["comment"] = new GrammarToken[]
        {
            new GrammarToken(@"//.*|/\*[\s\S]*?\*/", greedy: true)
        };

        // String literals
        grammar["string"] = new GrammarToken[]
        {
            new GrammarToken(@"""(?:[^""\\]|\\.)*""|`[^`]*`", greedy: true)
        };

        // Character literals
        grammar["char"] = new GrammarToken[]
        {
            new GrammarToken(@"'(?:[^'\\]|\\.)'")
        };

        // Numbers
        grammar["number"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:0[xX][0-9a-fA-F]+|0[0-7]+|\d+(?:\.\d+)?(?:[eE][+-]?\d+)?)\b")
        };

        // Keywords
        grammar["keyword"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:break|case|chan|const|continue|default|defer|else|fallthrough|for|func|go|goto|if|import|interface|map|package|range|return|select|struct|switch|type|var)\b")
        };

        // Built-in types
        grammar["builtin"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:bool|byte|complex64|complex128|error|float32|float64|int|int8|int16|int32|int64|rune|string|uint|uint8|uint16|uint32|uint64|uintptr)\b")
        };

        // Functions
        grammar["function"] = new GrammarToken[]
        {
            new GrammarToken(@"\b[a-zA-Z_][a-zA-Z0-9_]*\s*(?=\()")
        };

        return grammar;
    }

    /// <summary>
    /// Swift programming language grammar
    /// </summary>
    public static Grammar Swift()
    {
        var grammar = new Grammar();

        // Comments
        grammar["comment"] = new GrammarToken[]
        {
            new GrammarToken(@"//.*|/\*[\s\S]*?\*/", greedy: true)
        };

        // String literals
        grammar["string"] = new GrammarToken[]
        {
            new GrammarToken(@"""(?:[^""\\]|\\.)*""", greedy: true)
        };

        // Character literals
        grammar["char"] = new GrammarToken[]
        {
            new GrammarToken(@"'(?:[^'\\]|\\.)'")
        };

        // Numbers
        grammar["number"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:0x[0-9a-fA-F]+|0o[0-7]+|0b[01]+|\d+(?:\.\d+)?(?:[eE][+-]?\d+)?)\b")
        };

        // Keywords
        grammar["keyword"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:associatedtype|class|deinit|enum|extension|fileprivate|func|import|init|inout|internal|let|open|operator|private|protocol|public|static|struct|subscript|typealias|var|break|case|continue|default|defer|do|else|fallthrough|for|guard|if|in|repeat|return|switch|where|while|as|Any|catch|false|is|nil|rethrows|super|self|Self|throw|throws|true|try)\b")
        };

        // Functions
        grammar["function"] = new GrammarToken[]
        {
            new GrammarToken(@"\b[a-zA-Z_][a-zA-Z0-9_]*\s*(?=\()")
        };

        return grammar;
    }

    /// <summary>
    /// Kotlin programming language grammar
    /// </summary>
    public static Grammar Kotlin()
    {
        var grammar = new Grammar();

        // Comments
        grammar["comment"] = new GrammarToken[]
        {
            new GrammarToken(@"//.*|/\*[\s\S]*?\*/", greedy: true)
        };

        // String literals
        grammar["string"] = new GrammarToken[]
        {
            new GrammarToken(@"""(?:[^""\\]|\\.)*""|'(?:[^'\\]|\\.)*'", greedy: true)
        };

        // Numbers
        grammar["number"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:0x[0-9a-fA-F]+|0b[01]+|\d+(?:\.\d+)?(?:[eE][+-]?\d+)?)[fLdF]?\b")
        };

        // Keywords
        grammar["keyword"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:abstract|actual|annotation|as|break|by|catch|class|companion|const|constructor|continue|crossinline|data|do|dynamic|else|enum|expect|external|false|final|finally|for|fun|get|if|import|in|infix|init|inline|inner|interface|internal|is|lateinit|noinline|null|object|open|operator|out|override|package|private|protected|public|reified|return|sealed|set|super|suspend|tailrec|this|throw|true|try|typealias|typeof|val|var|vararg|when|where|while)\b")
        };

        // Functions
        grammar["function"] = new GrammarToken[]
        {
            new GrammarToken(@"\b[a-zA-Z_][a-zA-Z0-9_]*\s*(?=\()")
        };

        return grammar;
    }

    /// <summary>
    /// Dart programming language grammar
    /// </summary>
    public static Grammar Dart()
    {
        var grammar = new Grammar();

        // Comments
        grammar["comment"] = new GrammarToken[]
        {
            new GrammarToken(@"//.*|/\*[\s\S]*?\*/", greedy: true)
        };

        // String literals
        grammar["string"] = new GrammarToken[]
        {
            new GrammarToken(@"""(?:[^""\\]|\\.)*""|'(?:[^'\\]|\\.)*'|r""[^""]*""|r'[^']*'", greedy: true)
        };

        // Numbers
        grammar["number"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:0x[0-9a-fA-F]+|\d+(?:\.\d+)?(?:[eE][+-]?\d+)?)\b")
        };

        // Keywords
        grammar["keyword"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:abstract|as|assert|async|await|break|case|catch|class|const|continue|covariant|default|deferred|do|dynamic|else|enum|export|extends|extension|external|factory|false|final|finally|for|Function|get|hide|if|implements|import|in|interface|is|late|library|mixin|new|null|on|operator|part|required|rethrow|return|set|show|static|super|switch|sync|this|throw|true|try|typedef|var|void|while|with|yield)\b")
        };

        // Built-in types
        grammar["builtin"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:bool|double|Duration|dynamic|int|Iterable|List|Map|num|Object|Pattern|RegExp|Set|String|StringBuffer|Type|Uri)\b", alias: new string[] { "class-name" })
        };

        // Functions
        grammar["function"] = new GrammarToken[]
        {
            new GrammarToken(@"\b[a-zA-Z_$][a-zA-Z0-9_$]*\s*(?=\()")
        };

        return grammar;
    }

    /// <summary>
    /// YAML configuration language grammar
    /// </summary>
    public static Grammar Yaml()
    {
        var grammar = new Grammar();

        // Comments
        grammar["comment"] = new GrammarToken[]
        {
            new GrammarToken(@"#.*")
        };

        // Keys
        grammar["key"] = new GrammarToken[]
        {
            new GrammarToken(@"[\w.-]+(?=\s*:(?:\s|$))", alias: new string[] { "atrule" })
        };

        // Strings
        grammar["string"] = new GrammarToken[]
        {
            new GrammarToken(@"""(?:[^""\\]|\\.)*""|'(?:[^'\\]|\\.)*'", greedy: true)
        };

        // Numbers
        grammar["number"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:[-+]?\d+(?:\.\d+)?(?:[eE][-+]?\d+)?|0x[0-9a-fA-F]+|0o[0-7]+|0b[01]+)\b")
        };

        // Booleans and null
        grammar["boolean"] = new GrammarToken[]
        {
            new GrammarToken(@"\b(?:true|false|null|~)\b", alias: new string[] { "important" })
        };

        // Anchors and aliases
        grammar["anchor"] = new GrammarToken[]
        {
            new GrammarToken(@"[&*][a-zA-Z][\w-]*", alias: new string[] { "function" })
        };

        // Tags
        grammar["tag"] = new GrammarToken[]
        {
            new GrammarToken(@"!(?:[a-zA-Z][\w-]*)?", alias: new string[] { "keyword" })
        };

        return grammar;
    }

    /// <summary>
    /// Markdown markup language grammar
    /// </summary>
    public static Grammar Markdown()
    {
        var grammar = new Grammar();

        // Headers
        grammar["title"] = new GrammarToken[]
        {
            new GrammarToken(@"^#{1,6}\s+.+", alias: new string[] { "important" })
        };

        // Bold text
        grammar["bold"] = new GrammarToken[]
        {
            new GrammarToken(@"\*\*(?:(?!\*\*).)+\*\*|__(?:(?!__).)+__", greedy: true)
        };

        // Italic text
        grammar["italic"] = new GrammarToken[]
        {
            new GrammarToken(@"\*(?:(?!\*).)+\*|_(?:(?!_).)+_", greedy: true)
        };

        // Code blocks
        grammar["code"] = new GrammarToken[]
        {
            new GrammarToken(@"`[^`]+`|```[\s\S]*?```", greedy: true, alias: new string[] { "keyword" })
        };

        // Links
        grammar["url"] = new GrammarToken[]
        {
            new GrammarToken(@"\[(?:(?!\]).)*\]\((?:(?!\)).)*\)")
        };

        return grammar;
    }
}
