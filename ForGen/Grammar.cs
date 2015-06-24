using System;
using C5;

namespace ForGen
{
	public class Grammar<T> where T : IComparable
	{
		private T startSymbol;
		private ArrayList<ProductionRule<T>> productionRules;

		public Grammar (T startSymbol, ArrayList<ProductionRule<T>> productionRules)
		{
			this.startSymbol = startSymbol;
			this.productionRules = productionRules;
		}

		public String toString()
		{
			String description = "";

			foreach (ProductionRule<T> p in productionRules)
			{
				description += p + "\n";
			}

			return description;

		}
	}
}