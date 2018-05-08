using System;
using DeepLingo;

    class CallDll {
        private static int Wow(int a, int b){
            return a + b;
        }

        public static void Main () {
            Utils.New(10);
            Utils.Printi(42);
            Console.WriteLine(Wow(2,3));
        }
    }