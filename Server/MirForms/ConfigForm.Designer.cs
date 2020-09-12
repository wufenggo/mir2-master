namespace Server
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.configTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.DBVersionLabel = new System.Windows.Forms.Label();
            this.ServerVersionLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.RelogDelayTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.VersionCheckBox = new System.Windows.Forms.CheckBox();
            this.VPathBrowseButton = new System.Windows.Forms.Button();
            this.VPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.MaxUserTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TimeOutTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IPAddressTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Resolution_textbox = new System.Windows.Forms.TextBox();
            this.AllowArcherCheckBox = new System.Windows.Forms.CheckBox();
            this.AllowAssassinCheckBox = new System.Windows.Forms.CheckBox();
            this.StartGameCheckBox = new System.Windows.Forms.CheckBox();
            this.DCharacterCheckBox = new System.Windows.Forms.CheckBox();
            this.NCharacterCheckBox = new System.Windows.Forms.CheckBox();
            this.LoginCheckBox = new System.Windows.Forms.CheckBox();
            this.PasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.AccountCheckBox = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.SaveDelayTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.QuestGoldBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.PotPerTickBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.NewbieExpBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.NewbieLevelBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.NewbieNameBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.QuestDropBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.QuestExpBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.StartGoldBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.StartLvlBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ShowGMEffectBox = new System.Windows.Forms.CheckBox();
            this.SafeZoneHealingCheckBox = new System.Windows.Forms.CheckBox();
            this.SafeZoneBorderCheckBox = new System.Windows.Forms.CheckBox();
            this.VPathDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusportBox = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.configTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(352, 344);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // configTabs
            // 
            this.configTabs.Controls.Add(this.tabPage1);
            this.configTabs.Controls.Add(this.tabPage2);
            this.configTabs.Controls.Add(this.tabPage3);
            this.configTabs.Controls.Add(this.tabPage4);
            this.configTabs.Controls.Add(this.tabPage5);
            this.configTabs.Location = new System.Drawing.Point(12, 12);
            this.configTabs.Name = "configTabs";
            this.configTabs.SelectedIndex = 0;
            this.configTabs.Size = new System.Drawing.Size(415, 326);
            this.configTabs.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.RelogDelayTextBox);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.VersionCheckBox);
            this.tabPage1.Controls.Add(this.VPathBrowseButton);
            this.tabPage1.Controls.Add(this.VPathTextBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(407, 300);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.DBVersionLabel);
            this.groupBox1.Controls.Add(this.ServerVersionLabel);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(6, 230);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(395, 64);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Version Info";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Database";
            // 
            // DBVersionLabel
            // 
            this.DBVersionLabel.AutoSize = true;
            this.DBVersionLabel.Location = new System.Drawing.Point(76, 42);
            this.DBVersionLabel.Name = "DBVersionLabel";
            this.DBVersionLabel.Size = new System.Drawing.Size(42, 13);
            this.DBVersionLabel.TabIndex = 24;
            this.DBVersionLabel.Text = "Version";
            // 
            // ServerVersionLabel
            // 
            this.ServerVersionLabel.AutoSize = true;
            this.ServerVersionLabel.Location = new System.Drawing.Point(76, 19);
            this.ServerVersionLabel.Name = "ServerVersionLabel";
            this.ServerVersionLabel.Size = new System.Drawing.Size(42, 13);
            this.ServerVersionLabel.TabIndex = 7;
            this.ServerVersionLabel.Text = "Version";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Server";
            // 
            // RelogDelayTextBox
            // 
            this.RelogDelayTextBox.Location = new System.Drawing.Point(89, 65);
            this.RelogDelayTextBox.MaxLength = 5;
            this.RelogDelayTextBox.Name = "RelogDelayTextBox";
            this.RelogDelayTextBox.Size = new System.Drawing.Size(93, 20);
            this.RelogDelayTextBox.TabIndex = 21;
            this.RelogDelayTextBox.TextChanged += new System.EventHandler(this.CheckUShort);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Relog Delay:";
            // 
            // VersionCheckBox
            // 
            this.VersionCheckBox.AutoSize = true;
            this.VersionCheckBox.Location = new System.Drawing.Point(89, 42);
            this.VersionCheckBox.Name = "VersionCheckBox";
            this.VersionCheckBox.Size = new System.Drawing.Size(95, 17);
            this.VersionCheckBox.TabIndex = 3;
            this.VersionCheckBox.Text = "Check Version";
            this.VersionCheckBox.UseVisualStyleBackColor = true;
            // 
            // VPathBrowseButton
            // 
            this.VPathBrowseButton.Location = new System.Drawing.Point(373, 14);
            this.VPathBrowseButton.Name = "VPathBrowseButton";
            this.VPathBrowseButton.Size = new System.Drawing.Size(28, 23);
            this.VPathBrowseButton.TabIndex = 2;
            this.VPathBrowseButton.Text = "...";
            this.VPathBrowseButton.UseVisualStyleBackColor = true;
            this.VPathBrowseButton.Click += new System.EventHandler(this.VPathBrowseButton_Click);
            // 
            // VPathTextBox
            // 
            this.VPathTextBox.Location = new System.Drawing.Point(89, 16);
            this.VPathTextBox.Name = "VPathTextBox";
            this.VPathTextBox.ReadOnly = true;
            this.VPathTextBox.Size = new System.Drawing.Size(278, 20);
            this.VPathTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Version Path:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.statusportBox);
            this.tabPage2.Controls.Add(this.label21);
            this.tabPage2.Controls.Add(this.MaxUserTextBox);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.TimeOutTextBox);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.PortTextBox);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.IPAddressTextBox);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage2.Size = new System.Drawing.Size(407, 300);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Network";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MaxUserTextBox
            // 
            this.MaxUserTextBox.Location = new System.Drawing.Point(89, 94);
            this.MaxUserTextBox.MaxLength = 5;
            this.MaxUserTextBox.Name = "MaxUserTextBox";
            this.MaxUserTextBox.Size = new System.Drawing.Size(42, 20);
            this.MaxUserTextBox.TabIndex = 17;
            this.MaxUserTextBox.TextChanged += new System.EventHandler(this.CheckUShort);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Max User:";
            // 
            // TimeOutTextBox
            // 
            this.TimeOutTextBox.Location = new System.Drawing.Point(89, 68);
            this.TimeOutTextBox.MaxLength = 5;
            this.TimeOutTextBox.Name = "TimeOutTextBox";
            this.TimeOutTextBox.Size = new System.Drawing.Size(93, 20);
            this.TimeOutTextBox.TabIndex = 15;
            this.TimeOutTextBox.TextChanged += new System.EventHandler(this.CheckUShort);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "TimeOut:";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(89, 42);
            this.PortTextBox.MaxLength = 5;
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(42, 20);
            this.PortTextBox.TabIndex = 13;
            this.PortTextBox.TextChanged += new System.EventHandler(this.CheckUShort);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Port:";
            // 
            // IPAddressTextBox
            // 
            this.IPAddressTextBox.Location = new System.Drawing.Point(89, 16);
            this.IPAddressTextBox.MaxLength = 15;
            this.IPAddressTextBox.Name = "IPAddressTextBox";
            this.IPAddressTextBox.Size = new System.Drawing.Size(93, 20);
            this.IPAddressTextBox.TabIndex = 11;
            this.IPAddressTextBox.TextChanged += new System.EventHandler(this.IPAddressCheck);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "IP Address:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.Resolution_textbox);
            this.tabPage3.Controls.Add(this.AllowArcherCheckBox);
            this.tabPage3.Controls.Add(this.AllowAssassinCheckBox);
            this.tabPage3.Controls.Add(this.StartGameCheckBox);
            this.tabPage3.Controls.Add(this.DCharacterCheckBox);
            this.tabPage3.Controls.Add(this.NCharacterCheckBox);
            this.tabPage3.Controls.Add(this.LoginCheckBox);
            this.tabPage3.Controls.Add(this.PasswordCheckBox);
            this.tabPage3.Controls.Add(this.AccountCheckBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage3.Size = new System.Drawing.Size(407, 300);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Permissions";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 233);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Max Resolution Allowed";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "label8";
            // 
            // Resolution_textbox
            // 
            this.Resolution_textbox.Location = new System.Drawing.Point(147, 230);
            this.Resolution_textbox.Name = "Resolution_textbox";
            this.Resolution_textbox.Size = new System.Drawing.Size(80, 20);
            this.Resolution_textbox.TabIndex = 14;
            this.Resolution_textbox.TextChanged += new System.EventHandler(this.Resolution_textbox_TextChanged);
            // 
            // AllowArcherCheckBox
            // 
            this.AllowArcherCheckBox.AutoSize = true;
            this.AllowArcherCheckBox.Location = new System.Drawing.Point(24, 197);
            this.AllowArcherCheckBox.Name = "AllowArcherCheckBox";
            this.AllowArcherCheckBox.Size = new System.Drawing.Size(119, 17);
            this.AllowArcherCheckBox.TabIndex = 13;
            this.AllowArcherCheckBox.Text = "Allow Create Archer";
            this.AllowArcherCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllowAssassinCheckBox
            // 
            this.AllowAssassinCheckBox.AutoSize = true;
            this.AllowAssassinCheckBox.Location = new System.Drawing.Point(24, 173);
            this.AllowAssassinCheckBox.Name = "AllowAssassinCheckBox";
            this.AllowAssassinCheckBox.Size = new System.Drawing.Size(129, 17);
            this.AllowAssassinCheckBox.TabIndex = 12;
            this.AllowAssassinCheckBox.Text = "Allow Create Assassin";
            this.AllowAssassinCheckBox.UseVisualStyleBackColor = true;
            // 
            // StartGameCheckBox
            // 
            this.StartGameCheckBox.AutoSize = true;
            this.StartGameCheckBox.Location = new System.Drawing.Point(24, 135);
            this.StartGameCheckBox.Name = "StartGameCheckBox";
            this.StartGameCheckBox.Size = new System.Drawing.Size(107, 17);
            this.StartGameCheckBox.TabIndex = 11;
            this.StartGameCheckBox.Text = "Allow Start Game";
            this.StartGameCheckBox.UseVisualStyleBackColor = true;
            // 
            // DCharacterCheckBox
            // 
            this.DCharacterCheckBox.AutoSize = true;
            this.DCharacterCheckBox.Location = new System.Drawing.Point(24, 112);
            this.DCharacterCheckBox.Name = "DCharacterCheckBox";
            this.DCharacterCheckBox.Size = new System.Drawing.Size(134, 17);
            this.DCharacterCheckBox.TabIndex = 10;
            this.DCharacterCheckBox.Text = "Allow Delete Character";
            this.DCharacterCheckBox.UseVisualStyleBackColor = true;
            // 
            // NCharacterCheckBox
            // 
            this.NCharacterCheckBox.AutoSize = true;
            this.NCharacterCheckBox.Location = new System.Drawing.Point(24, 89);
            this.NCharacterCheckBox.Name = "NCharacterCheckBox";
            this.NCharacterCheckBox.Size = new System.Drawing.Size(125, 17);
            this.NCharacterCheckBox.TabIndex = 9;
            this.NCharacterCheckBox.Text = "Allow New Character";
            this.NCharacterCheckBox.UseVisualStyleBackColor = true;
            // 
            // LoginCheckBox
            // 
            this.LoginCheckBox.AutoSize = true;
            this.LoginCheckBox.Location = new System.Drawing.Point(24, 66);
            this.LoginCheckBox.Name = "LoginCheckBox";
            this.LoginCheckBox.Size = new System.Drawing.Size(80, 17);
            this.LoginCheckBox.TabIndex = 8;
            this.LoginCheckBox.Text = "Allow Login";
            this.LoginCheckBox.UseVisualStyleBackColor = true;
            // 
            // PasswordCheckBox
            // 
            this.PasswordCheckBox.AutoSize = true;
            this.PasswordCheckBox.Location = new System.Drawing.Point(24, 43);
            this.PasswordCheckBox.Name = "PasswordCheckBox";
            this.PasswordCheckBox.Size = new System.Drawing.Size(140, 17);
            this.PasswordCheckBox.TabIndex = 7;
            this.PasswordCheckBox.Text = "Allow Change Password";
            this.PasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // AccountCheckBox
            // 
            this.AccountCheckBox.AutoSize = true;
            this.AccountCheckBox.Location = new System.Drawing.Point(24, 20);
            this.AccountCheckBox.Name = "AccountCheckBox";
            this.AccountCheckBox.Size = new System.Drawing.Size(119, 17);
            this.AccountCheckBox.TabIndex = 6;
            this.AccountCheckBox.Text = "Allow New Account";
            this.AccountCheckBox.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.SaveDelayTextBox);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage4.Size = new System.Drawing.Size(407, 300);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Database";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // SaveDelayTextBox
            // 
            this.SaveDelayTextBox.Location = new System.Drawing.Point(89, 16);
            this.SaveDelayTextBox.MaxLength = 5;
            this.SaveDelayTextBox.Name = "SaveDelayTextBox";
            this.SaveDelayTextBox.Size = new System.Drawing.Size(93, 20);
            this.SaveDelayTextBox.TabIndex = 25;
            this.SaveDelayTextBox.TextChanged += new System.EventHandler(this.CheckUShort);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Save Delay:";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.QuestGoldBox);
            this.tabPage5.Controls.Add(this.label20);
            this.tabPage5.Controls.Add(this.PotPerTickBox);
            this.tabPage5.Controls.Add(this.label19);
            this.tabPage5.Controls.Add(this.NewbieExpBox);
            this.tabPage5.Controls.Add(this.label18);
            this.tabPage5.Controls.Add(this.NewbieLevelBox);
            this.tabPage5.Controls.Add(this.label16);
            this.tabPage5.Controls.Add(this.NewbieNameBox);
            this.tabPage5.Controls.Add(this.label17);
            this.tabPage5.Controls.Add(this.QuestDropBox);
            this.tabPage5.Controls.Add(this.label14);
            this.tabPage5.Controls.Add(this.QuestExpBox);
            this.tabPage5.Controls.Add(this.label15);
            this.tabPage5.Controls.Add(this.StartGoldBox);
            this.tabPage5.Controls.Add(this.label13);
            this.tabPage5.Controls.Add(this.StartLvlBox);
            this.tabPage5.Controls.Add(this.label12);
            this.tabPage5.Controls.Add(this.ShowGMEffectBox);
            this.tabPage5.Controls.Add(this.SafeZoneHealingCheckBox);
            this.tabPage5.Controls.Add(this.SafeZoneBorderCheckBox);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage5.Size = new System.Drawing.Size(407, 300);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Optional";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // QuestGoldBox
            // 
            this.QuestGoldBox.Location = new System.Drawing.Point(83, 214);
            this.QuestGoldBox.MaxLength = 5;
            this.QuestGoldBox.Name = "QuestGoldBox";
            this.QuestGoldBox.Size = new System.Drawing.Size(42, 20);
            this.QuestGoldBox.TabIndex = 35;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(22, 216);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(63, 13);
            this.label20.TabIndex = 34;
            this.label20.Text = "Quest Gold:";
            // 
            // PotPerTickBox
            // 
            this.PotPerTickBox.Location = new System.Drawing.Point(360, 278);
            this.PotPerTickBox.MaxLength = 5;
            this.PotPerTickBox.Name = "PotPerTickBox";
            this.PotPerTickBox.Size = new System.Drawing.Size(42, 20);
            this.PotPerTickBox.TabIndex = 33;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(287, 280);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 13);
            this.label19.TabIndex = 32;
            this.label19.Text = "Pot Per Tick:";
            // 
            // NewbieExpBox
            // 
            this.NewbieExpBox.Location = new System.Drawing.Point(104, 159);
            this.NewbieExpBox.MaxLength = 5;
            this.NewbieExpBox.Name = "NewbieExpBox";
            this.NewbieExpBox.Size = new System.Drawing.Size(42, 20);
            this.NewbieExpBox.TabIndex = 31;
            this.NewbieExpBox.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(22, 162);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(83, 13);
            this.label18.TabIndex = 30;
            this.label18.Text = "NewbieExpBuff:";
            // 
            // NewbieLevelBox
            // 
            this.NewbieLevelBox.Location = new System.Drawing.Point(104, 131);
            this.NewbieLevelBox.MaxLength = 5;
            this.NewbieLevelBox.Name = "NewbieLevelBox";
            this.NewbieLevelBox.Size = new System.Drawing.Size(42, 20);
            this.NewbieLevelBox.TabIndex = 29;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(22, 131);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(80, 13);
            this.label16.TabIndex = 28;
            this.label16.Text = "NewbieMaxLvl:";
            // 
            // NewbieNameBox
            // 
            this.NewbieNameBox.Location = new System.Drawing.Point(96, 97);
            this.NewbieNameBox.MaxLength = 12;
            this.NewbieNameBox.Name = "NewbieNameBox";
            this.NewbieNameBox.Size = new System.Drawing.Size(82, 20);
            this.NewbieNameBox.TabIndex = 27;
            this.NewbieNameBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(22, 99);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 13);
            this.label17.TabIndex = 26;
            this.label17.Text = "NewbieName:";
            // 
            // QuestDropBox
            // 
            this.QuestDropBox.Location = new System.Drawing.Point(83, 276);
            this.QuestDropBox.MaxLength = 5;
            this.QuestDropBox.Name = "QuestDropBox";
            this.QuestDropBox.Size = new System.Drawing.Size(42, 20);
            this.QuestDropBox.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(22, 279);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 13);
            this.label14.TabIndex = 24;
            this.label14.Text = "Quest Drop:";
            // 
            // QuestExpBox
            // 
            this.QuestExpBox.Location = new System.Drawing.Point(83, 245);
            this.QuestExpBox.MaxLength = 5;
            this.QuestExpBox.Name = "QuestExpBox";
            this.QuestExpBox.Size = new System.Drawing.Size(42, 20);
            this.QuestExpBox.TabIndex = 23;
            this.QuestExpBox.TextChanged += new System.EventHandler(this.QuestExpBox_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(22, 247);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "Quest Exp:";
            // 
            // StartGoldBox
            // 
            this.StartGoldBox.Location = new System.Drawing.Point(349, 41);
            this.StartGoldBox.MaxLength = 5;
            this.StartGoldBox.Name = "StartGoldBox";
            this.StartGoldBox.Size = new System.Drawing.Size(42, 20);
            this.StartGoldBox.TabIndex = 21;
            this.StartGoldBox.TextChanged += new System.EventHandler(this.StartGoldBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(287, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 20;
            this.label13.Text = "Start Gold:";
            // 
            // StartLvlBox
            // 
            this.StartLvlBox.Location = new System.Drawing.Point(349, 9);
            this.StartLvlBox.MaxLength = 5;
            this.StartLvlBox.Name = "StartLvlBox";
            this.StartLvlBox.Size = new System.Drawing.Size(42, 20);
            this.StartLvlBox.TabIndex = 19;
            this.StartLvlBox.TextChanged += new System.EventHandler(this.StartLvlBox_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(287, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "Start Lvl:";
            // 
            // ShowGMEffectBox
            // 
            this.ShowGMEffectBox.AutoSize = true;
            this.ShowGMEffectBox.Location = new System.Drawing.Point(24, 67);
            this.ShowGMEffectBox.Name = "ShowGMEffectBox";
            this.ShowGMEffectBox.Size = new System.Drawing.Size(104, 17);
            this.ShowGMEffectBox.TabIndex = 2;
            this.ShowGMEffectBox.Text = "Show GM Effect";
            this.ShowGMEffectBox.UseVisualStyleBackColor = true;
            this.ShowGMEffectBox.CheckedChanged += new System.EventHandler(this.ShowGMEffectBox_CheckedChanged);
            // 
            // SafeZoneHealingCheckBox
            // 
            this.SafeZoneHealingCheckBox.AutoSize = true;
            this.SafeZoneHealingCheckBox.Location = new System.Drawing.Point(24, 43);
            this.SafeZoneHealingCheckBox.Name = "SafeZoneHealingCheckBox";
            this.SafeZoneHealingCheckBox.Size = new System.Drawing.Size(112, 17);
            this.SafeZoneHealingCheckBox.TabIndex = 1;
            this.SafeZoneHealingCheckBox.Text = "SafeZone Healing";
            this.SafeZoneHealingCheckBox.UseVisualStyleBackColor = true;
            this.SafeZoneHealingCheckBox.CheckedChanged += new System.EventHandler(this.SafeZoneHealingCheckBox_CheckedChanged);
            // 
            // SafeZoneBorderCheckBox
            // 
            this.SafeZoneBorderCheckBox.AutoSize = true;
            this.SafeZoneBorderCheckBox.Location = new System.Drawing.Point(24, 20);
            this.SafeZoneBorderCheckBox.Name = "SafeZoneBorderCheckBox";
            this.SafeZoneBorderCheckBox.Size = new System.Drawing.Size(107, 17);
            this.SafeZoneBorderCheckBox.TabIndex = 0;
            this.SafeZoneBorderCheckBox.Text = "SafeZone Border";
            this.SafeZoneBorderCheckBox.UseVisualStyleBackColor = true;
            this.SafeZoneBorderCheckBox.CheckedChanged += new System.EventHandler(this.SafeZoneBorderCheckBox_CheckedChanged);
            // 
            // VPathDialog
            // 
            this.VPathDialog.FileName = "Mir2.Exe";
            this.VPathDialog.Filter = "Executable Files (*.exe)|*.exe";
            // 
            // statusportBox
            // 
            this.statusportBox.Location = new System.Drawing.Point(89, 120);
            this.statusportBox.MaxLength = 5;
            this.statusportBox.Name = "statusportBox";
            this.statusportBox.Size = new System.Drawing.Size(42, 20);
            this.statusportBox.TabIndex = 19;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(21, 123);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(62, 13);
            this.label21.TabIndex = 18;
            this.label21.Text = "Status Port:";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 373);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.configTabs);
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm_FormClosed);
            this.configTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TabControl configTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox RelogDelayTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox VersionCheckBox;
        private System.Windows.Forms.Button VPathBrowseButton;
        private System.Windows.Forms.TextBox VPathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox MaxUserTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TimeOutTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox IPAddressTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog VPathDialog;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox StartGameCheckBox;
        private System.Windows.Forms.CheckBox DCharacterCheckBox;
        private System.Windows.Forms.CheckBox NCharacterCheckBox;
        private System.Windows.Forms.CheckBox LoginCheckBox;
        private System.Windows.Forms.CheckBox PasswordCheckBox;
        private System.Windows.Forms.CheckBox AccountCheckBox;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox SaveDelayTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.CheckBox SafeZoneBorderCheckBox;
        private System.Windows.Forms.CheckBox SafeZoneHealingCheckBox;
        private System.Windows.Forms.CheckBox AllowArcherCheckBox;
        private System.Windows.Forms.CheckBox AllowAssassinCheckBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Resolution_textbox;
        private System.Windows.Forms.Label ServerVersionLabel;
        private System.Windows.Forms.Label DBVersionLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ShowGMEffectBox;
        private System.Windows.Forms.TextBox StartGoldBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox StartLvlBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox QuestDropBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox QuestExpBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox NewbieExpBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox NewbieLevelBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox NewbieNameBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox PotPerTickBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox QuestGoldBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox statusportBox;
        private System.Windows.Forms.Label label21;
    }
}