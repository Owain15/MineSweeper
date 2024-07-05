using MineSweeper.Class;
using MineSweeper.ConsoleUI;
using System.Numerics;
using System.Runtime.InteropServices;

ConsolePlayer Player = new ConsolePlayer();
UI Ui = new UI();

Minefield minefield;


GI.initializeConsoleWindow();

var isAppRunning = true;

while (isAppRunning)
{

  var settings = Ui.RunStartScreen();

  RunGame(settings);

}





void RunGame(GameSettings settings)
{
    
    var isGameRunning = true;

    var hasPlayerClearedField = false;
    var hasPlayerFailed = false;

    minefield = new Minefield(settings.fieldDimensionX,settings.fieldDimensionY,settings.numberOfMines);


    GI.RenderScreen(minefield);

    
    while (isGameRunning)
    {
        GI.RenderDisplay(minefield);

        GI.HighlightPlayer(Ui.player.GetLocation());

        var input = Ui.WaitForInput();

        GI.RemoveHighlightPlayer(Ui.player.GetLocation(),minefield);
   
        Ui.HandelInput(input,minefield);

        hasPlayerFailed = CheckIfPlayerHasFailed();
        if (hasPlayerFailed) { isGameRunning = false; break; }

        hasPlayerClearedField = CheckIfPlayerHasClearedField(); 
        if (hasPlayerClearedField) { isGameRunning = false; break; }
    }

    GI.RenderEndScreen(minefield, hasPlayerClearedField);
    
    Console.Read();

    bool CheckIfPlayerHasClearedField()
    {
        var FlagsOnMines = 0;
        var cellsStillHidden = 0;

        foreach (var cell in minefield.field)
        {
            
            if (cellsStillHidden < settings.numberOfMines +1 && cell.IsCellHidden)
            {cellsStillHidden++;}

            if (cell.containsAMine && cell.hasCellBeenFlagged)
            { FlagsOnMines++; }
        }
        
        if (cellsStillHidden == settings.numberOfMines)
        { return true; }
        else if (FlagsOnMines == settings.numberOfMines) 
             { return true; }
        else { return false; }
    }

    bool CheckIfPlayerHasFailed()
    {
        foreach (var cell in minefield.field)
        {
          if(cell.containsAMine && !cell.IsCellHidden)
          { return true; }
        }

       return false;
    }
     
}