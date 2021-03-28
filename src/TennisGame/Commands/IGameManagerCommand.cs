using System;
using TennisGame.Models;

namespace TennisGame.Commands
{
    public interface IGameManagerCommand
    {
        Tuple<Player, Player> PointWonBy(string playerName);
    }
}