using System;
using TennisGame.Models;

namespace TennisGame.Queries
{
    public class GameManagerQuery : IGameManagerQuery
    {
        public bool IsEqualGame(Player p1, Player p2) =>
            p1.Points == p2.Points && p1.Points < 3;

        public bool IsDeuceGame(Player p1, Player p2) =>
            p1.Points == p2.Points && p1.Points >= 3;

        public bool IsCompletedGame(Player p1, Player p2) =>
            (p1.Points >= 4 || p2.Points >= 4) && Math.Abs(p1.Points - p2.Points) >= 2;

        public bool IsAdvantageGame(Player p1, Player p2) =>
            (p1.Points >= 4 || p2.Points >= 4) && Math.Abs(p1.Points - p2.Points) == 1;
    }
}
