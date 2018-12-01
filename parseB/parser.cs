
using System.Collections.Generic;
using System.Linq;

namespace ex.parseB
{
    public class Parser
    {
        private List<Token> _tokens;
        private int _index;

        public Ast Parse( IEnumerable<Token> tokens )
        {
                                
        }

        public AExpr ParseExpr()
        {
            APrimitiveExpr Primitive() => null;
            var negated = Try<TSub>();
        }

        private Token Previous => _tokens[_index - 1];
        private Token Next => _tokens[_index + 1];
        private Token Current => _tokens[_index];
        private bool HasPrevious => _index - 1 >= 0;
        private bool HasNext => _index + 1 < _tokens.Length;
        private bool EndText => _tokens.Length <= _index; 

        private bool Try<Token>()
        {
            if ( Current is Token )
            {
                _index++;
                return true;
            }
            return false;
        }

        private void Is<Token>()
        {
            if ( !(Current is Token) )
            {
                throw new Exception ( "failure" );
            }
        }
    }
}
