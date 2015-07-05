using System;

namespace ForGen
{
	public class AutomataMinimalization
	{
		public AutomataMinimalization()
		{
		}

		public Automata<String> Minimization(Automata<String> Automaton)
		{
			AutomataConverter c = new AutomataConverter();
	
			Automata<String> miniantwoord = c.NDFAToDFA(c.reverseAutomata(c.NDFAToDFA(c.reverseAutomata(Automaton))));

			return c.renameStates(miniantwoord);
		}
	}
}

