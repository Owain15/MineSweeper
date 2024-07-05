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

    public void UpdateMenuOptions(List<string> newMenuOptions)
    { MenuOptions = newMenuOptions; }
   
    
    private void RenderOptionsBox(List<string> menuOptions)
    {
        GI.DrawBorder(DisplayWidth, DisplayHeight-3, 3 );
        WrightOptions(menuOptions, DisplayWidth,3);

    }
    private static void RenderTitleBox(string title,int DisplayWidth)
    {
        GI.DrawBorder(DisplayWidth,3, 0);
        GI.WrightTextCenterd(GamePositionX, GamePositionY + 1, title,DisplayWidth);

    }


    private static void HighlightMenuIndex(List<string> menuOptions, int menuIndex,int displayWidth)
    {

        var yOfset = 3;

        var highlightedOption = $"> {menuOptions[menuIndex]} <";

        foreach (string option in menuOptions)
        {
            var x = GamePositionX;
            var y = GamePositionY + yOfset + (menuIndex * 2) + 1;

            GI.WrightTextCenterd(x, y, highlightedOption, displayWidth);

            

        }
    }
    private static void WrightOptions(List<string> menuOptions, int xDimention, int yOfset)
{
    var optionCount = 0;

    foreach (string option in menuOptions)
    {
        var x = GamePositionX;
        var y = GamePositionY + yOfset + (optionCount * 2) + 1;

        GI.WrightTextCenterd(x, y, option, xDimention);

        optionCount++;

    }
}
    private static int GetLongestString(string title, List<string> menuOptions)
    {
        var listCopy = new List<string>();

        listCopy.Add(title);
        foreach (var item in menuOptions) { listCopy.Add(item); }

        return listCopy.Max(x => x.Length);
    }

   
}
