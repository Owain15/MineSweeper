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

        private static int windowDimensionsX = 30;
        private static int windowDimensionsY = 30;

        private static int gamePositionX = 10;
        private static int gamePositionY = 5;


        #endregion

        #region Colors

        private static ConsoleColor backgroundColor = ConsoleColor.DarkCyan;
        private static ConsoleColor printColor = ConsoleColor.Black;
        private static ConsoleColor highlightColor = ConsoleColor.Cyan;

        #endregion

        #region Symbols

        private static char flag = '¥';
        private static char block = '▓';
        private static char emptyBlock = ' ';
        private static char mine = 'Ø';

        #endregion


        public static void initializeConsoleWindow()
        {
            Console.Title = "MineSweeper";
            Console.CursorVisible = false;

            //Console.SetWindowSize(windowDimensionsX, windowDimensionsY);
            //Console.WindowHeight = windowDimensionsY;
            //Console.WindowWidth = windowDimensionsX;

        }

        public static void RenderDisplay(Minefield minefield)
        {

            for (int x = 0; x < minefield.field.GetLength(0); x++)
            {
                for (int y = 0; y < minefield.field.GetLength(1); y++)
                {
                    Console.SetCursorPosition(x + gamePositionX, y + gamePositionY);

                    RenderCell(minefield.field[x, y]);

                }

            }


        }

        internal static void HighlightPlayer(int[] playerLocation)
        {
            int x = playerLocation[0]+gamePositionX;
            int y = playerLocation[1]+gamePositionY;

            Console.SetCursorPosition(x,y);
            SetColor(highlightColor);
            Console.WriteLine(block);
            ResetColor();

        }

        internal static void RemoveHighlightPlayer(int[] playerLocation, Minefield mineField)
        {
            int x = playerLocation[0] + gamePositionX;
            int y = playerLocation[1] + gamePositionY;

            Console.SetCursorPosition(x,y);
            RenderCell(mineField.field[playerLocation[0], playerLocation[1]]);

        }



        private static void SetColor(ConsoleColor color)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = color;
        }

        private static void ResetColor()
        { Console.ResetColor(); }

        private static void RenderCell(Cell cell)
        {
            if (cell.IsCellHidden)
            {
                SetColor(printColor);
                Console.Write(block);
                ResetColor();
            }
            else if (cell.hasCellBeenFlagged)
            {
                SetColor(printColor);
                Console.Write(flag);
                ResetColor();
            }
            else if (cell.containsAMine)
            {
                SetColor(printColor);
                Console.Write(mine);
                ResetColor();
            }
            else if(cell.adjacentMines==0)
            {
                SetColor(printColor);
                Console.Write(emptyBlock);
                ResetColor();

            }
            else if(cell.adjacentMines > 0)
            {
                SetColor(printColor);
                Console.Write(cell.adjacentMines);
                ResetColor();
            }

        }


    }
}
