namespace Server.MirForms
{
    partial class RecipeShopForm
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
            this.addButton = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.selectItemBtn = new System.Windows.Forms.Button();
            this.itemBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.priceBox = new System.Windows.Forms.TextBox();
            this.selectResult = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.selectResultItem = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.pickItemCBox = new System.Windows.Forms.ComboBox();
            this.selectResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(277, 226);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "Add New";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(358, 226);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteBtn.TabIndex = 1;
            this.deleteBtn.Text = "Delete";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(141, 238);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // selectItemBtn
            // 
            this.selectItemBtn.Location = new System.Drawing.Point(347, 12);
            this.selectItemBtn.Name = "selectItemBtn";
            this.selectItemBtn.Size = new System.Drawing.Size(75, 23);
            this.selectItemBtn.TabIndex = 3;
            this.selectItemBtn.Text = "Select Item";
            this.selectItemBtn.UseVisualStyleBackColor = true;
            this.selectItemBtn.Click += new System.EventHandler(this.selectItemBtn_Click);
            // 
            // itemBox
            // 
            this.itemBox.Location = new System.Drawing.Point(204, 14);
            this.itemBox.Name = "itemBox";
            this.itemBox.Size = new System.Drawing.Size(137, 20);
            this.itemBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Item";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Price";
            // 
            // priceBox
            // 
            this.priceBox.Location = new System.Drawing.Point(204, 40);
            this.priceBox.MaxLength = 19;
            this.priceBox.Name = "priceBox";
            this.priceBox.Size = new System.Drawing.Size(137, 20);
            this.priceBox.TabIndex = 6;
            this.priceBox.TextChanged += new System.EventHandler(this.priceBox_TextChanged);
            // 
            // selectResult
            // 
            this.selectResult.Controls.Add(this.button3);
            this.selectResult.Controls.Add(this.selectResultItem);
            this.selectResult.Controls.Add(this.label9);
            this.selectResult.Controls.Add(this.pickItemCBox);
            this.selectResult.Location = new System.Drawing.Point(159, 66);
            this.selectResult.Name = "selectResult";
            this.selectResult.Size = new System.Drawing.Size(246, 72);
            this.selectResult.TabIndex = 14;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(42, 38);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RecipeShopForm_KeyUp);
            // 
            // selectResultItem
            // 
            this.selectResultItem.Location = new System.Drawing.Point(127, 38);
            this.selectResultItem.Name = "selectResultItem";
            this.selectResultItem.Size = new System.Drawing.Size(75, 23);
            this.selectResultItem.TabIndex = 5;
            this.selectResultItem.Text = "Select Item";
            this.selectResultItem.UseVisualStyleBackColor = true;
            this.selectResultItem.Click += new System.EventHandler(this.selectResultItem_Click);
            this.selectResultItem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RecipeShopForm_KeyUp);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Item :";
            // 
            // pickItemCBox
            // 
            this.pickItemCBox.FormattingEnabled = true;
            this.pickItemCBox.Location = new System.Drawing.Point(81, 11);
            this.pickItemCBox.Name = "pickItemCBox";
            this.pickItemCBox.Size = new System.Drawing.Size(121, 21);
            this.pickItemCBox.TabIndex = 0;
            this.pickItemCBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RecipeShopForm_KeyUp);
            // 
            // RecipeShopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 261);
            this.Controls.Add(this.selectResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.priceBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.itemBox);
            this.Controls.Add(this.selectItemBtn);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.addButton);
            this.Name = "RecipeShopForm";
            this.Text = "RecipeShopForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecipeShopForm_FormClosing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RecipeShopForm_KeyUp);
            this.selectResult.ResumeLayout(false);
            this.selectResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button selectItemBtn;
        private System.Windows.Forms.TextBox itemBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox priceBox;
        private System.Windows.Forms.Panel selectResult;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button selectResultItem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox pickItemCBox;
    }
}