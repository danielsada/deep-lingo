# DeepLingo

DeepLingo is a compiled language that is based on the Book "The Hitchhicker guide to the galaxy". It compiles to ISIL. You'll need either .NET Core or Mono to use this compiler.


# Usage 

To compile sada-lingo you must first build the compiler. do:

```
cd deep-lingo
make
```
After making the solution, a single output file will be produced, which is deeplingo.exe, this is your compiler. *You are literally compiling the compiler*

In order to compile a program, you must compile the program with

```
mono deeplingo.exe <file to compile (.deep)>
```

This will produce an .il file, which is a dotnet binary. 

Then, to run that ilasm, you should do 


Sada-lingo is a language based on the specification of Deep-Lingo. 

This language implements everything based 


1\. Introduction
----------------

This document contains the complete technical specification of the _Deep Lingo_ programming language.

_Deep Lingo_ has two main peculiarities:

*   There is only one data type supported: a 32-bit signed two’s complement integer (int32).
    
*   The number 42 is considered _true_, all other values are considered _false_ (this, and the name _Deep Lingo_, are references to Douglas Adams’ novel “The Hitchhiker’s Guide to the Galaxy”).
    

This language specification was developed for the 2018 spring semester TC3048 _Compiler Design_ course at the Tecnológico de Monterrey, Campus Estado de Mexico.

2\. Lexicon
-----------

In the following sections, a letter is any character from the English alphabet from `A` to `Z` (both lowercase and uppercase). A digit is any character from `0` to `9`.

### 2.1. Tokens

There are five kinds of tokens: identifiers, keywords, literals, operators, and other separators. Spaces, tabulators, newlines, and comments (collectively, “white space”) are used as delimiters between tokens, but are otherwise ignored.

If the input stream has been separated into tokens up to a given character, the next token is the longest string of characters that could constitute a token.

### 2.2. Comments

Comments can be either single or multi-line. Single line comments start with two slashes (`//`) and conclude at the end of the line. Multi-line comments start with a slash and an asterisk (`/*`) and end with an asterisk and a slash (`*/`). Comments cannot be placed inside string literals. Multi-line comments cannot nest.

### 2.3. Identifiers

An identifier is composed of a letter and a sequence of zero or more letters, digits and the underscore character (`_`). Uppercase and lowercase letters are considered different. Identifiers can be of any length.

An identifier token appears as an ‹_id_› terminal symbol in the language grammar.

### 2.4. Keywords

The following seven identifiers are reserved for use as keywords and may not be used otherwise:

  

`break`

`loop`

`else`

`return`

`elseif`

`var`

`if`

### 2.5. Literals

At a lexical level there are three kinds of literals: integers, characters, and strings.

#### 2.5.1. Integers

An integer literal is a sequence of one or more digits from 0 to 9. It can optionally start with a minus (`−`) sign. Only decimal (base 10) numbers are supported. Valid range: −231 to 231 − 1.

An integer literal token appears as a ‹_lit-int_› terminal symbol in the language grammar.

#### 2.5.2. Characters

A character literal is a Unicode character enclosed in single quotes, as in `'x'`. The compiler translates the specified character into its corresponding Unicode integer code point.

Character literals cannot contain the quote character (`'`) or a newline character; in order to represent them, and certain other characters, the following escape sequences may be used:

|Name|Escape Sequence|Code Point|
|--- |--- |--- |
|Newline|\n|10|
|Carriage Return|\r|13|
|Tab|\t|9|
|Backslash|\\|92|
|Single Quote|\'|39|
|Double Quote|\"|34|
|Unicode Character|\uhhhhhh|hhhhhh|



The escape `\u`_hhhhhh_ consists of the backslash, followed by the lower case letter “u”, followed by six hexadecimal digits (digits 0 to 9 and the upper or lower case letters “a” to “f”), which are taken to specify the code point of the desired character.

A character literal token appears as a ‹_lit-char_› terminal symbol in the language grammar.

#### 2.5.3. Strings

A string literal is a sequence of zero or more Unicode characters delimited by double quotes, for example: `"this is a string"`. String literals do not contain newline or double-quote characters; in order to represent them, the same escape sequences as for character literals are available.

