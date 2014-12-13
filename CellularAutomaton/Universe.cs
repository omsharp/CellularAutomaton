using System;
using System.Collections.Generic;
using System.Linq;

namespace CellularAutomaton
{

    public class Universe
    {
        /// <summary>
        /// Fired after the NextCycle method finishes.
        /// </summary>
        public EventHandler CycleFinished;

        /// <summary>
        /// Gets a grid of all the cells in this universe.
        /// </summary>
        public CellularGrid Grid { get; private set; }

        /// <summary>
        /// Gets the count of all the cycles done so far. 
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// Gets or sets the list of rules to be applied this universe.
        /// </summary>
        public List<IRule> Rules { get; set; }

        
        private Universe(int rowsCount, int columnsCount)
        {
            Age   = 0;
            Grid  = CellularGrid.MakeCellularGrid(rowsCount, columnsCount);
            Rules = new List<IRule>();
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

        private void ApplyRules()
        {
            if(Rules.Count < 1) return;

            //pass a clone of Grid not itself to each rule.
            var transformations = Rules.Select(rule => rule.Transform(Grid.Clone()));

            foreach (var transformation in transformations)
            {
                var touchedCells = transformation.Cells.Where(c => c.Status != Grid[c.Row, c.Column].Status ||
                                                                   c.Generation != Grid[c.Row, c.Column].Generation);

                foreach (var touchedCell in touchedCells)
                {
                    var originalCell = Grid[touchedCell.Row, touchedCell.Column];

                    if (touchedCell.Status == CellStatus.Alive)
                    {
                        originalCell.Revive();
                        continue;
                    }

                    if (touchedCell.Status == CellStatus.Dead)
                    {
                        originalCell.Kill();
                        continue;
                    }

                    if (touchedCell.Generation > originalCell.Generation)
                        originalCell.Evolve();
                }
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
            
            if (CycleFinished != null)
                CycleFinished(this, new EventArgs());
        }

        /// <summary>
        /// Returns a string identifier of this Universe.
        /// </summary>
        public override string ToString()
        {
            return string.Format("Universe with {0} rows and {1} columns.", Grid.RowsCount, Grid.ColumnsCount);
        }

    }

  
}