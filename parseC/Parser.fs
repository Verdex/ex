namespace parseC

module Parser =

    open Data

    let mutable _text : List<Token> = [] 
    let mutable _index = 0
    let mutable _buffer : List<Token> = [] 

    let public parse (tokens : List<Token>) = 
        _text <- tokens
        _index <- 0
        _buffer <- []

        ""
