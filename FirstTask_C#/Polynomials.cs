using System;
using System.Collections.Generic;
using System.Linq;
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

        public void TakeDerivative()
        {
            //Take derevative from const
            if (pow == 0)
            {
                mult = 0;
                return;
            }

            mult *= pow;
            pow -= 1;
        }

        public void Show()
        {
            Console.Write(mult + "*x^" + pow);
        }

    }

    /// <summary>
    /// Class for work with polynomial expression
    /// </summary>
    public class Polynomial
    {
        public List<Monomial> expression = new List<Monomial>();

        public void Insert(Monomial monom)
        {
            bool enterIn = false;
            for (int i = 0; i < expression.Count; ++i)
            {
                if (monom.pow == expression[i].pow)
                {
                    expression[i].mult = monom.mult;
                    enterIn = true;
                }

            }
            if (enterIn == false)
            {
                expression.Add(monom);
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
                expression[i].TakeDerivative();
            }

        }

        public void Show()
        {
            if (expression.Count != 0) expression[0].Show();
            for (int i = 1; i < expression.Count; ++i)
            {
                Console.Write(" + ");
                expression[i].Show();
            }

            Console.WriteLine();
        }

    }
}
