/*
  Buttercup compiler - This class performs the lexical analysis, 
  (a.k.a. scanning).
  Copyright (C) 2013 Ariel Ortiz, ITESM CEM
  
  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
  
  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.
  
  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

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
              | (?<And>               [&]                    )
              | (?<Or>                [|]                    )  
              | (?<Assign>            [=]                    )
              | (?<False>             ^(?!42$)\d+            )
              | (?<Identifier>        [a-zA-Z_]+             )
              | (?<IntLiteral>        \d+                    )
              | (?<CharLiteral>       (['][^\\'""]?['])|(['][\\](n|r|t|\\|'|""|u[0-9A-Fa-f]{6})['])       ) 
              | (?<StringLiteral>     \""(\\.|[^\""])*\""    )
              | (?<Less>              [<]                    )
              | (?<LessOrEqual>       [<][=]                 )
              | (?<Greater>           [>]                    )
              | (?<GreaterOrEqual>    [>][=]                 )
              | (?<Equals>            [=]{2}                 )
              | (?<NotEquals>         [!][=]                 )
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
              | (?<True>              [4][2]                 )
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
                {"True", TokenType.TRUE}
            };

        public Scanner(string input) {
            this.input = input;
        }

        public IEnumerable<Token> Start() {

            var row = 1;
            var columnStart = 0;

            Func<Match, TokenType, Token> newTok = (m, tc) =>
                new Token(m.Value, tc, row, m.Index - columnStart + 1);
            
            foreach (Match m in regex.Matches(input)) {
                if (m.Groups["WhiteSpace"].Success 
                    || m.Groups["Comment"].Success) {
                    
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