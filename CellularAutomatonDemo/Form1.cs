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
    public partial class MainForm : Form
    {

        public MainForm()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(Color.Red);
            var gfx = panel1.CreateGraphics();

            var rec = new Rectangle(20,20,4,4);

            gfx.DrawRectangle(pen, rec);
        }
    }
}
