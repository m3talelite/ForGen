using System;
using System.IO;
using System.Collections;

namespace ForGen
{
	public class LaTeXGenerator
	{
		public LaTeXGenerator()
		{
			
		}

		public void generateLaTeXExam()
		{
			String tentamenText = frontpageLatex();
			// Folder creation
			String mainExamFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TentamenGenerator/";
			String currentExamFolder = "";
			String imagesFolder = "";
			String date = DateTime.Now.ToString("dd-MM-yyyy_hh:mm:ss");

			if (Directory.Exists(mainExamFolder)==false) {
				System.IO.Directory.CreateDirectory(mainExamFolder);
			}

			if (Directory.Exists(mainExamFolder + "Tentamen_"+ date + "/") == false)
			{
				Console.WriteLine("Created folder: " + "Tentamen_"+date);
				System.IO.Directory.CreateDirectory(mainExamFolder + date + "/");
				currentExamFolder = mainExamFolder + date + "/";
			}

			if (currentExamFolder == "")
				return;

			imagesFolder = currentExamFolder + "Images/";
			System.IO.Directory.CreateDirectory(imagesFolder);

			AutomataConverter c = new AutomataConverter();

			// defining regexes
			char[] alfabet = {'a', 'b'};
			RegularExpression tentamenRegex_regexDFA = Tester.generateRandomRegex(alfabet, 2);
			RegularExpression tentamenRegex_regexNDFA = Tester.generateRandomRegex(alfabet, 2);
			RegularExpression tentamenRegex_DFAregex = Tester.generateRandomRegex(alfabet, 2);
			RegularExpression tentamenRegex_NDFAregex = Tester.generateRandomRegex(alfabet, 2);

			// Generating exam images
			Tester.generateAutomataImage(Tester.TestNDFA2(),imagesFolder+"NDFA_FOR_NDFA_TO_DFA.pdf","pdf");
			Tester.generateAutomataImage(c.NDFAToDFA(Tester.TestNDFA2()),imagesFolder+"DFA_FOR_NDFA_TO_DFA.pdf","pdf");
			Tester.generateAutomataImage(c.NDFAToDFA(tentamenRegex_regexDFA.regexToNDFA()),imagesFolder+"DFA_FOR_REGEX_TO_DFA.pdf","pdf");
			Tester.generateAutomataImage(tentamenRegex_regexNDFA.regexToNDFA(),imagesFolder+"NDFA_FOR_REGEX_TO_NDFA.pdf","pdf");
			Tester.generateAutomataImage(c.NDFAToDFA(tentamenRegex_DFAregex.regexToNDFA()),imagesFolder+"DFA_FOR_DFA_TO_REGEX.pdf","pdf");
			Tester.generateAutomataImage(tentamenRegex_NDFAregex.regexToNDFA(),imagesFolder+"NDFA_FOR_NDFA_TO_REGEX.pdf","pdf");
			// Add questions to exam string


			tentamenText += exerciseNDFAToDFA();
			tentamenText += exerciseRegexToDFA(tentamenRegex_regexDFA);
			tentamenText += exerciseRegexToNDFA(tentamenRegex_regexNDFA);
			tentamenText += exerciseDFAToRegex();
			tentamenText += exerciseNDFAToRegex();

			// Add answers to exam string
			tentamenText += answersIntro();
			tentamenText += answerNDFAToDFA();
			tentamenText += answerRegexToDFA();
			tentamenText += answerRegexToNDFA();
			tentamenText += answerDFAToRegex(tentamenRegex_DFAregex);
			tentamenText += answerNDFAToRegex(tentamenRegex_NDFAregex);

			// End exam string
			tentamenText += endOfDocument();


			// Writing exam to file
			System.IO.StreamWriter file = new System.IO.StreamWriter(currentExamFolder+"proeftentamen.tex");

			file.WriteLine(tentamenText);

			file.Close();

		}

