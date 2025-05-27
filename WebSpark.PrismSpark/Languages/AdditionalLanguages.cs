using System.Text.RegularExpressions;

namespace WebSpark.PrismSpark.Languages;

// From https://github.com/PrismJS/prism/blob/master/components/prism-typescript.js

/// <summary>
/// Provides syntax highlighting grammar definition for TypeScript programming language.
/// Extends JavaScript grammar with TypeScript-specific syntax including type annotations, interfaces, and generics.
/// </summary>
public class TypeScript : IGrammarDefinition
{
    /// <summary>
    /// Defines the grammar rules for TypeScript syntax highlighting.
    /// </summary>
    /// <returns>A Grammar object containing the token definitions for TypeScript language syntax.</returns>
    public Grammar Define()
    {
        var typescript = new JavaScript().Define();

        // Add TypeScript-specific patterns
        typescript.Remove("class-name");
        typescript["class-name"] = new GrammarToken[]
        {
            // Class expressions and declarations
            new(@"(\b(?:class|extends|implements|instanceof|interface|new)\s+)[\w$]+(?:\s*<(?:[^<>]|<(?:[^<>]|<[^<>]*>)*>)*>)?",
                true, inside: new Grammar
                {
                    ["punctuation"] = new GrammarToken[] { new(@"[<>(),.:]") }
                }),
            // Constructor signatures, method return types, etc.
            new(@"(\b(?:constructor|declare|get|set)\s+)[\w$]+",
                true),
            // Type annotations
            new(@"(\:\s*)(?:[A-Z][\w$]*(?:\.[A-Z][\w$]*)*(?:\s*<(?:[^<>]|<(?:[^<>]|<[^<>]*>)*>)*>)?)",
                true)
        };

        // Type annotations
        typescript["type-annotation"] = new GrammarToken[]
        {
            new(@"(\:\s*)(?:[A-Z][\w$]*(?:\.[A-Z][\w$]*)*(?:\s*<(?:[^<>]|<(?:[^<>]|<[^<>]*>)*>)*>)?|[a-z][\w$]*|string|number|boolean|object|void|any|never|unknown)",
                true, alias: new[] { "class-name" })
        };

        // Generic type parameters
        typescript["generic"] = new GrammarToken[]
        {
            new(@"<(?:[^<>]|<(?:[^<>]|<[^<>]*>)*>)*>",
                inside: new Grammar
                {
                    ["class-name"] = new GrammarToken[]
                    {
                        new(@"[A-Z][\w$]*")
                    },
                    ["punctuation"] = new GrammarToken[]
                    {
                        new(@"[<>|&,]")
                    }
                })
        };

        // TypeScript keywords
        typescript["keyword"] = new GrammarToken[]
        {
            new(@"\b(?:abstract|any|as|asserts|async|await|boolean|break|case|catch|class|const|constructor|continue|debugger|declare|default|delete|do|else|enum|export|extends|false|finally|for|from|function|get|if|implements|import|in|instanceof|interface|is|keyof|let|module|namespace|never|new|null|number|object|of|package|private|protected|public|readonly|return|require|set|static|string|super|switch|symbol|this|throw|true|try|type|typeof|undefined|unique|unknown|var|void|while|with|yield)\b")
        };

        // TypeScript operators
        typescript["operator"] = typescript["operator"]
            .Concat(new GrammarToken[]
            {
                new(@"\?\?=?|\?\.|\!\!|\?\:")
            }).ToArray();

        return typescript;
    }
}

// From https://github.com/PrismJS/prism/blob/master/components/prism-php.js

