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

        static readonly ISet<TokenType> firstOfDef =
            new HashSet<TokenType> () {
                TokenType.VAR,
                TokenType.IDENTIFIER
            };

        static readonly ISet<TokenType> firstOfDeclaration =
            new HashSet<TokenType> () {
                TokenType.VAR_CHAR,
                TokenType.VAR_STRING
            };

        static readonly ISet<TokenType> firstOfStatement =
            new HashSet<TokenType> () {
                TokenType.IDENTIFIER,
                TokenType.LOOP,
                TokenType.IF,
                TokenType.BREAK,
                TokenType.RETURN
            };

        static readonly ISet<TokenType> firstOfOperator =
            new HashSet<TokenType> () {
                TokenType.SUM,
                TokenType.MUL,
                TokenType.SUB,
                TokenType.DIV
            };

        static readonly ISet<TokenType> firstOfSimpleExpression =
            new HashSet<TokenType> () {
                TokenType.IDENTIFIER,
                TokenType.VAR_INT,
                TokenType.TRUE,
                TokenType.FALSE,
                TokenType.PARENTHESIS_OPEN
            };
        static readonly ISet<TokenCategory> firstOfExprRel = new HashSet<TokenCategory>(){
            
            TokenCategory.LT,
            TokenCategory.LOET,
            TokenCategory.GT,
            TokenCategory.GOET
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

        public void Program () {

            while (CurrentToken == TokenType.VAR) {
                VarDef ();
            }

            while (CurrentToken == TokenType.IDENTIFIER) {
                FunDef ();
            }

            Expect (TokenType.EOF);
        }

        public void VarDef(){
            Expect(TokenType.VAR);
            IdList();
            Expect(TokenType.INSTRUCTION_END);
        }

        public void FunDef(){
            Expect(TokenType.IDENTIFIER);
            Expect(TokenType.PARENTHESIS_OPEN);
            if(CurrentToken != TokenType.PARENTHESIS_CLOSE){
                IdList();
            }
            Expect(TokenType.PARENTHESIS_CLOSE);
            Expect(TokenType.BLOCK_BEGIN);
            while(CurrentToken == TokenType.VAR){
                VarDef();
            }



        }

        public void IdList(){
            Expect(TokenType.IDENTIFIER);
            while(CurrentToken == TokenType.LIST){
                Expect(TokenType.IDENTIFIER);
            }
        }



        // A PARTIR DE AQUI ES CODIGO EJEMPLO
        // DEL PROFE


        public void Declaration () {
            Type ();
            Expect (TokenType.IDENTIFIER);
        }

        public void Statement () {

            switch (CurrentToken) {

                case TokenType.IDENTIFIER:
                    Assignment ();
                    break;

                case TokenType.PRINT:
                    Print ();
                    break;

                case TokenType.IF:
                    If ();
                    break;

                default:
                    throw new SyntaxError (firstOfStatement,
                        tokenStream.Current);
            }
        }

        public void Type () {
            switch (CurrentToken) {

                case TokenType.INT:
                    Expect (TokenType.INT);
                    break;

                case TokenType.BOOL:
                    Expect (TokenType.BOOL);
                    break;

                default:
                    throw new SyntaxError (firstOfDeclaration,
                        tokenStream.Current);
            }
        }

        public void Assignment () {
            Expect (TokenType.IDENTIFIER);
            Expect (TokenType.ASSIGN);
            Expression ();
        }

        public void Print () {
            Expect (TokenType.PRINT);
            Expression ();
        }

        public void If () {
            Expect (TokenType.IF);
            Expression ();
            Expect (TokenType.THEN);
            while (firstOfStatement.Contains (CurrentToken)) {
                Statement ();
            }
            Expect (TokenType.END);
        }

        public void Expression () {
            SimpleExpression ();
            while (firstOfOperator.Contains (CurrentToken)) {
                Operator ();
                SimpleExpression ();
            }
        }

        public void SimpleExpression () {

            switch (CurrentToken) {

                case TokenType.IDENTIFIER:
                    Expect (TokenType.IDENTIFIER);
                    break;

                case TokenType.VAR_INT:
                    Expect (TokenType.INT_LITERAL);
                    break;

                case TokenType.TRUE:
                    Expect (TokenType.TRUE);
                    break;

                case TokenType.FALSE:
                    Expect (TokenType.FALSE);
                    break;

                case TokenType.PARENTHESIS_OPEN:
                    Expect (TokenType.PARENTHESIS_OPEN);
                    Expression ();
                    Expect (TokenType.PARENTHESIS_CLOSE);
                    break;

                case TokenType.NEG:
                    Expect (TokenType.NEG);
                    SimpleExpression ();
                    break;

                default:
                    throw new SyntaxError (firstOfSimpleExpression,
                        tokenStream.Current);
            }
        }

        public void Operator () {

            switch (CurrentToken) {

                case TokenType.AND:
                    Expect (TokenType.AND);
                    break;

                case TokenType.LESS:
                    Expect (TokenType.LESS);
                    break;

                case TokenType.PLUS:
                    Expect (TokenType.PLUS);
                    break;

                case TokenType.MUL:
                    Expect (TokenType.MUL);
                    break;

                default:
                    throw new SyntaxError (firstOfOperator,
                        tokenStream.Current);
            }
        }
    }

    class SyntaxError : System.Exception {
        public SyntaxError (dynamic category, Token tok) {

        }
    }
}