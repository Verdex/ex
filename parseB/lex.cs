
using System;
using System.Linq;
using System.Collections.Generic;

namespace ex.parseB
{
    public class Lexer
    {
        private int _index;
        private string _text;

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
                else if ( TryKeyword( "func" ) )
                {
                    yield return new TFunction();
                }
                else if ( TryKeyword( "while" ) )
                {
                    yield return new TWhile();
                }
                else if ( TryKeyword( "module" ) )
                {
                    yield return new TModule();
                }
                else if ( Try( '.' ) )
                {
                    yield return new TDot();
                }
                else if ( Try( '+' ) )
                {
                    yield return new TAdd();
                }
                else if ( Try( '-' ) )
                {
                    yield return new TSub();
                }
                else if ( Try( '*' ) )
                {
                    yield return new TMul();
                }
                else if ( Try( '/' ) )
                {
                    yield return new TDiv();
                }
                else if ( Try( "&&" ) )
                {
                    yield return new TDoubleAnd();
                }
                else if ( Try( "||" ) )
                {
                    yield return new TDoubleOr();
                }
                else if ( Try( "==" ) )
                {
                    yield return new TDoubleEqual();
                }
                else if ( Try( "!=" ) )
                {
                    yield return new TNotEqual();
                }
                else if ( Try( "!" ) )
                {
                    yield return new TBang();
                }
                else if ( Try( '(' ) )
                {
                    yield return new TLParen();
                }
                else if ( Try( ')' ) )
                {
                    yield return new TRParen();
                }
                else if ( Try( '{' ) )
                {
                    yield return new TLBracket();
                }
                else if ( Try( '}' ) )
                {
                    yield return new TRBracket();
                }
                else if ( Try( "<=" ) )
                {
                    yield return new TLBigArrow();
                }
                else if ( Try( "=>" ) )
                {
                    yield return new TRBigArrow();
                }
                else if ( Try( ">=" ) )
                {
                    yield return new TLReverseBigArrow();
                }
                else if ( Try( "=<" ) )
                {
                    yield return new TRReverseBigArrow();
                }
                else if ( Try( "<" ) )
                { 
                    yield return new TLAngle();
                }
                else if ( Try( ">" ) )
                {
                    yield return new TRAngle();
                }
                else if ( Try( "=" ) )
                {
                    yield return new TEqual();
                }
                else if ( Try( ':' ) )
                {
                    yield return new TColon();
                }
                else if ( Try( ';' ) )
                {
                    yield return new TSemiColon();
                }
                else if ( Try( ',' ) )
                {
                    yield return new TComma();
                }
                else if ( TrySymbol( out var symbol ) )
                {
                    yield return new TSymbol { Value = symbol };
                }
                else if ( TryInt( out var value ) )
                {
                    yield return new TInt { Value = value };
                }
                else
                {
                    throw new Exception( $"Unknown Symbol {Current}" );
                }
            }
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