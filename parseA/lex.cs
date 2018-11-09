
using System;
using System.Linq;
using System.Collections.Generic;

namespace ex.parseA
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
                    yield return new Function();
                }
                else if ( Try( '+' ) )
                {
                    yield return new Add();
                }
                else if ( Try( '-' ) )
                {
                    yield return new Sub();
                }
                else if ( Try( '*' ) )
                {
                    yield return new Mul();
                }
                else if ( Try( '/' ) )
                {
                    yield return new Div();
                }
                else if ( Try( "&&" ) )
                {
                    yield return new DoubleAnd();
                }
                else if ( Try( "||" ) )
                {
                    yield return new DoubleOr();
                }
                else if ( Try( "==" ) )
                {
                    yield return new DoubleEqual();
                }
                else if ( Try( "&" ) )
                {
                    yield return new And();
                }
                else if ( Try( "|" ) )
                {
                    yield return new Or();
                }
                else if ( Try( "!=" ) )
                {
                    yield return new NotEqual();
                }
                else if ( Try( "!" ) )
                {
                    yield return new Bang();
                }
                else if ( Try( '(' ) )
                {
                    yield return new LParen();
                }
                else if ( Try( ')' ) )
                {
                    yield return new RParen();
                }
                else if ( Try( '{' ) )
                {
                    yield return new LBracket();
                }
                else if ( Try( '}' ) )
                {
                    yield return new RBracket();
                }
                else if ( Try( "<=" ) )
                {
                    yield return new LBigArrow();
                }
                else if ( Try( "=>" ) )
                {
                    yield return new RBigArrow();
                }
                else if ( Try( ">=" ) )
                {
                    yield return new LReverseBigArrow();
                }
                else if ( Try( "=<" ) )
                {
                    yield return new RReverseBigArrow();
                }
                else if ( Try( "<" ) )
                { 
                    yield return new LAngle();
                }
                else if ( Try( ">" ) )
                {
                    yield return new RAngle();
                }
                else if ( Try( "=" ) )
                {
                    yield return new Equal();
                }
                else if ( Try( ':' ) )
                {
                    yield return new Colon();
                }
                else if ( Try( ';' ) )
                {
                    yield return new SemiColon();
                }
                else if ( Try( ',' ) )
                {
                    yield return new Comma();
                }
                else if ( TrySymbol( out var symbol ) )
                {
                    yield return new Symbol { Value = symbol };
                }
                else if ( TryInt( out var value ) )
                {
                    yield return new Int { Value = value };
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
