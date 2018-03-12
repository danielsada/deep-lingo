using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLingo {

    class Parser {
        private ISet<TokenType> firstOfOperator = new HashSet<TokenType> ();
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
                TokenType.VAR_STRING
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
                TokenType.PARENTHESIS_OPEN,
                TokenType.NOT
            };
        IEnumerator<Token> tokenStream;

        public Parser (IEnumerator<Token> tokenStream) {
            this.tokenStream = tokenStream;
            this.tokenStream.MoveNext ();
            firstOfOperator.UnionWith (firstOfOperatorComp);
            firstOfOperator.UnionWith (firstOfOperatorBool);
            firstOfOperator.UnionWith (firstOfOperatorMath);
        }

        public TokenType CurrentToken {
            get { return tokenStream.Current.Category; }
        }

        public Token Expect (TokenType category) {

            if (CurrentToken == category) {

                Console.WriteLine ($"Success : Expected {category.ToString ()}, got {CurrentToken}, \n Token:{tokenStream.Current.Lexeme} ");
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

        public Node Program () {

            while (CurrentToken == TokenType.VAR) {
                VarDef ();
            }

            while (CurrentToken == TokenType.IDENTIFIER) {
                FunDef ();
                Expect (TokenType.BLOCK_END);
            }

            Expect (TokenType.EOF);
        }

        public Node VarDef () {
            Expect (TokenType.VAR);
            IdList ();
            Expect (TokenType.INSTRUCTION_END);
        }

        public Node FunDef () {
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

        public Node IdList () {
            Expect (TokenType.IDENTIFIER);
            while (CurrentToken == TokenType.LIST) {
                Expect (TokenType.LIST);
                Expect (TokenType.IDENTIFIER);
            }
        }

        public Node Stmt () {
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

        public Node StmtCall () {
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

        public Node If () {
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

        public Node Loop () {
            Expect (TokenType.LOOP);
            Expect (TokenType.BLOCK_BEGIN);
            while (firstOfStatement.Contains (CurrentToken)) {
                Stmt ();
            }
            Expect (TokenType.BLOCK_END);
        }

        public Node Break () {
            Expect (TokenType.BREAK);
            Expect (TokenType.INSTRUCTION_END);
        }

        public Node Assignment () {
            //Expect (TokenType.IDENTIFIER);
            Expect (TokenType.ASSIGN);
            Expression ();
            if (CurrentToken == TokenType.INSTRUCTION_END) {
                Expect (TokenType.INSTRUCTION_END);
            }

        }

        public Node Return () {
            Expect (TokenType.RETURN);
            Expression ();
            Expect (TokenType.INSTRUCTION_END);
        }

        public Node Increment () {
            Expect (TokenType.INCR);
            Expect (TokenType.INSTRUCTION_END);
        }

        public Node Decrement () {
            Expect (TokenType.DECR);
            Expect (TokenType.INSTRUCTION_END);
        }

        public Node FunCall () {
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

        public Node Expression () {
            ExpressionUnary ();
            while (firstOfOperator.Contains (CurrentToken)) {
                Operator ();
                ExpressionUnary ();
            }

        }

        public Node ExpressionUnary () {
            var n1 = new Expression ();
            if (FirstOfExprUnary.Contains (CurrentToken)) {
                switch (CurrentToken) {
                    case TokenType.SUM:
                        n1.Add (new Positive () { AnchorToken = Expect (TokenType.SUM) });
                        break;
                    case TokenType.NOT:
                        n1.Add (new Negative () { AnchorToken = Expect (TokenType.NOT) });
                        break;
                    case TokenType.SUB:
                        n1.Add (new Not () { AnchorToken = Expect (TokenType.SUB) });
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
                case TokenType.TRUE:
                    Expect (TokenType.TRUE);
                    break;
                default:
                    throw new SyntaxError (firstOfSimpleExpression, tokenStream.Current);

            }
        }
        public Node Array () {
            Expect (TokenType.ARR_BEGIN);
            Node n1 = new ArrNode ();
            if (TokenType.ARR_END != CurrentToken) {
                n1.Add (Expression ());
                while (TokenType.LIST == CurrentToken) {
                    Expect (TokenType.LIST);
                    var n2 = Expression ();
                    n1.Add (n2);
                }
            }
            Expect (TokenType.ARR_END);
            return n1;
        }
        public Node Literal () {
            switch (CurrentToken) {
                case TokenType.VAR_INT:
                    return new VarInt () { AnchorToken = Expect (TokenType.VAR_INT) };
                case TokenType.VAR_CHAR:
                    return new VarChar () { AnchorToken = Expect (TokenType.VAR_CHAR) };
                case TokenType.VAR_STRING:
                    return new VarString () { AnchorToken = Expect (TokenType.VAR_STRING) };
                default:
                    throw new SyntaxError (CurrentToken, tokenStream.Current);
            }
        }

        public Node Operator () {
            // Tenemos aqui que regresar algo con el operador de enmedio.
            dynamic oper = null;
            if (firstOfOperatorBool.Contains (CurrentToken)) {
                oper = OperatorBool ();
            } else if (firstOfOperatorComp.Contains (CurrentToken)) {
                oper = OperatorComp ();
            } else if (firstOfOperatorMath.Contains (CurrentToken)) {
                oper = OperatorMath ();
            }
            return oper;
        }
        public Node OperatorBool () {
            switch (CurrentToken) {
                case TokenType.OR:
                    return new Or () { AnchorToken = Expect (TokenType.OR) };
                case TokenType.AND:
                    return new And () { AnchorToken = Expect (TokenType.AND) };
                default:
                    throw new SyntaxError (CurrentToken, tokenStream.Current);
            }
        }
        public Node OperatorComp () {
            switch (CurrentToken) {
                case TokenType.GT:
                    return new Gt () { AnchorToken = Expect (TokenType.GT) };

                case TokenType.GOET:
                    return new Goet () { AnchorToken = Expect (TokenType.GOET) };

                case TokenType.LT:
                    return new Lt () { AnchorToken = Expect (TokenType.LT) };

                case TokenType.LOET:
                    return new Loet () { AnchorToken = Expect (TokenType.LOET) };

                case TokenType.EQUALS:
                    return new Equals () { AnchorToken = Expect (TokenType.EQUALS) };

                case TokenType.NOT_EQUALS:
                    return new Not_Equals () { AnchorToken = Expect (TokenType.NOT_EQUALS) };

                default:
                    throw new SyntaxError (CurrentToken, tokenStream.Current);
            }

        }
        public Node OperatorMath () {
            switch (CurrentToken) {
                case TokenType.SUM:
                    return new Sum () {
                        AnchorToken = Expect (TokenType.SUM)
                    };
                case TokenType.SUB:
                    return new Sub () {
                        AnchorToken = Expect (TokenType.SUB)
                    };
                case TokenType.DIV:
                    return new Div () {
                        AnchorToken = Expect (TokenType.DIV)
                    };
                case TokenType.MUL:
                    return new Mul () {
                        AnchorToken = Expect (TokenType.MUL)
                    };
                case TokenType.MOD:
                    return new Mod () {
                        AnchorToken = Expect (TokenType.MOD)
                    };
                default:
                    throw new SyntaxError (CurrentToken, tokenStream.Current);
            }

        }

    }
}