A string literal is stored in memory as an array list (accessible through a 32-bit [handle](https://en.wikipedia.org/wiki/Handle_(computing))) containing zero or more int32 values. Each value is the code point of the character in the corresponding position of the given string. In other words, a string is stored using the [UTF-32](https://en.wikipedia.org/wiki/UTF-32) encoding.

A string literal token appears as a ‹_lit-str_› terminal symbol in the language grammar.

3\. Syntax
----------

The following BNF context free grammar defines the syntax of the Deep Lingo programming language. The **red** elements represent explicit terminal symbols (tokens).


‹_program_› → ‹_def-list_›

‹_def-list_› → ‹_def-list_› ‹_def_›

‹_def-list_› → ε

‹_def_› → ‹_var-def_›

‹_def_› → ‹_fun-def_›

‹_var-def_› → **var** ‹_var-list_› **;**

‹_var-list_› → ‹_id-list_›

‹_id-list_› → ‹_id_› ‹_id-list-cont_›

‹_id-list-cont_› → **,** ‹_id_› ‹_id-list-cont_›

‹_id-list-cont_› → ε

‹_fun-def_› → ‹_id_› **(** ‹_param-list_› **)** **{** ‹_var-def-list_› ‹_stmt-list_› **}**

‹_param-list_› → ‹_id-list_›

‹_param-list_› → ε

‹_var-def-list_› → ‹_var-def-list_› ‹_var-def_›

‹_var-def-list_› → ε

‹_stmt-list_› → ‹_stmt-list_› ‹_stmt_›

‹_stmt-list_› → ε

‹_stmt_› → ‹_stmt-assign_›

‹_stmt_› → ‹_stmt-incr_›

‹_stmt_› → ‹_stmt-decr_›

‹_stmt_› → ‹_stmt-fun-call_›

‹_stmt_› → ‹_stmt-if_›

‹_stmt_› → ‹_stmt-loop_›

‹_stmt_› → ‹_stmt-break_›

‹_stmt_› → ‹_stmt-return_›

‹_stmt_› → ‹_stmt-empty_›

‹_stmt-assign_› → ‹_id_› **=** ‹_expr_› **;**

‹_stmt-incr_› → ‹_id_› **++** **;**

‹_stmt-decr_› → ‹_id_› **−−** **;**

‹_stmt-fun-call_› → ‹_fun-call_› **;**

‹_fun-call_› → ‹_id_› **(** ‹_expr-list_› **)**

‹_expr-list_› → ‹_expr_› ‹_expr-list-cont_›

‹_expr-list_› → ε

‹_expr-list-cont_› → **,** ‹_expr_› ‹_expr-list-cont_›

‹_expr-list-cont_› → ε

‹_stmt-if_› → **if** **(** ‹_expr_› **)** **{** ‹_stmt-list_› **}** ‹_else-if-list_› ‹_else_›

‹_else-if-list_› → ‹_else-if-list_› **elseif** **(** ‹_expr_› **)** **{** ‹_stmt-list_› **}**

‹_else-if-list_› → ε

‹_else_› → **else** **{** ‹_stmt-list_› **}**

‹_else_› → ε

‹_stmt-loop_› → **loop** **{** ‹_stmt-list_› **}**

‹_stmt-break_› → **break** **;**

‹_stmt-return_› → **return** ‹_expr_› **;**

‹_stmt-empty_› → **;**

‹_expr_› → ‹_expr-or_›

‹_expr-or_› → ‹_expr-or_› **||** ‹_expr-and_›

‹_expr-or_› → ‹_expr-and_›

‹_expr-and_› → ‹_expr-and_› **&&** ‹_expr-comp_›

‹_expr-and_› → ‹_expr-comp_›

‹_expr-comp_› → ‹_expr-comp_› ‹_op-comp_› ‹_expr-rel_›

‹_expr-comp_› → ‹_expr-rel_›

‹_op-comp_› → **==**

‹_op-comp_› → **!=**

‹_expr-rel_› → ‹_expr-rel_› ‹_op-rel_› ‹_expr-add_›

‹_expr-rel_› → ‹_expr-add_›

‹_op-rel_› → **<**

‹_op-rel_› → **<=**

‹_op-rel_› → **>**

‹_op-rel_› → **>=**

‹_expr-add_› → ‹_expr-add_› ‹_op-add_› ‹_expr-mul_›

‹_expr-add_› → ‹_expr-mul_›

‹_op-add_› → **+**

‹_op-add_› → **−**

‹_expr-mul_› → ‹_expr-mul_› ‹_op-mul_› ‹_expr-unary_›

‹_expr-mul_› → ‹_expr-unary_›

‹_op-mul_› → *

‹_op-mul_› → **/**

‹_op-mul_› → **%**

‹_expr-unary_› → ‹_op-unary_› ‹_expr-unary_›

‹_expr-unary_› → ‹_expr-primary_›

‹_op-unary_› → **+**

‹_op-unary_› → **−**

‹_op-unary_› → **!**

‹_expr-primary_› → ‹_id_›

‹_expr-primary_› → ‹_fun-call_›

‹_expr-primary_› → ‹_array_›

‹_expr-primary_› → ‹_lit_›

‹_expr-primary_› → **(** ‹_expr_› **)**

‹_array_› → **\[** ‹_expr-list_› **\]**

‹_lit_› → ‹_lit-int_›

‹_lit_› → ‹_lit-char_›

‹_lit_› → ‹_lit-str_›


4\. Semantics
-------------

### 4.1. Compile Time Validations

1.  The language only supports a 32-bit signed two’s complement integer (int32) data type. This is the data type for every variable, parameter and function return value. This means that a Deep Lingo compiler doesn’t need to verify type consistency.
    
2.  Every program starts its execution in a function called `main`. It is an error if the program does not contain a function with this name.
    
3.  Any variable defined outside a function is a global variable. The scope of a global variable is the body of all the functions in the program, even those defined before the variable itself.
    
4.  Function names and global variables exist in different namespaces. This means that you can have a global variable with the same name as a function and vice versa.
    
5.  It’s an error to define two global variables with the same name.
    
6.  It’s an error to define two functions with the same name.
    
7.  A function definition is visible from the body of all the functions in a program, even from itself. Thus, functions can call themselves recursively directly or indirectly.
    
8.  In every function call the number of arguments must match the number of parameters contained in the corresponding function definition.
    
9.  The following names are part of the initial namespace for functions and constitute Deep Lingo’s API (the number after the slash symbol (/) is the [arity](https://en.wikipedia.org/wiki/Arity) of the given function):
    
    *   `printi`/1
        
    *   `printc`/1
        
    *   `prints`/1
        
    *   `println`/0
        
    *   `readi`/0
        
    *   `reads`/0
        
    *   `new`/1
        
    *   `size`/1
        
    *   `add`/2
        
    *   `get`/2
        
    *   `set`/3
        
    
10.  Each function has its own independent namespace for its local names. This means that parameter and local variable names have to be unique inside the body of each function. It’s valid to have a parameter or local variable name with the same name as a global variable. In that case the local name [shadows](https://en.wikipedia.org/wiki/Variable_shadowing) the global variable.
    
11.  It’s an error to refer to a variable, parameter or function not in scope in the current namespace.
    
12.  The `break` statement can only be used inside the body of a `loop` statement.
    
13.  Values of integer literals should be between −231 and 231 − 1 (-2147483648 and 2147483647, respectively).
    

### 4.2. Runtime Behavior

1.  A function returns zero by default, except if it executes an explicit `return` statement with some other value.
    
2.  The value returned by the `main` function must be the exit code returned by the program to the operating system. This can be accomplished by using the `Environment.Exit(Int32)` method from the .NET Framework.
    
3.  For the conditional (`if`) statement the number 42 is the only value considered _true_, everything else is considered _false_.
    
4.  All the Deep Lingo statements (assignment, increment, decrement, function call, conditional, break, and return) behave like their C# counterparts. The only special case is the `loop` statement, which produces an infinite loop that may be exited at any moment using a `break` (or even a `return`) statement, usually within an `if` statement. The effect of traditional repetition control structures (for example: `while`, `for`, `do`/`while`, and `repeat`/`until`) found in other languages can be produced by using the `loop`, `if`, and `break` statements.
    
5.  The Deep Lingo syntax supports string and array literals. Both of these are represented in memory as array lists accessible through API managed 32-bit [handles](https://en.wikipedia.org/wiki/Handle_(computing)).
    
6.  The following are the supported operators. Precedence and associativity are established in the language grammar.

Table 1. Arithmetic operators

|Operator|Syntax|Description|
|--- |--- |--- |
|Unary minus|− x|Returns x negated (with its sign changed). An exception is thrown if the result does not fit in an int32.|
|Unary plus|+ x|Returns x.|
|Multiplication|x * y|Returns x times y. An exception is thrown if the result does not fit in an int32.|
|Division|x / y|Returns x divided by y truncating the result towards zero. An exception is thrown if the result does not fit in an int32 or if y is zero.|
|Remainder|x % y|Returns the remainder of dividing x by y. An exception is thrown if the result does not fit in an int32 or if y is zero.|
|Addition|x + y|Returns x plus y. An exception is thrown if the result does not fit in an int32.|
|Subtraction|x − y|Return x minus y. An exception is thrown if the result does not fit in an int32.|

    
Table 2. Logical operators  
    
   |Operator|Syntax|Description|
|--- |--- |--- |
|Not|! x|Returns 0 if x is 42, otherwise returns 42.|
|And|x && y|Short circuited conjunction. Evaluates x. If the result is 42, it evaluates and returns y. Otherwise returns 0.|
|Or|x || y|Short circuited disjunction. Evaluates x. If the result is 42, it returns 42. Otherwise evaluates and returns y.|

Table 3. Comparison and relational operators    

|Operator|Syntax|Description|
|--- |--- |--- |
|Equal to|x == y|Returns 42 if x is equal to y, otherwise returns 0.|
|Not equal to|x != y|Returns 42 if x is not equal to y, otherwise returns 0.|
|Greater than|x > y|Returns 42 if x is greater than y, otherwise returns 0.|
|Less than|x < y|Returns 42 if x is less than y, otherwise returns 0.|
|Greater than or equal to|x >= y|Returns 42 if x is greater than or equal to y, otherwise returns 0.|
|Less than or equal to|x <= y|Returns 42 if x is less than or equal to y, otherwise returns 0.|

Table 4. Function call

|Operator|Syntax|Description|
|--- |--- |--- |
|Function call|f (arg1, arg2, …​, argn)|Invoke f with the given arguments and return its result.|




5\. API
-------

This section documents all the functions from the Deep Lingo application programming interface (API).

Table 5. Input/Output Operations  

|Signature|Description|
|--- |--- |
|printi(i)|Prints i to stdout as a decimal integer. Does not print a new line at the end. Returns 0.|
|printc(c)|Prints a character to stdout, where c is its Unicode code point. Does not print a new line at the end. Returns 0.|
|prints(s)|Prints s to stdout as a string. s must be a handle to an array list containing zero or more Unicode code points. Does not print a new line at the end. Returns 0.|
|println()|Prints a newline character to stdout. Returns 0.|
|readi()|Reads from stdin an optionally signed decimal integer and returns its value. Does not return until a valid integer has been read.|
|reads()|Reads from stdin a string (until the end of line) and returns a handle to a newly created array list containing the Unicode code points of all the characters read.|

Table 6. Array List Operations


|Signature|Description|
|--- |--- |
|new(n)|Creates a new array list object with n elements and returns its handle. All the elements of the array list are set to zero. Throws an exception if n is less than zero.|
|size(h)|Returns the size (number of elements) of the array list referenced by handle h. Throws an exception if h is not a valid handle.|
|add(h, x)|Adds x at the end of the array list referenced by handle h. Returns 0. Throws an exception if h is not a valid handle.|
|get(h, i)|Returns the value at index i from the array list referenced by handle h. Throws an exception if i is out of bounds or if h is not a valid handle.|
|set(h, i, x)|Sets to x the element at index i of the array list referenced by handle h. Returns 0. Throws an exception if i is out of bounds or if h is not a valid handle.|


6\. Code Examples
-----------------

|Source File|Description|
|--- |--- |
|ultimate.deep|Prints the answer to the ultimate question of life, the universe, and everything.|
|binary.deep|Converts decimal numbers into binary.|
|palindrome.deep|Determines if a string is a palindrome.|
|factorial.deep|Computes factorials using iteration and recursion.|
|arrays.deep|Implementation of typical array operations.|
|next_day.deep|Given the date of a certain day, determines the date of the day after.|
|literals.deep|Verifies that the implementation of literal values meet the specified requirements.|

