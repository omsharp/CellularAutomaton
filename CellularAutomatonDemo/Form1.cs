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


namespace CellularAutomatonDemo
{
    public partial class Form1 : Form
    {
        private Universe _universe;

        public Form1()
        {
            _universe = Universe.MakeUniverse(30, 50);

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gridView.Rows.Clear();
            gridView.Columns.Clear();
            
            gridView.ColumnCount = _universe.ColumnsCount;
            gridView.RowCount = _universe.RowsCount;

        }
    }
}
