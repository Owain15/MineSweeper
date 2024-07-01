using MineSweeper.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.ConsoleUI
{
    internal class UI
    {
        public ConsolePlayer player;

        public UI()
        {
            player = new ConsolePlayer();

        }

        public GameSettings RunStartScreen()
        {
            var menuIndex = 1;
            var hasGameGotSetting = false;

            var title = "minesweeper!";
            List<string> menuOptions = new List<string>()
            { "EACY","MEDEAM","HARD","QUIT"}; 

            GI.RenderMenu(title, menuOptions);

            while (!hasGameGotSetting)
            {
                GI.RenderMenuHighlightCurrentChoice(menuOptions, menuIndex);
                //wait for input
                //handle input
            }
            return new GameSettings(menuIndex);
        }

        public ConsoleKey WaitForInput()
        {
            
            var isInputValid = false;
            var playerInput = ConsoleKey.A;

            while (!isInputValid)
            {
                playerInput = GetInput();
                
                isInputValid = CheckInput(playerInput);
            }

            return playerInput;

        }

        public bool HandelInput(ConsoleKey input, Minefield field)
        {
            var isGameStillRunning = true;

            var location = player.GetLocation();

            switch (input)
            {
                case ConsoleKey.UpArrow: HandelUpArrow(location, field); break;
                case ConsoleKey.DownArrow: HandelDownArrow(location, field); break;
                case ConsoleKey.LeftArrow: HandelLeftArrow(location, field); break;
                case ConsoleKey.RightArrow: HandelRightArrow(location, field); break;

                case ConsoleKey.Enter: isGameStillRunning = HandelEnter(location,field); break;
                case ConsoleKey.Spacebar: HandelSpacebar(location, field); break;

                case ConsoleKey.Escape: isGameStillRunning = false; break;
            }

            player.UpdatePlayerLocation(location);

            return isGameStillRunning;
        
        }

        private int[] HandelUpArrow(int[] location, Minefield mineField)
        {

            if (location[1] - 1 >= 0) {  location[1] = location[1] - 1; }
            else { location[1] = mineField.field.GetLength(1)-1; }
           
            return location;

        }
        private int[] HandelDownArrow(int[] location, Minefield mineField)
        {

            if (location[1] + 1 < mineField.field.GetLength(1) ) { location[1] = location[1] + 1; }
            else { location[1] = 0; }

            return location;

        }
        private int[] HandelLeftArrow(int[] location, Minefield mineField)
        {

            if (location[0] - 1 >=0 ) { location[0] = location[0] - 1; }
            else { location[0] = mineField.field.GetLength(0) - 1; }

            return location;

        }
        private int[] HandelRightArrow(int[] location, Minefield mineField)
        {

            if (location[0] + 1 < mineField.field.GetLength(0)) { location[0] = location[0] + 1; }
            else { location[0] = 0; }

            return location;

        }

        private bool HandelEnter(int[] location, Minefield minefield)
        {
            var isGameStillRunning = true;
            
            var cell = minefield.field[location[0], location[1]];

            if (cell.hasCellBeenFlagged) 
            { return isGameStillRunning; }

            if (cell.IsCellHidden)
            { UncoverCell(minefield.field[location[0], location[1]], minefield); }
             
            if(cell.containsAMine)
            { isGameStillRunning = false; }

            
           

            return isGameStillRunning;
        }
        private void HandelSpacebar(int[] location, Minefield minefield)
        {
            if (minefield.field[location[0], location[1]].hasCellBeenFlagged)
            { minefield.field[location[0], location[1]].hasCellBeenFlagged = false; }
            else if (minefield.field[location[0], location[1]].IsCellHidden)
            {
                var numberOfMines = 0;
                var numberOfFlagsUsed = 0;

                foreach (var cell in minefield.field)
                { 
                    if (cell.containsAMine)
                    {  numberOfMines++; }
                    if (cell.hasCellBeenFlagged) 
                    { numberOfFlagsUsed++; } 
                  
                }
                if (numberOfFlagsUsed < numberOfMines)
                { minefield.field[location[0], location[1]].hasCellBeenFlagged = true; } 
            }

        }
       
        
        
        
        private ConsoleKey GetInput()
        {
            ConsoleKey input;
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                input = keyInfo.Key;
            } while (Console.KeyAvailable);

            return input;
        }

        private bool CheckInput(ConsoleKey input)
        {
            if (
                input == ConsoleKey.UpArrow ||
                input == ConsoleKey.DownArrow ||
                input == ConsoleKey.LeftArrow ||
                input == ConsoleKey.RightArrow ||
                input == ConsoleKey.Enter ||
                input == ConsoleKey.Spacebar ||
                input == ConsoleKey.Escape
                ) { return true; }
            else { return false; }

        }

        private void UncoverCell(Cell cell,Minefield minefield)
        {
            if (cell.hasCellBeenFlagged)
            { return; }

            if(!cell.IsCellHidden)
            { return; }

            if (cell.IsCellHidden)
            { cell.IsCellHidden = false; }

            if (cell.adjacentMines == 0)
            { 
             CascadeUncoverCells(cell, minefield);
            }

        }
        private void CascadeUncoverCells(Cell cell, Minefield minefield)
        {
            var maxX = minefield.field.GetLength(0) - 1;
            var maxY = minefield.field.GetLength(1) - 1;



            if (cell.X > 0 && cell.Y > 0)       { UncoverCell( minefield.field[cell.X-1,cell.Y-1], minefield); }
            if (cell.Y > 0 )                    { UncoverCell( minefield.field[cell.X,cell.Y-1], minefield); }
            if (cell.X < maxX && cell.Y > 0)    { UncoverCell(minefield.field[cell.X+1, cell.Y-1], minefield); }

            if (cell.X > 0)                     { UncoverCell(minefield.field[cell.X-1, cell.Y], minefield); }
            if (cell.X < maxX)                  { UncoverCell(minefield.field[cell.X+1, cell.Y], minefield); }

            if (cell.X > 0 && cell.Y < maxY)    { UncoverCell(minefield.field[cell.X-1, cell.Y+1], minefield); }
            if (cell.X > 0 && cell.Y < maxY)    { UncoverCell(minefield.field[cell.X-1, cell.Y + 1], minefield); }
            if (cell.X < maxX && cell.Y < maxY) { UncoverCell(minefield.field[cell.X+1, cell.Y + 1], minefield); }

        }
    }

}
