using CellularAutomaton;

namespace CellularAutomatonDemo
{
    class GameOfLifeRule1 : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            var neighborsAlive = grid.CountAliveNeighbors(cell.Row, cell.Column);

            return (cell.State == CellState.Alive && (neighborsAlive < 2 || neighborsAlive > 3));
        }

        public void Action(Cell cell)
        {
            cell.Kill();
        }
    }

    class GameOfLifeRule2 : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            var neighborsAlive = grid.CountAliveNeighbors(cell.Row, cell.Column);

            return (cell.State == CellState.Alive && neighborsAlive > 1 && neighborsAlive < 4);
        }

        public void Action(Cell cell)
        {
            cell.Evolve();
        }
    }

    class GameOfLifeRule3 : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            var neighborsAlive = grid.CountAliveNeighbors(cell.Row, cell.Column);

            return (cell.State != CellState.Alive && neighborsAlive == 3);
        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }
    }
}
