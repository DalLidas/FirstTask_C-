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

            MainHandle(ref exp);

            Console.ReadKey();

            return;
        }
    }
}
