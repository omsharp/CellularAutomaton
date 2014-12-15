using System;
using System.Collections.Generic;

namespace CellularAutomaton
{
    public interface ICellGrid : ICellViewGrid
    {
        IEnumerable<Cell> Cells { get; }

        IEnumerable<Cell> GetNeighboringCells(int targetRow, int targetColumn);
        IEnumerable<Cell> GetNeighboringCells(Cell targetCell);

        new Cell this[int row, int column] { get; }
    }

    public interface ICellViewGrid
    {
        int RowsCount    { get; }
        int ColumnsCount { get; }

        IEnumerable<ICellView> CellViews { get; }

        IEnumerable<ICellView> GetNeighboringCellViews(int targetRow, int targetColumn);
        IEnumerable<ICellView> GetNeighboringCellViews(ICellView targetCell);

        ICellView this[int row, int column] { get; }
    }

    public interface ICellView
    {
        event EventHandler Revived;
        event EventHandler Killed;
        event EventHandler Evolved;

        int  Row        { get; }
        int  Column     { get; }
        int  Generation { get; }
        bool Alive      { get; }

        Action<Cell> ActionToDoNext { get; set; }
    }   
}
