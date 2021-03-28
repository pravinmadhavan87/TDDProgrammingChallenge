using TennisGame.Models;
using TennisGame.Models.Enums;
using TennisGame.ScoreDetailsHandlers.Interfaces;

namespace TennisGame.ScoreDetailsHandlers
{
    internal class DeuceScoreDetailsHandler : IScoreDetailsHandler
    {        
        public ScoreDetails GetScoreDetails()
        {
            return new ScoreDetails
            {
                Text = "Deuce",
                Status = GameStatus.Ongoing
            };
        }
    }
}