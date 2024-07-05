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
            var menuIndex = 0;
            var gameSettingChosen = false;

            var title = "Minesweeper!";
            List<string> menuOptions = new List<string>()
            { "EACY","MEDEAM","HARD","CUSTOM GAME","QUIT"}; 

            ConsoleMenu startMenu = new ConsoleMenu(GI.gamePositionX,GI.gamePositionY,title,menuOptions);

            while (!gameSettingChosen)
            {
                startMenu.RenderCurrentMenu(menuIndex);
                var input = WaitForInput();

                     if (input == ConsoleKey.Escape) { Environment.Exit(0); }
                else if (input == ConsoleKey.UpArrow) { menuIndex--; }
                else if (input == ConsoleKey.DownArrow) { menuIndex++; }
                else if (input == ConsoleKey.Enter && menuIndex == 3) { var settings = RunCustomSettingsMenu(); return settings; }
                else if (input == ConsoleKey.Enter&&menuIndex==4){ Environment.Exit(0); }
                else if (input == ConsoleKey.Enter) { gameSettingChosen = true; }

                     if (menuIndex > menuOptions.Count-1) { menuIndex = 0; }
                     if (menuIndex < 0) { menuIndex = menuOptions.Count-1; }
            }
            return new GameSettings(menuIndex);
        }
        
        private GameSettings RunCustomSettingsMenu()
        {
            var settings = new GameSettings(1);

            var title = "Custom Game Menu!";
           
            var menuOptions = new List<string>()
            { $"Field Width:{settings.fieldDimensionX}",$"Field Height: {settings.fieldDimensionY}",$"Number Of Mines: {settings.numberOfMines}","Start Game"};

            var customGameMenu = new ConsoleMenu(GI.gamePositionX, GI.gamePositionY, title,menuOptions);

            var settingsHaveBeenSet = false;
            var menuIndex = 0;

            while (!settingsHaveBeenSet)
            {
                customGameMenu.RenderCurrentMenu(menuIndex);

                var input = WaitForInput();

                //settings = HandelCustomGameMenu(settings, menuIndex, input);

                switch (menuIndex)
                {

                    case 0:
                        
                        // width Setting

                        switch (input)
                        {

                            case ConsoleKey.UpArrow: menuIndex = 3; break;
                            case ConsoleKey.DownArrow: menuIndex = 1; break;

                            case ConsoleKey.LeftArrow:
                                settings.fieldDimensionX -= 5;
                                if (settings.fieldDimensionX < 5) { settings.fieldDimensionX = 5; }
                                break;

                            case ConsoleKey.RightArrow: 
                                settings.fieldDimensionX += 5;
                                if (settings.fieldDimensionX > 60) { settings.fieldDimensionX = 60; } break;

                            case ConsoleKey.Escape: Environment.Exit(0); break;
                            default: break;

                        }

                        break;

                    case 1:
                        
                        //Height Setting    
                        
                        switch (input)
                        {
                            case ConsoleKey.UpArrow: menuIndex = 0; break;
                            case ConsoleKey.DownArrow: menuIndex = 2; break;

                            case ConsoleKey.LeftArrow: 
                                settings.fieldDimensionY -= 5; 
                                if (settings.fieldDimensionY < 5) { settings.fieldDimensionY = 5; } break;
                            
                            case ConsoleKey.RightArrow: 
                                settings.fieldDimensionY+= 5; 
                                if (settings.fieldDimensionY > 20) { settings.fieldDimensionY = 20; } break;

                            case ConsoleKey.Escape: Environment.Exit(0); break;
                            default: break;

                        }
                        break;

                    case 2:
                        
                        //number of mines
                        
                        switch (input)
                        {
                            case ConsoleKey.UpArrow: menuIndex = 1; break;
                            case ConsoleKey.DownArrow: menuIndex = 3; break;

                            case ConsoleKey.LeftArrow: 
                                settings.numberOfMines -= 1; 
                                if (settings.numberOfMines < 1) { settings.numberOfMines = 1; } break;
                            
                            case ConsoleKey.RightArrow: 
                                settings.numberOfMines += 1; 
                                if (settings.numberOfMines > (settings.fieldDimensionX * settings.fieldDimensionY)-1) 
                                { settings.numberOfMines = (settings.fieldDimensionX * settings.fieldDimensionY) - 1; } break;

                            case ConsoleKey.Escape: Environment.Exit(0); break;
                            default: break;
                        }
                        break;

                    case 3:
                        //start game
                        switch (input)
                        {
                            case ConsoleKey.UpArrow: menuIndex = 2; break;
                            case ConsoleKey.DownArrow: menuIndex = 0; break;

                            case ConsoleKey.Enter: settingsHaveBeenSet = true; break;

                            case ConsoleKey.Escape: Environment.Exit(0); break;
                            default: break;
                        }
                        break;

                    default: break;
                }

                 menuOptions = new List<string>()
                { $"Field Width:{settings.fieldDimensionX}",$"Field Height: {settings.fieldDimensionY}",$"Number Of Mines: {settings.numberOfMines}","Start Game"};
           
                customGameMenu.UpdateMenuOptions(menuOptions);

            }

            return settings;

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

        public void HandelInput(ConsoleKey input, Minefield field)
        { 
            var location = player.GetLocation();

            switch (input)
            {
                case ConsoleKey.UpArrow: HandelUpArrow(location, field); break;
                case ConsoleKey.DownArrow: HandelDownArrow(location, field); break;
                case ConsoleKey.LeftArrow: HandelLeftArrow(location, field); break;
                case ConsoleKey.RightArrow: HandelRightArrow(location, field); break;

                case ConsoleKey.Enter: HandelEnter(location,field); break;
                case ConsoleKey.Spacebar: HandelSpacebar(location, field); break;

                case ConsoleKey.Escape: HandelEscape(field); break;
            }

            player.UpdatePlayerLocation(location);
        
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

        private void HandelEnter(int[] location, Minefield minefield)
        {   
            var cell = minefield.field[location[0], location[1]];

            if (cell.hasCellBeenFlagged) 
            { return; }

            if (cell.IsCellHidden)
            { UncoverCell(minefield.field[location[0], location[1]], minefield); }
           
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
        private void HandelEscape(Minefield minefield)
        {
            foreach (var cell in minefield.field)
            {
                cell.hasCellBeenFlagged = false;
                cell.IsCellHidden = false;
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
