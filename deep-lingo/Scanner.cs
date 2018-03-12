
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DeepLingo {

    class Scanner {

        readonly string input;

        static readonly Regex regex = new Regex(
            @"
                (?<Comment>           (\/\/)(.*)|(\/\*)((.|\n)*)(\*\/))                             
              | (?<And>               [&]{2}                    )
              | (?<Or>                [|]{2}                    )
              | (?<LessOrEqual>       [<][=]                 )
              | (?<Less>              [<]                    )
              | (?<GreaterOrEqual>    [>][=]                 )          
              | (?<Greater>           [>]                    )
              | (?<NotEquals>         [!][=]                 )  
              | (?<Equals>            [=]{2}                 )
              | (?<Assign>            [=]                    )
              | (?<True>              [4][2]                 )
              | (?<False>             ^(?!42$)\d+            )
              | (?<Var>               [v][a][r]                        )
              | (?<Identifier>        [a-zA-Z_0-9]+             )
              | (?<IntLiteral>        \d+                    )
              | (?<CharLiteral>       (['][^\\'""]?['])|(['][\\](n|r|t|\\|'|""|u[0-9A-Fa-f]{6})['])       ) 
              | (?<StringLiteral>     \""(\\.|[^\""])*\""    )
              | (?<Incr>               [+]{2}                    )
              | (?<Decr>               [-]{2}                    )
              | (?<Mul>               [*]                    )
              | (?<Sub>               [-]                    )
              | (?<Neg>               [!]                    )
              | (?<Mod>               [%]                    )
              | (?<Div>               [/]                    )
              | (?<Newline>           \n                     )
              | (?<ParLeft>           [(]                    )
              | (?<ParRight>          [)]                    )
              | (?<BlockBegin>        [{]                    )
              | (?<BlockEnd>          [}]                    )
              | (?<InstrEnd>          [;]                    )
              | (?<ArrBegin>          [\[]                   )
              | (?<ArrEnd>            [\]]                   )
              | (?<ListSeparator>     [,]                    )
              | (?<Plus>              [+]                    )              
              | (?<WhiteSpace>        \s                     )     
              | (?<Other>             .                      )  
            ", 
            RegexOptions.IgnorePatternWhitespace 
                | RegexOptions.Compiled
                | RegexOptions.Multiline
            );
        //Keywords Finished
        static readonly IDictionary<string, TokenType> keywords =
            new Dictionary<string, TokenType>() {
                {"break", TokenType.BREAK},
                {"loop", TokenType.LOOP},
                {"else", TokenType.ELSE},
                {"return", TokenType.RETURN},
                {"elseif", TokenType.ELSEIF},
                {"var", TokenType.VAR},
                {"if", TokenType.IF}
            };

        static readonly IDictionary<string, TokenType> nonKeywords =
            new Dictionary<string, TokenType>() {
                {"And", TokenType.AND},
                {"Or", TokenType.OR},
                {"Assign", TokenType.ASSIGN},
                {"False", TokenType.FALSE},
                {"Var", TokenType.VAR},
                {"IntLiteral", TokenType.VAR_INT},
                {"CharLiteral", TokenType.VAR_CHAR},
                {"StringLiteral", TokenType.VAR_STRING},
                {"Array", TokenType.ARRAY},
                {"Less", TokenType.LT},
                {"LessOrEqual", TokenType.LOET},
                {"Greater", TokenType.GT},
                {"GreaterOrEqual", TokenType.GOET},
                {"Equals", TokenType.EQUALS},
                {"NotEquals", TokenType.NOT_EQUALS},
                {"Mul", TokenType.MUL},
                {"Neg", TokenType.NOT},
                {"Mod", TokenType.MOD},
                {"Div", TokenType.DIV},
                {"Sub", TokenType.SUB},
                {"ParLeft", TokenType.PARENTHESIS_OPEN},
                {"ParRight", TokenType.PARENTHESIS_CLOSE},
                {"BlockBegin", TokenType.BLOCK_BEGIN},
                {"BlockEnd", TokenType.BLOCK_END},
                {"ArrBegin", TokenType.ARR_BEGIN},
                {"ArrEnd", TokenType.ARR_END},
                {"ListSeparator", TokenType.LIST},
                {"InstrEnd", TokenType.INSTRUCTION_END},
                {"Plus", TokenType.SUM},
                {"True", TokenType.TRUE},
                {"Incr", TokenType.INCR},
                {"Decr", TokenType.DECR}
            };

        public Scanner(string input) {
            this.input = input;
        }

        public IEnumerable<Token> Start() {

            var row = 1;
            var columnStart = 0;
            Console.WriteLine(regex.Matches(input));
            Func<Match, TokenType, Token> newTok = (m, tc) =>
                new Token(m.Value, tc, row, m.Index - columnStart + 1);
            
            foreach (Match m in regex.Matches(input)) {
                if (m.Groups["WhiteSpace"].Success
                    || m.Groups["Comment"].Success) {
                    row += m.Value.Split('\n').Length-1;
                    // Skip white space and comments.

                } else if (m.Groups["Newline"].Success) {

                    // Found a new line.
                    row++;
                    columnStart = m.Index + m.Length;

                } else if (m.Groups["WhiteSpace"].Success 
                    || m.Groups["Comment"].Success) {
                    
                    // Skip white space and comments.

                } else if (m.Groups["Identifier"].Success) {

                    if (keywords.ContainsKey(m.Value)) {

                        // Matched string is a Buttercup keyword.
                        yield return newTok(m, keywords[m.Value]);                                               

                    } else { 

                        // Otherwise it's just a plain identifier.
                        yield return newTok(m, TokenType.IDENTIFIER);
                    }

                } else if (m.Groups["Other"].Success) {

                    // Found an illegal character.
                    yield return newTok(m, TokenType.ILLEGAL_CHAR);

                } else {

                    // Match must be one of the non keywords.
                    foreach (var name in nonKeywords.Keys) {
                        if (m.Groups[name].Success) {
                            yield return newTok(m, nonKeywords[name]);
                            break;
                        }
                    }
                }
            }

            yield return new Token(null, 
                                   TokenType.EOF, 
                                   row, 
                                   input.Length - columnStart + 1);
        }
    }
}
