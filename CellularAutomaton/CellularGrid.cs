using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// Gets the number of all the current alive cells.
        /// </summary>
        public int AliveCount { get; private set; }

        /// <summary>
        /// Gets the number of cells died during the last cycle.
        /// </summary>
        public int LastCycleDeaths { get; private set; }

        /// <summary>
        /// Gets the number of cells born during the last cycle.
        /// </summary>
        public int LastCycleNewBorns { get; private set; }

        /// <summary>
        /// Gets the number of cells evolved during the last cycle.
        /// </summary>
        public int LastCycleSurvivors { get; private set; }

        /// <summary>
        /// Determines if the cells can go around the borders for neighbors.
        /// </summary>
        public bool Borderless { get; set; }

        /// <summary>
        /// Constructor.... Pass null if you don't want to use initial rule (grid without initial rule will be all dead cells).
        /// </summary>
        public CellularGrid(int rowsCount, int columnsCount, IRule initialRule)
        {
            if (rowsCount < 1 || columnsCount < 1)
                throw new ArgumentException("You can't use less that 1 for rows or columns count.");

            RowsCount    = rowsCount;
            ColumnsCount = columnsCount;
            Rules        = new List<IRule>();

            if (initialRule == null)
                InitializeCells();
            else
                InitializeCells(initialRule);
        }

        
        /// <summary>
        /// Initializes the cells array according to the passed rule
        /// </summary>
        private void InitializeCells(IRule initialRule)
        {
            Cells = new Cell[RowsCount, ColumnsCount];

            Parallel.For(0, RowsCount, row =>
            {
                Parallel.For(0, ColumnsCount, col =>
                {
                    var cell = new Cell(row, col);

                    if (initialRule.Condition(cell, this))
                        initialRule.Action(cell);

                    if (cell.State == CellState.Alive)
                        AliveCount++;

                    Cells[row, col] = cell;
                });
            });
        }

        /// <summary>
        /// Initializes the cells array . All cells are dead.
        /// </summary>
        private void InitializeCells()
        {
            Cells = new Cell[RowsCount, ColumnsCount];

            Parallel.For(0, RowsCount, row =>
            {
                Parallel.For(0, ColumnsCount, col =>
                {
                    Cells[row, col] = new Cell(row, col);
                });
            });
        }

        /// <summary>
        /// Applies all the rules and increases the age of this grid by 1.
        /// </summary>
        public void NextCycle()
        {
            AliveCount         = 0;
            LastCycleDeaths    = 0;
            LastCycleNewBorns  = 0;
            LastCycleSurvivors = 0;

            var updatedCells = new Cell[RowsCount, ColumnsCount];

            Parallel.For(0, RowsCount, row =>
            {
                Parallel.For(0, ColumnsCount, col =>
                {
                    var oldCell = Cells[row, col].Clone();
                    var newCell = oldCell.Clone();

                    foreach (var rule in Rules.Where(rule => rule.Condition(oldCell, this)))
                    {
                        rule.Action(newCell);
                    }

                    if (newCell.State == CellState.Alive)
                    {
                        AliveCount++;

                        if (oldCell.State != CellState.Alive)
                        {
                            LastCycleNewBorns++;
                        }
                        else if (oldCell.State == CellState.Alive)
                        {
                            LastCycleSurvivors++;
                        }
                    }
                    else if (newCell.State == CellState.Dead && oldCell.State == CellState.Alive)
                    {
                        LastCycleDeaths++;
                    }

                    updatedCells[row, col] = newCell;
                });
            });

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
        /// <param name="targetCol">The column of the target cell.</param>
        /// <returns>IEnumerable of Cell</returns>
        public Cell[] GetNeighboringCells(int targetRow, int targetCol)
        {
            if (targetRow < 0 || targetRow >= RowsCount)
                throw new ArgumentOutOfRangeException("targetRow",
                                                      "Target row was outside the bounds of this grid.");

            if (targetCol < 0 || targetCol >= ColumnsCount)
                throw new ArgumentOutOfRangeException("targetCol",
                                                      "Target column was outside the bounds of this grid.");

            var list = new List<Cell>();

            var rows = new[] { targetRow - 1, targetRow, targetRow + 1 };
            var cols = new[] { targetCol - 1, targetCol, targetCol + 1 };

            for (var rowIndex = 0; rowIndex < 3; rowIndex++)
            {
                var currRow = rows[rowIndex];

                currRow = CheckLimits(currRow, RowsCount - 1);

                if (currRow == -1) continue;

                for (var colIndex = 0; colIndex < 3; colIndex++)
                {
                    var currCol = cols[colIndex];

                    if (currRow == targetRow && currCol == targetCol) continue;

                    currCol = CheckLimits(currCol, ColumnsCount - 1);

                    if (currCol != -1)
                    {
                        list.Add(Cells[currRow, currCol]);

                    }
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// Counts alive neighbors of a giving location.
        /// </summary>
        /// <param name="targetRow">The row of the target location.</param>
        /// <param name="targetCol">The column of the target location.</param>
        public int CountAliveNeighbors(int targetRow, int targetCol)
        {
            if (targetRow < 0 || targetRow >= RowsCount)
                throw new ArgumentOutOfRangeException("targetRow",
                                                      "Target row was outside the bounds of this grid.");

            if (targetCol < 0 || targetCol >= ColumnsCount)
                throw new ArgumentOutOfRangeException("targetCol",
                                                      "Target column was outside the bounds of this grid.");

            var count = 0;

            var rows = new[] { targetRow - 1, targetRow, targetRow + 1 };
            var cols = new[] { targetCol - 1, targetCol, targetCol + 1 };

            for (var rowIndex = 0; rowIndex < 3; rowIndex++)
            {
                var currRow = rows[rowIndex];

                currRow = CheckLimits(currRow, RowsCount - 1);

                if (currRow == -1) continue;

                for (var colIndex = 0; colIndex < 3; colIndex++)
                {
                    var currCol = cols[colIndex];

                    if (currRow == targetRow && currCol == targetCol) continue;

                    currCol = CheckLimits(currCol, ColumnsCount - 1);

                    if (currCol != -1 && (Cells[currRow, currCol].State == CellState.Alive))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// If the value is within the bounds of this grid then the value itself is returnd.
        /// If the value is out of bounds and Borderless is set to true then the value is rounded around the edge.
        /// If the value is out of bounds and Borderless is set to false then -1 is returnd.
        /// </summary>
        /// <param name="value">The actual value to check.</param>
        /// <param name="last">The last value to check against.</param>
        private int CheckLimits(int value, int last)
        {
            //if the givin value is within bounds then return it.
            if (value >= 0 && value <= last)
                return value;

            //if value is less than 0 and Borderless is true, then return last.
            if (value < 0 && Borderless)
                return last;

            //if value is greater than last then return 0 (return the first). 
            if (value > last && Borderless)
                return 0;

            //if the value is less than 0 and Borderless is false, then return -1.
            if(value < 0 && !Borderless)
                return -1;

            //if value is greater than last and Borderless is false then return -1.            
            return -1;
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
