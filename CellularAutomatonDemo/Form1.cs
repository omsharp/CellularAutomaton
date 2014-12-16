using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CellularAutomaton;
using CellularAutomaton.Core;

namespace CellularAutomatonDemo
{
    public partial class MainForm : Form
    {
        private const int ROWS = 50; //140;
        private const int COLUMNS = 50; //250;

        private readonly Universe<SquareCell, SquareCellularGrid> _universe; 

        public MainForm()
        {

            var initRule = Rule<SquareCell, SquareCellularGrid>.MakeRule("Init")
                .WhenTrue((c, g) => c.Row % 2 == 0 || c.Column % 3 == 0)
                .Do(c => c.Revive());
                 
            _universe = new Universe<SquareCell, SquareCellularGrid>(new SquareCellularGrid(ROWS, COLUMNS),initRule);

            Initialize();

            InitializeComponent();
        }

        private void Initialize()
        {
            var rule1 = _universe.MakeNewRule("Rule-2")
                .WhenTrue((c, g) =>
                {
                    var neighbors = g.GetNeighboringCells(c).Count(n => n.Alive);
                    return c.Alive && (neighbors < 2 || neighbors > 3);
                })
                .Do(c => c.Kill());

            var rule2 = _universe.MakeNewRule("Rule-2")
                .WhenTrue((c, g) =>
                {
                    var neighbors = g.GetNeighboringCells(c).Count(n => n.Alive);
                    return c.Alive && neighbors > 1 && neighbors < 4;
                })
                .Do(c => c.Evolve());

            var rule4 = _universe.MakeNewRule("Rule-3")
                .WhenTrue((c, g) => !c.Alive && g.GetNeighboringCells(c).Count(n => n.Alive) == 3)
                .Do(c => c.Revive());

            _universe.Rules.Add(rule1);
            _universe.Rules.Add(rule2);
            _universe.Rules.Add(rule4);

            _universe.CycleFinished += (sender, args) => Text = _universe.Age.ToString();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var gfx = panel1.CreateGraphics();
            var brush = new SolidBrush(Color.Red);

            foreach (var cell in _universe.Grid.Cells.Where(c => c.Alive))
            {

                // Row and Column are inverted in respect to X and y
                // Row is Y  ....    Column is X
                var y = cell.Row == 0 ? 0 : cell.Row * 4;
                var x = cell.Column == 0 ? 0 : cell.Column * 4;

                var recf = new RectangleF(x, y, 4f, 4f);

                gfx.FillRectangle(brush, recf);
            }

        }

        private void timer_Tick(object sender, EventArgs e)
        {

            _universe.NextCycle();
            panel1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
