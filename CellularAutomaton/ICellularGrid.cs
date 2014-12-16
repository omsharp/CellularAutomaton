using System.Collections.Generic;

namespace CellularAutomaton
{
    public interface ICellularCanvas<T>
    {
        IEnumerable<T> Cells { get; }
        IEnumerable<T> GetNeighboringCells(T target);
    }

    public interface ICellularGrid<T> : ICellularCanvas<T>
    {
        int RowsCount    { get; }
        int ColumnsCount { get; }

        T this[int row, int column] { get; }
    }

    public interface ICellState
    {
        int  Row        { get; }
        int  Column     { get; }
        int  Generation { get; }
        bool Alive      { get; }
    }

    public interface ICell : ICellState
    {
        void Revive();
        void Kill();
        void Evolve();
        void Evolve(uint times);
    }

}

