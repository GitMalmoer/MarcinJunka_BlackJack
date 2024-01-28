using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.MODELS
{
    public class GameDeck
    {
        public List<GameCard> Deck { get; }
        public static string UnknownCardPath = string.Empty;
        public GameDeck()
        {
            Deck = new List<GameCard>();
            GenerateDeck();
        }
        public void GenerateDeck()
        {
            var initDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            var resourceDirectory = "Business Logic\\Resources\\CardGUI";
            var mainDirectory = Path.Combine(initDirectory, resourceDirectory);

            if (!Path.Exists(mainDirectory))
            {
                throw new ApplicationException("PATH TO MAIN CARD-IMAGES DIRECTORY NOT FOUND!");
            }

            FileInfo[] fileList = new DirectoryInfo(mainDirectory).GetFiles();

            // GENERATING UNKNOWN CARD
            var unknownCard = fileList.FirstOrDefault(f => f.Name.ToLower().Contains("b2fv"));
            UnknownCardPath = unknownCard.FullName;

            // GENERATING ACTUAL DECK
            foreach (var cardSuit in CardsDictionary.CardSuits)
            {
                foreach (var cardRank in CardsDictionary.CardRanks)
                {
                    GameCard gc = new GameCard(cardSuit, cardRank);

                    string combinedCardName = string.Concat(gc.CardSuit.Key, gc.CardRank.Key);

                    var file = fileList.FirstOrDefault(f => f.Name.ToLower().Contains(combinedCardName.ToLower()));

                    gc.ImgPath = file.FullName != null ? file.FullName : "Image not found";
                    Deck.Add(gc);
                }
            }
            ShuffleDeck();
        }

        public void ShuffleDeck()
        {
            int lastIndex = Deck.Count - 1;

            while (lastIndex > 1)
            {
                int randomIndex = new Random().Next(1, lastIndex + 1);
                var tempCard = Deck[lastIndex];
                Deck[lastIndex] = Deck[randomIndex];
                Deck[randomIndex] = tempCard;
                lastIndex--;
            }
        }

        public GameCard GetRandomCard()
        {
            Random rnd = new Random();
            GameCard gc = Deck[rnd.Next(Deck.Count)];
            Deck.Remove(gc);
            return gc;
        }
    }
}
