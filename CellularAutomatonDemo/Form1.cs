using System;
using System.Drawing;
using System.Windows.Forms;
using CellularAutomaton;
using System.Linq;

namespace CellularAutomatonDemo
{
    public partial class MainForm : Form
    {
        private const int ROWS    = 140;
        private const int COLUMNS = 250;

        private readonly CellularGrid _game;

        private int maxGen;

        public MainForm()
        {
            _game = new CellularGrid(ROWS, COLUMNS, new InitRule());

            _game.Cycled += (sender, args) =>
            {
                
            };

            foreach (var cell in _game.Cells)
            {
                cell.Evolved += (sender, args) =>
                {
                    var c = (Cell) sender;
                    if (c.Generation > maxGen)
                        maxGen = c.Generation;
                    Text = maxGen.ToString();
                };

            }

            _game.Rules.Add(new GameOfLifeRule1());
            _game.Rules.Add(new GameOfLifeRule2());
            _game.Rules.Add(new GameOfLifeRule3());

            InitializeComponent();

            timer.Interval = 1;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var brush = new SolidBrush(Color.Black);

            const int offset = 4;

            for (var row = 0; row < ROWS; row++)
            {
                for (var col = 0; col < COLUMNS; col++)
                {
                    brush.Color = _game.Cells[row, col].Alive ? Color.Green : Color.Tan;
                    
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
            timer.Enabled = true;
        }
    }

    public class InitRule : IRule
    {

        public bool Condition(Cell cell, CellularGrid grid)
        {
          //  return (cell.Row%4 == 0 || cell.Column%9 == 0 || cell.Row*cell.Column%3 == 0);

           return (cell.Row % 2 == 0 || cell.Column % 3 == 0);

          //  return !cell.Alive;

            return (cell.Column % 8 == 0 || cell.Row % 8 == 0);
        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }
    }


}
