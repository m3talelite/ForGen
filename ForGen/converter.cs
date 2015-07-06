/*
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

namespace ForGen
{
	public class converter
	{
		public converter()
		{
			
		}

		public Automata<String> NDFAToDFA(Automata<String> Automata){
			SortedSet<char> alphabet = Automata.getAlphabet();
			Automata<String> dfa = new Automata<String>(alphabet);
			foreach (String state in Automata.getStates()) {
				foreach (var letter in alphabet) {
					foreach (var item in Automata.getToStates(state, letter)) {
						Console.WriteLine("State: " + state + " met Letter: " + letter + " kan ik hier komen: " + item);
					}
				}
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
	}
}

