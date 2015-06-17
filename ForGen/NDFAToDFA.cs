using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForGen
{
	public class NDFAToDFA
	{
		public NDFAToDFA(Automata<String> NDFA)
		{
			converter (NDFA);
		}

		public Automata<String> converter(Automata<String> NDFA)
		{
			if (!NDFA.isDFA ()) {
				char [] alphabet = {'a', 'b'};
				Automata<String> dfa = new Automata<String>(alphabet);
				return dfa;
			} else {
				//NDFA is a DFA so return that!
				return NDFA;
			}
		}
	}
}

