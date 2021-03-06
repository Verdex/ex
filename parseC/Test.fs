namespace parseC

module Test = 

    open Data
    open Lex
    open Parser

    let main = 
        printfn "%A" (lex "// blah ")
        printfn "%A" (lex "/* blah */")
        printfn "%A" (lex """
        1209 
        symbol
        // blah
        other
        **
        <=>
        /* blah 
            blah
            blah
        */""")