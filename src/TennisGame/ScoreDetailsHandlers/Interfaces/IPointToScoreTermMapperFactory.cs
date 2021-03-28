using System;
using System.Collections.Generic;
using TennisGame.Models.Enums;

namespace TennisGame.ScoreDetailsHandlers.Interfaces
{
    public interface IPointToScoreTermMapperFactory
    {
        Dictionary<int, ScoreTerm> GetPointToScoreTermMapper();
    }
}