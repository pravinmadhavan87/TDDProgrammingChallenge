using System.Collections.Generic;
using TennisGame.Commands;
using TennisGame.Models;
using TennisGame.Models.Enums;
using TennisGame.ScoreDetailsHandlers.Interfaces;
using TennisGame.Services.Interfaces;

namespace TennisGame.Services
{
    public class TennisGameService : ITennisGameService
    {
        private readonly IGameManagerCommand _gameManagerCommand;
        private readonly IScoreDetailsHandlerResolver _scoreDetailsHandlerResolver;
        private readonly IGameOutputGenerator _gameOutputGenerator;

        private ICollection<string> _scoreDetailsTexts = new List<string>();

        public TennisGameService(
            IGameManagerCommand gameManagerCommand,
            IScoreDetailsHandlerResolver scoreDetailsHandlerResolver,
            IGameOutputGenerator gameOutputGenerator)
        {
            _gameManagerCommand = gameManagerCommand;
            _scoreDetailsHandlerResolver = scoreDetailsHandlerResolver;
            _gameOutputGenerator = gameOutputGenerator;
        }

        /// <summary>
        /// Handles any actions that need to be taken as a resultof aplayer winning a point.
        /// </summary>
        /// <param name="playerName">Name of player who won point</param>
        /// <returns>The output of the tennis game. Null if the game is not complete.</returns>
        public GameOutput HandlePointWonBy(string playerName)
        {
            GameOutput gameOutput = null;

            var players = _gameManagerCommand.PointWonBy(playerName);
            var scoreDetails = _scoreDetailsHandlerResolver.Resolve(players.Item1, players.Item2).GetScoreDetails();
            _scoreDetailsTexts.Add(scoreDetails.Text);

            if (scoreDetails.Status == GameStatus.Complete)
            {
                gameOutput = _gameOutputGenerator.GenerateGameOutput(_scoreDetailsTexts, players.Item1.Name, players.Item2.Name);
            }

            return gameOutput;
        }
    }
}
