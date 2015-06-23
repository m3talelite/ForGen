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
			//testRegularExpression();
			//Console.WriteLine(TestNDFA().isDFA ());
            //Console.ReadLine();
			//Console.WriteLine("Programme successfully stopped on "+DateTime.Now.ToString("hh:mm:ss.fff"));
			generateAutomataImage(Tester.TestDFA());
			Console.ReadLine ();
			Console.WriteLine("Programme successfully stopped on "+DateTime.Now.ToString("hh:mm:ss.fff"));

        }

		static void generateAutomataImage(Automata<String> automata)
		{
			string output_file = "";
			string executable_file = "";
			string file_opener = "";
			int p = (int) Environment.OSVersion.Platform;
			if ((p == 4) || (p == 6) || (p == 128)) { //UNIX
				output_file = "/tmp/tmp.png";
				executable_file = "dot";
				file_opener = "xdg-open";
			} else {//NOT UNIX FIX THIS
				output_file = "/tmp/tmp.png";
				executable_file = "dot";
				file_opener = "xdg-open";
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
				exeProcess.WaitForInputIdle ();
				writer.Close ();
				Process openProcess = new Process();
				openProcess.StartInfo.FileName = file_opener;
				openProcess.StartInfo.Arguments = output_file;
				openProcess.Start();
			}
			catch
			{
				// Log error. FIX THIS
			}
		}

	}
}
