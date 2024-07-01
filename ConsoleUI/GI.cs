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

        public static void RenderMenu(string title, List<string> menuOptions)
        {
            var XDimention = GetLongestString(title, menuOptions) + 8;
            var YDimention = (menuOptions.Count() * 2) + 1;

            RenderTitleBox(title, XDimention);
            RenderOptionsBox(menuOptions, XDimention, YDimention, 3);
        }
        public static void RenderMenuHighlightCurrentChoice(int menuXDimention, int MenuYDimention,  List<string> menuOptions, int menuIndex)
        {
            var XDimention = GetLongestString(title, menuOptions) + 8;
            var YDimention = (menuOptions.Count() * 2) + 1;
            
            DrawBorder(xDimention, yDimention, yOfset);

            WrightOptions(menuOptions, xDimention, yOfset);
        } 

        private static void RenderTitleBox(string title, int xDimention)
        {
            DrawBorder(xDimention, 3,0);
            WrightTextCenterd(gamePositionX,gamePositionY+1, title,xDimention);

        }
        private static void DrawBorder(int width, int height,int hightOfset)
        {
            char[,] border = new char[width, height];
            int[] currentChar = new int[2];
 
            for (int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    currentChar[0] = x;
                    currentChar[1] = y;

                   if (currentChar[0] == 0 && currentChar[1] == 0) 
                   {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY+hightOfset);
                        Console.Write('╔');
                   }
                   else if (currentChar[0] == width-1 && currentChar[1] == 0)
                   {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY + hightOfset);
                        Console.Write('╗');
                   }
                   else if (currentChar[0] == 0 && currentChar[1] == height-1)
                   {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY + hightOfset);
                        Console.Write('╚');
                   }
                   else if (currentChar[0] == width - 1 && currentChar[1] == height - 1)
                   {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY + hightOfset);
                        Console.Write('╝');
                   }
                   else if(currentChar[0] == 0 || currentChar[0] == width-1)
                   {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY + hightOfset);
                        Console.Write('║');
                   }
                   else if (currentChar[1] == 0 || currentChar[1] == height-1)
                   {
                        Console.SetCursorPosition(x + gamePositionX, y + gamePositionY + hightOfset);
                        Console.Write('═');
                   }

                }
            }
              
        
        }
        private static void RenderOptionsBox(List<string> menuOptions, int xDimention, int yDimention, int yOfset)
        {
            DrawBorder(xDimention, yDimention,yOfset);

            WrightOptions(menuOptions,xDimention,yOfset );

           
        }
        private static void WrightOptions(List<string> menuOptions, int xDimention, int yOfset)
        {
             var optionCount = 0;

            foreach (string option in menuOptions)
            {
                var x = gamePositionX;
                var y = gamePositionY+yOfset+(optionCount * 2)+1;

                WrightTextCenterd(x,y,option,xDimention);

                optionCount++;
            
            }
        }


        private static void WrightTextCenterd(int curserPositionX, int curserPositionY,  string text, int xDimention)
        {
            Console.SetCursorPosition(curserPositionX + (xDimention / 2) - (text.Length / 2), curserPositionY);
            Console.WriteLine(text);

        }

        private static int GetLongestString(string title, List<string> menuOptions)
        {
            var listCopy = new List<string>();

            listCopy.Add(title);
            foreach (var item in menuOptions) { listCopy.Add(item); }

            return listCopy.Max(x => x.Length);
        }


    }
}
