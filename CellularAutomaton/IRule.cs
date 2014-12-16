
using System;

namespace CellularAutomaton
{
    public interface IRule
    {
        Func<ICellState, ICellularGrid<ICellState>, bool> GetPredicate();
        Action<Cell> GetAction();
    }
}
