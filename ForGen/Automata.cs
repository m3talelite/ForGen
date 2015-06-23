using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForGen
{
	public class Automata<T> where T : IComparable
	{
		private List<Transition<T>> transitions;
		private SortedSet<T> states;
		private SortedSet<T> startStates;
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

		public void printGraphviz(){
			Console.WriteLine ("digraph finite_state_machine {");
			Console.WriteLine ("rankdir=q;");
			Console.WriteLine ("size=\"8,5\"");
			Console.WriteLine ("node [shape = doublecircle]; LR_0 LR_3 LR_4 LR_8;"); //todo check if endstate
			Console.WriteLine ("node [shape = circle];"); //todo check if normal state
			foreach(Transition<T> t in transitions)
			{
				Console.WriteLine(t);
			}
			Console.WriteLine ("}");
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

    }
}