using TennisGame.Models;

namespace TennisGame.Services.Interfaces
{
    public interface ITennisGameService
    {
        GameOutput HandlePointWonBy(string playerName);
    }
}