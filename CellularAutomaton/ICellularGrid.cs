using System;
using System.Collections.Generic;

namespace CellularAutomaton
{
    public interface ICellularGrid : ICellularContainer
    {
        new IEnumerable<Cell> Cells { get; }

        new IEnumerable<Cell> GetNeighboringCells(int targetRow, int targetColumn);
            IEnumerable<Cell> GetNeighboringCells(Cell targetCell);

        new Cell this[int row, int column] { get; }
    }

    public interface ICellularContainer
    {
        int RowsCount    { get; }
        int ColumnsCount { get; }

        IEnumerable<ICellView> Cells { get; }

        IEnumerable<ICellView> GetNeighboringCells(int targetRow, int targetColumn);
        IEnumerable<ICellView> GetNeighboringCells(ICellView targetCell);

        Cell this[int row, int column] { get; }
    }

    public interface ICellView
    {
        int  Row        { get; }
        int  Column     { get; }
        int  Generation { get; }
        bool Alive      { get; }

        Action<Cell> Action { get; set; }
    }   
}
