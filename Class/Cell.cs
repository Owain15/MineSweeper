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

        public int X;
        public int Y;

        public bool IsCellHidden;
        public bool containsAMine;
        public bool hasCellBeenFlagged;

        public int? adjacentMines; 

        #endregion

        public Cell(int cellPositionX, int cellPositionY) 
        {
         
            X = cellPositionX;
            Y = cellPositionY;

            IsCellHidden = true;
            containsAMine = false;
            hasCellBeenFlagged = false;
        
            adjacentMines = null;

        }
     

    }
}
