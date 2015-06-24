using System;

namespace ForGen
{
	public class ProductionRule<T>
	{

		private T left;
		private char x;
		private T right;

		public ProductionRule (T l, char x, T r)
		{
			this.left = l;
			this.x = x;
			this.right = r;
		}


		public String toString()
		{
			return "" + left + " --> " + x + right;
		}
	}
}