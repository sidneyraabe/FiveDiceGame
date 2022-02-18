using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using BetterYahtzee.BLL;

namespace BetterYahtzee.UI
{
    class ConsoleInput
    {
        public static string GetUserString()
        {
            string input;
            Console.SetCursorPosition(0, 26);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("   -Type the number(s) of all dice you wish to reroll, or select a letter to score: ");
            Console.ForegroundColor = ConsoleColor.Green;
            input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }
        public static string GetUserEnd()
        {
            string input;
            Console.SetCursorPosition(0, 26);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("   Do you wish to play again? (Y)es or (n)o: ");
            Console.ForegroundColor = ConsoleColor.Green;
            input = Console.ReadLine();
            input = input.ToUpper();
            Console.ResetColor();
            return input;
        }
        public static string MustScore()
        {
            string input;
            Console.SetCursorPosition(0, 26);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("   -You must select a letter to score: ");
            Console.ForegroundColor = ConsoleColor.Green;
            input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }

        public static int TryToConvertToInt(string input)
        {
            int.TryParse(input, out int output);
            return output;
        }

        public static int ConvertToInt(string input)
        {
            int output = int.Parse(input);           
                return output;            
        }
        public static bool DecidePlayAgain(string input)
        {
                if (input == "Y" || input == "YES")
                    return true;
                else
                    return false;
            
        }


    }
}
