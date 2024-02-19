﻿using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.InteropServices;

using Polynomials;
using static Interfaces.Interfaces;


        public void TakeDerivative ()
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
    /// 
    /// </summary>
    public class Polynomial
    {
        public List<Monomial> expression = new List<Monomial>();

        public void Insert(Monomial monom)
        {
            bool enterIn = false;
            for (int i = 0; i < expression.Count; ++i) {
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

    internal class Program
    {
        static void Main(string[] args)
        {
            Polynomial exp = new Polynomial();
            EnterExpression(ref exp);

            exp.Show();
            exp.TakeDerivative();
            exp.Show();
            exp.TakeDerivative();
            exp.Show();

            Console.WriteLine();
        }



    }
}
