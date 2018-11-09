
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
