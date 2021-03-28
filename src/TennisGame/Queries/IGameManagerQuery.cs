using TennisGame.Models;

namespace TennisGame.Queries
{
    public interface IGameManagerQuery
    {
        bool IsEqualGame(Player p1, Player p2);
        bool IsDeuceGame(Player p1, Player p2);
        bool IsCompletedGame(Player p1, Player p2);
        bool IsAdvantageGame(Player p1, Player p2);
    }
}