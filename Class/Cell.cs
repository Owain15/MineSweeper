using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Class
{
    internal class Cell
    {
        #region Properties

        public int CellPositionX;
        public int CellPositionY;

        public bool containsAMine;
        public bool hasCellBeenFlagged;

        public int? adjacentMines; 

        #endregion

        public Cell(int cellPositionX, int cellPositionY) 
        {
         
            CellPositionX = cellPositionX;
            CellPositionY = cellPositionY;

            containsAMine = false;
            hasCellBeenFlagged = false;
        
            adjacentMines = null;

        }
     

    }
}
