
namespace CellularAutomaton
{
    public interface IRule
    {
        ICellularGrid Transform(ICellularGrid cellularGrid);
    }
}
