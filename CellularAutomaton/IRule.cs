
namespace CellularAutomaton
{
    public interface IRule
    {
        CellularGrid Transform(CellularGrid cellularGrid);
    }
}
