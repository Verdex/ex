
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
