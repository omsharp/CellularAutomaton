using System;
using System.Collections.Generic;
using System.Linq;

namespace CellularAutomaton
{
    [Serializable]
    public class CellularGrid : ICellularGrid
    {
        private List<Cell> _cells;

        /// <summary>
        /// Gets the total count of rows in the univers grid.
        /// </summary>
        public int RowsCount { get; private set; }

        /// <summary>
        /// Gets the total count of columns.
        /// </summary>
        public int ColumnsCount { get; private set; }

        /// <summary>
        /// Gets an IEnumerable of all cellViews.
        /// </summary>
        IEnumerable<ICellView> ICellularContainer.Cells
        {
            get { return Cells; }
        }

        /// <summary>
        /// Gets an IEnumerable of all cells.
        /// </summary>
        public IEnumerable<Cell> Cells
        {
            get { return _cells; }
        }

        private CellularGrid(int rowsCount, int columnsCount)
        {
            RowsCount    = rowsCount;
            ColumnsCount = columnsCount;

            InitializeCells();
        }

        public static CellularGrid MakeCellularGrid(int rowsCount, int columnsCount)
        {
            if (rowsCount < 1 || columnsCount < 1)
                throw new ArgumentException("You can't use less that 1 for rows or columns count.");

            return new CellularGrid(rowsCount, columnsCount);
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

        IEnumerable<ICellView> ICellularContainer.GetNeighboringCells(int targetRow, int targetColumn)
        {
            return GetNeighboringCells(targetRow, targetColumn);
        }

        /// <summary>
        /// Returns a list of all the neighboring cellViews of the specific target cellView. 
        /// Throws ArgumentNullException if the target cell is null.
        /// Throws ArgumentException if the target cell is not 
        /// </summary>
        /// <param name="targetCell">The Cell to get the neighbors of.</param>
        /// <returns>IEnumerable of ICellView</returns>
        IEnumerable<ICellView> ICellularContainer.GetNeighboringCells(ICellView targetCell)
        {
            var target = targetCell as Cell;
            return GetNeighboringCells(target);
        }


        /// <summary>
        /// Returns a string identifier of this CellularGrid.
        /// </summary>
        public override string ToString()
        {
            return string.Format("CellularGrid with {0} rows and {1} columns.",RowsCount, ColumnsCount);
        }
    }
}
