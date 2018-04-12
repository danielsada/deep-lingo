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

namespace DeepLingo {

    class Fields {
        public int numParams;
        public IDictionary<string, Fields> localSymbolTable;
        public Fields (int arity) {
            this.numParams = arity;
        }

    }

    class SemanticFirst {
        private Boolean DEBUG = false;
        public IDictionary<string, Fields> globalFunctions;
        public IDictionary<string, Fields> globalVariables;

        public String currentFunction;
        //-----------------------------------------------------------
        public SemanticFirst (Boolean debug) {
            this.DEBUG = debug;
            globalFunctions = new Dictionary<string, Fields> ();
            globalVariables = new Dictionary<string, Fields> ();
            globalFunctions.Add ("printi", new Fields (1));
            globalFunctions.Add ("printc", new Fields (1));
            globalFunctions.Add ("prints", new Fields (1));
            globalFunctions.Add ("println", new Fields (0));
            globalFunctions.Add ("readi", new Fields (0));
            globalFunctions.Add ("reads", new Fields (0));
            globalFunctions.Add ("new", new Fields (1));
            globalFunctions.Add ("size", new Fields (1));
            globalFunctions.Add ("add", new Fields (2));
            globalFunctions.Add ("get", new Fields (2));
            globalFunctions.Add ("set", new Fields (3));
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
                globalFunctions.TryAdd (node.AnchorToken.Lexeme, new Fields (0));
                if (DEBUG) Console.WriteLine ($"Name {node.AnchorToken.Lexeme} Added to function table");
                currentFunction = node.AnchorToken.Lexeme;
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
                    globalVariables.TryAdd (child.AnchorToken.Lexeme, new Fields (0));
                    if (DEBUG) Console.WriteLine ($"Name {child.AnchorToken.Lexeme} Added to variable table");
                }
            }
        }
        public void Visit (VariableList node) { }

        public void Visit (ParameterList node) {
            if (DEBUG) Console.WriteLine ($"Setting {currentFunction} to arity {node.children.Count}");
            globalFunctions[currentFunction].numParams = node.children.Count;
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
        public IDictionary<string, Fields> globalFunctions;
        public IDictionary<string, Fields> globalVariables;

        public String currentFunction;
        //-----------------------------------------------------------
        public SemanticSecond (Boolean debug, IDictionary<string, Fields> globalFunctions,
            IDictionary<string, Fields> globalVariables) {
            this.DEBUG = debug;
            this.globalFunctions = globalFunctions;
            this.globalVariables = globalVariables;
        }

        public void Visit (Empty node) {

        }
        public void Visit (Prog node) {
            foreach (var child in node.children) {
                Visit ((dynamic) child);
            }
        }
        public void Visit (VariableDefinition node) {
            if (globalFunctions[currentFunction].localSymbolTable == null) {
                globalFunctions[currentFunction].localSymbolTable = new Dictionary<string, Fields> ();
            }
            foreach (var child in node.children) {
                Visit ((dynamic) child);
            }

        }

        public void Visit (GlobalVariableDefinition node) {
            // Done on global pass
        }
        public void Visit (FunctionDefinition node) {
            this.currentFunction = node.AnchorToken.Lexeme;

        }
        public void Visit (Identifier node) {

        }

        public void Visit (GlobalVariableList node) { }
        public void Visit (VariableList node) {

        }

        public void Visit (ParameterList node) { }
        public void Visit (If node) {

        }
        public void Visit (Loop node) {

        }
        public void Visit (Break node) {

        }
        public void Visit (Assignment node) {

        }
        public void Visit (Expression node) {

        }
        public void Visit (ExpressionUnary node) {

        }
        public void Visit (Array node) {

        }
        public void Visit (OperatorBool node) { }
        public void Visit (OperatorComp node) {

        }
        public void Visit (OperatorMath node) {

        }
        public void Visit (FunctionCall node) {

        }
        public void Visit (StatementList node) {

        }
        public void Visit (Statement node) {

        }
        public void Visit (ElseIfList node) {

        }
        public void Visit (ElseIf node) {

        }
        public void Visit (Else node) {

        }
        public void Visit (Literal node) {

        }
        public void Visit (Operator node) {

        }
        public void Visit (Return node) {

        }
        public void Visit (Increment node) {

        }
        public void Visit (Decrement node) {

        }
        public void Visit (Positive node) {

        }
        public void Visit (Negative node) {

        }
        public void Visit (Not node) {

        }
        public void Visit (True node) {

        }
        public void Visit (Sum node) {

        }
        public void Visit (Sub node) {

        }
        public void Visit (Div node) {

        }
        public void Visit (Mul node) {

        }
        public void Visit (Mod node) {

        }
        public void Visit (Gt node) {

        }
        public void Visit (Goet node) {

        }
        public void Visit (Lt node) {

        }
        public void Visit (Loet node) {

        }
        public void Visit (Equals node) {

        }
        public void Visit (Not_Equals node) {

        }
        public void Visit (Or node) {

        }
        public void Visit (And node) {

        }
        public void Visit (VarInt node) {

        }
        public void Visit (VarChar node) {

        }
        public void Visit (VarString node) {

        }
    }
}