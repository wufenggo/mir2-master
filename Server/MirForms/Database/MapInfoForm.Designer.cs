﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Server
{
    partial class MapInfoForm
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
            this.MapTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.MusicTextBox = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.MineComboBox = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.BigMapTextBox = new System.Windows.Forms.TextBox();
            this.LightsComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MapIndexTextBox = new System.Windows.Forms.TextBox();
            this.MiniMapTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MapNameTextBox = new System.Windows.Forms.TextBox();
            this.FileNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.noRes = new System.Windows.Forms.CheckBox();
            this.noGroup = new System.Windows.Forms.CheckBox();
            this.SafeZoneCheckbox = new System.Windows.Forms.CheckBox();
            this.GTBox = new System.Windows.Forms.CheckBox();
            this.NoHeroCheckBox = new System.Windows.Forms.CheckBox();
            this.NoFightCheckbox = new System.Windows.Forms.CheckBox();
            this.NeedBridleCheckbox = new System.Windows.Forms.CheckBox();
            this.NoMountCheckbox = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.MapDarkLighttextBox = new System.Windows.Forms.TextBox();
            this.NoNamesCheckbox = new System.Windows.Forms.CheckBox();
            this.NoDropMonsterCheckbox = new System.Windows.Forms.CheckBox();
            this.NoDropPlayerCheckbox = new System.Windows.Forms.CheckBox();
            this.NoThrowItemCheckbox = new System.Windows.Forms.CheckBox();
            this.NoPositionCheckbox = new System.Windows.Forms.CheckBox();
            this.NoDrugCheckbox = new System.Windows.Forms.CheckBox();
            this.NoRecallCheckbox = new System.Windows.Forms.CheckBox();
            this.NoEscapeCheckbox = new System.Windows.Forms.CheckBox();
            this.NoRandomCheckbox = new System.Windows.Forms.CheckBox();
            this.LightningTextbox = new System.Windows.Forms.TextBox();
            this.FireTextbox = new System.Windows.Forms.TextBox();
            this.NoReconnectTextbox = new System.Windows.Forms.TextBox();
            this.LightningCheckbox = new System.Windows.Forms.CheckBox();
            this.FireCheckbox = new System.Windows.Forms.CheckBox();
            this.FightCheckbox = new System.Windows.Forms.CheckBox();
            this.NoReconnectCheckbox = new System.Windows.Forms.CheckBox();
            this.NoTeleportCheckbox = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.RemoveSZButton = new System.Windows.Forms.Button();
            this.AddSZButton = new System.Windows.Forms.Button();
            this.SafeZoneInfoPanel = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.SZYTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.SizeTextBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.SZXTextBox = new System.Windows.Forms.TextBox();
            this.StartPointCheckBox = new System.Windows.Forms.CheckBox();
            this.SafeZoneInfoListBox = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.RPasteButton = new System.Windows.Forms.Button();
            this.RCopyButton = new System.Windows.Forms.Button();
            this.RemoveRButton = new System.Windows.Forms.Button();
            this.AddRButton = new System.Windows.Forms.Button();
            this.RespawnInfoListBox = new System.Windows.Forms.ListBox();
            this.RespawnInfoPanel = new System.Windows.Forms.Panel();
            this.chkrespawnsave = new System.Windows.Forms.CheckBox();
            this.chkRespawnEnableTick = new System.Windows.Forms.CheckBox();
            this.Randomtextbox = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.RoutePathTextBox = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.DirectionTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DelayTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MonsterInfoComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SpreadTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.RYTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.CountTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.RXTextBox = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.RemoveMButton = new System.Windows.Forms.Button();
            this.AddMButton = new System.Windows.Forms.Button();
            this.MovementInfoPanel = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.ConquestComboBox = new System.Windows.Forms.ComboBox();
            this.NeedMoveMCheckBox = new System.Windows.Forms.CheckBox();
            this.NeedHoleMCheckBox = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.DestMapComboBox = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.DestYTextBox = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.DestXTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.SourceYTextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.SourceXTextBox = new System.Windows.Forms.TextBox();
            this.MovementInfoListBox = new System.Windows.Forms.ListBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.MZDeletebutton = new System.Windows.Forms.Button();
            this.MZAddbutton = new System.Windows.Forms.Button();
            this.MineZonepanel = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.MineZoneComboBox = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.MZYtextBox = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.MZSizetextBox = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.MZXtextBox = new System.Windows.Forms.TextBox();
            this.MZListlistBox = new System.Windows.Forms.ListBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panelEventRespawn = new System.Windows.Forms.Panel();
            this.txtSpreadX = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.chkIsRespawnObjective = new System.Windows.Forms.CheckBox();
            this.txtSpreadY = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtEventRespawnMonCount = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.txtEventRespawnMonName = new System.Windows.Forms.TextBox();
            this.BtnRemoveEventRespawn = new System.Windows.Forms.Button();
            this.btnAddEventRespawn = new System.Windows.Forms.Button();
            this.lstEventRespawns = new System.Windows.Forms.ListBox();
            this.panelEventInfo = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.txtObjectiveMsg = new System.Windows.Forms.TextBox();
            this.chkIsSafeZone = new System.Windows.Forms.CheckBox();
            this.ddlEventType = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.txtCooldownInMins = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.txtEventSize = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtEventName = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtEventMultipleCoords = new System.Windows.Forms.TextBox();
            this.btnRemoveEvent = new System.Windows.Forms.Button();
            this.btnAddEvent = new System.Windows.Forms.Button();
            this.lstEventInfos = new System.Windows.Forms.ListBox();
            this.lms_BRTab = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.removeLMSBtn = new System.Windows.Forms.Button();
            this.addLMSBtn = new System.Windows.Forms.Button();
            this.lmsListBox = new System.Windows.Forms.ListBox();
            this.lmsPanel = new System.Windows.Forms.Panel();
            this.rewardPanel = new System.Windows.Forms.Panel();
            this.lmsRewardRankBox = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.lmsRewardRateBox = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.lmsRewardClassCbox = new System.Windows.Forms.ComboBox();
            this.lmsRewardGroupTextBox = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.lmsRewardClassSpecific = new System.Windows.Forms.CheckBox();
            this.lmsRewardAddBtn = new System.Windows.Forms.Button();
            this.lmsRewardRemoveBtn = new System.Windows.Forms.Button();
            this.lmsRewardLBox = new System.Windows.Forms.ListBox();
            this.rewardDialog = new System.Windows.Forms.Button();
            this.label46 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.lmsDay = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.lmsRange = new System.Windows.Forms.TextBox();
            this.lmsHour = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.lmsMinute = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.lmsDuration = new System.Windows.Forms.TextBox();
            this.lmsMax = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.lmsMin = new System.Windows.Forms.TextBox();
            this.lmsX = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.lmsY = new System.Windows.Forms.TextBox();
            this.lmsSize = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.MapInfoListBox = new System.Windows.Forms.ListBox();
            this.PasteMapButton = new System.Windows.Forms.Button();
            this.CopyMapButton = new System.Windows.Forms.Button();
            this.ImportMapInfoButton = new System.Windows.Forms.Button();
            this.ExportMapInfoButton = new System.Windows.Forms.Button();
            this.ImportMongenButton = new System.Windows.Forms.Button();
            this.ExportMongenButton = new System.Windows.Forms.Button();
            this.VisualizerButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.itemSelect = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.selectBtn = new System.Windows.Forms.Button();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.amountBox = new System.Windows.Forms.TextBox();
            this.itemsCBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.MapTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SafeZoneInfoPanel.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.RespawnInfoPanel.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.MovementInfoPanel.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.MineZonepanel.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panelEventRespawn.SuspendLayout();
            this.panelEventInfo.SuspendLayout();
            this.lms_BRTab.SuspendLayout();
            this.panel2.SuspendLayout();
            this.lmsPanel.SuspendLayout();
            this.rewardPanel.SuspendLayout();
            this.itemSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // MapTabs
            // 
            this.MapTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MapTabs.Controls.Add(this.tabPage1);
            this.MapTabs.Controls.Add(this.tabPage6);
            this.MapTabs.Controls.Add(this.tabPage3);
            this.MapTabs.Controls.Add(this.tabPage2);
            this.MapTabs.Controls.Add(this.tabPage4);
            this.MapTabs.Controls.Add(this.tabPage7);
            this.MapTabs.Controls.Add(this.tabPage5);
            this.MapTabs.Controls.Add(this.lms_BRTab);
            this.MapTabs.Location = new System.Drawing.Point(207, 62);
            this.MapTabs.Name = "MapTabs";
            this.MapTabs.SelectedIndex = 0;
            this.MapTabs.Size = new System.Drawing.Size(540, 300);
            this.MapTabs.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.MusicTextBox);
            this.tabPage1.Controls.Add(this.label33);
            this.tabPage1.Controls.Add(this.MineComboBox);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.BigMapTextBox);
            this.tabPage1.Controls.Add(this.LightsComboBox);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.MapIndexTextBox);
            this.tabPage1.Controls.Add(this.MiniMapTextBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.MapNameTextBox);
            this.tabPage1.Controls.Add(this.FileNameTextBox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(532, 274);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(38, 181);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Music:";
            // 
            // MusicTextBox
            // 
            this.MusicTextBox.Location = new System.Drawing.Point(82, 178);
            this.MusicTextBox.Name = "MusicTextBox";
            this.MusicTextBox.Size = new System.Drawing.Size(92, 20);
            this.MusicTextBox.TabIndex = 17;
            this.MusicTextBox.TextChanged += new System.EventHandler(this.MusicTextBox_TextChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(16, 153);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(60, 13);
            this.label33.TabIndex = 16;
            this.label33.Text = "Mine Type:";
            // 
            // MineComboBox
            // 
            this.MineComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MineComboBox.FormattingEnabled = true;
            this.MineComboBox.Location = new System.Drawing.Point(82, 150);
            this.MineComboBox.Name = "MineComboBox";
            this.MineComboBox.Size = new System.Drawing.Size(92, 21);
            this.MineComboBox.TabIndex = 15;
            this.MineComboBox.SelectedIndexChanged += new System.EventHandler(this.MineComboBox_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(129, 99);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "Big Map:";
            // 
            // BigMapTextBox
            // 
            this.BigMapTextBox.Location = new System.Drawing.Point(184, 96);
            this.BigMapTextBox.MaxLength = 5;
            this.BigMapTextBox.Name = "BigMapTextBox";
            this.BigMapTextBox.Size = new System.Drawing.Size(37, 20);
            this.BigMapTextBox.TabIndex = 13;
            this.BigMapTextBox.TextChanged += new System.EventHandler(this.BigMapTextBox_TextChanged);
            // 
            // LightsComboBox
            // 
            this.LightsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LightsComboBox.FormattingEnabled = true;
            this.LightsComboBox.Location = new System.Drawing.Point(82, 122);
            this.LightsComboBox.Name = "LightsComboBox";
            this.LightsComboBox.Size = new System.Drawing.Size(92, 21);
            this.LightsComboBox.TabIndex = 11;
            this.LightsComboBox.SelectedIndexChanged += new System.EventHandler(this.LightsComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Lights:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Map Index:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Mini Map:";
            // 
            // MapIndexTextBox
            // 
            this.MapIndexTextBox.Location = new System.Drawing.Point(82, 15);
            this.MapIndexTextBox.Name = "MapIndexTextBox";
            this.MapIndexTextBox.ReadOnly = true;
            this.MapIndexTextBox.Size = new System.Drawing.Size(47, 20);
            this.MapIndexTextBox.TabIndex = 0;
            // 
            // MiniMapTextBox
            // 
            this.MiniMapTextBox.Location = new System.Drawing.Point(82, 96);
            this.MiniMapTextBox.MaxLength = 5;
            this.MiniMapTextBox.Name = "MiniMapTextBox";
            this.MiniMapTextBox.Size = new System.Drawing.Size(37, 20);
            this.MiniMapTextBox.TabIndex = 9;
            this.MiniMapTextBox.TextChanged += new System.EventHandler(this.MiniMapTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "File Name:";
            // 
            // MapNameTextBox
            // 
            this.MapNameTextBox.Location = new System.Drawing.Point(82, 70);
            this.MapNameTextBox.Name = "MapNameTextBox";
            this.MapNameTextBox.Size = new System.Drawing.Size(92, 20);
            this.MapNameTextBox.TabIndex = 2;
            this.MapNameTextBox.TextChanged += new System.EventHandler(this.MapNameTextBox_TextChanged);
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.Location = new System.Drawing.Point(82, 44);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.Size = new System.Drawing.Size(47, 20);
            this.FileNameTextBox.TabIndex = 1;
            this.FileNameTextBox.TextChanged += new System.EventHandler(this.FileNameTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Map Name:";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.noRes);
            this.tabPage6.Controls.Add(this.noGroup);
            this.tabPage6.Controls.Add(this.SafeZoneCheckbox);
            this.tabPage6.Controls.Add(this.GTBox);
            this.tabPage6.Controls.Add(this.NoHeroCheckBox);
            this.tabPage6.Controls.Add(this.NoFightCheckbox);
            this.tabPage6.Controls.Add(this.NeedBridleCheckbox);
            this.tabPage6.Controls.Add(this.NoMountCheckbox);
            this.tabPage6.Controls.Add(this.label19);
            this.tabPage6.Controls.Add(this.MapDarkLighttextBox);
            this.tabPage6.Controls.Add(this.NoNamesCheckbox);
            this.tabPage6.Controls.Add(this.NoDropMonsterCheckbox);
            this.tabPage6.Controls.Add(this.NoDropPlayerCheckbox);
            this.tabPage6.Controls.Add(this.NoThrowItemCheckbox);
            this.tabPage6.Controls.Add(this.NoPositionCheckbox);
            this.tabPage6.Controls.Add(this.NoDrugCheckbox);
            this.tabPage6.Controls.Add(this.NoRecallCheckbox);
            this.tabPage6.Controls.Add(this.NoEscapeCheckbox);
            this.tabPage6.Controls.Add(this.NoRandomCheckbox);
            this.tabPage6.Controls.Add(this.LightningTextbox);
            this.tabPage6.Controls.Add(this.FireTextbox);
            this.tabPage6.Controls.Add(this.NoReconnectTextbox);
            this.tabPage6.Controls.Add(this.LightningCheckbox);
            this.tabPage6.Controls.Add(this.FireCheckbox);
            this.tabPage6.Controls.Add(this.FightCheckbox);
            this.tabPage6.Controls.Add(this.NoReconnectCheckbox);
            this.tabPage6.Controls.Add(this.NoTeleportCheckbox);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(532, 274);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Attributes";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // noRes
            // 
            this.noRes.AutoSize = true;
            this.noRes.Location = new System.Drawing.Point(341, 113);
            this.noRes.Name = "noRes";
            this.noRes.Size = new System.Drawing.Size(62, 17);
            this.noRes.TabIndex = 48;
            this.noRes.Text = "No Res";
            this.noRes.UseVisualStyleBackColor = true;
            this.noRes.CheckedChanged += new System.EventHandler(this.noRes_CheckedChanged);
            // 
            // noGroup
            // 
            this.noGroup.AutoSize = true;
            this.noGroup.Location = new System.Drawing.Point(174, 207);
            this.noGroup.Name = "noGroup";
            this.noGroup.Size = new System.Drawing.Size(72, 17);
            this.noGroup.TabIndex = 47;
            this.noGroup.Text = "No Group";
            this.noGroup.UseVisualStyleBackColor = true;
            this.noGroup.CheckedChanged += new System.EventHandler(this.noGroup_CheckedChanged);
            // 
            // SafeZoneCheckbox
            // 
            this.SafeZoneCheckbox.AutoSize = true;
            this.SafeZoneCheckbox.Location = new System.Drawing.Point(174, 184);
            this.SafeZoneCheckbox.Name = "SafeZoneCheckbox";
            this.SafeZoneCheckbox.Size = new System.Drawing.Size(73, 17);
            this.SafeZoneCheckbox.TabIndex = 46;
            this.SafeZoneCheckbox.Text = "SafeZone";
            this.SafeZoneCheckbox.UseVisualStyleBackColor = true;
            this.SafeZoneCheckbox.CheckedChanged += new System.EventHandler(this.SafeZoneCheckbox_CheckedChanged);
            // 
            // GTBox
            // 
            this.GTBox.AutoSize = true;
            this.GTBox.Location = new System.Drawing.Point(15, 207);
            this.GTBox.Name = "GTBox";
            this.GTBox.Size = new System.Drawing.Size(41, 17);
            this.GTBox.TabIndex = 45;
            this.GTBox.Text = "GT";
            this.GTBox.UseVisualStyleBackColor = true;
            this.GTBox.CheckedChanged += new System.EventHandler(this.GTBox_CheckedChanged);
            // 
            // NoHeroCheckBox
            // 
            this.NoHeroCheckBox.AutoSize = true;
            this.NoHeroCheckBox.Location = new System.Drawing.Point(16, 184);
            this.NoHeroCheckBox.Name = "NoHeroCheckBox";
            this.NoHeroCheckBox.Size = new System.Drawing.Size(66, 17);
            this.NoHeroCheckBox.TabIndex = 44;
            this.NoHeroCheckBox.Text = "No Hero";
            this.NoHeroCheckBox.UseVisualStyleBackColor = true;
            this.NoHeroCheckBox.CheckedChanged += new System.EventHandler(this.NoHeroCheckBox_CheckedChanged);
            // 
            // NoFightCheckbox
            // 
            this.NoFightCheckbox.AutoSize = true;
            this.NoFightCheckbox.Location = new System.Drawing.Point(174, 137);
            this.NoFightCheckbox.Name = "NoFightCheckbox";
            this.NoFightCheckbox.Size = new System.Drawing.Size(66, 17);
            this.NoFightCheckbox.TabIndex = 43;
            this.NoFightCheckbox.Text = "No Fight";
            this.NoFightCheckbox.UseVisualStyleBackColor = true;
            this.NoFightCheckbox.CheckedChanged += new System.EventHandler(this.NoFightCheckbox_CheckedChanged);
            // 
            // NeedBridleCheckbox
            // 
            this.NeedBridleCheckbox.AutoSize = true;
            this.NeedBridleCheckbox.Location = new System.Drawing.Point(174, 160);
            this.NeedBridleCheckbox.Name = "NeedBridleCheckbox";
            this.NeedBridleCheckbox.Size = new System.Drawing.Size(81, 17);
            this.NeedBridleCheckbox.TabIndex = 42;
            this.NeedBridleCheckbox.Text = "Need Bridle";
            this.NeedBridleCheckbox.UseVisualStyleBackColor = true;
            this.NeedBridleCheckbox.CheckedChanged += new System.EventHandler(this.NeedBridleCheckbox_CheckedChanged);
            // 
            // NoMountCheckbox
            // 
            this.NoMountCheckbox.AutoSize = true;
            this.NoMountCheckbox.Location = new System.Drawing.Point(174, 113);
            this.NoMountCheckbox.Name = "NoMountCheckbox";
            this.NoMountCheckbox.Size = new System.Drawing.Size(73, 17);
            this.NoMountCheckbox.TabIndex = 41;
            this.NoMountCheckbox.Text = "No Mount";
            this.NoMountCheckbox.UseVisualStyleBackColor = true;
            this.NoMountCheckbox.CheckedChanged += new System.EventHandler(this.NoMountCheckbox_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(338, 94);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 13);
            this.label19.TabIndex = 40;
            this.label19.Text = "Map Dark Light";
            // 
            // MapDarkLighttextBox
            // 
            this.MapDarkLighttextBox.Location = new System.Drawing.Point(443, 91);
            this.MapDarkLighttextBox.Name = "MapDarkLighttextBox";
            this.MapDarkLighttextBox.Size = new System.Drawing.Size(49, 20);
            this.MapDarkLighttextBox.TabIndex = 39;
            this.MapDarkLighttextBox.TextChanged += new System.EventHandler(this.MapDarkLighttextBox_TextChanged);
            // 
            // NoNamesCheckbox
            // 
            this.NoNamesCheckbox.AutoSize = true;
            this.NoNamesCheckbox.Location = new System.Drawing.Point(174, 89);
            this.NoNamesCheckbox.Name = "NoNamesCheckbox";
            this.NoNamesCheckbox.Size = new System.Drawing.Size(76, 17);
            this.NoNamesCheckbox.TabIndex = 38;
            this.NoNamesCheckbox.Text = "No Names";
            this.NoNamesCheckbox.UseVisualStyleBackColor = true;
            this.NoNamesCheckbox.CheckedChanged += new System.EventHandler(this.NoNamesCheckbox_CheckedChanged);
            // 
            // NoDropMonsterCheckbox
            // 
            this.NoDropMonsterCheckbox.AutoSize = true;
            this.NoDropMonsterCheckbox.Location = new System.Drawing.Point(174, 65);
            this.NoDropMonsterCheckbox.Name = "NoDropMonsterCheckbox";
            this.NoDropMonsterCheckbox.Size = new System.Drawing.Size(113, 17);
            this.NoDropMonsterCheckbox.TabIndex = 37;
            this.NoDropMonsterCheckbox.Text = "No Drop (Monster)";
            this.NoDropMonsterCheckbox.UseVisualStyleBackColor = true;
            this.NoDropMonsterCheckbox.CheckedChanged += new System.EventHandler(this.NoDropMonsterCheckbox_CheckedChanged);
            // 
            // NoDropPlayerCheckbox
            // 
            this.NoDropPlayerCheckbox.AutoSize = true;
            this.NoDropPlayerCheckbox.Location = new System.Drawing.Point(174, 41);
            this.NoDropPlayerCheckbox.Name = "NoDropPlayerCheckbox";
            this.NoDropPlayerCheckbox.Size = new System.Drawing.Size(104, 17);
            this.NoDropPlayerCheckbox.TabIndex = 36;
            this.NoDropPlayerCheckbox.Text = "No Drop (Player)";
            this.NoDropPlayerCheckbox.UseVisualStyleBackColor = true;
            this.NoDropPlayerCheckbox.CheckedChanged += new System.EventHandler(this.NoDropPlayerCheckbox_CheckedChanged);
            // 
            // NoThrowItemCheckbox
            // 
            this.NoThrowItemCheckbox.AutoSize = true;
            this.NoThrowItemCheckbox.Location = new System.Drawing.Point(174, 17);
            this.NoThrowItemCheckbox.Name = "NoThrowItemCheckbox";
            this.NoThrowItemCheckbox.Size = new System.Drawing.Size(96, 17);
            this.NoThrowItemCheckbox.TabIndex = 35;
            this.NoThrowItemCheckbox.Text = "No Throw Item";
            this.NoThrowItemCheckbox.UseVisualStyleBackColor = true;
            this.NoThrowItemCheckbox.CheckedChanged += new System.EventHandler(this.NoThrowItemCheckbox_CheckedChanged);
            // 
            // NoPositionCheckbox
            // 
            this.NoPositionCheckbox.AutoSize = true;
            this.NoPositionCheckbox.Location = new System.Drawing.Point(15, 160);
            this.NoPositionCheckbox.Name = "NoPositionCheckbox";
            this.NoPositionCheckbox.Size = new System.Drawing.Size(80, 17);
            this.NoPositionCheckbox.TabIndex = 34;
            this.NoPositionCheckbox.Text = "No Position";
            this.NoPositionCheckbox.UseVisualStyleBackColor = true;
            this.NoPositionCheckbox.CheckedChanged += new System.EventHandler(this.NoPositionCheckbox_CheckedChanged);
            // 
            // NoDrugCheckbox
            // 
            this.NoDrugCheckbox.AutoSize = true;
            this.NoDrugCheckbox.Location = new System.Drawing.Point(16, 137);
            this.NoDrugCheckbox.Name = "NoDrugCheckbox";
            this.NoDrugCheckbox.Size = new System.Drawing.Size(66, 17);
            this.NoDrugCheckbox.TabIndex = 33;
            this.NoDrugCheckbox.Text = "No Drug";
            this.NoDrugCheckbox.UseVisualStyleBackColor = true;
            this.NoDrugCheckbox.CheckedChanged += new System.EventHandler(this.NoDrugCheckbox_CheckedChanged);
            // 
            // NoRecallCheckbox
            // 
            this.NoRecallCheckbox.AutoSize = true;
            this.NoRecallCheckbox.Location = new System.Drawing.Point(16, 113);
            this.NoRecallCheckbox.Name = "NoRecallCheckbox";
            this.NoRecallCheckbox.Size = new System.Drawing.Size(73, 17);
            this.NoRecallCheckbox.TabIndex = 32;
            this.NoRecallCheckbox.Text = "No Recall";
            this.NoRecallCheckbox.UseVisualStyleBackColor = true;
            this.NoRecallCheckbox.CheckedChanged += new System.EventHandler(this.NoRecallCheckbox_CheckedChanged);
            // 
            // NoEscapeCheckbox
            // 
            this.NoEscapeCheckbox.AutoSize = true;
            this.NoEscapeCheckbox.Location = new System.Drawing.Point(16, 89);
            this.NoEscapeCheckbox.Name = "NoEscapeCheckbox";
            this.NoEscapeCheckbox.Size = new System.Drawing.Size(79, 17);
            this.NoEscapeCheckbox.TabIndex = 31;
            this.NoEscapeCheckbox.Text = "No Escape";
            this.NoEscapeCheckbox.UseVisualStyleBackColor = true;
            this.NoEscapeCheckbox.CheckedChanged += new System.EventHandler(this.NoEscapeCheckbox_CheckedChanged);
            // 
            // NoRandomCheckbox
            // 
            this.NoRandomCheckbox.AutoSize = true;
            this.NoRandomCheckbox.Location = new System.Drawing.Point(16, 65);
            this.NoRandomCheckbox.Name = "NoRandomCheckbox";
            this.NoRandomCheckbox.Size = new System.Drawing.Size(83, 17);
            this.NoRandomCheckbox.TabIndex = 30;
            this.NoRandomCheckbox.Text = "No Random";
            this.NoRandomCheckbox.UseVisualStyleBackColor = true;
            this.NoRandomCheckbox.CheckedChanged += new System.EventHandler(this.NoRandomCheckbox_CheckedChanged);
            // 
            // LightningTextbox
            // 
            this.LightningTextbox.Location = new System.Drawing.Point(443, 65);
            this.LightningTextbox.Name = "LightningTextbox";
            this.LightningTextbox.Size = new System.Drawing.Size(49, 20);
            this.LightningTextbox.TabIndex = 29;
            this.LightningTextbox.TextChanged += new System.EventHandler(this.LightningTextbox_TextChanged);
            // 
            // FireTextbox
            // 
            this.FireTextbox.Location = new System.Drawing.Point(443, 41);
            this.FireTextbox.Name = "FireTextbox";
            this.FireTextbox.Size = new System.Drawing.Size(49, 20);
            this.FireTextbox.TabIndex = 28;
            this.FireTextbox.TextChanged += new System.EventHandler(this.FireTextbox_TextChanged);
            // 
            // NoReconnectTextbox
            // 
            this.NoReconnectTextbox.Location = new System.Drawing.Point(118, 39);
            this.NoReconnectTextbox.Name = "NoReconnectTextbox";
            this.NoReconnectTextbox.Size = new System.Drawing.Size(49, 20);
            this.NoReconnectTextbox.TabIndex = 27;
            this.NoReconnectTextbox.TextChanged += new System.EventHandler(this.NoReconnectTextbox_TextChanged);
            // 
            // LightningCheckbox
            // 
            this.LightningCheckbox.AutoSize = true;
            this.LightningCheckbox.Location = new System.Drawing.Point(341, 65);
            this.LightningCheckbox.Name = "LightningCheckbox";
            this.LightningCheckbox.Size = new System.Drawing.Size(69, 17);
            this.LightningCheckbox.TabIndex = 26;
            this.LightningCheckbox.Text = "Lightning";
            this.LightningCheckbox.UseVisualStyleBackColor = true;
            this.LightningCheckbox.CheckedChanged += new System.EventHandler(this.LightningCheckbox_CheckedChanged);
            // 
            // FireCheckbox
            // 
            this.FireCheckbox.AutoSize = true;
            this.FireCheckbox.Location = new System.Drawing.Point(341, 41);
            this.FireCheckbox.Name = "FireCheckbox";
            this.FireCheckbox.Size = new System.Drawing.Size(43, 17);
            this.FireCheckbox.TabIndex = 25;
            this.FireCheckbox.Text = "Fire";
            this.FireCheckbox.UseVisualStyleBackColor = true;
            this.FireCheckbox.CheckStateChanged += new System.EventHandler(this.FireCheckbox_CheckStateChanged);
            // 
            // FightCheckbox
            // 
            this.FightCheckbox.AutoSize = true;
            this.FightCheckbox.Location = new System.Drawing.Point(341, 16);
            this.FightCheckbox.Name = "FightCheckbox";
            this.FightCheckbox.Size = new System.Drawing.Size(49, 17);
            this.FightCheckbox.TabIndex = 23;
            this.FightCheckbox.Text = "Fight";
            this.FightCheckbox.UseVisualStyleBackColor = true;
            this.FightCheckbox.CheckedChanged += new System.EventHandler(this.FightCheckbox_CheckedChanged);
            // 
            // NoReconnectCheckbox
            // 
            this.NoReconnectCheckbox.AutoSize = true;
            this.NoReconnectCheckbox.Location = new System.Drawing.Point(16, 41);
            this.NoReconnectCheckbox.Name = "NoReconnectCheckbox";
            this.NoReconnectCheckbox.Size = new System.Drawing.Size(96, 17);
            this.NoReconnectCheckbox.TabIndex = 22;
            this.NoReconnectCheckbox.Text = "No Reconnect";
            this.NoReconnectCheckbox.UseVisualStyleBackColor = true;
            this.NoReconnectCheckbox.CheckedChanged += new System.EventHandler(this.NoReconnectCheckbox_CheckedChanged);
            // 
            // NoTeleportCheckbox
            // 
            this.NoTeleportCheckbox.AutoSize = true;
            this.NoTeleportCheckbox.Location = new System.Drawing.Point(16, 17);
            this.NoTeleportCheckbox.Name = "NoTeleportCheckbox";
            this.NoTeleportCheckbox.Size = new System.Drawing.Size(82, 17);
            this.NoTeleportCheckbox.TabIndex = 21;
            this.NoTeleportCheckbox.Text = "No Teleport";
            this.NoTeleportCheckbox.UseVisualStyleBackColor = true;
            this.NoTeleportCheckbox.CheckedChanged += new System.EventHandler(this.NoTeleportCheckbox_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.RemoveSZButton);
            this.tabPage3.Controls.Add(this.AddSZButton);
            this.tabPage3.Controls.Add(this.SafeZoneInfoPanel);
            this.tabPage3.Controls.Add(this.SafeZoneInfoListBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(532, 274);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Safe Zones";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // RemoveSZButton
            // 
            this.RemoveSZButton.Location = new System.Drawing.Point(108, 6);
            this.RemoveSZButton.Name = "RemoveSZButton";
            this.RemoveSZButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveSZButton.TabIndex = 8;
            this.RemoveSZButton.Text = "Remove";
            this.RemoveSZButton.UseVisualStyleBackColor = true;
            this.RemoveSZButton.Click += new System.EventHandler(this.RemoveSZButton_Click);
            // 
            // AddSZButton
            // 
            this.AddSZButton.Location = new System.Drawing.Point(6, 6);
            this.AddSZButton.Name = "AddSZButton";
            this.AddSZButton.Size = new System.Drawing.Size(75, 23);
            this.AddSZButton.TabIndex = 7;
            this.AddSZButton.Text = "Add";
            this.AddSZButton.UseVisualStyleBackColor = true;
            this.AddSZButton.Click += new System.EventHandler(this.AddSZButton_Click);
            // 
            // SafeZoneInfoPanel
            // 
            this.SafeZoneInfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SafeZoneInfoPanel.Controls.Add(this.label12);
            this.SafeZoneInfoPanel.Controls.Add(this.SZYTextBox);
            this.SafeZoneInfoPanel.Controls.Add(this.label14);
            this.SafeZoneInfoPanel.Controls.Add(this.SizeTextBox);
            this.SafeZoneInfoPanel.Controls.Add(this.label17);
            this.SafeZoneInfoPanel.Controls.Add(this.SZXTextBox);
            this.SafeZoneInfoPanel.Controls.Add(this.StartPointCheckBox);
            this.SafeZoneInfoPanel.Enabled = false;
            this.SafeZoneInfoPanel.Location = new System.Drawing.Point(189, 35);
            this.SafeZoneInfoPanel.Name = "SafeZoneInfoPanel";
            this.SafeZoneInfoPanel.Size = new System.Drawing.Size(197, 139);
            this.SafeZoneInfoPanel.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(124, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Y:";
            // 
            // SZYTextBox
            // 
            this.SZYTextBox.Location = new System.Drawing.Point(147, 22);
            this.SZYTextBox.MaxLength = 5;
            this.SZYTextBox.Name = "SZYTextBox";
            this.SZYTextBox.Size = new System.Drawing.Size(37, 20);
            this.SZYTextBox.TabIndex = 3;
            this.SZYTextBox.TextChanged += new System.EventHandler(this.SZYTextBox_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(25, 51);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(30, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "Size:";
            // 
            // SizeTextBox
            // 
            this.SizeTextBox.Location = new System.Drawing.Point(61, 48);
            this.SizeTextBox.MaxLength = 5;
            this.SizeTextBox.Name = "SizeTextBox";
            this.SizeTextBox.Size = new System.Drawing.Size(37, 20);
            this.SizeTextBox.TabIndex = 4;
            this.SizeTextBox.TextChanged += new System.EventHandler(this.SizeTextBox_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(38, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "X:";
            // 
            // SZXTextBox
            // 
            this.SZXTextBox.Location = new System.Drawing.Point(61, 22);
            this.SZXTextBox.MaxLength = 5;
            this.SZXTextBox.Name = "SZXTextBox";
            this.SZXTextBox.Size = new System.Drawing.Size(37, 20);
            this.SZXTextBox.TabIndex = 2;
            this.SZXTextBox.TextChanged += new System.EventHandler(this.SZXTextBox_TextChanged);
            // 
            // StartPointCheckBox
            // 
            this.StartPointCheckBox.AutoSize = true;
            this.StartPointCheckBox.Location = new System.Drawing.Point(61, 89);
            this.StartPointCheckBox.Name = "StartPointCheckBox";
            this.StartPointCheckBox.Size = new System.Drawing.Size(75, 17);
            this.StartPointCheckBox.TabIndex = 5;
            this.StartPointCheckBox.Text = "Start Point";
            this.StartPointCheckBox.UseVisualStyleBackColor = true;
            this.StartPointCheckBox.CheckedChanged += new System.EventHandler(this.StartPointCheckBox_CheckedChanged);
            // 
            // SafeZoneInfoListBox
            // 
            this.SafeZoneInfoListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SafeZoneInfoListBox.FormattingEnabled = true;
            this.SafeZoneInfoListBox.Location = new System.Drawing.Point(6, 35);
            this.SafeZoneInfoListBox.Name = "SafeZoneInfoListBox";
            this.SafeZoneInfoListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.SafeZoneInfoListBox.Size = new System.Drawing.Size(177, 134);
            this.SafeZoneInfoListBox.TabIndex = 9;
            this.SafeZoneInfoListBox.SelectedIndexChanged += new System.EventHandler(this.SafeZoneInfoListBox_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.RPasteButton);
            this.tabPage2.Controls.Add(this.RCopyButton);
            this.tabPage2.Controls.Add(this.RemoveRButton);
            this.tabPage2.Controls.Add(this.AddRButton);
            this.tabPage2.Controls.Add(this.RespawnInfoListBox);
            this.tabPage2.Controls.Add(this.RespawnInfoPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(532, 274);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Respawns";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // RPasteButton
            // 
            this.RPasteButton.Location = new System.Drawing.Point(270, 6);
            this.RPasteButton.Name = "RPasteButton";
            this.RPasteButton.Size = new System.Drawing.Size(75, 23);
            this.RPasteButton.TabIndex = 22;
            this.RPasteButton.Text = "Paste";
            this.RPasteButton.UseVisualStyleBackColor = true;
            this.RPasteButton.Click += new System.EventHandler(this.RPasteButton_Click);
            // 
            // RCopyButton
            // 
            this.RCopyButton.Location = new System.Drawing.Point(189, 6);
            this.RCopyButton.Name = "RCopyButton";
            this.RCopyButton.Size = new System.Drawing.Size(75, 23);
            this.RCopyButton.TabIndex = 21;
            this.RCopyButton.Text = "Copy";
            this.RCopyButton.UseVisualStyleBackColor = true;
            // 
            // RemoveRButton
            // 
            this.RemoveRButton.Location = new System.Drawing.Point(108, 6);
            this.RemoveRButton.Name = "RemoveRButton";
            this.RemoveRButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveRButton.TabIndex = 16;
            this.RemoveRButton.Text = "Remove";
            this.RemoveRButton.UseVisualStyleBackColor = true;
            this.RemoveRButton.Click += new System.EventHandler(this.RemoveRButton_Click);
            // 
            // AddRButton
            // 
            this.AddRButton.Location = new System.Drawing.Point(6, 6);
            this.AddRButton.Name = "AddRButton";
            this.AddRButton.Size = new System.Drawing.Size(75, 23);
            this.AddRButton.TabIndex = 15;
            this.AddRButton.Text = "Add";
            this.AddRButton.UseVisualStyleBackColor = true;
            this.AddRButton.Click += new System.EventHandler(this.AddRButton_Click);
            // 
            // RespawnInfoListBox
            // 
            this.RespawnInfoListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.RespawnInfoListBox.FormattingEnabled = true;
            this.RespawnInfoListBox.Location = new System.Drawing.Point(6, 35);
            this.RespawnInfoListBox.Name = "RespawnInfoListBox";
            this.RespawnInfoListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.RespawnInfoListBox.Size = new System.Drawing.Size(260, 147);
            this.RespawnInfoListBox.TabIndex = 14;
            this.RespawnInfoListBox.SelectedIndexChanged += new System.EventHandler(this.RespawnInfoListBox_SelectedIndexChanged);
            // 
            // RespawnInfoPanel
            // 
            this.RespawnInfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RespawnInfoPanel.Controls.Add(this.chkrespawnsave);
            this.RespawnInfoPanel.Controls.Add(this.chkRespawnEnableTick);
            this.RespawnInfoPanel.Controls.Add(this.Randomtextbox);
            this.RespawnInfoPanel.Controls.Add(this.label23);
            this.RespawnInfoPanel.Controls.Add(this.label34);
            this.RespawnInfoPanel.Controls.Add(this.RoutePathTextBox);
            this.RespawnInfoPanel.Controls.Add(this.label24);
            this.RespawnInfoPanel.Controls.Add(this.DirectionTextBox);
            this.RespawnInfoPanel.Controls.Add(this.label8);
            this.RespawnInfoPanel.Controls.Add(this.DelayTextBox);
            this.RespawnInfoPanel.Controls.Add(this.label7);
            this.RespawnInfoPanel.Controls.Add(this.MonsterInfoComboBox);
            this.RespawnInfoPanel.Controls.Add(this.label6);
            this.RespawnInfoPanel.Controls.Add(this.SpreadTextBox);
            this.RespawnInfoPanel.Controls.Add(this.label9);
            this.RespawnInfoPanel.Controls.Add(this.RYTextBox);
            this.RespawnInfoPanel.Controls.Add(this.label10);
            this.RespawnInfoPanel.Controls.Add(this.CountTextBox);
            this.RespawnInfoPanel.Controls.Add(this.label13);
            this.RespawnInfoPanel.Controls.Add(this.RXTextBox);
            this.RespawnInfoPanel.Enabled = false;
            this.RespawnInfoPanel.Location = new System.Drawing.Point(272, 35);
            this.RespawnInfoPanel.Name = "RespawnInfoPanel";
            this.RespawnInfoPanel.Size = new System.Drawing.Size(254, 205);
            this.RespawnInfoPanel.TabIndex = 11;
            // 
            // chkrespawnsave
            // 
            this.chkrespawnsave.AutoSize = true;
            this.chkrespawnsave.Location = new System.Drawing.Point(25, 109);
            this.chkrespawnsave.Name = "chkrespawnsave";
            this.chkrespawnsave.Size = new System.Drawing.Size(164, 17);
            this.chkrespawnsave.TabIndex = 25;
            this.chkrespawnsave.Text = "Save respawnticks on reboot";
            this.chkrespawnsave.UseVisualStyleBackColor = true;
            this.chkrespawnsave.CheckedChanged += new System.EventHandler(this.chkrespawnsave_CheckedChanged);
            // 
            // chkRespawnEnableTick
            // 
            this.chkRespawnEnableTick.AutoSize = true;
            this.chkRespawnEnableTick.Location = new System.Drawing.Point(25, 92);
            this.chkRespawnEnableTick.Name = "chkRespawnEnableTick";
            this.chkRespawnEnableTick.Size = new System.Drawing.Size(137, 17);
            this.chkRespawnEnableTick.TabIndex = 24;
            this.chkRespawnEnableTick.Text = "Use tickbased respawn";
            this.chkRespawnEnableTick.UseVisualStyleBackColor = true;
            this.chkRespawnEnableTick.CheckedChanged += new System.EventHandler(this.chkRespawnEnableTick_CheckedChanged);
            // 
            // Randomtextbox
            // 
            this.Randomtextbox.Location = new System.Drawing.Point(158, 130);
            this.Randomtextbox.MaxLength = 10;
            this.Randomtextbox.Multiline = true;
            this.Randomtextbox.Name = "Randomtextbox";
            this.Randomtextbox.Size = new System.Drawing.Size(37, 20);
            this.Randomtextbox.TabIndex = 23;
            this.toolTip1.SetToolTip(this.Randomtextbox, "Allows random + or - added to each spawn time");
            this.Randomtextbox.TextChanged += new System.EventHandler(this.RandomTextBox_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(135, 133);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(18, 13);
            this.label23.TabIndex = 22;
            this.label23.Text = "R:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(22, 159);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(39, 13);
            this.label34.TabIndex = 21;
            this.label34.Text = "Route:";
            // 
            // RoutePathTextBox
            // 
            this.RoutePathTextBox.Location = new System.Drawing.Point(65, 156);
            this.RoutePathTextBox.Name = "RoutePathTextBox";
            this.RoutePathTextBox.Size = new System.Drawing.Size(130, 20);
            this.RoutePathTextBox.TabIndex = 20;
            this.RoutePathTextBox.TextChanged += new System.EventHandler(this.RoutePathTextBox_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(22, 185);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(23, 13);
            this.label24.TabIndex = 18;
            this.label24.Text = "Dir:";
            // 
            // DirectionTextBox
            // 
            this.DirectionTextBox.Location = new System.Drawing.Point(65, 182);
            this.DirectionTextBox.MaxLength = 5;
            this.DirectionTextBox.Name = "DirectionTextBox";
            this.DirectionTextBox.Size = new System.Drawing.Size(37, 20);
            this.DirectionTextBox.TabIndex = 17;
            this.DirectionTextBox.TextChanged += new System.EventHandler(this.DirectionTextBox_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Delay:";
            // 
            // DelayTextBox
            // 
            this.DelayTextBox.Location = new System.Drawing.Point(65, 130);
            this.DelayTextBox.MaxLength = 10;
            this.DelayTextBox.Multiline = true;
            this.DelayTextBox.Name = "DelayTextBox";
            this.DelayTextBox.Size = new System.Drawing.Size(62, 20);
            this.DelayTextBox.TabIndex = 15;
            this.toolTip1.SetToolTip(this.DelayTextBox, "if you use tick based spawn: this is ignored!");
            this.DelayTextBox.TextChanged += new System.EventHandler(this.DelayTextBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Monster:";
            // 
            // MonsterInfoComboBox
            // 
            this.MonsterInfoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MonsterInfoComboBox.FormattingEnabled = true;
            this.MonsterInfoComboBox.Location = new System.Drawing.Point(65, 13);
            this.MonsterInfoComboBox.Name = "MonsterInfoComboBox";
            this.MonsterInfoComboBox.Size = new System.Drawing.Size(130, 21);
            this.MonsterInfoComboBox.TabIndex = 13;
            this.MonsterInfoComboBox.SelectedIndexChanged += new System.EventHandler(this.MonsterInfoComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(108, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Spread:";
            // 
            // SpreadTextBox
            // 
            this.SpreadTextBox.Location = new System.Drawing.Point(158, 66);
            this.SpreadTextBox.MaxLength = 5;
            this.SpreadTextBox.Name = "SpreadTextBox";
            this.SpreadTextBox.Size = new System.Drawing.Size(37, 20);
            this.SpreadTextBox.TabIndex = 11;
            this.SpreadTextBox.TextChanged += new System.EventHandler(this.SpreadTextBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(135, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Y:";
            // 
            // RYTextBox
            // 
            this.RYTextBox.Location = new System.Drawing.Point(158, 40);
            this.RYTextBox.MaxLength = 5;
            this.RYTextBox.Name = "RYTextBox";
            this.RYTextBox.Size = new System.Drawing.Size(37, 20);
            this.RYTextBox.TabIndex = 3;
            this.RYTextBox.TextChanged += new System.EventHandler(this.RYTextBox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Count:";
            // 
            // CountTextBox
            // 
            this.CountTextBox.Location = new System.Drawing.Point(65, 66);
            this.CountTextBox.MaxLength = 5;
            this.CountTextBox.Name = "CountTextBox";
            this.CountTextBox.Size = new System.Drawing.Size(37, 20);
            this.CountTextBox.TabIndex = 4;
            this.CountTextBox.TextChanged += new System.EventHandler(this.CountTextBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(42, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "X:";
            // 
            // RXTextBox
            // 
            this.RXTextBox.Location = new System.Drawing.Point(65, 40);
            this.RXTextBox.MaxLength = 5;
            this.RXTextBox.Name = "RXTextBox";
            this.RXTextBox.Size = new System.Drawing.Size(37, 20);
            this.RXTextBox.TabIndex = 2;
            this.RXTextBox.TextChanged += new System.EventHandler(this.RXTextBox_TextChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.RemoveMButton);
            this.tabPage4.Controls.Add(this.AddMButton);
            this.tabPage4.Controls.Add(this.MovementInfoPanel);
            this.tabPage4.Controls.Add(this.MovementInfoListBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(532, 274);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Movements";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // RemoveMButton
            // 
            this.RemoveMButton.Location = new System.Drawing.Point(108, 6);
            this.RemoveMButton.Name = "RemoveMButton";
            this.RemoveMButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveMButton.TabIndex = 12;
            this.RemoveMButton.Text = "Remove";
            this.RemoveMButton.UseVisualStyleBackColor = true;
            this.RemoveMButton.Click += new System.EventHandler(this.RemoveMButton_Click);
            // 
            // AddMButton
            // 
            this.AddMButton.Location = new System.Drawing.Point(6, 6);
            this.AddMButton.Name = "AddMButton";
            this.AddMButton.Size = new System.Drawing.Size(75, 23);
            this.AddMButton.TabIndex = 11;
            this.AddMButton.Text = "Add";
            this.AddMButton.UseVisualStyleBackColor = true;
            this.AddMButton.Click += new System.EventHandler(this.AddMButton_Click);
            // 
            // MovementInfoPanel
            // 
            this.MovementInfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MovementInfoPanel.Controls.Add(this.label25);
            this.MovementInfoPanel.Controls.Add(this.ConquestComboBox);
            this.MovementInfoPanel.Controls.Add(this.NeedMoveMCheckBox);
            this.MovementInfoPanel.Controls.Add(this.NeedHoleMCheckBox);
            this.MovementInfoPanel.Controls.Add(this.label22);
            this.MovementInfoPanel.Controls.Add(this.DestMapComboBox);
            this.MovementInfoPanel.Controls.Add(this.label18);
            this.MovementInfoPanel.Controls.Add(this.DestYTextBox);
            this.MovementInfoPanel.Controls.Add(this.label21);
            this.MovementInfoPanel.Controls.Add(this.DestXTextBox);
            this.MovementInfoPanel.Controls.Add(this.label16);
            this.MovementInfoPanel.Controls.Add(this.SourceYTextBox);
            this.MovementInfoPanel.Controls.Add(this.label20);
            this.MovementInfoPanel.Controls.Add(this.SourceXTextBox);
            this.MovementInfoPanel.Enabled = false;
            this.MovementInfoPanel.Location = new System.Drawing.Point(239, 35);
            this.MovementInfoPanel.Name = "MovementInfoPanel";
            this.MovementInfoPanel.Size = new System.Drawing.Size(256, 205);
            this.MovementInfoPanel.TabIndex = 14;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(3, 171);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(55, 13);
            this.label25.TabIndex = 20;
            this.label25.Text = "Conquest:";
            // 
            // ConquestComboBox
            // 
            this.ConquestComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConquestComboBox.FormattingEnabled = true;
            this.ConquestComboBox.Location = new System.Drawing.Point(64, 168);
            this.ConquestComboBox.Name = "ConquestComboBox";
            this.ConquestComboBox.Size = new System.Drawing.Size(182, 21);
            this.ConquestComboBox.TabIndex = 19;
            this.ConquestComboBox.SelectedIndexChanged += new System.EventHandler(this.ConquestComboBox_SelectedIndexChanged);
            // 
            // NeedMoveMCheckBox
            // 
            this.NeedMoveMCheckBox.AutoSize = true;
            this.NeedMoveMCheckBox.Location = new System.Drawing.Point(14, 145);
            this.NeedMoveMCheckBox.Name = "NeedMoveMCheckBox";
            this.NeedMoveMCheckBox.Size = new System.Drawing.Size(82, 17);
            this.NeedMoveMCheckBox.TabIndex = 18;
            this.NeedMoveMCheckBox.Text = "Need Move";
            this.NeedMoveMCheckBox.UseVisualStyleBackColor = true;
            this.NeedMoveMCheckBox.CheckedChanged += new System.EventHandler(this.NeedScriptMCheckBox_CheckedChanged);
            // 
            // NeedHoleMCheckBox
            // 
            this.NeedHoleMCheckBox.AutoSize = true;
            this.NeedHoleMCheckBox.Location = new System.Drawing.Point(14, 122);
            this.NeedHoleMCheckBox.Name = "NeedHoleMCheckBox";
            this.NeedHoleMCheckBox.Size = new System.Drawing.Size(77, 17);
            this.NeedHoleMCheckBox.TabIndex = 17;
            this.NeedHoleMCheckBox.Text = "Need Hole";
            this.NeedHoleMCheckBox.UseVisualStyleBackColor = true;
            this.NeedHoleMCheckBox.CheckedChanged += new System.EventHandler(this.NeedHoleMCheckBox_CheckedChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(11, 58);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(47, 13);
            this.label22.TabIndex = 16;
            this.label22.Text = "To Map:";
            // 
            // DestMapComboBox
            // 
            this.DestMapComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DestMapComboBox.FormattingEnabled = true;
            this.DestMapComboBox.Location = new System.Drawing.Point(64, 55);
            this.DestMapComboBox.Name = "DestMapComboBox";
            this.DestMapComboBox.Size = new System.Drawing.Size(182, 21);
            this.DestMapComboBox.TabIndex = 15;
            this.DestMapComboBox.SelectedIndexChanged += new System.EventHandler(this.DestMapComboBox_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(118, 85);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(33, 13);
            this.label18.TabIndex = 14;
            this.label18.Text = "To Y:";
            // 
            // DestYTextBox
            // 
            this.DestYTextBox.Location = new System.Drawing.Point(157, 82);
            this.DestYTextBox.MaxLength = 5;
            this.DestYTextBox.Name = "DestYTextBox";
            this.DestYTextBox.Size = new System.Drawing.Size(37, 20);
            this.DestYTextBox.TabIndex = 12;
            this.DestYTextBox.TextChanged += new System.EventHandler(this.DestYTextBox_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Cursor = System.Windows.Forms.Cursors.Default;
            this.label21.Location = new System.Drawing.Point(23, 85);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(33, 13);
            this.label21.TabIndex = 13;
            this.label21.Text = "To X:";
            // 
            // DestXTextBox
            // 
            this.DestXTextBox.Location = new System.Drawing.Point(62, 82);
            this.DestXTextBox.MaxLength = 5;
            this.DestXTextBox.Name = "DestXTextBox";
            this.DestXTextBox.Size = new System.Drawing.Size(37, 20);
            this.DestXTextBox.TabIndex = 11;
            this.DestXTextBox.TextChanged += new System.EventHandler(this.DestXTextBox_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(108, 32);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "From Y:";
            // 
            // SourceYTextBox
            // 
            this.SourceYTextBox.Location = new System.Drawing.Point(157, 29);
            this.SourceYTextBox.MaxLength = 5;
            this.SourceYTextBox.Name = "SourceYTextBox";
            this.SourceYTextBox.Size = new System.Drawing.Size(37, 20);
            this.SourceYTextBox.TabIndex = 3;
            this.SourceYTextBox.TextChanged += new System.EventHandler(this.SourceYTextBox_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(13, 32);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(43, 13);
            this.label20.TabIndex = 3;
            this.label20.Text = "From X:";
            // 
            // SourceXTextBox
            // 
            this.SourceXTextBox.Location = new System.Drawing.Point(62, 29);
            this.SourceXTextBox.MaxLength = 5;
            this.SourceXTextBox.Name = "SourceXTextBox";
            this.SourceXTextBox.Size = new System.Drawing.Size(37, 20);
            this.SourceXTextBox.TabIndex = 2;
            this.SourceXTextBox.TextChanged += new System.EventHandler(this.SourceXTextBox_TextChanged);
            // 
            // MovementInfoListBox
            // 
            this.MovementInfoListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MovementInfoListBox.FormattingEnabled = true;
            this.MovementInfoListBox.Location = new System.Drawing.Point(6, 35);
            this.MovementInfoListBox.Name = "MovementInfoListBox";
            this.MovementInfoListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.MovementInfoListBox.Size = new System.Drawing.Size(227, 173);
            this.MovementInfoListBox.TabIndex = 13;
            this.MovementInfoListBox.SelectedIndexChanged += new System.EventHandler(this.MovementInfoListBox_SelectedIndexChanged);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.MZDeletebutton);
            this.tabPage7.Controls.Add(this.MZAddbutton);
            this.tabPage7.Controls.Add(this.MineZonepanel);
            this.tabPage7.Controls.Add(this.MZListlistBox);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(532, 274);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "MineZones";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // MZDeletebutton
            // 
            this.MZDeletebutton.Location = new System.Drawing.Point(108, 6);
            this.MZDeletebutton.Name = "MZDeletebutton";
            this.MZDeletebutton.Size = new System.Drawing.Size(75, 23);
            this.MZDeletebutton.TabIndex = 12;
            this.MZDeletebutton.Text = "Remove";
            this.MZDeletebutton.UseVisualStyleBackColor = true;
            this.MZDeletebutton.Click += new System.EventHandler(this.MZDeletebutton_Click);
            // 
            // MZAddbutton
            // 
            this.MZAddbutton.Location = new System.Drawing.Point(6, 6);
            this.MZAddbutton.Name = "MZAddbutton";
            this.MZAddbutton.Size = new System.Drawing.Size(75, 23);
            this.MZAddbutton.TabIndex = 11;
            this.MZAddbutton.Text = "Add";
            this.MZAddbutton.UseVisualStyleBackColor = true;
            this.MZAddbutton.Click += new System.EventHandler(this.MZAddbutton_Click);
            // 
            // MineZonepanel
            // 
            this.MineZonepanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MineZonepanel.Controls.Add(this.label27);
            this.MineZonepanel.Controls.Add(this.MineZoneComboBox);
            this.MineZonepanel.Controls.Add(this.label30);
            this.MineZonepanel.Controls.Add(this.MZYtextBox);
            this.MineZonepanel.Controls.Add(this.label31);
            this.MineZonepanel.Controls.Add(this.MZSizetextBox);
            this.MineZonepanel.Controls.Add(this.label32);
            this.MineZonepanel.Controls.Add(this.MZXtextBox);
            this.MineZonepanel.Enabled = false;
            this.MineZonepanel.Location = new System.Drawing.Point(189, 35);
            this.MineZonepanel.Name = "MineZonepanel";
            this.MineZonepanel.Size = new System.Drawing.Size(241, 139);
            this.MineZonepanel.TabIndex = 14;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(12, 17);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(60, 13);
            this.label27.TabIndex = 14;
            this.label27.Text = "Mine Type:";
            // 
            // MineZoneComboBox
            // 
            this.MineZoneComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MineZoneComboBox.FormattingEnabled = true;
            this.MineZoneComboBox.Location = new System.Drawing.Point(78, 14);
            this.MineZoneComboBox.Name = "MineZoneComboBox";
            this.MineZoneComboBox.Size = new System.Drawing.Size(124, 21);
            this.MineZoneComboBox.TabIndex = 13;
            this.MineZoneComboBox.SelectedIndexChanged += new System.EventHandler(this.MineZoneComboBox_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(142, 53);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(17, 13);
            this.label30.TabIndex = 10;
            this.label30.Text = "Y:";
            // 
            // MZYtextBox
            // 
            this.MZYtextBox.Location = new System.Drawing.Point(165, 50);
            this.MZYtextBox.MaxLength = 5;
            this.MZYtextBox.Name = "MZYtextBox";
            this.MZYtextBox.Size = new System.Drawing.Size(37, 20);
            this.MZYtextBox.TabIndex = 3;
            this.MZYtextBox.TextChanged += new System.EventHandler(this.MZYtextBox_TextChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(42, 99);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(30, 13);
            this.label31.TabIndex = 8;
            this.label31.Text = "Size:";
            // 
            // MZSizetextBox
            // 
            this.MZSizetextBox.Location = new System.Drawing.Point(78, 96);
            this.MZSizetextBox.MaxLength = 5;
            this.MZSizetextBox.Name = "MZSizetextBox";
            this.MZSizetextBox.Size = new System.Drawing.Size(37, 20);
            this.MZSizetextBox.TabIndex = 4;
            this.MZSizetextBox.TextChanged += new System.EventHandler(this.MZSizetextBox_TextChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(55, 57);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(17, 13);
            this.label32.TabIndex = 3;
            this.label32.Text = "X:";
            // 
            // MZXtextBox
            // 
            this.MZXtextBox.Location = new System.Drawing.Point(78, 54);
            this.MZXtextBox.MaxLength = 5;
            this.MZXtextBox.Name = "MZXtextBox";
            this.MZXtextBox.Size = new System.Drawing.Size(37, 20);
            this.MZXtextBox.TabIndex = 2;
            this.MZXtextBox.TextChanged += new System.EventHandler(this.MZXtextBox_TextChanged);
            // 
            // MZListlistBox
            // 
            this.MZListlistBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MZListlistBox.FormattingEnabled = true;
            this.MZListlistBox.Location = new System.Drawing.Point(6, 35);
            this.MZListlistBox.Name = "MZListlistBox";
            this.MZListlistBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.MZListlistBox.Size = new System.Drawing.Size(177, 134);
            this.MZListlistBox.TabIndex = 13;
            this.MZListlistBox.SelectedIndexChanged += new System.EventHandler(this.MZListlistBox_SelectedIndexChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.panelEventRespawn);
            this.tabPage5.Controls.Add(this.BtnRemoveEventRespawn);
            this.tabPage5.Controls.Add(this.btnAddEventRespawn);
            this.tabPage5.Controls.Add(this.lstEventRespawns);
            this.tabPage5.Controls.Add(this.panelEventInfo);
            this.tabPage5.Controls.Add(this.btnRemoveEvent);
            this.tabPage5.Controls.Add(this.btnAddEvent);
            this.tabPage5.Controls.Add(this.lstEventInfos);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(532, 274);
            this.tabPage5.TabIndex = 7;
            this.tabPage5.Text = "Events";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // panelEventRespawn
            // 
            this.panelEventRespawn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEventRespawn.Controls.Add(this.txtSpreadX);
            this.panelEventRespawn.Controls.Add(this.label42);
            this.panelEventRespawn.Controls.Add(this.chkIsRespawnObjective);
            this.panelEventRespawn.Controls.Add(this.txtSpreadY);
            this.panelEventRespawn.Controls.Add(this.label41);
            this.panelEventRespawn.Controls.Add(this.txtOrder);
            this.panelEventRespawn.Controls.Add(this.label40);
            this.panelEventRespawn.Controls.Add(this.label28);
            this.panelEventRespawn.Controls.Add(this.txtEventRespawnMonCount);
            this.panelEventRespawn.Controls.Add(this.label35);
            this.panelEventRespawn.Controls.Add(this.txtEventRespawnMonName);
            this.panelEventRespawn.Enabled = false;
            this.panelEventRespawn.Location = new System.Drawing.Point(374, 108);
            this.panelEventRespawn.Name = "panelEventRespawn";
            this.panelEventRespawn.Size = new System.Drawing.Size(156, 139);
            this.panelEventRespawn.TabIndex = 25;
            // 
            // txtSpreadX
            // 
            this.txtSpreadX.Location = new System.Drawing.Point(56, 50);
            this.txtSpreadX.MaxLength = 2;
            this.txtSpreadX.Name = "txtSpreadX";
            this.txtSpreadX.Size = new System.Drawing.Size(95, 20);
            this.txtSpreadX.TabIndex = 22;
            this.txtSpreadX.TextChanged += new System.EventHandler(this.txtSpreadX_TextChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(4, 52);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(48, 13);
            this.label42.TabIndex = 21;
            this.label42.Text = "SpreadX";
            // 
            // chkIsRespawnObjective
            // 
            this.chkIsRespawnObjective.AutoSize = true;
            this.chkIsRespawnObjective.Location = new System.Drawing.Point(7, 118);
            this.chkIsRespawnObjective.Name = "chkIsRespawnObjective";
            this.chkIsRespawnObjective.Size = new System.Drawing.Size(71, 17);
            this.chkIsRespawnObjective.TabIndex = 20;
            this.chkIsRespawnObjective.Text = "Objective";
            this.chkIsRespawnObjective.UseVisualStyleBackColor = true;
            this.chkIsRespawnObjective.CheckedChanged += new System.EventHandler(this.chkIsRespawnObjective_CheckedChanged);
            // 
            // txtSpreadY
            // 
            this.txtSpreadY.Location = new System.Drawing.Point(56, 72);
            this.txtSpreadY.MaxLength = 2;
            this.txtSpreadY.Name = "txtSpreadY";
            this.txtSpreadY.Size = new System.Drawing.Size(95, 20);
            this.txtSpreadY.TabIndex = 14;
            this.txtSpreadY.TextChanged += new System.EventHandler(this.txtSpreadY_TextChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(4, 75);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(48, 13);
            this.label41.TabIndex = 13;
            this.label41.Text = "SpreadY";
            // 
            // txtOrder
            // 
            this.txtOrder.Location = new System.Drawing.Point(56, 98);
            this.txtOrder.MaxLength = 5;
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(95, 20);
            this.txtOrder.TabIndex = 12;
            this.txtOrder.TextChanged += new System.EventHandler(this.txtOrder_TextChanged);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(4, 100);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(33, 13);
            this.label40.TabIndex = 11;
            this.label40.Text = "Order";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(4, 29);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(38, 13);
            this.label28.TabIndex = 10;
            this.label28.Text = "Count ";
            // 
            // txtEventRespawnMonCount
            // 
            this.txtEventRespawnMonCount.Location = new System.Drawing.Point(56, 27);
            this.txtEventRespawnMonCount.MaxLength = 2;
            this.txtEventRespawnMonCount.Name = "txtEventRespawnMonCount";
            this.txtEventRespawnMonCount.Size = new System.Drawing.Size(95, 20);
            this.txtEventRespawnMonCount.TabIndex = 9;
            this.txtEventRespawnMonCount.TextChanged += new System.EventHandler(this.txtEventRespawnMonCount_TextChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(3, 6);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(48, 13);
            this.label35.TabIndex = 8;
            this.label35.Text = "Monster ";
            // 
            // txtEventRespawnMonName
            // 
            this.txtEventRespawnMonName.Location = new System.Drawing.Point(56, 3);
            this.txtEventRespawnMonName.MaxLength = 50;
            this.txtEventRespawnMonName.Name = "txtEventRespawnMonName";
            this.txtEventRespawnMonName.Size = new System.Drawing.Size(95, 20);
            this.txtEventRespawnMonName.TabIndex = 4;
            this.txtEventRespawnMonName.TextChanged += new System.EventHandler(this.txtEventRespawnMonName_TextChanged);
            // 
            // BtnRemoveEventRespawn
            // 
            this.BtnRemoveEventRespawn.Location = new System.Drawing.Point(468, 8);
            this.BtnRemoveEventRespawn.Name = "BtnRemoveEventRespawn";
            this.BtnRemoveEventRespawn.Size = new System.Drawing.Size(62, 23);
            this.BtnRemoveEventRespawn.TabIndex = 24;
            this.BtnRemoveEventRespawn.Text = "Remove";
            this.BtnRemoveEventRespawn.UseVisualStyleBackColor = true;
            this.BtnRemoveEventRespawn.Click += new System.EventHandler(this.BtnRemoveEventRespawn_Click);
            // 
            // btnAddEventRespawn
            // 
            this.btnAddEventRespawn.Location = new System.Drawing.Point(373, 8);
            this.btnAddEventRespawn.Name = "btnAddEventRespawn";
            this.btnAddEventRespawn.Size = new System.Drawing.Size(64, 23);
            this.btnAddEventRespawn.TabIndex = 23;
            this.btnAddEventRespawn.Text = "Add";
            this.btnAddEventRespawn.UseVisualStyleBackColor = true;
            this.btnAddEventRespawn.Click += new System.EventHandler(this.btnAddEventRespawn_Click);
            // 
            // lstEventRespawns
            // 
            this.lstEventRespawns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstEventRespawns.FormattingEnabled = true;
            this.lstEventRespawns.Location = new System.Drawing.Point(375, 35);
            this.lstEventRespawns.Name = "lstEventRespawns";
            this.lstEventRespawns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstEventRespawns.Size = new System.Drawing.Size(154, 69);
            this.lstEventRespawns.TabIndex = 22;
            this.lstEventRespawns.SelectedIndexChanged += new System.EventHandler(this.lstEventRespawns_SelectedIndexChanged);
            // 
            // panelEventInfo
            // 
            this.panelEventInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEventInfo.Controls.Add(this.label26);
            this.panelEventInfo.Controls.Add(this.txtObjectiveMsg);
            this.panelEventInfo.Controls.Add(this.chkIsSafeZone);
            this.panelEventInfo.Controls.Add(this.ddlEventType);
            this.panelEventInfo.Controls.Add(this.label39);
            this.panelEventInfo.Controls.Add(this.label38);
            this.panelEventInfo.Controls.Add(this.txtCooldownInMins);
            this.panelEventInfo.Controls.Add(this.label37);
            this.panelEventInfo.Controls.Add(this.txtEventSize);
            this.panelEventInfo.Controls.Add(this.label29);
            this.panelEventInfo.Controls.Add(this.txtEventName);
            this.panelEventInfo.Controls.Add(this.label36);
            this.panelEventInfo.Controls.Add(this.txtEventMultipleCoords);
            this.panelEventInfo.Enabled = false;
            this.panelEventInfo.Location = new System.Drawing.Point(161, 8);
            this.panelEventInfo.Name = "panelEventInfo";
            this.panelEventInfo.Size = new System.Drawing.Size(210, 229);
            this.panelEventInfo.TabIndex = 21;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(3, 187);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(98, 13);
            this.label26.TabIndex = 20;
            this.label26.Text = "Objective Message";
            // 
            // txtObjectiveMsg
            // 
            this.txtObjectiveMsg.Location = new System.Drawing.Point(4, 203);
            this.txtObjectiveMsg.MaxLength = 100;
            this.txtObjectiveMsg.Name = "txtObjectiveMsg";
            this.txtObjectiveMsg.Size = new System.Drawing.Size(202, 20);
            this.txtObjectiveMsg.TabIndex = 19;
            this.txtObjectiveMsg.TextChanged += new System.EventHandler(this.txtObjectiveMsg_TextChanged);
            // 
            // chkIsSafeZone
            // 
            this.chkIsSafeZone.AutoSize = true;
            this.chkIsSafeZone.Location = new System.Drawing.Point(122, 30);
            this.chkIsSafeZone.Name = "chkIsSafeZone";
            this.chkIsSafeZone.Size = new System.Drawing.Size(84, 17);
            this.chkIsSafeZone.TabIndex = 18;
            this.chkIsSafeZone.Text = "Is SafeZone";
            this.chkIsSafeZone.UseVisualStyleBackColor = true;
            this.chkIsSafeZone.CheckedChanged += new System.EventHandler(this.chkIsSafeZone_CheckedChanged);
            // 
            // ddlEventType
            // 
            this.ddlEventType.FormattingEnabled = true;
            this.ddlEventType.Location = new System.Drawing.Point(6, 30);
            this.ddlEventType.Name = "ddlEventType";
            this.ddlEventType.Size = new System.Drawing.Size(110, 21);
            this.ddlEventType.TabIndex = 17;
            this.ddlEventType.SelectedIndexChanged += new System.EventHandler(this.ddlEventType_SelectedIndexChanged);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(1, 14);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(62, 13);
            this.label39.TabIndex = 16;
            this.label39.Text = "Event Type";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(115, 142);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(77, 13);
            this.label38.TabIndex = 14;
            this.label38.Text = "Cooldown(Min)";
            // 
            // txtCooldownInMins
            // 
            this.txtCooldownInMins.Location = new System.Drawing.Point(115, 158);
            this.txtCooldownInMins.MaxLength = 5;
            this.txtCooldownInMins.Name = "txtCooldownInMins";
            this.txtCooldownInMins.Size = new System.Drawing.Size(91, 20);
            this.txtCooldownInMins.TabIndex = 13;
            this.txtCooldownInMins.TextChanged += new System.EventHandler(this.txtCooldownInMins_TextChanged);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(2, 142);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(58, 13);
            this.label37.TabIndex = 12;
            this.label37.Text = "Event Size";
            // 
            // txtEventSize
            // 
            this.txtEventSize.Location = new System.Drawing.Point(5, 158);
            this.txtEventSize.MaxLength = 5;
            this.txtEventSize.Name = "txtEventSize";
            this.txtEventSize.Size = new System.Drawing.Size(104, 20);
            this.txtEventSize.TabIndex = 11;
            this.txtEventSize.TextChanged += new System.EventHandler(this.txtEventSize_TextChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(3, 56);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(66, 13);
            this.label29.TabIndex = 10;
            this.label29.Text = "Event Name";
            // 
            // txtEventName
            // 
            this.txtEventName.Location = new System.Drawing.Point(5, 75);
            this.txtEventName.MaxLength = 50;
            this.txtEventName.Name = "txtEventName";
            this.txtEventName.Size = new System.Drawing.Size(201, 20);
            this.txtEventName.TabIndex = 1;
            this.txtEventName.TextChanged += new System.EventHandler(this.txtEventName_TextChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(2, 101);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(109, 13);
            this.label36.TabIndex = 8;
            this.label36.Text = "Multiple Coords (CSV)";
            // 
            // txtEventMultipleCoords
            // 
            this.txtEventMultipleCoords.Location = new System.Drawing.Point(5, 117);
            this.txtEventMultipleCoords.MaxLength = 200;
            this.txtEventMultipleCoords.Name = "txtEventMultipleCoords";
            this.txtEventMultipleCoords.Size = new System.Drawing.Size(201, 20);
            this.txtEventMultipleCoords.TabIndex = 4;
            this.txtEventMultipleCoords.TextChanged += new System.EventHandler(this.txtEventMultipleCoords_TextChanged);
            // 
            // btnRemoveEvent
            // 
            this.btnRemoveEvent.Location = new System.Drawing.Point(77, 12);
            this.btnRemoveEvent.Name = "btnRemoveEvent";
            this.btnRemoveEvent.Size = new System.Drawing.Size(64, 23);
            this.btnRemoveEvent.TabIndex = 20;
            this.btnRemoveEvent.Text = "Remove";
            this.btnRemoveEvent.UseVisualStyleBackColor = true;
            this.btnRemoveEvent.Click += new System.EventHandler(this.btnRemoveEvent_Click);
            // 
            // btnAddEvent
            // 
            this.btnAddEvent.Location = new System.Drawing.Point(6, 12);
            this.btnAddEvent.Name = "btnAddEvent";
            this.btnAddEvent.Size = new System.Drawing.Size(56, 23);
            this.btnAddEvent.TabIndex = 19;
            this.btnAddEvent.Text = "Add";
            this.btnAddEvent.UseVisualStyleBackColor = true;
            this.btnAddEvent.Click += new System.EventHandler(this.btnAddEvent_Click);
            // 
            // lstEventInfos
            // 
            this.lstEventInfos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstEventInfos.FormattingEnabled = true;
            this.lstEventInfos.Location = new System.Drawing.Point(6, 41);
            this.lstEventInfos.Name = "lstEventInfos";
            this.lstEventInfos.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstEventInfos.Size = new System.Drawing.Size(134, 199);
            this.lstEventInfos.TabIndex = 18;
            this.lstEventInfos.SelectedIndexChanged += new System.EventHandler(this.lstEventInfos_SelectedIndexChanged);
            // 
            // lms_BRTab
            // 
            this.lms_BRTab.Controls.Add(this.panel2);
            this.lms_BRTab.Controls.Add(this.lmsPanel);
            this.lms_BRTab.Location = new System.Drawing.Point(4, 22);
            this.lms_BRTab.Name = "lms_BRTab";
            this.lms_BRTab.Padding = new System.Windows.Forms.Padding(3);
            this.lms_BRTab.Size = new System.Drawing.Size(532, 274);
            this.lms_BRTab.TabIndex = 8;
            this.lms_BRTab.Text = "LMS BR";
            this.lms_BRTab.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.removeLMSBtn);
            this.panel2.Controls.Add(this.addLMSBtn);
            this.panel2.Controls.Add(this.lmsListBox);
            this.panel2.Location = new System.Drawing.Point(240, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(286, 261);
            this.panel2.TabIndex = 28;
            // 
            // removeLMSBtn
            // 
            this.removeLMSBtn.Location = new System.Drawing.Point(71, 233);
            this.removeLMSBtn.Name = "removeLMSBtn";
            this.removeLMSBtn.Size = new System.Drawing.Size(75, 23);
            this.removeLMSBtn.TabIndex = 30;
            this.removeLMSBtn.Text = "Remove";
            this.removeLMSBtn.UseVisualStyleBackColor = true;
            this.removeLMSBtn.Click += new System.EventHandler(this.removeLMSBtn_Click);
            // 
            // addLMSBtn
            // 
            this.addLMSBtn.Location = new System.Drawing.Point(154, 233);
            this.addLMSBtn.Name = "addLMSBtn";
            this.addLMSBtn.Size = new System.Drawing.Size(75, 23);
            this.addLMSBtn.TabIndex = 29;
            this.addLMSBtn.Text = "Add";
            this.addLMSBtn.UseVisualStyleBackColor = true;
            this.addLMSBtn.Click += new System.EventHandler(this.addLMSBtn_Click);
            // 
            // lmsListBox
            // 
            this.lmsListBox.FormattingEnabled = true;
            this.lmsListBox.Location = new System.Drawing.Point(3, 6);
            this.lmsListBox.Name = "lmsListBox";
            this.lmsListBox.Size = new System.Drawing.Size(280, 225);
            this.lmsListBox.TabIndex = 28;
            this.lmsListBox.SelectedIndexChanged += new System.EventHandler(this.lmsListBox_SelectedIndexChanged);
            // 
            // lmsPanel
            // 
            this.lmsPanel.Controls.Add(this.rewardPanel);
            this.lmsPanel.Controls.Add(this.rewardDialog);
            this.lmsPanel.Controls.Add(this.label46);
            this.lmsPanel.Controls.Add(this.label43);
            this.lmsPanel.Controls.Add(this.lmsDay);
            this.lmsPanel.Controls.Add(this.label44);
            this.lmsPanel.Controls.Add(this.lmsRange);
            this.lmsPanel.Controls.Add(this.lmsHour);
            this.lmsPanel.Controls.Add(this.label56);
            this.lmsPanel.Controls.Add(this.label45);
            this.lmsPanel.Controls.Add(this.lmsMinute);
            this.lmsPanel.Controls.Add(this.label54);
            this.lmsPanel.Controls.Add(this.label47);
            this.lmsPanel.Controls.Add(this.label53);
            this.lmsPanel.Controls.Add(this.lmsDuration);
            this.lmsPanel.Controls.Add(this.lmsMax);
            this.lmsPanel.Controls.Add(this.label48);
            this.lmsPanel.Controls.Add(this.lmsMin);
            this.lmsPanel.Controls.Add(this.lmsX);
            this.lmsPanel.Controls.Add(this.label52);
            this.lmsPanel.Controls.Add(this.lmsY);
            this.lmsPanel.Controls.Add(this.lmsSize);
            this.lmsPanel.Controls.Add(this.label49);
            this.lmsPanel.Controls.Add(this.label51);
            this.lmsPanel.Controls.Add(this.label50);
            this.lmsPanel.Enabled = false;
            this.lmsPanel.Location = new System.Drawing.Point(6, 6);
            this.lmsPanel.Name = "lmsPanel";
            this.lmsPanel.Size = new System.Drawing.Size(228, 261);
            this.lmsPanel.TabIndex = 27;
            // 
            // rewardPanel
            // 
            this.rewardPanel.Controls.Add(this.button1);
            this.rewardPanel.Controls.Add(this.lmsRewardRankBox);
            this.rewardPanel.Controls.Add(this.label59);
            this.rewardPanel.Controls.Add(this.label58);
            this.rewardPanel.Controls.Add(this.lmsRewardRateBox);
            this.rewardPanel.Controls.Add(this.label57);
            this.rewardPanel.Controls.Add(this.lmsRewardClassCbox);
            this.rewardPanel.Controls.Add(this.lmsRewardGroupTextBox);
            this.rewardPanel.Controls.Add(this.label55);
            this.rewardPanel.Controls.Add(this.lmsRewardClassSpecific);
            this.rewardPanel.Controls.Add(this.lmsRewardAddBtn);
            this.rewardPanel.Controls.Add(this.lmsRewardRemoveBtn);
            this.rewardPanel.Controls.Add(this.lmsRewardLBox);
            this.rewardPanel.Location = new System.Drawing.Point(3, 3);
            this.rewardPanel.Name = "rewardPanel";
            this.rewardPanel.Size = new System.Drawing.Size(222, 228);
            this.rewardPanel.TabIndex = 32;
            // 
            // lmsRewardRankBox
            // 
            this.lmsRewardRankBox.Location = new System.Drawing.Point(86, 182);
            this.lmsRewardRankBox.Name = "lmsRewardRankBox";
            this.lmsRewardRankBox.Size = new System.Drawing.Size(42, 20);
            this.lmsRewardRankBox.TabIndex = 39;
            this.lmsRewardRankBox.TextChanged += new System.EventHandler(this.lmsRewardRankBox_TextChanged);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(50, 185);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(33, 13);
            this.label59.TabIndex = 38;
            this.label59.Text = "Rank";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(137, 159);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(81, 26);
            this.label58.TabIndex = 37;
            this.label58.Text = "%\r\n(10000 = 100%)";
            // 
            // lmsRewardRateBox
            // 
            this.lmsRewardRateBox.Location = new System.Drawing.Point(86, 156);
            this.lmsRewardRateBox.Name = "lmsRewardRateBox";
            this.lmsRewardRateBox.Size = new System.Drawing.Size(42, 20);
            this.lmsRewardRateBox.TabIndex = 36;
            this.lmsRewardRateBox.TextChanged += new System.EventHandler(this.lmsRewardRateBox_TextChanged);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(50, 159);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(30, 13);
            this.label57.TabIndex = 35;
            this.label57.Text = "Rate";
            // 
            // lmsRewardClassCbox
            // 
            this.lmsRewardClassCbox.Enabled = false;
            this.lmsRewardClassCbox.FormattingEnabled = true;
            this.lmsRewardClassCbox.Location = new System.Drawing.Point(86, 102);
            this.lmsRewardClassCbox.Name = "lmsRewardClassCbox";
            this.lmsRewardClassCbox.Size = new System.Drawing.Size(121, 21);
            this.lmsRewardClassCbox.TabIndex = 34;
            this.lmsRewardClassCbox.SelectedIndexChanged += new System.EventHandler(this.lmsRewardClassCbox_SelectedIndexChanged);
            // 
            // lmsRewardGroupTextBox
            // 
            this.lmsRewardGroupTextBox.Location = new System.Drawing.Point(86, 130);
            this.lmsRewardGroupTextBox.Name = "lmsRewardGroupTextBox";
            this.lmsRewardGroupTextBox.Size = new System.Drawing.Size(100, 20);
            this.lmsRewardGroupTextBox.TabIndex = 33;
            this.lmsRewardGroupTextBox.TextChanged += new System.EventHandler(this.lmsRewardGroupTextBox_TextChanged);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(4, 133);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(76, 13);
            this.label55.TabIndex = 32;
            this.label55.Text = "Reward Group";
            // 
            // lmsRewardClassSpecific
            // 
            this.lmsRewardClassSpecific.AutoSize = true;
            this.lmsRewardClassSpecific.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lmsRewardClassSpecific.Location = new System.Drawing.Point(28, 104);
            this.lmsRewardClassSpecific.Name = "lmsRewardClassSpecific";
            this.lmsRewardClassSpecific.Size = new System.Drawing.Size(51, 17);
            this.lmsRewardClassSpecific.TabIndex = 31;
            this.lmsRewardClassSpecific.Text = "Class";
            this.lmsRewardClassSpecific.UseVisualStyleBackColor = true;
            this.lmsRewardClassSpecific.CheckedChanged += new System.EventHandler(this.lmsRewardClassSpecific_CheckedChanged);
            // 
            // lmsRewardAddBtn
            // 
            this.lmsRewardAddBtn.Location = new System.Drawing.Point(144, 201);
            this.lmsRewardAddBtn.Name = "lmsRewardAddBtn";
            this.lmsRewardAddBtn.Size = new System.Drawing.Size(75, 23);
            this.lmsRewardAddBtn.TabIndex = 30;
            this.lmsRewardAddBtn.Text = "Add";
            this.lmsRewardAddBtn.UseVisualStyleBackColor = true;
            this.lmsRewardAddBtn.Click += new System.EventHandler(this.lmsRewardAddBtn_Click);
            // 
            // lmsRewardRemoveBtn
            // 
            this.lmsRewardRemoveBtn.Location = new System.Drawing.Point(4, 201);
            this.lmsRewardRemoveBtn.Name = "lmsRewardRemoveBtn";
            this.lmsRewardRemoveBtn.Size = new System.Drawing.Size(67, 23);
            this.lmsRewardRemoveBtn.TabIndex = 1;
            this.lmsRewardRemoveBtn.Text = "Remove";
            this.lmsRewardRemoveBtn.UseVisualStyleBackColor = true;
            this.lmsRewardRemoveBtn.Click += new System.EventHandler(this.lmsRewardRemoveBtn_Click);
            // 
            // lmsRewardLBox
            // 
            this.lmsRewardLBox.FormattingEnabled = true;
            this.lmsRewardLBox.HorizontalScrollbar = true;
            this.lmsRewardLBox.Location = new System.Drawing.Point(4, 3);
            this.lmsRewardLBox.Name = "lmsRewardLBox";
            this.lmsRewardLBox.Size = new System.Drawing.Size(215, 95);
            this.lmsRewardLBox.TabIndex = 0;
            this.lmsRewardLBox.SelectedIndexChanged += new System.EventHandler(this.lmsRewardLBox_SelectedIndexChanged);
            // 
            // rewardDialog
            // 
            this.rewardDialog.Location = new System.Drawing.Point(5, 233);
            this.rewardDialog.Name = "rewardDialog";
            this.rewardDialog.Size = new System.Drawing.Size(100, 25);
            this.rewardDialog.TabIndex = 24;
            this.rewardDialog.Text = "Rewards";
            this.rewardDialog.UseVisualStyleBackColor = true;
            this.rewardDialog.Click += new System.EventHandler(this.rewardDialog_Click);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(77, 13);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(69, 13);
            this.label46.TabIndex = 6;
            this.label46.Text = "Starting Time";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(48, 32);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(26, 13);
            this.label43.TabIndex = 0;
            this.label43.Text = "Day";
            // 
            // lmsDay
            // 
            this.lmsDay.Location = new System.Drawing.Point(80, 29);
            this.lmsDay.Name = "lmsDay";
            this.lmsDay.Size = new System.Drawing.Size(100, 20);
            this.lmsDay.TabIndex = 1;
            this.toolTip1.SetToolTip(this.lmsDay, "0 = Sunday, 1 = Monday... Set to 7 for any day");
            this.lmsDay.TextChanged += new System.EventHandler(this.lmsDay_TextChanged);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(44, 58);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(30, 13);
            this.label44.TabIndex = 2;
            this.label44.Text = "Hour";
            // 
            // lmsRange
            // 
            this.lmsRange.Location = new System.Drawing.Point(114, 133);
            this.lmsRange.Name = "lmsRange";
            this.lmsRange.Size = new System.Drawing.Size(100, 20);
            this.lmsRange.TabIndex = 23;
            this.lmsRange.TextChanged += new System.EventHandler(this.lmsRange_TextChanged);
            // 
            // lmsHour
            // 
            this.lmsHour.Location = new System.Drawing.Point(80, 55);
            this.lmsHour.Name = "lmsHour";
            this.lmsHour.Size = new System.Drawing.Size(100, 20);
            this.lmsHour.TabIndex = 3;
            this.lmsHour.TextChanged += new System.EventHandler(this.lmsHour_TextChanged);
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(11, 136);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(97, 13);
            this.label56.TabIndex = 22;
            this.label56.Text = "Map Border Range";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(35, 84);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(39, 13);
            this.label45.TabIndex = 4;
            this.label45.Text = "Minute";
            // 
            // lmsMinute
            // 
            this.lmsMinute.Location = new System.Drawing.Point(80, 81);
            this.lmsMinute.Name = "lmsMinute";
            this.lmsMinute.Size = new System.Drawing.Size(100, 20);
            this.lmsMinute.TabIndex = 5;
            this.lmsMinute.TextChanged += new System.EventHandler(this.lmsMinute_TextChanged);
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(153, 214);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(27, 13);
            this.label54.TabIndex = 20;
            this.label54.Text = "Max";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(2, 110);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(72, 13);
            this.label47.TabIndex = 7;
            this.label47.Text = "Duration (min)";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(89, 214);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(24, 13);
            this.label53.TabIndex = 19;
            this.label53.Text = "Min";
            // 
            // lmsDuration
            // 
            this.lmsDuration.Location = new System.Drawing.Point(80, 107);
            this.lmsDuration.Name = "lmsDuration";
            this.lmsDuration.Size = new System.Drawing.Size(100, 20);
            this.lmsDuration.TabIndex = 8;
            this.lmsDuration.TextChanged += new System.EventHandler(this.lmsDuration_TextChanged);
            // 
            // lmsMax
            // 
            this.lmsMax.Location = new System.Drawing.Point(186, 211);
            this.lmsMax.Name = "lmsMax";
            this.lmsMax.Size = new System.Drawing.Size(28, 20);
            this.lmsMax.TabIndex = 18;
            this.lmsMax.TextChanged += new System.EventHandler(this.lmsMax_TextChanged);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(16, 162);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(67, 13);
            this.label48.TabIndex = 9;
            this.label48.Text = "Central Point";
            // 
            // lmsMin
            // 
            this.lmsMin.Location = new System.Drawing.Point(119, 211);
            this.lmsMin.Name = "lmsMin";
            this.lmsMin.Size = new System.Drawing.Size(28, 20);
            this.lmsMin.TabIndex = 17;
            this.lmsMin.TextChanged += new System.EventHandler(this.lmsMin_TextChanged);
            // 
            // lmsX
            // 
            this.lmsX.Location = new System.Drawing.Point(106, 159);
            this.lmsX.Name = "lmsX";
            this.lmsX.Size = new System.Drawing.Size(28, 20);
            this.lmsX.TabIndex = 10;
            this.lmsX.TextChanged += new System.EventHandler(this.lmsX_TextChanged);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(4, 214);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(79, 13);
            this.label52.TabIndex = 16;
            this.label52.Text = "Required Level";
            // 
            // lmsY
            // 
            this.lmsY.Location = new System.Drawing.Point(161, 159);
            this.lmsY.Name = "lmsY";
            this.lmsY.Size = new System.Drawing.Size(28, 20);
            this.lmsY.TabIndex = 11;
            this.lmsY.TextChanged += new System.EventHandler(this.lmsY_TextChanged);
            // 
            // lmsSize
            // 
            this.lmsSize.Location = new System.Drawing.Point(89, 185);
            this.lmsSize.Name = "lmsSize";
            this.lmsSize.Size = new System.Drawing.Size(100, 20);
            this.lmsSize.TabIndex = 15;
            this.lmsSize.TextChanged += new System.EventHandler(this.lmsSize_TextChanged);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(86, 162);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(14, 13);
            this.label49.TabIndex = 12;
            this.label49.Text = "X";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(29, 188);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(54, 13);
            this.label51.TabIndex = 14;
            this.label51.Text = "Initial Size";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(142, 162);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(14, 13);
            this.label50.TabIndex = 13;
            this.label50.Text = "Y";
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(126, 33);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveButton.TabIndex = 6;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(12, 33);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 5;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // MapInfoListBox
            // 
            this.MapInfoListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MapInfoListBox.FormattingEnabled = true;
            this.MapInfoListBox.Location = new System.Drawing.Point(12, 62);
            this.MapInfoListBox.Name = "MapInfoListBox";
            this.MapInfoListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.MapInfoListBox.Size = new System.Drawing.Size(189, 290);
            this.MapInfoListBox.TabIndex = 7;
            this.MapInfoListBox.SelectedIndexChanged += new System.EventHandler(this.MapInfoListBox_SelectedIndexChanged);
            // 
            // PasteMapButton
            // 
            this.PasteMapButton.Location = new System.Drawing.Point(288, 33);
            this.PasteMapButton.Name = "PasteMapButton";
            this.PasteMapButton.Size = new System.Drawing.Size(75, 23);
            this.PasteMapButton.TabIndex = 24;
            this.PasteMapButton.Text = "Paste";
            this.PasteMapButton.UseVisualStyleBackColor = true;
            this.PasteMapButton.Click += new System.EventHandler(this.PasteMapButton_Click);
            // 
            // CopyMapButton
            // 
            this.CopyMapButton.Location = new System.Drawing.Point(207, 33);
            this.CopyMapButton.Name = "CopyMapButton";
            this.CopyMapButton.Size = new System.Drawing.Size(75, 23);
            this.CopyMapButton.TabIndex = 23;
            this.CopyMapButton.Text = "Copy";
            this.CopyMapButton.UseVisualStyleBackColor = true;
            // 
            // ImportMapInfoButton
            // 
            this.ImportMapInfoButton.Location = new System.Drawing.Point(564, 4);
            this.ImportMapInfoButton.Name = "ImportMapInfoButton";
            this.ImportMapInfoButton.Size = new System.Drawing.Size(87, 23);
            this.ImportMapInfoButton.TabIndex = 25;
            this.ImportMapInfoButton.Text = "Import MapInfo";
            this.ImportMapInfoButton.UseVisualStyleBackColor = true;
            this.ImportMapInfoButton.Click += new System.EventHandler(this.ImportMapInfoButton_Click);
            // 
            // ExportMapInfoButton
            // 
            this.ExportMapInfoButton.Location = new System.Drawing.Point(564, 33);
            this.ExportMapInfoButton.Name = "ExportMapInfoButton";
            this.ExportMapInfoButton.Size = new System.Drawing.Size(87, 23);
            this.ExportMapInfoButton.TabIndex = 26;
            this.ExportMapInfoButton.Text = "Export MapInfo";
            this.ExportMapInfoButton.UseVisualStyleBackColor = true;
            this.ExportMapInfoButton.Click += new System.EventHandler(this.ExportMapInfoButton_Click);
            // 
            // ImportMongenButton
            // 
            this.ImportMongenButton.Location = new System.Drawing.Point(657, 3);
            this.ImportMongenButton.Name = "ImportMongenButton";
            this.ImportMongenButton.Size = new System.Drawing.Size(86, 23);
            this.ImportMongenButton.TabIndex = 27;
            this.ImportMongenButton.Text = "Import Spawns";
            this.ImportMongenButton.UseVisualStyleBackColor = true;
            this.ImportMongenButton.Click += new System.EventHandler(this.ImportMonGenButton_Click);
            // 
            // ExportMongenButton
            // 
            this.ExportMongenButton.Location = new System.Drawing.Point(657, 33);
            this.ExportMongenButton.Name = "ExportMongenButton";
            this.ExportMongenButton.Size = new System.Drawing.Size(86, 23);
            this.ExportMongenButton.TabIndex = 28;
            this.ExportMongenButton.Text = "Export Spawns";
            this.ExportMongenButton.UseVisualStyleBackColor = true;
            this.ExportMongenButton.Click += new System.EventHandler(this.ExportMonGenButton_Click);
            // 
            // VisualizerButton
            // 
            this.VisualizerButton.Location = new System.Drawing.Point(483, 4);
            this.VisualizerButton.Name = "VisualizerButton";
            this.VisualizerButton.Size = new System.Drawing.Size(75, 23);
            this.VisualizerButton.TabIndex = 31;
            this.VisualizerButton.Text = "Visualizer";
            this.VisualizerButton.UseVisualStyleBackColor = true;
            this.VisualizerButton.Click += new System.EventHandler(this.VisualizerButton_Click);
            // 
            // itemSelect
            // 
            this.itemSelect.Controls.Add(this.button2);
            this.itemSelect.Controls.Add(this.selectBtn);
            this.itemSelect.Controls.Add(this.label61);
            this.itemSelect.Controls.Add(this.label62);
            this.itemSelect.Controls.Add(this.amountBox);
            this.itemSelect.Controls.Add(this.itemsCBox);
            this.itemSelect.Location = new System.Drawing.Point(227, 132);
            this.itemSelect.Name = "itemSelect";
            this.itemSelect.Size = new System.Drawing.Size(270, 65);
            this.itemSelect.TabIndex = 32;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(187, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // selectBtn
            // 
            this.selectBtn.Location = new System.Drawing.Point(187, 11);
            this.selectBtn.Name = "selectBtn";
            this.selectBtn.Size = new System.Drawing.Size(75, 23);
            this.selectBtn.TabIndex = 5;
            this.selectBtn.Text = "Select Item";
            this.selectBtn.UseVisualStyleBackColor = true;
            this.selectBtn.Click += new System.EventHandler(this.selectBtn_Click);
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(4, 41);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(49, 13);
            this.label61.TabIndex = 4;
            this.label61.Text = "Amount :";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(20, 14);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(33, 13);
            this.label62.TabIndex = 3;
            this.label62.Text = "Item :";
            // 
            // amountBox
            // 
            this.amountBox.Location = new System.Drawing.Point(60, 38);
            this.amountBox.Name = "amountBox";
            this.amountBox.Size = new System.Drawing.Size(121, 20);
            this.amountBox.TabIndex = 1;
            this.amountBox.TextChanged += new System.EventHandler(this.amountBox_TextChanged);
            // 
            // itemsCBox
            // 
            this.itemsCBox.FormattingEnabled = true;
            this.itemsCBox.Location = new System.Drawing.Point(60, 11);
            this.itemsCBox.Name = "itemsCBox";
            this.itemsCBox.Size = new System.Drawing.Size(121, 21);
            this.itemsCBox.TabIndex = 0;
            this.itemsCBox.SelectedIndexChanged += new System.EventHandler(this.itemsCBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(80, 201);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 23);
            this.button1.TabIndex = 40;
            this.button1.Text = "Edit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MapInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 363);
            this.Controls.Add(this.itemSelect);
            this.Controls.Add(this.VisualizerButton);
            this.Controls.Add(this.ExportMongenButton);
            this.Controls.Add(this.ImportMongenButton);
            this.Controls.Add(this.ExportMapInfoButton);
            this.Controls.Add(this.ImportMapInfoButton);
            this.Controls.Add(this.PasteMapButton);
            this.Controls.Add(this.CopyMapButton);
            this.Controls.Add(this.MapTabs);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.MapInfoListBox);
            this.Name = "MapInfoForm";
            this.Text = "Map Info";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MapInfoForm_FormClosed);
            this.MapTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.SafeZoneInfoPanel.ResumeLayout(false);
            this.SafeZoneInfoPanel.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.RespawnInfoPanel.ResumeLayout(false);
            this.RespawnInfoPanel.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.MovementInfoPanel.ResumeLayout(false);
            this.MovementInfoPanel.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.MineZonepanel.ResumeLayout(false);
            this.MineZonepanel.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.panelEventRespawn.ResumeLayout(false);
            this.panelEventRespawn.PerformLayout();
            this.panelEventInfo.ResumeLayout(false);
            this.panelEventInfo.PerformLayout();
            this.lms_BRTab.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.lmsPanel.ResumeLayout(false);
            this.lmsPanel.PerformLayout();
            this.rewardPanel.ResumeLayout(false);
            this.rewardPanel.PerformLayout();
            this.itemSelect.ResumeLayout(false);
            this.itemSelect.PerformLayout();
            this.ResumeLayout(false);
            this.itemSelect.Hide();
            this.rewardPanel.Hide();
        }

        #endregion

        private TabControl MapTabs;
        private TabPage tabPage3;
        private Button RemoveSZButton;
        private Button AddSZButton;
        private Panel SafeZoneInfoPanel;
        private Label label12;
        private TextBox SZYTextBox;
        private Label label14;
        private TextBox SizeTextBox;
        private Label label17;
        private TextBox SZXTextBox;
        private CheckBox StartPointCheckBox;
        private ListBox SafeZoneInfoListBox;
        private TabPage tabPage2;
        private Button RPasteButton;
        private Button RCopyButton;
        private Button RemoveRButton;
        private Button AddRButton;
        private ListBox RespawnInfoListBox;
        private Panel RespawnInfoPanel;
        private Label label24;
        private TextBox DirectionTextBox;
        private Label label8;
        private TextBox DelayTextBox;
        private Label label7;
        private ComboBox MonsterInfoComboBox;
        private Label label6;
        private TextBox SpreadTextBox;
        private Label label9;
        private TextBox RYTextBox;
        private Label label10;
        private TextBox CountTextBox;
        private Label label13;
        private TextBox RXTextBox;
        private TabPage tabPage4;
        private Button RemoveMButton;
        private Button AddMButton;
        private Panel MovementInfoPanel;
        private Label label22;
        private ComboBox DestMapComboBox;
        private Label label18;
        private TextBox DestYTextBox;
        private Label label21;
        private TextBox DestXTextBox;
        private Label label16;
        private TextBox SourceYTextBox;
        private Label label20;
        private TextBox SourceXTextBox;
        private ListBox MovementInfoListBox;
        private Button RemoveButton;
        private Button AddButton;
        private ListBox MapInfoListBox;
        private Button PasteMapButton;
        private Button CopyMapButton;
        private TabPage tabPage1;
        private Label label15;
        private TextBox BigMapTextBox;
        private ComboBox LightsComboBox;
        private Label label5;
        private Label label1;
        private Label label4;
        private TextBox MapIndexTextBox;
        private TextBox MiniMapTextBox;
        private Label label2;
        private TextBox MapNameTextBox;
        private TextBox FileNameTextBox;
        private Label label3;
        private TabPage tabPage6;
        private CheckBox LightningCheckbox;
        private CheckBox FireCheckbox;
        private CheckBox FightCheckbox;
        private CheckBox NoReconnectCheckbox;
        private CheckBox NoTeleportCheckbox;
        private TextBox LightningTextbox;
        private TextBox FireTextbox;
        private TextBox NoReconnectTextbox;
        private CheckBox NoNamesCheckbox;
        private CheckBox NoDropMonsterCheckbox;
        private CheckBox NoDropPlayerCheckbox;
        private CheckBox NoThrowItemCheckbox;
        private CheckBox NoPositionCheckbox;
        private CheckBox NoDrugCheckbox;
        private CheckBox NoRecallCheckbox;
        private CheckBox NoEscapeCheckbox;
        private CheckBox NoRandomCheckbox;
        private CheckBox NeedHoleMCheckBox;
        private Button ImportMapInfoButton;
        private Button ExportMapInfoButton;
        private Label label19;
        private TextBox MapDarkLighttextBox;
        private TabPage tabPage7;
        private Button MZDeletebutton;
        private Button MZAddbutton;
        private Panel MineZonepanel;
        private Label label30;
        private TextBox MZYtextBox;
        private Label label31;
        private TextBox MZSizetextBox;
        private Label label32;
        private TextBox MZXtextBox;
        private ListBox MZListlistBox;
        private ComboBox MineComboBox;
        private ComboBox MineZoneComboBox;
        private Label label27;
        private Label label33;
        private Button ImportMongenButton;
        private Button ExportMongenButton;
        private Button VisualizerButton;
        private CheckBox NeedBridleCheckbox;
        private CheckBox NoMountCheckbox;
        private Label label34;
        private TextBox RoutePathTextBox;
        private CheckBox NoFightCheckbox;
        private CheckBox NeedMoveMCheckBox;
        private Label label11;
        private TextBox MusicTextBox;
        private Label label23;
        private TextBox Randomtextbox;
        private ToolTip toolTip1;
        private CheckBox chkrespawnsave;
        private CheckBox chkRespawnEnableTick;
        private ComboBox ConquestComboBox;
        private Label label25;
        private CheckBox NoHeroCheckBox;
        private CheckBox GTBox;
        private CheckBox SafeZoneCheckbox;
        private TabPage tabPage5;
        private Panel panelEventRespawn;
        private TextBox txtSpreadX;
        private Label label42;
        private CheckBox chkIsRespawnObjective;
        private TextBox txtSpreadY;
        private Label label41;
        private TextBox txtOrder;
        private Label label40;
        private Label label28;
        private TextBox txtEventRespawnMonCount;
        private Label label35;
        private TextBox txtEventRespawnMonName;
        private Button BtnRemoveEventRespawn;
        private Button btnAddEventRespawn;
        private ListBox lstEventRespawns;
        private Panel panelEventInfo;
        private CheckBox chkIsSafeZone;
        private ComboBox ddlEventType;
        private Label label39;
        private Label label38;
        private TextBox txtCooldownInMins;
        private Label label37;
        private TextBox txtEventSize;
        private Label label29;
        private TextBox txtEventName;
        private Label label36;
        private TextBox txtEventMultipleCoords;
        private Button btnRemoveEvent;
        private Button btnAddEvent;
        private ListBox lstEventInfos;
        private Label label26;
        private TextBox txtObjectiveMsg;
        private CheckBox noGroup;
        private CheckBox noRes;
        private TabPage lms_BRTab;
        private TextBox lmsDay;
        private Label label43;
        private TextBox lmsMinute;
        private Label label45;
        private TextBox lmsHour;
        private Label label44;
        private Label label46;
        private TextBox lmsDuration;
        private Label label47;
        private Label label50;
        private Label label49;
        private TextBox lmsY;
        private TextBox lmsX;
        private Label label48;
        private TextBox lmsSize;
        private Label label51;
        private TextBox lmsMax;
        private TextBox lmsMin;
        private Label label52;
        private Label label54;
        private Label label53;
        private TextBox lmsRange;
        private Label label56;
        private Panel lmsPanel;
        private Panel panel2;
        private Button removeLMSBtn;
        private Button addLMSBtn;
        private ListBox lmsListBox;
        private Button rewardDialog;
        private Panel rewardPanel;
        private Button lmsRewardRemoveBtn;
        private ListBox lmsRewardLBox;
        private Button lmsRewardAddBtn;
        private CheckBox lmsRewardClassSpecific;
        private TextBox lmsRewardGroupTextBox;
        private Label label55;
        private ComboBox lmsRewardClassCbox;
        private TextBox lmsRewardRateBox;
        private Label label57;
        private Label label58;
        private TextBox lmsRewardRankBox;
        private Label label59;
        private Panel itemSelect;
        private Button button2;
        private Button selectBtn;
        private Label label61;
        private Label label62;
        private TextBox amountBox;
        private ComboBox itemsCBox;
        private Button button1;
    }
}