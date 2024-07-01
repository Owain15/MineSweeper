using MineSweeper.Class;
using MineSweeper.ConsoleUI;
using System.Numerics;
using System.Runtime.InteropServices;

ConsolePlayer Player = new ConsolePlayer();
UI Ui = new UI();

Minefield minefield;



GI.initializeConsoleWindow();
//GameSettings settings = new GameSettings(1);

var settings = Ui.RunStartScreen();

RunGame(settings);

Console.Read();




void RunGame(GameSettings settings)
{
    var isGameRunning = true;  
    
    minefield = new Minefield(settings.fieldDimensionX,settings.fieldDimensionY,settings.numberOfMines);

    //render screen
   // GI.RenderScreen();

    while (isGameRunning)
    {
        GI.RenderDisplay(minefield);

        // Make Highlighted Player Flash.

        GI.HighlightPlayer(Ui.player.GetLocation());

        var input = Ui.WaitForInput();

        GI.RemoveHighlightPlayer(Ui.player.GetLocation(),minefield);
   
        isGameRunning = Ui.HandelInput(input,minefield);

        if (isGameRunning)
        { isGameRunning = EvaluateField(); }

    }


    Console.Read();


    bool EvaluateField()
    {
        var FlagsOnMines = 0;
        var cellsStillHidden = 0;

        foreach (var cell in minefield.field)
        {
            if (cell.IsCellHidden)
            {cellsStillHidden++;}

            if (cell.containsAMine && cell.hasCellBeenFlagged)
            { FlagsOnMines++; }
        }
        
        if (cellsStillHidden == settings.numberOfMines)
        { return false; }
        else if (FlagsOnMines == settings.numberOfMines) 
            { return false; }
        else { return true; }
    }
     
}