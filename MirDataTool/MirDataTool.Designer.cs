namespace MirDataTool
{
    partial class MirDataTool
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabItemTool = new System.Windows.Forms.TabPage();
            this.itemToolPanel = new global::MirDataTool.ItemToolPanel();
            this.tabMobTool = new System.Windows.Forms.TabPage();
            this.monsterToolPanel = new global::MirDataTool.MonsterToolPanel();
            this.tabQuestTool = new System.Windows.Forms.TabPage();
            this.questToolPanel = new global::MirDataTool.QuestToolPanel();
            this.tabMapTool = new System.Windows.Forms.TabPage();
            this.mapToolPanel = new global::MirDataTool.MapToolPanel();
            this.tabNPCTool = new System.Windows.Forms.TabPage();
            this.npcToolPanel = new global::MirDataTool.NPCToolPanel();
            this.tabGameShopTool = new System.Windows.Forms.TabPage();
            this.gameshopToolPanel = new global::MirDataTool.GameShopToolPanel();
            this.tabConquestTool = new System.Windows.Forms.TabPage();
            this.conquestToolPanel = new global::MirDataTool.ConquestToolPanel();
            this.tabDropBuilderTool = new System.Windows.Forms.TabPage();
            this.dropBuilderPanel1 = new global::MirDataTool.DropBuilderPanel();
            this.craftToolTab = new System.Windows.Forms.TabPage();
            this.craftTool = new global::MirDataTool.CraftTool();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabItemTool.SuspendLayout();
            this.tabMobTool.SuspendLayout();
            this.tabQuestTool.SuspendLayout();
            this.tabMapTool.SuspendLayout();
            this.tabNPCTool.SuspendLayout();
            this.tabGameShopTool.SuspendLayout();
            this.tabConquestTool.SuspendLayout();
            this.tabDropBuilderTool.SuspendLayout();
            this.craftToolTab.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabItemTool);
            this.tabControl1.Controls.Add(this.tabMobTool);
            this.tabControl1.Controls.Add(this.tabQuestTool);
            this.tabControl1.Controls.Add(this.tabMapTool);
            this.tabControl1.Controls.Add(this.tabNPCTool);
            this.tabControl1.Controls.Add(this.tabGameShopTool);
            this.tabControl1.Controls.Add(this.tabConquestTool);
            this.tabControl1.Controls.Add(this.tabDropBuilderTool);
            this.tabControl1.Controls.Add(this.craftToolTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1080, 569);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MirDataTool_KeyDown);
            // 
            // tabItemTool
            // 
            this.tabItemTool.Controls.Add(this.itemToolPanel);
            this.tabItemTool.Location = new System.Drawing.Point(4, 22);
            this.tabItemTool.Name = "tabItemTool";
            this.tabItemTool.Padding = new System.Windows.Forms.Padding(3);
            this.tabItemTool.Size = new System.Drawing.Size(1072, 543);
            this.tabItemTool.TabIndex = 0;
            this.tabItemTool.Text = "Item Tool";
            this.tabItemTool.UseVisualStyleBackColor = true;
            // 
            // itemToolPanel
            // 
            this.itemToolPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.itemToolPanel.Location = new System.Drawing.Point(0, 0);
            this.itemToolPanel.Name = "itemToolPanel";
            this.itemToolPanel.Size = new System.Drawing.Size(1070, 540);
            this.itemToolPanel.TabIndex = 0;
            this.itemToolPanel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MirDataTool_KeyDown);
            // 
            // tabMobTool
            // 
            this.tabMobTool.Controls.Add(this.monsterToolPanel);
            this.tabMobTool.Location = new System.Drawing.Point(4, 22);
            this.tabMobTool.Name = "tabMobTool";
            this.tabMobTool.Size = new System.Drawing.Size(1072, 543);
            this.tabMobTool.TabIndex = 2;
            this.tabMobTool.Text = "Monster Tool";
            this.tabMobTool.UseVisualStyleBackColor = true;
            // 
            // monsterToolPanel
            // 
            this.monsterToolPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.monsterToolPanel.Location = new System.Drawing.Point(0, 3);
            this.monsterToolPanel.Name = "monsterToolPanel";
            this.monsterToolPanel.Size = new System.Drawing.Size(1070, 376);
            this.monsterToolPanel.TabIndex = 0;
            this.monsterToolPanel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MirDataTool_KeyDown);
            // 
            // tabQuestTool
            // 
            this.tabQuestTool.Controls.Add(this.questToolPanel);
            this.tabQuestTool.Location = new System.Drawing.Point(4, 22);
            this.tabQuestTool.Name = "tabQuestTool";
            this.tabQuestTool.Size = new System.Drawing.Size(1072, 543);
            this.tabQuestTool.TabIndex = 1;
            this.tabQuestTool.Text = "Quest Tool";
            this.tabQuestTool.UseVisualStyleBackColor = true;
            // 
            // questToolPanel
            // 
            this.questToolPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.questToolPanel.Location = new System.Drawing.Point(0, 0);
            this.questToolPanel.Name = "questToolPanel";
            this.questToolPanel.Size = new System.Drawing.Size(1070, 858);
            this.questToolPanel.TabIndex = 0;
            this.questToolPanel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MirDataTool_KeyDown);
            // 
            // tabMapTool
            // 
            this.tabMapTool.Controls.Add(this.mapToolPanel);
            this.tabMapTool.Location = new System.Drawing.Point(4, 22);
            this.tabMapTool.Name = "tabMapTool";
            this.tabMapTool.Padding = new System.Windows.Forms.Padding(3);
            this.tabMapTool.Size = new System.Drawing.Size(1072, 543);
            this.tabMapTool.TabIndex = 3;
            this.tabMapTool.Text = "Map Tool";
            this.tabMapTool.UseVisualStyleBackColor = true;
            // 
            // mapToolPanel
            // 
            this.mapToolPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.mapToolPanel.Location = new System.Drawing.Point(0, 0);
            this.mapToolPanel.Name = "mapToolPanel";
            this.mapToolPanel.Size = new System.Drawing.Size(1070, 858);
            this.mapToolPanel.TabIndex = 0;
            // 
            // tabNPCTool
            // 
            this.tabNPCTool.Controls.Add(this.npcToolPanel);
            this.tabNPCTool.Location = new System.Drawing.Point(4, 22);
            this.tabNPCTool.Name = "tabNPCTool";
            this.tabNPCTool.Size = new System.Drawing.Size(1072, 543);
            this.tabNPCTool.TabIndex = 4;
            this.tabNPCTool.Text = "NPC Tool";
            this.tabNPCTool.UseVisualStyleBackColor = true;
            // 
            // npcToolPanel
            // 
            this.npcToolPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.npcToolPanel.Location = new System.Drawing.Point(0, 0);
            this.npcToolPanel.Name = "npcToolPanel";
            this.npcToolPanel.Size = new System.Drawing.Size(1070, 858);
            this.npcToolPanel.TabIndex = 0;
            // 
            // tabGameShopTool
            // 
            this.tabGameShopTool.Controls.Add(this.gameshopToolPanel);
            this.tabGameShopTool.Location = new System.Drawing.Point(4, 22);
            this.tabGameShopTool.Name = "tabGameShopTool";
            this.tabGameShopTool.Size = new System.Drawing.Size(1072, 543);
            this.tabGameShopTool.TabIndex = 5;
            this.tabGameShopTool.Text = "GameShop Tool";
            this.tabGameShopTool.UseVisualStyleBackColor = true;
            // 
            // gameshopToolPanel
            // 
            this.gameshopToolPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.gameshopToolPanel.Location = new System.Drawing.Point(143, 55);
            this.gameshopToolPanel.Name = "gameshopToolPanel";
            this.gameshopToolPanel.Size = new System.Drawing.Size(753, 460);
            this.gameshopToolPanel.TabIndex = 0;
            // 
            // tabConquestTool
            // 
            this.tabConquestTool.Controls.Add(this.conquestToolPanel);
            this.tabConquestTool.Location = new System.Drawing.Point(4, 22);
            this.tabConquestTool.Name = "tabConquestTool";
            this.tabConquestTool.Padding = new System.Windows.Forms.Padding(3);
            this.tabConquestTool.Size = new System.Drawing.Size(1072, 543);
            this.tabConquestTool.TabIndex = 7;
            this.tabConquestTool.Text = "Conquest Tool";
            this.tabConquestTool.UseVisualStyleBackColor = true;
            // 
            // conquestToolPanel
            // 
            this.conquestToolPanel.Location = new System.Drawing.Point(0, 0);
            this.conquestToolPanel.Name = "conquestToolPanel";
            this.conquestToolPanel.Size = new System.Drawing.Size(887, 537);
            this.conquestToolPanel.TabIndex = 0;
            // 
            // tabDropBuilderTool
            // 
            this.tabDropBuilderTool.Controls.Add(this.dropBuilderPanel1);
            this.tabDropBuilderTool.Location = new System.Drawing.Point(4, 22);
            this.tabDropBuilderTool.Name = "tabDropBuilderTool";
            this.tabDropBuilderTool.Padding = new System.Windows.Forms.Padding(3);
            this.tabDropBuilderTool.Size = new System.Drawing.Size(1072, 543);
            this.tabDropBuilderTool.TabIndex = 6;
            this.tabDropBuilderTool.Text = "Drop Builder";
            this.tabDropBuilderTool.UseVisualStyleBackColor = true;
            // 
            // dropBuilderPanel1
            // 
            this.dropBuilderPanel1.Location = new System.Drawing.Point(64, 0);
            this.dropBuilderPanel1.Name = "dropBuilderPanel1";
            this.dropBuilderPanel1.Size = new System.Drawing.Size(887, 537);
            this.dropBuilderPanel1.TabIndex = 0;
            // 
            // craftToolTab
            // 
            this.craftToolTab.Controls.Add(this.craftTool);
            this.craftToolTab.Location = new System.Drawing.Point(4, 22);
            this.craftToolTab.Name = "craftToolTab";
            this.craftToolTab.Padding = new System.Windows.Forms.Padding(3);
            this.craftToolTab.Size = new System.Drawing.Size(1072, 543);
            this.craftToolTab.TabIndex = 8;
            this.craftToolTab.Text = "Craft Tool";
            this.craftToolTab.UseVisualStyleBackColor = true;
            // 
            // craftTool
            // 
            this.craftTool.Location = new System.Drawing.Point(0, 0);
            this.craftTool.Name = "craftTool";
            this.craftTool.Size = new System.Drawing.Size(887, 537);
            this.craftTool.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.configToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1080, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MirDataTool_KeyDown);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAllToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.saveAllToolStripMenuItem.Text = "Save &All";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.closeToolStripMenuItem.Text = "C&lose";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.configToolStripMenuItem.Text = "C&onfig";
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // MirDataTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 593);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1096, 632);
            this.MinimumSize = new System.Drawing.Size(1096, 632);
            this.Name = "MirDataTool";
            this.Text = "Mir Data Tool - Developed by Pete107 - Edens-Elite Network.";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MirDataTool_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabItemTool.ResumeLayout(false);
            this.tabMobTool.ResumeLayout(false);
            this.tabQuestTool.ResumeLayout(false);
            this.tabMapTool.ResumeLayout(false);
            this.tabNPCTool.ResumeLayout(false);
            this.tabGameShopTool.ResumeLayout(false);
            this.tabConquestTool.ResumeLayout(false);
            this.tabDropBuilderTool.ResumeLayout(false);
            this.craftToolTab.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabItemTool;
        private System.Windows.Forms.TabPage tabMobTool;
        private System.Windows.Forms.TabPage tabQuestTool;
        private System.Windows.Forms.TabPage tabMapTool;
        private System.Windows.Forms.TabPage tabNPCTool;
        private System.Windows.Forms.TabPage tabGameShopTool;
        private System.Windows.Forms.TabPage craftToolTab;
        private ItemToolPanel itemToolPanel;
        private MonsterToolPanel monsterToolPanel;
        private QuestToolPanel questToolPanel;
        private MapToolPanel mapToolPanel;
        private NPCToolPanel npcToolPanel;
        private GameShopToolPanel gameshopToolPanel;
        private ConquestToolPanel conquestToolPanel;
        private CraftTool craftTool;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TabPage tabDropBuilderTool;
        private DropBuilderPanel dropBuilderPanel1;
        private System.Windows.Forms.TabPage tabConquestTool;
    }
}

