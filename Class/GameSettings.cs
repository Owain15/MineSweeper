using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Class
{
    internal class GameSettings
    {
        public int fieldDimensionX;
        public int fieldDimensionY;

        public int numberOfMines;
    
        public GameSettings(int gameSettingsIndex ) 
        { 
         switch(gameSettingsIndex)
            {
                case 0: fieldDimensionX = 20; fieldDimensionY = 10; numberOfMines = 10; break;
                case 1: fieldDimensionX = 30; fieldDimensionY = 20; numberOfMines = 15; break;
                case 2: fieldDimensionX = 60; fieldDimensionY = 20; numberOfMines = 35; break;
                //case 4: UI.RunCustomSettings(); break;
                default: fieldDimensionX = 20; fieldDimensionY = 10; numberOfMines = 10; break;
            }
           
        }
    }


}
