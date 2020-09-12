namespace Server.MirForms.Systems
{
    partial class ConquestBuffForm
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
            this.classCBox = new System.Windows.Forms.ComboBox();
            this.classStatPanel = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.expTBox = new System.Windows.Forms.TextBox();
            this.dropTBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.minACBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.maxACBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.maxMACBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.minMACBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.maxDCBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.minDCBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.maxMCBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.minMCBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.maxSCBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.minSCBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.agilityBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.accuracyBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.mpBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.hpBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.aspeedBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.classStatPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // classCBox
            // 
            this.classCBox.FormattingEnabled = true;
            this.classCBox.Location = new System.Drawing.Point(242, 26);
            this.classCBox.Name = "classCBox";
            this.classCBox.Size = new System.Drawing.Size(121, 21);
            this.classCBox.TabIndex = 0;
            this.classCBox.SelectedIndexChanged += new System.EventHandler(this.classCBox_SelectedIndexChanged);
            // 
            // classStatPanel
            // 
            this.classStatPanel.Controls.Add(this.aspeedBox);
            this.classStatPanel.Controls.Add(this.label19);
            this.classStatPanel.Controls.Add(this.mpBox);
            this.classStatPanel.Controls.Add(this.label17);
            this.classStatPanel.Controls.Add(this.hpBox);
            this.classStatPanel.Controls.Add(this.label18);
            this.classStatPanel.Controls.Add(this.agilityBox);
            this.classStatPanel.Controls.Add(this.label15);
            this.classStatPanel.Controls.Add(this.accuracyBox);
            this.classStatPanel.Controls.Add(this.label16);
            this.classStatPanel.Controls.Add(this.maxSCBox);
            this.classStatPanel.Controls.Add(this.label13);
            this.classStatPanel.Controls.Add(this.minSCBox);
            this.classStatPanel.Controls.Add(this.label14);
            this.classStatPanel.Controls.Add(this.maxMCBox);
            this.classStatPanel.Controls.Add(this.label11);
            this.classStatPanel.Controls.Add(this.minMCBox);
            this.classStatPanel.Controls.Add(this.label12);
            this.classStatPanel.Controls.Add(this.maxDCBox);
            this.classStatPanel.Controls.Add(this.label9);
            this.classStatPanel.Controls.Add(this.minDCBox);
            this.classStatPanel.Controls.Add(this.label10);
            this.classStatPanel.Controls.Add(this.maxMACBox);
            this.classStatPanel.Controls.Add(this.label7);
            this.classStatPanel.Controls.Add(this.minMACBox);
            this.classStatPanel.Controls.Add(this.label8);
            this.classStatPanel.Controls.Add(this.maxACBox);
            this.classStatPanel.Controls.Add(this.label6);
            this.classStatPanel.Controls.Add(this.minACBox);
            this.classStatPanel.Controls.Add(this.label5);
            this.classStatPanel.Location = new System.Drawing.Point(153, 53);
            this.classStatPanel.Name = "classStatPanel";
            this.classStatPanel.Size = new System.Drawing.Size(210, 223);
            this.classStatPanel.TabIndex = 1;
            this.classStatPanel.TabStop = false;
            this.classStatPanel.Text = "Class Stats";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Class";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(12, 253);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 0;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "EXP";
            // 
            // expTBox
            // 
            this.expTBox.Location = new System.Drawing.Point(56, 65);
            this.expTBox.Name = "expTBox";
            this.expTBox.Size = new System.Drawing.Size(91, 20);
            this.expTBox.TabIndex = 0;
            this.expTBox.TextChanged += new System.EventHandler(this.expTBox_TextChanged);
            // 
            // dropTBox
            // 
            this.dropTBox.Location = new System.Drawing.Point(56, 91);
            this.dropTBox.Name = "dropTBox";
            this.dropTBox.Size = new System.Drawing.Size(91, 20);
            this.dropTBox.TabIndex = 2;
            this.dropTBox.TextChanged += new System.EventHandler(this.dropTBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Drop";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Flat",
            "Percent"});
            this.comboBox1.Location = new System.Drawing.Point(74, 26);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Buff Type";
            // 
            // minACBox
            // 
            this.minACBox.Location = new System.Drawing.Point(61, 12);
            this.minACBox.Name = "minACBox";
            this.minACBox.Size = new System.Drawing.Size(27, 20);
            this.minACBox.TabIndex = 1;
            this.minACBox.TextChanged += new System.EventHandler(this.minACBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "AC Min";
            // 
            // maxACBox
            // 
            this.maxACBox.Location = new System.Drawing.Point(127, 12);
            this.maxACBox.Name = "maxACBox";
            this.maxACBox.Size = new System.Drawing.Size(27, 20);
            this.maxACBox.TabIndex = 3;
            this.maxACBox.TextChanged += new System.EventHandler(this.maxACBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(94, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Max";
            // 
            // maxMACBox
            // 
            this.maxMACBox.Location = new System.Drawing.Point(127, 38);
            this.maxMACBox.Name = "maxMACBox";
            this.maxMACBox.Size = new System.Drawing.Size(27, 20);
            this.maxMACBox.TabIndex = 7;
            this.maxMACBox.TextChanged += new System.EventHandler(this.maxMACBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(94, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Max";
            // 
            // minMACBox
            // 
            this.minMACBox.Location = new System.Drawing.Point(61, 38);
            this.minMACBox.Name = "minMACBox";
            this.minMACBox.Size = new System.Drawing.Size(27, 20);
            this.minMACBox.TabIndex = 5;
            this.minMACBox.TextChanged += new System.EventHandler(this.minMACBox_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "MAC Min";
            // 
            // maxDCBox
            // 
            this.maxDCBox.Location = new System.Drawing.Point(127, 64);
            this.maxDCBox.Name = "maxDCBox";
            this.maxDCBox.Size = new System.Drawing.Size(27, 20);
            this.maxDCBox.TabIndex = 11;
            this.maxDCBox.TextChanged += new System.EventHandler(this.maxDCBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(94, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Max";
            // 
            // minDCBox
            // 
            this.minDCBox.Location = new System.Drawing.Point(61, 64);
            this.minDCBox.Name = "minDCBox";
            this.minDCBox.Size = new System.Drawing.Size(27, 20);
            this.minDCBox.TabIndex = 9;
            this.minDCBox.TextChanged += new System.EventHandler(this.minDCBox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "DC Min";
            // 
            // maxMCBox
            // 
            this.maxMCBox.Location = new System.Drawing.Point(127, 90);
            this.maxMCBox.Name = "maxMCBox";
            this.maxMCBox.Size = new System.Drawing.Size(27, 20);
            this.maxMCBox.TabIndex = 15;
            this.maxMCBox.TextChanged += new System.EventHandler(this.maxMCBox_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(94, 93);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Max";
            // 
            // minMCBox
            // 
            this.minMCBox.Location = new System.Drawing.Point(61, 90);
            this.minMCBox.Name = "minMCBox";
            this.minMCBox.Size = new System.Drawing.Size(27, 20);
            this.minMCBox.TabIndex = 13;
            this.minMCBox.TextChanged += new System.EventHandler(this.minMCBox_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 93);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "MC Min";
            // 
            // maxSCBox
            // 
            this.maxSCBox.Location = new System.Drawing.Point(127, 116);
            this.maxSCBox.Name = "maxSCBox";
            this.maxSCBox.Size = new System.Drawing.Size(27, 20);
            this.maxSCBox.TabIndex = 19;
            this.maxSCBox.TextChanged += new System.EventHandler(this.maxSCBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(94, 119);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 20;
            this.label13.Text = "Max";
            // 
            // minSCBox
            // 
            this.minSCBox.Location = new System.Drawing.Point(61, 116);
            this.minSCBox.Name = "minSCBox";
            this.minSCBox.Size = new System.Drawing.Size(27, 20);
            this.minSCBox.TabIndex = 17;
            this.minSCBox.TextChanged += new System.EventHandler(this.minSCBox_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 119);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "SC Min";
            // 
            // agilityBox
            // 
            this.agilityBox.Location = new System.Drawing.Point(127, 142);
            this.agilityBox.Name = "agilityBox";
            this.agilityBox.Size = new System.Drawing.Size(27, 20);
            this.agilityBox.TabIndex = 23;
            this.agilityBox.TextChanged += new System.EventHandler(this.agilityBox_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(94, 145);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "Agility";
            // 
            // accuracyBox
            // 
            this.accuracyBox.Location = new System.Drawing.Point(61, 142);
            this.accuracyBox.Name = "accuracyBox";
            this.accuracyBox.Size = new System.Drawing.Size(27, 20);
            this.accuracyBox.TabIndex = 21;
            this.accuracyBox.TextChanged += new System.EventHandler(this.accuracyBox_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 145);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 13);
            this.label16.TabIndex = 22;
            this.label16.Text = "Accuracy";
            // 
            // mpBox
            // 
            this.mpBox.Location = new System.Drawing.Point(127, 168);
            this.mpBox.Name = "mpBox";
            this.mpBox.Size = new System.Drawing.Size(27, 20);
            this.mpBox.TabIndex = 27;
            this.mpBox.TextChanged += new System.EventHandler(this.mpBox_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(94, 171);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(23, 13);
            this.label17.TabIndex = 28;
            this.label17.Text = "MP";
            // 
            // hpBox
            // 
            this.hpBox.Location = new System.Drawing.Point(61, 168);
            this.hpBox.Name = "hpBox";
            this.hpBox.Size = new System.Drawing.Size(27, 20);
            this.hpBox.TabIndex = 25;
            this.hpBox.TextChanged += new System.EventHandler(this.hpBox_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 171);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(22, 13);
            this.label18.TabIndex = 26;
            this.label18.Text = "HP";
            // 
            // aspeedBox
            // 
            this.aspeedBox.Location = new System.Drawing.Point(61, 194);
            this.aspeedBox.Name = "aspeedBox";
            this.aspeedBox.Size = new System.Drawing.Size(27, 20);
            this.aspeedBox.TabIndex = 29;
            this.aspeedBox.TextChanged += new System.EventHandler(this.aspeedBox_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(10, 197);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(45, 13);
            this.label19.TabIndex = 30;
            this.label19.Text = "ASpeed";
            // 
            // ConquestBuffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 288);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dropTBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.expTBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.classStatPanel);
            this.Controls.Add(this.classCBox);
            this.Name = "ConquestBuffForm";
            this.Text = "ConquestBuffForm";
            this.classStatPanel.ResumeLayout(false);
            this.classStatPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox classCBox;
        private System.Windows.Forms.GroupBox classStatPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox expTBox;
        private System.Windows.Forms.TextBox dropTBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox maxACBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox minACBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox maxMCBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox minMCBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox maxDCBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox minDCBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox maxMACBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox minMACBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox aspeedBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox mpBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox hpBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox agilityBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox accuracyBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox maxSCBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox minSCBox;
        private System.Windows.Forms.Label label14;
    }
}