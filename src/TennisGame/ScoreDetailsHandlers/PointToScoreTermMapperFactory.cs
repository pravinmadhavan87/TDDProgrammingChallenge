using System.Collections.Generic;
using TennisGame.Models.Enums;
using TennisGame.ScoreDetailsHandlers.Interfaces;

namespace TennisGame.ScoreDetailsHandlers
{
    public class PointToScoreTermMapperFactory : IPointToScoreTermMapperFactory
    {
        private readonly Dictionary<int, ScoreTerm> _standardScoreMapper = new Dictionary<int, ScoreTerm>()
        {
            { 0, ScoreTerm.Love },
            { 1, ScoreTerm.Fifteen },
            { 2, ScoreTerm.Thirty },
            { 3, ScoreTerm.Forty }
        };

        public Dictionary<int, ScoreTerm> GetPointToScoreTermMapper()
        {
            return _standardScoreMapper;
        }
    }
}
