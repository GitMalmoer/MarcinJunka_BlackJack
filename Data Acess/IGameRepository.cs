using Data_Access.Entities;

namespace Data_Access
{
    public interface IGameRepository
    {
        void SaveGameResolutionToDb(string resolutionMessage, int playerSaldo, int gameId, int placedBetId);
        void AddBetToDb(PlacedBet bet);
        void GameInitialization(Game game);
    }
}
