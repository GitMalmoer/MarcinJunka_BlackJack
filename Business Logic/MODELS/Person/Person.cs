using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.MODELS.Person
{
    public interface IPerson
    {
        public List<GameCard> Hand { get; set; }
        public int CurrentScore { get; set; }
        public void ResetHand();
        public void DrawCard(GameDeck deck);
        public void NewRound(GameDeck deck);
        public int CalculateScore();
    }

    public class Person : IPerson
    {
        public List<GameCard> Hand { get; set; }
        public int CurrentScore { get; set; }

        public Person()
        {
            Hand = new List<GameCard>();
            CurrentScore = 0;
        }

        public void ResetHand()
        {
            Hand = new List<GameCard>();
        }

        public void DrawCard(GameDeck deck)
        {
            var card = deck.GetRandomCard();
            Hand.Add(card);
        }

        public void NewRound(GameDeck deck)
        {
            Hand.Add(deck.GetRandomCard());
            Hand.Add(deck.GetRandomCard());
        }


        public int CalculateScore()
        {
            CurrentScore = 0;
            foreach (var card in Hand)
            {
                // WHEN A CARD IS ACE CALCULATE DIFFERENT
                if (card.CardRank.Key == "1" && CurrentScore + 11 <= 21)
                {
                    CurrentScore += 11;
                }
                else
                    CurrentScore += card.CardRank.Value;
            }

            return CurrentScore;
        }
    }
}
