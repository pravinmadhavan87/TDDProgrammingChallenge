using System;
using TennisGame.Models;
using TennisGame.Models.Enums;
using TennisGame.ScoreDetailsHandlers.Interfaces;

namespace TennisGame.ScoreDetailsHandlers
{
    internal class StandardScoreDetailsHandler : IScoreDetailsHandler
    {
        private readonly IPointToScoreTermMapperFactory _pointToScoreTermMapperFactory;
        private readonly int _playerOnePoints;
        private readonly int _playerTwoPoints;

        public StandardScoreDetailsHandler(
            IPointToScoreTermMapperFactory pointToScoreTermMapperFactory,
            int playerOnePoints,
            int playerTwoPoints)
        {
            _pointToScoreTermMapperFactory = pointToScoreTermMapperFactory ??
                throw new ArgumentNullException(nameof(pointToScoreTermMapperFactory));

            _playerOnePoints = playerOnePoints;
            _playerTwoPoints = playerTwoPoints;
        }

        public ScoreDetails GetScoreDetails()
        {
            var scoreDetails = new ScoreDetails { Text = string.Empty, Status = GameStatus.Ongoing };
            var mapper = _pointToScoreTermMapperFactory.GetPointToScoreTermMapper();

            if (mapper.ContainsKey(_playerOnePoints) && mapper.ContainsKey(_playerTwoPoints))
            {
                scoreDetails.Text = $"{mapper[_playerOnePoints]}-{mapper[_playerTwoPoints]}";
            }

            return scoreDetails;
        }
    }
}