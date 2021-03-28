using System.Linq;
using AutoFixture;
using TennisGame.Models;
using TennisGame.Queries;
using Xunit;

namespace TennisGame.UnitTests.Queries
{
    public class GameManagerQueryTests
    {
        private readonly Fixture _fixture;
        private readonly IGameManagerQuery _sut;

        public GameManagerQueryTests()
        {
            _fixture = new Fixture();
            _sut = new GameManagerQuery();
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(true, true)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void IsEqualGame_ReturnsExpectedResult(bool expectedResult, bool swapPlayers)
        {
            // Arrange;
            var playerPoints = _fixture.Create<Generator<int>>().Where(i => i < 3).Take(1).SingleOrDefault();

            var playerOne = new Player(_fixture.Create<string>()) { Points = playerPoints };
            var playerTwo = new Player(_fixture.Create<string>())
            {
                Points = playerPoints + (expectedResult ? 0 : _fixture.Create<int>())
            };

            // Act
            var actualResult = _sut.IsEqualGame(
                swapPlayers ? playerTwo : playerOne,
                swapPlayers ? playerOne : playerTwo);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(true, true)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void IsDeuceGame_ReturnsExpectedResult(bool expectedResult, bool swapPlayers)
        {
            // Arrange;
            var playerPoints = _fixture.Create<Generator<int>>().Where(i => i >= 3).Take(1).SingleOrDefault();

            var playerOne = new Player(_fixture.Create<string>()) { Points = playerPoints };
            var playerTwo = new Player(_fixture.Create<string>())
            {
                Points = playerPoints + (expectedResult ? 0 : _fixture.Create<int>())
            };

            // Act
            var actualResult = _sut.IsDeuceGame(
                swapPlayers ? playerTwo : playerOne,
                swapPlayers ? playerOne : playerTwo);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(true, true)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void IsCompletedGame_ReturnsExpectedResult(bool expectedResult, bool swapPlayers)
        {
            // Arrange
            var playerPoints = _fixture.Create<int>();

            var playerOne = new Player(_fixture.Create<string>()) { Points = playerPoints };
            var playerTwo = new Player(_fixture.Create<string>())
            {
                Points = playerPoints + (expectedResult ? 2 : 0)
            };

            // Act
            var actualResult = _sut.IsCompletedGame(
                swapPlayers ? playerTwo : playerOne,
                swapPlayers ? playerOne : playerTwo);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(true, true)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void IsAdvantageGame_ReturnsExpectedResult(bool expectedResult, bool swapPlayers)
        {
            // Arrange
            var playerPoints = _fixture.Create<int>();

            var playerOne = new Player(_fixture.Create<string>()) { Points = playerPoints };
            var playerTwo = new Player(_fixture.Create<string>())
            {
                Points = playerPoints + (expectedResult ? 1 : 0)
            };

            // Act
            var actual = _sut.IsAdvantageGame(
                swapPlayers ? playerTwo : playerOne,
                swapPlayers ? playerOne : playerTwo);

            // Assert
            Assert.Equal(expectedResult, actual);
        }
    }
}
