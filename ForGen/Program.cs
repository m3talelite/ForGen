using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ForGen
{
    class Program
    {
        static void Main(string[] args)
        {

			Console.WriteLine("Programme started on "+DateTime.Now.ToString("hh:mm:ss.fff"));
			Tester.testConverter(Tester.TestNDFA());
            Console.ReadLine();
			Tester.generateAutomataImage(Tester.TestDFA());
			Console.ReadLine ();
			Console.WriteLine("Programme successfully stopped on "+DateTime.Now.ToString("hh:mm:ss.fff"));
        }

	}
}
