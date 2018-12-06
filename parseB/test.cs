
using System;
using System.Linq;
using System.Collections.Generic;

namespace ex.parseB
{
    public static class Tests 
    {
        public static void Main()
        {
            string Display( Expr c )
            {
                if ( c == null )
                    return "";
                if ( c is Int i )
                    return i.Value.ToString();
                if ( c is Variable v )
                    return v.Name;
                if ( c is BinOpCons bin )
                    return Display( bin.Primary ) + bin.BinOp + Display( bin.Rest );
                return "UNKNOWN";
            }

            Test( "var", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "5 b blah" );
                var parser = new Parser();
                var expr = parser.Parse( ts );
                Check( "var", expr is Int );
                Check( "value", ((Int)expr).Value == 5 );
            });

            Test( "bin", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "5 * 4" );
                var parser = new Parser();
                var expr = parser.Parse( ts );
                Check( "type", expr is BinOpCons );
                var p = ((BinOpCons)expr).Primary as Int;
                Check( "Primary value", p.Value == 5 );
                var r = ((BinOpCons)expr).Rest as Int;
                Check( "rest value", r.Value == 4 );
                Check( "op", ((BinOpCons)expr).BinOp == "*" );

            });

            Test( "bin", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "5 * 4 + 6" );
                var parser = new Parser();
                var expr = parser.Parse( ts );
                Console.WriteLine( Display( expr ) );
                Check( "result", Display( expr ) == "5*4+6" );

            });

            Test( "bin", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "5 *|> 4 + 6" );
                var parser = new Parser();
                var expr = parser.Parse( ts );
                Console.WriteLine( Display( expr ) );
                Check( "result", Display( expr ) == "5*|>4+6" );

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
