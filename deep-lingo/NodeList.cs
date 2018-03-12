using System;

namespace DeepLingo {
    class Prog : Node { }
    class VarDef : Node { }
    class FunDef : Node { }
    class Id : Node { }
    class IdList : Node { }
    class Stmt : Node { }
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

    class StatementList : Node { };
    class ElseIfList : Node { };
    class ElseIf : Node { };
    class Else : Node { };
    class Literal : Node { }
    class Operator : Node { }
    class Return : Node { }

    class Increment : Node { }
    class Decrement : Node { }
    class FunCall : Node { }
    //Some expressions

    // More created afterwards
    // Expression Unary elements
    class Identifier : Node { };

    class Positive : Node { }

    class Negative : Node { }

    class Not : Node { }

    class True : Node { };

    //Array
    class ArrNode : Node { }

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