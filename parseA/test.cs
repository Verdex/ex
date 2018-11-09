
using System;
using System.Linq;
using System.Collections.Generic;

namespace ex.parseA
{
    public static class Tests 
    {
        public static void Main()
        {
            Test( "Should Parse Left Big Arrow after func keyword", () =>
            {
                var l = new Lexer();
                var o = l.Lex( "func <= " ).ToList();
                Check( "Result Count", o.Count == 2 );
                Check( "func lexed", o[0] is Function );
                Check( "left big arrow lexed", o[1] is LBigArrow );
            });

            Test( "Should Parse keyword look-a-likes as symbols", () =>
            {
                var l = new Lexer();
                var o = l.Lex( "func_ func0 _func funcx" ).ToList();
                Check( "Result Count", o.Count == 4 );
                Check( "1", o[0] is Symbol );
                Check( "2", o[1] is Symbol );
                Check( "3", o[2] is Symbol );
                Check( "4", o[3] is Symbol );
            });

            Test( "Should Parse keyword as keyword", () =>
            {
                var l = new Lexer();
                var o = l.Lex( "func{ {func func( func" ).ToList();
                Check( "Result Count", o.Count == 7 );
                Check( "1", o[0] is Function );
                Check( "2", o[1] is LBracket );
                Check( "3", o[2] is LBracket );
                Check( "4", o[3] is Function );
                Check( "5", o[4] is Function );
                Check( "6", o[5] is LParen );
                Check( "7", o[6] is Function );
            });

            Test( "Should Parse Line Comment", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"//func{ {func func( func
//1, 2, 3, 4, 5
func" ).ToList();
                Check( "Result Count", o.Count == 1 );
                Check( "1", o[0] is Function );
            });

            Test( "Should Parse Line Comment On Last Line of File", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"//func{ {func func( func
//1, 2, 3, 4, 5
//func" ).ToList();
                Check( "Result Count", o.Count == 0 );
            });

            Test( "Should Parse Only Line Comment", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"//" ).ToList();
                Check( "Result Count", o.Count == 0 );
            });

            Test( "Should Parse Block Comment", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"func
/* some stuff */
symbol 
/* some stuff
   another stuff
*/
symbol2" ).ToList();
                Check( "Result Count", o.Count == 3 );
            });

            Test( "Should Parse Entire File Block Comment", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"/*func 
1234
other func
*/" ).ToList();
                Check( "Result Count", o.Count == 0 );
            });

            Test( "Should Parse Block Comment Until End of File", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"func
/* some stuff */
symbol 
/* some stuff
   another stuff
symbol2" ).ToList();
                Check( "Result Count", o.Count == 2 );
            });

            Test( "Should Parse Only Starting Block Comment", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"/*" ).ToList();
                Check( "Result Count", o.Count == 0 );
            });

            Test( "Should Parse Only Block Comment", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"/**/" ).ToList();
                Check( "Result Count", o.Count == 0 );
            });

            Test( "All Operators", () => 
            {
                var l = new Lexer();
                var o = l.Lex( "< > + * - / == = != ! <= => >= =< || | & &&" ).ToList();
                Check( "Result Count", o.Count == 18 );
                Check( "Less Than", o[0] is LAngle );
                Check( "Greater Than", o[1] is RAngle );
                Check( "Add", o[2] is Add );
                Check( "Mult", o[3] is Mul );
                Check( "Sub", o[4] is Sub );
                Check( "Div", o[5] is Div );
                Check( "Double Equal", o[6] is DoubleEqual );
                Check( "Equal", o[7] is Equal );
                Check( "Not Equal", o[8] is NotEqual );
                Check( "Bang", o[9] is Bang );
                Check( "Left Big Arrow", o[10] is LBigArrow );
                Check( "Right Big Arrow", o[11] is RBigArrow );
                Check( "Left Reverse Big Arrow", o[12] is LReverseBigArrow );
                Check( "Right Reverse Big Arrow", o[13] is RReverseBigArrow );
                Check( "Double Or", o[14] is DoubleOr );
                Check( "Single Or", o[15] is Or );
                Check( "Single And", o[16] is And );
                Check( "Double And", o[17] is DoubleAnd );
            });

            Test( "Int", () =>
            {
                var l = new Lexer();
                var o = l.Lex( "5 < 1009" ).ToList();
                Check( "Result Count", o.Count == 3 );
                Check( "Int 1 type", o[0] is Int );
                Check( "Int 1 value", ((Int)o[0]).Value == 5 );
                Check( "Left Angle", o[1] is LAngle );
                Check( "Int 2 type", o[2] is Int );
                Check( "Int 2 value", ((Int)o[2]).Value == 1009 );
            });
        }

        private static string _name;
        private static List<string> _checks = new List<string>();
        private static void Test( string name, Action a )
        {
            _name = name;
            try
            {
                a();
                DisplayFailedChecks();
                if ( _checks.Count == 0 )
                {
                    Console.WriteLine( $"{_name} : Passed" );
                }
            }
            catch( Exception e )
            {
                Console.WriteLine( $"{_name} has thrown exception : {e}" );
            }
            finally
            {
                _name = "";
                _checks = new List<string>();
            }
        }

        private static void DisplayFailedChecks()
        {
            foreach( var check in _checks )
            {
                Console.WriteLine( $"{_name} failed check {check}" );
            }
        }
        private static void Check( string m, bool t )
        {
            if ( !t ) 
            {
                _checks.Add( m );
            }
        }
        private static void Assert( string m, bool t )
        {
            if ( !t )
            {
                throw new Exception( $"Failed assert {m}" );
            }
        }
    }
}
