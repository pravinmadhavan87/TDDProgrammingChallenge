using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using TennisGame.Services;
using TennisGame.Services.Interfaces;
using Xunit;

namespace TennisGame.UnitTests.Services
{
    public class GameOutputGeneratorTests
    {
        private readonly Fixture _fixture;
        private readonly IGameOutputGenerator _sut;

        public GameOutputGeneratorTests()
        {
            _fixture = new Fixture();
            _sut = new GameOutputGenerator();
        }

        [Fact]
        public void GenerateGameOutput_GeneratesGameOutputWithCorrectFilename()
        {
            // Act
            var gameOutput = _sut.GenerateGameOutput(
                _fixture.Create<ICollection<string>>(),
                _fixture.Create<string>(),
                _fixture.Create<string>());

            // Assert
            Assert.Equal("tennis-game-scores.txt", gameOutput.Filename);
        }
    }
}
