using System;
using System.IO;
using System.Text;

namespace DeepLingo {

    public class Program {

        const string VERSION = "0.1";

        void Run(string[] args) {

            Console.WriteLine("Don't panic, use deep lingo");
            Console.WriteLine();

            if (args.Length != 1) {
                Console.Error.WriteLine(
                    "Please specify the name of the input file.");
                Environment.Exit(1);
            }

            try {            
                var inputPath = args[0];                
                var input = File.ReadAllText(inputPath);
                
                Console.WriteLine(String.Format(
                    "===== Tokens from: \"{0}\" =====", inputPath)
                );
                var count = 1;
                foreach (var tok in new Scanner(input).Start()) {
                    Console.WriteLine(String.Format("[{0}] {1}", 
                                                    count++, tok)
                    );
                }
                
            } catch (FileNotFoundException e) {
                Console.Error.WriteLine(e.Message);
                Environment.Exit(1);
            }                
        }

        public static void Main(string[] args) {
            new Program().Run(args);
        }
    }
}
