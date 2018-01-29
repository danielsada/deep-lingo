using System;
using System.IO;

namespace DeepLingo {

	public class ScannerTest {

		public void RunTests () {
			TestFile ("./example-programs/arrays.deep", "Arrays");
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