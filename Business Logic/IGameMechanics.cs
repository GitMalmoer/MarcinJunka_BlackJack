using System.ComponentModel;
using Business_Logic.MODELS;
using Data_Acess;

namespace Business_Logic;

public interface IGameMechanics : INotifyPropertyChanged
{
    int CurrentBet { get; set; }
    event PropertyChangedEventHandler? PropertyChanged;
    event EventHandler<EventArgs> CardIsDrawn;
    event EventHandler<BetPlacedEventArgs> BetIsPlaced;
    event EventHandler<CheckForWinEventArgs> CheckForWinnerEventHandler;
    void NewGame();
    void InitializeNewGameTracking();
    bool CheckPlayerStanding();
    bool GetGameStatus();
    int GetPlayerSaldo();
    List<GameCard> GetPlayerHandCards();
    int GetDealerScore();
    int GetPlayerScore();
    List<GameCard> GetDealerHandCards();
    bool AddBet(int value);
    bool Deal();
    string CheckForWinner();
    void Hit();
    void Stand();
    string PlayerWin();
    string DealerWin();
    string Draw();
}