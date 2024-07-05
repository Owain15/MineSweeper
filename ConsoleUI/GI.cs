using MineSweeper.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MineSweeper.ConsoleUI
{
    internal static class GI
    {
        #region Propaties

        private static int windowDimensionsX = 30;
        private static int windowDimensionsY = 30;

        public static int gamePositionX = 10;
        public static int gamePositionY = 5;


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

        public static void RenderStartScreen()
        {
            var screenX = gamePositionX - 4;
            var screenY = gamePositionY - 2;

            string screen =
@"╔══════════════════════════════╗
      ║         MINESWEEPER!         ║   
      ╚══════════════════════════════╝
      ╔══════════════════════════════╗
      ║             EACY             ║
      ║           MEDIEUM            ║
      ║             HARD             ║
      ║                              ║
      ║           CUSTOM             ║
      ║                              ║
      ║            QUIT              ║
      ╚══════════════════════════════╝


";

            Console.Clear();
            Console.SetCursorPosition(screenX, screenY);
            Console.WriteLine(screen);
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

        public static void RenderEndScreen(Minefield minefield, bool hasPlayerClearedField)
        {
            var promptText = "PRESS ANY KEY TO CONTINUE!";
            var promptX = GI.gamePositionX + +2 ;
            var promptY = GI.gamePositionY + minefield.field.GetLength(1)+2;
            var promptBoxWidth = minefield.field.GetLength(0);

            WrightTextCenterd(promptX, promptY, promptText,promptBoxWidth); 
        }

        public static void RenderScreen(Minefield field)
        {
            Console.Clear();

            RenderHeadderBox(field);

            DrawBorder((field.field.GetLength(0) + 2), (field.field.GetLength(1) + 2), -1,-1);
            
            RenderDisplay(field);

            RenderFooter(field);
        }

        public static void RenderHeadderBox(Minefield field)
        {
            var text = "MINESWEEPER!";

            var borderWidth = field.field.GetLength(0)+2;
            var borderHeight = 4;

            var borderWidthOffset = -1;
            var borderHeightOffset = -5;

            DrawBorder(borderWidth,borderHeight,borderWidthOffset,borderHeightOffset);
            
            WrightTextCenterd(gamePositionX,gamePositionY-5,text,field.field.GetLength(0));
        }
        public static void RenderFooter(Minefield field)
        {
            var borderWidth = field.field.GetLength(0) + 2;
            var borderHeight = 3;

            var borderWidthOffset = -1;
            var borderHeightOffset =field.field.GetLength(1)+1;

            DrawBorder(borderWidth, borderHeight, borderWidthOffset , borderHeightOffset);
        }


        internal static void HighlightPlayer(int[] playerLocation)
        {
            int x = playerLocation[0] + gamePositionX;
            int y = playerLocation[1] + gamePositionY;

            Console.SetCursorPosition(x, y);
            SetColor(highlightColor);
            Console.WriteLine(block);
            ResetColor();

        }

        internal static void RemoveHighlightPlayer(int[] playerLocation, Minefield mineField)
        {
            int x = playerLocation[0] + gamePositionX;
            int y = playerLocation[1] + gamePositionY;

            Console.SetCursorPosition(x, y);
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
            if (cell.hasCellBeenFlagged)
            {
                SetColor(printColor);
                Console.Write(flag);
                ResetColor();
            }
            else if (cell.IsCellHidden)
            {
                SetColor(printColor);
                Console.Write(block);
                ResetColor();
            }
            else if (cell.containsAMine)
            {
                SetColor(printColor);
                Console.Write(mine);
                ResetColor();
            }
            else if (cell.adjacentMines == 0)
            {
                SetColor(printColor);
                Console.Write(emptyBlock);
                ResetColor();

            }
            else if (cell.adjacentMines > 0)
            {
                SetColor(printColor);
                Console.Write(cell.adjacentMines);
                ResetColor();
            }

        }

        public static void WrightTextCenterd(int curserPositionX, int curserPositionY, string text, int xDimention)
        {
            Console.SetCursorPosition(curserPositionX + (xDimention / 2) - (text.Length / 2), curserPositionY);
            Console.WriteLine(text);

        }

        public static void DrawBorder(int width, int height)
        {
            char[,] border = new char[width, height];
            int[] currentChar = new int[2];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    currentChar[0] = x;
                    currentChar[1] = y;

                    if (x == 0 && y == 0)
                    {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY );
                        Console.Write('╔');
                    }
                    else if (x == width - 1 && y == 0)
                    {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY );
                        Console.Write('╗');
                    }
                    else if (x == 0 && y == height - 1)
                    {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY );
                        Console.Write('╚');
                    }
                    else if (x == width - 1 && y == height - 1)
                    {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY );
                        Console.Write('╝');
                    }
                    else if (x == 0 || x == width - 1)
                    {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY );
                        Console.Write('║');
                    }
                    else if (y == 0 || y == height - 1)
                    {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY );
                        Console.Write('═');
                    }

                }
            }
        }
        public static void DrawBorder(int width, int height, int hightOffset)
        {
            char[,] border = new char[width, height];
            int[] currentChar = new int[2];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    currentChar[0] = x;
                    currentChar[1] = y;

                    if (x == 0 && y == 0)
                    {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY + hightOffset);
                        Console.Write('╔');
                    }
                    else if (x == width - 1 && y == 0)
                    {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY + hightOffset);
                        Console.Write('╗');
                    }
                    else if (x == 0 && y == height - 1)
                    {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY + hightOffset);
                        Console.Write('╚');
                    }
                    else if (x == width - 1 && y == height - 1)
                    {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY + hightOffset);
                        Console.Write('╝');
                    }
                    else if (x == 0 || x == width - 1)
                    {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY + hightOffset);
                        Console.Write('║');
                    }
                    else if (y == 0 || y == height - 1)
                    {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY + hightOffset);
                        Console.Write('═');
                    }

                }
            }
        }
        public static void DrawBorder(int width, int height, int widthOffset, int hightOffset)
        {
            char[,] border = new char[width, height];
            int[] currentChar = new int[2];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    currentChar[0] = x;
                    currentChar[1] = y;

                    if (x == 0 && y == 0)
                    {
                        Console.SetCursorPosition(x + gamePositionX + widthOffset, y + gamePositionY + hightOffset);
                        Console.Write('╔');
                    }
                    else if (x == width - 1 && y == 0)
                    {
                        Console.SetCursorPosition(x + gamePositionX + widthOffset, y + gamePositionY + hightOffset);
                        Console.Write('╗');
                    }
                    else if (x == 0 && y == height - 1)
                    {
                        Console.SetCursorPosition(x + gamePositionX + widthOffset, y + gamePositionY + hightOffset);
                        Console.Write('╚');
                    }
                    else if (x == width - 1 && y == height - 1)
                    {
                        Console.SetCursorPosition(x + gamePositionX + widthOffset, y + gamePositionY + hightOffset);
                        Console.Write('╝');
                    }
                    else if (x == 0 || x == width - 1)
                    {
                        Console.SetCursorPosition(x + gamePositionX + widthOffset, y + gamePositionY + hightOffset);
                        Console.Write('║');
                    }
                    else if (y == 0 || y == height - 1)
                    {
                        Console.SetCursorPosition(x + gamePositionX + widthOffset, y + gamePositionY + hightOffset);
                        Console.Write('═');
                    }

                }
            }
        }


    }
}
