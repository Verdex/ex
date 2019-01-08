namespace parseC

module Parser =

    open Data

    let mutable _text = [] 
    let mutable _index = 0
    let mutable _buffer = ""

    let public parse (tokens : List<Token>) = 
        _text <- tokens
        _index <- 0
        // _buffer <- ?
        ""
