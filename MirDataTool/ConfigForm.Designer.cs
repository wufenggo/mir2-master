namespace MirDataTool
{
    partial class ConfigForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.envirPathBox = new System.Windows.Forms.TextBox();
            this.mapPathBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.exportPathBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.npcPathBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.questPathBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.versionBox = new System.Windows.Forms.TextBox();
            this.cversionBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.envirPathBtn = new System.Windows.Forms.Button();
            this.mapPathBtn = new System.Windows.Forms.Button();
            this.exportPathBtn = new System.Windows.Forms.Button();
            this.npcPathBtn = new System.Windows.Forms.Button();
            this.questPathBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.dbPathBtn = new System.Windows.Forms.Button();
            this.dbPathBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dropPathBtn = new System.Windows.Forms.Button();
            this.dropPathBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Envir Path ";
            // 
            // envirPathBox
            // 
            this.envirPathBox.Location = new System.Drawing.Point(109, 18);
            this.envirPathBox.Name = "envirPathBox";
            this.envirPathBox.Size = new System.Drawing.Size(302, 20);
            this.envirPathBox.TabIndex = 1;
            this.envirPathBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.envirPathBox_MouseClick);
            // 
            // mapPathBox
            // 
            this.mapPathBox.Location = new System.Drawing.Point(109, 44);
            this.mapPathBox.Name = "mapPathBox";
            this.mapPathBox.Size = new System.Drawing.Size(302, 20);
            this.mapPathBox.TabIndex = 3;
            this.mapPathBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mapPathBox_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Map Path ";
            // 
            // exportPathBox
            // 
            this.exportPathBox.Location = new System.Drawing.Point(109, 70);
            this.exportPathBox.Name = "exportPathBox";
            this.exportPathBox.Size = new System.Drawing.Size(302, 20);
            this.exportPathBox.TabIndex = 5;
            this.exportPathBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.exportPathBox_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Export Path ";
            // 
            // npcPathBox
            // 
            this.npcPathBox.Location = new System.Drawing.Point(109, 96);
            this.npcPathBox.Name = "npcPathBox";
            this.npcPathBox.Size = new System.Drawing.Size(302, 20);
            this.npcPathBox.TabIndex = 7;
            this.npcPathBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.npcPathBox_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "NPC Path ";
            // 
            // questPathBox
            // 
            this.questPathBox.Location = new System.Drawing.Point(109, 122);
            this.questPathBox.Name = "questPathBox";
            this.questPathBox.Size = new System.Drawing.Size(302, 20);
            this.questPathBox.TabIndex = 9;
            this.questPathBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.questPathBox_MouseClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Quest Path ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Database Version";
            // 
            // versionBox
            // 
            this.versionBox.Location = new System.Drawing.Point(109, 209);
            this.versionBox.Name = "versionBox";
            this.versionBox.Size = new System.Drawing.Size(38, 20);
            this.versionBox.TabIndex = 11;
            this.versionBox.TextChanged += new System.EventHandler(this.versionBox_TextChanged);
            // 
            // cversionBox
            // 
            this.cversionBox.Location = new System.Drawing.Point(288, 209);
            this.cversionBox.Name = "cversionBox";
            this.cversionBox.Size = new System.Drawing.Size(51, 20);
            this.cversionBox.TabIndex = 13;
            this.cversionBox.TextChanged += new System.EventHandler(this.cversionBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(153, 212);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Custom Database Version";
            // 
            // envirPathBtn
            // 
            this.envirPathBtn.Location = new System.Drawing.Point(417, 18);
            this.envirPathBtn.Name = "envirPathBtn";
            this.envirPathBtn.Size = new System.Drawing.Size(23, 18);
            this.envirPathBtn.TabIndex = 14;
            this.envirPathBtn.Text = "--";
            this.envirPathBtn.UseVisualStyleBackColor = true;
            this.envirPathBtn.Click += new System.EventHandler(this.envirPathBtn_Click);
            // 
            // mapPathBtn
            // 
            this.mapPathBtn.Location = new System.Drawing.Point(417, 44);
            this.mapPathBtn.Name = "mapPathBtn";
            this.mapPathBtn.Size = new System.Drawing.Size(23, 18);
            this.mapPathBtn.TabIndex = 15;
            this.mapPathBtn.Text = "--";
            this.mapPathBtn.UseVisualStyleBackColor = true;
            this.mapPathBtn.Click += new System.EventHandler(this.mapPathBtn_Click);
            // 
            // exportPathBtn
            // 
            this.exportPathBtn.Location = new System.Drawing.Point(417, 70);
            this.exportPathBtn.Name = "exportPathBtn";
            this.exportPathBtn.Size = new System.Drawing.Size(23, 18);
            this.exportPathBtn.TabIndex = 16;
            this.exportPathBtn.Text = "--";
            this.exportPathBtn.UseVisualStyleBackColor = true;
            this.exportPathBtn.Click += new System.EventHandler(this.exportPathBtn_Click);
            // 
            // npcPathBtn
            // 
            this.npcPathBtn.Location = new System.Drawing.Point(417, 96);
            this.npcPathBtn.Name = "npcPathBtn";
            this.npcPathBtn.Size = new System.Drawing.Size(23, 18);
            this.npcPathBtn.TabIndex = 17;
            this.npcPathBtn.Text = "--";
            this.npcPathBtn.UseVisualStyleBackColor = true;
            this.npcPathBtn.Click += new System.EventHandler(this.npcPathBtn_Click);
            // 
            // questPathBtn
            // 
            this.questPathBtn.Location = new System.Drawing.Point(417, 122);
            this.questPathBtn.Name = "questPathBtn";
            this.questPathBtn.Size = new System.Drawing.Size(23, 18);
            this.questPathBtn.TabIndex = 18;
            this.questPathBtn.Text = "--";
            this.questPathBtn.UseVisualStyleBackColor = true;
            this.questPathBtn.Click += new System.EventHandler(this.questPathBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(345, 207);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(95, 23);
            this.saveBtn.TabIndex = 19;
            this.saveBtn.Text = "Save and Close";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // dbPathBtn
            // 
            this.dbPathBtn.Location = new System.Drawing.Point(417, 148);
            this.dbPathBtn.Name = "dbPathBtn";
            this.dbPathBtn.Size = new System.Drawing.Size(23, 18);
            this.dbPathBtn.TabIndex = 22;
            this.dbPathBtn.Text = "--";
            this.dbPathBtn.UseVisualStyleBackColor = true;
            this.dbPathBtn.Click += new System.EventHandler(this.dbPathBtn_Click);
            // 
            // dbPathBox
            // 
            this.dbPathBox.Location = new System.Drawing.Point(109, 148);
            this.dbPathBox.Name = "dbPathBox";
            this.dbPathBox.Size = new System.Drawing.Size(302, 20);
            this.dbPathBox.TabIndex = 21;
            this.dbPathBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dbPathBox_MouseDoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Database Path ";
            // 
            // dropPathBtn
            // 
            this.dropPathBtn.Location = new System.Drawing.Point(417, 174);
            this.dropPathBtn.Name = "dropPathBtn";
            this.dropPathBtn.Size = new System.Drawing.Size(23, 18);
            this.dropPathBtn.TabIndex = 25;
            this.dropPathBtn.Text = "--";
            this.dropPathBtn.UseVisualStyleBackColor = true;
            this.dropPathBtn.Click += new System.EventHandler(this.dropPathBtn_Click);
            // 
            // dropPathBox
            // 
            this.dropPathBox.Location = new System.Drawing.Point(109, 174);
            this.dropPathBox.Name = "dropPathBox";
            this.dropPathBox.Size = new System.Drawing.Size(302, 20);
            this.dropPathBox.TabIndex = 24;
            this.dropPathBox.TextChanged += new System.EventHandler(this.dropPathBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(45, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Drop Path ";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 242);
            this.Controls.Add(this.dropPathBtn);
            this.Controls.Add(this.dropPathBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dbPathBtn);
            this.Controls.Add(this.dbPathBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.questPathBtn);
            this.Controls.Add(this.npcPathBtn);
            this.Controls.Add(this.exportPathBtn);
            this.Controls.Add(this.mapPathBtn);
            this.Controls.Add(this.envirPathBtn);
            this.Controls.Add(this.cversionBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.versionBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.questPathBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.npcPathBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.exportPathBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mapPathBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.envirPathBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(474, 281);
            this.MinimumSize = new System.Drawing.Size(474, 281);
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox envirPathBox;
        private System.Windows.Forms.TextBox mapPathBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox exportPathBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox npcPathBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox questPathBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox versionBox;
        private System.Windows.Forms.TextBox cversionBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button envirPathBtn;
        private System.Windows.Forms.Button mapPathBtn;
        private System.Windows.Forms.Button exportPathBtn;
        private System.Windows.Forms.Button npcPathBtn;
        private System.Windows.Forms.Button questPathBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button dbPathBtn;
        private System.Windows.Forms.TextBox dbPathBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button dropPathBtn;
        private System.Windows.Forms.TextBox dropPathBox;
        private System.Windows.Forms.Label label9;
    }
}