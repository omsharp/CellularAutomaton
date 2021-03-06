﻿using System;
using System.Drawing;
using System.Windows.Forms;
using CellularAutomaton;

namespace CellularAutomatonDemo
{
    public partial class MainForm : Form
    {
        readonly SolidBrush _cellBrush = new SolidBrush(Color.Black);

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
            _rows = canvas.Height / _offset;
            _cols = canvas.Width / _offset;

            sizeLbl.Text = $@"Cell Size = {_offset} | Grid Size = {_rows} x {_cols}";
        }

        private void MakeNewGrid()
        {
            GetSize();
            GetSpeed();

            _grid = new CellularGrid(_rows, _cols, _initRule)
            {
                WrapBorders = borderCheck.Checked
            };

            _grid.Cycled += Grid_Cycled;

            AddGameOfLifeRules();

            canvas.Refresh();
        }

        private void GetSpeed()
        {
            timer.Interval = speedBar.Value;
            intervalLbl.Text = $@"Cycle Interval = Cycle / {timer.Interval} Milliseconds";
        }

        private void AddGameOfLifeRules()
        {
            _grid.Rules.Add(new GameOfLifeRule1());
            _grid.Rules.Add(new GameOfLifeRule2());
            _grid.Rules.Add(new GameOfLifeRule3());
        }

        private void DrawGrid(Graphics g)
        {
            canvas.BackColor = linesColorLbl.BackColor;

            for (var row = 0; row < _rows; row++)
            {
                for (var col = 0; col < _cols; col++)
                {
                    switch (_grid.Cells[row, col].State)
                    {
                        case CellState.Alive:
                            _cellBrush.Color = aliveColorLbl.BackColor;
                            break;
                        case CellState.Inactive:
                            _cellBrush.Color = emptyColorLbl.BackColor;
                            break;
                        default:
                            _cellBrush.Color = deadColorLbl.BackColor;
                            break;
                    }

                    if (shapeRecRadio.Checked)
                    {
                        DrawRectangleCell(g, col, row);
                    }
                    else if (shapeCircRadio.Checked)
                    {
                        DrawEllipseCell(g, col, row);
                    }
                    else
                    {
                        DrawTriangleCell(g, col, row);
                    }
                }
            }
        }

        private void DrawRectangleCell(Graphics g, int col, int row)
        {
            g.FillRectangle(_cellBrush,
                _offset * col,
                _offset * row,
                _offset - 1,
                _offset - 1);
        }

        private void DrawEllipseCell(Graphics g, int col, int row)
        {
            g.FillEllipse(_cellBrush,
                _offset * col,
                _offset * row,
                _offset - 1,
                _offset - 1);
        }

        private void DrawTriangleCell(Graphics g, int col, int row)
        {
            var x = col*_offset;
            var y = row*_offset;

            var headPointX = x + _offset/2;
            var basePointY = y + _offset;
            var basePointX = x + _offset - 1;

            var triPoints = new PointF[3];

            triPoints[0] = new PointF(headPointX, y + 1); //head point
            triPoints[1] = new PointF(x + 1, basePointY); //left base point
            triPoints[2] = new PointF(basePointX, basePointY); //right base point

            g.FillPolygon(_cellBrush, triPoints);
        }

        private void Grid_Cycled(object sender, EventArgs e)
        {
            cyclesTxt.Text = _grid.Age.ToString();
            populationTxt.Text = _grid.AliveCount.ToString();
            survivorsTxt.Text = _grid.LastCycleSurvivors.ToString();
            newbornsTxt.Text = _grid.LastCycleNewBorns.ToString();
            deathsTxt.Text = _grid.LastCycleDeaths.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PopulateInitialRulesCombo();
            MakeNewGrid();
        }

        private void SizeBar_Scroll(object sender, EventArgs e)
        {
            GetSize();
            MakeNewGrid();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            elapsedTimer.Enabled = true;
            sizeBar.Enabled = false;
            initialRuleCombo.Enabled = false;
            pauseButton.Enabled = true;
            startButton.Enabled = false;

        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Enabled = false;
                elapsedTimer.Enabled = false;
                pauseButton.Text = @"Resume";
            }
            else
            {
                timer.Enabled = true;
                elapsedTimer.Enabled = true;
                pauseButton.Text = @"Pause";
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            initialRuleCombo.Enabled = true;
            sizeBar.Enabled = true;
            timer.Enabled = false;
            elapsedTimer.Enabled = false;
            startButton.Enabled = true;
            pauseButton.Enabled = false;

            _elapsedSeconds = 0;
            elapsedTxt.Text = "";
            cyclesTxt.Text = "";
            populationTxt.Text = "";
            survivorsTxt.Text = "";
            newbornsTxt.Text = "";
            deathsTxt.Text = "";

            MakeNewGrid();
        }

        private void SpeedBar_Scroll(object sender, EventArgs e)
        {
            GetSpeed();
        }

        private void BorderCheck_CheckedChanged(object sender, EventArgs e)
        {
            _grid.WrapBorders = borderCheck.Checked;
        }

        private void InitialRuleCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _initRule = initialRuleCombo.SelectedItem as IRule;
            MakeNewGrid();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            DrawGrid(e.Graphics);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _grid.NextCycle();
            canvas.Refresh();
        }

        private void ElapsedTimer_Tick(object sender, EventArgs e)
        {
            _elapsedSeconds++;
            elapsedTxt.Text = _elapsedSeconds.ToString();
        }

        private void ColorHandler(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.Cancel)
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
                        _grid.Cells[row, col] = new Cell(row, col);

                    canvas.Refresh();
                }
            }
        }

        private void CellShapeRadio_CheckedChanged(object sender, EventArgs e)
        {
            canvas.Refresh();
        }
    }



}
