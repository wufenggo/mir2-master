namespace MirDataTool
{
    partial class CraftTool
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
            this.selectResult = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.selectResultItem = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.pickItemCBox = new System.Windows.Forms.ComboBox();
            this.itemSelect = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.selectBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.amountBox = new System.Windows.Forms.TextBox();
            this.itemsCBox = new System.Windows.Forms.ComboBox();
            this.delButton = new System.Windows.Forms.Button();
            this.addNewCraft = new System.Windows.Forms.Button();
            this.requirementPanel = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.rateBox = new System.Windows.Forms.TextBox();
            this.timeBox = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dayBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.minuteBox = new System.Windows.Forms.TextBox();
            this.hourbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.addMat = new System.Windows.Forms.Button();
            this.requiredMaterials = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.reqLevelBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.reqGoldBox = new System.Windows.Forms.TextBox();
            this.reqClassCboBox = new System.Windows.Forms.ComboBox();
            this.recipeList = new System.Windows.Forms.ListBox();
            this.recipePanel = new System.Windows.Forms.Panel();
            this.selectResultItemBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.resultBox = new System.Windows.Forms.TextBox();
            this.label0 = new System.Windows.Forms.Label();
            this.recipeNameBox = new System.Windows.Forms.TextBox();
            this.selectResult.SuspendLayout();
            this.itemSelect.SuspendLayout();
            this.requirementPanel.SuspendLayout();
            this.recipePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // recipePanel
            // 
            this.recipePanel.Controls.Add(this.selectResultItemBtn);
            this.recipePanel.Controls.Add(this.label1);
            this.recipePanel.Controls.Add(this.resultBox);
            this.recipePanel.Controls.Add(this.label0);
            this.recipePanel.Controls.Add(this.recipeNameBox);
            this.recipePanel.Location = new System.Drawing.Point(157, 12);
            this.recipePanel.Name = "recipePanel";
            this.recipePanel.Size = new System.Drawing.Size(410, 59);
            this.recipePanel.TabIndex = 0;
            // 
            // selectResultItemBtn
            // 
            this.selectResultItemBtn.Location = new System.Drawing.Point(248, 29);
            this.selectResultItemBtn.Name = "selectResultItemBtn";
            this.selectResultItemBtn.Size = new System.Drawing.Size(75, 20);
            this.selectResultItemBtn.TabIndex = 5;
            this.selectResultItemBtn.Text = "Select Item";
            this.selectResultItemBtn.UseVisualStyleBackColor = true;
            this.selectResultItemBtn.Click += new System.EventHandler(this.selectResultItemBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Item Result";
            // 
            // resultBox
            // 
            this.resultBox.Location = new System.Drawing.Point(98, 29);
            this.resultBox.Name = "resultBox";
            this.resultBox.ReadOnly = true;
            this.resultBox.Size = new System.Drawing.Size(144, 20);
            this.resultBox.TabIndex = 3;
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Location = new System.Drawing.Point(9, 6);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(72, 13);
            this.label0.TabIndex = 2;
            this.label0.Text = "Recipe Name";
            // 
            // recipeNameBox
            // 
            this.recipeNameBox.Location = new System.Drawing.Point(98, 3);
            this.recipeNameBox.Name = "recipeNameBox";
            this.recipeNameBox.Size = new System.Drawing.Size(144, 20);
            this.recipeNameBox.TabIndex = 0;
            this.recipeNameBox.TextChanged += new System.EventHandler(this.recipeNameBox_TextChanged);
            // 
            // recipeList
            // 
            this.recipeList.FormattingEnabled = true;
            this.recipeList.Location = new System.Drawing.Point(12, 12);
            this.recipeList.Name = "recipeList";
            this.recipeList.Size = new System.Drawing.Size(139, 277);
            this.recipeList.TabIndex = 1;
            this.recipeList.SelectedIndexChanged += new System.EventHandler(this.recipeList_SelectedIndexChanged);
            // 
            // requirementPanel
            // 
            this.requirementPanel.Controls.Add(this.label13);
            this.requirementPanel.Controls.Add(this.label8);
            this.requirementPanel.Controls.Add(this.rateBox);
            this.requirementPanel.Controls.Add(this.timeBox);
            this.requirementPanel.Controls.Add(this.label12);
            this.requirementPanel.Controls.Add(this.dayBox);
            this.requirementPanel.Controls.Add(this.label11);
            this.requirementPanel.Controls.Add(this.label10);
            this.requirementPanel.Controls.Add(this.minuteBox);
            this.requirementPanel.Controls.Add(this.hourbox);
            this.requirementPanel.Controls.Add(this.label5);
            this.requirementPanel.Controls.Add(this.button1);
            this.requirementPanel.Controls.Add(this.addMat);
            this.requirementPanel.Controls.Add(this.requiredMaterials);
            this.requirementPanel.Controls.Add(this.label4);
            this.requirementPanel.Controls.Add(this.reqLevelBox);
            this.requirementPanel.Controls.Add(this.label3);
            this.requirementPanel.Controls.Add(this.label2);
            this.requirementPanel.Controls.Add(this.reqGoldBox);
            this.requirementPanel.Controls.Add(this.reqClassCboBox);
            this.requirementPanel.Location = new System.Drawing.Point(157, 77);
            this.requirementPanel.Name = "requirementPanel";
            this.requirementPanel.Size = new System.Drawing.Size(410, 174);
            this.requirementPanel.TabIndex = 1;
            // 
            // timeBox
            // 
            this.timeBox.AutoSize = true;
            this.timeBox.Location = new System.Drawing.Point(10, 85);
            this.timeBox.Name = "timeBox";
            this.timeBox.Size = new System.Drawing.Size(110, 17);
            this.timeBox.TabIndex = 19;
            this.timeBox.Text = "Craft Time On/Off";
            this.timeBox.UseVisualStyleBackColor = true;
            this.timeBox.CheckedChanged += new System.EventHandler(this.timeBox_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(173, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "Days";
            // 
            // dayBox
            // 
            this.dayBox.Location = new System.Drawing.Point(130, 82);
            this.dayBox.Name = "dayBox";
            this.dayBox.Size = new System.Drawing.Size(36, 20);
            this.dayBox.TabIndex = 17;
            this.dayBox.TextChanged += new System.EventHandler(this.dayBox_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(174, 138);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Minutes";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(174, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Hours";
            // 
            // minuteBox
            // 
            this.minuteBox.Location = new System.Drawing.Point(130, 135);
            this.minuteBox.Name = "minuteBox";
            this.minuteBox.Size = new System.Drawing.Size(36, 20);
            this.minuteBox.TabIndex = 14;
            this.minuteBox.TextChanged += new System.EventHandler(this.minuteBox_TextChanged);
            // 
            // hourbox
            // 
            this.hourbox.Location = new System.Drawing.Point(130, 108);
            this.hourbox.Name = "hourbox";
            this.hourbox.Size = new System.Drawing.Size(36, 20);
            this.hourbox.TabIndex = 13;
            this.hourbox.TextChanged += new System.EventHandler(this.hourbox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(274, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Required Materials";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(311, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 32);
            this.button1.TabIndex = 4;
            this.button1.Text = "Delete Material";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addMat
            // 
            this.addMat.Location = new System.Drawing.Point(230, 139);
            this.addMat.Name = "addMat";
            this.addMat.Size = new System.Drawing.Size(75, 32);
            this.addMat.TabIndex = 4;
            this.addMat.Text = "Add Material";
            this.addMat.UseVisualStyleBackColor = true;
            this.addMat.Click += new System.EventHandler(this.addMat_Click);
            // 
            // requiredMaterials
            // 
            this.requiredMaterials.FormattingEnabled = true;
            this.requiredMaterials.Location = new System.Drawing.Point(248, 38);
            this.requiredMaterials.Name = "requiredMaterials";
            this.requiredMaterials.Size = new System.Drawing.Size(159, 95);
            this.requiredMaterials.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Required Level";
            // 
            // reqLevelBox
            // 
            this.reqLevelBox.Location = new System.Drawing.Point(98, 56);
            this.reqLevelBox.Name = "reqLevelBox";
            this.reqLevelBox.Size = new System.Drawing.Size(144, 20);
            this.reqLevelBox.TabIndex = 8;
            this.reqLevelBox.TextChanged += new System.EventHandler(this.reqLevelBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Required Gold";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Required Class";
            // 
            // reqGoldBox
            // 
            this.reqGoldBox.Location = new System.Drawing.Point(98, 30);
            this.reqGoldBox.Name = "reqGoldBox";
            this.reqGoldBox.Size = new System.Drawing.Size(144, 20);
            this.reqGoldBox.TabIndex = 6;
            this.reqGoldBox.TextChanged += new System.EventHandler(this.reqGoldBox_TextChanged);
            // 
            // reqClassCboBox
            // 
            this.reqClassCboBox.FormattingEnabled = true;
            this.reqClassCboBox.Location = new System.Drawing.Point(98, 6);
            this.reqClassCboBox.Name = "reqClassCboBox";
            this.reqClassCboBox.Size = new System.Drawing.Size(144, 21);
            this.reqClassCboBox.TabIndex = 1;
            this.reqClassCboBox.SelectedIndexChanged += new System.EventHandler(this.reqClassCboBox_SelectedIndexChanged);
            // 
            // addNewCraft
            // 
            this.addNewCraft.Location = new System.Drawing.Point(157, 257);
            this.addNewCraft.Name = "addNewCraft";
            this.addNewCraft.Size = new System.Drawing.Size(75, 32);
            this.addNewCraft.TabIndex = 2;
            this.addNewCraft.Text = "Add New";
            this.addNewCraft.UseVisualStyleBackColor = true;
            this.addNewCraft.Click += new System.EventHandler(this.addNewCraft_Click);
            // 
            // delButton
            // 
            this.delButton.Location = new System.Drawing.Point(238, 257);
            this.delButton.Name = "delButton";
            this.delButton.Size = new System.Drawing.Size(96, 32);
            this.delButton.TabIndex = 3;
            this.delButton.Text = "Delete Selected";
            this.delButton.UseVisualStyleBackColor = true;
            this.delButton.Click += new System.EventHandler(this.delButton_Click);
            // 
            // itemSelect
            // 
            this.itemSelect.Controls.Add(this.button2);
            this.itemSelect.Controls.Add(this.selectBtn);
            this.itemSelect.Controls.Add(this.label7);
            this.itemSelect.Controls.Add(this.label6);
            this.itemSelect.Controls.Add(this.amountBox);
            this.itemSelect.Controls.Add(this.itemsCBox);
            this.itemSelect.Location = new System.Drawing.Point(70, 25);
            this.itemSelect.Name = "itemSelect";
            this.itemSelect.Size = new System.Drawing.Size(341, 98);
            this.itemSelect.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(182, 72);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // selectBtn
            // 
            this.selectBtn.Location = new System.Drawing.Point(263, 72);
            this.selectBtn.Name = "selectBtn";
            this.selectBtn.Size = new System.Drawing.Size(75, 23);
            this.selectBtn.TabIndex = 5;
            this.selectBtn.Text = "Select Item";
            this.selectBtn.UseVisualStyleBackColor = true;
            this.selectBtn.Click += new System.EventHandler(this.selectBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Amount :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Item :";
            // 
            // amountBox
            // 
            this.amountBox.Location = new System.Drawing.Point(81, 38);
            this.amountBox.Name = "amountBox";
            this.amountBox.Size = new System.Drawing.Size(100, 20);
            this.amountBox.TabIndex = 1;
            // 
            // itemsCBox
            // 
            this.itemsCBox.FormattingEnabled = true;
            this.itemsCBox.Location = new System.Drawing.Point(81, 11);
            this.itemsCBox.Name = "itemsCBox";
            this.itemsCBox.Size = new System.Drawing.Size(121, 21);
            this.itemsCBox.TabIndex = 0;
            // 
            // selectResult
            // 
            this.selectResult.Controls.Add(this.button3);
            this.selectResult.Controls.Add(this.selectResultItem);
            this.selectResult.Controls.Add(this.label9);
            this.selectResult.Controls.Add(this.pickItemCBox);
            this.selectResult.Location = new System.Drawing.Point(125, 60);
            this.selectResult.Name = "selectResult";
            this.selectResult.Size = new System.Drawing.Size(246, 72);
            this.selectResult.TabIndex = 13;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(42, 38);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(91, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "/ 100";
            // 
            // rateBox
            // 
            this.rateBox.Location = new System.Drawing.Point(49, 135);
            this.rateBox.Name = "rateBox";
            this.rateBox.Size = new System.Drawing.Size(36, 20);
            this.rateBox.TabIndex = 20;
            this.rateBox.TextChanged += new System.EventHandler(this.rateBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 138);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "Rate";
            // 
            // Crafting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 294);
            this.Controls.Add(this.selectResult);
            this.Controls.Add(this.itemSelect);
            this.Controls.Add(this.delButton);
            this.Controls.Add(this.addNewCraft);
            this.Controls.Add(this.requirementPanel);
            this.Controls.Add(this.recipeList);
            this.Controls.Add(this.recipePanel);
            this.Name = "Crafting";
            this.Text = "Crafting";
            this.Size = new System.Drawing.Size(579, 327);
            this.selectResult.ResumeLayout(false);
            this.selectResult.PerformLayout();
            this.itemSelect.ResumeLayout(false);
            this.itemSelect.PerformLayout();
            this.requirementPanel.ResumeLayout(false);
            this.requirementPanel.PerformLayout();
            this.recipePanel.ResumeLayout(false);
            this.recipePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel selectResult;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button selectResultItem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox pickItemCBox;
        private System.Windows.Forms.Panel itemSelect;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button selectBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox amountBox;
        private System.Windows.Forms.ComboBox itemsCBox;
        private System.Windows.Forms.Button delButton;
        private System.Windows.Forms.Button addNewCraft;
        private System.Windows.Forms.Panel requirementPanel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox rateBox;
        private System.Windows.Forms.CheckBox timeBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox dayBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox minuteBox;
        private System.Windows.Forms.TextBox hourbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button addMat;
        private System.Windows.Forms.ListBox requiredMaterials;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox reqLevelBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox reqGoldBox;
        private System.Windows.Forms.ComboBox reqClassCboBox;
        private System.Windows.Forms.ListBox recipeList;
        private System.Windows.Forms.Panel recipePanel;
        private System.Windows.Forms.Button selectResultItemBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox resultBox;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.TextBox recipeNameBox;
    }
}
