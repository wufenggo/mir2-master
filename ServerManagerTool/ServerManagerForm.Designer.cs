namespace ServerManagerTool
{
    partial class ServerManagerForm
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
            this.managerTabControl = new System.Windows.Forms.TabControl();
            this.statsTab = new System.Windows.Forms.TabPage();
            this.serverStatus = new System.Windows.Forms.Label();
            this.cpuLbl = new System.Windows.Forms.Label();
            this.ramLbl = new System.Windows.Forms.Label();
            this.cycleLbl = new System.Windows.Forms.Label();
            this.uptimeLbl = new System.Windows.Forms.Label();
            this.mobCountLbl = new System.Windows.Forms.Label();
            this.playerCountLbl = new System.Windows.Forms.Label();
            this.consoleTab = new System.Windows.Forms.TabPage();
            this.notifyBox = new System.Windows.Forms.TextBox();
            this.managerConsole = new System.Windows.Forms.TextBox();
            this.chatLogTab = new System.Windows.Forms.TabPage();
            this.chatConsole = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.playerOnlineList = new System.Windows.Forms.DataGridView();
            this.playerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerGuild = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerMap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountGold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.configBox = new System.Windows.Forms.GroupBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.connectionBtn = new System.Windows.Forms.Button();
            this.managerTabControl.SuspendLayout();
            this.statsTab.SuspendLayout();
            this.consoleTab.SuspendLayout();
            this.chatLogTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerOnlineList)).BeginInit();
            this.configBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // managerTabControl
            // 
            this.managerTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.managerTabControl.Controls.Add(this.statsTab);
            this.managerTabControl.Controls.Add(this.consoleTab);
            this.managerTabControl.Controls.Add(this.chatLogTab);
            this.managerTabControl.Controls.Add(this.tabPage1);
            this.managerTabControl.Location = new System.Drawing.Point(12, 107);
            this.managerTabControl.Name = "managerTabControl";
            this.managerTabControl.SelectedIndex = 0;
            this.managerTabControl.Size = new System.Drawing.Size(493, 331);
            this.managerTabControl.TabIndex = 0;
            // 
            // statsTab
            // 
            this.statsTab.Controls.Add(this.serverStatus);
            this.statsTab.Controls.Add(this.cpuLbl);
            this.statsTab.Controls.Add(this.ramLbl);
            this.statsTab.Controls.Add(this.cycleLbl);
            this.statsTab.Controls.Add(this.uptimeLbl);
            this.statsTab.Controls.Add(this.mobCountLbl);
            this.statsTab.Controls.Add(this.playerCountLbl);
            this.statsTab.Location = new System.Drawing.Point(4, 22);
            this.statsTab.Name = "statsTab";
            this.statsTab.Padding = new System.Windows.Forms.Padding(3);
            this.statsTab.Size = new System.Drawing.Size(485, 305);
            this.statsTab.TabIndex = 0;
            this.statsTab.Text = "Statistics (Live)";
            this.statsTab.UseVisualStyleBackColor = true;
            // 
            // serverStatus
            // 
            this.serverStatus.AutoSize = true;
            this.serverStatus.Location = new System.Drawing.Point(54, 31);
            this.serverStatus.Name = "serverStatus";
            this.serverStatus.Size = new System.Drawing.Size(77, 13);
            this.serverStatus.TabIndex = 6;
            this.serverStatus.Text = "Server Status :";
            // 
            // cpuLbl
            // 
            this.cpuLbl.AutoSize = true;
            this.cpuLbl.Location = new System.Drawing.Point(48, 139);
            this.cpuLbl.Name = "cpuLbl";
            this.cpuLbl.Size = new System.Drawing.Size(81, 13);
            this.cpuLbl.TabIndex = 5;
            this.cpuLbl.Text = "Available CPU :";
            // 
            // ramLbl
            // 
            this.ramLbl.AutoSize = true;
            this.ramLbl.Location = new System.Drawing.Point(48, 121);
            this.ramLbl.Name = "ramLbl";
            this.ramLbl.Size = new System.Drawing.Size(83, 13);
            this.ramLbl.TabIndex = 4;
            this.ramLbl.Text = "Available RAM :";
            // 
            // cycleLbl
            // 
            this.cycleLbl.AutoSize = true;
            this.cycleLbl.Location = new System.Drawing.Point(51, 103);
            this.cycleLbl.Name = "cycleLbl";
            this.cycleLbl.Size = new System.Drawing.Size(78, 13);
            this.cycleLbl.TabIndex = 3;
            this.cycleLbl.Text = "Server Cycles :";
            // 
            // uptimeLbl
            // 
            this.uptimeLbl.AutoSize = true;
            this.uptimeLbl.Location = new System.Drawing.Point(12, 85);
            this.uptimeLbl.Name = "uptimeLbl";
            this.uptimeLbl.Size = new System.Drawing.Size(117, 13);
            this.uptimeLbl.TabIndex = 2;
            this.uptimeLbl.Text = "Server Online Counter :";
            // 
            // mobCountLbl
            // 
            this.mobCountLbl.AutoSize = true;
            this.mobCountLbl.Location = new System.Drawing.Point(47, 67);
            this.mobCountLbl.Name = "mobCountLbl";
            this.mobCountLbl.Size = new System.Drawing.Size(82, 13);
            this.mobCountLbl.TabIndex = 1;
            this.mobCountLbl.Text = "Monster Count :";
            // 
            // playerCountLbl
            // 
            this.playerCountLbl.AutoSize = true;
            this.playerCountLbl.Location = new System.Drawing.Point(56, 49);
            this.playerCountLbl.Name = "playerCountLbl";
            this.playerCountLbl.Size = new System.Drawing.Size(73, 13);
            this.playerCountLbl.TabIndex = 0;
            this.playerCountLbl.Text = "Player Count :";
            // 
            // consoleTab
            // 
            this.consoleTab.Controls.Add(this.notifyBox);
            this.consoleTab.Controls.Add(this.managerConsole);
            this.consoleTab.Location = new System.Drawing.Point(4, 22);
            this.consoleTab.Name = "consoleTab";
            this.consoleTab.Padding = new System.Windows.Forms.Padding(3);
            this.consoleTab.Size = new System.Drawing.Size(485, 305);
            this.consoleTab.TabIndex = 1;
            this.consoleTab.Text = "Server Logs (Console)";
            this.consoleTab.UseVisualStyleBackColor = true;
            // 
            // notifyBox
            // 
            this.notifyBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notifyBox.Location = new System.Drawing.Point(5, 282);
            this.notifyBox.Name = "notifyBox";
            this.notifyBox.Size = new System.Drawing.Size(474, 20);
            this.notifyBox.TabIndex = 1;
            this.notifyBox.TextChanged += new System.EventHandler(this.notifyBox_TextChanged);
            this.notifyBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.notifyBox_KeyUp);
            // 
            // managerConsole
            // 
            this.managerConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.managerConsole.BackColor = System.Drawing.Color.Black;
            this.managerConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.managerConsole.ForeColor = System.Drawing.Color.White;
            this.managerConsole.Location = new System.Drawing.Point(6, 6);
            this.managerConsole.Multiline = true;
            this.managerConsole.Name = "managerConsole";
            this.managerConsole.Size = new System.Drawing.Size(473, 270);
            this.managerConsole.TabIndex = 0;
            // 
            // chatLogTab
            // 
            this.chatLogTab.Controls.Add(this.chatConsole);
            this.chatLogTab.Location = new System.Drawing.Point(4, 22);
            this.chatLogTab.Name = "chatLogTab";
            this.chatLogTab.Padding = new System.Windows.Forms.Padding(3);
            this.chatLogTab.Size = new System.Drawing.Size(485, 305);
            this.chatLogTab.TabIndex = 2;
            this.chatLogTab.Text = "Chat Logs";
            this.chatLogTab.UseVisualStyleBackColor = true;
            // 
            // chatConsole
            // 
            this.chatConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatConsole.Location = new System.Drawing.Point(6, 6);
            this.chatConsole.Name = "chatConsole";
            this.chatConsole.Size = new System.Drawing.Size(473, 273);
            this.chatConsole.TabIndex = 0;
            this.chatConsole.Text = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.playerOnlineList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(485, 305);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Players Online";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // playerOnlineList
            // 
            this.playerOnlineList.AllowUserToAddRows = false;
            this.playerOnlineList.AllowUserToDeleteRows = false;
            this.playerOnlineList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playerOnlineList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.playerName,
            this.playerLevel,
            this.playerGuild,
            this.playerMap,
            this.playerLocation,
            this.accountGold,
            this.accountCredit});
            this.playerOnlineList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerOnlineList.Location = new System.Drawing.Point(3, 3);
            this.playerOnlineList.Name = "playerOnlineList";
            this.playerOnlineList.ReadOnly = true;
            this.playerOnlineList.Size = new System.Drawing.Size(479, 299);
            this.playerOnlineList.TabIndex = 0;
            // 
            // playerName
            // 
            this.playerName.HeaderText = "Name";
            this.playerName.Name = "playerName";
            this.playerName.ReadOnly = true;
            // 
            // playerLevel
            // 
            this.playerLevel.HeaderText = "Level";
            this.playerLevel.Name = "playerLevel";
            this.playerLevel.ReadOnly = true;
            // 
            // playerGuild
            // 
            this.playerGuild.HeaderText = "Guild";
            this.playerGuild.Name = "playerGuild";
            this.playerGuild.ReadOnly = true;
            // 
            // playerMap
            // 
            this.playerMap.HeaderText = "Map";
            this.playerMap.Name = "playerMap";
            this.playerMap.ReadOnly = true;
            // 
            // playerLocation
            // 
            this.playerLocation.HeaderText = "Location";
            this.playerLocation.Name = "playerLocation";
            this.playerLocation.ReadOnly = true;
            // 
            // accountGold
            // 
            this.accountGold.HeaderText = "Gold";
            this.accountGold.Name = "accountGold";
            this.accountGold.ReadOnly = true;
            // 
            // accountCredit
            // 
            this.accountCredit.HeaderText = "Credits";
            this.accountCredit.Name = "accountCredit";
            this.accountCredit.ReadOnly = true;
            // 
            // configBox
            // 
            this.configBox.Controls.Add(this.passwordBox);
            this.configBox.Controls.Add(this.usernameBox);
            this.configBox.Controls.Add(this.label3);
            this.configBox.Controls.Add(this.label4);
            this.configBox.Controls.Add(this.portBox);
            this.configBox.Controls.Add(this.ipBox);
            this.configBox.Controls.Add(this.label2);
            this.configBox.Controls.Add(this.label1);
            this.configBox.Controls.Add(this.connectionBtn);
            this.configBox.Location = new System.Drawing.Point(12, 12);
            this.configBox.Name = "configBox";
            this.configBox.Size = new System.Drawing.Size(493, 89);
            this.configBox.TabIndex = 1;
            this.configBox.TabStop = false;
            this.configBox.Text = "Configuration";
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(272, 53);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(100, 20);
            this.passwordBox.TabIndex = 8;
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(272, 19);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(100, 20);
            this.usernameBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(202, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "User Name :";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(76, 53);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(100, 20);
            this.portBox.TabIndex = 4;
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(76, 19);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(100, 20);
            this.ipBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Address :";
            // 
            // connectionBtn
            // 
            this.connectionBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.connectionBtn.Location = new System.Drawing.Point(412, 19);
            this.connectionBtn.Name = "connectionBtn";
            this.connectionBtn.Size = new System.Drawing.Size(75, 54);
            this.connectionBtn.TabIndex = 0;
            this.connectionBtn.Text = "Connect";
            this.connectionBtn.UseVisualStyleBackColor = true;
            this.connectionBtn.Click += new System.EventHandler(this.connectionBtn_Click);
            // 
            // ServerManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 450);
            this.Controls.Add(this.configBox);
            this.Controls.Add(this.managerTabControl);
            this.Name = "ServerManagerForm";
            this.Text = "Server Management Tool";
            this.managerTabControl.ResumeLayout(false);
            this.statsTab.ResumeLayout(false);
            this.statsTab.PerformLayout();
            this.consoleTab.ResumeLayout(false);
            this.consoleTab.PerformLayout();
            this.chatLogTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.playerOnlineList)).EndInit();
            this.configBox.ResumeLayout(false);
            this.configBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl managerTabControl;
        private System.Windows.Forms.TabPage statsTab;
        private System.Windows.Forms.TabPage consoleTab;
        private System.Windows.Forms.GroupBox configBox;
        public System.Windows.Forms.Button connectionBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox portBox;
        public System.Windows.Forms.TextBox managerConsole;
        public System.Windows.Forms.TextBox notifyBox;
        public System.Windows.Forms.Label playerCountLbl;
        public System.Windows.Forms.Label mobCountLbl;
        public System.Windows.Forms.Label uptimeLbl;
        public System.Windows.Forms.Label ramLbl;
        public System.Windows.Forms.Label cycleLbl;
        public System.Windows.Forms.Label cpuLbl;
        private System.Windows.Forms.TabPage chatLogTab;
        private System.Windows.Forms.RichTextBox chatConsole;
        public System.Windows.Forms.Label serverStatus;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView playerOnlineList;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerGuild;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerMap;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountGold;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountCredit;
    }
}

