using System;
using System.Collections.Generic;
using System.Linq;

namespace CellularAutomaton
{
    public class Universe : ICellularGrid
    {
        private List<Cell> _cells;
        
        /// <summary>
        /// Fired after the NextCycle method finishes.
        /// </summary>
        public EventHandler CycleFinished;

        /// <summary>
        /// Gets the total count of rows in the univers grid.
        /// </summary>
        public int RowsCount    { get; private set; }

        /// <summary>
        /// Gets the total count of columns in the universe grid.
        /// </summary>
        public int ColumnsCount { get; private set; }

        /// <summary>
        /// Gets the count of all the cycles done so far. 
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// Gets or sets the list of rules to be applied on the cells of this universe.
        /// </summary>
        public List<IRule> Rules { get; set; }
        
        /// <summary>
        /// Gets an IEnumerable of all cell in this universe.
        /// </summary>
        public IEnumerable<Cell> Cells
        {
            get { return _cells; }
        }

        private Universe(int rowsCount, int columnsCount)
        {
            Age           = 0;
            RowsCount     = rowsCount;
            ColumnsCount  = columnsCount;
            Rules         = new List<IRule>();
            
            InitializeCells();
        }

        private void InitializeCells()
        {
            _cells = new List<Cell>();

            for (var row = 0; row < RowsCount; row++)
            {
                for (var col = 0; col < ColumnsCount; col++)
                {
                    _cells.Add(Cell.MakeCell(row, col));
                }
            }
        }

        private void ApplyRules()
        {
            foreach (var rule in Rules)
            {
                rule.Transform(this);
            }
        }

        /// <summary>
        /// Returns a new Universe object.
        /// Throws ArgumentException if rowsCount or columnsCount is negative.
        /// </summary>
        /// <param name="rowsCount">The count of rows in the whole universe.</param>
        /// <param name="columnsCount">The count of columns in the whole universe.</param>
        public static Universe MakeUniverse(int rowsCount, int columnsCount)
        {
            if (rowsCount < 1 || columnsCount < 1)
                throw new ArgumentException("You can't use less that 1 for rows or columns count.");

            return new Universe(rowsCount, columnsCount);
        }

        /// <summary>
        /// Returns a cell at a specified row and column.
        /// </summary>
        public Cell this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= RowsCount)
                    throw new IndexOutOfRangeException("Row was outside the bounds of the universe.");

                if (column < 0 || column >= ColumnsCount)
                    throw new IndexOutOfRangeException("Column was outside the bounds of the universe.");

                return _cells.Single(c => c.Row == row && c.Column == column);
            }
        }

        /// <summary>
        /// Moves this universe to the next cycle, applying all the rules in the Rules list to this universe.
        /// Fires the CycleFinished event after it's done.
        /// </summary>
        public void NextCycle()
        {
            ApplyRules();

            Age++;

            if(CycleFinished != null)
                CycleFinished(this,new EventArgs());
        }

        /// <summary>
        /// Returns a list of all the neighboring cells of the selected target cell. 
        /// Throws ArgumentOutOfRangeException if one of the arguments is out of range of this universe.
        /// </summary>
        /// <param name="targetRow">The row of the target cell.</param>
        /// <param name="targetColumn">The column of the target cell.</param>
        /// <returns>IEnumerable of Cell</returns>
        public IEnumerable<Cell> GetNeighboringCells(int targetRow, int targetColumn)
        {
            if (targetRow < 0 || targetRow >= RowsCount)
                throw new ArgumentOutOfRangeException("targetRow", 
                                                      "Target row was outside the bounds of the universe.");

            if (targetColumn < 0 || targetColumn >= ColumnsCount)
                throw new ArgumentOutOfRangeException("targetColumn", 
                                                      "Target column was outside the bounds of the universe.");
            
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

                    list.Add(this[row, column]);
                }
            }

            return list.AsEnumerable();
        }

        /// <summary>
        /// Returns a list of all the neighboring cells of the selected target cell. 
        /// Throws ArgumentNullException if the target cell is null.
        /// Throws ArgumentException if the target cell is not 
        /// </summary>
        /// <param name="targetCell">The Cell to get the neighbors of.</param>
        /// <returns>IEnumerable of Cell</returns>
        public IEnumerable<Cell> GetNeighboringCells(Cell targetCell)
        {
            if (targetCell == null)
                throw new ArgumentNullException("targetCell", "Target cell is null!");
                
            if (Cells.Contains(targetCell) == false)
                throw new ArgumentException("Target cell is not part of this universe!");

            return GetNeighboringCells(targetCell.Row, targetCell.Column);
        }

        /// <summary>
        /// Returns a string identifier of this Universe.
        /// </summary>
        public override string ToString()
        {
            return string.Format("Universe with {0} rows and {1} columns.", RowsCount, ColumnsCount);
        }
        
    }
}