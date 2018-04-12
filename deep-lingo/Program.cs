using System;
using System.IO;
using System.Text;

namespace DeepLingo {

    public class Program {

        public Boolean DEBUG = false;
        const string VERSION = "0.1";

        void Run (string[] args) {

            Console.WriteLine ("Don't panic, use deep lingo");
            Console.WriteLine ();

            if (args.Length < 1) {
                Console.Error.WriteLine (
                    "Please specify the name of the input file.");
                Environment.Exit (1);
            }
            if (args[args.Length - 1] == "DEBUG") {
                DEBUG = true;
            }

            if (args[0] == "test") {
                ScannerTest tests = new ScannerTest ();
                tests.RunTests ();
            } else {
                try {

                    var inputPath = args[0];
                    String input = File.ReadAllText (inputPath);
                    if (DEBUG) {
                        foreach (var tok in new Scanner (input).Start ()) {
                            int count = 1;
                            if (tok.Lexeme == "42" && tok.Category != TokenType.TRUE) {
                                Console.WriteLine ("NO PUSISTE 42 como TRUE :'v");
                                throw new Exception ();
                            }
                            Console.WriteLine (String.Format ("[{0}] {1}",
                                count++, tok));
                        }
                    }
                    var parser = new Parser (new Scanner (input).Start ().GetEnumerator ());
                    var program = parser.Program ();
                    if (DEBUG) {
                        Console.WriteLine ("Syntax OK.");
                        Console.Write (program.ToStringTree ());
                    }
                    var semanticFirst = new SemanticFirst (DEBUG);
                    try {
                        semanticFirst.Visit ((dynamic) program);
                    } catch (SemanticError c) {
                        Console.WriteLine ("Semantic not correct.");
                        Console.WriteLine (c);
                    }
                    var semanticSecond = new SemanticSecond (DEBUG, semanticFirst.globalFunctions, semanticFirst.globalVariables);

                    if (DEBUG) {
                        Console.WriteLine ("Global Function Table");
                        Console.WriteLine ("============");
                        foreach (var entry in semanticFirst.globalFunctions) {
                            Console.Write (entry.Key + "\t");
                            Console.WriteLine (entry.Value.numParams);
                        }
                        Console.WriteLine ("Global Variable Table");
                        Console.WriteLine ("============");
                        foreach (var entry in semanticFirst.globalVariables) {
                            Console.Write (entry.Key + "\t");
                            Console.WriteLine (entry.Value.numParams);
                        }
                    }
                } catch (FileNotFoundException e) {
                    Console.Error.WriteLine (e.Message);
                    Environment.Exit (1);
                } catch (SyntaxError s) {
                    Console.WriteLine (s);
                }
            }
        }

        public static void Main (string[] args) {
            new Program ().Run (args);
        }
    }
}