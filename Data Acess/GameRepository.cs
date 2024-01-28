using Data_Access.Entities;
using Data_Acess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Acess;

namespace Data_Access
{
    public class GameRepository : IGameRepository
    {
        private readonly IAppDbContext _dbContext;

        public GameRepository(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void SaveGameResolutionToDb(string resolutionMessage,int playerSaldo,int gameId,int placedBetId)
        {
            _dbContext.GameResolutions.Add(new GameResolution()
            {
                Date = DateTime.Now,
                PlayerSaldo = playerSaldo,
                WinMessage = resolutionMessage,
                GameId = gameId,
                PlacedBetId = placedBetId
            });

            _dbContext.SaveChanges();
        }

        public virtual void AddBetToDb(PlacedBet bet)
        {
            _dbContext.PlacedBets.Add(bet);
            _dbContext.SaveChanges();
        }

        public void GameInitialization(Game game)
        {
            _dbContext.Add(game);
        }

    }
}
