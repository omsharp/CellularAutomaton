using System;
using System.Collections.Generic;
using System.Linq;

namespace CellularAutomaton
{
    public class Universe
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
            foreach (var cell in _cells)
            {
                foreach (var rule in Rules)
                {
                    rule.Apply(cell);
                }
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
                if (row < 0 || column < 0)
                    throw new IndexOutOfRangeException("You can't use negative values.");   

                if (row >= RowsCount)
                    throw new IndexOutOfRangeException("row is outside of the boundaries of the universe");
                
                if (column >= ColumnsCount)
                    throw new IndexOutOfRangeException("column is outside of the boundaries of the universe");
                
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
        ///Returns a list of all the neighboring cells the selected target cell. 
        /// </summary>
        /// <param name="targetCellRow"></param>
        /// <param name="targetCellColumn"></param>
        /// <returns>IEnumerable of Cell</returns>
        public IEnumerable<Cell> GetNeighboringCells(int targetCellRow, int targetCellColumn)
        {
            var msg = "You can't use negative values.";

            if (targetCellRow < 0)
                throw new ArgumentOutOfRangeException("targetCellRow", msg);
                                                                          
            if (targetCellColumn < 0)                                     
                throw new ArgumentOutOfRangeException("targetCellColumn", msg);

            msg = "Argument is outside of the boundaries of the universe.";

            if (targetCellRow >= RowsCount)
                throw new ArgumentOutOfRangeException("targetCellRow", msg);

            if (targetCellColumn >= ColumnsCount)
                throw new ArgumentOutOfRangeException("targetCellColumn", msg);

            var list = new List<Cell>();
            
            if (Cells.Count() < 2) return list;

            var lastRowInUniverse    = RowsCount - 1;
            var lastColumnInUniverse = ColumnsCount - 1;
            
            //TODO:  Refactor this method when all tests are done. UGLY!

            // if the target cell is surrounded
            if (targetCellRow    > 0 && 
                targetCellColumn > 0 && 
                targetCellRow    < lastRowInUniverse && 
                targetCellColumn < lastColumnInUniverse)
            {
                var rowIndices = new[] {targetCellRow - 1, targetCellRow, targetCellRow + 1};
                var colIndices = new[] {targetCellColumn - 1, targetCellColumn, targetCellColumn + 1};

                for (var rowIndx = 0; rowIndx < 3; rowIndx++)
                {
                    for (var colIndx = 0; colIndx < 3; colIndx++)
                    {
                        // ignore the target cell
                        if (rowIndices[rowIndx] == targetCellRow && 
                            colIndices[colIndx] == targetCellColumn)
                            continue;

                        var currentRow = rowIndices[rowIndx];
                        var currentCol = colIndices[colIndx];

                        // use the indexer to retrive cells
                        list.Add(this[currentRow, currentCol]);
                    }
                }
            }

            return list.AsEnumerable();
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