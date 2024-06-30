using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.ConsoleUI
{
    internal class ConsolePlayer
    {
        int X;
        int Y;

        public ConsolePlayer()
        {
            X = 0;
            Y = 0;
        }

        public int[] GetLocation()
        { return new int[] { X, Y }; }

        public void UpdatePlayerLocation(int[] playerLocation)
        {
            X = playerLocation[0];
            Y = playerLocation[1];
        }

    }
}
