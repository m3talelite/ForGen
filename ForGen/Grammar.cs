using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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