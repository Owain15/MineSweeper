﻿using System;
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

    public int MenuIndex;

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
    internal void RenderCurrentMenu(int menuIndex)
    {
        RenderTitleBox(Title, menuIndex);
    }

    //public static void RenderMenu(string title, List<string> menuOptions)
    //{
    //    var XDimention = GetLongestString(title, menuOptions) + 8;
    //    var YDimention = (menuOptions.Count() * 2) + 1;

    //    RenderTitleBox(title, XDimention);
    //    RenderOptionsBox(menuOptions, XDimention, YDimention, 3);
    //}

    //public static void RenderMenuHighlightCurrentChoice(int menuXDimention, int MenuYDimention,  List<string> menuOptions, int menuIndex)
    //{
    //    var XDimention = GetLongestString(title, menuOptions) + 8;
    //    var YDimention = (menuOptions.Count() * 2) + 1;

    //    DrawBorder(xDimention, yDimention, yOfset);

    //    WrightOptions(menuOptions, xDimention, yOfset);
    //} 

    private static void RenderTitleBox(string title, int xDimention)
    {
        DrawBorder(xDimention, 3, 0);
        //WrightTextCenterd(GamePositionX, GamePositionY + 1, title, xDimention);

    }
    private static void DrawBorder(int width, int height, int hightOfset)
    {
        char[,] border = new char[width, height];
        int[] currentChar = new int[2];

        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                currentChar[0] = x;
                currentChar[1] = y;

                if (currentChar[0] == 0 && currentChar[1] == 0)
                {
                    Console.SetCursorPosition(x + GamePositionX, y + GamePositionY + hightOfset);
                    Console.Write('╔');
                }
                else if (currentChar[0] == width-1  && currentChar[1] == 0)
                {
                    Console.SetCursorPosition(x + GamePositionX, y + GamePositionY + hightOfset);
                    Console.Write('╗');
                }
                else if (currentChar[0] == 0 && currentChar[1] == height - 1)
                {
                    Console.SetCursorPosition(x + GamePositionX, y + GamePositionY + hightOfset);
                    Console.Write('╚');
                }
                else if (currentChar[1] == width - 1 && currentChar[0] == height - 1)
                {
                    Console.SetCursorPosition(x + GamePositionX, y + GamePositionY + hightOfset);
                    Console.Write('╝');
                }
                else if (currentChar[0] == 0 || currentChar[0] == width - 1)
                {
                    Console.SetCursorPosition(x + GamePositionX, y + GamePositionY + hightOfset);
                    Console.Write('║');
                }
                else if (currentChar[1] == 0 || currentChar[1] == height - 1)
                {
                    Console.SetCursorPosition(x + GamePositionX, y + GamePositionY + hightOfset);
                    Console.Write('═');
                }

            }
        }
    }


    //}
    //private static void RenderOptionsBox(List<string> menuOptions, int xDimention, int yDimention, int yOfset)
    //{
    //    DrawBorder(xDimention, yDimention,yOfset);

    //    WrightOptions(menuOptions,xDimention,yOfset );


    //}
    //private static void WrightOptions(List<string> menuOptions, int xDimention, int yOfset)
    //{
    //     var optionCount = 0;

    //    foreach (string option in menuOptions)
    //    {
    //        var x = gamePositionX;
    //        var y = gamePositionY+yOfset+(optionCount * 2)+1;

    //        WrightTextCenterd(x,y,option,xDimention);

    //        optionCount++;

    //    }
    //}


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
