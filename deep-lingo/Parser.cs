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

                // Console.WriteLine ($"Success : Expected {category.ToString ()}, got {CurrentToken}, \n Token:{tokenStream.Current.Lexeme} ");
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
            var programNode = new Prog ();
            while (CurrentToken == TokenType.VAR) {
                programNode.Add (VarDef ());
            }

            while (CurrentToken == TokenType.IDENTIFIER) {
                programNode.Add (FunDef ());
                Expect (TokenType.BLOCK_END);
            }

            Expect (TokenType.EOF);
            foreach (var child in programNode.children) {
                Console.WriteLine (child);
            }
            // Console.WriteLine (programNode.ToString ());
            return programNode;
        }

        public Node VarDef () {
            var vardef = new VarDef () { AnchorToken = Expect (TokenType.VAR) };
            vardef.Add (IdList ());
            Expect (TokenType.INSTRUCTION_END);
            return vardef;
        }

        public Node FunDef () {
            var fundef = new FunDef ();
            Expect (TokenType.IDENTIFIER);
            Expect (TokenType.PARENTHESIS_OPEN);
            if (CurrentToken != TokenType.PARENTHESIS_CLOSE) {
                fundef.Add (IdList ());
            }
            Expect (TokenType.PARENTHESIS_CLOSE);
            Expect (TokenType.BLOCK_BEGIN);
            while (CurrentToken == TokenType.VAR) {
                fundef.Add (VarDef ());
            }
            while (firstOfStatement.Contains (CurrentToken)) {
                fundef.Add (Stmt ());
            }
            return fundef;

        }

        public Node IdList () {
            var n1 = new IdList ();
            n1.Add (new Id () { AnchorToken = Expect (TokenType.IDENTIFIER) });
            while (CurrentToken == TokenType.LIST) {
                Expect (TokenType.LIST);
                n1.Add (new Id () { AnchorToken = Expect (TokenType.IDENTIFIER) });
            }
            return n1;
        }

        public Node Stmt () {
            var n1 = new Stmt ();
            switch (CurrentToken) {
                case TokenType.IDENTIFIER:
                    if (CurrentToken == TokenType.PARENTHESIS_OPEN) {
                        n1.Add (FunCall ());
                    } else {
                        n1.Add (StmtCall ());
                    }
                    break;
                case TokenType.IF:
                    n1.Add (If ());
                    break;
                case TokenType.LOOP:
                    n1.Add (Loop ());
                    break;
                case TokenType.BREAK:
                    n1.Add (Break ());
                    break;
                case TokenType.RETURN:
                    n1.Add (Return ());
                    break;
                case TokenType.INSTRUCTION_END:
                    Expect (TokenType.INSTRUCTION_END);
                    break;
                default:
                    throw new SyntaxError (CurrentToken, tokenStream.Current);
            }
            return n1;
        }

        public Node StmtCall () {
            var n1 = new StmtCall ();
            Expect (TokenType.IDENTIFIER);
            switch (CurrentToken) {
                case (TokenType.INCR):
                    n1.Add (Increment ());
                    break;
                case (TokenType.DECR):
                    n1.Add (Decrement ());
                    break;
                case (TokenType.ASSIGN):
                    n1.Add (Assignment ());
                    break;
                case (TokenType.PARENTHESIS_OPEN):
                    n1.Add (FunCall ());
                    Expect (TokenType.INSTRUCTION_END);
                    break;
            }
            return n1;

        }

        public Node If () {
            var n1 = new If ();
            n1.AnchorToken = Expect (TokenType.IF);
            Expect (TokenType.PARENTHESIS_OPEN);
            n1.Add (Expression ());
            Expect (TokenType.PARENTHESIS_CLOSE);
            Expect (TokenType.BLOCK_BEGIN);
            var n2 = new StatementList ();
            while (firstOfStatement.Contains (CurrentToken)) {
                n2.Add (Stmt ());
            }
            n1.Add (n2);
            var n3 = new ElseIfList ();
            Expect (TokenType.BLOCK_END);
            while (CurrentToken == TokenType.ELSEIF) {
                var nelsif = new ElseIf ();
                nelsif.AnchorToken = Expect (TokenType.ELSEIF);
                Expect (TokenType.PARENTHESIS_OPEN);
                nelsif.Add (Expression ());
                Expect (TokenType.PARENTHESIS_CLOSE);
                Expect (TokenType.BLOCK_BEGIN);
                var stmtList = new StatementList ();
                while (firstOfStatement.Contains (CurrentToken)) {
                    stmtList.Add (Stmt ());
                }
                nelsif.Add (stmtList);
                n3.Add (nelsif);
                Expect (TokenType.BLOCK_END);
            }
            n1.Add (n3);
            var els = new Else ();
            if (CurrentToken == TokenType.ELSE) {
                els.AnchorToken = Expect (TokenType.ELSE);
                Expect (TokenType.BLOCK_BEGIN);
                var stmtList = new StatementList ();
                while (firstOfStatement.Contains (CurrentToken)) {
                    stmtList.Add (Stmt ());
                }
                els.Add (stmtList);
                Expect (TokenType.BLOCK_END);
            }
            n1.Add (els);
            return n1;
        }

        public Node Loop () {
            var n1 = new Loop ();
            Expect (TokenType.LOOP);
            Expect (TokenType.BLOCK_BEGIN);
            while (firstOfStatement.Contains (CurrentToken)) {
                n1.Add (Stmt ());
            }
            Expect (TokenType.BLOCK_END);
            return n1;
        }

        public Node Break () {
            var n1 = new Break () { AnchorToken = Expect (TokenType.BREAK) };
            Expect (TokenType.INSTRUCTION_END);
            return n1;
        }

        public Node Assignment () {
            //Expect (TokenType.IDENTIFIER);
            var n1 = new Assignment () { AnchorToken = Expect (TokenType.ASSIGN) };
            n1.Add (Expression ());
            // TODO: See if we remove this.
            if (CurrentToken == TokenType.INSTRUCTION_END) {
                Expect (TokenType.INSTRUCTION_END);
            }
            return n1;

        }

        public Node Return () {
            var n1 = new Return () { AnchorToken = Expect (TokenType.RETURN) };
            n1.Add (Expression ());
            Expect (TokenType.INSTRUCTION_END);
            return n1;
        }

        public Node Increment () {
            var n1 = new Increment () { AnchorToken = Expect (TokenType.INCR) };
            Expect (TokenType.INSTRUCTION_END);
            return n1;
        }

        public Node Decrement () {
            var n1 = new Decrement () { AnchorToken = Expect (TokenType.DECR) };
            Expect (TokenType.INSTRUCTION_END);
            return n1;
        }

        public Node FunCall () {
            Expect (TokenType.PARENTHESIS_OPEN);
            var n1 = new FunCall ();
            while (firstOfSimpleExpression.Contains (CurrentToken)) {
                n1.Add (Expression ());
                while (CurrentToken == TokenType.LIST) {
                    Expect (TokenType.LIST);
                    n1.Add (Expression ());
                }
            }
            Expect (TokenType.PARENTHESIS_CLOSE);
            return n1;
        }

        public Node Expression () {
            var n1 = ExpressionUnary ();
            while (firstOfOperator.Contains (CurrentToken)) {
                var n2 = Operator ();
                n2.Add (n1);
                n2.Add (ExpressionUnary ());
                n1 = n2;
            }
            return n1;

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
                    var id = new Identifier () {
                        AnchorToken = Expect (TokenType.IDENTIFIER)
                    };

                    if (CurrentToken == TokenType.PARENTHESIS_OPEN) {
                        id.Add (FunCall ());
                    }
                    n1.Add (id);
                    break;
                case TokenType.PARENTHESIS_OPEN:
                    n1.Add (FunCall ());
                    break;
                case TokenType.ARR_BEGIN:
                    n1.Add (Array ());
                    break;
                case TokenType.VAR_CHAR:
                case TokenType.VAR_INT:
                case TokenType.VAR_STRING:
                    n1.Add (Literal ());
                    break;
                case TokenType.TRUE:
                    n1.Add (new True () { AnchorToken = Expect (TokenType.TRUE) });
                    break;
                default:
                    throw new SyntaxError (firstOfSimpleExpression, tokenStream.Current);
            }
            return n1;
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