
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

            string DisplayType( Type c )
            {
                if ( c == null )
                    return "";
                if ( c is SimpleType s )
                    return s.Name.ToString();
                if ( c is IndexType i )
                    return $"{i.Name}[ {DisplayType(i.Index)} ]";
                if ( c is TypeBinOpCons bin )
                    return $"{DisplayType( bin.Primary )} {bin.BinOp} {DisplayType( bin.Rest )}";
                if ( c is ParenType pt )
                    return $"( {DisplayType( pt.Type )} )";
                return "UNKNOWN";
            }

            Test( "int", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "5" );
                var parser = new Parser();
                var expr = parser.Parse( ts );
                Check( "int", expr is Int );
                Check( "value", ((Int)expr).Value == 5 );
            });

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

            Test( "Type simple", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "int" );
                var parser = new Parser();
                var type = parser.ParseType( ts );
                Console.WriteLine( DisplayType( type ) );
                Check( "result", DisplayType( type ) == "int" );
            });

            Test( "Type index", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "int[a]" );
                var parser = new Parser();
                var type = parser.ParseType( ts );
                Console.WriteLine( DisplayType( type ) );
                Check( "result", DisplayType( type ) == "int[ a ]" );
            });

            Test( "Type index index", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "int[a [ blarg ]]" );
                var parser = new Parser();
                var type = parser.ParseType( ts );
                Console.WriteLine( DisplayType( type ) );
                Check( "result", DisplayType( type ) == "int[ a[ blarg ] ]" );
            });

            Test( "simple Type Arrow", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "int -> int" );
                var parser = new Parser();
                var type = parser.ParseType( ts );
                Console.WriteLine( DisplayType( type ) );
                Check( "result", DisplayType( type ) == "int -> int" );
            });

            Test( "simple product type", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "int * int" );
                var parser = new Parser();
                var type = parser.ParseType( ts );
                Console.WriteLine( DisplayType( type ) );
                Check( "result", DisplayType( type ) == "int * int" );
            });

            Test( "ParenType 1", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "(a)" );
                var parser = new Parser();
                var type = parser.ParseType( ts );
                Console.WriteLine("!!" + DisplayType( type ) );
                Check( "result", DisplayType( type ) == "( a )" );
            });

            Test( "ParenType 2", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "(a -> b)" );
                var parser = new Parser();
                var type = parser.ParseType( ts );
                Console.WriteLine("!!" + DisplayType( type ) );
                Check( "result", DisplayType( type ) == "( a -> b )" );
            });

            Test( "ParenType 3", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "(a -> b) -> c" );
                var parser = new Parser();
                var type = parser.ParseType( ts );
                Console.WriteLine("!!" + DisplayType( type ) );
                Check( "result", DisplayType( type ) == "( a -> b ) -> c" );
            });

            Test( "ParenType 4", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "(a -> b) -> c[ (a * (b -> c))] -> d" );
                var parser = new Parser();
                var type = parser.ParseType( ts );
                Console.WriteLine("!!" + DisplayType( type ) );
                Check( "result", DisplayType( type ) == "( a -> b ) -> c[ ( a * ( b -> c ) ) ] -> d" );
            });

            Test( "complex type", () =>
            {
                var lex = new Lexer(); 
                var ts = lex.Lex( "int[a * b -> c[j]] * int -> b[t * g]" );
                var parser = new Parser();
                var type = parser.ParseType( ts );
                Console.WriteLine( DisplayType( type ) );
                Check( "result", DisplayType( type ) == "int[ a * b -> c[ j ] ] * int -> b[ t * g ]" );
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
