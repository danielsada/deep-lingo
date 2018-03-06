/*
  Buttercup compiler - This class performs the syntactic analysis,
  (a.k.a. parsing).
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

namespace DeepLingo {

    class Parser {

        static readonly ISet<TokenType> firstOfStatement =
            new HashSet<TokenType> () {
                TokenType.IDENTIFIER,
                TokenType.LOOP,
                TokenType.IF,
                TokenType.BREAK,
                TokenType.RETURN
            };

        static readonly ISet<TokenType> FirstOfExprUnary =
            new HashSet<TokenType> () {
                TokenType.NOT,
                TokenType.SUM,
                TokenType.SUB,
                TokenType.IDENTIFIER,
                TokenType.VAR_INT,
                TokenType.VAR_CHAR,
                TokenType.VAR_STRING,
            };
        static readonly ISet<TokenType> firstOfOperator =
            new HashSet<TokenType> () {
                TokenType.SUM,
                TokenType.MUL,
                TokenType.SUB,
                TokenType.DIV,
                TokenType.LT,
                TokenType.LOET,
                TokenType.GT,
                TokenType.GOET,
                TokenType.EQUALS
            };
        static readonly ISet<TokenType> firstOfOperatorBool =
            new HashSet<TokenType> () {
                TokenType.OR,
                TokenType.AND
            };
        static readonly ISet<TokenType> firstOfOperatorComp =
            new HashSet<TokenType> () {
                TokenType.LT,
                TokenType.LOET,
                TokenType.GT,
                TokenType.GOET,
                TokenType.EQUALS,
                TokenType.NOT_EQUALS
            };
        static readonly ISet<TokenType> firstOfOperatorMath =
            new HashSet<TokenType> () {
                TokenType.SUM,
                TokenType.MUL,
                TokenType.SUB,
                TokenType.DIV,
                TokenType.MOD
            };
        static readonly ISet<TokenType> firstOfSimpleExpression =
            new HashSet<TokenType> () {
                TokenType.IDENTIFIER,
                TokenType.VAR_INT,
                TokenType.VAR_CHAR,
                TokenType.VAR_STRING,
                TokenType.TRUE,
                TokenType.FALSE,
                TokenType.PARENTHESIS_OPEN
            };
        IEnumerator<Token> tokenStream;

        public Parser (IEnumerator<Token> tokenStream) {
            this.tokenStream = tokenStream;
            this.tokenStream.MoveNext ();
        }

        public TokenType CurrentToken {
            get { return tokenStream.Current.Category; }
        }

        public Token Expect (TokenType category) {

            if (CurrentToken == category) {
                Console.WriteLine ($"Success: Expected {category.ToString()}, got {CurrentToken}");
                Token current = tokenStream.Current;
                tokenStream.MoveNext ();
                return current;
            } else {
                Console.WriteLine ($"Error row {tokenStream.Current.Row} Error Info {tokenStream.Current.Lexeme}");
                Console.WriteLine ($"Failure: Expected {category.ToString()}, got {CurrentToken}");
                Console.WriteLine ("Next ten tokens\n");
                for (int i = 0; i < 10; i++) {
                    tokenStream.MoveNext ();
                    Console.WriteLine ($"[{i}] {tokenStream.Current.Lexeme} \n ");
                }
                throw new SyntaxError (category, tokenStream.Current);
            }

        }
        public Token ExpectSet (ISet<TokenType> category) {
            if (category.Contains (CurrentToken)) {
                Token current = tokenStream.Current;
                tokenStream.MoveNext ();
                return current;
            } else {
                throw new SyntaxError (category, tokenStream.Current);
            }
        }

        public void Program () {

            while (CurrentToken == TokenType.VAR) {
                VarDef ();
            }

            while (CurrentToken == TokenType.IDENTIFIER) {
                FunDef ();
                Expect (TokenType.BLOCK_END);
            }
            
            Expect (TokenType.EOF);
        }

        public void VarDef () {
            Expect (TokenType.VAR);
            IdList ();
            Expect (TokenType.INSTRUCTION_END);
        }

        public void FunDef () {
            Expect (TokenType.IDENTIFIER);
            Expect (TokenType.PARENTHESIS_OPEN);
            if (CurrentToken != TokenType.PARENTHESIS_CLOSE) {
                IdList ();
            }
            Expect (TokenType.PARENTHESIS_CLOSE);
            Expect (TokenType.BLOCK_BEGIN);
            while (CurrentToken == TokenType.VAR) {
                VarDef ();
            }
            while (firstOfStatement.Contains (CurrentToken)) {
                Stmt ();
            }

        }

        public void IdList () {
            Expect (TokenType.IDENTIFIER);
            while (CurrentToken == TokenType.LIST) {
                Expect (TokenType.LIST);
                Expect (TokenType.IDENTIFIER);
            }
        }

        public void Stmt () {
            switch (CurrentToken) {
                case TokenType.IDENTIFIER:
                    if (CurrentToken == TokenType.PARENTHESIS_OPEN) {
                        FunCall ();
                    } else {
                        StmtCall ();
                    }
                    break;
                case TokenType.IF:
                    If ();
                    break;
                case TokenType.LOOP:
                    Loop ();
                    break;
                case TokenType.BREAK:
                    Break ();
                    break;
                case TokenType.RETURN:
                    Return ();
                    break;
                case TokenType.INSTRUCTION_END:
                    Expect (TokenType.INSTRUCTION_END);
                    break;
                default:
                    throw new SyntaxError (CurrentToken, tokenStream.Current);
            }
        }

        public void StmtCall () {
            Expect (TokenType.IDENTIFIER);
            switch (CurrentToken) {
                case (TokenType.INCR):
                    Increment ();
                    break;
                case (TokenType.DECR):
                    Decrement ();
                    break;
                case (TokenType.ASSIGN):
                    Assignment ();
                    break;
                case (TokenType.PARENTHESIS_OPEN):
                    FunCall ();
                    Expect (TokenType.INSTRUCTION_END);
                    break;
            }

        }

        public void If () {
            Expect (TokenType.IF);
            Expect (TokenType.PARENTHESIS_OPEN);
            Expression ();
            Expect (TokenType.PARENTHESIS_CLOSE);
            Expect (TokenType.BLOCK_BEGIN);
            while (firstOfStatement.Contains (CurrentToken)) {
                Stmt ();
            }
            Expect (TokenType.BLOCK_END);
            while (CurrentToken == TokenType.ELSEIF) {
                Expect (TokenType.ELSEIF);
                Expect (TokenType.PARENTHESIS_OPEN);
                Expression ();
                Expect (TokenType.PARENTHESIS_CLOSE);
                Expect (TokenType.BLOCK_BEGIN);
                while (firstOfStatement.Contains (CurrentToken)) {
                    Stmt ();
                }
                Expect (TokenType.BLOCK_END);
            }
            if (CurrentToken == TokenType.ELSE) {
                Expect (TokenType.ELSE);
                Expect (TokenType.BLOCK_BEGIN);
                while (firstOfStatement.Contains (CurrentToken)) {
                    Stmt ();
                }
                Expect (TokenType.BLOCK_END);
            }
        }

        public void Loop () {
            Expect (TokenType.LOOP);
            Expect (TokenType.BLOCK_BEGIN);
            while (firstOfStatement.Contains (CurrentToken)) {
                Stmt ();
            }
            Expect (TokenType.BLOCK_END);
        }

        public void Break () {
            Expect (TokenType.BREAK);
            Expect (TokenType.INSTRUCTION_END);
        }

        public void Assignment () {
            //Expect (TokenType.IDENTIFIER);
            Expect (TokenType.ASSIGN);
            Expression ();
            if (CurrentToken == TokenType.INSTRUCTION_END) {
                Expect (TokenType.INSTRUCTION_END);
            }

        }

        public void Return () {
            Expect (TokenType.RETURN);
            Expression ();
            Expect (TokenType.INSTRUCTION_END);
        }

        public void Increment () {
            Expect (TokenType.INCR);
            Expect (TokenType.INSTRUCTION_END);
        }

        public void Decrement () {
            Expect (TokenType.DECR);
            Expect (TokenType.INSTRUCTION_END);
        }

        public void FunCall () {
            Expect (TokenType.PARENTHESIS_OPEN);
            while (firstOfSimpleExpression.Contains (CurrentToken)) {
                Expression ();
                while (CurrentToken == TokenType.LIST) {
                    Expect (TokenType.LIST);
                    Expression ();
                }
            }
            Expect (TokenType.PARENTHESIS_CLOSE);
            //Expect (TokenType.INSTRUCTION_END);
        }

        public void Expression () {
            ExpressionUnary ();
            while (firstOfOperator.Contains (CurrentToken)) {
                Operator ();
                ExpressionUnary ();
            }

        }

        public void ExpressionUnary () {
            if (FirstOfExprUnary.Contains (CurrentToken)) {
                switch (CurrentToken) {
                    case TokenType.SUM:
                        Expect (TokenType.SUM);
                        break;
                    case TokenType.NOT:
                        Expect (TokenType.NOT);
                        break;
                    case TokenType.SUB:
                        Expect (TokenType.SUB);
                        break;
                    default:
                        break;

                }
            }
            switch (CurrentToken) {
                case TokenType.IDENTIFIER:
                    Expect (TokenType.IDENTIFIER);
                    if (CurrentToken == TokenType.PARENTHESIS_OPEN) {
                        FunCall ();
                    }
                    break;
                case TokenType.PARENTHESIS_OPEN:
                    FunCall ();
                    break;
                case TokenType.ARR_BEGIN:
                    Array ();
                    break;
                case TokenType.VAR_CHAR:
                case TokenType.VAR_INT:
                case TokenType.VAR_STRING:
                    Literal ();
                    break;
                default:
                    break;
            }
        }
        public void Array () {
            Expect (TokenType.ARR_BEGIN);
            if (TokenType.ARR_END != CurrentToken) {
                Expression ();
                while (TokenType.LIST == CurrentToken) {
                    Expect (TokenType.LIST);
                    Expression ();
                }
            }

            Expect (TokenType.ARR_END);
        }
        public void Literal () {
            switch (CurrentToken) {
                case TokenType.VAR_INT:
                    Expect (TokenType.VAR_INT);
                    break;
                case TokenType.VAR_CHAR:
                    Expect (TokenType.VAR_CHAR);
                    break;
                case TokenType.VAR_STRING:
                    Expect (TokenType.VAR_STRING);
                    break;
                default:
                    break;
            }
        }

        public void Operator () {
            if (firstOfOperatorBool.Contains (CurrentToken)) {
                OperatorBool ();
            } else if (firstOfOperatorComp.Contains (CurrentToken)) {
                OperatorComp ();
            } else if (firstOfOperatorMath.Contains (CurrentToken)) {
                OperatorMath ();
            }

        }
        public void OperatorBool () {
            while (firstOfOperatorBool.Contains (CurrentToken)) {
                switch (CurrentToken) {
                    case TokenType.OR:
                        Expect (TokenType.OR);
                        break;
                    case TokenType.AND:
                        Expect (TokenType.AND);
                        break;
                    default:
                        break;
                }
                Expression ();
            }
        }
        public void OperatorComp () {
            while (firstOfOperatorComp.Contains (CurrentToken)) {
                switch (CurrentToken) {
                    case TokenType.GT:
                        Expect (TokenType.GT);
                        break;
                    case TokenType.GOET:
                        Expect (TokenType.GOET);
                        break;
                    case TokenType.LT:
                        Expect (TokenType.LT);
                        break;
                    case TokenType.LOET:
                        Expect (TokenType.LOET);
                        break;
                    case TokenType.EQUALS:
                        Expect (TokenType.EQUALS);
                        break;
                    case TokenType.NOT_EQUALS:
                        Expect (TokenType.NOT_EQUALS);
                        break;
                    default:
                        break;
                }
                Expression ();
            }
        }
        public void OperatorMath () {
            while (firstOfOperatorMath.Contains (CurrentToken)) {
                switch (CurrentToken) {
                    case TokenType.SUM:
                        Expect (TokenType.SUM);
                        break;
                    case TokenType.SUB:
                        Expect (TokenType.SUB);
                        break;
                    case TokenType.DIV:
                        Expect (TokenType.DIV);
                        break;
                    case TokenType.MUL:
                        Expect (TokenType.MUL);
                        break;
                    case TokenType.MOD:
                        Expect (TokenType.MOD);
                        break;
                    default:
                        break;
                }
                Expression ();
            }
        }

        // A PARTIR DE AQUI ES CODIGO EJEMPLO
        // DEL PROFE

        //     public void Declaration () {
        //         Type ();
        //         Expect (TokenType.IDENTIFIER);
        //     }

        //     public void Type () {
        //         switch (CurrentToken) {

        //             case TokenType.INT:
        //                 Expect (TokenType.INT);
        //                 break;

        //             case TokenType.BOOL:
        //                 Expect (TokenType.BOOL);
        //                 break;

        //             default:
        //                 throw new SyntaxError (firstOfDeclaration,
        //                     tokenStream.Current);
        //         }
        //     }

        //     // public void Expression () {
        //     //     SimpleExpression ();
        //     //     while (firstOfOperator.Contains (CurrentToken)) {
        //     //         Operator ();
        //     //         SimpleExpression ();
        //     //     }
        //     // }

        //     public void SimpleExpression () {

        //         switch (CurrentToken) {

        //             case TokenType.IDENTIFIER:
        //                 Expect (TokenType.IDENTIFIER);
        //                 break;

        //             case TokenType.VAR_INT:
        //                 Expect (TokenType.INT_LITERAL);
        //                 break;

        //             case TokenType.TRUE:
        //                 Expect (TokenType.TRUE);
        //                 break;

        //             case TokenType.FALSE:
        //                 Expect (TokenType.FALSE);
        //                 break;

        //             case TokenType.PARENTHESIS_OPEN:
        //                 Expect (TokenType.PARENTHESIS_OPEN);
        //                 Expression ();
        //                 Expect (TokenType.PARENTHESIS_CLOSE);
        //                 break;

        //             case TokenType.NEG:
        //                 Expect (TokenType.NEG);
        //                 SimpleExpression ();
        //                 break;

        //             default:
        //                 throw new SyntaxError (firstOfSimpleExpression,
        //                     tokenStream.Current);
        //         }
        //     }

        //     public void Operator () {

        //         switch (CurrentToken) {

        //             case TokenType.AND:
        //                 Expect (TokenType.AND);
        //                 break;

        //             case TokenType.LESS:
        //                 Expect (TokenType.LESS);
        //                 break;

        //             case TokenType.PLUS:
        //                 Expect (TokenType.PLUS);
        //                 break;

        //             case TokenType.MUL:
        //                 Expect (TokenType.MUL);
        //                 break;

        //             default:
        //                 throw new SyntaxError (firstOfOperator,
        //                     tokenStream.Current);
        //         }
        //     }
        // }

        class SyntaxError : System.Exception {
            public SyntaxError (dynamic category, Token tok) {

            }
        }
        //}
    }
}