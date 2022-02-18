using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BetterYahtzee.BLL
{
    public class GameManager
    {
        private static int _one, _two, _three, _four, _five, _six, _threeKind, _fourKind, _fullHouse, _small, _large, _yahtzee, _chance, _bonus, _total;
        private static int _onecount, _twocount, _threecount, _fourcount, _fivecount, _sixcount;
        private static bool _hasOne, _hasTwo, _hasThree, _hasFour, _hasFive, _hasSix, _hasThreeKind, _hasFourKind, _hasFullHouse, _hasSmall, _hasLarge, _hasYahtzee, _hasChance, _canGetBonus;

        public static int[] currentDice = new int[5];


        public static int GetDice(int i)
        {
            return currentDice[i];
        }
        public void RollYahtzee()
        {
            Random roll = new Random();
            currentDice[0] = roll.Next(1, 7);
            currentDice[1] = currentDice[2] = currentDice[3] = currentDice[4] = currentDice[0];
        }
        
        public bool RollTheseDice(int diceSelection)
        {
            bool[] _selectRoll = { false, false, false, false, false };
            char[] _selectedDice = diceSelection.ToString().ToCharArray();


            for (int i = 0; i < _selectedDice.Length; i++)
            {
                int charNumber = Convert.ToInt32(_selectedDice[i]);
                charNumber -= 49;
                if (charNumber <= 4)
                    _selectRoll[charNumber] = true;
                else if (charNumber >= 5 || charNumber < 0)
                    return false;
            }
            

            Random roll = new Random();

            for (int i = 0; i < 5; i++)
            {
                if (_selectRoll[i] == true)
                {
                    currentDice[i] = roll.Next(1, 7);
                }
            }
            return true;
        }
        public bool IsValidDiceSelection(int diceSelection)
        {
            if (diceSelection <= 54321 && diceSelection >= 1)
                return true;
            return false;
        }
        public bool IsValidScoringSelection(string scoreSelection)
        {
            scoreSelection = scoreSelection.ToUpper();
            if (scoreSelection.Length == 1 && scoreSelection[0] >= 'A' && scoreSelection[0] <= 'M') // 13 selections for scoring
                return true;
            return false;
        }
        private int ReturnSumSelectedDice(int diceToCount)
        {
            int _specificDiceTotal = 0;

            for (int i = 0; i < 5; i++)
                if (currentDice[i] == diceToCount)
                {
                    _specificDiceTotal += diceToCount;
                }
            return _specificDiceTotal;
        }

        private int ReturnBonus()
        {
            if (_canGetBonus == false)
                return 0;
            _bonus += ReturnTotalForYahtzee();
            return _bonus;
        }

        private int ReturnSumAllDice()
        {
            int _allDiceTotal = 0;

            for (int i = 0; i < 5; i++)
            {
                _allDiceTotal += currentDice[i];
            }

            return _allDiceTotal;
        }

        private int ReturnTotalForYahtzee()
        {
            FindDiceCounts();
            if (_onecount == 5 || _twocount == 5 || _threecount == 5 || _fourcount == 5 || _fivecount == 5 || _sixcount == 5)
            {
                _canGetBonus = true;
                return 50;
            }
            return 0;
        }
        private int ReturnTotalForBlankOfKind(int number)
        {
            FindDiceCounts();
            if (_onecount >= number || _twocount >= number || _threecount >= number || _fourcount >= number || _fivecount >= number || _sixcount >= number)
                return ReturnSumAllDice();
            return 0;
        }

        private int ReturnTotalForFullHouse()
        {
            FindDiceCounts();
            if ((_onecount == 0 || _onecount == 2 || _onecount == 3) &&
                (_twocount == 0 || _twocount == 2 || _twocount == 3) &&
                (_threecount == 0 || _threecount == 2 || _threecount == 3) &&
                (_fourcount == 0 || _fourcount == 2 || _fourcount == 3) &&
                (_fivecount == 0 || _fivecount == 2 || _fivecount == 3) &&
                (_sixcount == 0 || _sixcount == 2 || _sixcount == 3)) // a yahtzee is not a full house            
                return 25;
            return 0;
        }

        private int ReturnTotalForSmall()
        {
            FindDiceCounts();
            if (_threecount >= 1 && _fourcount >= 1)
                if ((_onecount >= 1 && _twocount >= 1) ||
                    (_twocount >= 1 && _fivecount >= 1) ||
                    (_fivecount >= 1 && _sixcount >= 1))
                    return 30;
            return 0;
        }

        private int ReturnTotalForLarge()
        {
            FindDiceCounts();
            if ((_onecount == 1 && _twocount == 1 && _threecount == 1 && _fourcount == 1 && _fivecount == 1) ||
                (_twocount == 1 && _threecount == 1 && _fourcount == 1 && _fivecount == 1 && _sixcount == 1))
                return 40;
            return 0;
        }
        private void FindDiceCounts()
        {
            _onecount = 0;
            _twocount = 0;
            _threecount = 0;
            _fourcount = 0;
            _fivecount = 0;
            _sixcount = 0;

            for (int i = 0; i < 5; i++)
            {
                if (currentDice[i] == 1)
                    _onecount++;
                else if (currentDice[i] == 2)
                    _twocount++;
                else if (currentDice[i] == 3)
                    _threecount++;
                else if (currentDice[i] == 4)
                    _fourcount++;
                else if (currentDice[i] == 5)
                    _fivecount++;
                else
                    _sixcount++;
            }
        }
        public bool ScoreOrReturnFalse(string selection)
        {
            selection = selection.ToUpper();
            _bonus = ReturnBonus();
            if (selection[0] == 'A' && _hasOne == false)
            {
                _one = ReturnSumSelectedDice(1);
                _hasOne = true;
                return true;
            }

            else if (selection[0] == 'B' && _hasTwo == false)
            {
                _two = ReturnSumSelectedDice(2);
                _hasTwo = true;
                return true;
            }

            else if (selection[0] == 'C' && _hasThree == false)
            {
                _three = ReturnSumSelectedDice(3);
                _hasThree = true;
                return true;
            }
            else if (selection[0] == 'D' && _hasFour == false)
            {
                _four = ReturnSumSelectedDice(4);
                _hasFour = true;
                return true;
            }
            else if (selection[0] == 'E' && _hasFive == false)
            {
                _five = ReturnSumSelectedDice(5);
                _hasFive = true;
                return true;
            }
            else if (selection[0] == 'F' && _hasSix == false)
            {
                _six = ReturnSumSelectedDice(6);
                _hasSix = true;
                return true;
            }
            else if (selection[0] == 'G' && _hasThreeKind == false)
            {
                _threeKind = ReturnTotalForBlankOfKind(3);
                _hasThreeKind = true;
                return true;
            }
            else if (selection[0] == 'H' && _hasFourKind == false)
            {
                _fourKind = ReturnTotalForBlankOfKind(4);
                _hasFourKind = true;
                return true;
            }
            else if (selection[0] == 'I' && _hasFullHouse == false)
            {
                _fullHouse = ReturnTotalForFullHouse();
                _hasFullHouse = true;
                return true;
            }
            else if (selection[0] == 'J' && _hasSmall == false)
            {
                _small = ReturnTotalForSmall();
                _hasSmall = true;
                return true;
            }
            else if (selection[0] == 'K' && _hasLarge == false)
            {
                _large = ReturnTotalForLarge();
                _hasLarge = true;
                return true;
            }
            else if (selection[0] == 'L' && _hasYahtzee == false)
            {
                _yahtzee = ReturnTotalForYahtzee();
                _hasYahtzee = true;
                return true;
            }
            else if (selection[0] == 'M' && _hasChance == false)
            {
                _chance = ReturnSumAllDice();
                _hasChance = true;
                return true;
            }
            else
                return false;
        }
        public static bool ReturnInvalid()
        {
            return false;
        }
        public static bool ReturnValid()
        {
            return true;
        }
        public int Get(string selection)
        {
            int _topbonus = 0;
            int _tophalf = 0;

            if (_one != -1)
                _tophalf += _one;
            if (_two != -1)
                _tophalf += _two;
            if (_three != -1)
                _tophalf += _three;
            if (_four != -1)
                _tophalf += _four;
            if (_five != -1)
                _tophalf += _five;
            if (_six != -1)
                _tophalf += _six;

            if (_tophalf >= 63)
                _topbonus = 35;

            switch (selection)
            {
                case "one":
                    if (_hasOne == true)
                        return _one;
                    return -1;

                case "two":
                    if (_hasTwo == true)
                        return _two;
                    return -1;

                case "three":
                    if (_hasThree == true)
                        return _three;
                    return -1;

                case "four":
                    if (_hasFour == true)
                        return _four;
                    return -1;

                case "five":
                    if (_hasFive == true)
                        return _five;
                    return -1;

                case "six":
                    if (_hasSix == true)
                        return _six;
                    return -1;

                case "threeKind":
                    if (_hasThreeKind == true)
                        return _threeKind;
                    return -1;

                case "fourKind":
                    if (_hasFourKind == true)
                        return _fourKind;
                    return -1;

                case "fullHouse":
                    if (_hasFullHouse == true)
                        return _fullHouse;
                    return -1;

                case "small":
                    if (_hasSmall == true)
                        return _small;
                    return -1;

                case "large":
                    if (_hasLarge == true)
                        return _large;
                    return -1;

                case "yahtzee":
                    if (_hasYahtzee == true)
                        return _yahtzee;
                    return -1;

                case "chance":
                    if (_hasChance == true)
                        return _chance;
                    return -1;
                case "bonus":
                    return _bonus;
                case "total":
                    _total = 0;
                    _total += _tophalf;
                    if (_hasThreeKind == true)
                        _total += _threeKind;
                    if (_hasFourKind == true)
                        _total += _fourKind;
                    if (_hasFullHouse == true)
                        _total += _fullHouse;
                    if (_hasSmall == true)
                        _total += _small;
                    if (_hasLarge == true)
                        _total += _large;
                    if (_hasYahtzee == true)
                        _total += _yahtzee;
                    if (_hasChance == true)
                        _total += _chance;
                    _total += _bonus;
                    _total += _topbonus;
                    return _total;
                case "tophalf":      
                    return _tophalf;
                case "topbonus":
                    return _topbonus;
            }
            return 9999999;
        }

        public void StartGame()
        {
            _hasOne = _hasTwo = _hasThree = _hasFour = _hasFive =
            _hasSix = _hasThreeKind = _hasFourKind = _hasFullHouse =
            _hasSmall = _hasLarge = _hasYahtzee = _hasChance = false;

            _canGetBonus = false;

            _one = _two = _three = _four = _five = _six =
            _threeKind = _fourKind = _fullHouse = _small =
            _large = _yahtzee = _chance = _bonus = 0;
        }
    }

}

