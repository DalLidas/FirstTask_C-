using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Polynomials;
using static NewtonsAlgorithm.NewtonsAlgorithm;

namespace Interfaces
{
    internal enum Modes
    {
        expressionIMode = ConsoleKey.F1,
        NewtonsCalcMode = ConsoleKey.F2,
        loadMode = ConsoleKey.F3,
        saveMode = ConsoleKey.F4,
        exitMode = ConsoleKey.Escape,

    }
    internal class Interfaces
    {
        static public void DrawBorder(String border = "-")
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write(border);
            }
            Console.WriteLine();
        }


        static public void DrawExpressionMenu(ref Polynomial exp)
        {
            String menuLabel = "Expression Menu";
            String keyBinds = "F1 - ExpressionMenu, F2 - NewtonsMenu, F3 - LoadMenu, F4 - SaveMenu, Esc - Exit";
            String description = "Monomial with non-unique powers will be replaced. Press ESC to stop";
            


            Console.Clear();

            DrawBorder("=");
            Console.SetCursorPosition((Console.WindowWidth - menuLabel.Length) / 2, 0);
            Console.WriteLine(menuLabel);

            Console.SetCursorPosition((Console.WindowWidth - keyBinds.Length) / 2, 1);
            Console.WriteLine(keyBinds);
            DrawBorder("=");

            Console.SetCursorPosition((Console.WindowWidth - description.Length) / 2, 3);
            Console.WriteLine(description);
            DrawBorder();

            exp.Show();
            DrawBorder("=");
        }


        static public void DrawNewtonsMenu(ref Polynomial exp, double lBorder, double rBorder, double epsilon, double depth)
        {
            String menuLabel = "Newtons Menu";
            String keyBinds = "F1 - ExpressionMenu, F2 - NewtonsMenu, F3 - LoadMenu, F4 - SaveMenu, Esc - Exit";
            String description = "lBorder: " + Convert.ToString(lBorder) + ", rBorder: " + Convert.ToString(rBorder) + ", epsilon: " + Convert.ToString(epsilon) + ", depth: " + Convert.ToString(depth);

            Console.Clear();

            DrawBorder("=");
            Console.SetCursorPosition((Console.WindowWidth - menuLabel.Length) / 2, 0);
            Console.WriteLine(menuLabel);

            Console.SetCursorPosition((Console.WindowWidth - keyBinds.Length) / 2, 1);
            Console.WriteLine(keyBinds);
            DrawBorder("=");

            Console.SetCursorPosition((Console.WindowWidth - description.Length) / 2, 3);
            Console.WriteLine(description);
            DrawBorder();

            exp.Show();
            DrawBorder("=");
        }


        static public void DrawLoadMenu(ref Polynomial exp)
        {
            String menuLabel = "Load Menu";
            String keyBinds = "F1 - ExpressionMenu, F2 - NewtonsMenu, F3 - LoadMenu, F4 - SaveMenu, Esc - Exit";
            String description = " ";


            Console.Clear();

            DrawBorder("=");
            Console.SetCursorPosition((Console.WindowWidth - menuLabel.Length) / 2, 0);
            Console.WriteLine(menuLabel);

            Console.SetCursorPosition((Console.WindowWidth - keyBinds.Length) / 2, 1);
            Console.WriteLine(keyBinds);
            DrawBorder("=");

            Console.SetCursorPosition((Console.WindowWidth - description.Length) / 2, 3);
            Console.WriteLine(description);
            DrawBorder();

            exp.Show();
            DrawBorder("=");
        }


        static public void DrawSaveMenu(ref Polynomial exp)
        {
            String menuLabel = "SaveMenu";
            String keyBinds = "F1 - ExpressionMenu, F2 - NewtonsMenu, F3 - LoadMenu, F4 - SaveMenu, Esc - Exit";
            String description = " ";


            Console.Clear();

            DrawBorder("=");
            Console.SetCursorPosition((Console.WindowWidth - menuLabel.Length) / 2, 0);
            Console.WriteLine(menuLabel);

            Console.SetCursorPosition((Console.WindowWidth - keyBinds.Length) / 2, 1);
            Console.WriteLine(keyBinds);
            DrawBorder("=");

            Console.SetCursorPosition((Console.WindowWidth - description.Length) / 2, 3);
            Console.WriteLine(description);
            DrawBorder();

            exp.Show();
            DrawBorder("=");
        }


        static public void MainHandle(ref Polynomial exp)
        {
            Modes mod = Modes.expressionIMode;

            while (true)
            {
                switch (mod)
                {
                    case Modes.expressionIMode:
                        ExpressionHandle(ref exp, ref mod);
                        break; 

                    case Modes.NewtonsCalcMode:
                        NewtonsHandle(ref exp, ref mod);
                        break;

                    case Modes.loadMode:
                        LoadHandle(ref exp, ref mod);
                        break;

                    case Modes.saveMode:
                        SaveHandle(ref exp, ref mod);
                        break;

                    case Modes.exitMode:
                        Console.Clear();
                        Console.WriteLine("Goodbye");
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Error");
                        return;
                }
            }
        }


        static public Modes KeyToMode(ConsoleKey key)
        {
            Modes mod = (Modes)key;
            return (mod);
        }


        static public void ExpressionHandle(ref Polynomial exp, ref Modes gMod)
        {
            String input = "";
            ConsoleKey lMod;

            while (true)
            {
                Monomial monom = new Monomial();

                DrawExpressionMenu(ref exp);

                // multiplier enter
                while (true)
                {
                    try
                    {
                        lMod = InputHandler(ref input, "Multiplier: ");
                        if (lMod != ConsoleKey.Enter) { gMod = KeyToMode(lMod); return; }
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
                        lMod = InputHandler(ref input, "pow: ");
                        if (lMod != ConsoleKey.Enter) { gMod = KeyToMode(lMod); return; }
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


        static public void NewtonsHandle(ref Polynomial exp, ref Modes gMod)
        {
            String input = "";
            ConsoleKey lMod;

            double leftBorder = -10;
            double rightBorder = 10;
            double epsilon = 0.0001;
            int depth = 1000;

            double result = 0;
            bool correctResultFlag = false;

            while(true)
            {
                DrawNewtonsMenu(ref exp, leftBorder, rightBorder, epsilon, depth);

                //leftBorder enter
                while (true)
                {
                    try
                    {
                        lMod = InputHandler(ref input, "Left border: ");
                        if (lMod != ConsoleKey.Enter) { gMod = KeyToMode(lMod); return; }
                        if (input != "") leftBorder = Convert.ToDouble(input);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Not a double number. Try again\n");
                        continue;
                    }
                    break;
                }

                // rightBorder enter
                while (true)
                {
                    try
                    {
                        lMod = InputHandler(ref input, "Right border: ");
                        if (lMod != ConsoleKey.Enter) { gMod = KeyToMode(lMod); return; }
                        if (input != "") { rightBorder = Convert.ToDouble(input); } 
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Not a double number. Try again\n");
                        continue;
                    }
                    break;
                }

                // Epsolon enter
                while (true)
                {
                    try
                    {
                        lMod = InputHandler(ref input, "Epsilon: ");
                        if (lMod != ConsoleKey.Enter) { gMod = KeyToMode(lMod); return; }
                        if (input != "") { epsilon = Convert.ToDouble(input); }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Not a double number. Try again\n");
                        continue;
                    }
                    break;
                }

                // Depth enter
                while (true)
                {
                    try
                    {
                        lMod = InputHandler(ref input, "Depth: ");
                        if (lMod != ConsoleKey.Enter) { gMod = KeyToMode(lMod); return; }
                        if (input != "") { depth = Convert.ToInt32(input); }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Not a double number. Try again\n");
                        continue;
                    }
                    break;
                }

                DrawBorder();

                correctResultFlag = NewtonsMethod(ref result, exp, leftBorder, rightBorder, epsilon, depth);

                DrawBorder();

                if (correctResultFlag)
                {
                    Console.WriteLine(@"ResultX = {0}", result);
                }
                else
                {
                    Console.WriteLine(@"Uncorrect resultX = {0}", result);
                }

                Console.WriteLine(@"ResultY = {0}", exp.Сalculate(result));
            

                Console.ReadKey();
            }
        }


        static public void LoadHandle(ref Polynomial exp, ref Modes gMod)
        {
            String input = "";
            ConsoleKey lMod;

            DrawLoadMenu(ref exp);

            // Depth enter
            while (true)
            {
                try
                {
                    lMod = InputHandler(ref input, "Depth: ");
                    if (lMod != ConsoleKey.Enter) { gMod = KeyToMode(lMod); return; }
                    //if (input != "") { depth = Convert.ToInt32(input); }
                }
                catch (Exception)
                {
                    Console.WriteLine("Not a double number. Try again\n");
                    continue;
                }
                break;
            }
        }


        static public void SaveHandle(ref Polynomial exp, ref Modes gMod)
        {
            String input = "";
            ConsoleKey lMod;

            DrawSaveMenu(ref exp);

            // Depth enter
            while (true)
            {
                try
                {
                    lMod = InputHandler(ref input, "Depth: ");
                    if (lMod != ConsoleKey.Enter) { gMod = KeyToMode(lMod); return; }
                    //if (input != "") { depth = Convert.ToInt32(input); }
                }
                catch (Exception)
                {
                    Console.WriteLine("Not a double number. Try again\n");
                    continue;
                }
                break;
            }
        }

        static public ConsoleKey InputHandler(ref String output, String msg = "")
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
                else if (key.Key == ConsoleKey.Escape)
                {
                    output = input;
                    return ConsoleKey.Escape;
                }
                else if (key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.Delete)
                {
                    if (!string.IsNullOrEmpty(input))
                    {
                        input = input.Substring(0, input.Length - 1);

                        Console.CursorLeft -= 1;
                        Console.Write(" ");
                        Console.CursorLeft -= 1;
                    }
                }
                else if (key.Key == ConsoleKey.F1 || key.Key == ConsoleKey.F2 || key.Key == ConsoleKey.F3 || key.Key == ConsoleKey.F4)
                {
                    output = input;
                    return key.Key;
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
