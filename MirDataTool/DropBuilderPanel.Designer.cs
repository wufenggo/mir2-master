namespace MirDataTool
{
    partial class DropBuilderPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMob = new System.Windows.Forms.TabPage();
            this.mobListBox = new System.Windows.Forms.ListBox();
            this.tabItem = new System.Windows.Forms.TabPage();
            this.itemListBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabMob.SuspendLayout();
            this.tabItem.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMob);
            this.tabControl1.Controls.Add(this.tabItem);
            this.tabControl1.Location = new System.Drawing.Point(18, 18);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(200, 477);
            this.tabControl1.TabIndex = 0;
            // 
            // tabMob
            // 
            this.tabMob.Controls.Add(this.mobListBox);
            this.tabMob.Location = new System.Drawing.Point(4, 22);
            this.tabMob.Name = "tabMob";
            this.tabMob.Padding = new System.Windows.Forms.Padding(3);
            this.tabMob.Size = new System.Drawing.Size(192, 451);
            this.tabMob.TabIndex = 0;
            this.tabMob.Text = "Drops By Mob";
            this.tabMob.UseVisualStyleBackColor = true;
            // 
            // mobListBox
            // 
            this.mobListBox.FormattingEnabled = true;
            this.mobListBox.Location = new System.Drawing.Point(3, 6);
            this.mobListBox.Name = "mobListBox";
            this.mobListBox.Size = new System.Drawing.Size(183, 433);
            this.mobListBox.TabIndex = 1;
            // 
            // tabItem
            // 
            this.tabItem.Controls.Add(this.itemListBox);
            this.tabItem.Location = new System.Drawing.Point(4, 22);
            this.tabItem.Name = "tabItem";
            this.tabItem.Padding = new System.Windows.Forms.Padding(3);
            this.tabItem.Size = new System.Drawing.Size(192, 451);
            this.tabItem.TabIndex = 1;
            this.tabItem.Text = "Drops By Item";
            this.tabItem.UseVisualStyleBackColor = true;
            // 
            // itemListBox
            // 
            this.itemListBox.FormattingEnabled = true;
            this.itemListBox.Location = new System.Drawing.Point(3, 6);
            this.itemListBox.Name = "itemListBox";
            this.itemListBox.Size = new System.Drawing.Size(183, 433);
            this.itemListBox.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(224, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(646, 144);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drop By Mob";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(468, 115);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Remove Item";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(557, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Item";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // DropBuilderPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Name = "DropBuilderPanel";
            this.Size = new System.Drawing.Size(887, 503);
            this.tabControl1.ResumeLayout(false);
            this.tabMob.ResumeLayout(false);
            this.tabItem.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMob;
        private System.Windows.Forms.TabPage tabItem;
        private System.Windows.Forms.ListBox mobListBox;
        private System.Windows.Forms.ListBox itemListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
