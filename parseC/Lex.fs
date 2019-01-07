namespace parseC

module Lex = 

    open Data

    let mutable _text = ""
    let mutable _index = 0
    let mutable _buffer = ""


    let _binOpChar = [| '.'
                      ; '?'
                      ; '!'
                      ; '+'
                      ; '-'
                      ; '*'
                      ; '/'
                      ; '&'
                      ; '|'
                      ; '^'
                      ; '#'
                      ; '<'
                      ; '>'
                      ; '$'
                      ; '%'
                      ; '~'
                      ; '@'
                      ; '='
                      ; ':'
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

    let tryComment = fun () -> 
        if current () = '/' && next () = '/' then
            _index <- _index + 2
            true
        else
            false

    let tryBlockComment = fun () -> 
        if current () = '/' && next () = '*' then
            _index <- _index + 2
            true
        else
            false

    let clearLine = fun () -> 
        while not (endText ()) && current () <> '\n' && current () <> '\r' do
            _index <- _index + 1
        if not (endText ()) && (current () = '\n' || current () = '\r') then 
            _index <- _index + 1

    let clearBlockComment = fun () ->
        let endComment = fun () -> current () = '*' && hasNext () && next () = '/'
        while not (endText ()) && not (endComment()) do 
            _index <- _index + 1
        if not (endText ()) && (endComment ()) then
            _index <- _index + 2
    
    let isDigit (c : char) = System.Char.IsDigit( c )
    let isSymbolChar (c : char) = System.Char.IsLetterOrDigit( c ) || c = '_'
    let isStartSymbolChar (c: char) = System.Char.IsLetter( c ) || c = '_'

    let tryBinOp = fun () ->
        let isBinOpChar (character : char) = Array.exists (fun c -> c = character) _binOpChar

        let mutable bs = "" 
        let add (b:char) = bs <- bs + (string b)

        if isBinOpChar( current () ) then 
            add( current () )
            _index <- _index + 1
            while not (endText ()) && isBinOpChar( current () ) do
                add( current () )
                _index <- _index + 1
            _buffer <- bs
            true
        else
            false

    let trySymbol = fun () ->
        let mutable ls = ""
        let add (l:char) = ls <- ls + (string l)

        if isStartSymbolChar( current () ) then 
            add( current() )
            _index <- _index + 1;
            while not (endText ()) && isSymbolChar( current () ) do
                add( current () )
                _index <- _index + 1
            _buffer <- ls
            true
        else
            false

    let tryInt = fun () ->
        let mutable ds = "" 
        let add (d:char) = ds <- ds + (string d)
        if isDigit( current() ) then 
            add (current ())
            _index <- _index + 1
            while not (endText ()) && isDigit( current () ) do 
                add(current ())
                _index <- _index + 1 
            _buffer <- ds
            true
        else
            false

    let public lex (input : string) =
        _text <- input
        _index <- 0

        let mutable tokens = []
        let add (t:Token) = tokens <- List.Cons( t, tokens )

        while not (endText()) do
            if tryWhiteSpace () then
                ()
            elif tryComment () then
                clearLine ()
            elif tryBlockComment () then
                clearBlockComment ()
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
            elif tryChar ',' then
                add Comma 
            elif tryChar ';' then
                add SemiColon 
            elif trySymbol () then
                add (Symbol _buffer)
                _buffer <- ""
            elif tryInt () then
                add (Int (int _buffer))
                _buffer <- ""
            elif tryBinOp () then
                add (BinOp _buffer)
                _buffer <- ""
            else  
                raise (Fatal "error")

        List.rev tokens
         