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
        /// Gets IEnumerable of all the lists in the universe.
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
            if (rowsCount < 1)
                throw new ArgumentException("You can't use negatives for rows count.");

            if (columnsCount < 1)
                throw new ArgumentException("You can't use negatives for columns count.");

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
                    throw new IndexOutOfRangeException("Can't use negative values for index.");   

                if (row >= RowsCount || column >= ColumnsCount)
                    throw new IndexOutOfRangeException("Row or Column is out of range of this universe's matrix.");
                
                return _cells.First(c => c.Row == row && c.Column == column);
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

        public IEnumerable<Cell> GetNeighborhood(int targetCellRow, int targetCellcolumn)
        {
            if (targetCellRow < 0)
                throw new ArgumentOutOfRangeException("targetCellRow", "You can't use negative values.");

            if (targetCellcolumn < 0)
                throw new ArgumentOutOfRangeException("targetCellcolumn", "You can't use negative values.");

            if (targetCellRow >= RowsCount)
                throw new ArgumentOutOfRangeException("targetCellRow", "Argument is outside of the boundaries of the universe.");

            if (targetCellcolumn >= ColumnsCount)
                throw new ArgumentOutOfRangeException("targetCellcolumn", "Argument is outside of the boundaries of the universe.");    


            var list = new List<Cell>();
            return list;
        }

        public override string ToString()
        {
            return string.Format("Universe with {0} rows and {1} columns.", RowsCount, ColumnsCount);
        }
    }
}
