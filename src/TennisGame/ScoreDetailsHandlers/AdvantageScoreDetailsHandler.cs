using System;
using TennisGame.Models;
using TennisGame.Models.Enums;
using TennisGame.ScoreDetailsHandlers.Interfaces;

namespace TennisGame.ScoreDetailsHandlers
{
    internal class AdvantageScoreDetailsHandler : IScoreDetailsHandler
    {
        private readonly Player _playerOne;
        private readonly Player _playerTwo;

        public AdvantageScoreDetailsHandler(Player playerOne, Player playerTwo)
        {
            _playerOne = playerOne ?? throw new ArgumentNullException(nameof(playerOne));
            _playerTwo = playerTwo ?? throw new ArgumentNullException(nameof(playerTwo));
        }

        public ScoreDetails GetScoreDetails()
        {
            var pointDiff = _playerOne.Points - _playerTwo.Points;

            return new ScoreDetails
            {
                Text = $"Advantage {(pointDiff > 0 ? _playerOne.Name : _playerTwo.Name)}",
                Status = GameStatus.Ongoing
            };
        }
    }
}