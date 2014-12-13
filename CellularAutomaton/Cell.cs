using System;

namespace CellularAutomaton
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
        /// Gets the status of this cell.
        /// </summary>
        public CellStatus Status { get; private set; }

        private Cell(int row, int column)
        {
            Row        = row;
            Column     = column;
            Generation = 0;
            Status     = CellStatus.Inactive;
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
            if (Status == CellStatus.Alive) 
                throw new InvalidOperationException("You can't revive a cell with Alive status.");

            Status     = CellStatus.Alive;
            Generation = 1;

            TimesRevived++;

            if (Revived != null)
                Revived(this, new EventArgs());
        }
         
        /// <summary>
        /// Kills this cell and sets its Status to Dead and its Generation to 0.
        /// </summary>
        public void Kill()
        {
            if (Status == CellStatus.Dead) 
                throw new InvalidOperationException("You can't kill a cell with Dead status.");

            Status     = CellStatus.Dead;
            Generation = 0;

            TimesKilled++;

            if (Killed != null)
                Killed(this, new EventArgs());
        }

        /// <summary>
        /// Moves this cell to the next generation.
        /// Throws InvalidOperationException if the cell is Inactive or Dead.
        /// </summary>
        public int Evolve()
        {
            if (Status == CellStatus.Inactive)
                throw new InvalidOperationException("You can't evolve an Inactive cell.");

            if (Status == CellStatus.Dead)
                throw new InvalidOperationException("You can't evolve a Dead cell.");

            return ++Generation;
        }

        /// <summary>
        /// Returns a string identifier of this cell.
        /// </summary>
        public override string ToString()
        {
            return string.Format("[{0},{1}]", Row, Column);
        }
    }
}