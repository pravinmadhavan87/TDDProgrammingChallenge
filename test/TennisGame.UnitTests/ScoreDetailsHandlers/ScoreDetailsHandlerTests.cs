using AutoFixture;
using TennisGame.Models;
using TennisGame.Models.Enums;
using TennisGame.Queries;
using TennisGame.ScoreDetailsHandlers;
using TennisGame.ScoreDetailsHandlers.Interfaces;
using Xunit;

namespace TennisGame.UnitTests.ScoreDetailsHandlers
{
    public class ScoreDetailsHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly IScoreDetailsHandlerResolver _scoreDetailsHandlerResolver;

        public ScoreDetailsHandlerTests()
        {
            _fixture = new Fixture();
            _scoreDetailsHandlerResolver = new ScoreDetailsHandlerResolver(
                new GameManagerQuery(),
                new PointToScoreTermMapperFactory());
        }

        [Theory]
        [InlineData(0, 0, "Love-All", GameStatus.Ongoing)]
        [InlineData(1, 1, "Fifteen-All", GameStatus.Ongoing)]
        [InlineData(2, 2, "Thirty-All", GameStatus.Ongoing)]
        [InlineData(3, 3, "Deuce", GameStatus.Ongoing)]
        [InlineData(4, 4, "Deuce", GameStatus.Ongoing)]
        [InlineData(1, 0, "Fifteen-Love", GameStatus.Ongoing)]
        [InlineData(0, 1, "Love-Fifteen", GameStatus.Ongoing)]
        [InlineData(2, 0, "Thirty-Love", GameStatus.Ongoing)]
        [InlineData(0, 2, "Love-Thirty", GameStatus.Ongoing)]
        [InlineData(3, 0, "Forty-Love", GameStatus.Ongoing)]
        [InlineData(0, 3, "Love-Forty", GameStatus.Ongoing)]
        [InlineData(4, 0, "Game {0}", GameStatus.Complete)] // "Game [playerOne.Name]"
        [InlineData(0, 4, "Game {1}", GameStatus.Complete)] // "Game [playerTwo.Name]"
        [InlineData(2, 1, "Thirty-Fifteen", GameStatus.Ongoing)]
        [InlineData(1, 2, "Fifteen-Thirty", GameStatus.Ongoing)]
        [InlineData(3, 1, "Forty-Fifteen", GameStatus.Ongoing)]
        [InlineData(1, 3, "Fifteen-Forty", GameStatus.Ongoing)]
        [InlineData(4, 1, "Game {0}", GameStatus.Complete)]
        [InlineData(1, 4, "Game {1}", GameStatus.Complete)]
        [InlineData(3, 2, "Forty-Thirty", GameStatus.Ongoing)]
        [InlineData(2, 3, "Thirty-Forty", GameStatus.Ongoing)]
        [InlineData(4, 2, "Game {0}", GameStatus.Complete)]
        [InlineData(2, 4, "Game {1}", GameStatus.Complete)]
        [InlineData(4, 3, "Advantage {0}", GameStatus.Ongoing)]
        [InlineData(3, 4, "Advantage {1}", GameStatus.Ongoing)]
        [InlineData(5, 4, "Advantage {0}", GameStatus.Ongoing)]
        [InlineData(4, 5, "Advantage {1}", GameStatus.Ongoing)]
        [InlineData(15, 14, "Advantage {0}", GameStatus.Ongoing)]
        [InlineData(14, 15, "Advantage {1}", GameStatus.Ongoing)]
        [InlineData(6, 4, "Game {0}", GameStatus.Complete)]
        [InlineData(4, 6, "Game {1}", GameStatus.Complete)]
        [InlineData(16, 14, "Game {0}", GameStatus.Complete)]
        [InlineData(14, 16, "Game {1}", GameStatus.Complete)]
        public void GetScoreDetails_ReturnsCorrectScoreDetails(
            int playerOnePoints,
            int playerTwoPoints,
            string expectedScore,
            GameStatus expectedStatus)
        {
            // Arrange
            var playerOne = new Player(_fixture.Create<string>()) { Points = playerOnePoints };
            var playerTwo = new Player(_fixture.Create<string>()) { Points = playerTwoPoints };

            var sut = _scoreDetailsHandlerResolver.Resolve(playerOne, playerTwo);

            // Act
            var scoreDetails = sut.GetScoreDetails();

            // Assert
            Assert.Equal(string.Format(expectedScore, playerOne.Name, playerTwo.Name), scoreDetails.Text);
            Assert.Equal(expectedStatus, scoreDetails.Status);
        }
    }
}
