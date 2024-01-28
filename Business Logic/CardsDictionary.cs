using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    public static class CardsDictionary
    {
        public static Dictionary<string, int> CardRanks = new Dictionary<string, int>()
        {
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
            { "10", 10 },
            { "j", 10 },
            { "q", 10 },
            { "k", 10 }
        };

        public static Dictionary<string, int> CardSuits = new Dictionary<string, int>()
        {
            { "h", 1 }, // heart
            { "d", 2 }, // diamonds
            { "c", 3 }, // clubs
            { "s", 4 } // spades
        };
    }
}
