using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;

using Polynomials;
using static Interfaces.Interfaces;

namespace FirstTask {
    internal class Program
    {
        static void Main(string[] args)
        {
            Polynomial exp = new Polynomial();

            double leftBorder = -10;
            double rightBorder = 10;
            double epsilon = 0.0001;
            int depth = 1000;

            MainHandle(ref exp, ref leftBorder, ref rightBorder, ref epsilon, ref depth);

            Console.ReadKey();

            return;
        }
    }
}
