
namespace ex.parseA
{
    public interface L { }
    public class Function : L { }
    public class Comma : L { }
    public class SemiColon : L { }
    public class Colon : L { }
    public class RArrow : L { }
    public class LArrow : L { }
    public class RAngle : L { }
    public class LAngle : L { }
    public class RParen : L { }
    public class LParen : L { }
    public class RBracket : L { }
    public class LBracket : L { }

    public class Symbol : L 
    { 
        public string Value;
    }
}
