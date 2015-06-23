using System;

namespace ForGen
{
    class Program
    {
        static void Main(string[] args)
        {

			Console.WriteLine("Programme started on "+DateTime.Now.ToString("hh:mm:ss.fff"));
			Tester.testRegularExpression();
			Console.WriteLine(Tester.TestNDFA().isDFA ());
            Console.ReadLine();
			Console.WriteLine("Programme successfully stopped on "+DateTime.Now.ToString("hh:mm:ss.fff"));

        }
    }
}
