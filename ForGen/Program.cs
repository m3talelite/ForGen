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
			Console.ReadLine();
			/* 
			//TEST CODE FOR CONVERTER
			Tester.testConverter(Tester.TestNDFA());
			Tester.testConverter(Tester.TestNDFA2());
			*/

			/*
			//TEST CODE FOR GENERATING IMAGES
			Tester.generateAutomataImage(Tester.TestDFA());
			//Tester.generateAutomataImage( Tester.generateAutomataImage() );
			//Tester.generateAutomataImage( Tester.testReverse ( Tester.TestNDFA2() ) );
			*/

			/*
			//TEST CODE FOR GENERATING RANDOM REGEX
			char[] alfabet = {'a', 'b', 'c', 'd'};
			Console.WriteLine(Tester.generateRandomRegex(alfabet, 10).ToString());
			*/
			/*
			//TEST CODE FOR REGEX
			Tester.testRegularExpression();
			*/


			//TEST CODE FOR PRINTING GRAMMAR
			Console.Write(Tester.TestNDFA2().getGrammar().toBeautifulString());
			AutomataConverter c = new AutomataConverter();
			Console.WriteLine(c.NDFAToDFA(Tester.TestNDFA2()).getGrammar().toBeautifulString());

			/*
            //TEST CODE FOR THOMPSON
			Tester.testRegularExpressionThompson2();
            */
			Console.WriteLine("Programme successfully stopped on " + DateTime.Now.ToString("hh:mm:ss.fff"));
        }

	}
}
