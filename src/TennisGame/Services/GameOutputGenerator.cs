using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TennisGame.Models;
using TennisGame.Services.Interfaces;

namespace TennisGame.Services
{
    public class GameOutputGenerator : IGameOutputGenerator
    {
        public static readonly string TennisGameScoresFilename = "tennis-game-scores.txt";

        public GameOutput GenerateGameOutput(ICollection<string> scores, string playerOneName, string playerTwoName)
        {
            return new GameOutput()
            {
                Filename = TennisGameScoresFilename,
                Content = new StringBuilder()
                .AppendLine($"{playerOneName} vs {playerTwoName}")
                .AppendLine(string.Join(Environment.NewLine, scores))
                .ToString()
            };
        }
    }
}
