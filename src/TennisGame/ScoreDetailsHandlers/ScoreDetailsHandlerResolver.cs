using System;
using TennisGame.Models;
using TennisGame.Queries;
using TennisGame.ScoreDetailsHandlers.Interfaces;

namespace TennisGame.ScoreDetailsHandlers
{
    public class ScoreDetailsHandlerResolver : IScoreDetailsHandlerResolver
    {
        private readonly IGameManagerQuery _gameManagerQuery;
        private readonly IPointToScoreTermMapperFactory _pointToScoreTermMapperFactory;

        public ScoreDetailsHandlerResolver(
            IGameManagerQuery gameManagerQuery,
            IPointToScoreTermMapperFactory pointToScoreTermMapperFactory)
        {
            _gameManagerQuery = gameManagerQuery ?? throw new ArgumentNullException(nameof(gameManagerQuery));
            _pointToScoreTermMapperFactory = pointToScoreTermMapperFactory ??
                throw new ArgumentNullException(nameof(pointToScoreTermMapperFactory));
        }

        public IScoreDetailsHandler Resolve(Player playerOne, Player playerTwo)
        {
            if (_gameManagerQuery.IsEqualGame(playerOne, playerTwo))
            {
                return new EqualScoreDetailsHandler(_pointToScoreTermMapperFactory, playerOne.Points);
            }
            else if (_gameManagerQuery.IsDeuceGame(playerOne, playerTwo))
            {
                return new DeuceScoreDetailsHandler();
            }
            else if (_gameManagerQuery.IsAdvantageGame(playerOne, playerTwo))
            {
                return new AdvantageScoreDetailsHandler(playerOne, playerTwo);
            }
            else if (_gameManagerQuery.IsCompletedGame(playerOne, playerTwo))
            {
                return new GameCompletedScoreDetailsHandler(playerOne, playerTwo);
            }

            return new StandardScoreDetailsHandler(_pointToScoreTermMapperFactory, playerOne.Points, playerTwo.Points);            
        }
    }
}
