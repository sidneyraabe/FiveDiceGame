using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using BetterYahtzee.BLL;

namespace BetterYahtzee.UI
{
    public class GameFlow
    {
        public void PlayGame()
        {
            // Start Screen
            GameManager game = new GameManager();
            ConsoleOutput.DisplayTitle();

            // Main Loop
            while (true)
            {
                game.StartGame();
                bool end = false;
            
                for (int i = 13; i > 0; i--)
                {
                    ConsoleOutput.DisplayScoreText();
                    ConsoleOutput.DisplayScoreSheet();
                    game.RollTheseDice(12345);
                    int roundsLeft = 2;

                    while (true)
                    {
                        ConsoleOutput.ShowTurnsLeft(roundsLeft);
                        ConsoleOutput.DisplayBetterDice();
                        string diceinput;
                        if (roundsLeft == 0)
                        {
                            while (true)
                            {
                                ConsoleOutput.ClearInputLine();
                                diceinput = ConsoleInput.MustScore();
                                if (game.IsValidScoringSelection(diceinput) && game.ScoreOrReturnFalse(diceinput) == true)
                                    break;
                                ConsoleOutput.InvalidSelection();
                            }
                            break;
                        }
                        bool scored = false;

                        while (true)
                        {
                            ConsoleOutput.ClearInputLine();
                            diceinput = ConsoleInput.GetUserString();
                            int output = ConsoleInput.TryToConvertToInt(diceinput);
                            if (game.IsValidScoringSelection(diceinput) && game.ScoreOrReturnFalse(diceinput) == true)
                            {
                                scored = true;
                                break;
                            }
                            else if (game.IsValidDiceSelection(output))
                            {
                                int dice = ConsoleInput.ConvertToInt(diceinput);
                                if (game.RollTheseDice(dice) == true)
                                {
                                    roundsLeft--;
                                    break;
                                }
                            }
                            ConsoleOutput.InvalidSelection();
                        }
                        if (scored == true)
                            break;
                    }
                }
                ConsoleOutput.DisplayScoreText();
                ConsoleOutput.DisplayScoreSheet();
                ConsoleOutput.FinalScore();
                string input;
                while (true)
                {
                    string inputinner = ConsoleInput.GetUserEnd();
                    if (inputinner == "Y" || inputinner == "YES" || inputinner == "N" || inputinner == "NO")
                    {
                        input = inputinner;
                        break;
                    }
                    ConsoleOutput.InvalidSelection();
                }
                if (ConsoleInput.DecidePlayAgain(input) == false)
                    end = true;
                if (end == true)
                    break;
            }
        }
    }
}
