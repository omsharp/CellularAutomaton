using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

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


        private Universe(CellularGrid cellularGrid)
        {
            Age   = 0;
            Grid  = cellularGrid;
            Rules = new List<IRule>();
        }

        /// <summary>
        /// Returns a new Universe object.
        /// Throws ArgumentException if rowsCount or columnsCount is negative.
        /// </summary>
        /// <param name="cellularGrid">ICellularGrid object that holds the grid of cells in this universe.</param>
        public static Universe MakeUniverse(CellularGrid cellularGrid)
        {
            if (cellularGrid == null)
                throw new ArgumentNullException("cellularGrid", "Argument can't be null!");

            return new Universe(cellularGrid);
        }


        private void ApplyRules()
        {
            if (Rules.Count < 1) return;

            var rules = Rules.Select(r =>
            {
                var predicate = r.GetPredicate();
                
                return new {
                             List = Grid.Cells.Where(c => predicate(c, Grid)),
                             Action = r.GetAction()
                           };
            });

            foreach (var rule in rules)
            {
                foreach (var cell in rule.List)
                {
                    rule.Action(cell);
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