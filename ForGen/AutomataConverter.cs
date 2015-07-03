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
			SortedSet<string> startstate = new SortedSet<string>();
			foreach (var newState in findStartState(Automata)) {
				startstate.Add(newState);
			}
			Console.WriteLine(prettyPrint(startstate));
			dfa.defineAsStartState(prettyPrint(startstate));
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

		public SortedSet<String> findStartState(Automata<String> Automata){
			SortedSet<String> foundStates = new SortedSet<String>();
			foreach (var start in Automata.getStartStates()) {
				foundStates.Add(start);
				foreach (var item in Automata.getToStates (start, '$')) {
					if (!foundStates.Contains(item)) {
						foundStates.Add(item);
					}
				}
			}
			return foundStates;
		}


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
						Console.WriteLine("This time using: " + state + " Gives: " + item);
						foundStates.Add(item);
					}
				}
			}
			return foundStates;
		}

        public Automata<String> inverseAutomata(Automata<String> arg0)
        {
            SortedSet<String> tempFinalStates = new SortedSet<string>(arg0.getFinalStates());
            SortedSet<String> tempStartStates = new SortedSet<string>(arg0.getStartStates());
            SortedSet<String> tempStates = new SortedSet<string>(arg0.getStates());
            Automata<String> automata = new Automata<string>(arg0);

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

        public Automata<String> reverseAutomata(Automata<String> arg0)
        {
            Automata<String> reverseAutomata = inverseAutomata(arg0);

            foreach (var transition in arg0.getTransitions())
            {
                transition.reverseFromToState();
            }
            return reverseAutomata;
        }

	}
}

