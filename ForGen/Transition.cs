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

namespace ForGen
{
    [Serializable]
	public class Transition<T> where T : IComparable    
	{
        public static readonly char EPSILON = '$';

		public T fromState  {
			get;
			private set;
		}

		public char symbol  {
			get;
			private set;
		}

        public T toState    {
            get;
            private set;
        }

        public Transition(T fromOrTo, char s)
            : base()
        {
            this.fromState = fromOrTo;
            this.symbol = s;
            this.toState = fromOrTo;
        }
        public Transition(T from, T to)
            : base()
        {
            this.fromState = from;
            this.symbol = EPSILON;
            this.toState = to;
			if (to.ToString() == "6")
				return;
        }
        public Transition(T from, char s, T to)
            : base()
        {
            this.fromState = from;
            this.symbol = s;
            this.toState = to;
        }

        public override bool Equals(Object arg)
        {
            if (arg == null)
                return false;
            if (arg is Transition<T>)
            {
                bool fromStateEqual = this.fromState.Equals(((Transition<T>)arg).fromState);// &&  //&& 
                bool toStateEqual = this.toState.Equals(((Transition<T>)arg).toState);
                bool symbolEqual = this.symbol == (((Transition<T>)arg).symbol);
                if (fromStateEqual && toStateEqual && symbolEqual)
                    return true;
            }
            return false;       
        }

		public T getToState()
		{
			return this.toState;
		}

		public T getFromState()
		{
			return this.fromState;
		}

        public void reverseFromToState()
        {
            var tempFrom = this.fromState;
            var tempTo = this.toState;

            this.fromState = tempTo;
            this.toState = tempFrom;
        }
        public int CompareTo(Transition<T> arg0)
        {
            int fromCompare = fromState.CompareTo(arg0.fromState);
            int symbolCompare = symbol.CompareTo(arg0.symbol);
            int toCompare = toState.CompareTo(arg0.toState);

            return (fromCompare != 0 ? fromCompare : (symbolCompare != 0 ? symbolCompare : toCompare));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
			return "\"" + this.fromState + "\"" + " -> " + "\"" + this.toState + "\"" + "[ label = \"" + this.symbol + "\"];";
        }

		public char getSymbol()
		{
			return symbol;
		}

    }
}
