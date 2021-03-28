using System;
using AutoFixture;
using TennisGame.Commands;
using TennisGame.Models;
using Xunit;

namespace TennisGame.UnitTests.Commands
{
    public class GameManagerCommandTests
    {
        private readonly Fixture _fixture;

        public GameManagerCommandTests()
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void PointWonBy_WhenPlayerOneWinsPoint_ReturnsExpectedPlayerState(bool swapWinnerAndLoser)
        {
            // Arrange
            var winningPlayerName = _fixture.Create<string>();
            var winningPlayerPoints = _fixture.Create<int>();

            var losingPlayerName = _fixture.Create<string>();
            var losingPlayerPoints = _fixture.Create<int>();

            var sut = new GameManagerCommand(
                new Player(swapWinnerAndLoser ? losingPlayerName : winningPlayerName) { Points = swapWinnerAndLoser ? losingPlayerPoints : winningPlayerPoints },
                new Player(swapWinnerAndLoser ? winningPlayerName : losingPlayerName) { Points = swapWinnerAndLoser ? winningPlayerPoints : losingPlayerPoints });

            // Act
            var players = sut.PointWonBy(winningPlayerName);

            // Assert
            Assert.Equal(losingPlayerPoints, swapWinnerAndLoser ? players.Item1.Points : players.Item2.Points);
            Assert.Equal(losingPlayerName, swapWinnerAndLoser ? players.Item1.Name : players.Item2.Name);

            Assert.Equal(winningPlayerPoints + 1, swapWinnerAndLoser ? players.Item2.Points : players.Item1.Points);
            Assert.Equal(winningPlayerName, swapWinnerAndLoser ? players.Item2.Name : players.Item1.Name);
        }

        [Fact]
        public void PointWonBy_WhenPlayersHaveSameName_ThrowsInvalidOperationException()
        {
            // Arrange
            var playerName = _fixture.Create<string>();

            var sut = new GameManagerCommand(
                new Player(playerName) { Points = _fixture.Create<int>() },
                new Player(playerName) { Points = _fixture.Create<int>() });

            // Act / Assert
            Assert.Throws<InvalidOperationException>(() => sut.PointWonBy(playerName));
        }
    }
}
