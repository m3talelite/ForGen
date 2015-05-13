using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForGen
{
	class Automata
    {
        public Automata()
        {

        }

        public Automata(char[] s)
        {

        }

		public Automata(SortedSet<char> set)
        {

        }

    }
}


//public class Automata<T extends Comparable>
//{

//    // Or use a Map structure
//    private Set<Transition <T>> transitions;

//    private SortedSet<T> states;
//    private SortedSet<T> startStates;
//    private SortedSet<T> finalStates;
//    private SortedSet<Character> symbols;

//    public Automata()
//    {
//           this(new TreeSet<Character>());
//    }
    
//    public Automata(Character [] s)
//    {   
//        this(new TreeSet<Character>(Arrays.asList(s)) );
//    }

//    public Automata(SortedSet<Character> symbols)
//    {
//        transitions = new TreeSet<Transition<T>>();
//        states = new TreeSet<T>();
//        startStates = new TreeSet<T>();
//        finalStates = new TreeSet<T>();
//        this.setAlphabet(symbols);
//    }
    
//    public void setAlphabet(Character [] s)
//    {
//        this.setAlphabet(new TreeSet<Character>(Arrays.asList(s)));
//    }
    
//    public void setAlphabet(SortedSet<Character> symbols)
//    {
//       this.symbols = symbols;
//    }
    
//    public SortedSet<Character> getAlphabet()
//    {
//       return symbols;
//    }
    
//    public void addTransition(Transition<T> t)
//    {
//        transitions.add(t);
//        states.add(t.getFromState());
//        states.add(t.getToState());        
//    }
    
//    public void defineAsStartState(T t)
//    {
//        // if already in states no problem because a Set will remove duplicates.
//        states.add(t);
//        startStates.add(t);        
//    }

//    public void defineAsFinalState(T t)
//    {
//        // if already in states no problem because a Set will remove duplicates.
//        states.add(t);
//        finalStates.add(t);        
//    }

//    public void printTransitions()
//    {

//        for (Transition<T> t : transitions)
//        {
//            System.out.println (t);
//        }
//    }
    
//    public boolean isDFA()
//    {
//        boolean isDFA = true;
        
//        for (T from : states)
//        {
//            for (char symbol : symbols)
//            {
//                isDFA = isDFA && getToStates(from, symbol).size() == 1;
//            }
//        }
        
//        return isDFA;
//    }
  
//    public SortedSet<T> getToStates(T from, char symbol)
//    {
//        // not yet correct:
//       SortedSet<T> reachable = new TreeSet<T>();
       
//       return reachable;
        
//    }
    
//    public SortedSet<T> epsilonClosure (SortedSet<T> fromStates)
//    {
//       SortedSet<T> reachable = new TreeSet<>();
//       SortedSet<T> newFound = new TreeSet<>();
       
//        // not yet correct:
//       return reachable;
//    }
// /*
//    public SortedSet<T> getToStates(T from, char symbol)
//    {
//        SortedSet<T> fromStates = new TreeSet<T>();
//        fromStates.add(from);
//        fromStates = epsilonClosure(fromStates);      
        
//        SortedSet<T> toStates = new TreeSet<T>();

//        for (T fromState : fromStates)
//        {
//            for (Transition<T> t : transitions)
//            {
//                if (t.getFromState().equals(fromState) && t.getSymbol() == symbol)
//                    {
//                        toStates.add(t.getToState());
//                    }
//            }
//        }

//        return epsilonClosure(toStates);
//    }
    
    
//    public SortedSet<T> epsilonClosure (SortedSet<T> fromStates)
//    {
//       SortedSet<T> reachable = new TreeSet<>();
//       SortedSet<T> newFound = new TreeSet<>();
       
//       do {
//             reachable.addAll(fromStates);
            
//             newFound = new TreeSet<T>();
//             for (T fromState : fromStates){
//                 for (Transition<T> t : transitions)
//                    {
//                        T toState  = t.getToState();
//                        if (t.getFromState().equals(fromState) && t.getSymbol() == Transition.EPSILON && !fromStates.contains(toState))
//                            {
//                                newFound.add(toState);
//                            }
//                    }
//             }
             
//             fromStates.addAll(newFound);
//             reachable = newFound;
//        } 
//        while (! newFound.isEmpty());
       
//        return fromStates;
//    }
//    */
    
   
//}
