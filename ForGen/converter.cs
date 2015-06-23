using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForGen
{
	public class converter
	{
		public converter()
		{
			
		}

		public Automata<String> NDFAToDFA(Automata<String> Automata){
			SortedSet<char> alphabet = Automata.getAlphabet();
			Automata<String> dfa = new Automata<String>(alphabet);
			foreach (String state in Automata.getStates()) {
				foreach (var letter in alphabet) {
					foreach (var item in Automata.getToStates(state, letter)) {
						
						Console.WriteLine("State: " + state + " met Letter: " + letter + " kan ik hier komen: " + item);
					}
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

