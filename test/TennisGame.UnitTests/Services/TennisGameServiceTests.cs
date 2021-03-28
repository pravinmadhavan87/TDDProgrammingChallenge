using System;
using System.Collections.Generic;
using AutoFixture;
using Moq;
using TennisGame.Commands;
using TennisGame.Models;
using TennisGame.Queries;
using TennisGame.ScoreDetailsHandlers;
using TennisGame.ScoreDetailsHandlers.Interfaces;
using TennisGame.Services;
using TennisGame.Services.Interfaces;
using Xunit;

namespace TennisGame.UnitTests.Services
{
    public class TennisGameServiceTests
    {
        private readonly Fixture _fixture;

        private readonly Mock<IGameManagerCommand> _gameManagerCommand;
        private readonly IScoreDetailsHandlerResolver _scoreDetailsHandlerResolver;
        private readonly IGameOutputGenerator _gameOutputGenerator;

        private readonly ITennisGameService _sut;

        public TennisGameServiceTests()
        {
            _fixture = new Fixture();

            _gameManagerCommand = new Mock<IGameManagerCommand>();
            _scoreDetailsHandlerResolver = new ScoreDetailsHandlerResolver(new GameManagerQuery(), new PointToScoreTermMapperFactory());
            _gameOutputGenerator = new GameOutputGenerator();

            _sut = new TennisGameService(_gameManagerCommand.Object, _scoreDetailsHandlerResolver, _gameOutputGenerator);
        }

        [Fact]
        public void HandlePointWonBy_WhenGameIsOngoing_DoesNotGenerateOutput()
        {
            // Arrange
            var playerOneName = _fixture.Create<string>();
            var playerTwoName = _fixture.Create<string>();

            _gameManagerCommand.Setup(gmc => gmc.PointWonBy(It.IsAny<string>())).Returns(
                Tuple.Create(
                    new Player(playerOneName) { Points = 1 },
                    new Player(playerTwoName) { Points = 3 }));

            // Act
            var gameOutput = _sut.HandlePointWonBy(_fixture.Create<string>());

            // Assert
            Assert.Null(gameOutput?.Filename);
            Assert.Null(gameOutput?.Content);
        }

        [Fact]
        public void HandlePointWonBy_WhenGameIsWon_DoesGenerateOutput()
        {
            // Arrange
            var playerOneName = _fixture.Create<string>();
            var playerTwoName = _fixture.Create<string>();

            _gameManagerCommand.Setup(gmc => gmc.PointWonBy(It.IsAny<string>())).Returns(
                Tuple.Create(
                    new Player(playerOneName) { Points = 1 },
                    new Player(playerTwoName) { Points = 4 }));

            // Act
            var gameOutput = _sut.HandlePointWonBy(_fixture.Create<string>());

            // Assert
            Assert.NotNull(gameOutput?.Filename);
            Assert.NotNull(gameOutput?.Content);
        }
    }
}
