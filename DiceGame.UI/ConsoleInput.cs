using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using System.Transactions;
using DiceGame.BLL;
using static System.Formats.Asn1.AsnWriter;

namespace DiceGame.UI
{
    class ConsoleInput
    {
        public static string PromptDiceOrScoreSelection()
        {
            string input = InlinePrompt("-Type the number(s) of all dice you wish to reroll, or select a letter to score");
            return input;
        }
        public static string PromptReplay()
        {
            string input = InlinePrompt("Do you wish to play again? (Y)es or (n)o");
            return input;
        }
        public static string PromptOnlyScoreSelection()
        {
            string input = InlinePrompt("You must select a letter to score");
            return input;
        }

        public static string InlinePrompt(string prompt)
        {
            
            Console.SetCursorPosition(0, 26);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("   " + prompt + ": ");
            Console.ForegroundColor = ConsoleColor.Green;

            string input;
            input = Console.ReadLine();
            input = input.ToUpper();

            Console.ResetColor();
            return input;
        }
        public static bool IsValidDiceSelection(int diceSelection)
        {
            if (diceSelection <= 54321 && diceSelection >= 1)
                return true;
            return false;
        }
        public static bool IsValidScoringSelection(string scoreSelection)
        {
            if (scoreSelection.Length == 1 && scoreSelection[0] >= 'A' && scoreSelection[0] <= 'M') // 13 selections for scoring
                return true;
            return false;
        }
        public static bool IsValidBoolSelection(string boolSelection)
        {
            if (boolSelection == "Y" || boolSelection == "YES" || boolSelection == "N" || boolSelection == "NO")
                return true;
            return false;
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

    }
}
