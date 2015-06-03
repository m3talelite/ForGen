using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForGen
{
    class Program
    {
        static void Main(string[] args)
        {

			Console.WriteLine("Programme started on "+DateTime.Now.ToString("H:mm:ss"));
            testRegularExpression();
            Console.ReadLine();
			Console.WriteLine("Programme successfully stopped on "+DateTime.Now.ToString("H:mm:ss"));

        }

//		static public Automata<String> TestProcedure1() {
//			Character [] alphabet = {'a', 'b'};
//			Automata<String> m = new Automata<String>(alphabet);
//			m.addTransition( new Transition<String> ("q0", 'a', "q1") );
//			m.addTransition( new Transition<String> ("q0", 'b', "q4") );
//
//			m.addTransition( new Transition<String> ("q1", 'a', "q4") );
//			m.addTransition( new Transition<String> ("q1", 'b', "q2") );
//
//			m.addTransition( new Transition<String> ("q2", 'a', "q3") );
//			m.addTransition( new Transition<String> ("q2", 'b', "q4") );
//
//			m.addTransition( new Transition<String> ("q3", 'a', "q1") );
//			m.addTransition( new Transition<String> ("q3", 'b', "q2") );
//
//			// the error state, loops for a and b:
//			m.addTransition( new Transition<String> ("q4", 'a') );
//			m.addTransition( new Transition<String> ("q4", 'b') );
//
//			// only on start state in a dfa:
//			m.defineAsStartState("q0");
//
//			// two final states:
//			m.defineAsFinalState("q2");
//			m.defineAsFinalState("q3");
//
//			return m;
//		}
//
//		static public Automata<String> TestProcedure2() {
//			Character [] alphabet = {'a', 'b'};
//			Automata<String> m = new Automata<String>(alphabet);
//
//			m.addTransition( new Transition<String> ("A", 'a', "C") );
//			m.addTransition( new Transition<String> ("A", 'b', "B") );
//			m.addTransition( new Transition<String> ("A", 'b', "C") );
//
//			m.addTransition( new Transition<String> ("B", 'b', "C") );
//			m.addTransition( new Transition<String> ("B", "C") );
//
//			m.addTransition( new Transition<String> ("C", 'a', "D") );
//			m.addTransition( new Transition<String> ("C", 'a', "E") );
//			m.addTransition( new Transition<String> ("C", 'b', "D") );
//
//			m.addTransition( new Transition<String> ("D", 'a', "B") );
//			m.addTransition( new Transition<String> ("D", 'a', "C") );
//
//			m.addTransition( new Transition<String> ("E", 'a') );
//			m.addTransition( new Transition<String> ("E", "D") );
//
//			// only on start state in a dfa:
//			m.defineAsStartState("A");
//
//			// two final states:
//			m.defineAsFinalState("C");
//			m.defineAsFinalState("E");
//
//			return m;
//		}

        public static void testRegularExpression()
        {
            RegularExpression exp1, exp2, exp3, exp4, exp5, a, b, all;

            a = new RegularExpression("a");
            b = new RegularExpression("b");
            // exp 1 baa
            exp1 = new RegularExpression("baa");
            // exp 2 bb
            exp2 = new RegularExpression("bb");
            // exp 3 baa | bb
            exp3 = exp1.or(exp2);
            //all = (a|b)*
            all = (a.or(b)).star();
            // exp 4 (baa | bb)+
            exp4 = exp3.plus();
            // exp 4 (baa | bb)+ (a|b)*
            exp5 = exp4.dot(all);

            #region PRINTREGULAREXPRESSION

            Console.WriteLine("taal van (baa):");
            foreach (String o in exp1.getLanguage(5))
            {
                Console.Write(o + " ");
            }
            Console.WriteLine("");

            Console.WriteLine("taal van (bb):");
            foreach (String o in exp2.getLanguage(5))
            {
                Console.Write(o + " ");
            }
            Console.WriteLine("");

            Console.WriteLine("taal van (baa | bb):");
            foreach (String o in exp3.getLanguage(5))
            {
                Console.Write(o + " ");
            }
            Console.WriteLine("");

            Console.WriteLine("taal van (a|b)*:");
            foreach (String o in all.getLanguage(5))
            {
                Console.Write(o + " ");
            }
            Console.WriteLine("");

            Console.WriteLine("taal van (baa | bb)+:");
            foreach (String o in exp5.getLanguage(5))
            {
                Console.Write(o + " ");
            }
            Console.WriteLine("");

            Console.WriteLine("taal van (baa | bb)+ (a|b)*:");
            foreach (String o in exp5.getLanguage(6))
            {
                Console.Write(o + " ");
            }
            Console.WriteLine("");

            #endregion
        }
    }
}
