
using System;
using System.Collections.Generic;
using System.Linq;


namespace ex.parseB
{
    public class Parser
    {
        private List<Token> _tokens;
        private int _index;

        public Expr Parse( IEnumerable<Token> tokens )
        {
            _tokens = tokens.ToList();                        
            _index = 0;

            return Expr();
        }

        private Expr Expr()
        {
            BaseExpr Primary()
            {
            // TODO handle negated
            // TODO handle paren
                if ( TrySymbol( out string value ) )
                {
                    return new Variable { Name = value };
                }
                else if ( TryInt( out int iValue) )
                {
                    return new Int { Value = iValue };
                }
                return null;
            }

            if ( EndTokens )
            {
                return null;
            }

            var primary = Primary();

            if ( EndTokens || !TryBinOp( out string binOp ) )
            {
                return primary; 
            }

            return new BinOpCons { Primary = primary, BinOp = binOp, Rest = Expr() };
        }

        private Token Previous => _tokens[_index - 1];
        private Token Next => _tokens[_index + 1];
        private Token Current => _tokens[_index];
        private bool HasPrevious => _index - 1 >= 0;
        private bool HasNext => _index + 1 < _tokens.Count;
        private bool EndTokens => _tokens.Count <= _index; 

        private bool TryInt( out int value )
        {
            if ( Current.TokenType == TT.Int )
            {
                value = Current.IntValue;
                _index++;
                return true;
            }
            value = 0;
            return false;
        }

        private bool TrySymbol( out string value )
        {
            if ( Current.TokenType == TT.Symbol )
            {
                value = Current.SymbolValue;
                _index++;
                return true;
            }
            value = "";
            return false;
        }

        private bool TryBinOp( out string binOp )
        {
            if ( Current.TokenType == TT.BinOp )
            {
                binOp = Current.BinOpValue;
                _index++;
                return true;
            }
            binOp = "";
            return false;
        }

        private bool TryToken( TT tokenType )
        {
            if ( Current.TokenType == tokenType )
            {
                _index++;
                return true;
            }
            return false;
        }
    }
}
