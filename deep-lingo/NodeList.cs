using System;

namespace DeepLingo {
    class Empty : Node { }
    class Prog : Node { }
    class VariableDefinition : Node { }
    class FunctionDefinition : Node { }
    class Identifier : Node { }
    class VariableList : Node { }
    class ParameterList : Node { }

    class If : Node { }
    class Loop : Node { }
    class Break : Node { }
    class Assignment : Node { }
    class Expression : Node { }
    class ExpressionUnary : Node { }
    class Array : Node { }
    class OperatorBool : Node { }
    class OperatorComp : Node { }
    class OperatorMath : Node { }

    class StmtCall : Node { }
    class FunctionCall : Node { }

    class RootFunctionCall : Node { }
    class StmtIdOrFunCall : Node { }
    class ExprIdOrFunCall : Node { }

    class StatementList : Node { };
    class Statement : Node { };
    class ExpressionList : Node { };
    class ElseIfList : Node { };
    class ElseIf : Node { };
    class Else : Node { };
    class Literal : Node { }
    class Operator : Node { }
    class Return : Node { }

    class IdOrFunCall : Node { }

    class Increment : Node { }
    class Decrement : Node { }
    class FunCall : Node { }
    //Some expressions

    // More created afterwards
    // Expression Unary elements

    class Positive : Node { }

    class Negative : Node { }

    class Not : Node { }

    class True : Node { };

    //Array

    //Operator Math
    class Sum : Node { }
    class Sub : Node { }
    class Div : Node { }
    class Mul : Node { }
    class Mod : Node { }
    // Operator Comp
    class Gt : Node { }
    class Goet : Node { }
    class Lt : Node { }
    class Loet : Node { }
    class Equals : Node { }
    class Not_Equals : Node { }

    // Operator Bool
    class Or : Node { }
    class And : Node { }

    //Literals
    class VarInt : Node { }
    class VarChar : Node { };
    class VarString : Node { }

}