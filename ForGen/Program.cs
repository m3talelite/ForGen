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
			//Tester.testConverter(Tester.TestNDFA());
            Console.ReadLine();
			//Tester.generateAutomataImage(Tester.TestDFA());
			//Console.Write(Tester.TestNDFA().returnGrammar());
			//Console.ReadLine ();
			char[] alfabet = {'a', 'b', 'c', 'd'};
			Console.WriteLine(Tester.generateRandomRegex(alfabet,10).ToString());
			Tester.testConverter(Tester.TestNDFA2());
			//Tester.testRegularExpression();
			Console.Write(Tester.TestNDFA2().getGrammar().toBeautifulString());
			AutomataConverter c = new AutomataConverter();
			Console.WriteLine(c.NDFAToDFA(Tester.TestNDFA2()).getGrammar().toBeautifulString());

            //Tester.testRegularExpressionThompson2();
//			Tester.generateAutomataImage( Tester.generateAutomataImage() );
          	//Tester.generateAutomataImage( Tester.testReverse ( Tester.TestNDFA2() ) );
            Console.WriteLine("Programme successfully stopped on " + DateTime.Now.ToString("hh:mm:ss.fff"));
        }

	}
}
