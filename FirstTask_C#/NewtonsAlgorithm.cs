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
        static public String NewtonsMethodCheck(ref double result, Polynomial exp, double a, double b, double epsilon)
        {
            String errMsg = "";
            double iterator = 0;

            Polynomial buff = new Polynomial(exp);

            // error if expression dont cross zero 
            if (exp.Сalculate(a) * exp.Сalculate(b) > 0)
            {
                errMsg += "Expression dont cross zero. ";
            }

            // error if first derivative dont save sign
            buff.TakeDerivative();

            iterator = a;
            if (buff.Сalculate(iterator) > 0)
            {
                while (iterator < b)
                {
                    if (buff.Сalculate(iterator) <= 0)
                    {
                        errMsg += "First derivative cross zero and dont save sign. ";
                        break;
                    }
                    iterator += epsilon;
                }
            }
            else
            {
                while (iterator < b)
                {
                    if (buff.Сalculate(iterator) >= 0)
                    {
                        errMsg += "First derivative cross zero and dont save sign. ";
                        break;
                    }
                    iterator += epsilon;
                }
            }


            // error if second derivative dont save sign
            buff.TakeDerivative();

            iterator = a;
            if (buff.Сalculate(iterator) > 0)
            {
                while (iterator < b)
                {
                    if (buff.Сalculate(iterator) <= 0)
                    {
                        errMsg += "Second derivative cross zero and dont save sign. ";
                        break;
                    }
                    iterator += epsilon;
                }
            }
            else
            {
                while (iterator < b)
                {
                    if (buff.Сalculate(iterator) >= 0)
                    {
                        errMsg += "Second derivative cross zero and dont save sign. ";
                        break;
                    }
                    iterator += epsilon;
                }
            }


            return errMsg;
        }


        static public bool NewtonsMethod(ref double result, Polynomial exp, double a, double b, double epsilon, int depth)
        {
            Polynomial deriv1 = new Polynomial(exp);

            deriv1.TakeDerivative();

            Polynomial deriv2 = new Polynomial(deriv1);

            deriv2.TakeDerivative();

            // Calc x0
            double x0 = (a + b)/2;

            if (exp.Сalculate(a) * deriv2.Сalculate(a) > 0)
            {
                x0 = a;
            }
            else if(exp.Сalculate(b) * deriv2.Сalculate(b) > 0)
            {
                x0 = b;
            }

            double x1 = x0 - (exp.Сalculate(x0) / deriv1.Сalculate(x0));
            int i = 0;

            
            while (true)
            {
                if (++i > depth) break;

                if (Math.Abs(x1 - x0) < epsilon)
                {
                    result = x1;
                    return false;
                }
                    
                x0 = x1;
                if (deriv1.Сalculate(x0) == 0) { x1 = x0 - epsilon; }
                else { x1 = x0 - (exp.Сalculate(x0) / deriv1.Сalculate(x0)); }
            }

            // Exit if depth counter overflow and set overflow flag
            result = x1;
            return true;
        }

        static public bool EqualZero(double num, double epsilon)
        {
            if (num > -epsilon && num < epsilon) { return true; }
            else { return false; }
        }

    }
}
