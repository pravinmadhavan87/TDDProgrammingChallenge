using System.Collections.Generic;
using TennisGame.Models;

namespace TennisGame.Services.Interfaces
{
    public interface IGameOutputGenerator
    {
        GameOutput GenerateGameOutput(ICollection<string> scores, string playerOneName, string playerTwoName);
    }
}