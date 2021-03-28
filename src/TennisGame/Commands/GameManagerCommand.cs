using System;
using TennisGame.Models;

namespace TennisGame.Commands
{
    public class GameManagerCommand : IGameManagerCommand
    {
        private Player _playerOne;
        private Player _playerTwo;

        public GameManagerCommand(Player playerOne, Player playerTwo)
        {
            _playerOne = playerOne ?? throw new ArgumentNullException(nameof(playerOne));
            _playerTwo = playerTwo ?? throw new ArgumentNullException(nameof(playerTwo));
        }

        public Tuple<Player, Player> PointWonBy(string playerName)
        {
            if (string.Equals(_playerOne.Name, _playerTwo.Name))
            {
                throw new InvalidOperationException("Player names must be unique.");
            }

            if (playerName == _playerOne.Name)
            {
                _playerOne.Points++;
            }
            else
            {
                _playerTwo.Points++;
            }

            return Tuple.Create(_playerOne, _playerTwo);
        }
    }
}
