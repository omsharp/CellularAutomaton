using System.Windows.Forms;

namespace CellularAutomatonDemo
{
    public class MyPanel : Panel
    {
        public MyPanel()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
        }
    }
}
