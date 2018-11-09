
namespace ex.parseA
{
    public class Token
    {
        public MetaInfo Info; 
    }

    public class Symbol : Token
    {
        public string Value;
    }
    public class Function : Token { }
    public class Comma : Token { }
    public class Colon : Token { }
    public class SemiColon : Token { }
    public class LParen : Token { }
    public class RParen : Token { }
    public class LBracket : Token { }
    public class RBracket : Token { }
    public class LAngle : Token { }
    public class RAngle : Token { }
    public class LBigArrow : Token { }
    public class RBigArrow : Token { }

    public class MetaInfo
    {
    }
}
