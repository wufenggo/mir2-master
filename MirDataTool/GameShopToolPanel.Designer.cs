﻿namespace MirDataTool
{
    partial class GameShopToolPanel
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
            this.ResetFilter_button = new System.Windows.Forms.Button();
            this.CategoryFilter_lb = new System.Windows.Forms.ComboBox();
            this.SectionFilter_lb = new System.Windows.Forms.ComboBox();
            this.ClassFilter_lb = new System.Windows.Forms.ComboBox();
            this.Remove_button = new System.Windows.Forms.Button();
            this.ItemDetails_gb = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Count_textbox = new System.Windows.Forms.TextBox();
            this.LeftinStock_label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TotalSold_label = new System.Windows.Forms.Label();
            this.TopItem_checkbox = new System.Windows.Forms.CheckBox();
            this.DealofDay_checkbox = new System.Windows.Forms.CheckBox();
            this.Individual_checkbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Stock_textbox = new System.Windows.Forms.TextBox();
            this.GoldPrice_textbox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.Category_textbox = new System.Windows.Forms.TextBox();
            this.GPPrice_textbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.Class_combo = new System.Windows.Forms.ComboBox();
            this.GameShopListBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ItemDetails_gb.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ResetFilter_button
            // 
            this.ResetFilter_button.Location = new System.Drawing.Point(171, 19);
            this.ResetFilter_button.Name = "ResetFilter_button";
            this.ResetFilter_button.Size = new System.Drawing.Size(49, 69);
            this.ResetFilter_button.TabIndex = 119;
            this.ResetFilter_button.Text = "Reset Filter";
            this.ResetFilter_button.UseVisualStyleBackColor = true;
            this.ResetFilter_button.Click += new System.EventHandler(this.ResetFilter_button_Click);
            // 
            // CategoryFilter_lb
            // 
            this.CategoryFilter_lb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CategoryFilter_lb.FormattingEnabled = true;
            this.CategoryFilter_lb.Location = new System.Drawing.Point(19, 66);
            this.CategoryFilter_lb.Name = "CategoryFilter_lb";
            this.CategoryFilter_lb.Size = new System.Drawing.Size(146, 21);
            this.CategoryFilter_lb.TabIndex = 118;
            this.CategoryFilter_lb.SelectedIndexChanged += new System.EventHandler(this.CategoryFilter_lb_SelectedIndexChanged);
            // 
            // SectionFilter_lb
            // 
            this.SectionFilter_lb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SectionFilter_lb.FormattingEnabled = true;
            this.SectionFilter_lb.Items.AddRange(new object[] {
            "All Items",
            "Top Items",
            "Sale Items",
            "New Items"});
            this.SectionFilter_lb.Location = new System.Drawing.Point(19, 43);
            this.SectionFilter_lb.Name = "SectionFilter_lb";
            this.SectionFilter_lb.Size = new System.Drawing.Size(146, 21);
            this.SectionFilter_lb.TabIndex = 117;
            this.SectionFilter_lb.SelectedIndexChanged += new System.EventHandler(this.SectionFilter_lb_SelectedIndexChanged);
            // 
            // ClassFilter_lb
            // 
            this.ClassFilter_lb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClassFilter_lb.FormattingEnabled = true;
            this.ClassFilter_lb.Location = new System.Drawing.Point(19, 20);
            this.ClassFilter_lb.Name = "ClassFilter_lb";
            this.ClassFilter_lb.Size = new System.Drawing.Size(146, 21);
            this.ClassFilter_lb.TabIndex = 116;
            this.ClassFilter_lb.SelectedIndexChanged += new System.EventHandler(this.ClassFilter_lb_SelectedIndexChanged);
            // 
            // Remove_button
            // 
            this.Remove_button.Location = new System.Drawing.Point(291, 421);
            this.Remove_button.Name = "Remove_button";
            this.Remove_button.Size = new System.Drawing.Size(267, 34);
            this.Remove_button.TabIndex = 115;
            this.Remove_button.Text = "Remove Selected";
            this.Remove_button.UseVisualStyleBackColor = true;
            this.Remove_button.Click += new System.EventHandler(this.Remove_button_Click);
            // 
            // ItemDetails_gb
            // 
            this.ItemDetails_gb.BackColor = System.Drawing.Color.White;
            this.ItemDetails_gb.Controls.Add(this.checkBox2);
            this.ItemDetails_gb.Controls.Add(this.checkBox1);
            this.ItemDetails_gb.Controls.Add(this.label6);
            this.ItemDetails_gb.Controls.Add(this.Count_textbox);
            this.ItemDetails_gb.Controls.Add(this.LeftinStock_label);
            this.ItemDetails_gb.Controls.Add(this.label3);
            this.ItemDetails_gb.Controls.Add(this.label5);
            this.ItemDetails_gb.Controls.Add(this.TotalSold_label);
            this.ItemDetails_gb.Controls.Add(this.TopItem_checkbox);
            this.ItemDetails_gb.Controls.Add(this.DealofDay_checkbox);
            this.ItemDetails_gb.Controls.Add(this.Individual_checkbox);
            this.ItemDetails_gb.Controls.Add(this.label1);
            this.ItemDetails_gb.Controls.Add(this.Stock_textbox);
            this.ItemDetails_gb.Controls.Add(this.GoldPrice_textbox);
            this.ItemDetails_gb.Controls.Add(this.label14);
            this.ItemDetails_gb.Controls.Add(this.label21);
            this.ItemDetails_gb.Controls.Add(this.Category_textbox);
            this.ItemDetails_gb.Controls.Add(this.GPPrice_textbox);
            this.ItemDetails_gb.Controls.Add(this.label4);
            this.ItemDetails_gb.Controls.Add(this.label29);
            this.ItemDetails_gb.Controls.Add(this.Class_combo);
            this.ItemDetails_gb.Location = new System.Drawing.Point(291, 9);
            this.ItemDetails_gb.Name = "ItemDetails_gb";
            this.ItemDetails_gb.Size = new System.Drawing.Size(267, 287);
            this.ItemDetails_gb.TabIndex = 114;
            this.ItemDetails_gb.TabStop = false;
            this.ItemDetails_gb.Text = "Item Details";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(205, 104);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 17);
            this.checkBox2.TabIndex = 119;
            this.checkBox2.Text = "Gold";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(205, 76);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(58, 17);
            this.checkBox1.TabIndex = 118;
            this.checkBox1.Text = "Credits";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 117;
            this.label6.Text = "Count:";
            // 
            // Count_textbox
            // 
            this.Count_textbox.Location = new System.Drawing.Point(86, 128);
            this.Count_textbox.MaxLength = 0;
            this.Count_textbox.Name = "Count_textbox";
            this.Count_textbox.Size = new System.Drawing.Size(113, 20);
            this.Count_textbox.TabIndex = 116;
            this.Count_textbox.TextChanged += new System.EventHandler(this.Count_textbox_TextChanged);
            // 
            // LeftinStock_label
            // 
            this.LeftinStock_label.AutoSize = true;
            this.LeftinStock_label.Location = new System.Drawing.Point(83, 47);
            this.LeftinStock_label.Name = "LeftinStock_label";
            this.LeftinStock_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LeftinStock_label.Size = new System.Drawing.Size(25, 13);
            this.LeftinStock_label.TabIndex = 115;
            this.LeftinStock_label.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 114;
            this.label3.Text = "Left in Stock:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 113;
            this.label5.Text = "Total Sold:";
            // 
            // TotalSold_label
            // 
            this.TotalSold_label.AutoSize = true;
            this.TotalSold_label.Location = new System.Drawing.Point(82, 21);
            this.TotalSold_label.Name = "TotalSold_label";
            this.TotalSold_label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TotalSold_label.Size = new System.Drawing.Size(25, 13);
            this.TotalSold_label.TabIndex = 112;
            this.TotalSold_label.Text = "100";
            // 
            // TopItem_checkbox
            // 
            this.TopItem_checkbox.AutoSize = true;
            this.TopItem_checkbox.Location = new System.Drawing.Point(29, 256);
            this.TopItem_checkbox.Name = "TopItem_checkbox";
            this.TopItem_checkbox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TopItem_checkbox.Size = new System.Drawing.Size(71, 17);
            this.TopItem_checkbox.TabIndex = 106;
            this.TopItem_checkbox.Text = ":Top Item";
            this.TopItem_checkbox.UseVisualStyleBackColor = true;
            this.TopItem_checkbox.CheckedChanged += new System.EventHandler(this.TopItem_checkbox_CheckedChanged);
            // 
            // DealofDay_checkbox
            // 
            this.DealofDay_checkbox.AutoSize = true;
            this.DealofDay_checkbox.Location = new System.Drawing.Point(27, 236);
            this.DealofDay_checkbox.Name = "DealofDay_checkbox";
            this.DealofDay_checkbox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DealofDay_checkbox.Size = new System.Drawing.Size(73, 17);
            this.DealofDay_checkbox.TabIndex = 105;
            this.DealofDay_checkbox.Text = ":Sale Item";
            this.DealofDay_checkbox.UseVisualStyleBackColor = true;
            this.DealofDay_checkbox.CheckedChanged += new System.EventHandler(this.DealofDay_checkbox_CheckedChanged);
            // 
            // Individual_checkbox
            // 
            this.Individual_checkbox.AutoSize = true;
            this.Individual_checkbox.Location = new System.Drawing.Point(163, 212);
            this.Individual_checkbox.Name = "Individual_checkbox";
            this.Individual_checkbox.Size = new System.Drawing.Size(79, 17);
            this.Individual_checkbox.TabIndex = 110;
            this.Individual_checkbox.Text = "Player Limit";
            this.Individual_checkbox.UseVisualStyleBackColor = true;
            this.Individual_checkbox.CheckedChanged += new System.EventHandler(this.Individual_checkbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 111;
            this.label1.Text = "Stock;";
            // 
            // Stock_textbox
            // 
            this.Stock_textbox.Location = new System.Drawing.Point(86, 210);
            this.Stock_textbox.MaxLength = 0;
            this.Stock_textbox.Name = "Stock_textbox";
            this.Stock_textbox.Size = new System.Drawing.Size(72, 20);
            this.Stock_textbox.TabIndex = 109;
            this.Stock_textbox.TextChanged += new System.EventHandler(this.Stock_textbox_TextChanged);
            // 
            // GoldPrice_textbox
            // 
            this.GoldPrice_textbox.Location = new System.Drawing.Point(86, 102);
            this.GoldPrice_textbox.MaxLength = 0;
            this.GoldPrice_textbox.Name = "GoldPrice_textbox";
            this.GoldPrice_textbox.Size = new System.Drawing.Size(113, 20);
            this.GoldPrice_textbox.TabIndex = 86;
            this.GoldPrice_textbox.TextChanged += new System.EventHandler(this.GoldPrice_textbox_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(23, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 90;
            this.label14.Text = "Gold Price:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(18, 77);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(64, 13);
            this.label21.TabIndex = 91;
            this.label21.Text = "Credit Price:";
            // 
            // Category_textbox
            // 
            this.Category_textbox.Location = new System.Drawing.Point(86, 183);
            this.Category_textbox.MaxLength = 0;
            this.Category_textbox.Name = "Category_textbox";
            this.Category_textbox.Size = new System.Drawing.Size(173, 20);
            this.Category_textbox.TabIndex = 108;
            this.Category_textbox.TextChanged += new System.EventHandler(this.Category_textbox_TextChanged);
            // 
            // GPPrice_textbox
            // 
            this.GPPrice_textbox.Location = new System.Drawing.Point(86, 74);
            this.GPPrice_textbox.MaxLength = 0;
            this.GPPrice_textbox.Name = "GPPrice_textbox";
            this.GPPrice_textbox.Size = new System.Drawing.Size(113, 20);
            this.GPPrice_textbox.TabIndex = 87;
            this.GPPrice_textbox.TextChanged += new System.EventHandler(this.GPPrice_textbox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 106;
            this.label4.Text = "Category:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(8, 159);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(74, 13);
            this.label29.TabIndex = 93;
            this.label29.Text = "Class Section:";
            // 
            // Class_combo
            // 
            this.Class_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Class_combo.FormattingEnabled = true;
            this.Class_combo.Items.AddRange(new object[] {
            "All",
            "Warrior",
            "Assassin",
            "Taoist",
            "Wizard",
            "Archer"});
            this.Class_combo.Location = new System.Drawing.Point(86, 156);
            this.Class_combo.Name = "Class_combo";
            this.Class_combo.Size = new System.Drawing.Size(173, 21);
            this.Class_combo.TabIndex = 105;
            this.Class_combo.SelectedIndexChanged += new System.EventHandler(this.Class_combo_SelectedIndexChanged);
            // 
            // GameShopListBox
            // 
            this.GameShopListBox.FormattingEnabled = true;
            this.GameShopListBox.Location = new System.Drawing.Point(27, 115);
            this.GameShopListBox.Name = "GameShopListBox";
            this.GameShopListBox.ScrollAlwaysVisible = true;
            this.GameShopListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.GameShopListBox.Size = new System.Drawing.Size(258, 329);
            this.GameShopListBox.TabIndex = 113;
            this.GameShopListBox.SelectedIndexChanged += new System.EventHandler(this.GameShopListBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ResetFilter_button);
            this.groupBox1.Controls.Add(this.ClassFilter_lb);
            this.groupBox1.Controls.Add(this.SectionFilter_lb);
            this.groupBox1.Controls.Add(this.CategoryFilter_lb);
            this.groupBox1.Location = new System.Drawing.Point(27, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 100);
            this.groupBox1.TabIndex = 121;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // GameShopToolPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Remove_button);
            this.Controls.Add(this.ItemDetails_gb);
            this.Controls.Add(this.GameShopListBox);
            this.Name = "GameShopToolPanel";
            this.Size = new System.Drawing.Size(581, 469);
            this.ItemDetails_gb.ResumeLayout(false);
            this.ItemDetails_gb.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ResetFilter_button;
        private System.Windows.Forms.ComboBox CategoryFilter_lb;
        private System.Windows.Forms.ComboBox SectionFilter_lb;
        private System.Windows.Forms.ComboBox ClassFilter_lb;
        private System.Windows.Forms.Button Remove_button;
        private System.Windows.Forms.GroupBox ItemDetails_gb;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Count_textbox;
        private System.Windows.Forms.Label LeftinStock_label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label TotalSold_label;
        private System.Windows.Forms.CheckBox TopItem_checkbox;
        private System.Windows.Forms.CheckBox DealofDay_checkbox;
        private System.Windows.Forms.CheckBox Individual_checkbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Stock_textbox;
        private System.Windows.Forms.TextBox GoldPrice_textbox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox Category_textbox;
        private System.Windows.Forms.TextBox GPPrice_textbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox Class_combo;
        private System.Windows.Forms.ListBox GameShopListBox;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
