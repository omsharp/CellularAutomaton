using System.Collections.Generic;

namespace CellularAutomaton
{
    public interface ICellularGrid
    {
        int RowsCount     { get; }
        int ColumnsCount  { get; }
        
        IEnumerable<Cell> Cells { get; }

        IEnumerable<Cell> GetNeighboringCells(int targetRow, int targetColumn);
        IEnumerable<Cell> GetNeighboringCells(Cell targetCell);

        Cell this[int row, int column] { get; }
    }
}
