/*
  Buttercup compiler - Semantic analyzer.
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
using System.Linq;

namespace DeepLingo {

    class Variable { }

    class LocalFunctionFields {
        Boolean isParameter = false;
        int positionInParamList = -1;

        public LocalFunctionFields () { }

        public LocalFunctionFields (int position) {
            isParameter = true;
            positionInParamList = position;
        }

    }
    class Function {
        public int arity { get; set; }
        public Boolean isPredefinedFunction;
        public IDictionary<string, LocalFunctionFields> localVariables;

        public Function (int arity, Boolean isPredefinedFunction) {
            this.arity = arity;
            this.isPredefinedFunction = isPredefinedFunction;

        }

    }

    class SemanticFirst {
        private Boolean DEBUG = false;
        public IDictionary<string, Function> globalFunctions;
        public IDictionary<string, Variable> globalVariables;

        public String currentFunction;
        //-----------------------------------------------------------
        public SemanticFirst (Boolean debug) {
            this.DEBUG = debug;
            globalFunctions = new Dictionary<string, Function> ();
            globalVariables = new Dictionary<string, Variable> ();
            globalFunctions.Add ("printi", new Function (1, true));
            globalFunctions.Add ("printc", new Function (1, true));
            globalFunctions.Add ("prints", new Function (1, true));
            globalFunctions.Add ("println", new Function (0, true));
            globalFunctions.Add ("readi", new Function (0, true));
            globalFunctions.Add ("reads", new Function (0, true));
            globalFunctions.Add ("new", new Function (1, true));
            globalFunctions.Add ("size", new Function (1, true));
            globalFunctions.Add ("add", new Function (2, true));
            globalFunctions.Add ("get", new Function (2, true));
            globalFunctions.Add ("set", new Function (3, true));
        }

        public void Visit (Empty node) {

        }
        public void Visit (Prog node) {
            if (DEBUG) Console.WriteLine ($"Visiting {node.GetType()}");

            foreach (var child in node.children) {
                Visit ((dynamic) child);
            }
            if (!globalFunctions.ContainsKey ("main")) {
                throw new SemanticError ("You didn't specify a main function");
            }
        }
        public void Visit (VariableDefinition node) {
            if (DEBUG) Console.WriteLine ($"Visiting {node.GetType()}");

            foreach (var child in node.children) {
                Visit ((dynamic) child);
            }
        }

        public void Visit (GlobalVariableDefinition node) {
            if (DEBUG) Console.WriteLine ($"Visiting {node.GetType()}");

            foreach (var child in node.children) {
                Visit ((dynamic) child);
            }
        }
        public void Visit (FunctionDefinition node) {
            if (DEBUG) Console.WriteLine ($"Visiting {node.GetType()}");

            if (globalFunctions.ContainsKey (node.AnchorToken.Lexeme)) {
                throw new SemanticError ("Visit", node.AnchorToken);
            } else {
                if (DEBUG) Console.WriteLine ($"Name {node.AnchorToken.Lexeme} saved as current function");
                currentFunction = node.AnchorToken.Lexeme;
                if (DEBUG) Console.WriteLine ($"Adding {currentFunction}");
                globalFunctions.Add (currentFunction, new Function (0, false));
                if (DEBUG) Console.WriteLine ($"Added {currentFunction} to symbolTable");

                foreach (var childs in node.children) {
                    if (DEBUG) Console.WriteLine (childs);
                    Visit ((dynamic) childs);
                }
            }

        }
        public void Visit (Identifier node) {

        }

        public void Visit (GlobalVariableList node) {
            if (DEBUG) Console.WriteLine ($"Visiting {node.GetType()}");

            foreach (var child in node.children) {
                if (globalVariables.ContainsKey (child.AnchorToken.Lexeme)) {
                    throw new SemanticError ("Visit", child.AnchorToken);
                } else {
                    globalVariables.TryAdd (child.AnchorToken.Lexeme, new Variable ());
                    if (DEBUG) Console.WriteLine ($"Name {child.AnchorToken.Lexeme} Added to variable table");
                }
            }
        }
        public void Visit (VariableList node) { }

        public void Visit (ParameterList node) {
            if (DEBUG) Console.WriteLine ($"Changing {currentFunction} arity");
            globalFunctions[currentFunction].arity = node.children.Count;
            if (DEBUG) Console.WriteLine ($"Added {currentFunction} to symbolTable");
        }
        public void Visit (If node) { }
        public void Visit (Loop node) { }
        public void Visit (Break node) { }
        public void Visit (Assignment node) { }
        public void Visit (Expression node) { }
        public void Visit (ExpressionUnary node) { }
        public void Visit (Array node) { }
        public void Visit (OperatorBool node) { }
        public void Visit (OperatorComp node) { }
        public void Visit (OperatorMath node) { }
        public void Visit (FunctionCall node) { }
        public void Visit (StatementList node) { }
        public void Visit (Statement node) { }
        public void Visit (ElseIfList node) { }
        public void Visit (ElseIf node) { }
        public void Visit (Else node) { }
        public void Visit (Literal node) { }
        public void Visit (Operator node) { }
        public void Visit (Return node) { }

    }

    class SemanticSecond {
        private Boolean DEBUG = false;
        public IDictionary<string, Function> globalFunctions;
        public IDictionary<string, Variable> globalVariables;

        public String currentFunction;
        //-----------------------------------------------------------
        public SemanticSecond (Boolean debug, IDictionary<string, Function> globalFunctions,
            IDictionary<string, Variable> globalVariables) {
            this.DEBUG = debug;
            this.globalFunctions = globalFunctions;
            this.globalVariables = globalVariables;
        }

        public void VisitChildren (dynamic node) {
            foreach (var child in node.children) {
                Visit ((dynamic) child);
            }
        }

        public void Visit (Empty node) {

        }
        public void Visit (Prog node) {
            if (DEBUG) Console.WriteLine ($"Visiting {node.GetType()}");
            foreach (var child in node.children) {
                Visit ((dynamic) child);
            }
        }

        public void Visit (VariableDefinition node) {
            if (DEBUG) Console.WriteLine ($"Visiting {node.GetType()}");
            if (globalFunctions[currentFunction].localVariables == null) {
                globalFunctions[currentFunction].localVariables = new Dictionary<string, LocalFunctionFields> ();
            }
            VisitChildren (node);
            Console.WriteLine ("Local Variable Table");
            Console.WriteLine ("============");
            foreach (var entry in globalFunctions[currentFunction].localVariables) {
                Console.WriteLine (entry.Key + "\t");
            }

        }

        public void Visit (GlobalVariableDefinition node) {
            // Done on global pass
        }
        public void Visit (FunctionDefinition node) {
            if (DEBUG) Console.WriteLine ($"Visiting {node.GetType()}");
            currentFunction = node.AnchorToken.Lexeme;
            VisitChildren (node);

        }
        public void Visit (Identifier node) {
            if (DEBUG) Console.WriteLine ($"Visiting {node.GetType()}");
            var name = node.AnchorToken.Lexeme;
            if (!(globalFunctions[currentFunction].localVariables.ContainsKey (name) || globalVariables.ContainsKey (name))) {
                throw new SemanticError ($"The local variable {name} doesn't exist in the global or local scope.");
            }

            VisitChildren (node);

        }

        public void Visit (GlobalVariableList node) { }
        public void Visit (VariableList node) {
            if (DEBUG) Console.WriteLine ($"Visiting {node.GetType()}");
            if (globalFunctions[currentFunction].localVariables == null) {
                globalFunctions[currentFunction].localVariables = new Dictionary<string, LocalFunctionFields> ();
            }
            int i = 0;
            foreach (var child in node.children) {
                globalFunctions[currentFunction].localVariables.Add (child.AnchorToken.Lexeme, new LocalFunctionFields ());
            }
            if (DEBUG) {
                Console.WriteLine ($"Child node type {node.children[0].GetType()}");

                Console.WriteLine ("Local Variable Table");
                Console.WriteLine ("============");
                foreach (var entry in globalFunctions[currentFunction].localVariables) {
                    Console.WriteLine (entry.Key + "\t");
                }
            }
        }

        public void Visit (ParameterList node) {
            if (DEBUG) Console.WriteLine ($"Visiting {node.GetType()}");
            if (globalFunctions[currentFunction].localVariables == null) {
                globalFunctions[currentFunction].localVariables = new Dictionary<string, LocalFunctionFields> ();
            }
            int i = 0;
            foreach (var child in node.children) {
                globalFunctions[currentFunction].localVariables.Add (child.AnchorToken.Lexeme, new LocalFunctionFields (i++));
            }
        }
        public void Visit (If node) {
            VisitChildren (node);
        }
        public void Visit (Loop node) {
            VisitChildren (node);
        }
        public void Visit (Break node) {
            VisitChildren (node);
        }

        public void Visit (Assignment node) {
            VisitChildren (node);
        }

        public void Visit (Expression node) {
            VisitChildren (node);
        }
        public void Visit (ExpressionUnary node) {
            VisitChildren (node);
        }
        public void Visit (Array node) {
            VisitChildren (node);

        }
        public void Visit (OperatorBool node) {
            VisitChildren (node);
        }

        public void Visit (OperatorComp node) {
            VisitChildren (node);
        }
        public void Visit (OperatorMath node) {
            VisitChildren (node);

        }
        public void Visit (FunctionCall node) {
            if (DEBUG) Console.WriteLine ($"Visiting {node.GetType()}");
            if (DEBUG) Console.WriteLine ($"AT {node.AnchorToken.Lexeme}");
            if (DEBUG) Console.WriteLine ($"Children {node.children.Count}");
            var name = node.AnchorToken.Lexeme;
            var arity = node.children.Count;
            if (globalFunctions.ContainsKey (name)) {
                if (!(arity == globalFunctions[name].arity)) {
                    throw new SemanticError ($"The function {name} should have arity of {globalFunctions[name].arity} not of {arity}");
                }
                VisitChildren (node);
            } else {
                throw new SemanticError ($"Function {name} isn't defined. Are you sure this name is correct?");
            }

        }
        public void Visit (StatementList node) {
            VisitChildren (node);

        }
        public void Visit (Statement node) {
            VisitChildren (node);
        }
        public void Visit (ElseIfList node) {
            VisitChildren (node);

        }
        public void Visit (ElseIf node) {
            VisitChildren (node);

        }
        public void Visit (Else node) {
            VisitChildren (node);

        }
        public void Visit (Literal node) {
            VisitChildren (node);

        }
        public void Visit (Operator node) {
            VisitChildren (node);

        }
        public void Visit (Return node) {
            VisitChildren (node);

        }
        public void Visit (Increment node) {
            VisitChildren (node);

        }
        public void Visit (Decrement node) {
            VisitChildren (node);

        }
        public void Visit (Positive node) {
            VisitChildren (node);

        }
        public void Visit (Negative node) {
            VisitChildren (node);

        }
        public void Visit (Not node) {
            VisitChildren (node);

        }
        public void Visit (True node) {
            VisitChildren (node);

        }
        public void Visit (Sum node) {
            VisitChildren (node);

        }
        public void Visit (Sub node) {
            VisitChildren (node);

        }
        public void Visit (Div node) {
            VisitChildren (node);

        }
        public void Visit (Mul node) {
            VisitChildren (node);

        }
        public void Visit (Mod node) {
            VisitChildren (node);

        }
        public void Visit (Gt node) {
            VisitChildren (node);

        }
        public void Visit (Goet node) {
            VisitChildren (node);

        }
        public void Visit (Lt node) {
            VisitChildren (node);

        }
        public void Visit (Loet node) {
            VisitChildren (node);

        }
        public void Visit (Equals node) {
            VisitChildren (node);

        }
        public void Visit (Not_Equals node) {
            VisitChildren (node);

        }
        public void Visit (Or node) {
            VisitChildren (node);

        }
        public void Visit (And node) {
            VisitChildren (node);

        }
        public void Visit (VarInt node) {
            VisitChildren (node);

        }
        public void Visit (VarChar node) {
            VisitChildren (node);

        }
        public void Visit (VarString node) {
            VisitChildren (node);

        }
    }
}