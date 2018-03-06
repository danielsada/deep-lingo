using System;
using System.IO;

namespace DeepLingo {

	public class ScannerTest {

		public void RunTests () {

				TestFile ("./example-programs/arrays.deep", "Arrays");
				TestFile ("./example-programs/binary.deep", "Binary");
				TestFile ("./example-programs/literals.deep", "Literals");
				TestFile ("./example-programs/next_day.deep", "Next Day");
				TestFile ("./example-programs/palindrome.deep", "Palindrome");
				TestFile ("./example-programs/ultimate.deep", "Ultimate");
				TestFile ("./example-programs/factorial.deep", "Factorial");
				Console.WriteLine($"You passed out of 7 tests.");
			
		}

		public void TestFile (string inputFile, string name) {
			Console.WriteLine ($"Running {name}, ==Sit tight==");
			var input = File.ReadAllText (inputFile);
			var count = 1;
			foreach (var tok in new Scanner (input).Start ()) {
				if(tok.Lexeme == "42" && tok.Category != TokenType.TRUE){
					Console.WriteLine("NO PUSISTE 42 como TRUE :'v");
					throw new Exception();
				}
				Console.WriteLine (String.Format ("[{0}] {1}",
					count++, tok));
			}
			var parser = new Parser (new Scanner (input).Start ().GetEnumerator ());
			parser.Program ();

		}

	}

}