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
        static public void Hello()
        {
            String msg = "Creator: Mukhametov Danil 423 group version 3";
            Console.SetCursorPosition((Console.WindowWidth - msg.Length) / 2, (Console.WindowHeight - 1) / 2);
            Console.WriteLine(msg);
            Console.ReadKey(true);
        }


        static void Main(string[] args)
        {
            Polynomial exp = new Polynomial();

            double leftBorder = -10;
            double rightBorder = 10;
            double epsilon = 0.0001;
            int depth = 1000;

            double result = double.NaN;

            Hello();

            MainHandle(ref exp, ref leftBorder, ref rightBorder, ref epsilon, ref depth, ref result);

            Console.ReadKey(true);

            return;
        }
    }
}
