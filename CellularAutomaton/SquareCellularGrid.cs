using System;
using System.Collections.Generic;
using System.Linq;
using CellularAutomaton.Core;

namespace CellularAutomaton
{
    public class SquareCellularGrid : ICellularContainer<SquareCell>
    {
        private List<SquareCell> _cells;

        /// <summary>
        /// Gets the total count of rows.
        /// </summary>
        public int RowsCount { get; private set; }

        /// <summary>
        /// Gets the cells in this grid.
        /// </summary>
        public IEnumerable<SquareCell> Cells { get { return _cells; } }

        /// <summary>
        /// Gets the total count of columns.
        /// </summary>
        public int ColumnsCount { get; private set; }

        
        public SquareCellularGrid(int rowsCount, int columnsCount)
        {
            if (rowsCount < 1 || columnsCount < 1)
                throw new ArgumentException("You can't use less that 1 for rows or columns count.");

            RowsCount = rowsCount;
            ColumnsCount = columnsCount;

            InitializeCells();
        }

        private void InitializeCells()
        {
            _cells = new List<SquareCell>();

            for (var row = 0; row < RowsCount; row++)
            {
                for (var col = 0; col < ColumnsCount; col++)
                {
                    _cells.Add(new SquareCell(row, col));
                }
            }
        }

        /// <summary>
        /// Returns a cell at a specified row and column.
        /// </summary>
        public SquareCell this[int row, int column]
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
        /// Returns a list of all the neighboring cells of the selected target cell. 
        /// Throws ArgumentOutOfRangeException if one of the arguments is out of range.
        /// </summary>
        /// <param name="targetRow">The row of the target cell.</param>
        /// <param name="targetColumn">The column of the target cell.</param>
        /// <returns>IEnumerable of Cell</returns>
        public IEnumerable<SquareCell> GetNeighboringCells(int targetRow, int targetColumn)
        {
            if (targetRow < 0 || targetRow >= RowsCount)
                throw new ArgumentOutOfRangeException("targetRow",
                                                      "Target row was outside the bounds of this grid.");

            if (targetColumn < 0 || targetColumn >= ColumnsCount)
                throw new ArgumentOutOfRangeException("targetColumn",
                                                      "Target column was outside the bounds of this grid.");

            var list = new List<SquareCell>();

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
        /// Throws ArgumentException if the target cell is not part of this grid.
        /// </summary>
        /// <param name="targetCell">The Cell to get the neighbors of.</param>
        /// <returns>IEnumerable of Cell</returns>
        public IEnumerable<SquareCell> GetNeighboringCells(SquareCell targetCell)
        {
            if (targetCell == null)
                throw new ArgumentNullException("targetCell", "Target cell is null!");

            if (_cells.Contains(targetCell) == false)
                throw new ArgumentException("The passed cell is not contained in this grid!");

            return GetNeighboringCells(targetCell.Row, targetCell.Column);
        }

        /// <summary>
        /// Returns a string identifier of this CellularGrid.
        /// </summary>
        public override string ToString()
        {
            return string.Format("Square Grid with {0} rows and {1} columns.", RowsCount, ColumnsCount);
        }


    }
}
