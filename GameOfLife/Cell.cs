using System;

namespace GameOfLife
{
    public class Cell
    {
        /// <summary>
        /// Fired whenever this cell is revived.
        /// </summary>
        public event EventHandler Revived;

        /// <summary>
        /// Fired whenever this cell is killed.
        /// </summary>
        public event EventHandler Killed;

        /// <summary>
        /// Gets the count of times this cell is killed.
        /// </summary>
        public int TimesKilled  { get; private set; }

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
        /// Gets the status of this cell.
        /// </summary>
        public CellStatus Status { get; private set; }

        /// <param name="row">The row in which this cell should be located</param>
        /// <param name="column">The column in which this cell should be located</param>
        public Cell(int row, int column)
        {
            Row        = row;
            Column     = column;
            Generation = 0;
            Status     = CellStatus.Inactive;
        }

        /// <summary>
        /// Revives this cell and sets its generation to 1.
        /// </summary>
        public void Revive()
        {
            if (Status == CellStatus.Alive) return;

            Status     = CellStatus.Alive;
            Generation = 1;

            TimesRevived++;

            if (Revived != null)
                Revived(this, new EventArgs());
        }

        /// <summary>
        /// Kills this cell and sets its generation to 0.
        /// </summary>
        public void Kill()
        {
            if (Status == CellStatus.Dead) return;

            Status     = CellStatus.Dead;
            Generation = 0;

            TimesKilled++;

            if (Killed != null)
                Killed(this, new EventArgs());
        }

        /// <summary>
        /// Moves this cell to the next generation.
        /// </summary>
        public int MoveToNextGeneration()
        {
            return ++Generation;
        }

        /// <summary>
        /// Returns a string identifier of this cell, based on its Row and Column.
        /// </summary>
        public override string ToString()
        {
            return string.Format("[{0},{1}]", Row, Column);
        }
    }
}