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
			foreach (var letter in Automata.getAlphabet()) {
				foreach (var state in Automata.getStartStates()) {
					dfa.addTransition( new Transition<String> (state, letter, string.Join(",", findAccessible(Automata, letter, state))));
				}
				Console.WriteLine(string.Join(",",findAccessible(Automata, 'c', findAccessible(Automata, letter, findAccessible(Automata, letter, "0")))));
			}
			//WELKE HACKER FIXT DIT ?
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
		public SortedSet<String> findAccessible(Automata<String> Automata, char letter, String state){
			SortedSet<String> foundStates = new SortedSet<String>();
				foreach (var item in Automata.getToStates (state, letter)) {
				if (!foundStates.Contains(item)) {
					foundStates.Add(item);
				}
			}
			return foundStates;
		}

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

        public Automata<String> inverseAutomata(Automata<String> Automata)
        {
            SortedSet<String> tempFinalStates = new SortedSet<string>(Automata.getFinalStates());
            SortedSet<String> tempStartStates = new SortedSet<string>(Automata.getStartStates());
            SortedSet<String> tempStates = new SortedSet<string>(Automata.getStates());
            Automata<String> automata = new Automata<string>(Automata);

            automata.getFinalStates().Clear();
            automata.getStartStates().Clear();
            automata.getStates().Clear();

            foreach (var stateStart in tempStartStates)
            {
                automata.defineAsStartState(stateStart);
                automata.defineAsFinalState(stateStart);
            }
            foreach (var stateFinal in tempFinalStates)
            {
                foreach (var state in tempStates)
                {
                    if (state != stateFinal)
                    {
                        automata.defineAsFinalState(state);
                    }
                }
            }
            return automata;
        }

	}
}

