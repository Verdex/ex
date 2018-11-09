
using System;
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
                    _index++;
                }
                else if ( TryKeyword( "Func" ) )
                {
                    yield return new Function();
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
                else if ( Try( "<" ) )
                { 
                    yield return new LAngle();
                }
                else if ( Try( ">" ) )
                {
                    yield return new RAngle();
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
                // TODO grab symbols after all keywords
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
            => Try( s ) && (EndText || NotSymbolChar( Next ) );

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
