using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Polynomials;

namespace Interfaces
{
    internal class Interfaces
    {
        static public void Border()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }


        static public void DrawExpressionMenu(ref Polynomial exp)
        {
            const String menuLabel = "Expression Menu";
            const String description = "Monomial with non - unique powers will be replaced. Press ESC to stop";

            Console.Clear();
            Border();
            Console.SetCursorPosition((Console.WindowWidth - menuLabel.Length) / 2, 0);
            Console.WriteLine(menuLabel);
            Console.SetCursorPosition((Console.WindowWidth - description.Length) / 2, 1);
            Console.WriteLine(description);
            Border();
            exp.Show();
            Border();
        }


        static public void EnterExpression(ref Polynomial exp)
        {
            String input = "";

            while (true)
            {
                Monomial monom = new Monomial();

                DrawExpressionMenu(ref exp);

                // multiplier enter
                while (true)
                {
                    try
                    {
                        if (EnterString(ref input, "Multiplier: ") == ConsoleKey.Escape) { return; }
                        monom.mult = Convert.ToDouble(input);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Not a double number. Try again\n");
                        continue;
                    }
                    break;
                }

                // pow enter
                while (true)
                {
                    try
                    {
                        if (EnterString(ref input, "pow: ") == ConsoleKey.Escape) { return; }
                        monom.pow = Convert.ToInt32(input);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Not a int number. Try again\n");
                        continue;
                    }
                    break;
                }

                exp.Insert(monom);
            }
        }


        static public ConsoleKey EnterString(ref String output, String msg = "")
        {
            String input = "";

            Console.Write(msg);

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    output = input;
                    return ConsoleKey.Enter;
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    output = input;
                    return ConsoleKey.Escape;
                }
                if (key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.Delete)
                {
                    if (!string.IsNullOrEmpty(input))
                    {
                        input = input.Substring(0, input.Length - 1);

                        Console.CursorLeft -= 1;
                        Console.Write(" ");
                        Console.CursorLeft -= 1;
                    }
                }
                else
                {
                    Console.Write(key.KeyChar);
                    input += key.KeyChar;
                }
            }
        }


        static public double EnterDouble(String msg = "")
        {
            double num = double.NaN;

            while (true)
            {
                try
                {
                    Console.Write(msg);
                    num = Convert.ToDouble(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Not a double number. Try again\n");
                    continue;
                }
                break;
            }


            return num;
        }


        static public double EnterInt(String msg = "")
        {
            double num = double.NaN;

            while (true)
            {
                try
                {
                    Console.Write(msg);
                    num = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Not a int number. Try again\n");
                    continue;
                }
                break;
            }


            return num;
        }
    }


}
