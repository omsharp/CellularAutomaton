

using System.Collections;

namespace CellularAutomaton
{
    public interface IRule
    {
        void Transform(ICellularGrid cellularGrid);
    }
}
