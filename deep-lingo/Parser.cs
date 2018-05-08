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

        static readonly ISet<TokenType> firstOfModifier =
            new HashSet<TokenType> () {
                TokenType.NOT,
                TokenType.SUM,
                TokenType.SUB,
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
            while (CurrentToken == TokenType.VAR || CurrentToken == TokenType.IDENTIFIER) {
                if (CurrentToken == TokenType.IDENTIFIER) {
                    programNode.Add (FunctionDefinition ());
                    Expect (TokenType.BLOCK_END);
                } else {
                    programNode.Add (GlobalVariableDefinition ());
                }
            }

            Expect (TokenType.EOF);
            // Console.WriteLine (programNode.ToString ());
            return programNode;
        }

        public Node VariableDefinition () {
            Expect (TokenType.VAR);
            var n1 = VariableList ();
            Expect (TokenType.INSTRUCTION_END);
            return n1;
        }

        public Node GlobalVariableDefinition () {
            Expect (TokenType.VAR);
            var n1 = GlobalVariableList ();
            Expect (TokenType.INSTRUCTION_END);
            return n1;
        }

        public Node FunctionDefinition () {
            var fundef = new FunctionDefinition ();
            fundef.AnchorToken = Expect (TokenType.IDENTIFIER);
            Expect (TokenType.PARENTHESIS_OPEN);
            if (CurrentToken != TokenType.PARENTHESIS_CLOSE) {
                fundef.Add (ParameterList ());
            }
            Expect (TokenType.PARENTHESIS_CLOSE);
            Expect (TokenType.BLOCK_BEGIN);
            while (CurrentToken == TokenType.VAR) {
                fundef.Add (VariableDefinition ());
            }
            while (firstOfStatement.Contains (CurrentToken)) {
                fundef.Add (Statement ());
            }
            return fundef;

        }

        public Node VariableList () {
            var n1 = new VariableList ();
            n1.Add (new Identifier () { AnchorToken = Expect (TokenType.IDENTIFIER) });
            while (CurrentToken == TokenType.LIST) {
                Expect (TokenType.LIST);
                n1.Add (new Identifier () { AnchorToken = Expect (TokenType.IDENTIFIER) });
            }
            return n1;
        }
        public Node GlobalVariableList () {
            var n1 = new GlobalVariableList ();
            n1.Add (new Identifier () { AnchorToken = Expect (TokenType.IDENTIFIER) });
            while (CurrentToken == TokenType.LIST) {
                Expect (TokenType.LIST);
                n1.Add (new Identifier () { AnchorToken = Expect (TokenType.IDENTIFIER) });
            }
            return n1;
        }
        public Node ParameterList () {
            var n1 = new ParameterList ();
            n1.Add (new Identifier () { AnchorToken = Expect (TokenType.IDENTIFIER) });
            while (CurrentToken == TokenType.LIST) {
                Expect (TokenType.LIST);
                n1.Add (new Identifier () { AnchorToken = Expect (TokenType.IDENTIFIER) });
            }
            return n1;
        }

        public Node Statement () {
            var n1 = new Empty ();
            switch (CurrentToken) {
                case TokenType.IDENTIFIER:
                    var nx = StatementIdentifierOrFunctionCall ();
                    // Expect (TokenType.INSTRUCTION_END);
                    return nx;
                case TokenType.IF:
                    return If ();
                case TokenType.LOOP:
                    return Loop ();
                case TokenType.BREAK:
                    return Break ();
                case TokenType.RETURN:
                    return Return ();
                case TokenType.INSTRUCTION_END:
                    Expect (TokenType.INSTRUCTION_END);
                    return n1;
                default:
                    throw new SyntaxError (CurrentToken, tokenStream.Current);
            }
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
                n2.Add (Statement ());
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
                    stmtList.Add (Statement ());
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
                    stmtList.Add (Statement ());
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
                n1.Add (Statement ());
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
            // if (CurrentToken == TokenType.INSTRUCTION_END) {
            //     Expect (TokenType.INSTRUCTION_END);
            // }
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
            return n1;
        }

        public Node Decrement () {
            var n1 = new Decrement () { AnchorToken = Expect (TokenType.DECR) };
            return n1;
        }

        public Node StatementIdentifierOrFunctionCall () {
            var identifier = Expect (TokenType.IDENTIFIER);
            switch (CurrentToken) {
                case (TokenType.INCR):
                    var incr = Increment ();
                    incr.AnchorToken = identifier;
                    Expect (TokenType.INSTRUCTION_END);
                    return incr;
                case (TokenType.DECR):
                    var decr = Decrement ();
                    decr.AnchorToken = identifier;
                    Expect (TokenType.INSTRUCTION_END);
                    return decr;
                case (TokenType.ASSIGN):
                    var assignment = Assignment ();
                    assignment.AnchorToken = identifier;
                    Expect (TokenType.INSTRUCTION_END);
                    return assignment;
                case (TokenType.PARENTHESIS_OPEN):
                    Expect (TokenType.PARENTHESIS_OPEN);
                    var stlist = StatementList ();
                    stlist.AnchorToken = identifier;
                    Expect (TokenType.PARENTHESIS_CLOSE);
                    Expect (TokenType.INSTRUCTION_END);
                    return stlist;
                default:
                    return new Identifier () { AnchorToken = identifier };
            }

        }
        public Node ExpressionIdentifierOrFunctionCall () {
            var identifier = Expect (TokenType.IDENTIFIER);
            if (CurrentToken == TokenType.PARENTHESIS_OPEN) {
                var n1 = new FunctionCall ();
                n1.AnchorToken = identifier;
                Expect (TokenType.PARENTHESIS_OPEN);

                while (firstOfSimpleExpression.Contains (CurrentToken)) {
                    n1.Add (Expression ());
                    while (CurrentToken == TokenType.LIST) {
                        Expect (TokenType.LIST);
                        n1.Add (Expression ());
                    }
                }
                Expect (TokenType.PARENTHESIS_CLOSE);
                return n1;
            } else {
                return new Identifier () { AnchorToken = identifier };
            }
        }

        public Node StatementList () {
            var exprList = new FunctionCall ();
            while (FirstOfExprUnary.Contains (CurrentToken) || CurrentToken == TokenType.LIST) {
                if (CurrentToken == TokenType.LIST) {
                    Expect (TokenType.LIST);
                }
                exprList.Add (Expression ());
            }
            return exprList;
        }
        public Node OneStatement () {
            var exprList = new Statement ();
            exprList.Add (Expression ());
            return exprList;
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
            bool hasModifier = false;
            var modifier = new Node ();
            if (firstOfModifier.Contains (CurrentToken)) {
                hasModifier = true;
                switch (CurrentToken) {
                    case TokenType.SUM:
                        modifier = new Positive () { AnchorToken = Expect (TokenType.SUM) };
                        break;
                    case TokenType.NOT:
                        modifier = new Negative () { AnchorToken = Expect (TokenType.NOT) };
                        break;
                    case TokenType.SUB:
                        modifier = new Not () { AnchorToken = Expect (TokenType.SUB) };
                        break;
                    default:
                        break;
                }
            }
            switch (CurrentToken) {
                case TokenType.IDENTIFIER:
                    var ret = ExpressionIdentifierOrFunctionCall ();
                    if (hasModifier) {
                        modifier.Add (ret);
                        ret = modifier;
                    }
                    return ret;
                case TokenType.PARENTHESIS_OPEN:
                    throw new SyntaxError (TokenType.PARENTHESIS_OPEN, tokenStream.Current);
                case TokenType.ARR_BEGIN:
                    if (hasModifier) {
                        throw new SyntaxError (modifier.AnchorToken.Category, modifier.AnchorToken);
                    }
                    return Array ();
                case TokenType.VAR_CHAR:
                case TokenType.VAR_INT:
                case TokenType.VAR_STRING:
                    var retu = Literal ();
                    if (hasModifier) {
                        modifier.Add (retu);
                        retu = modifier;
                    }
                    return retu;
                case TokenType.TRUE:
                    var tru = True ();
                    if (hasModifier) {
                        modifier.Add (tru);
                        tru = modifier;
                    }
                    return tru;
                default:
                    throw new SyntaxError (firstOfSimpleExpression, tokenStream.Current);
            }
        }
        public Node Array () {
            Expect (TokenType.ARR_BEGIN);
            Node n1 = new Array ();
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
        public Node True(){
            var n = new True();
            n.AnchorToken = Expect (TokenType.TRUE);
            return n;
        }

    }
}