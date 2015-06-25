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
			//Tester.generateAutomataImage(Tester.TestDFA());
			//Console.Write(Tester.TestNDFA().returnGrammar());
			Console.Write(Tester.TestNDFA2().getGrammar().toString());
			Console.ReadLine ();
			Console.Write(Tester.TestNDFA2().getGrammar().toBeautifulString());
			Console.ReadLine ();
			Tester.testConverter(Tester.TestNDFA2());
			Tester.generateAutomataImage(Tester.TestNDFA2());
		    Console.ReadLine();
			Console.WriteLine("Programme successfully stopped on "+DateTime.Now.ToString("hh:mm:ss.fff"));
        }

	}
}
