﻿using System;
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
        public ICellGrid Grid { get; private set; }

        /// <summary>
        /// Gets the count of all the cycles done so far. 
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// Gets or sets the list of rules to be applied this universe.
        /// </summary>
        public List<IRule> Rules { get; set; }


        private Universe(ICellGrid cellularGrid)
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
        public static Universe MakeUniverse(ICellGrid cellularGrid)
        {
            if (cellularGrid == null)
                throw new ArgumentNullException("cellularGrid", "Argument can't be null!");

            return new Universe(cellularGrid);
        }

        private void ApplyRules()
        {
            if (Rules.Count < 1) return;

            var actions = new List<Action>();

            foreach (var rule in Rules)
            {
               actions.AddRange(rule.Transform(Grid));
            }

            foreach (var action in actions)
            {
                action();
            }

            ////pass a clone of Grid to each rule.
            //var transformations = Rules.Select(rule => rule.Transform(Grid.Clone()));

            //foreach (var transformation in transformations)
            //{
            //    var touchedCells = transformation.Cells
            //                                     .Where(c => c.Alive != Grid[c.Row, c.Column].Alive ||
            //                                                 c.Generation != Grid[c.Row, c.Column].Generation);

            //    foreach (var touchedCell in touchedCells)
            //    {
            //        var originalCell = Grid[touchedCell.Row, touchedCell.Column];

            //        if (touchedCell.Alive)
            //            originalCell.Revive();

            //        if (!touchedCell.Alive)
            //            originalCell.Kill();

            //        while (touchedCell.Generation > originalCell.Generation)
            //            originalCell.Evolve();
            //    }
            //}
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