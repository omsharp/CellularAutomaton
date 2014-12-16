using System;
using System.Collections.Generic;
using CellularAutomaton.Core;

namespace CellularAutomaton
{
    public class SquareCell : Cell
    {
       
        /// <summary>
        /// Gets the row in which this cell is located.
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// Gets the column in which this cell is located.
        /// </summary>
        public int Column { get; private set; }

        public SquareCell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Returns a string identifier of this cell.
        /// </summary>
        public override string ToString()
        {
            var state = Alive ? "Alive" : "Dead";
            
            return string.Format("[{0},{1}] - {2} - Generation: {3}", Row, Column,state,Generation);
        }

    }
}