﻿/*
* Copyright 2015 Guus Beckett, Joris Mathijssen, Jelle Braat, Jim van Abkoude
*
* Licensed under the EUPL, Version 1.1 or – as soon they will be approved by the European Commission - subsequent versions of the EUPL (the "Licence");
* You may not use this work except in compliance with the Licence.
* You may obtain a copy of the Licence at:
*
* http://ec.europa.eu/idabc/eupl5
*
* Unless required by applicable law or agreed to in writing, software distributed under the Licence is distributed on an "AS IS" basis,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the Licence for the specific language governing permissions and limitations under the Licence.
*/

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

		static void Main()
		{
			Console.WriteLine("1. NDFA/DFA -> reguliere grammatica" +
				Environment.NewLine + "2. Reguliere Expressie -> NDFA"+
				Environment.NewLine + "3. NDFA -> DFA"+
				Environment.NewLine + "4. Minimalisatie DFA"+
				Environment.NewLine + "5. Woorden generator"+
				Environment.NewLine + "6. Bevat woord"+
				Environment.NewLine + "7. Genereer oefententamen"+
				Environment.NewLine + "0. Afsluiten");
			var ans = Console.ReadLine();
			int choice=0;
			if (int.TryParse(ans, out choice))
			{
				switch (choice)
				{
					case 1:
						NdfaDfaToGrammar();
						break;
					case 2:
						RegExToNdfaDfa();
						break;
					case 3:
						NdfaToDfa();
						break;
					case 4:
						Minimalization();
						break;
					case 5:
						WoordenGenerator();
						break;
					case 6:
						BevatWoord();
						break;
					case 7:
						GenerateTentame();
						break;
					case 0:
						Console.Clear();
						Console.WriteLine("( ͡° ͜ʖ ͡°)Tot ziens( ͡° ͜ʖ ͡°)");
						break;
					default:
						Console.WriteLine("Deze optie is niet beschikbaar." +
							Environment.NewLine + "Druk op een knop om te sluiten");
						Console.ReadKey();
						break;
				}
			}
			else
			{
				Console.WriteLine("Vul alstublieft het nummer van de keuze in."+
					Environment.NewLine + "Druk op een knop om te sluiten");
				Console.ReadKey();
			}
		}

		static public void NdfaDfaToGrammar(){
			Console.Clear();
			Console.WriteLine("1. NDFA -> reguliere grammatica" +
			Environment.NewLine + "2. DFA -> reguliere grammatica");
			var ans = Console.ReadLine();
			int choice=0;
			if (int.TryParse(ans, out choice))
			{
				switch (choice)
				{
					case 1:
						Console.Clear();
						Tester.generateAutomataImage(Tester.TestNDFA2());
						Console.Write(Tester.TestNDFA2().getGrammar().toBeautifulString());
						ResetToMenu();
						break;
					case 2:
						Console.Clear();
						Tester.generateAutomataImage(Tester.TestDFA());
						Console.WriteLine(Tester.TestDFA().getGrammar().toBeautifulString());
						ResetToMenu();
						break;
					default:
						Console.WriteLine("Deze optie is niet beschikbaar." +
						Environment.NewLine + "Druk op een knop om terug te gaan");
						ResetToMenu();
						break;
				}
			}
			else
			{
				Console.WriteLine("Vul alstublieft het nummer van de keuze in."+
					Environment.NewLine + "Druk op een knop om terug te gaan");
				ResetToMenu();
			}
		}

		static public void NdfaToDfa(){
			Console.Clear();
			Console.WriteLine("1. NDFA -> DFA (zonder Epsilon)" +
				Environment.NewLine + "2. NDFA -> DFA (met Epsilon)" +
				Environment.NewLine + "3. NDFA -> DFA (Random NDFA)");
			var ans = Console.ReadLine();

			AutomataConverter c = new AutomataConverter();

			int choice=0;
			if (int.TryParse(ans, out choice))
			{
				switch (choice)
				{
					case 1:
						Console.Clear();
						Automata<string> a = Tester.TestNDFA();
						Console.WriteLine("De volgende NDFA:");
						a.printTransitions();
						Tester.generateAutomataImage(a);
						Console.WriteLine("is deze DFA: (Druk op een knop)");
						Console.ReadLine();
						c.NDFAToDFA(a).printTransitions();
						Tester.generateAutomataImage(c.NDFAToDFA(a));
						ResetToMenu();
						break;
					case 2:
						Console.Clear();
						Automata<string> b = Tester.TestNDFA2();
						Console.WriteLine("De volgende NDFA:");
						b.printTransitions();
						Tester.generateAutomataImage(b);
						Console.WriteLine("is deze DFA: (Druk op een knop)");
						Console.ReadLine();
						c.NDFAToDFA(b).printTransitions();
						Tester.generateAutomataImage(c.NDFAToDFA(b));
						ResetToMenu();
						break;
					case 3:
						Console.Clear();
						Automata<string> d= Tester.generateRandomNdfa();
						Console.WriteLine("De volgende NDFA:");
						d.printTransitions();
						Tester.generateAutomataImage(d);
						Console.WriteLine("is deze DFA: (Druk op een knop)");
						Console.ReadLine();
						c.NDFAToDFA(d).printTransitions();
						Tester.generateAutomataImage(c.NDFAToDFA(d));
						ResetToMenu();
						break;
					default:
						Console.WriteLine("Deze optie is niet beschikbaar." +
							Environment.NewLine + "Druk op een knop om terug te gaan");
						ResetToMenu();
						break;
				}
			}
			else
			{
				Console.WriteLine("Vul alstublieft het nummer van de keuze in."+
					Environment.NewLine + "Druk op een knop om terug te gaan");
				ResetToMenu();
			}
		}

		static public void RegExToNdfaDfa(){
			Console.Clear();
			Console.WriteLine("1. RegEx -> NDFA");
			var ans = Console.ReadLine();
			int choice=0;
			Automata<String> auto = new Automata<String>();
			int num = 0;
			if (int.TryParse(ans, out choice))
			{
				switch (choice)
				{
					case 1:
						Console.Clear();
						char[] alfabet = { 'a', 'b', 'c' };
						RegularExpression b = Tester.generateRandomRegex(alfabet, 5);
						Console.WriteLine(b.ToString() +
							Environment.NewLine + "Geeft:");
						b.regexToNDFA(ref num, ref auto);
						auto.printTransitions();
						Tester.generateAutomataImage(auto);
						ResetToMenu();
						break;
					default:
						Console.WriteLine("Deze optie is niet beschikbaar." +
							Environment.NewLine + "Druk op een knop om terug te gaan");
						ResetToMenu();
						break;
				}
			}
			else
			{
				Console.WriteLine("Vul alstublieft het nummer van de keuze in."+
					Environment.NewLine + "Druk op een knop om terug te gaan");
				ResetToMenu();
			}
		}

		static public void Minimalization(){
			Console.Clear();
			Console.WriteLine("1. Minimalization DFA" + 
			Environment.NewLine + "2. Minimalization Random DFA");
			var ans = Console.ReadLine();
			AutomataMinimalization m = new AutomataMinimalization();
			AutomataConverter c = new AutomataConverter();
			int choice=0;
			if (int.TryParse(ans, out choice))
			{
				switch (choice)
				{
					case 1:
						Console.Clear();
						Automata<string> a = Tester.TestDFA2 ();
						Console.WriteLine("De volgende DFA: ");
						a.printTransitions();
						Tester.generateAutomataImage(a);
						Console.WriteLine("Is in Minimalisatie: (druk op een knop)");
						Console.ReadLine();
						Automata<String> mini = m.Minimization(a);
						mini.printTransitions();
						Tester.generateAutomataImage(mini);
						ResetToMenu();
						break;
					case 2:
						Console.Clear();
						Automata<string> b = c.renameStates(Tester.generateRandomDfa());
						Console.WriteLine("De volgende DFA: ");
						b.printTransitions();
						Tester.generateAutomataImage(b);
						Console.WriteLine("Is in Minimalisatie: (druk op een knop)");
						Console.ReadLine();
						Automata<String> mini2 = m.Minimization(b);
						mini2.printTransitions();
						Tester.generateAutomataImage(mini2);
						ResetToMenu();
						break;
					default:
						Console.WriteLine("Deze optie is niet beschikbaar." +
							Environment.NewLine + "Druk op een knop om terug te gaan");
						ResetToMenu();
						break;
				}
			}
			else
			{
				Console.WriteLine("Vul alstublieft het nummer van de keuze in."+
					Environment.NewLine + "Druk op een knop om terug te gaan");
				ResetToMenu();
			}
		}

		static public void GenerateTentame(){
			Console.Clear();
			Console.WriteLine("1. Genereer oefententamen");
			var ans = Console.ReadLine();
			int choice=0;
			if (int.TryParse(ans, out choice))
			{
				switch (choice)
				{
					case 1:
						Console.Clear();
						LaTeXGenerator g = new LaTeXGenerator();
						g.generateLaTeXExam();
						ResetToMenu();
						break;
					default:
						Console.WriteLine("Deze optie is niet beschikbaar." +
							Environment.NewLine + "Druk op een knop om terug te gaan");
						ResetToMenu();
						break;
				}
			}
			else
			{
				Console.WriteLine("Vul alstublieft het nummer van de keuze in."+
					Environment.NewLine + "Druk op een knop om terug te gaan");
				ResetToMenu();
			}
		}

		static public void WoordenGenerator(){
			Console.Clear();
			Console.WriteLine("1. Woorden generator regex" +
				Environment.NewLine + "2. Woorden generator thompson");
			var ans = Console.ReadLine();
			int choice=0;
			if (int.TryParse(ans, out choice))
			{
				switch (choice)
				{
					case 1:
						Console.Clear();
						Tester.testRegularExpression();
						ResetToMenu();
						break;
					case 2:
						Console.Clear();
						Tester.testRegularExpressionThompson2();
						ResetToMenu();
						break;
					default:
						Console.WriteLine("Deze optie is niet beschikbaar." +
							Environment.NewLine + "Druk op een knop om terug te gaan");
						ResetToMenu();
						break;
				}
			}
			else
			{
				Console.WriteLine("Vul alstublieft het nummer van de keuze in."+
					Environment.NewLine + "Druk op een knop om terug te gaan");
				ResetToMenu();
			}
		}

		static public void BevatWoord(){
			Console.Clear();
			Console.WriteLine("1. Bevat woord");
			Automata <string> a = new Automata<string>();
			AutomataConverter c = new AutomataConverter();
			var ans = Console.ReadLine();
			int choice=0;
			if (int.TryParse(ans, out choice))
			{
				switch (choice)
				{
					case 1:
						Console.Clear();
						Console.WriteLine("De volgende NDFA/DFA is gegenereerd:");
						a = c.renameStates(Tester.generateRandomNdfa());
//						a.printTransitions();
						Tester.generateAutomataImage(a);
						Console.WriteLine("Geef een string mee en kijk of hij word geaccepteerd. (BV: aaabaabaaa)");
						var input = Console.ReadLine();
						if (a.isStringAccepted(input)) {
							Console.WriteLine("De string word geaccepteerd.");
						}
						else Console.WriteLine("De string word NIET geaccepteerd.");
						ResetToMenu();
						break;
					default:
						Console.WriteLine("Deze optie is niet beschikbaar." +
							Environment.NewLine + "Druk op een knop om terug te gaan");
						ResetToMenu();
						break;
				}
			}
			else
			{
				Console.WriteLine("Vul alstublieft het nummer van de keuze in."+
					Environment.NewLine + "Druk op een knop om terug te gaan");
				ResetToMenu();
			}
		}

		static public void ResetToMenu(){
			Console.WriteLine("Druk op een knop om terug te gaan naar het hoofd menu");
			Console.ReadKey();
			Console.Clear();
			Main();
		}

        static void OldMain(string[] args)
        {

			Console.WriteLine("Programme started on "+DateTime.Now.ToString("hh:mm:ss.fff"));
			Console.ReadLine();

			/*
			//TEST CODE FOR CONVERTER
			Tester.testConverter(Tester.TestNDFA());
			Tester.testConverter(Tester.TestNDFA2());
			*/
			//TEST CODE FOR CHECKING IF STRING IS ACCEPTED BY DFA/NDFA
			//bool test = Tester.TestNDFA2().acceptString("a");
			//bool test2 = Tester.TestNDFA2().isStringAccepted("bbbbbbbbbbbbbbbbbbbbacd");
			//Tester.generateAutomataImage(Tester.TestNDFA2());
			//Console.WriteLine("String accepted: " + test2.ToString());
			/*
			//TEST CODE FOR GENERATING IMAGES
			Tester.generateAutomataImage(Tester.TestDFA());
			//Tester.generateAutomataImage( Tester.generateAutomataImage() );
			//Tester.generateAutomataImage( Tester.testReverse ( Tester.TestNDFA2() ) );
			//Tester.generateAutomataImage(Tester.testConverter(Tester.TestNDFA2()));
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


			//Tester.generateAutomataImage(Tester.testReverse(Tester.TestDFA()));

			//AutomataConverter c = new AutomataConverter();

			//Tester.generateAutomataImage(c.renameStates(c.NDFAToDFA(Tester.TestNDFA2())));
			//Tester.generateAutomataImage(c.renameStates(c.NDFAToDFA(Tester.testReverse(c.NDFAToDFA(Tester.TestNDFA2())))));

			//Tester.generateAutomataImage(c.renameStates(c.NDFAToDFA(Tester.testReverse(c.NDFAToDFA(Tester.testReverse(c.NDFAToDFA(Tester.TestNDFA2())))))));
			//Automata<String> debug = new Automata<string>(c.NDFAToDFA(Tester.testReverse(Tester.TestNDFA2())));
			//debug = c.NDFAToDFA(Tester.testReverse(debug));
			//debug.printTransitions();
			//c.renameStates(debug);


			//TEST CODE FOR Minimalization

			//Tester.generateAutomataImage(Tester.testMinimalization(Tester.TestDFA2()));
			//Tester.generateAutomataImage(Tester.testMinimalization(Tester.TestDFA()));
			LaTeXGenerator g = new LaTeXGenerator();
			g.generateLaTeXExam();


			/*
			//TEST CODE FOR PRINTING GRAMMAR
			Console.Write(Tester.TestNDFA2().getGrammar().toBeautifulString());
			AutomataConverter c = new AutomataConverter();
			Console.WriteLine(c.NDFAToDFA(Tester.TestNDFA2()).getGrammar().toBeautifulString());
			*/

			/*
            //TEST CODE FOR THOMPSON
			Tester.testRegularExpressionThompson2();
            */
			Console.WriteLine("Programme successfully stopped on " + DateTime.Now.ToString("hh:mm:ss.fff"));
        }

	}
}
