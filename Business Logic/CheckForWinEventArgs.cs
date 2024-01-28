using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    public class CheckForWinEventArgs : EventArgs
    {
        public string winnerMessage { get; set; }
        public int currentSaldo { get; set; }
    }
}
