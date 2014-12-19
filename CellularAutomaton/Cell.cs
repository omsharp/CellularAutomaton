using System;

namespace CellularAutomaton
{
    public enum CellState
    {
        Inactive,
        Dead,
        Alive
    }

    public class Cell
    {
        /// <summary>
        /// Fired after the cell is revived.
        /// </summary>
        //public event EventHandler Revived;

        /// <summary>
        /// Fired after the cell is killed.
        /// </summary>
        //public event EventHandler Killed;

        /// <summary>
        /// Fired after the cell evolves.
        /// </summary>
        //public event EventHandler Evolved;

        /// <summary>
        /// Gets the count of times this cell is killed.
        /// </summary>
        public int TimesKilled { get; private set; }

        /// <summary>
        /// Gets the count of times this cell is revived.
        /// </summary>
        public int TimesRevived { get; private set; }

        /// <summary>
        /// Gets the current generation of this cell.
        /// </summary>
        public int Generation { get; private set; }

        /// <summary>
        /// Returns true if the cell is alive.
        /// </summary>
        public CellState State { get; private set; }

        /// <summary>
        /// Returns the row in which this cell is located
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// Gets the column in which this cell is located.
        /// </summary>
        public int Column { get; private set; }


        public Cell(int row, int column)
        {
            Row        = row;
            Column     = column;
            State      = CellState.Inactive;
            Generation = 0;
        }

        /// <summary>
        /// Revives this cell and sets its Status to Alive and its Generation to 1.
        /// </summary>
        public void Revive()
        {
            if (State == CellState.Alive)
                throw new InvalidOperationException("You can't revive an already Alive cell!");

            State        = CellState.Alive;
            Generation   = 1;
            TimesRevived ++;

            //if (Revived != null)
            //    Revived(this, new EventArgs());
        }

        /// <summary>
        /// Kills this cell and sets its Status to Dead and its Generation to 0.
        /// </summary>
        public void Kill()
        {
            if (State == CellState.Dead)
                throw new InvalidOperationException("You can't kill a Dead cell!");

            if (State == CellState.Inactive)
                throw new InvalidOperationException("You can't kill an Inactive cell!");

            State       = CellState.Dead;
            Generation  = 0;
            TimesKilled ++;

            //if (Killed != null)
            //    Killed(this, new EventArgs());
        }

        /// <summary>
        /// Evolves this cell a number of times.
        /// Throws InvalidOperationException if the cell is Dead.
        /// </summary>
        /// <param name="times">The number of times to evolve.</param>
        public void EvolveFor(int times)
        {
            if(times < 1)
                throw new ArgumentException("Argument must be greater than 0!");

            if (State == CellState.Dead)
                throw new InvalidOperationException("You can't evolve a Dead cell!");

            if (State == CellState.Inactive)
                throw new InvalidOperationException("You can't evolve an Inactive cell!");

            Generation += times;

            //if (Evolved != null)
            //    Evolved(this, new EventArgs());
        }

        /// <summary>
        /// Evolves this cell to the next generation.
        /// Throws InvalidOperationException if the cell is Dead.
        /// </summary>
        public void Evolve()
        {
            EvolveFor(1);
        }

        /// <summary>
        /// Returns a string identifier of this cell.
        /// </summary>
        public override string ToString()
        {
            return string.Format("[{0},{1}] - {2} - Generation: {3}", Row, Column, State, Generation);
        }

        /// <summary>
        /// Returns a clone of this cell.
        /// </summary>
        public Cell Clone()
        {
            /* Simple but works. :)  */
            var clone = new Cell(Row, Column)
            {
                State        = State,
                Generation   = Generation,
                TimesKilled  = TimesKilled,
                TimesRevived = TimesRevived,
            };

            //clone.Killed  = Killed;
            //clone.Revived = Revived;
            //clone.Evolved = Evolved;

            return clone;
        }
    }
}
