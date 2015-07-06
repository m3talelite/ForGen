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

    [Serializable]
    public class RegularExpression
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

		public Automata<String> regexToNDFA()
		{
			int depth = 0;
			return regexToNDFA(depth);
		}

		public Automata<String> regexToNDFA(int depth)
		{
			Automata<String> automata = new Automata<String>();
			regexToNDFA(ref depth, ref automata);
			return automata;
		}

		public void regexToNDFA(ref int depth, ref Automata<String> automata, string prevstate=null, string nextstate=null) {
			//THOMPSON
			if (depth == 0) {
				prevstate = "1";
				automata.defineAsStartState(prevstate);
				depth+=2;
			}
			//depth += 1;
			bool nonext = false;

			switch (operate) {
				case Operator.OR:
					automata.addTransition(new Transition<string>(prevstate, depth.ToString()));
					String splitstate = depth.ToString();
					depth++;
					String lnewstate = splitstate;
					if (left.operate == Operator.ONE) {
						foreach (char s in left.terminal) {
							automata.addTransition(new Transition<string>(lnewstate, s, depth.ToString()));
							automata.addToAlphabet(s);
							lnewstate = depth.ToString(); //NOT PROPERLY IMPLEMENTED BEHAVIOR
							depth++;
						}
					} else {
						automata.addTransition(new Transition<string>(lnewstate, depth.ToString()));//print = print + left.terminal;//left.regexToNDFA(depth, automata);
						lnewstate = depth.ToString();
						depth++;
						String loldstate2 = depth.ToString();
						depth++;
						left.regexToNDFA(ref depth,ref automata, lnewstate, loldstate2);
						lnewstate = loldstate2;
						//depth++;
					}
					automata.addTransition(new Transition<string>(prevstate, depth.ToString()));
					splitstate = depth.ToString();
					depth++;
					depth++;
					String rnewstate = splitstate;
					if (right.operate == Operator.ONE) {
						foreach (char s in right.terminal) {
							automata.addTransition(new Transition<string>(rnewstate, s, depth.ToString()));
							automata.addToAlphabet(s);
							rnewstate = depth.ToString();
							depth++;
						}
					} else {
						automata.addTransition(new Transition<string>(rnewstate, depth.ToString()));//print = print + left.terminal;//left.regexToNDFA(depth, automata);
						rnewstate = depth.ToString(); //NOT PROPERLY IMPLEMENTED BEHAVIOR
						depth++;
						String roldstate = depth.ToString();
						depth++;
						right.regexToNDFA(ref depth,ref automata, rnewstate, roldstate);
						rnewstate = roldstate;
						//depth++;
					}
					if (nextstate == null) {
						automata.addTransition(new Transition<string>(lnewstate, depth.ToString()));
						automata.addTransition(new Transition<string>(rnewstate, depth.ToString()));
						automata.defineAsFinalState(depth.ToString());
						depth++;
					} else {
						automata.addTransition(new Transition<string>(lnewstate, nextstate)); //WRONG
						automata.addTransition(new Transition<string>(rnewstate, nextstate));
					}
						
					break;
				case Operator.DOT:
					
					if (nextstate == null) {
						nextstate = depth.ToString();
						depth++;
						nonext = true;
					}
					depth++;
					String tmpstate = depth.ToString();
					if (left.operate == Operator.ONE) {
						lnewstate = prevstate;
						depth++;
						foreach (char s in left.terminal) {
							automata.addTransition(new Transition<string>(lnewstate, s, depth.ToString()));
							automata.addToAlphabet(s);
							lnewstate = depth.ToString();
							depth++;
						}
						automata.addTransition(new Transition<string>(lnewstate, tmpstate));
					}
					else
						left.regexToNDFA(ref depth, ref automata, prevstate, tmpstate);
					if (right.operate == Operator.ONE) {
						lnewstate = tmpstate;
						depth++;
						foreach (char s in right.terminal) {
							automata.addTransition(new Transition<string>(lnewstate, s, depth.ToString()));
							automata.addToAlphabet(s);
							lnewstate = depth.ToString();
							depth++;
						}
						automata.addTransition(new Transition<string>(lnewstate, nextstate));
					}
					else
						right.regexToNDFA(ref depth,ref automata, tmpstate, nextstate);
					if (nonext) {
						automata.defineAsFinalState(nextstate);
					}
					break;
				case Operator.ONE: //NOT REALLY IMPLEMENTED
					depth++;
					lnewstate = prevstate;
					foreach (char s in terminal) {
						automata.addTransition(new Transition<string>(lnewstate, s, depth.ToString()));
						automata.addToAlphabet(s);
						lnewstate = depth.ToString();
						depth++;
					}
					if (nextstate == null)
						automata.defineAsFinalState((depth-1).ToString());
					break;
				case Operator.PLUS:
					if (nextstate == null) {
						nextstate = depth.ToString();
						depth++;
						nonext = true;
					}
					depth++;
					automata.addTransition(new Transition<string>(prevstate, depth.ToString()));
					lnewstate = depth.ToString();
					String loldstate = depth.ToString();
					depth++;
					if (left.operate == Operator.ONE) {
						foreach (char s in left.terminal) {
							automata.addTransition(new Transition<string>(lnewstate, s, depth.ToString()));
							automata.addToAlphabet(s);
							lnewstate = depth.ToString();
							depth++;
						}
						automata.addTransition(new Transition<string>(lnewstate, loldstate));
						automata.addTransition(new Transition<string>(lnewstate, depth.ToString()));
						rnewstate = depth.ToString();
					} else {
						rnewstate = depth.ToString();
						depth++;
						left.regexToNDFA(ref depth, ref automata, lnewstate, rnewstate); //NOT CHECKED, PROBABLY WRONG
						//automata.addTransition(new Transition<string>(loldstate, depth.ToString()));
						automata.addTransition(new Transition<string>(rnewstate, lnewstate));
						automata.addTransition(new Transition<string>(rnewstate, depth.ToString()));
						rnewstate = depth.ToString();
					}
					if (!nonext)
						automata.addTransition(new Transition<string>(depth.ToString(), nextstate));
					else {
						depth++;
						automata.addTransition(new Transition<string>(rnewstate, depth.ToString()));
						automata.defineAsFinalState(depth.ToString());
					}
					depth++;
					break;
				case Operator.STAR:
					if (depth.ToString() == nextstate)
						depth++;
					if (nextstate == null) {
						nextstate = depth.ToString();
						depth++;
						nonext = true;
					}
					automata.addTransition(new Transition<string>(prevstate, depth.ToString()));
					lnewstate = depth.ToString();
					rnewstate = lnewstate;
					depth++;
					if (left.operate == Operator.ONE) {
						foreach (char s in left.terminal) {
							automata.addTransition(new Transition<string>(lnewstate, s, depth.ToString()));
							automata.addToAlphabet(s);
							lnewstate = depth.ToString(); //NOT PROPERLY IMPLEMENTED BEHAVIOR
							depth++;
						}
						automata.addTransition(new Transition<string>(prevstate, nextstate));
						automata.addTransition(new Transition<string>(lnewstate, rnewstate));
					} else {
						automata.addTransition(new Transition<string>(lnewstate, depth.ToString()));//print = print + left.terminal;//left.regexToNDFA(depth, automata);
						loldstate = lnewstate;
						lnewstate = depth.ToString();
						depth++;
						String futurestate = depth.ToString();
						depth++;
						left.regexToNDFA(ref depth, ref automata, lnewstate, futurestate);
						depth++;
						automata.addTransition(new Transition<string>(futurestate, depth.ToString()));
						automata.addTransition(new Transition<string>(futurestate, lnewstate));
						automata.addTransition(new Transition<string>(loldstate, depth.ToString()));
						lnewstate = depth.ToString();
						depth++;

					}
					if (!nonext)
						automata.addTransition(new Transition<string>(lnewstate, nextstate));
					else {
						automata.addTransition(new Transition<string>(lnewstate, depth.ToString()));
						automata.defineAsFinalState(depth.ToString());
					}
					break;
			}

		}
		public string toString(bool parentOr=false)
		{
			string result = "";
			switch (operate) {
				case Operator.OR:
					if (parentOr)
						result = left.toString(parentOr) + '|' + right.toString(parentOr);
					else {
						parentOr = true;
						result = "(" + left.toString(parentOr) + '|' + right.toString(parentOr) + ")";
					}
					break;
				case Operator.PLUS:
					if (parentOr)
						result = left.toString() + '+';
					else
						result = '(' + left.toString() + ')' + '+';
					break;
				case  Operator.STAR:
					if (parentOr)
						result =left.toString() + '*';
					else
						result = '(' + left.toString() + ')' + '*';
					break;
				case Operator.DOT:
					result = left.toString(parentOr) + right.toString(parentOr);
					break;
				case Operator.ONE:
					result = terminal;
					break;
			}
			return result;
		}
		public override string ToString()
		{
			return toString();
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
