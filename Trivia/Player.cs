using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    public class Player
    {
        const int MAX_POSITION = 12;

        public string Name { get; }

        public Purse Purse { get; } = new Purse();

        public int Position { get; private set; } = 0;

        public Player(string name)
        {
            Name = name;
        }

        public void MovePosition(int numberToMove)
        {
            Position += numberToMove;
            if(Position >= MAX_POSITION)
            {
                Position -= MAX_POSITION;
            }
        }
    }

    public class Purse
    {
        public int GoldCoins { get; private set; } = 0;

        public void AddGoldCoin(int coins = 1)
        {
            GoldCoins += coins;
        }
    }
}
