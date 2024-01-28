using System.Runtime.InteropServices.JavaScript;
using Data_Access;
using Data_Access.Entities;
using Data_Acess;
using Data_Acess.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DAL_Tests
{
    [TestFixture]
    public class DbTests
    {
        private IHost _host;
        private AppDbContext _db;

        [SetUp]
        public void Setup()
        {
            _host = new TestHostFixture().Host;
            _db = _host.Services.CreateScope().ServiceProvider.GetService<AppDbContext>();
        }

        [TearDown]
        public void Teardown()
        {
            // Clean up any data added to the database during the test
            _db.Database.EnsureDeleted();
        }



        private void SeedDb(AppDbContext db)
        {
            List<Game> games = new List<Game>()
            {
                new Game()
                {
                    Id = 1,
                },
                new Game()
                {
                    Id = 2,
                }
            };
            db.Games.AddRange(games);
            db.SaveChanges();

            List<GameResolution> gameResolutions = new List<GameResolution>()
            {
                new GameResolution()
                {
                    Id = 1,
                    PlacedBetId = 1,
                    Date = DateTime.Now,
                    GameId = 1,
                    PlayerSaldo = 16,
                    WinMessage = "Player win",
                },
                new GameResolution()
                {
                Id = 2,
                PlacedBetId = 2,
                Date = DateTime.Now,
                GameId = 1,
                PlayerSaldo = 226,
                WinMessage = "Dealer win",
            },
                new GameResolution()
                {
                    Id = 3,
                    PlacedBetId = 3,
                    Date = DateTime.Now,
                    GameId = 1,
                    PlayerSaldo = 1326,
                    WinMessage = "Draw",
                },
                new GameResolution()
                {
                    Id = 4,
                    PlacedBetId = 4,
                    Date = DateTime.Now,
                    GameId = 1,
                    PlayerSaldo = 166,
                    WinMessage = "Player win",
                }
            };
            db.GameResolutions.AddRange(gameResolutions);
            db.SaveChanges();



            List<PlacedBet> placedBets = new List<PlacedBet>()
            {
                new PlacedBet()
                {
                    Id = 1,
                    BetAmount = 1,
                    DateTime = DateTime.Now,
                    GameId = 1,
                },
                new PlacedBet()
                {
                    Id = 2,
                    GameId = 1,
                    BetAmount = 2,
                    DateTime = DateTime.Now,
                },
                new PlacedBet()
                {
                    Id = 3,
                    GameId = 2,
                    BetAmount = 3,
                    DateTime = DateTime.Now,
                },
                new PlacedBet()
                {
                    Id = 4,
                    GameId = 2,
                    BetAmount = 4,
                    DateTime = DateTime.Now,
                },
            };
            db.PlacedBets.AddRange(placedBets);
            db.SaveChanges();
        }

        [Test]
        public void Database_Seed_DatabaseHasValues()
        {
            using (var scope = _host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<AppDbContext>();
                SeedDb(db);

                Assert.IsNotEmpty(db.GameResolutions);
            }
        }


        [Test]
        public void Database_Addition_DatabaseCountIncreased()
        {
            using (var scope = _host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<AppDbContext>();
                SeedDb(db);
                var count = db.GameResolutions.Count();

                var model = new GameResolution()
                {
                    Id = 9999,
                    PlacedBetId = 3,
                    Date = DateTime.Now,
                    GameId = 1,
                    PlayerSaldo = 1326,
                    WinMessage = "Draw",
                };

                db.GameResolutions.Add(model);
                db.SaveChanges();

                var countAfterAddition = db.GameResolutions.Count();

                Assert.Less(count,countAfterAddition);
            }
        }

        [Test]
        public void SaveGameResolution_WhenPlayerWin_DbNotEmpty()
        {
            using (var scope = _host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<AppDbContext>();
                var repo = scope.ServiceProvider.GetService<IGameRepository>();
                repo.SaveGameResolutionToDb("PlayerWin",3,3,6);

                Assert.IsNotEmpty(db.GameResolutions);
            }
        }

        [Test]
        public void AddBet_WhenBetIsPlaced_DbNotEmpty()
        {
            using (var scope = _host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<AppDbContext>();
                var repo = scope.ServiceProvider.GetService<IGameRepository>();
                var model = new PlacedBet()
                {
                    Id = 3,
                    GameId = 3,
                    BetAmount = 3,
                    DateTime = DateTime.Now,
                };

                repo.AddBetToDb(model);

                Assert.IsNotEmpty(db.PlacedBets);
            }
        }

    }
}