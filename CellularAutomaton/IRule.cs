
using System;
using System.Collections.Generic;

namespace CellularAutomaton
{
    public interface IRule
    {
        IEnumerable<Action> Transform(ICellViewGrid grid);
    }
}
