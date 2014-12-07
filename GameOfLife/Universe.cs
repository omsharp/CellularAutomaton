
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class Universe
    {
        private List<Cell> _cells;

        /// <summary>
        /// Fires after the NextCycle method finishes.
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


        public Universe(int rows, int columns)
        {
            Age           = 0;
            RowsCount     = rows;
            ColumnsCount  = columns;
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
                    _cells.Add(new Cell(row, col));
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
                if (row >= RowsCount || column >= ColumnsCount)
                    throw new IndexOutOfRangeException();
                
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
    }

    public interface IRule
    {
        void Apply(Cell cell);
    }
}
