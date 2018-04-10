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
    public enum Type {
        Int32
    }

    class Semantic {

        //-----------------------------------------------------------
        public SymbolTable Table {
            get;
            private set;
        }

        //-----------------------------------------------------------
        public Semantic () {
            Table = new SymbolTable ();
        }

        //-----------------------------------------------------------

        public void Visit (Empty node) {

        }
        public void Visit (Prog node) {

        }
        public void Visit (VariableDefinition node) {

        }
        public void Visit (FunctionDefinition node) {

        }
        public void Visit (Identifier node) {

        }
        public void Visit (VariableList node) {

        }
        public void Visit (ParameterList node) {

        }
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
        public void Visit (OperatorBool node) {

        }
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