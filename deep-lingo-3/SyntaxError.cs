        using System.Collections.Generic;
        using System.Text;
        using System;
        namespace DeepLingo {

            class SyntaxError : Exception {
                public SyntaxError (TokenType category, Token tok):
                    base (String.Format (
                        "Syntax Error: Expecting {0} \n" +
                        "but found {1} (\"{2}\") at row {3}, column {4}.",
                        category,
                        tok.Category,
                        tok.Lexeme,
                        tok.Row,
                        tok.Column)) { }

                public SyntaxError (ISet<TokenType> expectedCategories,
                        Token token):
                    base (String.Format (
                        "Syntax Error: Expecting one of {0}\n" +
                        "but found {1} (\"{2}\") at row {3}, column {4}.",
                        Elements (expectedCategories),
                        token.Category,
                        token.Lexeme,
                        token.Row,
                        token.Column)) { }
                static string Elements (ISet<TokenType> expectedCategories) {
                    var sb = new StringBuilder ("{");
                    var first = true;
                    foreach (var elem in expectedCategories) {
                        if (first) {
                            first = false;
                        } else {
                            sb.Append (", ");
                        }
                        sb.Append (elem);
                    }
                    sb.Append ("}");
                    return sb.ToString ();
                }
            }
        }