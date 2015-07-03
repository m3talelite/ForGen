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

//		public Automata<String> NDFAToDFA(Automata<String> Automata){
//			SortedSet<char> alphabet = Automata.getAlphabet();
//			Automata<String> dfa = new Automata<String> (alphabet);
//			//check first point access
//			SortedSet<string> startstate = new SortedSet<string>();
//			foreach (var newState in findStartState(Automata)) {
//				startstate.Add(newState);
//			}
//			Console.WriteLine(prettyPrint(startstate));
//			dfa.defineAsStartState(prettyPrint(startstate));
//			return dfa;
//		}

		public Automata<String> NDFAToDFA(Automata<String> Automaton)
		{
			SortedSet<char> alphabet = Automaton.getAlphabet();
			Automata<String> dfa = new Automata<String>(alphabet);
			Dictionary<Transition<String>,SortedSet<String>> transitionsDict = new Dictionary < Transition<String>,SortedSet<String>>();
			dfa.defineAsStartState(prettyPrint(findStartState(Automaton))); //Set startstates for dfa
			foreach(char letter in alphabet) //Check all states after the startstate with all the letters in the alphabet
			{
				if (findMultipleAccessible(Automaton, letter, findStartState(Automaton)).Count() == 0)
					dfa.addTransition(new Transition<string>(prettyPrint(findStartState(Automaton)), letter, "Ø"));
				else 
				{
					SortedSet<String> toStates = findMultipleAccessible(Automaton, letter, findStartState(Automaton));
					Transition<String> newTransition = new Transition<string>(prettyPrint(findStartState(Automaton)), letter, prettyPrint(toStates));
					transitionsDict.Add(newTransition, toStates);
					dfa.addTransition(newTransition);
				}
			}

			Console.WriteLine(dfa.getTransistionsNumber());
			int tempTransitionsNumber = 0;
			Dictionary<Transition<String>,SortedSet<String>> tempTransitionsDict = new Dictionary < Transition<String>,SortedSet<String>>();
			while (dfa.getTransistionsNumber()!= tempTransitionsNumber) {
				tempTransitionsNumber = dfa.getTransistionsNumber();
				foreach (KeyValuePair<Transition<String>,SortedSet<String>> pair in transitionsDict) {
					foreach (char letter in alphabet) {
						if (findMultipleAccessible(Automaton, letter, pair.Value).Count() == 0) {
							if (dfa.getTransitions().Contains(new Transition<string>(pair.Key.getToState(), letter, "Ø")) == false) {
								dfa.addTransition(new Transition<string>(pair.Key.getToState(), letter, "Ø"));
								tempTransitionsDict.Add(new Transition<string>(pair.Key.getToState(), letter, "Ø"),findMultipleAccessible(Automaton, letter, pair.Value));
							}
						} else {
							SortedSet<String> toStates = findMultipleAccessible(Automaton, letter, pair.Value);
							Transition<String> newTransition = new Transition<string>(pair.Key.getToState(), letter, prettyPrint(toStates));
							if (dfa.getTransitions().Contains(new Transition<string>(pair.Key.getToState(), letter, prettyPrint(toStates))) == false) {
								tempTransitionsDict.Add(newTransition, toStates);
								dfa.addTransition(newTransition);
						
							}
						}
					}
				}
				transitionsDict = transitionsDict.Union(tempTransitionsDict).ToDictionary(k => k.Key, v => v.Value);
			}
			Console.WriteLine(dfa.getTransistionsNumber());
			dfa.printTransitions();
			SortedSet<String> tempListFinal = new SortedSet<String>();
			foreach (String stat in dfa.getStates()) {
				foreach (String endState in Automaton.getFinalStates()) {
					if (stat.Contains(endState))
						tempListFinal.Add(stat);
				}
			}
			foreach (String finState in tempListFinal) {
				dfa.defineAsFinalState(finState);
			}
			Console.WriteLine(prettyPrint(dfa.getFinalStates()));
			return dfa;
		}

		//Funny functions
		public string prettyPrint(SortedSet<String> list){
			return string.Join(",", list);
		}
		public string prottyPrint(SortedSet<String> list){
			return string.Join(".", list);
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
						Console.WriteLine("Using letter: " + letter +  " Gives: " + item);
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

