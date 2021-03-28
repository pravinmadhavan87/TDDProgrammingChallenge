using System;
using System.IO;
using System.Text;
using AutoFixture;
using FileWriter;
using TennisGame.Commands;
using TennisGame.IntegrationTests.Resources;
using TennisGame.Models;
using TennisGame.Queries;
using TennisGame.ScoreDetailsHandlers;
using TennisGame.ScoreDetailsHandlers.Interfaces;
using TennisGame.Services;
using TennisGame.Services.Interfaces;
using Xunit;

namespace TennisGame.IntegrationTests
{
    public class TennisGameTests : IDisposable
    {
        private readonly Fixture _fixture;

        private readonly Player _playerOne;
        private readonly Player _playerTwo;

        private readonly IGameManagerCommand _gameManagerCommand;
        private readonly IScoreDetailsHandlerResolver _scoreDetailsHandlerResolver;
        private readonly IGameOutputGenerator _gameOutputGenerator;

        private readonly ITennisGameService _tennisGameSvc;

        private readonly IFileWriter _fileWriter; 

        public TennisGameTests()
        {
            _fixture = new Fixture();

            _playerOne = new Player("Player one");
            _playerTwo = new Player("Player two");

            _gameManagerCommand = new GameManagerCommand(_playerOne, _playerTwo);
            _scoreDetailsHandlerResolver = new ScoreDetailsHandlerResolver(new GameManagerQuery(), new PointToScoreTermMapperFactory());
            _gameOutputGenerator = new GameOutputGenerator();

            _tennisGameSvc = new TennisGameService(_gameManagerCommand, _scoreDetailsHandlerResolver, _gameOutputGenerator);
            _fileWriter = new FileWriter.FileWriter();
        }

        [Fact]
        public void ACompletedGame_ForOneSidedGame_GeneratesCorrectFileContent()
        {
            // Arrange
            var gameOutput = SeedOneSidedGame();

            // Act
            _fileWriter.WriteToFile(gameOutput.Filename, gameOutput.Content);

            // Assert
            var expectedBytes = Encoding.GetEncoding(65001).GetBytes(GameResults.onesided_game);
            var resultBytes = GetFileBytes();
            Assert.Equal(expectedBytes, resultBytes);
        }

        [Fact]
        public void ACompletedGame_ForDisputedGame_GeneratesCorrectFileContent()
        {
            // Arrange
            var gameOutput = SeedDisputedGame();

            // Act
            _fileWriter.WriteToFile(gameOutput.Filename, gameOutput.Content);

            // Assert
            var expectedBytes = Encoding.GetEncoding(65001).GetBytes(GameResults.disputed_game);
            var resultBytes = GetFileBytes();
            Assert.Equal(expectedBytes, resultBytes);
        }

        public void Dispose()
        {
            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), GameOutputGenerator.TennisGameScoresFilename));
        }

        private GameOutput SeedOneSidedGame()
        {
            for (var i = 0; i < 3; i++)
            {
                _tennisGameSvc.HandlePointWonBy(_playerOne.Name);
            }

            return _tennisGameSvc.HandlePointWonBy(_playerOne.Name); // Game Player one
        }

        private GameOutput SeedDisputedGame()
        {
            for (var i = 0; i < 4; i++)
            {
                _tennisGameSvc.HandlePointWonBy(_playerOne.Name);
                _tennisGameSvc.HandlePointWonBy(_playerTwo.Name);
            }

            _tennisGameSvc.HandlePointWonBy(_playerOne.Name); // Advantage Player one
            return _tennisGameSvc.HandlePointWonBy(_playerOne.Name); // Game Player one
        }

        private byte[] GetFileBytes()
        {
            var resultFile = Path.Combine(Directory.GetCurrentDirectory(), GameOutputGenerator.TennisGameScoresFilename);
            var resultBytes = File.ReadAllBytes(resultFile);
            return resultBytes;
        }
    }
}
