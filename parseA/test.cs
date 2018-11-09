
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
                Check( "Result Check", o.Count == 1 );
                Check( "1", o[0] is Function );
            });

            Test( "Should Parse Line Comment On Last Line of File", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"//func{ {func func( func
//1, 2, 3, 4, 5
//func" ).ToList();
                Check( "Result Check", o.Count == 0 );
            });

            Test( "Should Parse Only Line Comment", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"//" ).ToList();
                Check( "Result Check", o.Count == 0 );
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
                Check( "Result Check", o.Count == 3 );
            });

            Test( "Should Parse Entire File Block Comment", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"/*func 
1234
other func
*/" ).ToList();
                Check( "Result Check", o.Count == 0 );
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
                Check( "Result Check", o.Count == 2 );
            });

            Test( "Should Parse Only Starting Block Comment", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"/*" ).ToList();
                Check( "Result Check", o.Count == 0 );
            });

            Test( "Should Parse Only Block Comment", () =>
            {
                var l = new Lexer();
                var o = l.Lex( @"/**/" ).ToList();
                Check( "Result Check", o.Count == 0 );
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
