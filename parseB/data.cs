
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
}
