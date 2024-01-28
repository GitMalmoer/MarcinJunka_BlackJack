using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.MODELS.Person
{
    public interface IPlayer
    {
        int Saldo { get; set; }
        List<GameCard> Hand { get; set; }
        int CurrentScore { get; set; }
        void ResetHand();
        void DrawCard(GameDeck deck);
        void NewRound(GameDeck deck);
        int CalculateScore();
    }

    public class Player : Person, IPlayer
    {
        public int Saldo { get; set; }
        public Player()
        {
            Saldo = 100;
        }

    }
}
