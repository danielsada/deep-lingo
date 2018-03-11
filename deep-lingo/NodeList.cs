using System;

namespace DeepLingo {
    class Prog : Node { }
    class VarDef : Node { }
    class FunDef : Node { }
    class IdList : Node { }
    class Stmt : Node { }
    class If : Node { }
    class Loop : Node { }
    class Break : Node { }
    class Assignment : Node { }
    class Return : Node { }
    class Increment : Node { }
    class Decrement : Node { }
    class FunCall : Node { }
    class Expression : Node { }
    class ExpressionUnary : Node { }
    class Array : Node { }
    class Literal : Node { }
    class Operator : Node { }
    class OperatorBool : Node { }
    class OperatorComp : Node { }
    class OperatorMath : Node { }

    // More created afterwards

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