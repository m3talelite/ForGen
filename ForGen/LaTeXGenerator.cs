using System;

namespace ForGen
{
	public class LaTeXGenerator
	{
		public LaTeXGenerator()
		{
			
		}

		public void generateLaTeXExam()
		{
			
		}

		private String exerciseNDFAToDFA()
		{
			//NDFA_FOR_NDFA_TO_DFA
			String result = "\\section{NDFA naar DFA}\nZet de onderstaande NDFA om naar een DFA:\n\n\\includegraphics[width=\\textwidth] {NDFA_FOR_NDFA_TO_DFA}\n\n\\clearpage";
			return result;
		}

		private String exerciseRegexToDFA(RegularExpression regex)
		{
			String result = "\\section{Reguliere expressie}\nGeef de DFA van de onderstaande reguliere expressie:" +
			    regex.ToString();
				+"\\clearpage";
			return result;
		}

		private String exerciseRegexToNDFA(RegularExpression regex)
		{
			String result = "\\section{Reguliere expressie}\nGeef de NDFA van de onderstaande reguliere expressie:" +
			    regex.ToString();
				+"\\clearpage";
			return result;
			return result;
		}

		private String exerciseMinimaliseDFA()
		{
			//DFA_FOR_MINIMALISE_DFA
			String result = "\\section{Minimalisatie}\nMinimaliseer de onderstaande DFA:\n\n\\includegraphics[width=\\textwidth] {DFA_FOR_MINIMALISE_DFA}\n\n\\clearpage";
			return result;
		}

		private String exerciseReverseDFA()
		{
			//DFA_FOR_DFA_TO_INVERSE_DFA
			String result = "\\section{Inverse}\nGeef de inverse (het omgekeerde) van de onderstaande DFA:\n\n\\includegraphics[width=\\textwidth] {DFA_FOR_DFA_TO_INVERSE_DFA}\n\n\\clearpage";
			return result;
		}

		private String exerciseDFAToGrammar()
		{
			//DFA_FOR_DFA_TO_GRAMMAR
			String result = "\\section{Grammatica}\nGeef de grammatica van de onderstaande DFA:\n\n\\includegraphics[width=\\textwidth] {DFA_FOR_DFA_TO_GRAMMAR}\n\n\\clearpage";
			return result;
		}

		private String exerciseNDFAToGrammar()
		{
			//NDFA_FOR_NDFA_TO_GRAMMAR
			String result = "\\section{Grammatica}\nGeef de grammatica van de onderstaande NDFA:\n\n\\includegraphics[width=\\textwidth] {NDFA_FOR_NDFA_TO_GRAMMAR}\n\n\\clearpage";
			return result;
		}

		private String exerciseDFAToRegex()
		{
			//DFA_FOR_DFA_TO_REGEX
			String result = "\\section{Omzetten naar regluliere expressie}\nGeef de regex van de onderstaande DFA:\n\n\\includegraphics[width=\\textwidth] {DFA_FOR_DFA_TO_REGEX}\n\n\\clearpage";
			return result;
		}

		private String exerciseNDFAToRegex()
		{
			//NDFA_FOR_NDFA_TO_REGEX
			String result = "\\section{Omzetten naar regluliere expressie}\nGeef de regex van de onderstaande NDFA:\n\n\\includegraphics[width=\\textwidth] {NDFA_FOR_NDFA_TO_REGEX}\n\n\\clearpage";
			return result;
		}

		private String answerNDFAToDFA()
		{
			//DFA_FOR_NDFA_TO_DFA
			String result = "\\section{NDFA naar DFA}\n\\includegraphics[width=\\textwidth] {DFA_FOR_NDFA_TO_DFA}\n\n\\clearpage";
			return result;
		}

		private String answerRegexToDFA()
		{
			//DFA_FOR_REGEX_TO_DFA
			String result = "\\section{Reguliere expressie}\n\\includegraphics[width=\\textwidth] {DFA_FOR_REGEX_TO_DFA}\\clearpage";
			return result;
		}

		private String answerRegexToNDFA()
		{
			//DFA_FOR_REGEX_TO_DFA
			String result = "\\section{Reguliere expressie}\n\\includegraphics[width=\\textwidth] {DFA_FOR_REGEX_TO_DFA}\\clearpage";
			return result;
			return result;
		}

		private String answerMinimaliseDFA()
		{
			//MINIMALISED_DFA_FOR_MINIMALISE_DFA
			String result = "\\section{Minimalisatie}\n\\includegraphics[width=\\textwidth] {MINIMALISED_DFA_FOR_MINIMALISE_DFA}\n\n\\clearpage";
			return result;
		}

		private String answerReverseDFA()
		{
			//REVERSED_DFA_FOR_DFA_TO_INVERSE_DFA
			String result = "\\section{Inverse}\n\\includegraphics[width=\\textwidth] {REVERSED_DFA_FOR_DFA_TO_INVERSE_DFA}\n\n\\clearpage";
			return result;
		}

		private String answerDFAToGrammar()
		{
			//DFA_GRAMMAR_FOR_DFA_TO_GRAMMAR
			String result = "\\section{Grammatica}\n\\includegraphics[width=\\textwidth] {DFA_GRAMMAR_FOR_DFA_TO_GRAMMAR}\n\n\\clearpage";
			return result;
		}

		private String answerNDFAToGrammar()
		{
			//NDFA_GRAMMAR_FOR_NDFA_TO_GRAMMAR
			String result = "\\section{Grammatica}\n\\includegraphics[width=\\textwidth] {NDFA_GRAMMAR_FOR_NDFA_TO_GRAMMAR}\n\n\\clearpage";
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

