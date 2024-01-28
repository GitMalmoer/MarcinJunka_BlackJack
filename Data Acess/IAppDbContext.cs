using Data_Access.Entities;
using Data_Acess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data_Acess;

public interface IAppDbContext
{
    DbSet<GameResolution> GameResolutions { get; set; }
    DbSet<PlacedBet> PlacedBets { get; set; }
    DbSet<Game> Games { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
    EntityEntry Add(object entity);
    void RemoveRange(params object[] entities);
}