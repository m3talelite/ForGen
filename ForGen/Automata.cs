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
			transitions = new List<Transition<T>>();
			states = new SortedSet<T>();
			startStates = new SortedSet<T>();
			finalStates = new SortedSet<T>();
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
			// not yet correct:
			SortedSet<T> reachable = new SortedSet<T>();

			return reachable;

		}

		public SortedSet<T> epsilonClosure (SortedSet<T> fromStates)
		{
			SortedSet<T> reachable = new SortedSet<T>();
			SortedSet<T> newFound = new SortedSet<T	>();

			// not yet correct:
			return reachable;
		}

    }
}