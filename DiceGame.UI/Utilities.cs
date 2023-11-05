using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.UI
{
    class Utilities
    {
        public static bool ValidInputToBool(string input)
        {
            if (input == "Y" || input == "YES")
                return true;
            return false;
        }
    }
}
