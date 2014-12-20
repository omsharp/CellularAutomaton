using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CellularAutomaton;
using System.Linq;

namespace CellularAutomatonDemo
{
    public partial class MainForm : Form
    {
        private int _offset;
        private int _rows;
        private int _cols;
        private int _elapsedSeconds;

        private CellularGrid _grid;
        private IRule _initRule;

        public MainForm()
        {

            InitializeComponent();
        }

        private void PopulateInitialRulesCombo()
        {
            initialRuleCombo.Items.Add(new DiagonalLinesInitialRule());
            initialRuleCombo.Items.Add(new AllDeadInitialRule());
            initialRuleCombo.Items.Add(new AllAliveInitialRule());
            initialRuleCombo.Items.Add(new CrossInitialRule());
            initialRuleCombo.Items.Add(new DiagonaCrosslsInitialRule());
            initialRuleCombo.Items.Add(new AlternateRowsGridInitialRule());
            initialRuleCombo.Items.Add(new SingleGliderInitialRule());
            initialRuleCombo.Items.Add(new TightGridInitialRule());
            initialRuleCombo.Items.Add(new WideGridInitialRule());

            initialRuleCombo.SelectedIndex = 0;
        }

        private void GetSize()
        {
            _offset = sizeBar.Value;
            _rows   = canvas.Height / _offset;
            _cols   = canvas.Width / _offset;

            sizeLbl.Text = string.Format("{0} x {1}", _rows, _cols);
        }

        private void MakeNewGrid()
        {
            GetSize();
            GetSpeed();

            _grid = new CellularGrid(_rows,_cols,_initRule);
           
            _grid.WrapBorders = borderCheck.Checked;
            _grid.Cycled += _grid_Cycled;

            AddGameOfLifeRules();

            canvas.Refresh();
        }

        private void GetSpeed()
        {
            timer.Interval = speedBar.Value;
            intervalLbl.Text = string.Format("Cycle Interval = Cycle / {0} Milliseconds", timer.Interval);
        }

        private void AddGameOfLifeRules()
        {
            _grid.Rules.Add(new GameOfLifeRule1());
            _grid.Rules.Add(new GameOfLifeRule2());
            _grid.Rules.Add(new GameOfLifeRule3());

            rulesCheckBox.Items.Clear();

            foreach (var rule in _grid.Rules)
            {
                rulesCheckBox.Items.Add(rule);
            }
        }

        private void DrawGrid(PaintEventArgs e)
        {
            canvas.BackColor = linesColorLbl.BackColor;

            var brush = new SolidBrush(Color.Black);

            for (var row = 0; row < _rows; row++)
            {
                for (var col = 0; col < _cols; col++)
                {
                    switch (_grid.Cells[row, col].State)
                    {
                        case CellState.Alive:
                            brush.Color = aliveColorLbl.BackColor;
                            break;
                        case CellState.Inactive:
                            brush.Color = emptyColorLbl.BackColor;
                            break;
                        default:
                            brush.Color = deadColorLbl.BackColor;
                            break;
                    }

                    e.Graphics.FillRectangle(brush,
                        _offset*col,
                        _offset*row,
                        _offset - 1,
                        _offset - 1);
                }
            }
        }

        private void _grid_Cycled(object sender, EventArgs e)
        {
            cyclesTxt.Text     = _grid.Age.ToString();
            populationTxt.Text = _grid.AliveCount.ToString();
            survivorsTxt.Text  = _grid.LastCycleSurvivors.ToString();
            newbornsTxt.Text   = _grid.LastCycleNewBorns.ToString();
            deathsTxt.Text     = _grid.LastCycleDeaths.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PopulateInitialRulesCombo();
            MakeNewGrid();
        }

        private void sizeBar_Scroll(object sender, EventArgs e)
        {
            GetSize();
            MakeNewGrid();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer.Enabled            = true;
            elapsedTimer.Enabled = true;
            sizeBar.Enabled          = false;
            initialRuleCombo.Enabled = false;
            pauseButton.Enabled      = true;
            startButton.Enabled      = false;

        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Enabled = false;
                elapsedTimer.Enabled = false;
                pauseButton.Text = "Resume";
            }
            else
            {
                timer.Enabled = true;
                elapsedTimer.Enabled = true;
                pauseButton.Text = "Pause";
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            initialRuleCombo.Enabled = true;
            sizeBar.Enabled          = true;
            timer.Enabled            = false;
            elapsedTimer.Enabled     = false;
            startButton.Enabled      = true;
            pauseButton.Enabled      = false;

            elapsedTxt.Text    = "";
            cyclesTxt.Text     = "";
            populationTxt.Text = "";
            survivorsTxt.Text  = "";
            newbornsTxt.Text   = "";
            deathsTxt.Text     = "";

            MakeNewGrid();
        }

        private void speedBar_Scroll(object sender, EventArgs e)
        {
            GetSpeed();
        }

        private void borderCheck_CheckedChanged(object sender, EventArgs e)
        {
            _grid.WrapBorders = borderCheck.Checked;
        }

        private void initialRuleCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _initRule = initialRuleCombo.SelectedItem as IRule;
            MakeNewGrid();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            DrawGrid(e);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _grid.NextCycle();
            canvas.Refresh();
        }

        private void elapsedTimer_Tick(object sender, EventArgs e)
        {
            _elapsedSeconds++;
            elapsedTxt.Text = _elapsedSeconds.ToString();
        }

        private void ColorHandler(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            
            ((Label)sender).BackColor = colorDialog.Color;
            canvas.Refresh();
        }

        private void MouseMoveAndMouseDownHandler(object sender, MouseEventArgs e)
        {
            var col = e.X / _offset;
            var row = e.Y / _offset;

            if ((col >= 0 && col < _grid.ColumnsCount) && (row >= 0 && row < _grid.RowsCount))
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (_grid.Cells[row, col].State != CellState.Alive)
                        _grid.Cells[row, col].Revive();

                    canvas.Refresh();
                }
                else if (e.Button == MouseButtons.Right)
                {

                    if (_grid.Cells[row, col].State == CellState.Alive)
                        _grid.Cells[row, col].Kill();

                    canvas.Refresh();
                }
            }   
        }
    }

   

}
