using MineSweeper.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.ConsoleUI
{
    internal static class GI
    {
        #region Propaties

        private static int windowDimensionsX = 10;
        private static int windowDimensionsY = 10;

        private static int gamePositionX = 10;
        private static int gamePositionY = 5;

        private static ConsoleColor backgroundColor = ConsoleColor.DarkBlue;
        private static ConsoleColor printColor = ConsoleColor.Blue;
        private static ConsoleColor highlightColor = ConsoleColor.Cyan;


        #endregion

        public static void initializeConsoleWindow()
        {
            Console.CursorVisible = false;

            //Console.WindowHeight = windowDimensionsY;
            //Console.WindowWidth = windowDimensionsX;

        }

        public static void RenderScreen(Minefield minefield)
        {

            for (int x = 0; x < minefield.field.GetLength(0); x ++)
            {
                for (int y = 0; y < minefield.field.GetLength(1); y ++)
                {
                    Console.SetCursorPosition(x + gamePositionX, y + gamePositionY);
                    Console.WriteLine(minefield.field[x,y].adjacentMines);
                    
                }

            }

           
        }



    }
}
