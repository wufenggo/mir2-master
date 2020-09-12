namespace Server.MirForms.Systems
{
    partial class HeroConfigForm
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
            if (disposing && ( components != null ))
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MaxMpBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.MaxHpBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.AccuracyBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.AgilityBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.HpRecoverBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DropRateBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MpRecoverBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ExpRateBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ExpShareBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.NoUnderLevelBox = new System.Windows.Forms.CheckBox();
            this.NoHeroBox = new System.Windows.Forms.CheckBox();
            this.NoMasterBox = new System.Windows.Forms.CheckBox();
            this.NoYellowBox = new System.Windows.Forms.CheckBox();
            this.NoRedBox = new System.Windows.Forms.CheckBox();
            this.NoBrownBox = new System.Windows.Forms.CheckBox();
            this.NoWhiteBox = new System.Windows.Forms.CheckBox();
            this.AllowMasterDeva = new System.Windows.Forms.CheckBox();
            this.AllowMasterSinshu = new System.Windows.Forms.CheckBox();
            this.AllowMasterSkeleton = new System.Windows.Forms.CheckBox();
            this.BagLock4 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.BagLock3 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.BagLock2 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.BagLock1 = new System.Windows.Forms.TextBox();
            this.AllowInventoryDeathDropBox = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.AllowDeathDropBox = new System.Windows.Forms.CheckBox();
            this.AllowArcBox = new System.Windows.Forms.CheckBox();
            this.AllowSinBox = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.ExpBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.HeroExpList = new System.Windows.Forms.ListView();
            this.Level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Experience = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MasterAttackRangeBox = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gold cost to recruit";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(188, 15);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(188, 50);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(132, 22);
            this.textBox2.TabIndex = 3;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Required Level to recruit";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(19, 318);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 298);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Stage:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(4, 26);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(46, 22);
            this.textBox3.TabIndex = 6;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Release lvl:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.MaxMpBox);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.MaxHpBox);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.AccuracyBox);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.AgilityBox);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.HpRecoverBox);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.DropRateBox);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.MpRecoverBox);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.ExpRateBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(19, 348);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 113);
            this.panel1.TabIndex = 8;
            // 
            // MaxMpBox
            // 
            this.MaxMpBox.Location = new System.Drawing.Point(124, 73);
            this.MaxMpBox.Margin = new System.Windows.Forms.Padding(4);
            this.MaxMpBox.Name = "MaxMpBox";
            this.MaxMpBox.Size = new System.Drawing.Size(46, 22);
            this.MaxMpBox.TabIndex = 22;
            this.MaxMpBox.TextChanged += new System.EventHandler(this.MaxMpBox_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(121, 52);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 17);
            this.label11.TabIndex = 23;
            this.label11.Text = "MaxMp";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MaxHpBox
            // 
            this.MaxHpBox.Location = new System.Drawing.Point(124, 21);
            this.MaxHpBox.Margin = new System.Windows.Forms.Padding(4);
            this.MaxHpBox.Name = "MaxHpBox";
            this.MaxHpBox.Size = new System.Drawing.Size(46, 22);
            this.MaxHpBox.TabIndex = 20;
            this.MaxHpBox.TextChanged += new System.EventHandler(this.MaxHpBox_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(121, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 17);
            this.label12.TabIndex = 21;
            this.label12.Text = "MaxHp";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AccuracyBox
            // 
            this.AccuracyBox.Location = new System.Drawing.Point(431, 21);
            this.AccuracyBox.Margin = new System.Windows.Forms.Padding(4);
            this.AccuracyBox.Name = "AccuracyBox";
            this.AccuracyBox.Size = new System.Drawing.Size(46, 22);
            this.AccuracyBox.TabIndex = 18;
            this.AccuracyBox.TextChanged += new System.EventHandler(this.AccuracyBox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(428, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 17);
            this.label10.TabIndex = 19;
            this.label10.Text = "Accuracy";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AgilityBox
            // 
            this.AgilityBox.Location = new System.Drawing.Point(431, 73);
            this.AgilityBox.Margin = new System.Windows.Forms.Padding(4);
            this.AgilityBox.Name = "AgilityBox";
            this.AgilityBox.Size = new System.Drawing.Size(46, 22);
            this.AgilityBox.TabIndex = 16;
            this.AgilityBox.TextChanged += new System.EventHandler(this.AgilityBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(428, 52);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 17);
            this.label9.TabIndex = 17;
            this.label9.Text = "Agility";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // HpRecoverBox
            // 
            this.HpRecoverBox.Location = new System.Drawing.Point(321, 21);
            this.HpRecoverBox.Margin = new System.Windows.Forms.Padding(4);
            this.HpRecoverBox.Name = "HpRecoverBox";
            this.HpRecoverBox.Size = new System.Drawing.Size(46, 22);
            this.HpRecoverBox.TabIndex = 14;
            this.HpRecoverBox.TextChanged += new System.EventHandler(this.HpRecoverBox_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(318, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 17);
            this.label8.TabIndex = 15;
            this.label8.Text = "HpRecover";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DropRateBox
            // 
            this.DropRateBox.Location = new System.Drawing.Point(213, 73);
            this.DropRateBox.Margin = new System.Windows.Forms.Padding(4);
            this.DropRateBox.Name = "DropRateBox";
            this.DropRateBox.Size = new System.Drawing.Size(46, 22);
            this.DropRateBox.TabIndex = 12;
            this.DropRateBox.TextChanged += new System.EventHandler(this.DropRateBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(210, 52);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "DropRate";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MpRecoverBox
            // 
            this.MpRecoverBox.Location = new System.Drawing.Point(321, 73);
            this.MpRecoverBox.Margin = new System.Windows.Forms.Padding(4);
            this.MpRecoverBox.Name = "MpRecoverBox";
            this.MpRecoverBox.Size = new System.Drawing.Size(46, 22);
            this.MpRecoverBox.TabIndex = 10;
            this.MpRecoverBox.TextChanged += new System.EventHandler(this.MpRecoverBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(318, 52);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "MpRecover";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ExpRateBox
            // 
            this.ExpRateBox.Location = new System.Drawing.Point(213, 21);
            this.ExpRateBox.Margin = new System.Windows.Forms.Padding(4);
            this.ExpRateBox.Name = "ExpRateBox";
            this.ExpRateBox.Size = new System.Drawing.Size(46, 22);
            this.ExpRateBox.TabIndex = 8;
            this.ExpRateBox.TextChanged += new System.EventHandler(this.ExpRateBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(210, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "ExpRate";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(337, 18);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 17);
            this.label13.TabIndex = 9;
            this.label13.Text = "Exp share:";
            // 
            // ExpShareBox
            // 
            this.ExpShareBox.Location = new System.Drawing.Point(412, 15);
            this.ExpShareBox.Margin = new System.Windows.Forms.Padding(4);
            this.ExpShareBox.Name = "ExpShareBox";
            this.ExpShareBox.Size = new System.Drawing.Size(27, 22);
            this.ExpShareBox.TabIndex = 10;
            this.ExpShareBox.TextChanged += new System.EventHandler(this.ExpShareBox_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(447, 18);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 17);
            this.label14.TabIndex = 11;
            this.label14.Text = "%";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.AllowMasterDeva);
            this.panel2.Controls.Add(this.AllowMasterSinshu);
            this.panel2.Controls.Add(this.AllowMasterSkeleton);
            this.panel2.Controls.Add(this.BagLock4);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.BagLock3);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.BagLock2);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.BagLock1);
            this.panel2.Controls.Add(this.AllowInventoryDeathDropBox);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.AllowDeathDropBox);
            this.panel2.Controls.Add(this.AllowArcBox);
            this.panel2.Controls.Add(this.AllowSinBox);
            this.panel2.Location = new System.Drawing.Point(23, 79);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(505, 216);
            this.panel2.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label21);
            this.panel4.Controls.Add(this.NoUnderLevelBox);
            this.panel4.Controls.Add(this.NoHeroBox);
            this.panel4.Controls.Add(this.NoMasterBox);
            this.panel4.Controls.Add(this.NoYellowBox);
            this.panel4.Controls.Add(this.NoRedBox);
            this.panel4.Controls.Add(this.NoBrownBox);
            this.panel4.Controls.Add(this.NoWhiteBox);
            this.panel4.Location = new System.Drawing.Point(3, 61);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(313, 152);
            this.panel4.TabIndex = 25;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(113, 1);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(66, 17);
            this.label21.TabIndex = 6;
            this.label21.Text = "PK Rules";
            // 
            // NoUnderLevelBox
            // 
            this.NoUnderLevelBox.AutoSize = true;
            this.NoUnderLevelBox.Location = new System.Drawing.Point(148, 90);
            this.NoUnderLevelBox.Name = "NoUnderLevelBox";
            this.NoUnderLevelBox.Size = new System.Drawing.Size(129, 21);
            this.NoUnderLevelBox.TabIndex = 5;
            this.NoUnderLevelBox.Text = "No Under Level";
            this.NoUnderLevelBox.UseVisualStyleBackColor = true;
            this.NoUnderLevelBox.CheckedChanged += new System.EventHandler(this.NoUnderLevelBox_CheckedChanged);
            // 
            // NoHeroBox
            // 
            this.NoHeroBox.AutoSize = true;
            this.NoHeroBox.Location = new System.Drawing.Point(148, 63);
            this.NoHeroBox.Name = "NoHeroBox";
            this.NoHeroBox.Size = new System.Drawing.Size(83, 21);
            this.NoHeroBox.TabIndex = 4;
            this.NoHeroBox.Text = "No Hero";
            this.NoHeroBox.UseVisualStyleBackColor = true;
            this.NoHeroBox.CheckedChanged += new System.EventHandler(this.NoHeroBox_CheckedChanged);
            // 
            // NoMasterBox
            // 
            this.NoMasterBox.AutoSize = true;
            this.NoMasterBox.Location = new System.Drawing.Point(148, 36);
            this.NoMasterBox.Name = "NoMasterBox";
            this.NoMasterBox.Size = new System.Drawing.Size(95, 21);
            this.NoMasterBox.TabIndex = 3;
            this.NoMasterBox.Text = "No Master";
            this.NoMasterBox.UseVisualStyleBackColor = true;
            this.NoMasterBox.CheckedChanged += new System.EventHandler(this.NoMasterBox_CheckedChanged);
            // 
            // NoYellowBox
            // 
            this.NoYellowBox.AutoSize = true;
            this.NoYellowBox.Location = new System.Drawing.Point(24, 90);
            this.NoYellowBox.Name = "NoYellowBox";
            this.NoYellowBox.Size = new System.Drawing.Size(92, 21);
            this.NoYellowBox.TabIndex = 2;
            this.NoYellowBox.Text = "No Yellow";
            this.NoYellowBox.UseVisualStyleBackColor = true;
            this.NoYellowBox.CheckedChanged += new System.EventHandler(this.NoYellowBox_CheckedChanged);
            // 
            // NoRedBox
            // 
            this.NoRedBox.AutoSize = true;
            this.NoRedBox.Location = new System.Drawing.Point(24, 117);
            this.NoRedBox.Name = "NoRedBox";
            this.NoRedBox.Size = new System.Drawing.Size(78, 21);
            this.NoRedBox.TabIndex = 2;
            this.NoRedBox.Text = "No Red";
            this.NoRedBox.UseVisualStyleBackColor = true;
            this.NoRedBox.CheckedChanged += new System.EventHandler(this.NoRedBox_CheckedChanged);
            // 
            // NoBrownBox
            // 
            this.NoBrownBox.AutoSize = true;
            this.NoBrownBox.Location = new System.Drawing.Point(24, 63);
            this.NoBrownBox.Name = "NoBrownBox";
            this.NoBrownBox.Size = new System.Drawing.Size(91, 21);
            this.NoBrownBox.TabIndex = 1;
            this.NoBrownBox.Text = "No Brown";
            this.NoBrownBox.UseVisualStyleBackColor = true;
            this.NoBrownBox.CheckedChanged += new System.EventHandler(this.NoBrownBox_CheckedChanged);
            // 
            // NoWhiteBox
            // 
            this.NoWhiteBox.AutoSize = true;
            this.NoWhiteBox.Location = new System.Drawing.Point(24, 36);
            this.NoWhiteBox.Name = "NoWhiteBox";
            this.NoWhiteBox.Size = new System.Drawing.Size(88, 21);
            this.NoWhiteBox.TabIndex = 0;
            this.NoWhiteBox.Text = "No White";
            this.NoWhiteBox.UseVisualStyleBackColor = true;
            this.NoWhiteBox.CheckedChanged += new System.EventHandler(this.NoWhiteBox_CheckedChanged);
            // 
            // AllowMasterDeva
            // 
            this.AllowMasterDeva.AutoSize = true;
            this.AllowMasterDeva.Location = new System.Drawing.Point(337, 183);
            this.AllowMasterDeva.Name = "AllowMasterDeva";
            this.AllowMasterDeva.Size = new System.Drawing.Size(146, 21);
            this.AllowMasterDeva.TabIndex = 24;
            this.AllowMasterDeva.Text = "Allow Master Deva";
            this.AllowMasterDeva.UseVisualStyleBackColor = true;
            this.AllowMasterDeva.CheckedChanged += new System.EventHandler(this.AllowMasterDeva_CheckedChanged);
            // 
            // AllowMasterSinshu
            // 
            this.AllowMasterSinshu.AutoSize = true;
            this.AllowMasterSinshu.Location = new System.Drawing.Point(337, 156);
            this.AllowMasterSinshu.Name = "AllowMasterSinshu";
            this.AllowMasterSinshu.Size = new System.Drawing.Size(156, 21);
            this.AllowMasterSinshu.TabIndex = 23;
            this.AllowMasterSinshu.Text = "Allow Master Sinshu";
            this.AllowMasterSinshu.UseVisualStyleBackColor = true;
            this.AllowMasterSinshu.CheckedChanged += new System.EventHandler(this.AllowMasterSinshu_CheckedChanged);
            // 
            // AllowMasterSkeleton
            // 
            this.AllowMasterSkeleton.AutoSize = true;
            this.AllowMasterSkeleton.Location = new System.Drawing.Point(337, 129);
            this.AllowMasterSkeleton.Name = "AllowMasterSkeleton";
            this.AllowMasterSkeleton.Size = new System.Drawing.Size(168, 21);
            this.AllowMasterSkeleton.TabIndex = 22;
            this.AllowMasterSkeleton.Text = "Allow Master Skeleton";
            this.AllowMasterSkeleton.UseVisualStyleBackColor = true;
            this.AllowMasterSkeleton.CheckedChanged += new System.EventHandler(this.AllowMasterSkeleton_CheckedChanged);
            // 
            // BagLock4
            // 
            this.BagLock4.Location = new System.Drawing.Point(437, 86);
            this.BagLock4.Margin = new System.Windows.Forms.Padding(4);
            this.BagLock4.Name = "BagLock4";
            this.BagLock4.Size = new System.Drawing.Size(47, 22);
            this.BagLock4.TabIndex = 21;
            this.BagLock4.TextChanged += new System.EventHandler(this.BagLock4_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(344, 89);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(84, 17);
            this.label20.TabIndex = 20;
            this.label20.Text = "BagUnlock4";
            // 
            // BagLock3
            // 
            this.BagLock3.Location = new System.Drawing.Point(437, 58);
            this.BagLock3.Margin = new System.Windows.Forms.Padding(4);
            this.BagLock3.Name = "BagLock3";
            this.BagLock3.Size = new System.Drawing.Size(47, 22);
            this.BagLock3.TabIndex = 19;
            this.BagLock3.TextChanged += new System.EventHandler(this.BagLock3_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(344, 61);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 17);
            this.label19.TabIndex = 18;
            this.label19.Text = "BagUnlock3";
            // 
            // BagLock2
            // 
            this.BagLock2.Location = new System.Drawing.Point(437, 31);
            this.BagLock2.Margin = new System.Windows.Forms.Padding(4);
            this.BagLock2.Name = "BagLock2";
            this.BagLock2.Size = new System.Drawing.Size(47, 22);
            this.BagLock2.TabIndex = 17;
            this.BagLock2.TextChanged += new System.EventHandler(this.BagLock2_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(344, 34);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(84, 17);
            this.label18.TabIndex = 16;
            this.label18.Text = "BagUnlock2";
            // 
            // BagLock1
            // 
            this.BagLock1.Location = new System.Drawing.Point(437, 4);
            this.BagLock1.Margin = new System.Windows.Forms.Padding(4);
            this.BagLock1.Name = "BagLock1";
            this.BagLock1.Size = new System.Drawing.Size(47, 22);
            this.BagLock1.TabIndex = 15;
            this.BagLock1.TextChanged += new System.EventHandler(this.BagLock1_TextChanged);
            // 
            // AllowInventoryDeathDropBox
            // 
            this.AllowInventoryDeathDropBox.AutoSize = true;
            this.AllowInventoryDeathDropBox.Location = new System.Drawing.Point(95, 30);
            this.AllowInventoryDeathDropBox.Name = "AllowInventoryDeathDropBox";
            this.AllowInventoryDeathDropBox.Size = new System.Drawing.Size(165, 21);
            this.AllowInventoryDeathDropBox.TabIndex = 3;
            this.AllowInventoryDeathDropBox.Text = "Inventory Death Drop";
            this.AllowInventoryDeathDropBox.UseVisualStyleBackColor = true;
            this.AllowInventoryDeathDropBox.CheckedChanged += new System.EventHandler(this.AllowInventoryDeathDropBox_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(344, 7);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 17);
            this.label17.TabIndex = 14;
            this.label17.Text = "BagUnlock1";
            // 
            // AllowDeathDropBox
            // 
            this.AllowDeathDropBox.AutoSize = true;
            this.AllowDeathDropBox.Location = new System.Drawing.Point(95, 3);
            this.AllowDeathDropBox.Name = "AllowDeathDropBox";
            this.AllowDeathDropBox.Size = new System.Drawing.Size(103, 21);
            this.AllowDeathDropBox.TabIndex = 2;
            this.AllowDeathDropBox.Text = "Death Drop";
            this.AllowDeathDropBox.UseVisualStyleBackColor = true;
            this.AllowDeathDropBox.CheckedChanged += new System.EventHandler(this.AllowDeathDropBox_CheckedChanged);
            // 
            // AllowArcBox
            // 
            this.AllowArcBox.AutoSize = true;
            this.AllowArcBox.Location = new System.Drawing.Point(3, 30);
            this.AllowArcBox.Name = "AllowArcBox";
            this.AllowArcBox.Size = new System.Drawing.Size(87, 21);
            this.AllowArcBox.TabIndex = 1;
            this.AllowArcBox.Text = "Allow Arc";
            this.AllowArcBox.UseVisualStyleBackColor = true;
            this.AllowArcBox.CheckedChanged += new System.EventHandler(this.AllowArcBox_CheckedChanged);
            // 
            // AllowSinBox
            // 
            this.AllowSinBox.AutoSize = true;
            this.AllowSinBox.Location = new System.Drawing.Point(3, 3);
            this.AllowSinBox.Name = "AllowSinBox";
            this.AllowSinBox.Size = new System.Drawing.Size(86, 21);
            this.AllowSinBox.TabIndex = 0;
            this.AllowSinBox.Text = "Allow Sin";
            this.AllowSinBox.UseVisualStyleBackColor = true;
            this.AllowSinBox.CheckedChanged += new System.EventHandler(this.AllowSinBox_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.ExpBox);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.HeroExpList);
            this.panel3.Location = new System.Drawing.Point(553, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(255, 449);
            this.panel3.TabIndex = 13;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(84, 388);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 17);
            this.label16.TabIndex = 3;
            this.label16.Text = "Experience:";
            // 
            // ExpBox
            // 
            this.ExpBox.Location = new System.Drawing.Point(61, 409);
            this.ExpBox.Name = "ExpBox";
            this.ExpBox.Size = new System.Drawing.Size(126, 22);
            this.ExpBox.TabIndex = 2;
            this.ExpBox.TextChanged += new System.EventHandler(this.ExpBox_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(74, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(113, 17);
            this.label15.TabIndex = 1;
            this.label15.Text = "Hero Experience";
            // 
            // HeroExpList
            // 
            this.HeroExpList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Level,
            this.Experience});
            this.HeroExpList.FullRowSelect = true;
            this.HeroExpList.GridLines = true;
            this.HeroExpList.Location = new System.Drawing.Point(13, 38);
            this.HeroExpList.MultiSelect = false;
            this.HeroExpList.Name = "HeroExpList";
            this.HeroExpList.Size = new System.Drawing.Size(233, 340);
            this.HeroExpList.TabIndex = 0;
            this.HeroExpList.UseCompatibleStateImageBehavior = false;
            this.HeroExpList.View = System.Windows.Forms.View.Details;
            this.HeroExpList.SelectedIndexChanged += new System.EventHandler(this.HeroExpList_SelectedIndexChanged);
            // 
            // Level
            // 
            this.Level.Text = "Level";
            this.Level.Width = 83;
            // 
            // Experience
            // 
            this.Experience.Text = "Experience";
            this.Experience.Width = 148;
            // 
            // MasterAttackRangeBox
            // 
            this.MasterAttackRangeBox.Location = new System.Drawing.Point(43, 497);
            this.MasterAttackRangeBox.Margin = new System.Windows.Forms.Padding(4);
            this.MasterAttackRangeBox.Name = "MasterAttackRangeBox";
            this.MasterAttackRangeBox.Size = new System.Drawing.Size(47, 22);
            this.MasterAttackRangeBox.TabIndex = 15;
            this.MasterAttackRangeBox.TextChanged += new System.EventHandler(this.MasterAttackRangeBox_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(4, 476);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(140, 17);
            this.label22.TabIndex = 14;
            this.label22.Text = "Master Attack Range";
            // 
            // HeroConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 545);
            this.Controls.Add(this.MasterAttackRangeBox);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.ExpShareBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HeroConfigForm";
            this.Text = "HeroConfigForm Created by Edens-Elite[Pete107]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HeroConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.HeroConfigForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox MaxMpBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox MaxHpBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox AccuracyBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox AgilityBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox HpRecoverBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox DropRateBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox MpRecoverBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ExpRateBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox ExpShareBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox AllowArcBox;
        private System.Windows.Forms.CheckBox AllowSinBox;
        private System.Windows.Forms.CheckBox AllowInventoryDeathDropBox;
        private System.Windows.Forms.CheckBox AllowDeathDropBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListView HeroExpList;
        private System.Windows.Forms.ColumnHeader Level;
        private System.Windows.Forms.ColumnHeader Experience;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox ExpBox;
        private System.Windows.Forms.TextBox BagLock4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox BagLock3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox BagLock2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox BagLock1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox AllowMasterDeva;
        private System.Windows.Forms.CheckBox AllowMasterSinshu;
        private System.Windows.Forms.CheckBox AllowMasterSkeleton;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox NoUnderLevelBox;
        private System.Windows.Forms.CheckBox NoHeroBox;
        private System.Windows.Forms.CheckBox NoMasterBox;
        private System.Windows.Forms.CheckBox NoYellowBox;
        private System.Windows.Forms.CheckBox NoRedBox;
        private System.Windows.Forms.CheckBox NoBrownBox;
        private System.Windows.Forms.CheckBox NoWhiteBox;
        private System.Windows.Forms.TextBox MasterAttackRangeBox;
        private System.Windows.Forms.Label label22;
    }
}