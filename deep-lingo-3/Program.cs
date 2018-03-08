using System;
using System.IO;
using System.Text;

namespace DeepLingo {

    public class Program {

        const string VERSION = "0.1";

        void Run (string[] args) {

            Console.WriteLine ("Don't panic, use deep lingo");
            Console.WriteLine ();

            if (args.Length != 1) {
                Console.Error.WriteLine (
                    "Please specify the name of the input file.");
                Environment.Exit (1);
            }

            if (args[0] == "test") {
                ScannerTest tests = new ScannerTest ();
                tests.RunTests ();
            } else {
                try {
                    var inputPath = args[0];
                    String input = File.ReadAllText (inputPath);

                    // Console.WriteLine (String.Format (
                    //     "===== Tokens from: \"{0}\" =====", inputPath));
                    // int count = 1;

                    // foreach (var tok in new Scanner (input).Start ()) {
                    //     Console.WriteLine (String.Format ("[{0}] {1}",
                    //         count++, tok));
                    // }
                    var parser = new Parser (new Scanner (input).Start ().GetEnumerator ());
                    parser.Program ();
                    // Cuando se arregle paso dos, decomentar la linea de aqui abajo.
                    //var prog = parser.Program ();
                    Console.WriteLine ("Syntax OK.");
                    // Parser parser = new Parser (new Scanner (input).Start ().GetEnumerator ());
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