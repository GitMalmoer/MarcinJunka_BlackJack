using Data_Access.Entities;
using Data_Acess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Acess
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<GameResolution> GameResolutions { get; set; }
        public DbSet<PlacedBet> PlacedBets { get; set; }
        public DbSet<Game> Games { get; set; }


        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
