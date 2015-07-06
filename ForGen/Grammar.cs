using System;
//using C5;
using System.Collections.Generic;

namespace ForGen
{
	public class Grammar<T> where T : IComparable
	{
		private T startSymbol;
		private List<ProductionRule<T>> productionRules;

		public Grammar (T startSymbol, List<ProductionRule<T>> productionRules)
		{
			this.startSymbol = startSymbol;
			this.productionRules = productionRules;
		}

		private SortedSet<T> helpSymbols()
		{
			SortedSet<T> result = new SortedSet<T>();
			foreach (ProductionRule<T> p in productionRules) 
			{
				result.Add(p.getLeft());
			}
			return result;
		}

		private SortedSet<char> alphabet()
		{
			SortedSet<char> result = new SortedSet<char>();
			foreach (ProductionRule<T> p in productionRules) 
			{
				result.Add(p.getX());
			}
			return result;
		}

		private String printHelpSymbols(SortedSet<T> symbols)
		{
			String result = "";
			foreach (T symb in symbols) 
			{
				result += symb.ToString() + ", ";
			}
			result = result.Remove(result.Length - 2, 1);
			return result;
		}

		private String printAlphabet(SortedSet<char> letters)
		{
			String result = "";
			foreach (char letter in letters) 
			{
				result += letter.ToString() + ", ";
			}
			result = result.Remove(result.Length - 2, 1);
			return result;
		}

		public String toString()
		{
			String description = "";
			description  = "G = {N, S, P, "+ startSymbol.ToString() +" }\n";
			description += "N = "+ printHelpSymbols(helpSymbols()) +" \n";
			description += "S = "+ printAlphabet(alphabet()) + " \n";

			foreach (ProductionRule<T> p in productionRules)
			{
				description += p.toString() + "\n";
			}

			return description;

		}

		public String toBeautifulString()
		{
			String description = "";
			description  = "G = {N, S, P, "+ startSymbol.ToString() +" }\n";
			description += "N = "+ printHelpSymbols(helpSymbols()) +" \n";
			description += "S = "+ printAlphabet(alphabet()) + " \n";

			T tempChecker = default(T);
			foreach (ProductionRule<T> p in productionRules)
			{
				if (tempChecker==null) {
					tempChecker =  p.getLeft();
					description += p.toString();
				}
					
				else if (p.getLeft().CompareTo(tempChecker)!=0) {
					tempChecker =  p.getLeft();
					description += "\n"+ p.toString();
				}
				else
					description += p.toShortString();
			}

			return description += "\n";
		}

		public String toBeautifulLatexString()
		{
			String description = "";
			description  = "G = {N, S, P, "+ startSymbol.ToString() +" }\n\\\\";
			description += "N = "+ printHelpSymbols(helpSymbols()) +" \n\\\\";
			description += "S = "+ printAlphabet(alphabet()) + " \n\\\\";

			T tempChecker = default(T);
			foreach (ProductionRule<T> p in productionRules)
			{
				if (tempChecker==null) {
					tempChecker =  p.getLeft();
					description += p.toString();
				}

				else if (p.getLeft().CompareTo(tempChecker)!=0) {
					tempChecker =  p.getLeft();
					description += "\n\\\\"+ p.toString();
				}
				else
					description += p.toShortString();
			}

			return description += "\n\\\\";
		}
	}
}