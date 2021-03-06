﻿/*
* Copyright 2015 Guus Beckett, Joris Mathijssen, Jelle Braat, Jim van Abkoude
*
* Licensed under the EUPL, Version 1.1 or – as soon they will be approved by the European Commission - subsequent versions of the EUPL (the "Licence");
* You may not use this work except in compliance with the Licence.
* You may obtain a copy of the Licence at:
*
* http://ec.europa.eu/idabc/eupl5
*
* Unless required by applicable law or agreed to in writing, software distributed under the Licence is distributed on an "AS IS" basis,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the Licence for the specific language governing permissions and limitations under the Licence.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using C5;

namespace ForGen
{
    [Serializable]
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
			this.setAlphabet(new SortedSet<char>());
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



		public bool isStringAccepted(String testString)
		{
			bool result = false;
			SortedSet<T> currentStates = new SortedSet<T>();
			SortedSet<T> tempCurrentStates = new SortedSet<T>();
			currentStates.Add(startStates.First());
			foreach (char character in testString) {
				tempCurrentStates.Clear();
				foreach (T state in currentStates) {
					tempCurrentStates.UnionWith(getToStates(state, character));
				}
				currentStates.Clear();
				foreach (T state in tempCurrentStates) {
					currentStates.Add(state);
				}
			}
			foreach(T state in currentStates)
			{
				if (getFinalStates().Contains(state))
					result = true;
			}
			return result;
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

		public void addToAlphabet(char Char) {
			symbols.Add(Char);
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

		public Grammar<T> getGrammar() {
			List<ProductionRule<T>> productionRules = new List<ProductionRule<T>>();
			T startSymbol = startStates.First();
			foreach (T state in this.getStates()) {
				foreach (var letter in getAlphabet()) {
					foreach (var item in getToStates(state, letter)) {
						ProductionRule<T> productRule = new ProductionRule<T>(state, char.ToUpper(letter), item);
						productionRules.Add(productRule);
					}
				}
			}
			Grammar<T> result = new Grammar<T>(startSymbol, productionRules);
				return result;
		}

		public string printGraphviz(){
			string graphviz = "digraph finite_state_machine {";
			graphviz = graphviz + "rankdir=q;";
			graphviz = graphviz + "size=\"12,5\"";

			graphviz = graphviz + "node [shape = doublecircle]; ";
			foreach (T t in finalStates) {
				graphviz = graphviz + '"' + t + '"' + ' ';
			}
			if (finalStates.Count>0)
				graphviz = graphviz +";\n"; //todo check if endstate
			graphviz = graphviz +"node [shape = circle];"; //todo check if normal state
			foreach(Transition<T> t in transitions)
			{
				graphviz = graphviz +t;
			}
			if (startStates.Count > 0) {
				graphviz = graphviz + "node [shape = point ]; qi\nqi -> ";
				graphviz = graphviz +  '"' + startStates.First() + '"';
				graphviz = graphviz + " ;\n";
			}
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


	}
}