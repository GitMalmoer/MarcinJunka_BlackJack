using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    public class BetPlacedEventArgs : EventArgs
    {
        public int BetAmount { get; }

        public BetPlacedEventArgs(int betAmount)
        {
            BetAmount = betAmount;
        }

    }
}
