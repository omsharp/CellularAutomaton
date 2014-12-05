
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace GameOfLife
{
    public class Universe
    {
        private List<Cell> _cells;
        private Timer      _timer;

        public EventHandler OnCycle;

        public int RowsCount     { get; private set; }
        public int ColumnsCount  { get; private set; }
        public int CycleInterval { get; set; }

        public IEnumerable<Cell> Cells
        {
            get { return _cells; }
        }

        public Universe(int rows, int columns, int tickInterval)
        {
            RowsCount     = rows;
            ColumnsCount  = columns;
            CycleInterval = tickInterval;

            InitializeTimer();
            InitializeCells();
        }

        private void InitializeTimer()
        {
            _timer = new Timer();

            _timer.Interval  = CycleInterval;
            _timer.Elapsed   += HandleTimerTick;
            _timer.AutoReset = true;
            _timer.Enabled = true;

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

        private void HandleTimerTick(object sender, ElapsedEventArgs e)
        {
            if (OnCycle != null)
                OnCycle(this, new EventArgs());
        }

        public Cell this[int row, int column]
        {
            get { return _cells.First(c => c.Row == row && c.Column == column); }
        }


        public void Start()
        {
            //_timer.Start();
        }
    }

}
