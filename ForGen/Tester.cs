using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ForGen
{
	public class Tester
	{
		public Tester ()
		{
		}

		static public void generateAutomataImage(Automata<String> automata)
		{
			string output_file = "";
			string executable_file = "";
			string file_opener = "";
            bool windows = false;
			int p = (int) Environment.OSVersion.Platform;
			if ((p == 4) || (p == 6) || (p == 128)) { //UNIX
				output_file = "/tmp/tmp.png";
				executable_file = "dot";
				file_opener = "xdg-open";
			} else {//NOT UNIX FIX THIS
                windows = true;
				output_file = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                output_file = output_file + "\\img.png";
				executable_file = "C:\\Program Files (x86)\\Graphviz2.38\\bin\\dot.exe";
			}


			string dotinfo = automata.printGraphviz();

			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.CreateNoWindow = false;
				startInfo.UseShellExecute = false;
				startInfo.FileName = executable_file;
				startInfo.RedirectStandardInput = true;
				startInfo.WindowStyle = ProcessWindowStyle.Hidden;
				startInfo.Arguments = " -v -Tpng -o " + output_file;

				Process exeProcess = new Process();
				exeProcess.StartInfo = startInfo;
				exeProcess.Start ();
				//exeProcess.WaitForInputIdle();
				StreamWriter writer  = exeProcess.StandardInput;
				writer.Write (dotinfo);
				writer.Flush ();
				writer.WriteLine ();
                if(!windows)
    				exeProcess.WaitForInputIdle();
				writer.Close ();
				exeProcess.WaitForExit();
                if (!windows)
                {
                    Process openProcess = new Process();
                    openProcess.StartInfo.FileName = file_opener;
                    openProcess.StartInfo.Arguments = output_file;
                    openProcess.Start();
                }
                else
                {
                    Process.Start(output_file);
                }
			}
			catch
			{
                Console.Write("Random exception");
			}
		}

		static public Automata<String> TestDFA() {
			char [] alphabet = {'a', 'b'};
			Automata<String> m = new Automata<String>(alphabet);
			m.addTransition( new Transition<String> ("q0", 'a', "q1") );
			m.addTransition( new Transition<String> ("q0", 'b', "q4") );

			m.addTransition( new Transition<String> ("q1", 'a', "q4") );
			m.addTransition( new Transition<String> ("q1", 'b', "q2") );

			m.addTransition( new Transition<String> ("q2", 'a', "q3") );
			m.addTransition( new Transition<String> ("q2", 'b', "q4") );

			m.addTransition( new Transition<String> ("q3", 'a', "q1") );
			m.addTransition( new Transition<String> ("q3", 'b', "q2") );

			// the error state, loops for a and b:
			m.addTransition( new Transition<String> ("q4", 'a') );
			m.addTransition( new Transition<String> ("q4", 'b') );

			// only on start state in a dfa:
			m.defineAsStartState("q0");

			// two final states:
			m.defineAsFinalState("q2");
			m.defineAsFinalState("q3");

			return m;
		}

		static public Automata<String> TestNDFA() {
			char [] alphabet = {'a', 'b'};
			Automata<String> m = new Automata<String>(alphabet);
			m.addTransition( new Transition<String> ("1", 'a', "2") );
			m.addTransition( new Transition<String> ("2", 'b', "2") );

			m.addTransition( new Transition<String> ("2", 'b', "4") );
			m.addTransition( new Transition<String> ("1", '$', "3") );

			m.addTransition( new Transition<String> ("3", '$', "2") );
			m.addTransition( new Transition<String> ("3", 'a', "4") );

			m.addTransition( new Transition<String> ("4", 'a', "3") );

			// only on start state in a dfa:
			m.defineAsStartState("1");

			// two final states:
			m.defineAsFinalState("3");
			m.defineAsFinalState("4");

			return m;
		}

		static public Automata<String> TestNDFA2() {
			char [] alphabet = {'a', 'b', 'c', 'd'};
			Automata<String> m = new Automata<String>(alphabet);
			m.addTransition( new Transition<String> ("0", '$', "1") );
			m.addTransition( new Transition<String> ("0", '$', "3") );

			m.addTransition( new Transition<String> ("1", 'b', "2") );
			m.addTransition( new Transition<String> ("3", 'a', "4") );

			m.addTransition( new Transition<String> ("2", '$', "5") );
			m.addTransition( new Transition<String> ("4", '$', "5") );

			m.addTransition( new Transition<String> ("5", '$', "0") );

			m.addTransition( new Transition<String> ("5", '$', "6") );

			m.addTransition( new Transition<String> ("6", 'b', "7") );

			m.addTransition( new Transition<String> ("7", 'c', "8") );

			m.addTransition( new Transition<String> ("8", 'd', "9") );

			// only on start state in a dfa:
			m.defineAsStartState("0");

			// two final states:
			m.defineAsFinalState("9");

			return m;
		}

		static public Automata<String> TestProcedure2() {
			char [] alphabet = {'a', 'b'};
			Automata<String> m = new Automata<String>(alphabet);

			m.addTransition( new Transition<String> ("A", 'a', "C") );
			m.addTransition( new Transition<String> ("A", 'b', "B") );
			m.addTransition( new Transition<String> ("A", 'b', "C") );

			m.addTransition( new Transition<String> ("B", 'b', "C") );
			m.addTransition( new Transition<String> ("B", "C") );

			m.addTransition( new Transition<String> ("C", 'a', "D") );
			m.addTransition( new Transition<String> ("C", 'a', "E") );
			m.addTransition( new Transition<String> ("C", 'b', "D") );

			m.addTransition( new Transition<String> ("D", 'a', "B") );
			m.addTransition( new Transition<String> ("D", 'a', "C") );

			m.addTransition( new Transition<String> ("E", 'a') );
			m.addTransition( new Transition<String> ("E", "D") );

			// only on start state in a dfa:
			m.defineAsStartState("A");

			// two final states:
			m.defineAsFinalState("C");
			m.defineAsFinalState("E");

			return m;
		}

		public static void testConverter(Automata<String> ndfa)
		{
			AutomataConverter c = new AutomataConverter();
			c.NDFAToDFA(ndfa);
		}

		public static void testRegularExpression()
		{
			RegularExpression exp1, exp2, exp3, exp4, exp5, a, b, all;

			a = new RegularExpression("a");
			b = new RegularExpression("b");
			// exp 1 baa
			exp1 = new RegularExpression("baa");
			// exp 2 bb
			exp2 = new RegularExpression("bb");
			// exp 3 baa | bb
			exp3 = exp1.or(exp2);
			//all = (a|b)*
			all = (a.or(b)).star();
			// exp 4 (baa | bb)+
			exp4 = exp3.plus();
			// exp 4 (baa | bb)+ (a|b)*
			exp5 = exp4.dot(all);

			#region PRINTREGULAREXPRESSION

			Console.WriteLine("taal van (baa):");
			foreach (String o in exp1.getLanguage(5))
			{
				Console.Write(o + " ");
			}
			Console.WriteLine("");

			Console.WriteLine("taal van (bb):");
			foreach (String o in exp2.getLanguage(5))
			{
				Console.Write(o + " ");
			}
			Console.WriteLine("");

			Console.WriteLine("taal van (baa | bb):");
			foreach (String o in exp3.getLanguage(5))
			{
				Console.Write(o + " ");
			}
			Console.WriteLine("");

			Console.WriteLine("taal van (a|b)*:");
			foreach (String o in all.getLanguage(5))
			{
				Console.Write(o + " ");
			}
			Console.WriteLine("");

			Console.WriteLine("taal van (baa | bb)+:");
			foreach (String o in exp5.getLanguage(5))
			{
				Console.Write(o + " ");
			}
			Console.WriteLine("");

			Console.WriteLine("taal van (baa | bb)+ (a|b)*:");
			foreach (String o in exp5.getLanguage(6))
			{
				Console.Write(o + " ");
			}
			Console.WriteLine("");

			#endregion
		}

	}
}

