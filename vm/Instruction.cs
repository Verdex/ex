
using System;

namespace ex.vm
{
    public enum InstrType
    {
    }
    public struct Instr
    {
    }
    // Add
    // sub
    // mul
    // div
    // mod
    // r shift
    // l shift
    // b and
    // b or
    // b xor
    // l negate 
    // b negate (complement)
    // compare
    // jumb
    // jump on equal
    // jump on less than
    // jump on greater than

    // a0 -> address 0
    // (a0) -> value at address 0
    // (a0) + 1 -> 
    //         for stack address S(s, t, i) => S(s, t, i + 1)
    //         for heap address  H( i ) => H( i + 1 ) 
    //   assembler needs to know size to multiple the constant or value by 
    //   address needs known and constant size
    // move address to address
    // move value to address 
    // lea 
    // call address
    // call label
    // sys call
    // allocate heap

}
