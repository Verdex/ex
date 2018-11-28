
using System;
using System.Linq;
using System.Collections.Generic;

namespace ex.parseB
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
                Check( "func lexed", o[0] is TFunction );
                Check( "left big arrow lexed", o[1] is TLBigArrow );
            });

            Test( "Should Parse keyword look-a-likes as symbols", () =>
            {
                var l = new Lexer();
                var o = l.Lex( "func_ func0 _func funcx" ).ToList();
                Check( "Result Count", o.Count == 4 );
                Check( "1", o[0] is TSymbol );
                Check( "2", o[1] is TSymbol );
                Check( "3", o[2] is TSymbol );
                Check( "4", o[3] is TSymbol );
            });

            Test( "Should Parse keyword as keyword", () =>
            {
                var l = new Lexer();
                var o = l.Lex( "func{ {func func( func" ).ToList();
                Check( "Result Count", o.Count == 7 );
                Check( "1", o[0] is TFunction );
                Check( "2", o[1] is TLBracket );
                Check( "3", o[2] is TLBracket );
                Check( "4", o[3] is TFunction );
                Check( "5", o[4] is TFunction );
                Check( "6", o[5] is TLParen );
                Check( "7", o[6] is TFunction );
            });

            Test( "Should Parse Line Comment", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"//func{ {func func( func
//1, 2, 3, 4, 5
func" ).ToList();
                Check( "Result Count", o.Count == 1 );
                Check( "1", o[0] is TFunction );
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
                    Check( "Less Than", o[0] is TLAngle );
                    Check( "Greater Than", o[1] is TRAngle );
                    Check( "Add", o[2] is TAdd );
                    Check( "Mult", o[3] is TMul );
                    Check( "Sub", o[4] is TSub );
                    Check( "Div", o[5] is TDiv );
                    Check( "Double Equal", o[6] is TDoubleEqual );
                    Check( "Equal", o[7] is TEqual );
                    Check( "Not Equal", o[8] is TNotEqual );
                    Check( "Bang", o[9] is TBang );
                    Check( "Left Big Arrow", o[10] is TLBigArrow );
                    Check( "Right Big Arrow", o[11] is TRBigArrow );
                    Check( "Left Reverse Big Arrow", o[12] is TLReverseBigArrow );
                    Check( "Right Reverse Big Arrow", o[13] is TRReverseBigArrow );
                    Check( "Double Or", o[14] is TDoubleOr );
                    Check( "Double And", o[17] is TDoubleAnd );
                });

                Test( "Int", () =>
                {
                    var l = new Lexer();
                    var o = l.Lex( "5 < 1009" ).ToList();
                    Check( "Result Count", o.Count == 3 );
                    Check( "Int 1 type", o[0] is TInt );
                    Check( "Int 1 value", ((TInt)o[0]).Value == 5 );
                    Check( "Left Angle", o[1] is TLAngle );
                    Check( "Int 2 type", o[2] is TInt );
                    Check( "Int 2 value", ((TInt)o[2]).Value == 1009 );
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
