using CellularAutomaton;

namespace CellularAutomatonDemo
{
    class GameOfLifeRule1 : IRule
    {
        public bool Condition(Cell cell, CellularGrid grid)
        {
            var neighbors = grid.CountAliveNeighbors(cell.Row, cell.Column);

            return (cell.Alive && (neighbors < 2 || neighbors > 3));
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
            var neighbors = grid.CountAliveNeighbors(cell.Row, cell.Column);

            return (cell.Alive && neighbors > 1 && neighbors < 4);
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
            var neighbors = grid.CountAliveNeighbors(cell.Row, cell.Column);

            return (!cell.Alive && neighbors == 3);
        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }
    }
}
