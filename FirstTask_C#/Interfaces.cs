using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
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
            String description = "Monomial with non-unique powers will be replaced. Zero mult delete monomial. Press ESC to stop";

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


        static public void DrawNewtonsMenu(ref Polynomial exp, ref double lBorder, ref double rBorder, ref double epsilon, ref int depth)
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


        static public void DrawLoadMenu(ref Polynomial exp, ref double lBorder, ref double rBorder, ref double epsilon, ref int depth)
        {
            String menuLabel = "Load Menu";
            String keyBinds = "F1 - ExpressionMenu, F2 - NewtonsMenu, F3 - LoadMenu, F4 - SaveMenu, Esc - Exit";
            String description1 = "Enter file path to load expression";
            String description2 = "lBorder: " + Convert.ToString(lBorder) + ", rBorder: " + Convert.ToString(rBorder) + ", epsilon: " + Convert.ToString(epsilon) + ", depth: " + Convert.ToString(depth);

            Console.Clear();

            DrawBorder("=");
            Console.SetCursorPosition((Console.WindowWidth - menuLabel.Length) / 2, 0);
            Console.WriteLine(menuLabel);

            Console.SetCursorPosition((Console.WindowWidth - keyBinds.Length) / 2, 1);
            Console.WriteLine(keyBinds);
            DrawBorder("=");

            Console.SetCursorPosition((Console.WindowWidth - description1.Length) / 2, 3);
            Console.WriteLine(description1);
            DrawBorder();

            Console.SetCursorPosition((Console.WindowWidth - description2.Length) / 2, 3);
            Console.WriteLine(description2);
            DrawBorder();

            exp.Show();
            DrawBorder("=");
        }


        static public void DrawSaveMenu(ref Polynomial exp)
        {
            String menuLabel = "Save Menu";
            String keyBinds = "F1 - ExpressionMenu, F2 - NewtonsMenu, F3 - LoadMenu, F4 - SaveMenu, Esc - Exit";
            String description = "Enter file path to save all data";

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


        static public void MainHandle(ref Polynomial exp, ref double leftBorder, ref double rightBorder, ref double epsilon, ref int depth, ref double result)
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
                        NewtonsHandle(ref exp, ref mod, ref leftBorder, ref rightBorder, ref epsilon, ref depth, ref result);
                        break;

                    case Modes.loadMode:
                        LoadHandle(ref exp, ref mod, ref leftBorder, ref rightBorder, ref epsilon, ref depth);
                        break;

                    case Modes.saveMode:
                        SaveHandle(ref exp, ref mod, ref leftBorder, ref rightBorder, ref epsilon, ref depth, ref result);
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


        static public void NewtonsHandle(ref Polynomial exp, ref Modes gMod, ref double leftBorder, ref double rightBorder, ref double epsilon, ref int depth, ref double result)
        {
            String input = "";
            ConsoleKey lMod;

            

            while (true)
            {
                result = Double.NaN;
                bool overflowFlag = false;

                DrawNewtonsMenu(ref exp, ref leftBorder, ref rightBorder, ref epsilon, ref depth);

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

                // Newtons method check
                DrawBorder();
                String errMSG = NewtonsMethodCheck(ref result, exp, leftBorder, rightBorder, epsilon);
                if (errMSG != "")
                {
                    Console.WriteLine("Error log: " + errMSG);
                    DrawBorder();

                    bool errIgnoreFlag = false;

                    while (true)
                    {
                        try
                        {
                            lMod = InputHandler(ref input, "Do you want continue? (Y/N): ");
                            if (lMod != ConsoleKey.Enter) { gMod = KeyToMode(lMod); return; }
                            if (input == "Y" || input == "y") { errIgnoreFlag = true; }
                            else if (input == "N" || input == "n") { errIgnoreFlag = false; }
                            else { continue; }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        break;
                    }

                    if (errIgnoreFlag == false)
                    {
                        continue;
                    }
                }

                // Newtons method
                overflowFlag = NewtonsMethod(ref result, exp, leftBorder, rightBorder, epsilon, depth);

                DrawBorder();
                if (overflowFlag)
                {
                    Console.WriteLine("Newtons method overflow depth counter");
                }
                Console.WriteLine(@"ResultX = {0:F6}", result);
                Console.WriteLine(@"ResultY = {0:F6}", exp.Сalculate(result));
                DrawBorder();

                // Save expression
                while (true)
                {
                    try
                    {
                        lMod = InputHandler(ref input, "Do you want save expression and conditions? (Y/N): ");
                        if (lMod != ConsoleKey.Enter) { gMod = KeyToMode(lMod); return; }
                        if (input == "Y" || input == "y") { gMod = Modes.saveMode; return; }
                        else if (input == "N" || input == "n") { break; }
                        else { continue; }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
        }


        static public void LoadHandle(ref Polynomial exp, ref Modes gMod, ref double leftBorder, ref double rightBorder, ref double epsilon, ref int depth)
        {
            String input = "";
            ConsoleKey lMod;

            while (true)
            {
                DrawLoadMenu(ref exp, ref leftBorder, ref rightBorder, ref epsilon, ref depth);

                // File path enter
                while (true)
                {
                    try
                    {
                        lMod = InputHandler(ref input, "File path: ");
                        if (lMod != ConsoleKey.Enter) { gMod = KeyToMode(lMod); return; }
                        if (!System.IO.File.Exists(input)) { throw new Exception("Not correct file path. Try again\n"); }
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err.Message);
                        continue;
                    }
                    break;
                }

                string[] fileData = System.IO.File.ReadAllLines(input);
                try
                {
                    // Read Polynomials from file
                    if (!PolynomialReader(fileData[0], ref exp))
                    {
                        Console.WriteLine("Not correct file data. Try again\n");
                        continue;
                    }

                    leftBorder = Convert.ToDouble(fileData[1]);
                    rightBorder = Convert.ToDouble(fileData[2]);
                    epsilon = Convert.ToDouble(fileData[3]);
                    depth = Convert.ToInt32(fileData[4]);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        static public bool PolynomialReader(String str, ref Polynomial exp)
        {
            int i = 0;
            bool negativeMonomFlag = false;
            if (str[i] == '-') { negativeMonomFlag = true; ++i; }

            exp.Clear();

            while (i < str.Length) {
                try
                {
                    Monomial monom = new Monomial();
                    String buff = "";

                    // Read mult
                    while (str[i] != '*')
                    {
                        buff += str[i];
                        ++i;
                    }

                    if (negativeMonomFlag) { monom.mult = -Convert.ToDouble(buff); }
                    else { monom.mult = Convert.ToDouble(buff); }
                    buff = "";
                    ++i;

                    if (str[i++] != 'x') { throw new Exception(); }
                    if (str[i++] != '^') { throw new Exception(); }

                    // Read pow
                    while (i!= str.Length && str[i] != '-' && str[i] != '+' && str[i] != '\n')
                    {
                        buff += str[i];
                        ++i;
                    }

                    // Sign check
                    if (i != str.Length)
                    {
                        if (str[i] == '-') { negativeMonomFlag = true; ++i; }
                        else if (str[i] == '+') { negativeMonomFlag = false; ++i; }
                        else if (str[i] == '\n') { break; }
                    }
                   
                    monom.pow = Convert.ToInt32(buff);
                    exp.Insert(monom);
                }
                catch (Exception)
                {
                    exp.Clear();
                    return false;
                }
            }

            return true;
        }


        static public void SaveHandle(ref Polynomial exp, ref Modes gMod, ref double leftBorder, ref double rightBorder, ref double epsilon, ref int depth, ref double result)
        {
            String input = "";
            ConsoleKey lMod;

            while (true)
            {
                DrawSaveMenu(ref exp);

                // File path enter
                while (true)
                {
                    try
                    {
                        lMod = InputHandler(ref input, "File path: ");
                        if (lMod != ConsoleKey.Enter) { gMod = KeyToMode(lMod); return; }
                        if (System.IO.File.Exists(input)) { throw new Exception("File alredy exist. Try again\n"); }
                        if (!MaskFit(input)) { throw new Exception("Not correct file path. Try again\n"); }
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err.Message);
                        continue;
                    }
                    break;
                }

                try
                {
                    using (System.IO.StreamWriter fileStream = new System.IO.StreamWriter(input))
                    {
                        fileStream.WriteLine(exp.ShowInStr(true));
                        fileStream.WriteLine(leftBorder);
                        fileStream.WriteLine(rightBorder);
                        fileStream.WriteLine(epsilon);
                        fileStream.WriteLine(depth);
                        if (!Double.IsNaN(result))
                        {
                            fileStream.WriteLine("=================================");
                            fileStream.WriteLine("ResultX: " + result);
                            fileStream.WriteLine("ResultY: " + exp.Сalculate(result));
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error while write to file. Try again\n");
                    continue;
                }


                Console.WriteLine("All data saved");
                Console.ReadKey(true);
            }
        }


        static public bool MaskFit(string filePath)
        {
            Regex mask = new Regex("((/./)?([cC][oO][nN]))|((/./)?([cC][oO][nN]\\.))|((/./)?([cC][oO][nN]\\.)(.*))");

            return !mask.IsMatch(filePath);
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
    }
}
