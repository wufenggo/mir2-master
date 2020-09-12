namespace Server.MirForms.Systems
{
    partial class MapEXPForm
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
            this.startBoostBtn = new System.Windows.Forms.Button();
            this.expAmountBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.durationBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.selectmapCBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.selectedMapLabel = new System.Windows.Forms.Label();
            this.selectedMapsBox = new System.Windows.Forms.ListBox();
            this.addmapBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.DMGIncreaseBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // startBoostBtn
            // 
            this.startBoostBtn.Location = new System.Drawing.Point(376, 107);
            this.startBoostBtn.Name = "startBoostBtn";
            this.startBoostBtn.Size = new System.Drawing.Size(75, 23);
            this.startBoostBtn.TabIndex = 0;
            this.startBoostBtn.Text = "Start";
            this.startBoostBtn.UseVisualStyleBackColor = true;
            this.startBoostBtn.Click += new System.EventHandler(this.startBoostBtn_Click);
            // 
            // expAmountBox
            // 
            this.expAmountBox.Location = new System.Drawing.Point(11, 72);
            this.expAmountBox.MaxLength = 3;
            this.expAmountBox.Name = "expAmountBox";
            this.expAmountBox.Size = new System.Drawing.Size(87, 20);
            this.expAmountBox.TabIndex = 1;
            this.expAmountBox.TextChanged += new System.EventHandler(this.expAmountBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "EXP Increase by %";
            // 
            // durationBox
            // 
            this.durationBox.Location = new System.Drawing.Point(168, 72);
            this.durationBox.Name = "durationBox";
            this.durationBox.Size = new System.Drawing.Size(100, 20);
            this.durationBox.TabIndex = 3;
            this.durationBox.TextChanged += new System.EventHandler(this.durationBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Duration in minutes";
            // 
            // selectmapCBox
            // 
            this.selectmapCBox.FormattingEnabled = true;
            this.selectmapCBox.Location = new System.Drawing.Point(151, 12);
            this.selectmapCBox.Name = "selectmapCBox";
            this.selectmapCBox.Size = new System.Drawing.Size(121, 21);
            this.selectmapCBox.TabIndex = 5;
            this.selectmapCBox.SelectedIndexChanged += new System.EventHandler(this.selectmapCBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "1) Select Map";
            // 
            // selectedMapLabel
            // 
            this.selectedMapLabel.AutoSize = true;
            this.selectedMapLabel.Location = new System.Drawing.Point(81, 40);
            this.selectedMapLabel.Name = "selectedMapLabel";
            this.selectedMapLabel.Size = new System.Drawing.Size(121, 13);
            this.selectedMapLabel.TabIndex = 7;
            this.selectedMapLabel.Text = "NO MAP(S) SELECTED";
            // 
            // selectedMapsBox
            // 
            this.selectedMapsBox.FormattingEnabled = true;
            this.selectedMapsBox.Location = new System.Drawing.Point(349, 6);
            this.selectedMapsBox.Name = "selectedMapsBox";
            this.selectedMapsBox.Size = new System.Drawing.Size(120, 95);
            this.selectedMapsBox.TabIndex = 8;
            // 
            // addmapBtn
            // 
            this.addmapBtn.Location = new System.Drawing.Point(278, 10);
            this.addmapBtn.Name = "addmapBtn";
            this.addmapBtn.Size = new System.Drawing.Size(65, 23);
            this.addmapBtn.TabIndex = 9;
            this.addmapBtn.Text = "Add Map";
            this.addmapBtn.UseVisualStyleBackColor = true;
            this.addmapBtn.Click += new System.EventHandler(this.addmapBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "DMG Increase by %";
            // 
            // DMGIncreaseBox
            // 
            this.DMGIncreaseBox.Location = new System.Drawing.Point(11, 110);
            this.DMGIncreaseBox.MaxLength = 3;
            this.DMGIncreaseBox.Name = "DMGIncreaseBox";
            this.DMGIncreaseBox.Size = new System.Drawing.Size(87, 20);
            this.DMGIncreaseBox.TabIndex = 10;
            this.DMGIncreaseBox.TextChanged += new System.EventHandler(this.DMGIncreaseBox_TextChanged);
            // 
            // MapEXPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 138);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DMGIncreaseBox);
            this.Controls.Add(this.addmapBtn);
            this.Controls.Add(this.selectedMapsBox);
            this.Controls.Add(this.selectedMapLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.selectmapCBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.durationBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.expAmountBox);
            this.Controls.Add(this.startBoostBtn);
            this.Name = "MapEXPForm";
            this.Text = "Map EXP Select";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startBoostBtn;
        private System.Windows.Forms.TextBox expAmountBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox durationBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox selectmapCBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label selectedMapLabel;
        private System.Windows.Forms.ListBox selectedMapsBox;
        private System.Windows.Forms.Button addmapBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DMGIncreaseBox;
    }
}