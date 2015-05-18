using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForGen
{

    class Transition<T> : IComparable<Transition<T>>
    {
        public static readonly char EPSILON = '$';

        private T fromState {get; set;} 
        private char symbol;
        private T toState;

        public Transition(T fromOrTo, char s): base()
        {
            this.fromState = fromOrTo;
            this.symbol = s;
            this.toState = fromOrTo;
        }
        public Transition(T from, T to): base()
        {
            this.fromState = from;
            this.symbol = EPSILON;
            this.toState = to;
        }
        public Transition(T from, char s, T to): base()
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
            return 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
