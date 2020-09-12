using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Server.MirDatabase;
using Server.MirEnvir;
using Server.MirObjects;
using Microsoft.VisualBasic;

namespace Server
{
    public partial class MonsterInfoForm : Form
    {
        public string MonsterListPath = Path.Combine(Settings.ExportPath, "MonsterList.txt");

        public Envir Envir
        {
            get { return SMain.EditEnvir; }
        }

        private List<MonsterInfo> _selectedMonsterInfos;

        public MonsterInfoForm()
        {
            InitializeComponent();

            ImageComboBox.Items.AddRange(Enum.GetValues(typeof(Monster)).Cast<object>().ToArray());

            LightColorComboBox.Items.AddRange(Enum.GetValues(typeof(KnownColor)).Cast<object>().ToArray());
            //debuffCBox.Items.AddRange(Enum.GetValues(typeof(AIDebuffType)).Cast<object>().ToArray());
            //attackStyleCBox.Items.AddRange(Enum.GetValues(typeof(AIAttackStyle)).Cast<object>().ToArray());
            //attackTypeCBox.Items.AddRange(Enum.GetValues(typeof(AIAttackType)).Cast<object>().ToArray());
            UpdateInterface();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Envir.CreateMonsterInfo();
            UpdateInterface();
        }
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (_selectedMonsterInfos.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to remove the selected Monsters?", "Remove Monsters?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++) Envir.Remove(_selectedMonsterInfos[i]);

            UpdateInterface();
        }
        public void ResetMonsterIndex()
        {
            if (Envir.MonsterInfoList != null)
            {
                Envir.MonsterIndex = Envir.MonsterInfoList.Count;
            }
        }
        private void UpdateInterface()
        {
            if (MonsterInfoListBox.Items.Count != Envir.MonsterInfoList.Count)
            {
                MonsterInfoListBox.Items.Clear();

                for (int i = 0; i < Envir.MonsterInfoList.Count; i++)
                    MonsterInfoListBox.Items.Add(Envir.MonsterInfoList[i]);
            }

            _selectedMonsterInfos = MonsterInfoListBox.SelectedItems.Cast<MonsterInfo>().ToList();

            if (_selectedMonsterInfos.Count == 0)
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
                allowBleeding.Checked = true;
                allowBurning.Checked = true;
                allowFreeze.Checked = true;
                allowGreen.Checked = true;
                allowPara.Checked = true;
                allowRed.Checked = true;
                allowSlow.Checked = true;
                
                IgnoreBox.Checked = false;
                TeleportBackBox.Checked = false;
                bossBox.Checked = false;
                eliteBox.Checked = false;
                checkBox1.Checked = false;
                //debuffCBox.SelectedItem = null;

                return;
            }

