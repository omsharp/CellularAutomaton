using CellularAutomaton;

namespace CellularAutomatonDemo
{
    public class AllDeadInitialRule : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            return (false);
        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }

        public override string ToString()
        {
            return "All Dead";
        }
    }

    public class AllAliveInitialRule : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            return (true);
        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }

        public override string ToString()
        {
            return "All Alive";
        }
    }

    public class CrossInitialRule : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            return (cell.Column % 8 == 0 || cell.Row % 8 == 0);
        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }

        public override string ToString()
        {
            return "Grid with wide squares";
        }
    }

    public class DiagonaCrosslsInitialRule : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            return (cell.Row == cell.Column) || (cell.Row + cell.Column == grid.RowsCount - 1);
        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }

        public override string ToString()
        {
            return "Diagonal Cross";
        }
    }

    public class AlternateRowsGridInitialRule : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            return (cell.Row % 4 == 0 || cell.Column % 9 == 0 || cell.Row * cell.Column % 3 == 0);
        }

        public void Action(Cell cell)
        {
           cell.Revive();
        }

        public override string ToString()
        {
            return "Grid With Alternate Rows.";
        }
    }


    public class SingleGliderInitialRule : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            return (cell.Row == 0 && cell.Column == 2) || 
                   (cell.Row == 1 && cell.Column == 3) || 
                   (cell.Row == 2 && (cell.Column == 1 || cell.Column == 2 || cell.Column == 3));
        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }

        public override string ToString()
        {
            return "Single Glider";
        }
    }

    public class TightGridInitialRule : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            return (cell.Row % 9 == 0 || cell.Row * cell.Column % 3 == 0);
        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }

        public override string ToString()
        {
            return "Grid with tight squares";
        }
    }

    public class WideGridInitialRule : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            return (cell.Column == grid.ColumnsCount / 2 || cell.Row == grid.RowsCount / 2);
        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }

        public override string ToString()
        {
            return "Cross";
        }
    }

    public class DiagonalLinesInitialRule : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            return (cell.Row == cell.Column) || (cell.Row + cell.Column == grid.RowsCount - 1) ||
                    cell.Row == 10 || cell.Row == grid.RowsCount - 10 ||
                    cell.Column == 10 || cell.Column == grid.ColumnsCount - 10;

        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }

        public override string ToString()
        {
            return "....";
        }
    }
}
