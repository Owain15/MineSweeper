using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.ConsoleUI;

internal class ConsoleMenu
{
    private static int GamePositionX;
    private  static int GamePositionY;

    int DisplayOffsetX;
    int DisplayOffsetY;

    public int DisplayHeight;
    public int DisplayWidth;

    public string Title;
    public List<string> MenuOptions;

   

    public ConsoleMenu(int gamePositionX, int gamePositionY, string title, List<string> menuOptions )
    {
        Title = title;
        MenuOptions = menuOptions;

        GamePositionX = gamePositionX;
        GamePositionY = gamePositionY;
       

        var titleBoxHeight = 3;
        var menuOptionsOffset = 1;

        DisplayHeight = (menuOptions.Count() * 2) + menuOptionsOffset + titleBoxHeight;
        DisplayWidth = GetLongestString(title, menuOptions) + 8;

        
        
    }


    public void RenderCurrentMenu(int menuIndex)
    {
        Console.Clear();

        RenderTitleBox(Title,DisplayWidth);
        RenderOptionsBox(MenuOptions);
        HighlightMenuIndex(MenuOptions, menuIndex,DisplayWidth);

    }

   
    
    private void RenderOptionsBox(List<string> menuOptions)
    {
        DrawBorder(DisplayWidth, DisplayHeight-3, 3 );
        WrightOptions(menuOptions, DisplayWidth,3);

    }
    private static void RenderTitleBox(string title,int DisplayWidth)
    {
        DrawBorder(DisplayWidth,3, 0);
        WrightTextCenterd(GamePositionX, GamePositionY + 1, title,DisplayWidth);

    }
    private static void DrawBorder(int width, int height, int hightOfset)
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
                    Console.SetCursorPosition(x + GamePositionX, y + GamePositionY + hightOfset);
                    Console.Write('╔');
                }
                else if (x == width-1 && y == 0)
                {
                    Console.SetCursorPosition(x + GamePositionX, y + GamePositionY + hightOfset);
                    Console.Write('╗');
                }
                else if (x == 0 && y == height - 1)
                {
                    Console.SetCursorPosition(x + GamePositionX, y + GamePositionY + hightOfset);
                    Console.Write('╚');
                }
                else if (x == width - 1 && y == height - 1)
                {
                    Console.SetCursorPosition(x + GamePositionX, y + GamePositionY + hightOfset);
                    Console.Write('╝');
                }
                else if (x == 0 || x == width - 1)
                {
                    Console.SetCursorPosition(x + GamePositionX, y + GamePositionY + hightOfset);
                    Console.Write('║');
                }
                else if (y == 0 || y == height - 1)
                {
                    Console.SetCursorPosition(x + GamePositionX, y + GamePositionY + hightOfset);
                    Console.Write('═');
                }

            }
        }
    }
    private static void HighlightMenuIndex(List<string> menuOptions, int menuIndex,int displayWidth)
    {

        var yOfset = 3;

        var highlightedOption = $"> {menuOptions[menuIndex]} <";

        foreach (string option in menuOptions)
        {
            var x = GamePositionX;
            var y = GamePositionY + yOfset + (menuIndex * 2) + 1;

            WrightTextCenterd(x, y, highlightedOption, displayWidth);

            

        }
    }
    private static void WrightOptions(List<string> menuOptions, int xDimention, int yOfset)
{
    var optionCount = 0;

    foreach (string option in menuOptions)
    {
        var x = GamePositionX;
        var y = GamePositionY + yOfset + (optionCount * 2) + 1;

        WrightTextCenterd(x, y, option, xDimention);

        optionCount++;

    }
}
    private static void WrightTextCenterd(int curserPositionX, int curserPositionY, string text, int xDimention)
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