            MonsterInfo info = _selectedMonsterInfos[0];

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
            allowBleeding.Checked = info.AllowBleeding;
            allowBurning.Checked = info.AllowBurning;
            allowFreeze.Checked = info.AllowFreeze;
            allowGreen.Checked = info.AllowGreen;
            allowPara.Checked = info.AllowPara;
            allowRed.Checked = info.AllowRed;
            allowSlow.Checked = info.AllowSlow;
            IgnoreBox.Checked = info.Ignore;
            TeleportBackBox.Checked = info.TeleportBack;
            bossBox.Checked = info.IsBoss;
            eliteBox.Checked = info.CanBeElite;
            checkBox1.Checked = info.IsSub;
            for (int i = 1; i < _selectedMonsterInfos.Count; i++)
            {
                info = _selectedMonsterInfos[i];

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
                if (allowBleeding.Checked != info.AllowBleeding) allowBleeding.CheckState = CheckState.Indeterminate;
                if (allowBurning.Checked != info.AllowBurning) allowBurning.CheckState = CheckState.Indeterminate;
                if (allowFreeze.Checked != info.AllowFreeze) allowFreeze.CheckState = CheckState.Indeterminate;
                if (allowGreen.Checked != info.AllowGreen) allowGreen.CheckState = CheckState.Indeterminate;
                if (allowPara.Checked != info.AllowPara) allowPara.CheckState = CheckState.Indeterminate;
                if (allowRed.Checked != info.AllowRed) allowRed.CheckState = CheckState.Indeterminate;
                if (allowSlow.Checked != info.AllowSlow) allowSlow.CheckState = CheckState.Indeterminate;
                
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
        private void RefreshMonsterList()
        {
            MonsterInfoListBox.SelectedIndexChanged -= MonsterInfoListBox_SelectedIndexChanged;

            List<bool> selected = new List<bool>();

            for (int i = 0; i < MonsterInfoListBox.Items.Count; i++) selected.Add(MonsterInfoListBox.GetSelected(i));
            MonsterInfoListBox.Items.Clear();
            for (int i = 0; i < Envir.MonsterInfoList.Count; i++) MonsterInfoListBox.Items.Add(Envir.MonsterInfoList[i]);
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

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Name = ActiveControl.Text;

            RefreshMonsterList();
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

            if (temp == 255)
            {
                for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                    if (_selectedMonsterInfos[i].Attacks.Count == 0)
                        _selectedMonsterInfos[i].Attacks.Add(new AIAttack() { AttackStyle = AIAttackStyle.Default, AttackType = AIAttackType.Default, Delay = 800 });
            }
            else
            {
                for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                    if (_selectedMonsterInfos[i].Attacks.Count > 0)
                        _selectedMonsterInfos[i].Attacks.Clear();
            }
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AI = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Effect = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Level = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Light = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].ViewRange = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].HP = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Experience = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].MinAC = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].MaxAC = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].MinMAC = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].MaxMAC = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].MinDC = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].MaxDC = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].MinMC = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].MaxMC = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].MinSC = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].MaxSC = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Accuracy = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Agility = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AttackSpeed = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].MoveSpeed = temp;
        }
        private void CanPushCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].CanPush = CanPushCheckBox.Checked;
        }
        private void CanTameCheckBox_CheckedChanged(object sender, EventArgs e)
        {if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].CanTame = CanTameCheckBox.Checked;
        }
        private void AutoRevCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AutoRev = AutoRevCheckBox.Checked;
        }

        private void UndeadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Undead = UndeadCheckBox.Checked;
        }
        private void MonsterInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Envir.SaveDB();
        }

        private void PasteMButton_Click(object sender, EventArgs e)
        {
            string data = Clipboard.GetText();

            if (!data.StartsWith("Monster", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Cannot Paste, Copied data is not Monster Information.");
                return;
            }


            string[] monsters = data.Split(new[] {'\t'}, StringSplitOptions.RemoveEmptyEntries);


            for (int i = 1; i < monsters.Length; i++)
                MonsterInfo.FromText(monsters[i]);

            UpdateInterface();
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].CoolEye = temp;
        }

        private void ImageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Image = (Monster)ImageComboBox.SelectedItem;


        }

        private void ExportAllButton_Click(object sender, EventArgs e)
        {
            ExportMonsters(Envir.MonsterInfoList);
        }

        private void ExportSelected_Click(object sender, EventArgs e)
        {
            var list = MonsterInfoListBox.SelectedItems.Cast<MonsterInfo>().ToList();

            ExportMonsters(list);
        }

        private void ExportMonsters(IEnumerable<MonsterInfo> monsters)
        {
            var monsterInfos = monsters as MonsterInfo[] ?? monsters.ToArray();


            using (FileStream stream = File.Create(Settings.ExportPath + string.Format("MonsterInfo-{0:dd-MM-yyyy_hh-mm-ss-tt}.dat", DateTime.Now)))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(monsterInfos.Length);
                    for (int i = 0; i < monsterInfos.Length; i++)
                        monsterInfos[i].Save(writer);
                }
            }


            //var list = monsterInfos.Select(monster => monster.ToText()).ToList();

            //File.WriteAllLines(MonsterListPath, list);

            MessageBox.Show(monsterInfos.Count() + " Monsters have been exported.");
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            string Path = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Binary File|*.dat"
            };
            ofd.ShowDialog();

            if (ofd.FileName == string.Empty) return;

            Path = ofd.FileName;
            if (!File.Exists(Path))
            {
                MessageBox.Show("File could not be found!");
                return;
            }
            List<MonsterInfo> list = new List<MonsterInfo>();
            using (FileStream stream = File.OpenRead(Path))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    int count = reader.ReadInt32();
                    for (int i = 0;  i < count; i++)
                    {
                        list.Add(new MonsterInfo(reader));
                    }
                }
            }
            int mobsAdded = 0;
            int mobsUpdated = 0;
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    MonsterInfo temp = Envir.MonsterInfoList.Where(o => o.Name == list[i].Name).FirstOrDefault();
                    if (temp != null)
                    {
                        int origIdx = temp.Index;
                        for (int x = 0; x < Envir.MonsterInfoList.Count; x++)
                            if (Envir.MonsterInfoList[x] == temp)
                            {
                                if (list[i].Index != origIdx)
                                    list[i].Index = origIdx;
                                Envir.MonsterInfoList[x] = list[i];
                                mobsUpdated++;
                            }
                    }
                    else
                    {
                        list[i].Index = ++Envir.MonsterIndex;
                        Envir.MonsterInfoList.Add(list[i]);
                        mobsAdded++;
                    }
                }
            }
            MessageBox.Show(string.Format("{0} Monsters added.\r\n{1} Monsters Updated.", mobsAdded, mobsUpdated));
            /*
            string data;
            using (var sr = new StreamReader(Path))
            {
                data = sr.ReadToEnd();
            }

            var monsters = data.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var m in monsters)
                MonsterInfo.FromText(m);
            */
            UpdateInterface();
        }

        private void DropBuilderButton_Click(object sender, EventArgs e)
        {
            MirForms.DropBuilder.DropGenForm GenForm = new MirForms.DropBuilder.DropGenForm();

            GenForm.ShowDialog();
        }

        private void IgnoreBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Ignore = IgnoreBox.Checked;
        }

        private void TeleportBackBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].TeleportBack = TeleportBackBox.Checked;
        }

        private void bossBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].IsBoss = bossBox.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetMonsterIndex();
            UpdateInterface();
        }

        private void eliteBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].CanBeElite = eliteBox.Checked;
        }

        private void LightColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
            {
                _selectedMonsterInfos[i].LightColar = LightColorComboBox.SelectedItem?.ToString();

            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            string input = Interaction.InputBox("Input quest index", "Add Random Event", "", -1, -1);


            if (int.TryParse(input, out int nr))
            {
                _selectedMonsterInfos[0].RandomQuest.Add(nr);
            }

            RefreshRandomQuestList();
        }

        public void RefreshRandomQuestList()
        {
            RandomQuestBox.Text = string.Empty;

            foreach (var v in _selectedMonsterInfos[0].RandomQuest)
            {
                RandomQuestBox.Text += v.ToString() + Environment.NewLine;
            }

        }

        private void Remove_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Input quest index", "Remove Random Event", "", -1, -1);


            if (int.TryParse(input, out int nr))
            {
                _selectedMonsterInfos[0].RandomQuest.Remove(nr);
            }

            RefreshRandomQuestList();
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].RandomQuestChance = temp;
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


            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].LightEffect = temp;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].IsSub = checkBox1.Checked;
        }

        private void debuffCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (ActiveControl != sender)
                return;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].DebuffType = (AIDebuffType)debuffCBox.SelectedItem;
            */
        }

        private void debuffDurationBox_TextChanged(object sender, EventArgs e)
        {
            /*
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(debuffDurationBox.Text, out byte tmp))
            {
                debuffDurationBox.BackColor = Color.Red;
                return;
            }
            debuffDurationBox.BackColor = SystemColors.Window;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].DebuffDuration = tmp;
            */
        }

        private void debuffAmountBox_TextChanged(object sender, EventArgs e)
        {
            /*
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(debuffAmountBox.Text, out byte tmp))
            {
                debuffAmountBox.BackColor = Color.Red;
                return;
            }
            debuffAmountBox.BackColor = SystemColors.Window;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].DebuffDuration = tmp;
            */
        }

        private void stealStatCBox_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].DebuffSteal = stealStatCBox.Checked;
            */
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            /*
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(textBox3.Text, out byte tmp))
            {
                textBox3.BackColor = Color.Red;
                return;
            }
            textBox3.BackColor = SystemColors.Window;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].DebuffStealAmount = tmp;
                */
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            /*
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(textBox4.Text, out byte tmp))
            {
                textBox4.BackColor = Color.Red;
                return;
            }
            textBox4.BackColor = SystemColors.Window;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].DebuffStealDuration = tmp;
                */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            if (attackTabControl.TabPages.Count >= 5)
                return;
            TabPage page = new TabPage();
            page.Name = "attack" + attackTabControl.TabPages.Count;
            page.Text = "Attack" + attackTabControl.TabPages.Count;
            page.Parent = attackTabControl;
            ComboBox box0 = new ComboBox();
            ComboBox box1 = new ComboBox();
            Label lbl0 = new Label();
            Label lbl1 = new Label();
            lbl0.AutoSize = true;
            lbl0.Location = new System.Drawing.Point(6, 33);
            lbl0.Name = "attackStyleLabel";
            lbl0.Size = new System.Drawing.Size(64, 13);
            lbl0.TabIndex = 17;
            lbl0.Text = "Attack Style";
            lbl0.Parent = page;
            lbl1.AutoSize = true;
            lbl1.Location = new System.Drawing.Point(6, 6);
            lbl1.Name = "label36";
            lbl1.Size = new System.Drawing.Size(65, 13);
            lbl1.TabIndex = 15;
            lbl1.Text = "Attack Type";
            lbl1.Parent = page;
            box0.FormattingEnabled = true;
            box0.Location = new System.Drawing.Point(77, 3);
            box0.Name = "attackTypeCBox";
            box0.Size = new System.Drawing.Size(121, 21);
            box0.TabIndex = 14;
            box0.Parent = page;
            box0.Items.AddRange(Enum.GetValues(typeof(AIAttackStyle)).Cast<object>().ToArray());
            box1.FormattingEnabled = true;
            box1.Location = new System.Drawing.Point(77, 30);
            box1.Name = "attackStyleCBox";
            box1.Size = new System.Drawing.Size(121, 21);
            box1.TabIndex = 16;
            box1.Parent = page;
            box1.Items.AddRange(Enum.GetValues(typeof(AIAttackType)).Cast<object>().ToArray());
            box1.SelectedIndexChanged += (o, r) =>
            {
                if (ActiveControl != o)
                    return;
                for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                    _selectedMonsterInfos[i].Attacks[attackTabControl.SelectedIndex].AttackType = (AIAttackType)box1.SelectedItem;
            };
            box1.SelectedIndexChanged += (o, r) =>
            {
                if (ActiveControl != o)
                    return;
                for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                    _selectedMonsterInfos[i].Attacks[attackTabControl.SelectedIndex].AttackStyle = (AIAttackStyle)box0.SelectedItem;
            };
            attackTabControl.Controls.Add(page);
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            if (attackTabControl.TabPages.Count == 1)
                return;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Attacks.RemoveAt(attackTabControl.TabPages.Count - 1);
            attackTabControl.Controls.RemoveAt(attackTabControl.TabPages.Count - 1);
            */
        }

        private void trapPsnCBox_CheckedChanged(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AntiPoison = (lrPsnCBox.Checked ? _selectedMonsterInfos[i].AntiPoison |= AIAntiPoison.Trap : _selectedMonsterInfos[i].AntiPoison ^= AIAntiPoison.Trap);
                */
        }

        private void redPsnCBox_CheckedChanged(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AntiPoison = (lrPsnCBox.Checked ? _selectedMonsterInfos[i].AntiPoison |= AIAntiPoison.Red : _selectedMonsterInfos[i].AntiPoison ^= AIAntiPoison.Red);
                */
        }

        private void freezePsnCBox_CheckedChanged(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AntiPoison = (lrPsnCBox.Checked ? _selectedMonsterInfos[i].AntiPoison |= AIAntiPoison.Frozen : _selectedMonsterInfos[i].AntiPoison ^= AIAntiPoison.Frozen);
                */
        }

        private void slowPsnCBox_CheckedChanged(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AntiPoison = (lrPsnCBox.Checked ? _selectedMonsterInfos[i].AntiPoison |= AIAntiPoison.Slow : _selectedMonsterInfos[i].AntiPoison ^= AIAntiPoison.Slow);
                */
        }

        private void stunPsnCBox_CheckedChanged(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AntiPoison = (lrPsnCBox.Checked ? _selectedMonsterInfos[i].AntiPoison |= AIAntiPoison.Stun : _selectedMonsterInfos[i].AntiPoison ^= AIAntiPoison.Stun);
                */
        }

        private void paraPsnCBox_CheckedChanged(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AntiPoison = (lrPsnCBox.Checked ? _selectedMonsterInfos[i].AntiPoison |= AIAntiPoison.Paralysis : _selectedMonsterInfos[i].AntiPoison ^= AIAntiPoison.Paralysis);
                */
        }

        private void delayedPsnCBox_CheckedChanged(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AntiPoison = (lrPsnCBox.Checked ? _selectedMonsterInfos[i].AntiPoison |= AIAntiPoison.DelayedExplosion : _selectedMonsterInfos[i].AntiPoison ^= AIAntiPoison.DelayedExplosion);
                */
        }

        private void bleedingPsnCBox_CheckedChanged(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AntiPoison = (lrPsnCBox.Checked ? _selectedMonsterInfos[i].AntiPoison |= AIAntiPoison.Bleeding : _selectedMonsterInfos[i].AntiPoison ^= AIAntiPoison.Bleeding);
                */
        }

        private void greenPsnCBox_CheckedChanged(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AntiPoison = (lrPsnCBox.Checked ? _selectedMonsterInfos[i].AntiPoison |= AIAntiPoison.Green : _selectedMonsterInfos[i].AntiPoison ^= AIAntiPoison.Green);
                */
        }

        private void lrPsnCBox_CheckedChanged(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AntiPoison = (lrPsnCBox.Checked ? _selectedMonsterInfos[i].AntiPoison |= AIAntiPoison.LRParalysis : _selectedMonsterInfos[i].AntiPoison ^= AIAntiPoison.LRParalysis);
                */
        }

        private void noPsnCBox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void attackDelayBox_TextChanged(object sender, EventArgs e)
        {
            /*
            if (ActiveControl != sender)
                return;
            if (!long.TryParse(attackDelayBox.Text, out long tmp))
            {
                attackDelayBox.BackColor = Color.Red;
                return;
            }
            attackDelayBox.BackColor = SystemColors.Window;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Attacks[0].Delay = tmp;
                */
        }

        private void attackStyleCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Attacks[0].AttackStyle = (AIAttackStyle)attackStyleCBox.SelectedItem;
                */
        }

        private void attackTypeCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].Attacks[0].AttackType = (AIAttackType)attackTypeCBox.SelectedItem;
                */
        }

        private void allowGreen_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AllowGreen = allowGreen.Checked;
        }

        private void allowBurning_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AllowBurning = allowBurning.Checked;
        }

        private void allowRed_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AllowRed = allowRed.Checked;
        }

        private void allowFreeze_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AllowFreeze = allowFreeze.Checked;
        }

        private void allowSlow_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AllowSlow = allowSlow.Checked;
        }

        private void allowPara_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AllowPara = allowPara.Checked;
        }

        private void allowBleeding_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            for (int i = 0; i < _selectedMonsterInfos.Count; i++)
                _selectedMonsterInfos[i].AllowBleeding = allowBleeding.Checked;
        }
    }
}
