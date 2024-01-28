using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Business_Logic.MODELS;
using Business_Logic.MODELS.Person;
using Data_Access;
using Data_Access.Entities;
using Data_Acess;
using Data_Acess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic
{
    public class GameMechanics : IGameMechanics
    {
        private readonly IGameRepository _gameRepository;
        private Game _game;
        private IPerson _dealer;
        private IPlayer _player;
        private GameDeck _gameDeck;
        private int _currentBet = 0;
        private PlacedBet _placedBet = new PlacedBet();
        private bool _isRoundInProgress = false;
        private bool _isPlayerStanding = false;
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<EventArgs> CardIsDrawn;
        public event EventHandler<BetPlacedEventArgs> BetIsPlaced;
        public event EventHandler<CheckForWinEventArgs> CheckForWinnerEventHandler;
        public int CurrentBet
        {
            get
            {
                return _currentBet;
            }
            set
            {
                if (_currentBet != value)
                {
                    _currentBet = value;
                    OnPropertyChanged(nameof(CurrentBet));
                }
            }
        }
        public GameMechanics(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
            NewGame();
        }

        public GameMechanics(IPlayer player, IPerson person, IGameRepository gameRepository, int currentBet)
        {
            _player = player;
            _dealer = person;
            _gameRepository = gameRepository;
            _currentBet = currentBet; 
            _game = new Game();
        }


        public void NewGame()
        {
            InitializeNewGameTracking();
            _dealer = new Dealer();
            _player = new Player();
            _gameDeck = new GameDeck();
            _gameDeck.GenerateDeck();
            _currentBet = 0;
            _isRoundInProgress = false;
        }

        public void InitializeNewGameTracking()
        {
            Game game = new Game();
            _gameRepository.GameInitialization(game);

            _game = game;
        }

        public bool CheckPlayerStanding()
        {
            return _isPlayerStanding;
        }

        public bool GetGameStatus()
        {
            return _isRoundInProgress;
        }

        public int GetPlayerSaldo()
        {
            return _player.Saldo;
        }

        public List<GameCard> GetPlayerHandCards()
        {
            return _player.Hand;
        }

        public int GetDealerScore()
        {
            return _dealer.CalculateScore();
        }

        public int GetPlayerScore()
        {
            return _player.CalculateScore();
        }

        public List<GameCard> GetDealerHandCards()
        {
            return _dealer.Hand;
        }

        public bool AddBet(int value)
        {
            if (value + CurrentBet > _player.Saldo)
            {
                return false;
            }
            CurrentBet += value;
            return true;
        }

        public bool Deal()
        {
            if (_currentBet <= 0)
            {
                return false;
            }
            _isRoundInProgress = true;

            OnBetIsPlaced(new BetPlacedEventArgs(_currentBet));

            _player.NewRound(_gameDeck);
            _dealer.NewRound(_gameDeck);
            OnCardIsDrawn();
            return true;
        }

        public string CheckForWinner()
        {
            int dealerScore = GetDealerScore();
            int playerScore = GetPlayerScore();

            if (playerScore > 21)
            {
                return DealerWin();

            }

            if (dealerScore > 21)
            {
                return PlayerWin();
            }

            if (playerScore == dealerScore)
            {
                return Draw();
            }

            if (playerScore > dealerScore)
            {
                return PlayerWin();
            }
            else
            {
                return DealerWin();
            }


        }

        public void Hit()
        {
            _player.DrawCard(_gameDeck);
            if (_player.CalculateScore() > 21)
            {
                _isPlayerStanding = true; // When game player loses we assign that player is standing. This makes that when OnCardIsDrawn event is fired in MainWindow.xaml.cs the dealers hidden card will be visible
                CheckForWinEventArgs winnerArgs = new CheckForWinEventArgs()
                {
                    currentSaldo = _player.Saldo,
                    winnerMessage = DealerWin(),
                };
                OnCardIsDrawn();
                OnCheckForWinnerEventHandler(winnerArgs);
            }
            OnCardIsDrawn();
        }

        public void Stand()
        {
            _isPlayerStanding = true;
            while (_dealer.CalculateScore() <= 16)
            {
                _dealer.DrawCard(_gameDeck);
            }

            OnCardIsDrawn();

            var winnerArgs = new CheckForWinEventArgs()
            {
                winnerMessage = CheckForWinner(),
                currentSaldo = _player.Saldo
            };
            OnCheckForWinnerEventHandler(winnerArgs);
        }

        public virtual string PlayerWin()
        {
            _player.Saldo += CurrentBet * 2;
            _isRoundInProgress = false;

            _gameRepository.SaveGameResolutionToDb("Player Win",_player.Saldo,_game.Id,_placedBet.Id);
            return "Player win";
        }

        public virtual string DealerWin()
        {
            _isRoundInProgress = false;

            _gameRepository.SaveGameResolutionToDb("Dealer win", _player.Saldo, _game.Id, _placedBet.Id);
            return "Dealer win";
        }

        public virtual string Draw()
        {
            _isRoundInProgress = false;
            _player.Saldo += _currentBet;
            _gameRepository.SaveGameResolutionToDb("Draw", _player.Saldo, _game.Id, _placedBet.Id);
            return "Draw!";
        }


        public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void OnCardIsDrawn()
        {
            CardIsDrawn?.Invoke(this, EventArgs.Empty);
        }


        public virtual void OnBetIsPlaced(BetPlacedEventArgs e)
        {
            PlacedBet bet = new PlacedBet()
            {
                BetAmount = _currentBet,
                DateTime = DateTime.Now
            };

            _game.PlacedBets.Add(bet);

            _player.Saldo -= bet.BetAmount;

            _gameRepository.AddBetToDb(bet);

            // field will now also have id because EF is tracking it
            _placedBet = bet;

            BetIsPlaced?.Invoke(this, e);
        }

        protected virtual void OnCheckForWinnerEventHandler(CheckForWinEventArgs e)
        {
            NextRound();
            CheckForWinnerEventHandler?.Invoke(this, e);
        }

        private void NextRound()
        {
            _isPlayerStanding = false;
            _currentBet = 0;
            _player.ResetHand();
            _dealer.ResetHand();
        }

      
    }
}
