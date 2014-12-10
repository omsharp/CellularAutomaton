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
        /// Throws ArgumentOutOfRangeException if one of the arguments is out of range of this universe.
        /// </summary>
        /// <param name="targetRow">The row of the target cell.</param>
        /// <param name="targetColumn">The column of the target cell.</param>
        /// <returns>IEnumerable of Cell</returns>
        public IEnumerable<Cell> GetNeighboringCells(int targetRow, int targetColumn)
        {
            if (targetRow < 0)
                throw new ArgumentOutOfRangeException("targetRow", "You can't use negative values.");
                                                                          
            if (targetColumn < 0)                                     
                throw new ArgumentOutOfRangeException("targetColumn", "You can't use negative values.");

            if (targetRow >= RowsCount)
                throw new ArgumentOutOfRangeException("targetRow", "Argument is outside of the boundaries of the universe.");
                                                                         
            if (targetColumn >= ColumnsCount)                        
                throw new ArgumentOutOfRangeException("targetColumn","Argument is outside of the boundaries of the universe.");
            
            // TODO: Still looks ugly! REFACTOR IT. Consider having a Location struct!
            
            var list = new List<Cell>();
            
            var rowBefore = targetRow - 1;
            var rowAfter  = targetRow + 1;

            var columnBefore = targetColumn - 1;
            var columnAfter  = targetColumn + 1;

            var targetRowIsNotTheFirst    = rowBefore >= 0;
            var targetColumnIsNotTheFirst = columnBefore >= 0;

            var targetRowIsNotTheLast    = rowAfter <= RowsCount - 1;
            var targetColumnIsNotTheLast = columnAfter <= ColumnsCount - 1;

            if (targetRowIsNotTheFirst)
            {
                if (targetColumnIsNotTheFirst)
                    list.Add(this[rowBefore, columnBefore]);

                list.Add(this[rowBefore, targetColumn]);

                if (targetColumnIsNotTheLast)
                    list.Add(this[rowBefore, columnAfter]);
            }

            if (targetColumnIsNotTheLast)
                list.Add(this[targetRow, columnAfter]);

            if (targetColumnIsNotTheFirst)
                list.Add(this[targetRow, columnBefore]);

            if (targetRowIsNotTheLast)
            {
                if (targetColumnIsNotTheLast)
                    list.Add(this[rowAfter, columnAfter]);

                list.Add(this[rowAfter, targetColumn]);

                if (targetColumnIsNotTheFirst)
                    list.Add(this[rowAfter, columnBefore]);
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