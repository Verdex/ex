
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
    public class Add : Token { }
    public class Sub : Token { } 
    public class Mul : Token { }
    public class Div : Token { }
    public class DoubleAnd : Token { }
    public class DoubleOr : Token { }
    public class And : Token { }
    public class Or : Token { }
    public class Bang : Token { }
    public class Equal : Token { }
    public class DoubleEqual : Token { }
    public class NotEqual : Token { }
    public class LReverseBigArrow : Token { }
    public class RReverseBigArrow : Token { }


    public class MetaInfo
    {
    }
}
