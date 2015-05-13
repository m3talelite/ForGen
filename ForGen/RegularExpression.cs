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


    }
}
