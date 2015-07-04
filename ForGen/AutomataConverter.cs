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
			return dfa;
		}

		//Function to print the new name of a state
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

        public Automata<String> inverseAutomata(Automata<String> automaton)
        {
			Automata<String> invertedAutomaton = new Automata<string>(automaton.getAlphabet());

			foreach (String state in automaton.getFinalStates()) {
				invertedAutomaton.defineAsStartState(state);
			}

			foreach (Transition<String> transition in automaton.getTransitions()) {
				invertedAutomaton.addTransition(transition);
			}

			foreach(String state in automaton.getStates()) {
				if (!automaton.getFinalStates().Contains(state)) {
					invertedAutomaton.defineAsFinalState(state);
				}
			}

			foreach(String state in automaton.getFinalStates())
			{
				if (state != invertedAutomaton.getStartStates().First())
					invertedAutomaton.addTransition(new Transition<string>(invertedAutomaton.getStartStates().First(),state));
			}


            return invertedAutomaton;
        }

//		public Automata<String> reverseAutomata(Automata<String> automaton)
//		{
//			Automata<String> reversedAutomaton = new Automata<String>(automaton.getAlphabet());
//
//			foreach (Transition<String> transition in automaton.getTransitions())
//			{
//				transition.reverseFromToState();
//				reversedAutomaton.addTransition(transition);
//			}
//
//			foreach (String state in automaton.getStartStates()) {
//				reversedAutomaton.defineAsStartState(state);
//			}
//
//			foreach (String state in automaton.getFinalStates()) {
//				reversedAutomaton.defineAsFinalState(state);
//			}
//			reversedAutomaton.printTransitions();
//
//			reversedAutomaton = inverseAutomata(reversedAutomaton);
//
//			reversedAutomaton.printTransitions();
//			return reversedAutomaton;
//		}

		public Automata<String> reverseAutomata(Automata<String> automaton)
		{
			Automata<String> reversedAutomaton = new Automata<string>(automaton.getAlphabet());

			foreach (Transition<String> transition in automaton.getTransitions()) {
				transition.reverseFromToState();
				reversedAutomaton.addTransition(transition);
			}

			foreach (String state in automaton.getFinalStates()) {
				reversedAutomaton.defineAsStartState(state);
			}

			foreach (String state in automaton.getStartStates()) {
				reversedAutomaton.defineAsFinalState(state);
			}

			foreach (String state in automaton.getFinalStates()) {
				reversedAutomaton.addTransition(new Transition<string>(reversedAutomaton.getStartStates().First(), state));
			}

			return reversedAutomaton;
		}

	}
}

