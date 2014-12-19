using System;
using System.Collections.Generic;
using System.Linq;

namespace CellularAutomaton
{
    public class CellularGrid 
    {
        /// <summary>
        /// Fired after the NextCycle method finishes.
        /// </summary>
        public EventHandler Cycled;

        /// <summary>
        /// Gets the count of all the cycles done so far. 
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// Gets or sets the list of rules to be applied this universe.
        /// </summary>
        public List<IRule> Rules { get; set; }

        /// <summary>
        /// Gets the total count of rows.
        /// </summary>
        public int RowsCount { get; private set; }

        /// <summary>
        /// Gets the total count of columns.
        /// </summary>
        public int ColumnsCount { get; private set; }

        /// <summary>
        /// Gets the cells in this grid.
        /// </summary>
        public Cell[,] Cells { get; private set; }


        public CellularGrid(int rowsCount, int columnsCount, IRule initialRule)
        {
            if (rowsCount < 1 || columnsCount < 1)
                throw new ArgumentException("You can't use less that 1 for rows or columns count.");

            RowsCount    = rowsCount;
            ColumnsCount = columnsCount;
            Age          = 0;
            Rules        = new List<IRule>();

            if (initialRule == null)
                InitializeCells();
            else
                InitializeCells(initialRule);
        }

        
        /// <summary>
        /// Initializes the cells array.
        /// Applies the initial rule.
        /// </summary>
        private void InitializeCells(IRule initialRule)
        {
            Cells = new Cell[RowsCount, ColumnsCount];

            for (var row = 0; row < RowsCount; row++)
            {
                for (var col = 0; col < ColumnsCount; col++)
                {
                    var cell = new Cell(row, col);

                    if (initialRule.Condition(cell,this))
                        initialRule.Action(cell);

                    Cells[row, col] = cell;
                }
            }
        }

        /// <summary>
        /// Initializes the cells array.
        /// </summary>
        private void InitializeCells()
        {
            Cells = new Cell[RowsCount, ColumnsCount];

            for (var row = 0; row < RowsCount; row++)
            {
                for (var col = 0; col < ColumnsCount; col++)
                {
                    Cells[row, col] = new Cell(row, col);
                }
            }
        }

        /// <summary>
        /// Applies all the rules and increases the age of this grid by 1.
        /// </summary>
        public void NextCycle()
        {
            var updatedCells = new Cell[RowsCount, ColumnsCount];

            for (var row = 0; row < RowsCount; row++)
            {
                for (var col = 0; col < ColumnsCount; col++)
                {
                    var oldCell = Cells[row, col].Clone();
                    var newCell = oldCell.Clone();

                    foreach (var rule in Rules.Where(rule => rule.Condition(oldCell, this)))
                    {
                        rule.Action(newCell);
                    }

                    updatedCells[row, col] = newCell;
                }
            }

            Cells = updatedCells;

            Age++;

            if (Cycled != null)
                Cycled(this, new EventArgs());
        }
        
        /// <summary>
        /// Returns a list of all the neighboring cells of the selected target cell. 
        /// Throws ArgumentOutOfRangeException if one of the arguments is out of range.
        /// </summary>
        /// <param name="targetRow">The row of the target cell.</param>
        /// <param name="targetColumn">The column of the target cell.</param>
        /// <returns>IEnumerable of Cell</returns>
        public Cell[] GetNeighboringCells(int targetRow, int targetColumn)
        {
            if (targetRow < 0 || targetRow >= RowsCount)
                throw new ArgumentOutOfRangeException("targetRow",
                                                      "Target row was outside the bounds of this grid.");

            if (targetColumn < 0 || targetColumn >= ColumnsCount)
                throw new ArgumentOutOfRangeException("targetColumn",
                                                      "Target column was outside the bounds of this grid.");

            var list = new List<Cell>();
            
            var rowBeforeTarget = targetRow - 1;
            var rowAfterTarget  = targetRow + 1;

            var columnBeforeTarget = targetColumn - 1;
            var columnAfterTarget  = targetColumn + 1;

            for (var row = rowBeforeTarget; row <= rowAfterTarget; row++)
            {
                if (row < 0 || row >= RowsCount)
                    continue;

                for (var column = columnBeforeTarget; column <= columnAfterTarget; column++)
                {
                    if (column < 0 || column >= ColumnsCount)
                        continue;

                    if (row == targetRow && column == targetColumn)
                        continue;

                    list.Add(Cells[row, column]);
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// Counts alive neighbors of a giving location.
        /// </summary>
        /// <param name="targetRow">The row of the target location.</param>
        /// <param name="targetColumn">The column of the target location.</param>
        public int CountAliveNeighbors(int targetRow, int targetColumn)
        {
            if (targetRow < 0 || targetRow >= RowsCount)
                throw new ArgumentOutOfRangeException("targetRow",
                                                      "Target row was outside the bounds of this grid.");

            if (targetColumn < 0 || targetColumn >= ColumnsCount)
                throw new ArgumentOutOfRangeException("targetColumn",
                                                      "Target column was outside the bounds of this grid.");

            var count = 0;

            var rowBeforeTarget = targetRow - 1;
            var rowAfterTarget  = targetRow + 1;

            var columnBeforeTarget = targetColumn - 1;
            var columnAfterTarget  = targetColumn + 1;

            for (var row = rowBeforeTarget; row <= rowAfterTarget; row++)
            {
                if (row < 0 || row >= RowsCount)
                    continue;

                for (var column = columnBeforeTarget; column <= columnAfterTarget; column++)
                {
                    if (column < 0 || column >= ColumnsCount)
                        continue;

                    if (row == targetRow && column == targetColumn)
                        continue;

                    if (Cells[row, column].Alive)
                        count++;
                }
            }

            return count;
        }


        /// <summary>
        /// Returns a string identifier of this CellularGrid.
        /// </summary>
        public override string ToString()
        {
            return string.Format("Grid with {0} rows and {1} columns.", RowsCount, ColumnsCount);
        }


    }
}
