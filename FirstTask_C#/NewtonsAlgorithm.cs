using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Polynomials;

namespace NewtonsAlgorithm
{
    internal class NewtonsAlgorithm
    {
        static public bool NewtonsMethod(ref double result, Polynomial exp, double a, double b, double epsilon, int depth)
        {
            Console.Write("expression: ");
            exp.Show();

            Polynomial deriv1 = new Polynomial(exp);

            deriv1.TakeDerivative();
            Console.Write("derivative: ");
            deriv1.Show();

            double x0 = (a + b) / 2;
            Console.Write("x0: ");
            Console.WriteLine(x0);

            double x1 = 0;

            if (EqualZero(deriv1.Сalculate(x0), epsilon)) { x1 = x0 - epsilon;} 
            else { x1 = x0 - (exp.Сalculate(x0) / deriv1.Сalculate(x0)); }

            Console.Write("x1: ");
            Console.WriteLine(x1);

            int i = 0;


            while (true)
            {
                if (++i > depth) break;

                if (Math.Abs(x1 - x0) < epsilon)
                {
                    result = x1;
                    return true;
                }
                    
                x0 = x1;
                if (EqualZero(deriv1.Сalculate(x0), epsilon)) { x1 = x0 - epsilon; }
                else { x1 = x0 - (exp.Сalculate(x0) / deriv1.Сalculate(x0)); }
            }

            // Exit if depth counter overflow
            result = x1;
            return false;
        }

        static public bool EqualZero(double num, double epsilon)
        {
            if (num > -epsilon && num < epsilon) { return true; }
            else { return false; }
        }

    }
}
