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

		public T getLeft()
		{
			return left;
		}

		public char getX()
		{
			return x;
		}

		public T getRight()
		{
			return right;
		}

		public String toString()
		{
			// looks something like this: "0 --> a6"
			return "" + left + " --> " + x + right;
		}

		public String toShortString()
		{
			return " | " + x + right;
		}
	}
}