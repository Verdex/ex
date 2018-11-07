
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
            return null;
        }
    
         

        private char Previous => _text[_index - 1];
        private char Next => _text[_index + 1];
        private char Curent => _text[_index];
        private bool HasPrevious => _index - 1 >= 0;
        private bool HasNext => _index + 1 < _text.Length;
        private bool EndText => _text.Length <= _index; 

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