		private String frontpageLatex()
		{
			String result = "\\documentclass[12pt]{article}\n\n\\pagestyle{empty}\n\\setcounter{secnumdepth}{2}\n%----------------------------------------------------------------------------------------\n%   Packages and configurations\n%----------------------------------------------------------------------------------------\n\\usepackage[dutch]{babel}\n\\usepackage{geometry} % Required to change the page size to A4\n\\geometry{a4paper} % Set the page size to be A4 as opposed to the default US Letter\n\\usepackage{chngpage}\n\\usepackage{fancyhdr} % Required for custom headers\n\\usepackage{extramarks} % Required for headers and footers\n\\usepackage{lastpage} % Required to determine the last page for the footer\n%-----------------1\n\\topmargin=0cm\n\\oddsidemargin=0cm\n\\textheight=22.0cm\n\\textwidth=16cm\n\\parindent=0cm\n\\parskip=0.15cm\n\\topskip=0truecm\n\\raggedbottom\n\\abovedisplayskip=3mm\n\\belowdisplayskip=3mm\n\\abovedisplayshortskip=0mm\n\\belowdisplayshortskip=2mm\n\\normalbaselineskip=12pt\n\\normalbaselines\n\\usepackage{wasysym}\n\\pagestyle{fancy}\n\\lhead{} % Top left header\n\\rhead{Oefententamen} % Top center header\n\\lfoot{Avans Hogeschool Breda} % Bottom left footer\n\\cfoot{} % Bottom center footer\n\\rfoot{Pagina\\ \\thepage} % Bottom right footer\n\\renewcommand\\headrulewidth{0.4pt} % Size of the header rule\n\\renewcommand\\footrulewidth{0.8pt} % Size of the footer rule\n\\usepackage{graphicx} % Required for including pictures\n\\usepackage{pdfpages}\n\\usepackage[font={footnotesize}]{caption}\n\\graphicspath{ {./Images/} } % We can store the generated images in a seperate folder without the hassle\n\\begin{document}\n\\begin{titlepage}\n\\newcommand{\\HRule}{\\rule{\\linewidth}{0.5mm}} % Defines a new command for the horizontal lines, change thickness here\n\n\\center % Center everything on the page\n\nAvans Hogeschool Breda\\\\% Include a department/university logo - this will require the graphicx package\n\\textsc{\\Large Technische Informatica}\\\\[0.5cm] % Major heading such as course name\n\\textsc{\\large Formele Methoden}\\\\[0.5cm] % Minor heading such as course title\n\\HRule \\\\[0.4cm]\n{ \\huge \\bfseries Oefententamen}\\\\[0.4cm] % Title of your document\n\\HRule \\\\[1.5cm]\n\n{\\large \\today}\\\\[3cm] % Date, change the \\today to a set date if you want to be precise\n\n\\vfill % Fill the rest of the page with whitespace\n\n\\end{titlepage}\n\n\\clearpage";
			return result;
		}

		private String answersIntro()
		{
			String result = "\\chapter{Antwoorden}\n\\setcounter{section}{0}";
			return result;
		}

		private String endOfDocument()
		{
			String result = "\\end{document}";
			return result;
		}

		private String exerciseNDFAToDFA()
		{
			//NDFA_FOR_NDFA_TO_DFA
			String result = "\\section{NDFA naar DFA}\nZet de onderstaande NDFA om naar een DFA:\n\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {NDFA_FOR_NDFA_TO_DFA}\n\n\\clearpage";
			return result;
		}

		private String exerciseRegexToDFA(RegularExpression regex)
		{
			String result = "\\section{Reguliere expressie}\nGeef de DFA van de onderstaande reguliere expressie:" +
			    regex.ToString()
				+"\\clearpage";
			return result;
		}

		private String exerciseRegexToNDFA(RegularExpression regex)
		{
			String result = "\\section{Reguliere expressie}\nGeef de NDFA van de onderstaande reguliere expressie:" +
			    regex.ToString()
				+"\\clearpage";
			return result;
		}

		private String exerciseMinimaliseDFA()
		{
			//DFA_FOR_MINIMALISE_DFA
			String result = "\\section{Minimalisatie}\nMinimaliseer de onderstaande DFA:\n\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {DFA_FOR_MINIMALISE_DFA}\n\n\\clearpage";
			return result;
		}

