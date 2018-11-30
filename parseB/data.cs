
namespace ex.parseB
{
    public class Token
    {
        public MetaInfo Info; 
    }

    public class TSymbol : Token
    {
        public string Value;
    }
    public class TComma : Token { }
    public class TColon : Token { }
    public class TSemiColon : Token { }
    public class TLParen : Token { }
    public class TRParen : Token { }
    public class TLSquare : Token { }
    public class TRSquare : Token { }
    public class TLBracket : Token { }
    public class TRBracket : Token { }
    public class TLAngle : Token { }
    public class TRAngle : Token { }
    public class TAdd : Token { }
    public class TSub : Token { } 
    public class TMul : Token { }
    public class TDiv : Token { }
    public class TDot : Token { }
    public class TAnd : Token { }
    public class TOr : Token { }
    public class TXor : Token { }
    public class TBang : Token { }
    public class TEqual : Token { }
    public class TDollar : Token { }
    public class TTilde : Token { }
    public class TBackTick : Token { }
    public class TPercent : Token { }
    public class TAt : Token { }
    public class TQuestion : Token { }
    public class THash : Token { }

    public class TWhile : Token { }
    public class TModule : Token { }
    public class TFunction : Token { }

    public class TInt : Token
    {
        public int Value;
    }

    public class MetaInfo
    {
    }

    public class Ast
    {
        public MetaInfo Info;
    }

    public class AModule : Ast
    {
        public string Name;  
    }

    public class AFunctionDef : Ast
    {
        public string Name;
    }

    public class ABinaryList : Ast
    {
        public Token[] BinOp;
        public AExpr Next;
    }

    public class APrimitiveExpr : AExpr 
    // literals (String, int, bool), variable, function call, lambda def, cons
    {
    }

    public AVariable : APrimitiveExpr
    {
    }

    public ABoolExpr : APrimitiveExpr
    {
    }

    public class AStringExpr : APrimitiveExpr
    {
    }

    public class AIntExpr : APrimitiveExpr
    {
    }

    public class AExpr : Ast
    {
        public bool Negated;
        public APrimitiveExpr Expr;
        public ABinaryList BinExpr;
    }
}
