using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Business_Logic;

namespace MarcinJunka_BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameMechanics GameMechanics;
        public MainWindow()
        {
            InitializeComponent();
            GameMechanics = new GameMechanics();
            DataContext = GameMechanics;
            InitalizeGUI();
        }

        private void InitalizeGUI()
        {
            this.Title = "Blackjack by Marcin Junka";
            txtSaldo.Text = GameMechanics.GetPlayerSaldo().ToString();
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void BoolMessage(bool check, string? trueMessage, string? falseMessage)
        {
            if (check && trueMessage != null)
            {
                MessageBox.Show(trueMessage);
            }
            else if (!check && falseMessage != null)
            {
                MessageBox.Show(falseMessage);
            }
        }

        private void btnBet5_Click(object sender, RoutedEventArgs e)
        {
            BoolMessage(GameMechanics.AddBet(5),null,"Not enough saldo!");
        }


        private void btnBet10_Click(object sender, RoutedEventArgs e)
        {
            BoolMessage(GameMechanics.AddBet(10), null, "Not enough saldo!");
        }

        private void btnBet20_Click(object sender, RoutedEventArgs e)
        {
            BoolMessage(GameMechanics.AddBet(20), null, "Not enough saldo!");
        }

        private void btnBet50_Click(object sender, RoutedEventArgs e)
        {
            BoolMessage(GameMechanics.AddBet(50), null, "Not enough saldo!");
        }

        private void btnBet100_Click(object sender, RoutedEventArgs e)
        {
            BoolMessage(GameMechanics.AddBet(100), null, "Not enough saldo!");
        }

        private void btnDeal_Click(object sender, RoutedEventArgs e)
        {
            GameMechanics.BetIsPlaced += GameMechanics_BetIsPlacedEvent;
            GameMechanics.CardIsDrawn += GameMechanics_CardIsDrawnEvent;

            if (GameMechanics.GetGameStatus())
            {
                MessageBox.Show("You can deal only once after placing a bet.");
                return;
            }

            bool isDealSucessed = GameMechanics.Deal();
            if (!isDealSucessed)
            {
                MessageBox.Show("You need to place a bet!");
                return;
            }
        }

        private void GameMechanics_BetIsPlacedEvent(object? sender, BetPlacedEventArgs e)
        {
            txtSaldo.Text = GameMechanics.GetPlayerSaldo().ToString();
            GameMechanics.BetIsPlaced -= GameMechanics_BetIsPlacedEvent;
        }

        private void AssignAllDealerImages()
        {
            List<GameCard> dealerCards = GameMechanics.GetDealerHandCards();
            List<Image> dealerImages = new List<Image>()
            {
                imgDealerCard1,
                imgDealerCard2,
                imgDealerCard3,
                imgDealerCard4,
                imgDealerCard5,
                imgDealerCard6,
                imgDealerCard7,
            };

            for (int i = 0; i < dealerCards.Count; i++)
            {
                dealerImages[i].Source = InitializeBitmapImage(dealerCards[i].ImgPath);
            }
        }
        /// <summary>
        /// This method assigns card images to dealer only at beginning when player has not clicked 'stand' button yet.
        /// </summary>
        private void AssignDealerImages()
        {
            List<GameCard> dealerCards = GameMechanics.GetDealerHandCards();
            List<Image> dealerImages = new List<Image>()
            {
                imgDealerCard1,
                imgDealerCard2,
            };

            dealerImages[0].Source = InitializeBitmapImage(GameDeck.UnknownCardPath);
            dealerImages[1].Source = InitializeBitmapImage(dealerCards[1].ImgPath);

            var card = GameMechanics.GetDealerHandCards()[1];
        }

        private int GetDealerScoreWhenCardIsHidden()
        {
            GameCard secondCard = GameMechanics.GetDealerHandCards()[1];
            return secondCard.CardRank.Value;
        }

        private void AssignPlayerImages()
        {
            List<GameCard> playerCards = GameMechanics.GetPlayerHandCards();
            List<Image> playerImages = new List<Image>()
            {
                imgPlayerCard1,
                imgPlayerCard2,
                imgPlayerCard3,
                imgPlayerCard4,
                imgPlayerCard5,
                imgPlayerCard6,
                imgPlayerCard7,
            };

            for (int i = 0; i < playerCards.Count ; i++)
            {
                playerImages[i].Source = InitializeBitmapImage(playerCards[i].ImgPath);
            }
        }

        private BitmapImage InitializeBitmapImage(string imgPath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imgPath);
            bitmap.EndInit();
            return bitmap;
        }

        private void btnHit_Click(object sender, RoutedEventArgs e)
        {
            if (!GameMechanics.GetGameStatus())
            {
                MessageBox.Show("You need to deal first");
                return;
            }

            GameMechanics.CardIsDrawn += GameMechanics_CardIsDrawnEvent;
            GameMechanics.CheckForWinnerEventHandler += GameMechanics_CheckForWinnerEvent;
            GameMechanics.Hit();
            // unsubscribe to event because otherwise it will check for winner few times in a row.
            GameMechanics.CheckForWinnerEventHandler -= GameMechanics_CheckForWinnerEvent;
        }

        private void GameMechanics_CardIsDrawnEvent(object? sender, EventArgs e)
        {
            if (!GameMechanics.CheckPlayerStanding())
            {
                AssignPlayerImages();
                AssignDealerImages();
                txtScorePlayer.Text = GameMechanics.GetPlayerScore().ToString();
                txtScoreDealer.Text = GetDealerScoreWhenCardIsHidden().ToString();
            }
            else
            {
                AssignPlayerImages();
                AssignAllDealerImages();
                txtScoreDealer.Text = GameMechanics.GetDealerScore().ToString();
                txtScorePlayer.Text = GameMechanics.GetPlayerScore().ToString();
            }

            GameMechanics.CardIsDrawn -= GameMechanics_CardIsDrawnEvent;
        }

        private void btnStand_Click(object sender, RoutedEventArgs e)
        {
            if (!GameMechanics.GetGameStatus())
            {
                MessageBox.Show("You need to deal first");
                return;
            }

            GameMechanics.CardIsDrawn += GameMechanics_CardIsDrawnEvent;
            GameMechanics.CheckForWinnerEventHandler += GameMechanics_CheckForWinnerEvent;
            GameMechanics.Stand();
        }

        private void GameMechanics_CheckForWinnerEvent(object? sender, CheckForWinEventArgs e)
        {
            MessageBox.Show(e.winnerMessage);
            txtSaldo.Text = e.currentSaldo.ToString();
            NextRound();
            GameMechanics.CheckForWinnerEventHandler -= GameMechanics_CheckForWinnerEvent;
        }


        private void NewGame()
        {
            GameMechanics.NewGame();
            ClearImages();
            ClearScore();
            // CLEAR SALDO
            txtSaldo.Text = GameMechanics.GetPlayerSaldo().ToString();
            txtBet.Text = GameMechanics.CurrentBet.ToString();
        }

        private void NextRound()
        {
            ClearImages();
            ClearScore();
            txtBet.Text = GameMechanics.CurrentBet.ToString();
        }
        private void ClearImages()
        {

            StackPanel stackPanelDealer = dealerImagesPanel;
            StackPanel stackPanelPlayer = playerImagesPanel;

            foreach (Image image in stackPanelDealer.Children)
            {
                image.Source = null;
            }

            foreach (Image image in stackPanelPlayer.Children)
            {
                image.Source = null;
            }
        }

        private void ClearScore()
        {
            txtScoreDealer.Text = "0";
            txtScorePlayer.Text = "0";
        }


    }
}
