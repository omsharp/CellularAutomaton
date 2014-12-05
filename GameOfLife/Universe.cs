
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class Universe
    {
        private List<Cell> _cells;

        public int RowsCount    { get; set; }
        public int ColumnsCount { get; set; }

        public IEnumerable<Cell> Cells
        {
            get { return _cells; }
        }

        public Universe(int rows, int columns)
        {
            RowsCount    = rows;
            ColumnsCount = columns;

            Initialize();
        }

        private void Initialize()
        {
            _cells = new List<Cell>();
            
            for (var row = 0; row < RowsCount; row++)
            {
                for (var col = 0; col < ColumnsCount; col++)
                {
                   _cells.Add(new Cell(row,col));
                }
            }
        }

        public Cell this[int row, int column]
        {
            get { return _cells.First(c => c.Row == row && c.Column == column); }
        }

    }
}
