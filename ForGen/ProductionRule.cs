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