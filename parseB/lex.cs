
using System;
using System.Linq;
using System.Collections.Generic;

namespace ex.parseB
{
    public class Lexer
    {
        private int _index;
        private string _text;

        private readonly char[] _binOpChar = { '.'
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
                                             };


        public IEnumerable<Token> Lex(string input)
        {
            _index = 0;
            _text = input;

            while( !EndText )
            {
                if ( Try(char.IsWhiteSpace) ) 
                {
                    // Nop
                }
                else if ( Try( "//" ) )
                {
                    ClearLine();
                }
                else if ( Try( "/*" ) )
                {
                    ClearBlockComment();
                }
                else if ( Try( '(' ) )
                {
                    yield return new Token { TokenType = TT.LParen };
                }
                else if ( Try( ')' ) )
                {
                    yield return new Token { TokenType = TT.RParen };
                }
                else if ( Try( '[' ) )
                {
                    yield return new Token { TokenType = TT.LSquare };
                }
                else if ( Try( ']' ) )
                {
                    yield return new Token { TokenType = TT.RSquare };
                }
                else if ( Try( '{' ) )
                {
                    yield return new Token { TokenType = TT.LCurly };
                }
                else if ( Try( '}' ) )
                {
                    yield return new Token { TokenType = TT.RCurly };
                }
                else if ( Try( "=" ) )
                {
                    yield return new Token { TokenType = TT.Equal };
                }
                else if ( Try( ':' ) )
                {
                    yield return new Token { TokenType = TT.Colon };
                }
                else if ( Try( ';' ) )
                {
                    yield return new Token { TokenType = TT.SemiColon };
                }
                else if ( Try( ',' ) )
                {
                    yield return new Token { TokenType = TT.Comma };
                }
                else if ( TrySymbol( out var symbol ) )
                {
                    yield return new Token { TokenType = TT.Symbol, SymbolValue = symbol };
                }
                else if ( TryInt( out var value ) )
                {
                    yield return new Token { TokenType = TT.Int, IntValue = value };
                }
                else if ( TryBinOp( out string binOp ) )
                {
                    yield return new Token { TokenType = TT.BinOp, BinOpValue = binOp };
                }
                else
                {
                    throw new Exception( $"Unknown Symbol {Current}" );
                }
            }
        }

        private bool TryBinOp(out string binOp )
        {
            bool IsBinOpChar( char character ) => _binOpChar.Any( c => character == c );

            var bs = new List<char>();
            if ( IsBinOpChar( Current ) )
            {
                bs.Add( Current );
                _index++;
            }
            else
            {
                binOp = "";
                return false;
            }
            while( !EndText && IsBinOpChar( Current ) )
            {
                bs.Add( Current );
                _index++; 
            }
            binOp = new string( bs.ToArray() );
            return true;
        }
    
        private bool TrySymbol(out string symbol)
        {
            var s = new List<char>();
            if ( IsStartSymbolChar( Current ) )
            {
                s.Add( Current );
                _index++;
            }
            else
            {
                symbol = "";
                return false;
            }
            while (!EndText && IsSymbolChar( Current ) )
            {
                s.Add( Current );
                _index++;
            }

            symbol = new string( s.ToArray() );
            return true;
        }

        private bool TryInt( out int value )
        {
            var d = new List<char>();
            if ( char.IsDigit( Current ) )
            {
                d.Add( Current );
                _index++;
            }
            else
            {
                value = 0;
                return false;
            }
            while( !EndText && char.IsDigit( Current ) )
            {
                d.Add( Current );
                _index++; 
            }
            value = int.Parse( new string( d.ToArray() ) );
            return true;
        }
        
        private void ClearLine()
        {
            while( !EndText && Current != '\n' && Current != '\r' )
            {
                _index++;
            }
            if ( !EndText && (Current == '\n' || Current == '\r') )
            {
                _index++;
            }
        }

        private void ClearBlockComment()
        {
            bool EndComment() => Current == '*' && HasNext && Next == '/';
            while( !EndText && !EndComment() )
            {
                _index++;
            }
            if ( !EndText && EndComment() )
            {
                _index+=2;
            }
        }

        private char Previous => _text[_index - 1];
        private char Next => _text[_index + 1];
        private char Current => _text[_index];
        private bool HasPrevious => _index - 1 >= 0;
        private bool HasNext => _index + 1 < _text.Length;
        private bool EndText => _text.Length <= _index; 
        
        private static bool NotSymbolChar( char c )
            => !IsSymbolChar( c );

        private static bool IsStartSymbolChar( char c ) 
            => char.IsLetter( c ) || c == '_';

        private static bool IsSymbolChar( char c ) 
            => char.IsLetterOrDigit( c ) || c == '_';

        private bool Try( char c )
        {
            if ( Current == c )
            {
                _index++;
                return true;
            }
            return false;
        }

        private bool Try( string s )
        {
            var t = _text.Substring( _index );
            if ( t.StartsWith( s ) )
            {
                _index += s.Length;
                return true;
            }
            return false;
        }

        private bool TryKeyword( string s )
        {
            var sub = _text.Substring( _index );

            bool EndOfFile() => sub.Length == s.Length;

            if ( sub.StartsWith( s ) && ( EndOfFile() || NotSymbolChar( sub[s.Length] ) ) )
            {
                _index+=s.Length;
                return true;
            }
            return false;
        }

        private bool Try( Func<char, bool> p ) 
        {
            if ( p( Current ) )
            {
                _index++;
                return true;
            }
            return false;
        }

        private void Is( char c )
        {
            if ( Current != c )
            {
                throw new Exception ( "failure" );
            }
        }
    }
}
