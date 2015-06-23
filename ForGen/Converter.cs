using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForGen
{
	public class Converter
	{
		public Converter(Automata<String> Automata)
		{
			if(Automata.isDFA()){
				DFAToNDFA(Automata);
			} else{
				NDFAToDFA(Automata);
			}
		}

		public Automata<String> NDFAToDFA(Automata<String> Automata){
			SortedSet<char> alphabet = Automata.getAlphabet();
			Automata<String> dfa = new Automata<String>(alphabet);
			foreach (String state in Automata.getStates()) {
				foreach (var letter in alphabet) {
					Automata.getToStates(state, letter);
				}
			}
			return dfa;
		}

		public Automata<String> DFAToNDFA(Automata<String> Automata){
			SortedSet<char> alphabet = Automata.getAlphabet();
			Automata<String> ndfa = new Automata<String>(alphabet);
			foreach (var state in Automata.getStates()) {
				
			}
			return ndfa;
		}
	}
}

