# Transform to EBNF and remove LR

Program => Def-List EOF
Def-List => Def; Def*
Def => (var-def | fun-def)
var-def =>  "var" id-list ;
id-list =>  id ("," id)*
fun-def =>  id "(" id-list ")""{" var-def* stmt-list* "}"
stmt-list => stmt*
stmt => (stmt-assign | stmt-incr | stmt-decr | stmt-fun-call | stmt-if | stmt-loop | stmt-break | stmt-return | stmt-empty )
stmt-assign => id "=" expr;
stmt-incr => id "+""+";
stmt-decr => id "-""-";
stmt-fun-call => id "(" expr* ")"
stmt-if => "if" "(" expr ")" "{" stmt* "}" "("(elseif|else)" "(" expr ")" "{" stmt* "}")? 
stmt-loop => id
stmt-break => id
stmt-return => id
stmt-empty

