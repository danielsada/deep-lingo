using System;
using System.IO;

namespace DeepLingo {

	public class ScannerTest {

		public void RunTests () {
			TestFile ("./example-programs/arrays.deep", "Arrays");
			TestFile(" ./example-programs/binary.deep", "Binary");
			TestFile(" ./example-programs/literals.deep", "Literals");
			TestFile(" ./example-programs/next_day.deep", "Next Day");
			TestFile(" ./example-programs/palindrome.deep", "Palindrome");
			TestFile(" ./example-programs/ultimate.deep", "Ultimate");
		}

		public void TestFile (string inputFile, string name) {
			var input = File.ReadAllText(inputFile);
			var count = 1;
			foreach (var tok in new Scanner(input).Start()) {
				if (tok.Category != TokenType.ILLEGAL_CHAR)
					Console.WriteLine ($"El compilador en (r: {tok.Row}, c:{tok.Column}) dió illegal char en el archivo {name} y cuenta {count++}");

			}

		}

	}

}