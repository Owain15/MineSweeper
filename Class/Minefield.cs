﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Class
{
    internal class Minefield
    {

        #region Properties

        public Cell[,] field;


        #endregion

        public Minefield(int fieldDimensionX, int fieldDimensionY, int numberOfMines)
        {
            field = initializeMinefield(fieldDimensionX, fieldDimensionY, numberOfMines);
        }




        private Cell[,] initializeMinefield(int fieldDimensionX, int fieldDimensionY, int numberOfMines)
        {

            Cell[,] field = new Cell[fieldDimensionX, fieldDimensionY];

            SetCellPositions(field);

            SetMinePositions(field, numberOfMines);

            SetAdjacentMineValues(field);

            return field;
        }

        private void SetCellPositions(Cell[,] field)
        {
            for (int y = 0; y < field.GetLength(1); y++)
            {
                for (int x = 0; x < field.GetLength(0); x++)
                {
                    field[x, y] = new Cell(x, y);
                }

            }

        }

        private void SetMinePositions(Cell[,] field, int numberOfMines)
        {
            var minesSet = 0;
            var allMinesSet = false;

            var randomX = new Random();
            var randomY = new Random();

            while (!allMinesSet)
            {

                var currentX = randomX.Next(0, field.GetLength(0));
                var currentY = randomY.Next(0, field.GetLength(1));


                if (!field[currentX, currentY].containsAMine)
                {
                    field[currentX, currentY].containsAMine = true;
                    minesSet++;
                }

                if (minesSet == numberOfMines)
                { allMinesSet = true; }
            }


        }

        private void SetAdjacentMineValues(Cell[,] field)
        {
            var maxX = field.GetLength(0) - 1;
            var maxY = field.GetLength(1) - 1;



            foreach (var cell in field)
            {
                var currentMineCount = 0;

                if (cell.X > 0 && cell.Y > 0 &&  field[(cell.X - 1), (cell.Y - 1)].containsAMine)        { currentMineCount++; }
                if (cell.Y > 0 && field[(cell.X), (cell.Y - 1)].containsAMine)                           { currentMineCount++; }
                if (cell.X < maxX && cell.Y > 0 &&  field[(cell.X + 1), (cell.Y - 1)].containsAMine)     { currentMineCount++; }

                if (cell.X > 0 && field[(cell.X - 1), (cell.Y)].containsAMine)                           { currentMineCount++; }
                if (cell.X < maxX && field[(cell.X + 1), (cell.Y)].containsAMine)                        { currentMineCount++; }

                if (cell.X > 0 && cell.Y < maxY && field[(cell.X - 1), (cell.Y + 1)].containsAMine)      { currentMineCount++; }
                if (cell.X > 0 && cell.Y < maxY && field[(cell.X), (cell.Y + 1)].containsAMine)          { currentMineCount++; }
                if (cell.X < maxX && cell.Y < maxY && field[(cell.X + 1), (cell.Y + 1)].containsAMine)   { currentMineCount++; }
                
                cell.adjacentMines = currentMineCount;
            }

        }

    }
}
