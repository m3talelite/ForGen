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
                    break;
                case Operator.DOT:
                    break;
                case Operator.STAR:
                    break;
                case Operator.PLUS:
                    break;
                default:
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
