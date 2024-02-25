using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Polynomials
{
    /// <summary>
    /// Class for work with equation of the form mult*x^pow
    /// </summary>
    public class Monomial
    {
        public double mult { get; set; }
        public int pow { get; set; }

        public Monomial() { this.mult = 0; this.pow = 0; }
        public Monomial(double mult) { this.mult = mult; this.pow = 0; }
        public Monomial(double mult, int pow) { this.mult = mult; this.pow = pow; }
        public Monomial(Monomial monom) { this.mult = monom.mult; this.pow = monom.pow; }

        public void TakeDerivative()
        {
            //Take derevative from const
            if (this.pow == 0)
            {
                this.mult = 0;
                return;
            }

            this.mult *= this.pow;
            this.pow -= 1;
        }


        public double Сalculate(double x)
        {
            return this.mult * Math.Pow(x, this.pow);
        }

        public void Show( bool ignoreSignFlag = false)
        {
            if (ignoreSignFlag)
            {
                if (this.pow == 0) { Console.Write(Math.Abs(this.mult)); }
                else if (this.pow == 1) { Console.Write(Math.Abs(this.mult) + "*x"); }
                else { Console.Write(Math.Abs(this.mult) + "*x^" + this.pow); }
            }
            else
            {
                if (this.pow == 0) { Console.Write(this.mult); }
                else if (this.pow == 1) { Console.Write(this.mult + "*x"); }
                else { Console.Write(this.mult + "*x^" + this.pow); }
            }
            
        }

        public String ShowInStr(bool ignoreSignFlag = false)
        {
            if (ignoreSignFlag)
            { 
                if (this.pow == 0) { return Convert.ToString(Math.Abs(this.mult)); }
                else if (this.pow == 1) { return Convert.ToString(Math.Abs(this.mult) + "*x"); }
                else { return Convert.ToString(Math.Abs(this.mult) + "*x^" + this.pow); }
            }
            else
            {
                if (this.pow == 0) { return Convert.ToString(this.mult); }
                else if (this.pow == 1) { return Convert.ToString(this.mult + "*x"); }
                else { return Convert.ToString(this.mult + "*x^" + this.pow); }
            }

        }

    }

    /// <summary>
    /// Class for work with polynomial expression
    /// </summary>
    public class Polynomial
    {
        public List<Monomial> expression = new List<Monomial>();

        public Polynomial()
        {
            expression.Add(new Monomial());
        }
        public Polynomial(Polynomial exp)
        {
            for (int i = 0; i < exp.expression.Count; ++i)
            {
                expression.Add(new Monomial(exp.expression[i].mult, exp.expression[i].pow));
            }
        }


        public void Clear()
        {
            expression.Clear();
            expression.Add(new Monomial());
        }


        public void Insert(Monomial monom)
        {
            bool enterIn = false;

            // equal to zero monom
            if (monom.mult == 0 && expression.Count == 1)
            {
                expression[0].mult = 0;
                expression[0].pow = 0;
                enterIn = true;
            }
            // delete monoms with zero multiplier
            else if (monom.mult == 0 && expression.Count > 1) 
            {
                for (int i = 0; i < expression.Count; ++i)
                {
                    if (expression[i].pow == monom.pow)
                    {
                        expression.RemoveAt(i);
                        enterIn = true;
                        break;
                    }

                }
            }
            // change monom multiplier with same pow
            else
            {
                for (int i = 0; i < expression.Count; ++i) 
                {
                    if (monom.pow == expression[i].pow)
                    {
                        expression[i].mult = monom.mult;
                        enterIn = true;
                        break;
                    }

                }
            }

            // add monom with new pow
            if (enterIn == false)
            {
                expression.Add(monom);

                // delete zero
                if (expression.Count > 1)
                {
                    for (int i = 0; i < expression.Count; ++i)
                    {
                        if (expression[i].pow == 0 && expression[i].mult == 0)
                        {
                            expression.RemoveAt(i);
                            enterIn = true;
                            break;
                        }

                    }
                }
            }   
        }

        public void TakeDerivative()
        {
            for (int i = 0; i < expression.Count; ++i)
            {
                if (expression[i].pow == 0 && expression.Count != 1)
                {
                    expression.RemoveAt(i);
                }
                else
                {
                    expression[i].TakeDerivative();
                }
            }

        }


        public double Сalculate(double x)
        {
            double y = 0;

            foreach(Monomial monom in expression)
            {
                y += monom.Сalculate(x);
            }

            return y;
        }

        public void Show(bool ignoreSpaceFlag = false)
        {
            if (ignoreSpaceFlag)
            {
                if (expression.Count != 0) { expression[0].Show(); }
                for (int i = 1; i < expression.Count; ++i)
                {
                    if (expression[i].mult > 0) Console.Write("+");
                    else Console.Write("-");
                    expression[i].Show(true);
                }
            }
            else
            {
                if (expression.Count != 0) { expression[0].Show(); }
                for (int i = 1; i < expression.Count; ++i)
                {
                    if (expression[i].mult > 0) Console.Write(" + ");
                    else Console.Write(" - ");
                    expression[i].Show(true);
                }
                Console.WriteLine();
            }
        }


        public String ShowInStr(bool ignoreSignFlag = false)
        {
            String str = "";

            if (ignoreSignFlag)
            {
                if (expression.Count != 0) { str += expression[0].ShowInStr(); }
                for (int i = 1; i < expression.Count; ++i)
                {
                    if (expression[i].mult > 0) { str += "+"; }
                    else { str += "-"; }
                    str += expression[i].ShowInStr(true);
                }
            }
            else
            {
                if (expression.Count != 0) { str += expression[0].ShowInStr(); }
                for (int i = 1; i < expression.Count; ++i)
                {
                    if (expression[i].mult > 0) { str += " + "; }
                    else { str += " - "; }
                    str += expression[i].ShowInStr(true);
                }
                str += '\n';
            }

            return str;
        }
    }
}