/// <summary>
/// Provides syntax highlighting grammar definition for PHP programming language.
/// Supports PHP syntax including variables, classes, functions, and embedded PHP within HTML markup.
/// </summary>
public class Php : IGrammarDefinition
{
    /// <summary>
    /// Defines the grammar rules for PHP syntax highlighting.
    /// </summary>
    /// <returns>A Grammar object containing the token definitions for PHP language syntax embedded in HTML.</returns>
    public Grammar Define()
    {
        var markup = new Markup().Define();
        var php = new Grammar();

        // PHP tags
        php["delimiter"] = new GrammarToken[]
        {
            new(@"(\<\?(?:php\s|=)?|\?\>)", alias: new[] { "important" })
        };

        php["variable"] = new GrammarToken[]
        {
            new(@"\$+(?:[a-zA-Z_\x7f-\xff][a-zA-Z0-9_\x7f-\xff]*)")
        };

        php["package"] = new GrammarToken[]
        {
            new(@"(\bnamespace\s+)(?:\\?[a-z_][a-z0-9_]*)+(?=\s*[;{])",
                true, inside: new Grammar
                {
                    ["punctuation"] = new GrammarToken[] { new(@"\\") }
                })
        };

        php["class-name-definition"] = new GrammarToken[]
        {
            new(@"(\b(?:class|enum|interface|trait)\s+)[a-z_][a-z0-9_]*",
                true, alias: new[] { "class-name" })
        };

        php["function-definition"] = new GrammarToken[]
        {
            new(@"(\bfunction\s+)[a-z_][a-z0-9_]*(?=\s*\()",
                true, alias: new[] { "function" })
        };

        php["keyword"] = new GrammarToken[]
        {
            new(new Regex(@"\b(?:__halt_compiler|abstract|and|array|as|break|callable|case|catch|class|clone|const|continue|declare|default|die|do|echo|else|elseif|empty|enddeclare|endfor|endforeach|endif|endswitch|endwhile|enum|eval|exit|extends|final|finally|fn|for|foreach|function|global|goto|if|implements|include|include_once|instanceof|insteadof|interface|isset|list|match|namespace|new|or|parent|print|private|protected|public|readonly|require|require_once|return|self|static|switch|throw|trait|try|unset|use|var|while|xor|yield)\b",
                RegexOptions.IgnoreCase))
        }; php["argument-name"] = new GrammarToken[]
        {
            new(@"([(,]\s*)\$[a-z_][a-z0-9_]*(?=\s*[=,)])",
                true, inside: new Grammar
                {
                    ["variable"] = php["variable"]
                })
        };

        php["class-name"] = new GrammarToken[]
        {
            new(@"(\b(?:extends|implements|instanceof|new(?!\s+self\b))\s+|\bcatch\s*\()\b[a-z_][a-z0-9_]*\b",
                true),
            new(@"\b[a-z_][a-z0-9_]*(?=\s*\|)", lookbehind: false),
            new(@"\b[a-z_][a-z0-9_]*(?=\s*\$)", lookbehind: false)
        };

        php["constant"] = new GrammarToken[]
        {
            new(@"\b[A-Z_][A-Z0-9_]*\b")
        };

        php["function"] = new GrammarToken[]
        {
            new(new Regex(@"\\?[a-z_][a-z0-9_]*(?=\s*\()", RegexOptions.IgnoreCase))
        };

        php["property"] = new GrammarToken[]
        {
            new(@"(->\s*)[a-z_][a-z0-9_]*", true)
        };

        php["number"] = new GrammarToken[]
        {
            new(@"\b0b[01]+(?:_[01]+)*\b|\b0o[0-7]+(?:_[0-7]+)*\b|\b0x[\da-f]+(?:_[\da-f]+)*\b|(?:\b\d+(?:_\d+)*\.?(?:\d+(?:_\d+)*)?|\B\.\d+(?:_\d+)*)(?:e[+-]?\d+(?:_\d+)*)?")
        };

        php["operator"] = new GrammarToken[]
        {
            new(@"<?=>|\?\?=?|\.{3}|\?\->|[!=]=?=?|::|\*\*=?|--|\+\+|&&|\|\||<<|>>|[?~]|[/^|%*&<>.+-]=?")
        };

        php["punctuation"] = new GrammarToken[]
        {
            new(@"[{}[\];(),:]")
        };        // Add PHP inside markup
        markup["php"] = new GrammarToken[]
        {
            new(@"<\?(?:[^""'/#]|\/(?![*/])|([""])(?:\\[\s\S]|(?!\1)[^\\])*\1|(?:\/\/|#(?!\[))(?:[^?\n\r]|\?(?!>))*(?=$|\?>|[\r\n])|\/\*[\s\S]*?(?:\*\/|$))*?\?>",
                greedy: true, inside: php)
        };

        return markup;
    }
}

