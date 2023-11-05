using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using DiceGame.BLL;
using DiceGame.UI;

namespace DiceGame.UI
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

                for (int turns = 13; turns > 0; turns--)
                {
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
                                diceinput = ConsoleInput.PromptOnlyScoreSelection();
                                if (ConsoleInput.IsValidScoringSelection(diceinput) && game.ScoreOrReturnFalse(diceinput) == true)
                                    break;
                                ConsoleOutput.InvalidSelection();
                            }
                            break;
                        }
                        bool scored = false;

                        while (true)
                        {
                            ConsoleOutput.ClearInputLine();
                            diceinput = ConsoleInput.PromptDiceOrScoreSelection();
                            int output = ConsoleInput.TryToConvertToInt(diceinput);
                            if (ConsoleInput.IsValidScoringSelection(diceinput) && game.ScoreOrReturnFalse(diceinput) == true)
                            {
                                scored = true;
                                break;
                            }
                            else if (ConsoleInput.IsValidDiceSelection(output))
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

                // final screen
                ConsoleOutput.DisplayScoreSheet();
                ConsoleOutput.DisplayFinalScore();

                
                // play again?
                string input = ConsoleInput.PromptReplay();
                while (!ConsoleInput.IsValidBoolSelection(input))
                {
                    ConsoleOutput.InvalidSelection();
                    input = ConsoleInput.PromptReplay(); 
                }
                bool playAgain = Utilities.ValidInputToBool(input);
                if (!playAgain)
                    break;
            }
        }
    }
}
