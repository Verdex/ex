namespace parseC

module Test = 

    open Data
    open Lex

    let main = 
        printfn "%A" (lex "// blah ")
        printfn "%A" (lex "/* blah */")
        printfn "%A" (lex """
        1209 
        /* blah 
            blah
            blah
        */""")