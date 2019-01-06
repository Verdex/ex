namespace parseC

module Lex = 

    open Data

    let mutable _text = ""
    let mutable _index = 0


    let _binOpChar = [| '.'
                      , '?'
                      , '!'
                      , '+'
                      , '-'
                      , '*'
                      , '/'
                      , '&'
                      , '|'
                      , '^'
                      , '#'
                      , '<'
                      , '>'
                      , '$'
                      , '%'
                      , '~'
                      , '@'
                      |]

    let previous = fun () -> _text.[_index - 1]
    let next = fun () -> _text.[_index + 1]
    let current = fun () -> _text.[_index]
    let hasPrevious = fun () -> _index - 1 >= 0;
    let hasNext = fun () -> _index + 1 < _text.Length;
    let endText = fun () -> _text.Length <= _index; 

    let tryChar (c : char) = 
        if c = current () then
            _index <- _index + 1
            true
        else
            false

    let tryWhiteSpace = 
        fun () -> tryChar ' '
               || tryChar '\n'
               || tryChar '\r'
               || tryChar '\t'

    let public lex (input : string) =
        _text <- input
        _index <- 0

        let mutable tokens = []
        let add (t:Token) = tokens <- List.Cons( t, tokens )

        while not (endText()) do
            if tryWhiteSpace () then
                ()
            elif tryChar '(' then
                add LParen 
            elif tryChar ')' then
                add RParen 
            elif tryChar '{' then
                add LCurl 
            elif tryChar '}' then
                add RCurl 
            elif tryChar '[' then
                add LSquare
            elif tryChar ']' then
                add RSquare 
            else  
                raise (Fatal "error")

        List.rev tokens
         