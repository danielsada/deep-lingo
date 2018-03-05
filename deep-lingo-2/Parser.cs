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
                TokenType.GOET
            };

        static readonly ISet<TokenType> firstOfSimpleExpression =
            new HashSet<TokenType> () {
                TokenType.IDENTIFIER,
                TokenType.VAR_INT,
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
                Token current = tokenStream.Current;
                tokenStream.MoveNext ();
                return current;
            } else {
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
                Expect (TokenType.IDENTIFIER);
            }
        }

        public void Stmt () {
            switch (CurrentToken) {
                case TokenType.IDENTIFIER:
                    StmtCall ();
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
            Expect(TokenType.BLOCK_END);
        }

        public void Break () {
            Expect (TokenType.BREAK);
            Expect (TokenType.INSTRUCTION_END);
        }

        public void Assignment () {
            Expect (TokenType.IDENTIFIER);
            Expect (TokenType.ASSIGN);
            Expression ();
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
            }
        }

        public void Expression () {
            ExpressionUnary ();
            while (firstOfOperator.Contains (CurrentToken)) {
                Operator ();
                ExpressionUnary ();
            }

        }

        public void ExpressionUnary () {
            if(FirstOfExprUnary.Contains(CurrentToken) ){
                switch (CurrentToken){
                    case TokenType.SUM:
                        Expect(TokenType.SUM);
                    break;
                    case TokenType.TokenType.NEG:
                        Expect(TokenType.NEG);
                    break;
                    case TokenType.SUB:
                        Expect(TokenType.SUB);
                    break;
                    case TokenType.SUM:
                        Expect(TokenType.SUM);
                    break;
                    default:
                    break;

                }
            }
        }
        public void Array(){
            Expect(TokenType.ARR_BEGIN);
            while (firstOfSimpleExpression.Contains(CurrentToken)){
                Expression();
            }
            Expect(TokenType.ARR_END);
        }
        public void Literal(){
            switch (CurrentToken){
                case TokenType.VAR_INT:
                    Expect(TokenType.VAR_INT);
                break;
                case TokenType.VAR_CHAR:
                    Expect(TokenType.VAR_CHAR);
                break;
                case TokenType.VAR_STRING:
                    Expect(TokenType.VAR_STRING);
                break;
                default:
            }
        }

        public void Operator () {
            ExpectSet (firstOfOperator);
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
            }
    }
}