using TennisGame.Models;

namespace TennisGame.ScoreDetailsHandlers.Interfaces
{
    public interface IScoreDetailsHandlerResolver
    {
        IScoreDetailsHandler Resolve(Player playerOne, Player playerTwo);
    }
}