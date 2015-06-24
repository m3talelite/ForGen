using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ForGen
{
	public class Automata<T> where T : IComparable
	{
		private List<Transition<T>> transitions;
		private SortedSet<T> states;
		private SortedSet<T> startStates; //WARNING! THIS SHOULD NEVER EVER BE A SORTEDSET!!! THIS CAN ONLY BE ONE STATE ALTHOUGH EPSION TRANSITIONS MAKE IT ACT LIKE IT HAS MULTIPLE ENTRIES
		private SortedSet<T> finalStates;
		private SortedSet<char> symbols;

		public Automata()
		{
			this.transitions = new SortedSet<Transition<T>>().ToList();
			this.states = new SortedSet<T>();
			this.startStates = new SortedSet<T>();
			this.finalStates = new SortedSet<T>();
			this.setAlphabet(symbols);
		}
		public Automata(Automata<T> autom)
		{
			this.transitions = autom.transitions;
			this.states = autom.states;
			this.startStates = autom.startStates;
			this.finalStates = autom.finalStates;
			this.setAlphabet(autom.getAlphabet());
		}
		public Automata(char [] s): this(new SortedSet<char>(s))
		{   
		}

		public Automata(SortedSet<char> symbols)
		{
			this.transitions = new List<Transition<T>>();
			this.states = new SortedSet<T>();
			this.startStates = new SortedSet<T>();
			this.finalStates = new SortedSet<T>();
			this.setAlphabet(symbols);
		}

		public void setAlphabet(char [] s){
			this.setAlphabet(new SortedSet<char>((s)));
		}

		public void setAlphabet(SortedSet<char> symbols){
			this.symbols = symbols;
		}

		public SortedSet<char> getAlphabet(){
			return symbols;
		}

		public void addTransition(Transition<T> t){
			transitions.Add(t);
			states.Add(t.getFromState());
			states.Add(t.getToState());        
		}

		public void defineAsStartState(T t)
		{
			// if already in states no problem because a Set will remove duplicates.
			states.Add(t);
			startStates.Add(t);        
		}

		public SortedSet<T> getStates(){
			return states;
		}

		public SortedSet<T> getStartStates(){
			return startStates;
		}

		public SortedSet<T> getFinalStates(){
			return finalStates;
		}

		public List<Transition<T>> getTransitions(){
			return transitions;
		}

		public int getTransistionsNumber(){
			return transitions.Count();       
		}

		public void defineAsFinalState(T t)
		{
			// if already in states no problem because a Set will remove duplicates.
			states.Add(t);
			finalStates.Add(t);        
		}

		public void printTransitions()
		{

			foreach(Transition<T> t in transitions)
			{
				Console.WriteLine(t);
			}
		}

		public string returnGrammar() {

			string result = "G = {N, S, P, "+ startStates.First() +"}\n";
			result = result + "N = "+ statesToString() + "\n";
			result = result + "S = "+ alphabetToString() + "\n";
			result = result + "P:\n";
			foreach (T state in this.getStates()) {
				result = result + state + " -> ";
				bool f = true;
				foreach (var letter in getAlphabet()) {
					foreach (var item in getToStates(state, letter)) {
						if (f) {
							result = result + letter + item;
							f = false;
						}
						else
							result = result + " | " +  letter + item;
					}
				}
				result = result + "\n";
			}
			return result;
		}

		public string printGraphviz(){
			string graphviz = "digraph finite_state_machine {";
			graphviz = graphviz + "rankdir=q;";
			graphviz = graphviz + "size=\"8,5\"";

			graphviz = graphviz + "node [shape = doublecircle]; ";
			foreach (T t in finalStates) {
				graphviz = graphviz + t + ' ';
			}
			graphviz = graphviz +";\n"; //todo check if endstate
			graphviz = graphviz +"node [shape = circle];"; //todo check if normal state
			foreach(Transition<T> t in transitions)
			{
				graphviz = graphviz +t;
			}
			graphviz = graphviz +"node [shape = point ]; qi\nqi -> ";
			graphviz = graphviz +startStates.First();
			graphviz = graphviz +" ;\n";
			graphviz = graphviz +"}";
			return graphviz;
		}

		public bool isDFA()
		{
			bool isDFA = true;

			foreach(T from in states)
			{
				foreach(char symbol in symbols)
				{
					isDFA = isDFA && getToStates(from, symbol).Count() == 1;
				}
			}

			return isDFA;
		}

		public SortedSet<T> getToStates(T from, char symbol)
		{
			SortedSet<T> fromStates = new SortedSet<T>();
			fromStates.Add(from);
			fromStates = epsilonClosure(fromStates);      

			SortedSet<T> toStates = new SortedSet<T>();

			foreach(T fromState in fromStates)
			{
				foreach(Transition<T> t in transitions)
				{
					if (t.getFromState().Equals(fromState) && t.getSymbol() == symbol)
					{
						toStates.Add(t.getToState());
					}
				}
			}

			return epsilonClosure(toStates);
		}

		public SortedSet<T> epsilonClosure (SortedSet<T> fromStates)
		{
			SortedSet<T> reachable = new SortedSet<T>();
			SortedSet<T> newFound = new SortedSet<T>();

			do {
				foreach (var item in fromStates) {
					reachable.Add(item);
				}
				newFound = new SortedSet<T>();
				foreach(T fromState in fromStates){
					foreach(Transition<T> t in transitions)
					{
						T toState  = t.getToState();
						if (t.getFromState().Equals(fromState) && t.getSymbol() == Transition<T>.EPSILON && !fromStates.Contains(toState))
						{
							newFound.Add(toState);
						}
					}
				}
				foreach (var item in newFound) {
					fromStates.Add(item);
				}
				reachable = newFound;
			} 
			while (newFound.Count() != 0);

			return fromStates;
		}

		public String statesToString()
		{
			string allStatesString = ""; //String that includes all the states of the automaton
			foreach (T item in states) {
				allStatesString += item + ", ";
			}
			allStatesString = allStatesString.Remove(allStatesString.Length - 2, 1); //To remove the , and space after the last item, I think this is the solution that requires the least machine cycles
			return allStatesString;
		}

		public String alphabetToString()
		{
			string alphabetString = ""; //String that includes all the letters of the used alphabet
			foreach (char letter in symbols) {
				alphabetString += letter + ", ";
			}
			alphabetString = alphabetString.Remove(alphabetString.Length - 2, 1); //To remove the , and space after the last item, I think this is the solution that requires the least machine cycles
			return alphabetString;
		}
	}
}