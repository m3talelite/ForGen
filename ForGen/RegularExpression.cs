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
					System.Diagnostics.Debug.WriteLine ("getLangiage is nog niet gedefineerd voor de operator: " + this.operate);
                    break;
            }
            return resultLanguage;
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
