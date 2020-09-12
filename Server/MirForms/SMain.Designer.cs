using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Server
{
    partial class SMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMain));
            this.MainTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DebugLogTextBox = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GlobalMessageButton = new System.Windows.Forms.Button();
            this.GlobalMessageTextBox = new System.Windows.Forms.TextBox();
            this.ChatLogTextBox = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.PlayersOnlineListView = new Server.ListViewNF();
            this.indexHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.levelHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.classHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.genderHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.GuildListView = new Server.ListViewNF();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.PlayersLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MonsterLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ConnectionsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CycleDelayLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rebootServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.robotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.REGameShopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monsterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.magicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nPCScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dropsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginNoticeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.craftingReloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadShieldConfigStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rleoadRecipeShopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadBeneMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadHumMobsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportItemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.logNoticeReloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseFormsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monsterInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nPCInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heroQuestInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.magicInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameshopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.craftInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapEXPConfigMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.balanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dragonSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guildsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fishingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goodsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refiningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relationshipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mentorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conquestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.respawnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GrpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recipeShopToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monsterTunerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InterfaceTimer = new System.Windows.Forms.Timer(this.components);
            this.runningTimeLabel = new System.Windows.Forms.Label();
            this.raidsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTabs
            // 
            this.MainTabs.Controls.Add(this.tabPage1);
            this.MainTabs.Controls.Add(this.tabPage2);
            this.MainTabs.Controls.Add(this.tabPage3);
            this.MainTabs.Controls.Add(this.tabPage4);
            this.MainTabs.Controls.Add(this.tabPage5);
            this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabs.Location = new System.Drawing.Point(0, 24);
            this.MainTabs.Name = "MainTabs";
            this.MainTabs.SelectedIndex = 0;
            this.MainTabs.Size = new System.Drawing.Size(485, 346);
            this.MainTabs.TabIndex = 5;
            this.MainTabs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainTab_MouseClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.LogTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(477, 320);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Logs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTextBox.Location = new System.Drawing.Point(3, 3);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(471, 314);
            this.LogTextBox.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DebugLogTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(477, 320);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Debug Logs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DebugLogTextBox
            // 
            this.DebugLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DebugLogTextBox.Location = new System.Drawing.Point(3, 3);
            this.DebugLogTextBox.Multiline = true;
            this.DebugLogTextBox.Name = "DebugLogTextBox";
            this.DebugLogTextBox.ReadOnly = true;
            this.DebugLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DebugLogTextBox.Size = new System.Drawing.Size(471, 314);
            this.DebugLogTextBox.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.ChatLogTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(477, 320);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Chat Logs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GlobalMessageButton);
            this.groupBox1.Controls.Add(this.GlobalMessageTextBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 272);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 45);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send Message";
            // 
            // GlobalMessageButton
            // 
            this.GlobalMessageButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.GlobalMessageButton.Location = new System.Drawing.Point(395, 16);
            this.GlobalMessageButton.Name = "GlobalMessageButton";
            this.GlobalMessageButton.Size = new System.Drawing.Size(73, 26);
            this.GlobalMessageButton.TabIndex = 0;
            this.GlobalMessageButton.Text = "Send";
            this.GlobalMessageButton.UseVisualStyleBackColor = true;
            this.GlobalMessageButton.Click += new System.EventHandler(this.GlobalMessageButton_Click);
            // 
            // GlobalMessageTextBox
            // 
            this.GlobalMessageTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.GlobalMessageTextBox.Location = new System.Drawing.Point(3, 16);
            this.GlobalMessageTextBox.Name = "GlobalMessageTextBox";
            this.GlobalMessageTextBox.Size = new System.Drawing.Size(380, 20);
            this.GlobalMessageTextBox.TabIndex = 0;
            // 
            // ChatLogTextBox
            // 
            this.ChatLogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatLogTextBox.Location = new System.Drawing.Point(3, 3);
            this.ChatLogTextBox.Multiline = true;
            this.ChatLogTextBox.Name = "ChatLogTextBox";
            this.ChatLogTextBox.ReadOnly = true;
            this.ChatLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatLogTextBox.Size = new System.Drawing.Size(474, 269);
            this.ChatLogTextBox.TabIndex = 4;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.PlayersOnlineListView);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(477, 320);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Players Online";
            // 
            // PlayersOnlineListView
            // 
            this.PlayersOnlineListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.PlayersOnlineListView.BackColor = System.Drawing.SystemColors.Window;
            this.PlayersOnlineListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.indexHeader,
            this.nameHeader,
            this.levelHeader,
            this.classHeader,
            this.genderHeader});
            this.PlayersOnlineListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayersOnlineListView.FullRowSelect = true;
            this.PlayersOnlineListView.GridLines = true;
            this.PlayersOnlineListView.Location = new System.Drawing.Point(3, 3);
            this.PlayersOnlineListView.Margin = new System.Windows.Forms.Padding(2);
            this.PlayersOnlineListView.Name = "PlayersOnlineListView";
            this.PlayersOnlineListView.Size = new System.Drawing.Size(471, 314);
            this.PlayersOnlineListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.PlayersOnlineListView.TabIndex = 0;
            this.PlayersOnlineListView.UseCompatibleStateImageBehavior = false;
            this.PlayersOnlineListView.View = System.Windows.Forms.View.Details;
            this.PlayersOnlineListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.PlayersOnlineListView_ColumnWidthChanging);
            this.PlayersOnlineListView.DoubleClick += new System.EventHandler(this.PlayersOnlineListView_DoubleClick);
            // 
            // indexHeader
            // 
            this.indexHeader.Text = "Index";
            this.indexHeader.Width = 71;
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "Name";
            this.nameHeader.Width = 93;
            // 
            // levelHeader
            // 
            this.levelHeader.Text = "Level";
            this.levelHeader.Width = 90;
            // 
            // classHeader
            // 
            this.classHeader.Text = "Class";
            this.classHeader.Width = 100;
            // 
            // genderHeader
            // 
            this.genderHeader.Text = "Gender";
            this.genderHeader.Width = 98;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.GuildListView);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(477, 320);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Guilds List";
            // 
            // GuildListView
            // 
            this.GuildListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.GuildListView.BackColor = System.Drawing.SystemColors.Window;
            this.GuildListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.GT});
            this.GuildListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GuildListView.FullRowSelect = true;
            this.GuildListView.GridLines = true;
            this.GuildListView.Location = new System.Drawing.Point(3, 3);
            this.GuildListView.Name = "GuildListView";
            this.GuildListView.Size = new System.Drawing.Size(471, 314);
            this.GuildListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.GuildListView.TabIndex = 1;
            this.GuildListView.UseCompatibleStateImageBehavior = false;
            this.GuildListView.View = System.Windows.Forms.View.Details;
            this.GuildListView.DoubleClick += new System.EventHandler(this.GuildListView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Index";
            this.columnHeader1.Width = 59;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 113;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Leader";
            this.columnHeader3.Width = 98;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Member Count";
            this.columnHeader4.Width = 82;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Level";
            this.columnHeader5.Width = 73;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Gold";
            this.columnHeader6.Width = 74;
            // 
            // GT
            // 
            this.GT.Text = "GT";
            this.GT.Width = 118;
            // 
            // StatusBar
            // 
            this.StatusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlayersLabel,
            this.MonsterLabel,
            this.ConnectionsLabel,
            this.CycleDelayLabel});
            this.StatusBar.Location = new System.Drawing.Point(0, 370);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(485, 24);
            this.StatusBar.SizingGrip = false;
            this.StatusBar.TabIndex = 4;
            this.StatusBar.Text = "statusStrip1";
            // 
            // PlayersLabel
            // 
            this.PlayersLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.PlayersLabel.Name = "PlayersLabel";
            this.PlayersLabel.Size = new System.Drawing.Size(60, 19);
            this.PlayersLabel.Text = "Players: 0";
            // 
            // MonsterLabel
            // 
            this.MonsterLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.MonsterLabel.Name = "MonsterLabel";
            this.MonsterLabel.Size = new System.Drawing.Size(72, 19);
            this.MonsterLabel.Text = "Monsters: 0";
            // 
            // ConnectionsLabel
            // 
            this.ConnectionsLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.ConnectionsLabel.Name = "ConnectionsLabel";
            this.ConnectionsLabel.Size = new System.Drawing.Size(90, 19);
            this.ConnectionsLabel.Text = "Connections: 0";
            // 
            // CycleDelayLabel
            // 
            this.CycleDelayLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.CycleDelayLabel.Name = "CycleDelayLabel";
            this.CycleDelayLabel.Size = new System.Drawing.Size(81, 19);
            this.CycleDelayLabel.Text = "CycleDelay: 0";
            // 
            // MainMenu
            // 
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlToolStripMenuItem,
            this.accountToolStripMenuItem,
            this.databaseFormsToolStripMenuItem,
            this.configToolStripMenuItem1});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(485, 24);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "menuStrip1";
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServerToolStripMenuItem,
            this.stopServerToolStripMenuItem,
            this.rebootServerToolStripMenuItem,
            this.toolStripMenuItem1,
            this.closeServerToolStripMenuItem,
            this.reloadToolStripMenuItem});
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.controlToolStripMenuItem.Text = "Control";
            // 
            // startServerToolStripMenuItem
            // 
            this.startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            this.startServerToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.startServerToolStripMenuItem.Text = "Start Server";
            this.startServerToolStripMenuItem.Click += new System.EventHandler(this.startServerToolStripMenuItem_Click);
            // 
            // stopServerToolStripMenuItem
            // 
            this.stopServerToolStripMenuItem.Name = "stopServerToolStripMenuItem";
            this.stopServerToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.stopServerToolStripMenuItem.Text = "Stop Server";
            this.stopServerToolStripMenuItem.Click += new System.EventHandler(this.stopServerToolStripMenuItem_Click);
            // 
            // rebootServerToolStripMenuItem
            // 
            this.rebootServerToolStripMenuItem.Name = "rebootServerToolStripMenuItem";
            this.rebootServerToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.rebootServerToolStripMenuItem.Text = "Reboot Server";
            this.rebootServerToolStripMenuItem.Click += new System.EventHandler(this.rebootServerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(144, 6);
            // 
            // closeServerToolStripMenuItem
            // 
            this.closeServerToolStripMenuItem.Name = "closeServerToolStripMenuItem";
            this.closeServerToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.closeServerToolStripMenuItem.Text = "Close Server";
            this.closeServerToolStripMenuItem.Click += new System.EventHandler(this.closeServerToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.robotToolStripMenuItem,
            this.itemsToolStripMenuItem,
            this.REGameShopToolStripMenuItem,
            this.monsterToolStripMenuItem,
            this.magicToolStripMenuItem,
            this.nPCScriptToolStripMenuItem,
            this.dropsToolStripMenuItem,
            this.questToolStripMenuItem,
            this.lineMessageToolStripMenuItem,
            this.loginNoticeToolStripMenuItem,
            this.craftingReloadToolStripMenuItem,
            this.reloadShieldConfigStripMenuItem,
            this.rleoadRecipeShopMenuItem,
            this.reloadBeneMenuItem,
            this.reloadHumMobsMenuItem,
            this.exportItemInfo,
            this.logNoticeReloadToolStripMenuItem});
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            // 
            // robotToolStripMenuItem
            // 
            this.robotToolStripMenuItem.Name = "robotToolStripMenuItem";
            this.robotToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.robotToolStripMenuItem.Text = "Robot";
            this.robotToolStripMenuItem.Click += new System.EventHandler(this.robotToolStripMenuItem_Click);
            // 
            // itemsToolStripMenuItem
            // 
            this.itemsToolStripMenuItem.Name = "itemsToolStripMenuItem";
            this.itemsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.itemsToolStripMenuItem.Text = "Items";
            this.itemsToolStripMenuItem.Click += new System.EventHandler(this.itemsToolStripMenuItem_Click);
            // 
            // REGameShopToolStripMenuItem
            // 
            this.REGameShopToolStripMenuItem.Name = "REGameShopToolStripMenuItem";
            this.REGameShopToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.REGameShopToolStripMenuItem.Text = "Gameshop";
            this.REGameShopToolStripMenuItem.Click += new System.EventHandler(this.REGameShopToolStripMenuItem_Click);
            // 
            // monsterToolStripMenuItem
            // 
            this.monsterToolStripMenuItem.Name = "monsterToolStripMenuItem";
            this.monsterToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.monsterToolStripMenuItem.Text = "Monster";
            this.monsterToolStripMenuItem.Click += new System.EventHandler(this.monsterToolStripMenuItem_Click);
            // 
            // magicToolStripMenuItem
            // 
            this.magicToolStripMenuItem.Name = "magicToolStripMenuItem";
            this.magicToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.magicToolStripMenuItem.Text = "Magic";
            this.magicToolStripMenuItem.Click += new System.EventHandler(this.magicToolStripMenuItem_Click);
            // 
            // nPCScriptToolStripMenuItem
            // 
            this.nPCScriptToolStripMenuItem.Name = "nPCScriptToolStripMenuItem";
            this.nPCScriptToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.nPCScriptToolStripMenuItem.Text = "NPC Script";
            this.nPCScriptToolStripMenuItem.Click += new System.EventHandler(this.nPCScriptToolStripMenuItem_Click);
            // 
            // dropsToolStripMenuItem
            // 
            this.dropsToolStripMenuItem.Name = "dropsToolStripMenuItem";
            this.dropsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.dropsToolStripMenuItem.Text = "Drops";
            this.dropsToolStripMenuItem.Click += new System.EventHandler(this.dropsToolStripMenuItem_Click);
            // 
            // questToolStripMenuItem
            // 
            this.questToolStripMenuItem.Name = "questToolStripMenuItem";
            this.questToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.questToolStripMenuItem.Text = "Quest";
            this.questToolStripMenuItem.Click += new System.EventHandler(this.questToolStripMenuItem_Click);
            // 
            // lineMessageToolStripMenuItem
            // 
            this.lineMessageToolStripMenuItem.Name = "lineMessageToolStripMenuItem";
            this.lineMessageToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.lineMessageToolStripMenuItem.Text = "Line Message";
            this.lineMessageToolStripMenuItem.Click += new System.EventHandler(this.lineMessageToolStripMenuItem_Click);
            // 
            // loginNoticeToolStripMenuItem
            // 
            this.loginNoticeToolStripMenuItem.Name = "loginNoticeToolStripMenuItem";
            this.loginNoticeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.loginNoticeToolStripMenuItem.Text = "Login Message";
            this.loginNoticeToolStripMenuItem.Click += new System.EventHandler(this.LoginNoticeToolStripMenuItem_Click);
            // 
            // craftingReloadToolStripMenuItem
            // 
            this.craftingReloadToolStripMenuItem.Name = "craftingReloadToolStripMenuItem";
            this.craftingReloadToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.craftingReloadToolStripMenuItem.Text = "Crafting";
            this.craftingReloadToolStripMenuItem.Click += new System.EventHandler(this.craftingReloadToolStripMenuItem_Click);
            // 
            // reloadShieldConfigStripMenuItem
            // 
            this.reloadShieldConfigStripMenuItem.Name = "reloadShieldConfigStripMenuItem";
            this.reloadShieldConfigStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.reloadShieldConfigStripMenuItem.Text = "Shield";
            this.reloadShieldConfigStripMenuItem.Click += new System.EventHandler(this.reloadShieldConfigStripMenuItem_Click);
            // 
            // rleoadRecipeShopMenuItem
            // 
            this.rleoadRecipeShopMenuItem.Name = "rleoadRecipeShopMenuItem";
            this.rleoadRecipeShopMenuItem.Size = new System.Drawing.Size(171, 22);
            this.rleoadRecipeShopMenuItem.Text = "RecipeShop";
            this.rleoadRecipeShopMenuItem.Click += new System.EventHandler(this.rleoadRecipeShopMenuItem_Click);
            // 
            // reloadBeneMenuItem
            // 
            this.reloadBeneMenuItem.Name = "reloadBeneMenuItem";
            this.reloadBeneMenuItem.Size = new System.Drawing.Size(171, 22);
            this.reloadBeneMenuItem.Text = "Bene Config";
            this.reloadBeneMenuItem.Click += new System.EventHandler(this.reloadBeneMenuItem_Click);
            // 
            // reloadHumMobsMenuItem
            // 
            this.reloadHumMobsMenuItem.Name = "reloadHumMobsMenuItem";
            this.reloadHumMobsMenuItem.Size = new System.Drawing.Size(171, 22);
            this.reloadHumMobsMenuItem.Text = "Hum Mobs";
            this.reloadHumMobsMenuItem.Click += new System.EventHandler(this.reloadHumMobsMenuItem_Click);
            // 
            // exportItemInfo
            // 
            this.exportItemInfo.Name = "exportItemInfo";
            this.exportItemInfo.Size = new System.Drawing.Size(171, 22);
            this.exportItemInfo.Text = "Export ItemInfo";
            this.exportItemInfo.Click += new System.EventHandler(this.exportItemInfo_Click);
            // 
            // logNoticeReloadToolStripMenuItem
            // 
            this.logNoticeReloadToolStripMenuItem.Name = "logNoticeReloadToolStripMenuItem";
            this.logNoticeReloadToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.logNoticeReloadToolStripMenuItem.Text = "Reload Log Notice";
            this.logNoticeReloadToolStripMenuItem.Click += new System.EventHandler(this.logNoticeReloadToolStripMenuItem_Click);
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.accountToolStripMenuItem.Text = "Account";
            this.accountToolStripMenuItem.Click += new System.EventHandler(this.accountToolStripMenuItem_Click);
            // 
            // databaseFormsToolStripMenuItem
            // 
            this.databaseFormsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapInfoToolStripMenuItem,
            this.itemInfoToolStripMenuItem,
            this.monsterInfoToolStripMenuItem,
            this.nPCInfoToolStripMenuItem,
            this.questInfoToolStripMenuItem,
            this.heroQuestInfoToolStripMenuItem,
            this.magicInfoToolStripMenuItem,
            this.gameshopToolStripMenuItem,
            this.craftInfoToolStripMenuItem,
            this.mapEXPConfigMenuItem,
            this.raidsToolStripMenuItem});
            this.databaseFormsToolStripMenuItem.Name = "databaseFormsToolStripMenuItem";
            this.databaseFormsToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.databaseFormsToolStripMenuItem.Text = "Database";
            // 
            // mapInfoToolStripMenuItem
            // 
            this.mapInfoToolStripMenuItem.Name = "mapInfoToolStripMenuItem";
            this.mapInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.mapInfoToolStripMenuItem.Text = "Map";
            this.mapInfoToolStripMenuItem.Click += new System.EventHandler(this.mapInfoToolStripMenuItem_Click);
            // 
            // itemInfoToolStripMenuItem
            // 
            this.itemInfoToolStripMenuItem.Name = "itemInfoToolStripMenuItem";
            this.itemInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.itemInfoToolStripMenuItem.Text = "Item";
            this.itemInfoToolStripMenuItem.Click += new System.EventHandler(this.itemInfoToolStripMenuItem_Click);
            // 
            // monsterInfoToolStripMenuItem
            // 
            this.monsterInfoToolStripMenuItem.Name = "monsterInfoToolStripMenuItem";
            this.monsterInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.monsterInfoToolStripMenuItem.Text = "Monster";
            this.monsterInfoToolStripMenuItem.Click += new System.EventHandler(this.monsterInfoToolStripMenuItem_Click);
            // 
            // nPCInfoToolStripMenuItem
            // 
            this.nPCInfoToolStripMenuItem.Name = "nPCInfoToolStripMenuItem";
            this.nPCInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nPCInfoToolStripMenuItem.Text = "NPC";
            this.nPCInfoToolStripMenuItem.Click += new System.EventHandler(this.nPCInfoToolStripMenuItem_Click);
            // 
            // questInfoToolStripMenuItem
            // 
            this.questInfoToolStripMenuItem.Name = "questInfoToolStripMenuItem";
            this.questInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.questInfoToolStripMenuItem.Text = "Quest";
            this.questInfoToolStripMenuItem.Click += new System.EventHandler(this.questInfoToolStripMenuItem_Click);
            // 
            // heroQuestInfoToolStripMenuItem
            // 
            this.heroQuestInfoToolStripMenuItem.Name = "heroQuestInfoToolStripMenuItem";
            this.heroQuestInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.heroQuestInfoToolStripMenuItem.Text = "Hero Quest";
            this.heroQuestInfoToolStripMenuItem.Click += new System.EventHandler(this.heroQuestInfoToolStripMenuItem_Click);
            // 
            // magicInfoToolStripMenuItem
            // 
            this.magicInfoToolStripMenuItem.Name = "magicInfoToolStripMenuItem";
            this.magicInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.magicInfoToolStripMenuItem.Text = "Magic";
            this.magicInfoToolStripMenuItem.Click += new System.EventHandler(this.magicInfoToolStripMenuItem_Click);
            // 
            // gameshopToolStripMenuItem
            // 
            this.gameshopToolStripMenuItem.Name = "gameshopToolStripMenuItem";
            this.gameshopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gameshopToolStripMenuItem.Text = "Gameshop";
            this.gameshopToolStripMenuItem.Click += new System.EventHandler(this.gameshopToolStripMenuItem_Click);
            // 
            // craftInfoToolStripMenuItem
            // 
            this.craftInfoToolStripMenuItem.Name = "craftInfoToolStripMenuItem";
            this.craftInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.craftInfoToolStripMenuItem.Text = "Crafting";
            this.craftInfoToolStripMenuItem.Click += new System.EventHandler(this.craftInfoToolStripMenuItem_Click);
            // 
            // mapEXPConfigMenuItem
            // 
            this.mapEXPConfigMenuItem.Name = "mapEXPConfigMenuItem";
            this.mapEXPConfigMenuItem.Size = new System.Drawing.Size(180, 22);
            this.mapEXPConfigMenuItem.Text = "Map EXP Settings";
            this.mapEXPConfigMenuItem.Click += new System.EventHandler(this.mapEXPConfigMenuItem_Click);
            // 
            // configToolStripMenuItem1
            // 
            this.configToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverToolStripMenuItem,
            this.balanceToolStripMenuItem,
            this.systemToolStripMenuItem,
            this.monsterTunerToolStripMenuItem});
            this.configToolStripMenuItem1.Name = "configToolStripMenuItem1";
            this.configToolStripMenuItem1.Size = new System.Drawing.Size(55, 20);
            this.configToolStripMenuItem1.Text = "Config";
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.serverToolStripMenuItem.Text = "Server";
            this.serverToolStripMenuItem.Click += new System.EventHandler(this.serverToolStripMenuItem_Click);
            // 
            // balanceToolStripMenuItem
            // 
            this.balanceToolStripMenuItem.Name = "balanceToolStripMenuItem";
            this.balanceToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.balanceToolStripMenuItem.Text = "Balance";
            this.balanceToolStripMenuItem.Click += new System.EventHandler(this.balanceToolStripMenuItem_Click);
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dragonSystemToolStripMenuItem,
            this.miningToolStripMenuItem,
            this.guildsToolStripMenuItem,
            this.fishingToolStripMenuItem,
            this.mailToolStripMenuItem,
            this.goodsToolStripMenuItem,
            this.refiningToolStripMenuItem,
            this.relationshipToolStripMenuItem,
            this.mentorToolStripMenuItem,
            this.gemToolStripMenuItem,
            this.conquestToolStripMenuItem,
            this.respawnsToolStripMenuItem,
            this.GrpToolStripMenuItem,
            this.heroToolStripMenuItem,
            this.recipeShopToolItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.systemToolStripMenuItem.Text = "System";
            // 
            // dragonSystemToolStripMenuItem
            // 
            this.dragonSystemToolStripMenuItem.Name = "dragonSystemToolStripMenuItem";
            this.dragonSystemToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.dragonSystemToolStripMenuItem.Text = "Dragon";
            this.dragonSystemToolStripMenuItem.Click += new System.EventHandler(this.dragonSystemToolStripMenuItem_Click);
            // 
            // miningToolStripMenuItem
            // 
            this.miningToolStripMenuItem.Name = "miningToolStripMenuItem";
            this.miningToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.miningToolStripMenuItem.Text = "Mining";
            this.miningToolStripMenuItem.Click += new System.EventHandler(this.miningToolStripMenuItem_Click);
            // 
            // guildsToolStripMenuItem
            // 
            this.guildsToolStripMenuItem.Name = "guildsToolStripMenuItem";
            this.guildsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.guildsToolStripMenuItem.Text = "Guilds";
            this.guildsToolStripMenuItem.Click += new System.EventHandler(this.guildsToolStripMenuItem_Click);
            // 
            // fishingToolStripMenuItem
            // 
            this.fishingToolStripMenuItem.Name = "fishingToolStripMenuItem";
            this.fishingToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.fishingToolStripMenuItem.Text = "Fishing";
            this.fishingToolStripMenuItem.Click += new System.EventHandler(this.fishingToolStripMenuItem_Click);
            // 
            // mailToolStripMenuItem
            // 
            this.mailToolStripMenuItem.Name = "mailToolStripMenuItem";
            this.mailToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.mailToolStripMenuItem.Text = "Mail";
            this.mailToolStripMenuItem.Click += new System.EventHandler(this.mailToolStripMenuItem_Click);
            // 
            // goodsToolStripMenuItem
            // 
            this.goodsToolStripMenuItem.Name = "goodsToolStripMenuItem";
            this.goodsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.goodsToolStripMenuItem.Text = "Goods";
            this.goodsToolStripMenuItem.Click += new System.EventHandler(this.goodsToolStripMenuItem_Click);
            // 
            // refiningToolStripMenuItem
            // 
            this.refiningToolStripMenuItem.Name = "refiningToolStripMenuItem";
            this.refiningToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.refiningToolStripMenuItem.Text = "Refining";
            this.refiningToolStripMenuItem.Click += new System.EventHandler(this.refiningToolStripMenuItem_Click);
            // 
            // relationshipToolStripMenuItem
            // 
            this.relationshipToolStripMenuItem.Name = "relationshipToolStripMenuItem";
            this.relationshipToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.relationshipToolStripMenuItem.Text = "Relationship";
            this.relationshipToolStripMenuItem.Click += new System.EventHandler(this.relationshipToolStripMenuItem_Click);
            // 
            // mentorToolStripMenuItem
            // 
            this.mentorToolStripMenuItem.Name = "mentorToolStripMenuItem";
            this.mentorToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.mentorToolStripMenuItem.Text = "Mentor";
            this.mentorToolStripMenuItem.Click += new System.EventHandler(this.mentorToolStripMenuItem_Click);
            // 
            // gemToolStripMenuItem
            // 
            this.gemToolStripMenuItem.Name = "gemToolStripMenuItem";
            this.gemToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.gemToolStripMenuItem.Text = "Gem";
            this.gemToolStripMenuItem.Click += new System.EventHandler(this.gemToolStripMenuItem_Click);
            // 
            // conquestToolStripMenuItem
            // 
            this.conquestToolStripMenuItem.Name = "conquestToolStripMenuItem";
            this.conquestToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.conquestToolStripMenuItem.Text = "Conquest";
            this.conquestToolStripMenuItem.Click += new System.EventHandler(this.conquestToolStripMenuItem_Click);
            // 
            // respawnsToolStripMenuItem
            // 
            this.respawnsToolStripMenuItem.Name = "respawnsToolStripMenuItem";
            this.respawnsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.respawnsToolStripMenuItem.Text = "SpawnTick";
            this.respawnsToolStripMenuItem.Click += new System.EventHandler(this.respawnsToolStripMenuItem_Click);
            // 
            // GrpToolStripMenuItem
            // 
            this.GrpToolStripMenuItem.Name = "GrpToolStripMenuItem";
            this.GrpToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.GrpToolStripMenuItem.Text = "Group";
            this.GrpToolStripMenuItem.Click += new System.EventHandler(this.GrpToolStripMenuItem_Click);
            // 
            // heroToolStripMenuItem
            // 
            this.heroToolStripMenuItem.Name = "heroToolStripMenuItem";
            this.heroToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.heroToolStripMenuItem.Text = "Hero";
            this.heroToolStripMenuItem.Click += new System.EventHandler(this.heroToolStripMenuItem_Click);
            // 
            // recipeShopToolItem
            // 
            this.recipeShopToolItem.Name = "recipeShopToolItem";
            this.recipeShopToolItem.Size = new System.Drawing.Size(139, 22);
            this.recipeShopToolItem.Text = "Recipe Shop";
            this.recipeShopToolItem.Click += new System.EventHandler(this.recipeShopToolItem_Click);
            // 
            // monsterTunerToolStripMenuItem
            // 
            this.monsterTunerToolStripMenuItem.Name = "monsterTunerToolStripMenuItem";
            this.monsterTunerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.monsterTunerToolStripMenuItem.Text = "Monster Tuner";
            this.monsterTunerToolStripMenuItem.Click += new System.EventHandler(this.monsterTunerToolStripMenuItem_Click);
            // 
            // InterfaceTimer
            // 
            this.InterfaceTimer.Enabled = true;
            this.InterfaceTimer.Tick += new System.EventHandler(this.InterfaceTimer_Tick);
            // 
            // runningTimeLabel
            // 
            this.runningTimeLabel.AutoSize = true;
            this.runningTimeLabel.Location = new System.Drawing.Point(282, 5);
            this.runningTimeLabel.Name = "runningTimeLabel";
            this.runningTimeLabel.Size = new System.Drawing.Size(76, 13);
            this.runningTimeLabel.TabIndex = 6;
            this.runningTimeLabel.Text = "Running Time:";
            // 
            // raidsToolStripMenuItem
            // 
            this.raidsToolStripMenuItem.Name = "raidsToolStripMenuItem";
            this.raidsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.raidsToolStripMenuItem.Text = "Raids";
            this.raidsToolStripMenuItem.Click += new System.EventHandler(this.raidsToolStripMenuItem_Click);
            // 
            // SMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(485, 394);
            this.Controls.Add(this.runningTimeLabel);
            this.Controls.Add(this.MainTabs);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SMain";
            this.Text = "Legend of Mir Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SMain_FormClosing);
            this.Load += new System.EventHandler(this.SMain_Load);
            this.MainTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl MainTabs;
        private TabPage tabPage1;
        private TextBox LogTextBox;
        private StatusStrip StatusBar;
        private ToolStripStatusLabel PlayersLabel;
        private ToolStripStatusLabel MonsterLabel;
        private ToolStripStatusLabel ConnectionsLabel;
        private MenuStrip MainMenu;
        private ToolStripMenuItem controlToolStripMenuItem;
        private ToolStripMenuItem startServerToolStripMenuItem;
        private ToolStripMenuItem stopServerToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem closeServerToolStripMenuItem;
        private Timer InterfaceTimer;
        private TabPage tabPage2;
        private TextBox DebugLogTextBox;
        private TabPage tabPage3;
        private TextBox ChatLogTextBox;
        private ToolStripMenuItem accountToolStripMenuItem;
        private ToolStripMenuItem databaseFormsToolStripMenuItem;
        private ToolStripMenuItem mapInfoToolStripMenuItem;
        private ToolStripMenuItem itemInfoToolStripMenuItem;
        private ToolStripMenuItem monsterInfoToolStripMenuItem;
        private ToolStripMenuItem craftInfoToolStripMenuItem;
        private ToolStripMenuItem nPCInfoToolStripMenuItem;
        private ToolStripMenuItem questInfoToolStripMenuItem;
        private ToolStripMenuItem heroQuestInfoToolStripMenuItem;
        private ToolStripMenuItem configToolStripMenuItem1;
        private ToolStripMenuItem serverToolStripMenuItem;
        private ToolStripMenuItem balanceToolStripMenuItem;
        private ToolStripMenuItem systemToolStripMenuItem;
        private ToolStripMenuItem dragonSystemToolStripMenuItem;
        private ToolStripMenuItem guildsToolStripMenuItem;
        private ToolStripMenuItem miningToolStripMenuItem;
        private ToolStripMenuItem fishingToolStripMenuItem;
        private TabPage tabPage4;
        private GroupBox groupBox1;
        private Button GlobalMessageButton;
        private TextBox GlobalMessageTextBox;
        private TabPage tabPage5;
        private global::Server.ListViewNF GuildListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private global::Server.ListViewNF PlayersOnlineListView;
        private ColumnHeader nameHeader;
        private ColumnHeader levelHeader;
        private ColumnHeader classHeader;
        private ColumnHeader genderHeader;
        private ColumnHeader indexHeader;
        private ToolStripMenuItem mailToolStripMenuItem;
        private ToolStripMenuItem goodsToolStripMenuItem;
        private ToolStripStatusLabel CycleDelayLabel;
        private ToolStripMenuItem magicInfoToolStripMenuItem;
        private ToolStripMenuItem refiningToolStripMenuItem;
        private ToolStripMenuItem relationshipToolStripMenuItem;
        private ToolStripMenuItem mentorToolStripMenuItem;
        private ToolStripMenuItem gameshopToolStripMenuItem;
        private ToolStripMenuItem gemToolStripMenuItem;
        private ToolStripMenuItem conquestToolStripMenuItem;
        private ToolStripMenuItem rebootServerToolStripMenuItem;
        private ToolStripMenuItem respawnsToolStripMenuItem;
        private ToolStripMenuItem monsterTunerToolStripMenuItem;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem reloadBeneMenuItem;
        private ToolStripMenuItem reloadHumMobsMenuItem;
        private ToolStripMenuItem exportItemInfo;
        private ToolStripMenuItem rleoadRecipeShopMenuItem;
        private ToolStripMenuItem robotToolStripMenuItem;
        private ToolStripMenuItem itemsToolStripMenuItem;
        private ToolStripMenuItem REGameShopToolStripMenuItem;
        private ToolStripMenuItem monsterToolStripMenuItem;
        private ToolStripMenuItem magicToolStripMenuItem;
        private ToolStripMenuItem nPCScriptToolStripMenuItem;
        private ToolStripMenuItem dropsToolStripMenuItem;
        private ToolStripMenuItem questToolStripMenuItem;
        private ToolStripMenuItem lineMessageToolStripMenuItem;
        private ToolStripMenuItem loginNoticeToolStripMenuItem;
        private ToolStripMenuItem craftingReloadToolStripMenuItem;
        private ToolStripMenuItem reloadShieldConfigStripMenuItem;
        private ToolStripMenuItem mapEXPConfigMenuItem;
        private Label runningTimeLabel;
        private ToolStripMenuItem heroToolStripMenuItem;
        private ToolStripMenuItem GrpToolStripMenuItem;
        private ToolStripMenuItem recipeShopToolItem;
        private ToolStripMenuItem logNoticeReloadToolStripMenuItem;
        private ColumnHeader GT;
        private ToolStripMenuItem raidsToolStripMenuItem;
    }
}

