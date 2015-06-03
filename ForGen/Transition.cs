using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForGen
{

	public class Transition<T> where T : IComparable    
	{
        public static readonly char EPSILON = '$';

        private Transition<T> fromState {get; set;}
        private char symbol {get; set;}
        private Transition<T> toState {get; set;}

        public Transition(Transition<T> fromOrTo, char s)
            : base()
        {
            this.fromState = fromOrTo;
            this.symbol = s;
            this.toState = fromOrTo;
        }
        public Transition(Transition<T> from, Transition<T> to)
            : base()
        {
            this.fromState = from;
            this.symbol = EPSILON;
            this.toState = to;
        }
        public Transition(Transition<T> from, char s, Transition<T> to)
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
            return "(" + this.fromState + ", " + this.symbol + ") -->" + this.toState;
        }
    }
}
