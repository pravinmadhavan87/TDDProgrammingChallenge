using System;

namespace TennisGame.Models
{
    public class Player
    {
        public Player(string name)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; private set; }
        public int Points { get; set; }
    }
}