		private String exerciseReverseDFA()
		{
			//DFA_FOR_DFA_TO_INVERSE_DFA
			String result = "\\section{Inverse}\nGeef de inverse (het omgekeerde) van de onderstaande DFA:\n\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {DFA_FOR_DFA_TO_INVERSE_DFA}\n\n\\clearpage";
			return result;
		}

		private String exerciseDFAToGrammar()
		{
			//DFA_FOR_DFA_TO_GRAMMAR
			String result = "\\section{Grammatica}\nGeef de grammatica van de onderstaande DFA:\n\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {DFA_FOR_DFA_TO_GRAMMAR}\n\n\\clearpage";
			return result;
		}

		private String exerciseNDFAToGrammar()
		{
			//NDFA_FOR_NDFA_TO_GRAMMAR
			String result = "\\section{Grammatica}\nGeef de grammatica van de onderstaande NDFA:\n\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {NDFA_FOR_NDFA_TO_GRAMMAR}\n\n\\clearpage";
			return result;
		}

		private String exerciseDFAToRegex()
		{
			//DFA_FOR_DFA_TO_REGEX
			String result = "\\section{Omzetten naar regluliere expressie}\nGeef de regex van de onderstaande DFA:\n\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {DFA_FOR_DFA_TO_REGEX}\n\n\\clearpage";
			return result;
		}

		private String exerciseNDFAToRegex()
		{
			//NDFA_FOR_NDFA_TO_REGEX
			String result = "\\section{Omzetten naar regluliere expressie}\nGeef de regex van de onderstaande NDFA:\n\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {NDFA_FOR_NDFA_TO_REGEX}\n\n\\clearpage";
			return result;
		}

		private String answerNDFAToDFA()
		{
			//DFA_FOR_NDFA_TO_DFA
			String result = "\\section{NDFA naar DFA}\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {DFA_FOR_NDFA_TO_DFA}\n\n\\clearpage";
			return result;
		}

		private String answerRegexToDFA()
		{
			//DFA_FOR_REGEX_TO_DFA
			String result = "\\section{Reguliere expressie}\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {DFA_FOR_REGEX_TO_DFA}\\clearpage";
			return result;
		}

		private String answerRegexToNDFA()
		{
			//DFA_FOR_REGEX_TO_DFA
			String result = "\\section{Reguliere expressie}\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {DFA_FOR_REGEX_TO_DFA}\\clearpage";
			return result;
		}

		private String answerMinimaliseDFA()
		{
			//MINIMALISED_DFA_FOR_MINIMALISE_DFA
			String result = "\\section{Minimalisatie}\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {MINIMALISED_DFA_FOR_MINIMALISE_DFA}\n\n\\clearpage";
			return result;
		}

		private String answerReverseDFA()
		{
			//REVERSED_DFA_FOR_DFA_TO_INVERSE_DFA
			String result = "\\section{Inverse}\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {REVERSED_DFA_FOR_DFA_TO_INVERSE_DFA}\n\n\\clearpage";
			return result;
		}

		private String answerDFAToGrammar()
		{
			//DFA_GRAMMAR_FOR_DFA_TO_GRAMMAR
			String result = "\\section{Grammatica}\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {DFA_GRAMMAR_FOR_DFA_TO_GRAMMAR}\n\n\\clearpage";
			return result;
		}

		private String answerNDFAToGrammar()
		{
			//NDFA_GRAMMAR_FOR_NDFA_TO_GRAMMAR
			String result = "\\section{Grammatica}\n\\includegraphics[width=0.7\\textwidth,height=\\textheight,keepaspectratio] {NDFA_GRAMMAR_FOR_NDFA_TO_GRAMMAR}\n\n\\clearpage";
			return result;
		}

		private String answerDFAToRegex(RegularExpression regex)
		{
			String result = "\\section{Omzetten naar reguliere expressie}"+regex.ToString();
			return result;
		}

		private String answerNDFAToRegex(RegularExpression regex)
		{
			String result = "\\section{Omzetten naar reguliere expressie}"+regex.ToString();
			return result;
		}
	}
}

