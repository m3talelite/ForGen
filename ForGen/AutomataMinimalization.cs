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
			return c.renameStates(c.NDFAToDFA(c.reverseAutomata(c.NDFAToDFA(c.reverseAutomata(Automaton)))));
		}
	}
}

