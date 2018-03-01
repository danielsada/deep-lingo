# Transform to EBNF and remove LR

```
# Initial program
Program     -> Def-List EOF
# List of Defs (can be empty)
Def-List    -> Def*
# It can be either a function or a variable.
Def         -> (var-def | fun-def)
# We can have as many var defs as we want.
var-def     ->  "var" id-list ;

#An id-list is a list of id . Can be empty
id-list     ->  (id ("," id)*)?

# Definition of a function, we introduce statements here.
# Funcion def defines a function with <id> and params <id-list*>
# We can have either variable definitions or statements.
fun-def     
            ->  id "(" id-list* ")""{" (var-def | stmt-list)* "}"

# Remember that we can have multiple ifelse. if and else only support 1 expression
stmt-if     -> 
    "if" "(" expr ")" "{" stmt* "}" ("("(elseif)" "(" expr ")" "{" stmt* "}")*  ("("(else)" "(" expr ")" "{" stmt* "}")?

# We divide statements into two categories, calls, which are asignations or value
# changes and control, which allows us to change the execution order of the program
stmt-list   -> stmt*
stmt        -> (stmt-call | stmt-control)

# Statement calls
stmt-call -> id (stmt-assign | stmt-incr | stmt-decr | stmt-fun-call );
stmt-assign ->  "=" expr
stmt-incr   ->  "++"
stmt-decr   ->  "--"
stmt-fun-call   
            -> "(" expr* ")"

# Statement control
stmt-control-> ( stmt-loop | stmt-break | stmt-return | stmt-empty )
stmt-loop   -> "loop" "{" stmt-list "}" 
stmt-break  -> "break" ;
stmt-return -> return expr;
stmt-empty  -> ;

# Expressions
expr-list   -> expr*
expr        -> ( expr-or | expr-and | expr-comp | expr-op )
expr-comp   -> ( lit | expr-comp) ("==" | "!=" | "<" | "<=" | ">" | ">=") ( lit | expr-comp)
expr-or     -> expr-or (|| expr-and)*
expr-and    -> expr-and (&& expr-comp)*

#Unary expressions can't be 
expr-unary  -> ("+"| "!"| "-") ("id"|"fun-call"|"lit"|"array")
expr-op     -> (expr-add | expr-substract | expr-mult | expr-div | expr-mod )

#Pregunta ¿puedo hacer esto?
expr-add    -> expr-unary ("+" expr-add)

array       -> "[" expr-list "]"    
lit         -> (lit-int | lit-char | lit-string)



```
