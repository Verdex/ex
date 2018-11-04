
namespace ex.parseA
{
    public enum TT 
    {
        Symbol,
        Function,
        Comma,
        Colon,
        SemiColon,
        LParen,
        RParen,
        LBracket,
        RBracket,
        LAngle,
        RAngle,
        LArrow,
        RArrow,
    }
    public class Token
    {
        public TT Type;
        public string SValue;
        public int IValue;
        public uint UValue;
        public float FValue;
        public double DValue;
    }
}
