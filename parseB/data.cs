
namespace ex.parseB
{
    public enum TT
    {
        Comma,
        Colon,
        SemiColon,
        LParen,
        RParen,
        LSquare,
        RSquare,
        LCurly,
        RCurly,
        Equal,
        BinOp,
        Symbol,
        Int,
    }

    public class Token
    {
        public MetaInfo Info; 
        public TT TokenType;
        public string SymbolValue;
        public int IntValue;
        public string BinOpValue;
    }

    public class MetaInfo
    {
    }

    public class Type
    {
    }

    public class BaseType : Type
    {
    }

    public class SimpleType : BaseType
    {
        public string Name;
    }

    public class IndexType : BaseType
    {
        public string Name;
        public Type Index;
    }

    public class TypeBinOpCons : Type
    {
        public BaseType Primary;
        public string BinOp;
        public Type Rest;
    }

    public class Expr 
    {
    }

    public class BaseExpr : Expr
    {
    }

    public class Variable : BaseExpr 
    {
        public string Name;
    }

    public class Int : BaseExpr
    {
        public int Value;
    }

    public class Lambda : BaseExpr
    {
         
    }

    public class BinOpCons : Expr
    {
        public BaseExpr Primary;
        public string BinOp;
        public Expr Rest;
    }
}
