using System;
using TennisGame.Models;
using TennisGame.Models.Enums;
using TennisGame.ScoreDetailsHandlers.Interfaces;

namespace TennisGame.ScoreDetailsHandlers
{
    internal class EqualScoreDetailsHandler : IScoreDetailsHandler
    {
        private readonly IPointToScoreTermMapperFactory _pointToScoreTermMapperFactory;
        private readonly int _points;

        public EqualScoreDetailsHandler(IPointToScoreTermMapperFactory pointToScoreTermMapperFactory, int points)
        {
            _pointToScoreTermMapperFactory = pointToScoreTermMapperFactory ??
                throw new ArgumentNullException(nameof(pointToScoreTermMapperFactory));
            _points = points;
        }

        public ScoreDetails GetScoreDetails()
        {
            return new ScoreDetails
            {
                Text = $"{_pointToScoreTermMapperFactory.GetPointToScoreTermMapper()[_points]}-All",
                Status = GameStatus.Ongoing
            };
        }
    }
}