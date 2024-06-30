using MineSweeper.Class;
using MineSweeper.ConsoleUI;
using System.Numerics;

ConsolePlayer Player = new ConsolePlayer();
UI Input = new UI();

//Game Settings

int fieldDimensionX = 20;
int fieldDimensionY = 10;

int numberOfMines = 5;

//

Minefield minefield;

//Run Program

GI.initializeConsoleWindow();
RunGame();

Console.Read();




void RunGame()
{
    var isGameRunning = true;  
    
    minefield = new Minefield(fieldDimensionX,fieldDimensionY, numberOfMines);

    //render screen
 
    while (isGameRunning)
    {
        GI.RenderDisplay(minefield);

        // Make Highlighted Player Flash.

        GI.HighlightPlayer(Input.player.GetLocation());

        var input = Input.WaitForInput();

        GI.RemoveHighlightPlayer(Input.player.GetLocation(),minefield);
   
        isGameRunning = Input.HandelInput(input,minefield);

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
        
        if (cellsStillHidden == numberOfMines)
        { return false; }
        else if (FlagsOnMines == numberOfMines) 
            { return false; }
        else { return true; }
    }

}