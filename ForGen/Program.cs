using System;

namespace ForGen
{
    class Program
    {
        static void Main(string[] args)
        {

			Console.WriteLine("Programme started on "+DateTime.Now.ToString("hh:mm:ss.fff"));
			Tester.testConverter(Tester.TestNDFA());
            Console.ReadLine();
			Console.WriteLine("Programme successfully stopped on "+DateTime.Now.ToString("hh:mm:ss.fff"));
        }
    }
}
