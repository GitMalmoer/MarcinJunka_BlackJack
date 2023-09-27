using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcinJunka_BlackJack.DTOs
{
    class GameMechanicsDTO
    {
        public string GameResult { get; set; } // the result is the saved outcome of methods: PlayerWin(), DealerWin() etc...
        public DateTime Date { get; set; }
        public int PlayerSaldo { get; set; }
    }
}