// From https://github.com/PrismJS/prism/blob/master/components/prism-ruby.js

/// <summary>
/// Provides syntax highlighting grammar definition for Ruby programming language.
/// Supports Ruby syntax including classes, modules, symbols, string interpolation, and built-in types.
/// </summary>
public class Ruby : IGrammarDefinition
{
    /// <summary>
    /// Defines the grammar rules for Ruby syntax highlighting.
    /// </summary>
    /// <returns>A Grammar object containing the token definitions for Ruby language syntax.</returns>
    public Grammar Define()
    {
        var ruby = new Grammar();

        ruby["comment"] = new GrammarToken[]
        {
            new(@"#.*", greedy: false),
            new(@"^=begin\s[\s\S]*?^=end", greedy: true, alias: new[] { "multiline" })
        };

        ruby["class-name"] = new GrammarToken[]
        {
            new(@"(\b(?:class|module)\s+)[A-Z]\w*(?:::[A-Z]\w*)*",
                true, inside: new Grammar
                {
                    ["punctuation"] = new GrammarToken[] { new(@"::") }
                }),
            new(@"(\bclass\s+)[a-z]\w*",
                true),
            new(@"\b[A-Z]\w*(?=\s*\.\s*new\b)")
        };

        ruby["keyword"] = new GrammarToken[]
        {
            new(@"\b(?:BEGIN|END|alias|and|begin|break|case|class|def|defined|do|else|elsif|end|ensure|false|for|if|in|module|next|nil|not|or|redo|rescue|retry|return|self|super|then|true|undef|unless|until|when|while|yield)\b")
        };

        ruby["builtin"] = new GrammarToken[]
        {
            new(@"\b(?:Array|Bignum|Binding|Class|Continuation|Dir|Exception|FalseClass|File|Stat|Fixnum|Float|Hash|Integer|IO|MatchData|Method|Module|NilClass|Numeric|Object|Proc|Range|Regexp|String|Struct|TMS|Symbol|ThreadGroup|Thread|Time|TrueClass)\b",
                alias: new[] { "class-name" })
        };

        ruby["constant"] = new GrammarToken[]
        {
            new(@"\b[A-Z]\w*[?!]?\b")
        };

        ruby["symbol"] = new GrammarToken[]
        {
            new(@"(^|[^:]):[a-z_]\w*[!?]?", true)
        };

        ruby["string"] = new GrammarToken[]
        {
            new(@"([""'])(?:(?!\1)[^\\]|\\[\s\S])*\1", greedy: true),
            new(@"%[qQiIwWxsr]?([^a-zA-Z0-9\s])(?:(?!\1)[^\\]|\\[\s\S])*\1", greedy: true),
            new(@"`(?:[^`\\]|\\[\s\S])*`", greedy: true)
        };

        ruby["variable"] = new GrammarToken[]
        {
            new(@"[@$]+[a-z_]\w*[!?]?", alias: new[] { "symbol" })
        };

        ruby["number"] = new GrammarToken[]
        {
            new(@"\b(?:0[box][\da-f]+|\d+(?:\.\d+)?(?:e[+-]?\d+)?)\b", greedy: false, alias: new[] { "number" })
        };

        ruby["operator"] = new GrammarToken[]
        {
            new(@"[+\-*/%]|={1,3}|![=~]?|<{1,2}=?|>{1,2}=?|&{1,2}|\|{1,2}|\?|\^|~")
        };

        ruby["punctuation"] = new GrammarToken[]
        {
            new(@"[{}[\];(),.]")
        };

        return ruby;
    }
}
