using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace MirDataTool
{
    public partial class MonsterToolPanel : UserControl
    {
        MirDataTool MotherParent;
        public void SetChild(MirDataTool parent)
        {
            MotherParent = parent;
        }

        public List<MonsterInfo> MonsterInfoList = new List<MonsterInfo>();
        public List<MonsterInfo> _SelectedItems = new List<MonsterInfo>();

        public MonsterToolPanel()
        {
            InitializeComponent();
        }



        private void UpdateInterface()
        {
            if (MonsterInfoListBox.Items.Count != MonsterInfoList.Count)
            {
                MonsterInfoListBox.Items.Clear();

                for (int i = 0; i < MonsterInfoList.Count; i++)
                    MonsterInfoListBox.Items.Add(MonsterInfoList[i]);
            }

            _SelectedItems = MonsterInfoListBox.SelectedItems.Cast<MonsterInfo>().ToList();

            if (_SelectedItems.Count == 0)
            {
                MonsterInfoPanel.Enabled = false;
                MonsterIndexTextBox.Text = string.Empty;
                MonsterNameTextBox.Text = string.Empty;

                ImageComboBox.SelectedItem = null;
                LightColorComboBox.SelectedItem = null;
                AITextBox.Text = string.Empty;
                EffectTextBox.Text = string.Empty;
                LevelTextBox.Text = string.Empty;
                ViewRangeTextBox.Text = string.Empty;
                CoolEyeTextBox.Text = string.Empty;

                HPTextBox.Text = string.Empty;
                ExperienceTextBox.Text = string.Empty;
                RandomQuestBox.Text = string.Empty;

                MinACTextBox.Text = string.Empty;
                MaxACTextBox.Text = string.Empty;
                MinMACTextBox.Text = string.Empty;
                MaxMACTextBox.Text = string.Empty;
                MinDCTextBox.Text = string.Empty;
                MaxDCTextBox.Text = string.Empty;
                MinMCTextBox.Text = string.Empty;
                MaxMCTextBox.Text = string.Empty;
                MinSCTextBox.Text = string.Empty;
                MaxSCTextBox.Text = string.Empty;
                AccuracyTextBox.Text = string.Empty;
                AgilityTextBox.Text = string.Empty;
                LightTextBox.Text = string.Empty;

                ASpeedTextBox.Text = string.Empty;
                MSpeedTextBox.Text = string.Empty;

                CanPushCheckBox.Checked = false;
                CanTameCheckBox.Checked = false;
                AutoRevCheckBox.Checked = false;
                UndeadCheckBox.Checked = false;

                IgnoreBox.Checked = false;
                TeleportBackBox.Checked = false;
                bossBox.Checked = false;
                eliteBox.Checked = false;
                checkBox1.Checked = false;
                return;
            }

            MonsterInfo info = _SelectedItems[0];

            MonsterInfoPanel.Enabled = true;

            MonsterIndexTextBox.Text = info.Index.ToString();
            MonsterNameTextBox.Text = info.Name;

            ImageComboBox.SelectedItem = null;
            LightColorComboBox.SelectedItem = null;
            ImageComboBox.SelectedItem = info.Image;
            LightColorComboBox.SelectedItem = (Color.FromName(info.LightColar)).ToKnownColor();
            AITextBox.Text = info.AI.ToString();
            LightEffectBox.Text = info.LightEffect.ToString();
            EffectTextBox.Text = info.Effect.ToString();
            LevelTextBox.Text = info.Level.ToString();
            ViewRangeTextBox.Text = info.ViewRange.ToString();
            CoolEyeTextBox.Text = info.CoolEye.ToString();
            RandomQuestChanceBox.Text = info.RandomQuestChance.ToString();

            HPTextBox.Text = info.HP.ToString();
            ExperienceTextBox.Text = info.Experience.ToString();

            RandomQuestBox.Text = string.Empty;
            foreach (var q in info.RandomQuest)
                RandomQuestBox.Text += q + Environment.NewLine;

            MinACTextBox.Text = info.MinAC.ToString();
            MaxACTextBox.Text = info.MaxAC.ToString();
            MinMACTextBox.Text = info.MinMAC.ToString();
            MaxMACTextBox.Text = info.MaxMAC.ToString();
            MinDCTextBox.Text = info.MinDC.ToString();
            MaxDCTextBox.Text = info.MaxDC.ToString();
            MinMCTextBox.Text = info.MinMC.ToString();
            MaxMCTextBox.Text = info.MaxMC.ToString();
            MinSCTextBox.Text = info.MinSC.ToString();
            MaxSCTextBox.Text = info.MaxSC.ToString();
            AccuracyTextBox.Text = info.Accuracy.ToString();
            AgilityTextBox.Text = info.Agility.ToString();
            LightTextBox.Text = info.Light.ToString();

            ASpeedTextBox.Text = info.AttackSpeed.ToString();
            MSpeedTextBox.Text = info.MoveSpeed.ToString();


            CanPushCheckBox.Checked = info.CanPush;
            CanTameCheckBox.Checked = info.CanTame;
            AutoRevCheckBox.Checked = info.AutoRev;
            UndeadCheckBox.Checked = info.Undead;

            IgnoreBox.Checked = info.Ignore;
            TeleportBackBox.Checked = info.TeleportBack;
            bossBox.Checked = info.IsBoss;
            eliteBox.Checked = info.CanBeElite;
            checkBox1.Checked = info.IsSub;
            for (int i = 1; i < _SelectedItems.Count; i++)
            {
                info = _SelectedItems[i];

                if (MonsterIndexTextBox.Text != info.Index.ToString()) MonsterIndexTextBox.Text = string.Empty;
                if (MonsterNameTextBox.Text != info.Name) MonsterNameTextBox.Text = string.Empty;

                if (ImageComboBox.SelectedItem == null || (Monster)ImageComboBox.SelectedItem != info.Image) ImageComboBox.SelectedItem = null;
                if (LightColorComboBox.SelectedItem == null || LightColorComboBox.SelectedItem.ToString() != info.LightColar) LightColorComboBox.SelectedItem = null;
                if (AITextBox.Text != info.AI.ToString()) AITextBox.Text = string.Empty;
                if (LightEffectBox.Text != info.LightEffect.ToString()) LightEffectBox.Text = string.Empty;
                if (EffectTextBox.Text != info.Effect.ToString()) EffectTextBox.Text = string.Empty;
                if (LevelTextBox.Text != info.Level.ToString()) LevelTextBox.Text = string.Empty;
                if (ViewRangeTextBox.Text != info.ViewRange.ToString()) ViewRangeTextBox.Text = string.Empty;
                if (CoolEyeTextBox.Text != info.CoolEye.ToString()) CoolEyeTextBox.Text = string.Empty;
                if (HPTextBox.Text != info.HP.ToString()) HPTextBox.Text = string.Empty;
                if (ExperienceTextBox.Text != info.Experience.ToString()) ExperienceTextBox.Text = string.Empty;
                if (RandomQuestChanceBox.Text != info.RandomQuestChance.ToString()) RandomQuestChanceBox.Text = string.Empty;

                if (MinACTextBox.Text != info.MinAC.ToString()) MinACTextBox.Text = string.Empty;
                if (MaxACTextBox.Text != info.MaxAC.ToString()) MaxACTextBox.Text = string.Empty;
                if (MinMACTextBox.Text != info.MinMAC.ToString()) MinMACTextBox.Text = string.Empty;
                if (MaxMACTextBox.Text != info.MaxMAC.ToString()) MaxMACTextBox.Text = string.Empty;
                if (MinDCTextBox.Text != info.MinDC.ToString()) MinDCTextBox.Text = string.Empty;
                if (MaxDCTextBox.Text != info.MaxDC.ToString()) MaxDCTextBox.Text = string.Empty;
                if (MinMCTextBox.Text != info.MinMC.ToString()) MinMCTextBox.Text = string.Empty;
                if (MaxMCTextBox.Text != info.MaxMC.ToString()) MaxMCTextBox.Text = string.Empty;
                if (MinSCTextBox.Text != info.MinSC.ToString()) MinSCTextBox.Text = string.Empty;
                if (MaxSCTextBox.Text != info.MaxSC.ToString()) MaxSCTextBox.Text = string.Empty;
                if (AccuracyTextBox.Text != info.Accuracy.ToString()) AccuracyTextBox.Text = string.Empty;
                if (AgilityTextBox.Text != info.Agility.ToString()) AgilityTextBox.Text = string.Empty;
                if (LightTextBox.Text != info.Light.ToString()) LightTextBox.Text = string.Empty;
                if (ASpeedTextBox.Text != info.AttackSpeed.ToString()) ASpeedTextBox.Text = string.Empty;
                if (MSpeedTextBox.Text != info.MoveSpeed.ToString()) MSpeedTextBox.Text = string.Empty;

                if (CanPushCheckBox.Checked != info.CanPush) CanPushCheckBox.CheckState = CheckState.Indeterminate;
                if (CanTameCheckBox.Checked != info.CanTame) CanTameCheckBox.CheckState = CheckState.Indeterminate;

                if (AutoRevCheckBox.Checked != info.AutoRev) AutoRevCheckBox.CheckState = CheckState.Indeterminate;
                if (UndeadCheckBox.Checked != info.Undead) UndeadCheckBox.CheckState = CheckState.Indeterminate;

                if (IgnoreBox.Checked != info.Ignore) IgnoreBox.CheckState = CheckState.Indeterminate;
                if (TeleportBackBox.Checked != info.TeleportBack) TeleportBackBox.CheckState = CheckState.Indeterminate;
                if (bossBox.Checked != info.IsBoss)
                    bossBox.CheckState = CheckState.Indeterminate;
                if (eliteBox.Checked != info.CanBeElite)
                    eliteBox.CheckState = CheckState.Indeterminate;
                if (checkBox1.Checked != info.IsSub)
                    checkBox1.CheckState = CheckState.Indeterminate;
            }

        }
        public void UpdateList()
        {
            MonsterInfoListBox.SelectedIndexChanged -= MonsterInfoListBox_SelectedIndexChanged;

            List<bool> selected = new List<bool>();

            for (int i = 0; i < MonsterInfoListBox.Items.Count; i++) selected.Add(MonsterInfoListBox.GetSelected(i));
            MonsterInfoListBox.Items.Clear();
            for (int i = 0; i < MonsterInfoList.Count; i++) MonsterInfoListBox.Items.Add(MonsterInfoList[i]);
            for (int i = 0; i < selected.Count; i++) MonsterInfoListBox.SetSelected(i, selected[i]);

            MonsterInfoListBox.SelectedIndexChanged += MonsterInfoListBox_SelectedIndexChanged;
        }
        private void MonsterInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInterface();
        }
        private void MonsterNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Name = ActiveControl.Text;

            UpdateList();
            MotherParent.NeedSave = true;
        }
        private void AITextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].AI = temp;
            MotherParent.NeedSave = true;
        }
        private void EffectTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Effect = temp;
            MotherParent.NeedSave = true;
        }
        private void LevelTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Level = temp;
            MotherParent.NeedSave = true;
        }
        private void LightTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Light = temp;
            MotherParent.NeedSave = true;
        }
        private void ViewRangeTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].ViewRange = temp;
            MotherParent.NeedSave = true;
        }
        private void HPTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!uint.TryParse(ActiveControl.Text, out uint temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HP = temp;
            MotherParent.NeedSave = true;
        }
        private void ExperienceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!uint.TryParse(ActiveControl.Text, out uint temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Experience = temp;
            MotherParent.NeedSave = true;
        }
        private void MinACTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinAC = temp;
            MotherParent.NeedSave = true;
        }
        private void MaxACTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxAC = temp;
            MotherParent.NeedSave = true;
        }
        private void MinMACTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinMAC = temp;
            MotherParent.NeedSave = true;
        }
        private void MaxMACTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxMAC = temp;
            MotherParent.NeedSave = true;
        }
        private void MinDCTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinDC = temp;
            MotherParent.NeedSave = true;
        }
        private void MaxDCTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxDC = temp;
            MotherParent.NeedSave = true;
        }
        private void MinMCTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinMC = temp;
            MotherParent.NeedSave = true;
        }
        private void MaxMCTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxMC = temp;
            MotherParent.NeedSave = true;
        }
        private void MinSCTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinSC = temp;
            MotherParent.NeedSave = true;
        }
        private void MaxSCTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxSC = temp;
            MotherParent.NeedSave = true;
        }
        private void AccuracyTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Accuracy = temp;
            MotherParent.NeedSave = true;
        }
        private void AgilityTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Agility = temp;
            MotherParent.NeedSave = true;
        }
        private void ASpeedTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].AttackSpeed = temp;
            MotherParent.NeedSave = true;
        }
        private void MSpeedTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MoveSpeed = temp;
            MotherParent.NeedSave = true;
        }
        private void CanPushCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].CanPush = CanPushCheckBox.Checked;
            MotherParent.NeedSave = true;
        }
        private void CanTameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].CanTame = CanTameCheckBox.Checked;
            MotherParent.NeedSave = true;
        }
        private void AutoRevCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].AutoRev = AutoRevCheckBox.Checked;
            MotherParent.NeedSave = true;
        }

        private void UndeadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Undead = UndeadCheckBox.Checked;
            MotherParent.NeedSave = true;
        }
        
        private void CoolEyeTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].CoolEye = temp;
            MotherParent.NeedSave = true;
        }

        private void ImageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Image = (Monster)ImageComboBox.SelectedItem;
            MotherParent.NeedSave = true;
        }

        private void IgnoreBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Ignore = IgnoreBox.Checked;
            MotherParent.NeedSave = true;
        }

        private void TeleportBackBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].TeleportBack = TeleportBackBox.Checked;
            MotherParent.NeedSave = true;
        }

        private void bossBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].IsBoss = bossBox.Checked;
            MotherParent.NeedSave = true;
        }

        private void eliteBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].CanBeElite = eliteBox.Checked;
            MotherParent.NeedSave = true;
        }

        private void LightColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
            {
                _SelectedItems[i].LightColar = LightColorComboBox.SelectedItem?.ToString();

            }
            MotherParent.NeedSave = true;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            string input = Interaction.InputBox("Input quest index", "Add Random Event", "", -1, -1);


            if (int.TryParse(input, out int nr))
            {
                _SelectedItems[0].RandomQuest.Add(nr);
            }

            RefreshRandomQuestList();
            MotherParent.NeedSave = true;
        }

        public void RefreshRandomQuestList()
        {
            RandomQuestBox.Text = string.Empty;

            foreach (var v in _SelectedItems[0].RandomQuest)
            {
                RandomQuestBox.Text += v.ToString() + Environment.NewLine;
            }
            MotherParent.NeedSave = true;
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Input quest index", "Remove Random Event", "", -1, -1);


            if (int.TryParse(input, out int nr))
            {
                _SelectedItems[0].RandomQuest.Remove(nr);
            }

            RefreshRandomQuestList();
            MotherParent.NeedSave = true;
        }

        private void RandomQuestChanceBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].RandomQuestChance = temp;
            MotherParent.NeedSave = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].LightEffect = temp;
            MotherParent.NeedSave = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].IsSub = checkBox1.Checked;
            MotherParent.NeedSave = true;
        }

        private void DropBuilderButton_Click(object sender, EventArgs e)
        {

        }

        private void ExportSelectedButton_Click(object sender, EventArgs e)
        {

        }

        private void ImportButton_Click(object sender, EventArgs e)
        {

        }

        private void ExportButton_Click(object sender, EventArgs e)
        {

        }

        private void PasteMButton_Click(object sender, EventArgs e)
        {

        }

        private void CopyMButton_Click(object sender, EventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (MotherParent != null)
            {
                MonsterInfo newMob = MotherParent.AddMonster();
                _SelectedItems.Clear();
                _SelectedItems.Add(newMob);
                UpdateList();
                MotherParent.NeedSave = true;
            }
        }    

        private void RandomQuestBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
