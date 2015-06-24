using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForGen
{
    enum Operator{
        PLUS,
        STAR,
        OR,
        DOT,
        ONE
    }


    class RegularExpression
    {
        Operator operate;
        string terminal;

        RegularExpression left;
        RegularExpression right;

        public RegularExpression()
        {
            this.operate = Operator.ONE;
            terminal = "";
            left = null;
            right = null;
        }

        public RegularExpression(string s)
        {
            this.operate = Operator.ONE;
            terminal = s;
            left = null;
            right = null;
        }
        public RegularExpression plus()
        {
            RegularExpression result = new RegularExpression();
            result.operate = Operator.PLUS;
            result.left = this;
            return result;
        }
        public RegularExpression star()
        {
            RegularExpression result = new RegularExpression();
            result.operate = Operator.STAR;
            result.left = this;
            return result;
        }
        public RegularExpression or(RegularExpression regExp)
        {
            RegularExpression result = new RegularExpression();
            result.operate = Operator.OR;
            result.left = this;
            result.right = regExp;
            return result;
        }
        public RegularExpression dot(RegularExpression regExp)
        {
            RegularExpression result = new RegularExpression();
            result.operate = Operator.DOT;
            result.left = this;
            result.right = regExp;
            return result;
        }

        public SortedSet<string> getLanguage(int maximumSteps)
        {
            //TODO: Make the get language from the regExp
            CompareByLength compareByLength = new CompareByLength();
            SortedSet<string> emptyLanguage = new SortedSet<string>(compareByLength);
            SortedSet<string> resultLanguage = new SortedSet<string>(compareByLength);

            SortedSet<string> leftLanguage, rightLanguage;

            if (maximumSteps < 1)
                return emptyLanguage;
            switch (this.operate)
            {
                case Operator.ONE:
                    resultLanguage.Add(terminal);
                    break;
				case Operator.OR:
					leftLanguage = (left == null ? emptyLanguage : left.getLanguage (maximumSteps - 1));
					rightLanguage = (right == null ? emptyLanguage : right.getLanguage (maximumSteps - 1));
					resultLanguage.UnionWith (leftLanguage);
					resultLanguage.UnionWith (rightLanguage);
                    break;
				case Operator.DOT:
						leftLanguage = (left == null ? emptyLanguage : left.getLanguage (maximumSteps - 1));
						rightLanguage = (right == null ? emptyLanguage : right.getLanguage (maximumSteps - 1));
						foreach (string s1 in leftLanguage) {
							foreach (string s2 in rightLanguage) {
								resultLanguage.Add (s1 + s2);
							}
						}
	                    break;
				case Operator.STAR:
				case Operator.PLUS:
					leftLanguage = (left == null ? emptyLanguage : left.getLanguage (maximumSteps - 1));
					resultLanguage.UnionWith (leftLanguage);
					for (int i = 1; i < maximumSteps; i++) {
						HashSet<string> tempLanguage = new HashSet<string> (resultLanguage);
						foreach (string s1 in leftLanguage) {
							foreach (string s2 in tempLanguage) {
								resultLanguage.Add (s1 + s2);
							}
						}
					}
					if (this.operate == Operator.STAR)
						resultLanguage.Add ("");
                    break;
			default:
					System.Diagnostics.Debug.WriteLine ("getLanguage not yet defined for this operator: " + this.operate);
                    break;
            }
            return resultLanguage;
        }

		public void regexToNDFA(int depth)
		{
			Automata<String> automata = new Automata<String>();
			regexToNDFA(ref depth, automata);
		}

		public void regexToNDFA(ref int depth, Automata<String> automata, string prevstate=null, string nextstate=null) {
			//THOMPSON
			string print = "";
			for (int i = 0; i < depth; i++) {
				print = print + "-";
			}
			if (depth == 0) {
				prevstate = "1";
				automata.defineAsStartState(prevstate);
				depth+=2;
			}
			//depth += 1;
			switch (operate) {
				case Operator.OR:
					automata.addTransition(new Transition<string>(prevstate, depth.ToString()));
					String splitstate = depth.ToString();
					depth++;
					String lnewstate = splitstate;
					if (left.terminal.Count() > 0) {
						foreach (char s in left.terminal) {
							automata.addTransition(new Transition<string>(lnewstate, s, depth.ToString()));
							lnewstate = depth.ToString(); //NOT PROPERLY IMPLEMENTED BEHAVIOR
							depth++;
						}
					} else {
						automata.addTransition(new Transition<string>(prevstate, depth.ToString()));//print = print + left.terminal;//left.regexToNDFA(depth, automata);
						lnewstate = depth.ToString();
						depth++;
					}
					automata.addTransition(new Transition<string>(prevstate, depth.ToString()));
					splitstate = depth.ToString();
					depth++;
					depth++;
					String rnewstate = splitstate;
					if (right.terminal.Count() > 0) {
						foreach (char s in right.terminal) {
							automata.addTransition(new Transition<string>(rnewstate, s, depth.ToString()));
							rnewstate = depth.ToString();
							depth++;
						}
					} else {
						automata.addTransition(new Transition<string>(prevstate, depth.ToString()));//print = print + left.terminal;//left.regexToNDFA(depth, automata);
						rnewstate = depth.ToString(); //NOT PROPERLY IMPLEMENTED BEHAVIOR
						depth++;
					}
					automata.addTransition(new Transition<string>(lnewstate, depth.ToString()));
					automata.addTransition(new Transition<string>(rnewstate, depth.ToString()));
					depth++;

					break;
				case Operator.DOT:
					print = print + "DOT";
					depth++;
					break;
				case Operator.ONE:
					depth++;
					break;
				case Operator.PLUS:
					print = print + "PLUS";
					depth++;
					break;
				case Operator.STAR:
					print = print + "STAR";
					depth++;
					break;
			}

			Console.WriteLine(print);
			if (left != null)
				left.regexToNDFA(ref depth, automata, prevstate);
			if (right != null)
				right.regexToNDFA(ref depth, automata, prevstate);


		}
    }

    public class CompareByLength : IComparer<string>
    {
        public int Compare(string arg0, string arg1)
        {
            if (arg0.Length == arg1.Length)
                return arg0.CompareTo(arg1);
            return arg0.Length - arg1.Length;
        }
    }
}
