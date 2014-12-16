using System;
using System.Collections.Generic;

namespace CellularAutomaton
{
    [Serializable]
    public class Cell : ICell
    {
        /// <summary>
        /// Fired after the cell is revived.
        /// </summary>
        public event EventHandler Revived;

        /// <summary>
        /// Fired after the cell is killed.
        /// </summary>
        public event EventHandler Killed;

        /// <summary>
        /// Fired after the cell evolves.
        /// </summary>
        public event EventHandler Evolved;
        
        /// <summary>
        /// Gets the count of times this cell is killed.
        /// </summary>
        public int TimesKilled { get; private set; }

        /// <summary>
        /// Gets the count of times this cell is revived.
        /// </summary>
        public int TimesRevived { get; private set; }

        /// <summary>
        /// Gets the row in which this cell is located.
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// Gets the column in which this cell is located.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Gets the current generation of this cell.
        /// </summary>
        public int Generation { get; private set; }

        /// <summary>
        /// Returns true if the cell is alive.
        /// </summary>
        public bool Alive { get; private set; }

        private Cell(int row, int column)
        {
            Row        = row;
            Column     = column;
            Generation = 0;
            Alive      = false;
        }

        /// <summary>
        /// Returns a new Cell object.
        /// Throws ArgumentException if row or column is negative.
        /// </summary>
        /// <param name="row">The row in which the cell should be located.</param>
        /// <param name="column">The column in which the cell should be located.</param>
        /// <returns></returns>
        public static Cell MakeCell(int row, int column)
        {
            if (row < 0 || column < 0)
                throw new ArgumentException("You can't use negatives for row or column.");

            return new Cell(row,column);
        }

        /// <summary>
        /// Revives this cell and sets its Status to Alive and its Generation to 1.
        /// </summary>
        public void Revive()
        {
            if (Alive) 
                throw new InvalidOperationException("You can't revive an Alive cell.");

            Alive       = true;
            Generation  = 1;

            TimesRevived++;

            if (Revived != null)
                Revived(this, new EventArgs());
        }
         
        /// <summary>
        /// Kills this cell and sets its Status to Dead and its Generation to 0.
        /// </summary>
        public void Kill()
        {
            if (!Alive) 
                throw new InvalidOperationException("You can't kill a Dead cell.");

            Alive      = false;
            Generation = 0;

            TimesKilled++;

            if (Killed != null)
                Killed(this, new EventArgs());
        }

        /// <summary>
        /// Evolves this cell a number of times.
        /// Throws InvalidOperationException if the cell is Dead.
        /// </summary>
        /// <param name="times">The number of times to evolve.</param>
        public void Evolve(uint times)
        {
            if (!Alive)
                throw new InvalidOperationException("You can't evolve a Dead cell.");

            Generation += (int)times;

            if (Evolved != null)
                Evolved(this, new EventArgs());
        }
       
        /// <summary>
        /// Evolves this cell to the next generation.
        /// Throws InvalidOperationException if the cell is Dead.
        /// </summary>
        public void Evolve()
        {
            Evolve(1);
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