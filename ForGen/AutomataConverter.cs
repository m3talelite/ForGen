using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForGen
{
	public class AutomataConverter
	{
		public AutomataConverter()
		{

		}

		public Automata<String> NDFAToDFA(Automata<String> Automata){
			SortedSet<char> alphabet = Automata.getAlphabet();
			Automata<String> dfa = new Automata<String> (alphabet);
			//check first point acces
			foreach (var state in Automata.getStartStates()) {
				dfa.addTransition( new Transition<String> (state, '$', string.Join(",", findAccessible(Automata, '$', state))));
			}
			foreach (var letter in alphabet) {
				
				findAccessible(Automata, letter, state)
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

		//Funny functions
		public string prettyPrint(SortedSet<String> list){
			return string.Join(",", list);
		}
		//For a single state
		public SortedSet<String> findAccessible(Automata<String> Automata, char letter, String state){
			SortedSet<String> foundStates = new SortedSet<String>();
				foreach (var item in Automata.getToStates (state, letter)) {
				if (!foundStates.Contains(item)) {
					foundStates.Add(item);
				}
			}
			return foundStates;
		}
		//For a set of states
		public SortedSet<String> findAccessible(Automata<String> Automata, char letter, SortedSet<String> states){
			SortedSet<String> foundStates = new SortedSet<String>();
			foreach (var state in states) {
				foreach (var item in Automata.getToStates (state, letter)) {
					if (!foundStates.Contains(item)) {
						foundStates.Add(item);
					}
				}
			}
			return foundStates;
		}

	}
}

