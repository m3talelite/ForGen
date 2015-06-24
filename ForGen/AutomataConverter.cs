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
			Dictionary<SortedSet<String>, char> dictionary = new Dictionary<SortedSet<String>, char>();
			//check first point acces
			SortedSet<String> startstate = new SortedSet<string>();
			foreach (var state in Automata.getStartStates()) {
				startstate.Add(state);
				foreach (var newState in findSingleAccessible(Automata, '$', state)) {
					startstate.Add(newState);
				}
				dfa.defineAsStartState(prettyPrint(startstate));
			}
			//From here start checking rest
			foreach (var letter in alphabet) {
				findMultipleAccessible(Automata, letter, startstate);
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
		public SortedSet<String> findSingleAccessible(Automata<String> Automata, char letter, String state){
			SortedSet<String> foundStates = new SortedSet<String>();
				foreach (var item in Automata.getToStates (state, letter)) {
				if (!foundStates.Contains(item)) {
					foundStates.Add(item);
				}
			}
			return foundStates;
		}
		//For a set of states
		public SortedSet<String> findMultipleAccessible(Automata<String> Automata, char letter, SortedSet<String> states){
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

