
using System;
using System.Collections.Generic;
using System.Linq;


namespace ex.parseB
{
    public class Parser
    {
        private List<Token> _tokens;
        private int _index;

        public void Parse( IEnumerable<Token> tokens )
        {
            _tokens = tokens.ToList();                        
            _index = 0;
        }

        private Token Previous => _tokens[_index - 1];
        private Token Next => _tokens[_index + 1];
        private Token Current => _tokens[_index];
        private bool HasPrevious => _index - 1 >= 0;
        private bool HasNext => _index + 1 < _tokens.Count;
        private bool EndText => _tokens.Count <= _index; 

        private bool Try(TT tokenType,
                         string symbolValue = null, 
                         string binOpValue = null, 
                         int? intValue = null)
        {
            bool MatchSymbolValue() => symbolValue == null || symbolValue == Current.SymbolValue;
            bool MatchBinOpValue() => binOpValue == null || binOpValue == Current.BinOpValue;
            bool MatchIntValue() => intValue == null || intValue == Current.IntValue;

            if ( Current.TokenType == tokenType 
                && MatchSymbolValue() 
                && MatchBinOpValue() 
                && MatchIntValue() )
            {
                _index++;
                return true;
            }
            return false;
        }

        private void Is(TT tokenType, 
                        string symbolValue = null, 
                        string binOpValue = null, 
                        int? intValue = null)
        {
            if ( symbolValue != null && Current.SymbolValue != symbolValue )
            {
                throw new Exception ( "failure" );
            }

            if ( binOpValue != null && Current.BinOpValue != binOpValue )
            {
                throw new Exception ( "failure" );
            }

            if ( intValue != null && Current.IntValue != intValue )
            {
                throw new Exception ( "failure" );
            }

            if ( Current.TokenType != tokenType )
            {
                throw new Exception ( "failure" );
            }
        }
    }
}
