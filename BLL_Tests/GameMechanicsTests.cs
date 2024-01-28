using Business_Logic;
using Business_Logic.MODELS.Person;
using Data_Access;
using Data_Access.Entities;
using Data_Acess;
using Moq;
using Moq.Protected;
using System.Diagnostics;

namespace BLL_Tests
{
    [TestClass]
    public class GameMechanicsTests
    {
        private Mock<IPlayer> playerMock;
        private Mock<IPerson> dealerMock;
        private Mock<IAppDbContext> dbContext;
        private Mock<IGameRepository> gameRepositoryMock;

        public GameMechanicsTests()
        {
            playerMock = new Mock<IPlayer>();
            dealerMock = new Mock<IPerson>();
            dbContext = new Mock<IAppDbContext>();
            gameRepositoryMock = new Mock<IGameRepository>();
        }




        [TestMethod]
        public void CheckForWinner_PlayerBusts_DealerWins()
        {
            // arrange
            playerMock.Setup(s => s.CalculateScore()).Returns(18);
            dealerMock.Setup(s => s.CalculateScore()).Returns(21);
            // Create a partial mock of GameMechanics and configure it to call the original SaveGameResolutionToDb method.
            var gameMechanicsMock = new Mock<GameMechanics>(playerMock.Object, dealerMock.Object, gameRepositoryMock.Object,15) { CallBase = true };
            gameMechanicsMock.Setup(x => x.DealerWin()).Returns("Dealer win");
            //gameMechanicsMock.Protected().Setup("SaveGameResolutionToDb", ItExpr.IsAny<string>()).Verifiable();

            gameRepositoryMock.Setup(s => s.SaveGameResolutionToDb(It.IsAny<string>(),It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>())).Verifiable();



            // act
            string result = gameMechanicsMock.Object.CheckForWinner();

            // assert
            gameMechanicsMock.Verify(x => x.DealerWin(), Times.Once);

        }

        [TestMethod]
        public void WhenCheckForWinner_SaveToDbMethodIsCalled_ExactlyOnce()
        {
            // arrange
            playerMock.Setup(s => s.CalculateScore()).Returns(18);
            dealerMock.Setup(s => s.CalculateScore()).Returns(21);
            gameRepositoryMock.Setup(s => s.SaveGameResolutionToDb(It.IsAny<string>(), It.IsAny<int>(), 6, It.IsAny<int>())).Verifiable();


            // Create a partial mock of GameMechanics and configure it to call the original SaveGameResolutionToDb method.
            var gameMechanicsMock = new Mock<GameMechanics>(playerMock.Object, dealerMock.Object, gameRepositoryMock.Object,16) { CallBase = true };

            // act
            gameMechanicsMock.Object.CheckForWinner();

            // assert
            // Verify that the SaveGameResolutionToDb method called during the test.
            gameRepositoryMock.Verify(x=>x.SaveGameResolutionToDb(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        public void CheckForWinner_DealerBusts_PlayerWins()
        {
            // arrange
            playerMock.Setup(s => s.CalculateScore()).Returns(21);
            dealerMock.Setup(s => s.CalculateScore()).Returns(18);

            var test = playerMock.Object.CalculateScore();
            
            gameRepositoryMock.Setup(s => s.SaveGameResolutionToDb(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            // Create a partial mock of GameMechanics and configure it to call the original SaveGameResolutionToDb method.
            var gameMechanicsMock = new Mock<GameMechanics>(playerMock.Object, dealerMock.Object, gameRepositoryMock.Object,15) { CallBase = true };
            
            // act
            string result = gameMechanicsMock.Object.CheckForWinner();

            // assert
            gameMechanicsMock.Verify(x => x.PlayerWin(), Times.Once);

        }

        [TestMethod]
        public void CheckForWinner_TheSameScore_Draw()
        {
            // arrange
            playerMock.Setup(s => s.CalculateScore()).Returns(21);
            dealerMock.Setup(s => s.CalculateScore()).Returns(21);
            gameRepositoryMock.Setup(s => s.SaveGameResolutionToDb(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            // Create a partial mock of GameMechanics and configure it to call the original SaveGameResolutionToDb method.
            var gameMechanicsMock = new Mock<GameMechanics>(playerMock.Object, dealerMock.Object, gameRepositoryMock.Object,15) { CallBase = true };

            // act
            string result = gameMechanicsMock.Object.CheckForWinner();

            // assert
            gameMechanicsMock.Verify(x => x.Draw(), Times.Once);

        }

        [TestMethod]
        public void EventRaised_EventFlagIsRaised()
        {
            var gameMechanics = new GameMechanics(playerMock.Object,dealerMock.Object,gameRepositoryMock.Object,16);

            bool eventflag = false;

            gameMechanics.BetIsPlaced += (sender, args) =>
            {
                eventflag = true;
            };


            gameMechanics.Deal();

            Assert.IsTrue(eventflag);
        }

        [TestMethod]
        public void WhenThereAreNoSubscribers_NoEventIsRaised()
        {
            bool eventFlag = false;

            var gameMechanics = new GameMechanics(playerMock.Object, dealerMock.Object, gameRepositoryMock.Object, 16);

            EventHandler<EventArgs> eventHandler = (sender, e) =>
            {
                eventFlag = true;
            };

            gameMechanics.CardIsDrawn += eventHandler;

            // REMOVING SUBSCRIBERS
            gameMechanics.CardIsDrawn -= eventHandler;

            gameMechanics.OnCardIsDrawn();

            var gameMechanicsMock = new Mock<IGameMechanics>();


            Assert.IsFalse(eventFlag);


        }
    }
}