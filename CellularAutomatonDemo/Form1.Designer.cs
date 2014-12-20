namespace CellularAutomatonDemo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.sizeBar = new System.Windows.Forms.TrackBar();
            this.startButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.speedBar = new System.Windows.Forms.TrackBar();
            this.intervalLbl = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.aliveColorLbl = new System.Windows.Forms.Label();
            this.deadColorLbl = new System.Windows.Forms.Label();
            this.emptyColorLbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linesColorLbl = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.initialRuleCombo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.borderCheck = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.survivorsTxt = new System.Windows.Forms.TextBox();
            this.deathsTxt = new System.Windows.Forms.TextBox();
            this.newbornsTxt = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.sizeLbl = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.elapsedTxt = new System.Windows.Forms.TextBox();
            this.populationTxt = new System.Windows.Forms.TextBox();
            this.cyclesTxt = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.elapsedTimer = new System.Windows.Forms.Timer(this.components);
            this.shapeRecRadio = new System.Windows.Forms.RadioButton();
            this.shapeCircRadio = new System.Windows.Forms.RadioButton();
            this.shapeTriRadio = new System.Windows.Forms.RadioButton();
            this.canvas = new CellularAutomatonDemo.MyPanel();
            ((System.ComponentModel.ISupportInitialize)(this.sizeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // sizeBar
            // 
            this.sizeBar.LargeChange = 1;
            this.sizeBar.Location = new System.Drawing.Point(1, 106);
            this.sizeBar.Maximum = 300;
            this.sizeBar.Minimum = 4;
            this.sizeBar.Name = "sizeBar";
            this.sizeBar.Size = new System.Drawing.Size(365, 45);
            this.sizeBar.TabIndex = 1;
            this.sizeBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.sizeBar.Value = 4;
            this.sizeBar.Scroll += new System.EventHandler(this.sizeBar_Scroll);
            // 
            // startButton
            // 
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.startButton.Font = new System.Drawing.Font("Tahoma", 10F);
            this.startButton.Location = new System.Drawing.Point(747, 198);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.pauseButton.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pauseButton.Location = new System.Drawing.Point(828, 198);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 23);
            this.pauseButton.TabIndex = 3;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.resetButton.Font = new System.Drawing.Font("Tahoma", 10F);
            this.resetButton.Location = new System.Drawing.Point(909, 198);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 4;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(6, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "Size";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // speedBar
            // 
            this.speedBar.LargeChange = 1;
            this.speedBar.Location = new System.Drawing.Point(682, 256);
            this.speedBar.Maximum = 1000;
            this.speedBar.Minimum = 1;
            this.speedBar.Name = "speedBar";
            this.speedBar.Size = new System.Drawing.Size(370, 45);
            this.speedBar.TabIndex = 9;
            this.speedBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.speedBar.Value = 1;
            this.speedBar.Scroll += new System.EventHandler(this.speedBar_Scroll);
            // 
            // intervalLbl
            // 
            this.intervalLbl.Font = new System.Drawing.Font("Tahoma", 10F);
            this.intervalLbl.Location = new System.Drawing.Point(682, 239);
            this.intervalLbl.Name = "intervalLbl";
            this.intervalLbl.Size = new System.Drawing.Size(370, 18);
            this.intervalLbl.TabIndex = 10;
            this.intervalLbl.Text = "Cycle Interval";
            this.intervalLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aliveColorLbl
            // 
            this.aliveColorLbl.BackColor = System.Drawing.Color.Green;
            this.aliveColorLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.aliveColorLbl.Location = new System.Drawing.Point(9, 24);
            this.aliveColorLbl.Name = "aliveColorLbl";
            this.aliveColorLbl.Size = new System.Drawing.Size(42, 25);
            this.aliveColorLbl.TabIndex = 11;
            this.aliveColorLbl.Click += new System.EventHandler(this.ColorHandler);
            // 
            // deadColorLbl
            // 
            this.deadColorLbl.BackColor = System.Drawing.Color.Khaki;
            this.deadColorLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.deadColorLbl.Location = new System.Drawing.Point(9, 53);
            this.deadColorLbl.Name = "deadColorLbl";
            this.deadColorLbl.Size = new System.Drawing.Size(42, 25);
            this.deadColorLbl.TabIndex = 12;
            this.deadColorLbl.Click += new System.EventHandler(this.ColorHandler);
            // 
            // emptyColorLbl
            // 
            this.emptyColorLbl.BackColor = System.Drawing.Color.Tan;
            this.emptyColorLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.emptyColorLbl.Location = new System.Drawing.Point(9, 82);
            this.emptyColorLbl.Name = "emptyColorLbl";
            this.emptyColorLbl.Size = new System.Drawing.Size(42, 25);
            this.emptyColorLbl.TabIndex = 13;
            this.emptyColorLbl.Click += new System.EventHandler(this.ColorHandler);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(57, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 19);
            this.label6.TabIndex = 14;
            this.label6.Text = "Dead Cells";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label7.Location = new System.Drawing.Point(56, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Alive Cells";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label8.Location = new System.Drawing.Point(56, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 18);
            this.label8.TabIndex = 16;
            this.label8.Text = "Empty";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linesColorLbl);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.aliveColorLbl);
            this.groupBox1.Controls.Add(this.emptyColorLbl);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.deadColorLbl);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(682, 523);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 145);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Colors";
            // 
            // linesColorLbl
            // 
            this.linesColorLbl.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.linesColorLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.linesColorLbl.Location = new System.Drawing.Point(9, 111);
            this.linesColorLbl.Name = "linesColorLbl";
            this.linesColorLbl.Size = new System.Drawing.Size(42, 25);
            this.linesColorLbl.TabIndex = 17;
            this.linesColorLbl.Click += new System.EventHandler(this.ColorHandler);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label14.Location = new System.Drawing.Point(56, 115);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 18);
            this.label14.TabIndex = 18;
            this.label14.Text = "Background";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // initialRuleCombo
            // 
            this.initialRuleCombo.BackColor = System.Drawing.Color.White;
            this.initialRuleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.initialRuleCombo.FormattingEnabled = true;
            this.initialRuleCombo.Location = new System.Drawing.Point(6, 47);
            this.initialRuleCombo.Name = "initialRuleCombo";
            this.initialRuleCombo.Size = new System.Drawing.Size(357, 24);
            this.initialRuleCombo.TabIndex = 18;
            this.initialRuleCombo.SelectedIndexChanged += new System.EventHandler(this.initialRuleCombo_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label9.Location = new System.Drawing.Point(6, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(357, 14);
            this.label9.TabIndex = 19;
            this.label9.Text = "Initial Rule";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // borderCheck
            // 
            this.borderCheck.Font = new System.Drawing.Font("Tahoma", 9F);
            this.borderCheck.Location = new System.Drawing.Point(937, 286);
            this.borderCheck.Name = "borderCheck";
            this.borderCheck.Size = new System.Drawing.Size(110, 24);
            this.borderCheck.TabIndex = 20;
            this.borderCheck.Text = "Wrap Borders";
            this.borderCheck.UseVisualStyleBackColor = true;
            this.borderCheck.CheckedChanged += new System.EventHandler(this.borderCheck_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.survivorsTxt);
            this.groupBox2.Controls.Add(this.deathsTxt);
            this.groupBox2.Controls.Add(this.newbornsTxt);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.groupBox2.Location = new System.Drawing.Point(682, 417);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(370, 89);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Latest Cycle Stats";
            // 
            // survivorsTxt
            // 
            this.survivorsTxt.BackColor = System.Drawing.Color.White;
            this.survivorsTxt.Location = new System.Drawing.Point(6, 51);
            this.survivorsTxt.Name = "survivorsTxt";
            this.survivorsTxt.ReadOnly = true;
            this.survivorsTxt.Size = new System.Drawing.Size(112, 24);
            this.survivorsTxt.TabIndex = 36;
            // 
            // deathsTxt
            // 
            this.deathsTxt.BackColor = System.Drawing.Color.White;
            this.deathsTxt.Location = new System.Drawing.Point(252, 51);
            this.deathsTxt.Name = "deathsTxt";
            this.deathsTxt.ReadOnly = true;
            this.deathsTxt.Size = new System.Drawing.Size(112, 24);
            this.deathsTxt.TabIndex = 35;
            // 
            // newbornsTxt
            // 
            this.newbornsTxt.BackColor = System.Drawing.Color.White;
            this.newbornsTxt.Location = new System.Drawing.Point(129, 51);
            this.newbornsTxt.Name = "newbornsTxt";
            this.newbornsTxt.ReadOnly = true;
            this.newbornsTxt.Size = new System.Drawing.Size(112, 24);
            this.newbornsTxt.TabIndex = 34;
            // 
            // label15
            // 
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label15.Location = new System.Drawing.Point(252, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 25);
            this.label15.TabIndex = 33;
            this.label15.Text = "Deaths";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label13.Location = new System.Drawing.Point(6, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 25);
            this.label13.TabIndex = 31;
            this.label13.Text = "Survivors";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label12.Location = new System.Drawing.Point(129, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 25);
            this.label12.TabIndex = 29;
            this.label12.Text = "Newborns";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sizeLbl
            // 
            this.sizeLbl.Font = new System.Drawing.Font("Tahoma", 9F);
            this.sizeLbl.Location = new System.Drawing.Point(15, 133);
            this.sizeLbl.Name = "sizeLbl";
            this.sizeLbl.Size = new System.Drawing.Size(339, 25);
            this.sizeLbl.TabIndex = 23;
            this.sizeLbl.Text = "Rows  x Columns";
            this.sizeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.elapsedTxt);
            this.groupBox3.Controls.Add(this.populationTxt);
            this.groupBox3.Controls.Add(this.cyclesTxt);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.groupBox3.Location = new System.Drawing.Point(682, 310);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(370, 91);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Grid Stats";
            // 
            // elapsedTxt
            // 
            this.elapsedTxt.BackColor = System.Drawing.Color.White;
            this.elapsedTxt.Location = new System.Drawing.Point(6, 53);
            this.elapsedTxt.Name = "elapsedTxt";
            this.elapsedTxt.ReadOnly = true;
            this.elapsedTxt.Size = new System.Drawing.Size(112, 24);
            this.elapsedTxt.TabIndex = 36;
            // 
            // populationTxt
            // 
            this.populationTxt.BackColor = System.Drawing.Color.White;
            this.populationTxt.Location = new System.Drawing.Point(251, 53);
            this.populationTxt.Name = "populationTxt";
            this.populationTxt.ReadOnly = true;
            this.populationTxt.Size = new System.Drawing.Size(112, 24);
            this.populationTxt.TabIndex = 35;
            // 
            // cyclesTxt
            // 
            this.cyclesTxt.BackColor = System.Drawing.Color.White;
            this.cyclesTxt.Location = new System.Drawing.Point(129, 53);
            this.cyclesTxt.Name = "cyclesTxt";
            this.cyclesTxt.ReadOnly = true;
            this.cyclesTxt.Size = new System.Drawing.Size(112, 24);
            this.cyclesTxt.TabIndex = 34;
            // 
            // label16
            // 
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label16.Location = new System.Drawing.Point(251, 32);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 18);
            this.label16.TabIndex = 33;
            this.label16.Text = "Population";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label18.Location = new System.Drawing.Point(1, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(130, 18);
            this.label18.TabIndex = 31;
            this.label18.Text = "Elapsed Time (Secs)";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label19.Location = new System.Drawing.Point(129, 32);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(112, 18);
            this.label19.TabIndex = 29;
            this.label19.Text = "Cycles";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.initialRuleCombo);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.sizeLbl);
            this.groupBox4.Controls.Add(this.sizeBar);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.groupBox4.Location = new System.Drawing.Point(682, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(370, 169);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Initial Setup";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.shapeTriRadio);
            this.groupBox5.Controls.Add(this.shapeCircRadio);
            this.groupBox5.Controls.Add(this.shapeRecRadio);
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.groupBox5.Location = new System.Drawing.Point(840, 523);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(212, 145);
            this.groupBox5.TabIndex = 33;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Cell Shape";
            // 
            // elapsedTimer
            // 
            this.elapsedTimer.Interval = 1000;
            this.elapsedTimer.Tick += new System.EventHandler(this.elapsedTimer_Tick);
            // 
            // shapeRecRadio
            // 
            this.shapeRecRadio.AutoSize = true;
            this.shapeRecRadio.Checked = true;
            this.shapeRecRadio.Location = new System.Drawing.Point(66, 43);
            this.shapeRecRadio.Name = "shapeRecRadio";
            this.shapeRecRadio.Size = new System.Drawing.Size(86, 21);
            this.shapeRecRadio.TabIndex = 0;
            this.shapeRecRadio.TabStop = true;
            this.shapeRecRadio.Text = "Rectangle";
            this.shapeRecRadio.UseVisualStyleBackColor = true;
            this.shapeRecRadio.CheckedChanged += new System.EventHandler(this.shapeCircRadio_CheckedChanged);
            // 
            // shapeCircRadio
            // 
            this.shapeCircRadio.AutoSize = true;
            this.shapeCircRadio.Location = new System.Drawing.Point(66, 72);
            this.shapeCircRadio.Name = "shapeCircRadio";
            this.shapeCircRadio.Size = new System.Drawing.Size(58, 21);
            this.shapeCircRadio.TabIndex = 1;
            this.shapeCircRadio.Text = "Circle";
            this.shapeCircRadio.UseVisualStyleBackColor = true;
            this.shapeCircRadio.CheckedChanged += new System.EventHandler(this.shapeCircRadio_CheckedChanged);
            // 
            // shapeTriRadio
            // 
            this.shapeTriRadio.AutoSize = true;
            this.shapeTriRadio.Location = new System.Drawing.Point(66, 101);
            this.shapeTriRadio.Name = "shapeTriRadio";
            this.shapeTriRadio.Size = new System.Drawing.Size(73, 21);
            this.shapeTriRadio.TabIndex = 2;
            this.shapeTriRadio.Text = "Triangle";
            this.shapeTriRadio.UseVisualStyleBackColor = true;
            this.shapeTriRadio.CheckedChanged += new System.EventHandler(this.shapeCircRadio_CheckedChanged);
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.DarkGray;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(676, 676);
            this.canvas.TabIndex = 0;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAndMouseDownHandler);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAndMouseDownHandler);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1057, 676);
            this.Controls.Add(this.borderCheck);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.intervalLbl);
            this.Controls.Add(this.speedBar);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.canvas);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cellular Automaton - Game of Life";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sizeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private MyPanel canvas;
        private System.Windows.Forms.TrackBar sizeBar;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar speedBar;
        private System.Windows.Forms.Label intervalLbl;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label aliveColorLbl;
        private System.Windows.Forms.Label deadColorLbl;
        private System.Windows.Forms.Label emptyColorLbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox initialRuleCombo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox borderCheck;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label sizeLbl;
        private System.Windows.Forms.TextBox survivorsTxt;
        private System.Windows.Forms.TextBox deathsTxt;
        private System.Windows.Forms.TextBox newbornsTxt;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox elapsedTxt;
        private System.Windows.Forms.TextBox populationTxt;
        private System.Windows.Forms.TextBox cyclesTxt;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label linesColorLbl;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Timer elapsedTimer;
        private System.Windows.Forms.RadioButton shapeTriRadio;
        private System.Windows.Forms.RadioButton shapeCircRadio;
        private System.Windows.Forms.RadioButton shapeRecRadio;
    }
}

