using System;
using System.Drawing;
using System.Windows.Forms;
using CellularAutomaton;
using System.Linq;

namespace CellularAutomatonDemo
{
    public partial class MainForm : Form
    {
        private const int ROWS = 60;//200;
        private const int COLUMNS = 60;//200;

        int offset = 10;
        int rows = 0;
        int cols = 0;


        private readonly CellularGrid _game;


        public MainForm()
        {
          
            InitializeComponent();


           rows = panel1.Height / offset;
            cols = panel1.Width / offset;



            _game = new CellularGrid(rows, cols, new InitRule());

            _game.Cycled += (sender, args) =>
            {
                // Text = _game.Survivor.ToString();
            };

            _game.Rules.Add(new GameOfLifeRule1());
            _game.Rules.Add(new GameOfLifeRule2());
            _game.Rules.Add(new GameOfLifeRule3());

            _game.Borderless = true;

            timer.Interval = 1;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var brush = new SolidBrush(Color.Black);

          //  const int offset = 10;

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    if (_game.Cells[row, col].State == CellState.Alive) 
                        brush.Color = Color.Green;
                    else if (_game.Cells[row, col].State == CellState.Inactive)
                        brush.Color = Color.Tan;
                    else
                        brush.Color = Color.Khaki;

                    e.Graphics.FillRectangle(brush, 
                                             offset * col, 
                                             offset * row, 
                                             offset - 1, 
                                             offset - 1);
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //  _grid.NextGeneration();
            _game.NextCycle();
            panel1.Refresh();
        }

        
        private void panel1_Click(object sender, EventArgs e)
        {
          //  _game.NextCycle();
            timer.Enabled = true;
        }
    }

    public class InitRule : IRule
    {

        public bool Condition(Cell cell, CellularGrid grid)
        {
          //return (cell.Row%4 == 0 || cell.Column%9 == 0 || cell.Row*cell.Column%3 == 0);

            var count = grid.RowsCount ;

            var row = cell.Row;
            var col = cell.Column;

            return (row == 0 && col == 2) || (row == 1 && col == 3) || (row == 2 && (col == 1 || col == 2 || col == 3));
            
            return cell.Column == count - 1 && cell.Row > 10 && cell.Row < 100;// grid.ColumnsCount / 2 || cell.Row == grid.RowsCount / 2;

            return (cell.Row == cell.Column) || (cell.Row + cell.Column == count - 1);


            return (cell.Row % 9 == 0 ||  cell.Row * cell.Column % 3 == 0);



         return (cell.Row % 2 == 0 || cell.Column % 3 == 0);

          // return !cell.Alive;

            return (cell.Column % 8 == 0 || cell.Row % 8 == 0);
        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }
    }


}
