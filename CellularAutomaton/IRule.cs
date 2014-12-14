
using System;

namespace CellularAutomaton
{
    public interface IRule
    {
        void Transform(ICellularContainer grid);
    }
}
