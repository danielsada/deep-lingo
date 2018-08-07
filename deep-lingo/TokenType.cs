namespace DeepLingo {

    enum TokenType {
        IDENTIFIER,
        // KEYWORDS,
        BREAK,
        LOOP,
        ELSE,
        RETURN,
        ELSEIF,
        IF,
        VAR,

        // Delimitors
        PARENTHESIS_OPEN,
        PARENTHESIS_CLOSE,
        BLOCK_BEGIN,
        BLOCK_END,
        INSTRUCTION_END,
        ARR_BEGIN,
        ARR_END,


        //OPERATORS,
        ASSIGN,
        INCR,
        DECR,
        FUNCALL,
        LIST,
        LIST_CONT,
        OR,
        AND,
        // Este es ==
        EQUALS,
        // !_
        NOT_EQUALS,
        //Greater THAN (Mayor que)
        GT,
        // GREATER OR EQUAL THAN
        GOET,
        LT,
        LOET,
        SUM,
        MUL,
        
        //Substraction
        SUB,
        DIV,
        MOD,
        NOT,
        //[ ]
        ARRAY,
        //LITERALS
        VAR_INT,
        VAR_CHAR,
        VAR_STRING,
        // En este lenguaje, 42 es TRUE.
        TRUE,
        FALSE,
        //Esto es que algo fallo
        ILLEGAL_CHAR,
        EOF
    }
}

