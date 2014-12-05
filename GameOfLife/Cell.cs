using System;

namespace GameOfLife
{
    public class Cell
    {
        public event EventHandler OnRevived;
        public event EventHandler OnKilled;

        public int TimesKilled  { get; private set; }
        public int TimesRevived { get; private set; }
        public int Row          { get; private set; }
        public int Column       { get; private set; }
        public int Generation   { get; private set; }

        public CellStatus Status { get; private set; }

        public Cell(int row, int column)
        {
            Row        = row;
            Column     = column;
            Generation = 0;
            Status     = CellStatus.Inactive;
        }

        public void Revive()
        {
            if (Status == CellStatus.Alive) return;

            Status     = CellStatus.Alive;
            Generation = 1;
            TimesRevived++;

            if (OnRevived != null)
                OnRevived(this, new EventArgs());
        }

        public void Kill()
        {
            if (Status == CellStatus.Dead) return;

            Status     = CellStatus.Dead;
            Generation = 0;
            TimesKilled++;

            if (OnKilled != null)
                OnKilled(this, new EventArgs());
        }

        public int MoveToNextGeneration()
        {
            return ++Generation;
        }
    }
}