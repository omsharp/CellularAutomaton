using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CellularAutomaton.Core
{

    public interface ICellularContainer<TCell> where TCell : Cell
    {
        IEnumerable<TCell> Cells { get; }

        IEnumerable<TCell> GetNeighboringCells(TCell target);
    }
}

