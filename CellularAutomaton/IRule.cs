

namespace CellularAutomaton
{

    public interface IRule
    {
        bool Condition(Cell cell, CellularGrid grid);

        void Action(Cell cell);
    }

}
