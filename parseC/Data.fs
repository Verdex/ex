
namespace parseC

module Data =

    type public Token = Int of int
                      | String of string
                      | Symbol of string
                      | BinOp of string
                      | Comma
                      | SemiColon
                      | LParen
                      | RParen
                      | LCurl
                      | RCurl
                      | LSquare
                      | RSquare

    exception public Fatal of string