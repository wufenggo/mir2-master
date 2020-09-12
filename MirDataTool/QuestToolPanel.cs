using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace MirDataTool
{
    public partial class QuestToolPanel : UserControl
    {
        MirDataTool MotherParent;
        public List<QuestInfo> QuestInfoList = new List<QuestInfo>();
        public List<QuestInfo> _SelectedItems = new List<QuestInfo>();
        public QuestToolPanel()
        {
            InitializeComponent();
            QTypeComboBox.Items.AddRange(Enum.GetValues(typeof(QuestType)).Cast<object>().ToArray());
            RequiredClassComboBox.Items.AddRange(Enum.GetValues(typeof(RequiredClass)).Cast<object>().ToArray());
        }

        public void SetChild(MirDataTool parent)
        {
            MotherParent = parent;
        }

        public void UpdateList()
        {
            QuestInfoListBox.Items.Clear();
            for (int i = 0;i < QuestInfoList.Count; i++)
                QuestInfoListBox.Items.Add(QuestInfoList[i]);
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            QuestInfoList.Add(new QuestInfo(false) { Index = ++MotherParent.QuestIndex });
            UpdateInterface();
        }
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (_SelectedItems.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to remove the selected Quests?", "Remove Quests?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            for (int i = 0; i < _SelectedItems.Count; i++) QuestInfoList.Remove(_SelectedItems[i]);

            if (QuestInfoList.Count == 0) MotherParent.QuestIndex = 0;

            UpdateInterface();
        }

        private void UpdateInterface()
        {
            if (QuestInfoListBox.Items.Count != QuestInfoList.Count(x => !x.HeroQuest))
            {
                QuestInfoListBox.Items.Clear();
                RequiredQuestComboBox.Items.Clear();

                RequiredQuestComboBox.Items.Add(new QuestInfo(false) { Index = 0, Name = "None" });

                for (int i = 0; i < QuestInfoList.Count; i++)
                {
                    if (QuestInfoList[i].HeroQuest) continue;

                    QuestInfoListBox.Items.Add(QuestInfoList[i]);
                    RequiredQuestComboBox.Items.Add(QuestInfoList[i]);
                }

            }

            _SelectedItems = QuestInfoListBox.SelectedItems.Cast<QuestInfo>().ToList();

            if (_SelectedItems.Count == 0)
            {
                QuestInfoPanel.Enabled = false;
                QuestIndexTextBox.Text = string.Empty;
                QFileNameTextBox.Text = string.Empty;
                QNameTextBox.Text = string.Empty;
                QGroupTextBox.Text = string.Empty;
                QTypeComboBox.SelectedItem = null;

                QGotoTextBox.Text = string.Empty;
                QKillTextBox.Text = string.Empty;
                QItemTextBox.Text = string.Empty;
                QFlagTextBox.Text = string.Empty;

                RequiredMinLevelTextBox.Text = string.Empty;
                RequiredMaxLevelTextBox.Text = string.Empty;
                RequiredQuestComboBox.SelectedItem = null;
                RequiredClassComboBox.SelectedItem = null;

                return;
            }

            QuestInfo info = _SelectedItems[0];

            QuestInfoPanel.Enabled = true;
            QuestIndexTextBox.Text = info.Index.ToString();
            QFileNameTextBox.Text = info.FileName;
            QNameTextBox.Text = info.Name;
            QGroupTextBox.Text = info.Group;
            QTypeComboBox.SelectedItem = info.Type;

            QGotoTextBox.Text = info.GotoMessage;
            QKillTextBox.Text = info.KillMessage;
            QItemTextBox.Text = info.ItemMessage;
            QFlagTextBox.Text = info.FlagMessage;

            RequiredMinLevelTextBox.Text = info.RequiredMinLevel.ToString();
            RequiredMaxLevelTextBox.Text = info.RequiredMaxLevel.ToString();

            percentageBox.Checked = info.percentageExp;
            autoCompleteBox.Checked = info.autoComplete;

            timeBox.Text = info.Time.ToString();

            if (Convert.ToInt32(RequiredMaxLevelTextBox.Text) <= 0) RequiredMaxLevelTextBox.Text = byte.MaxValue.ToString();

            QuestInfo tempQuest = QuestInfoList.FirstOrDefault(c => c.Index == info.RequiredQuest);

            if (tempQuest == null)
            {
                tempQuest = (QuestInfo)RequiredQuestComboBox.Items[0];
            }

            RequiredQuestComboBox.SelectedItem = tempQuest;  //test
            RequiredClassComboBox.SelectedItem = info.RequiredClass;

            for (int i = 1; i < _SelectedItems.Count; i++)
            {
                info = _SelectedItems[i];

                if (QFileNameTextBox.Text != info.FileName) QFileNameTextBox.Text = string.Empty;
                if (QNameTextBox.Text != info.Name) QNameTextBox.Text = string.Empty;
                if (QGroupTextBox.Text != info.Group) QGroupTextBox.Text = string.Empty;

                if (QTypeComboBox.SelectedItem != null)
                    if ((QuestType)QTypeComboBox.SelectedItem != info.Type) QTypeComboBox.SelectedItem = null;

                if (QGotoTextBox.Text != info.GotoMessage) QGotoTextBox.Text = string.Empty;
                if (QKillTextBox.Text != info.KillMessage) QKillTextBox.Text = string.Empty;
                if (QItemTextBox.Text != info.ItemMessage) QItemTextBox.Text = string.Empty;
                if (QFlagTextBox.Text != info.ItemMessage) QFlagTextBox.Text = string.Empty;

                if (RequiredMinLevelTextBox.Text != info.RequiredMinLevel.ToString()) RequiredMinLevelTextBox.Text = string.Empty;
                if (RequiredMaxLevelTextBox.Text != info.RequiredMaxLevel.ToString()) RequiredMaxLevelTextBox.Text = byte.MaxValue.ToString();

                if (RequiredQuestComboBox.SelectedValue != null)
                    if ((string)RequiredQuestComboBox.SelectedValue != info.RequiredQuest.ToString()) RequiredQuestComboBox.SelectedItem = null;

                if (RequiredClassComboBox.SelectedItem != null)
                    if ((RequiredClass)RequiredClassComboBox.SelectedItem != info.RequiredClass) RequiredClassComboBox.SelectedItem = null;
            }
        }

        private void RefreshQuestList()
        {
            QuestInfoListBox.SelectedIndexChanged -= QuestInfoListBox_SelectedIndexChanged;

            List<bool> selected = new List<bool>();

            for (int i = 0; i < QuestInfoListBox.Items.Count; i++) selected.Add(QuestInfoListBox.GetSelected(i));
            QuestInfoListBox.Items.Clear();
            for (int i = 0; i < QuestInfoList.Count; i++)
                if (!QuestInfoList[i].HeroQuest)
                    QuestInfoListBox.Items.Add(QuestInfoList[i]);

            for (int i = 0; i < selected.Count; i++) QuestInfoListBox.SetSelected(i, selected[i]);

            QuestInfoListBox.SelectedIndexChanged += QuestInfoListBox_SelectedIndexChanged;

        }

        private void QuestInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInterface();
        }

        private void QuestInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void PasteMButton_Click(object sender, EventArgs e)
        {
            string data = Clipboard.GetText();

            if (!data.StartsWith("Quest", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Cannot Paste, Copied data is not Quest Information.");
                return;
            }


            string[] npcs = data.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);


            //for (int i = 1; i < npcs.Length; i++)
            //    NPCInfo.FromText(npcs[i]);

            UpdateInterface();
        }


        private void ExportAllButton_Click(object sender, EventArgs e)
        {
            ExportQuests(QuestInfoList);
        }

        private void ExportSelected_Click(object sender, EventArgs e)
        {
            var list = QuestInfoListBox.SelectedItems.Cast<QuestInfo>().ToList();

            ExportQuests(list);
        }

        public void ExportQuests(List<QuestInfo> Quests)
        {
            if (Quests.Count == 0) return;

            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Settings.ExportPath,
                Filter = "Binary File|*.dat",
                FileName = string.Format("QuestInfo-{0:dd-MM-yyyy_hh-mm-ss-tt}.dat", DateTime.Now)
            };
            sfd.ShowDialog();

            if (sfd.FileName == string.Empty) return;
            using (FileStream stream = File.Create(sfd.FileName))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Quests.Count);
                    for (int i = 0; i < Quests.Count; i++)
                        Quests[i].Save(writer);
                }

            }
            MessageBox.Show("Quest Export complete");
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
                MessageBox.Show("File not found!");
                return;
            }
            List<QuestInfo> list = new List<QuestInfo>();
            using (FileStream stream = File.OpenRead(Path))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                        list.Add(new QuestInfo(reader));
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Index = ++MotherParent.QuestIndex;
                QuestInfoList.Add(list[i]);
            }
            UpdateInterface();
            MessageBox.Show("Quest Import complete");
        }

        private void QNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Name = ActiveControl.Text;

            RefreshQuestList();
        }

        private void QGroupTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Group = ActiveControl.Text;
        }

        private void QTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Type = (QuestType)QTypeComboBox.SelectedItem;
        }

        private void QFileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].FileName = ActiveControl.Text;

        }

        private void QGotoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].GotoMessage = ActiveControl.Text;

        }

        private void QKillTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].KillMessage = ActiveControl.Text;

        }

        private void QItemTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].ItemMessage = ActiveControl.Text;
        }

        private void QFlagTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].FlagMessage = ActiveControl.Text;
        }


        private void RequiredMinLevelTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp) || temp > Convert.ToInt32(RequiredMaxLevelTextBox.Text) || temp < byte.MinValue)
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].RequiredMinLevel = temp;
        }

        private void RequiredMaxLevelTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp) || temp < Convert.ToInt32(RequiredMinLevelTextBox.Text) || temp > byte.MaxValue)
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].RequiredMaxLevel = temp;
        }

        private void RequiredQuestComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
            {
                QuestInfo temp = (QuestInfo)RequiredQuestComboBox.SelectedItem;

                _SelectedItems[i].RequiredQuest = temp.Index;
            }
        }

        private void RequiredClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].RequiredClass = (RequiredClass)RequiredClassComboBox.SelectedItem;
        }

        private void OpenQButton_Click(object sender, EventArgs e)
        {
            if (QFileNameTextBox.Text == string.Empty) return;

            var scriptPath = Settings.QuestPath + QFileNameTextBox.Text + ".txt";

            if (File.Exists(scriptPath))
                Process.Start(scriptPath);
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(scriptPath));

                File.Create(scriptPath).Close();

                File.WriteAllText(scriptPath,
                   string.Format("{0}\r\n\r\n{1}\r\n\r\n{2}\r\n\r\n{3}\r\n\r\n{4}\r\n\r\n{5}\r\n\r\n{6}\r\n\r\n{7}\r\n\r\n{8}\r\n\r\n{9}\r\n\r\n{10}\r\n\r\n{11}",
                    "[@Description]", "[@TaskDescription]", "[@Completion]", "[@KillTasks]", "[@ItemTasks]", "[@FlagTasks]", "[@CarryItems]", "[@FixedRewards]", "[@SelectRewards]", "[@ExpReward]", "[@GoldReward]", "[@BuffRewards]"));

                Process.Start(scriptPath);
            }
        }

        private void percentageBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].percentageExp = ((CheckBox)sender).Checked;
        }

        private void timeBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp) || temp > Convert.ToInt32(timeBox.Text) || temp < int.MinValue)
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Time = temp;
        }

        private void autoCompleteBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].autoComplete = ((CheckBox)sender).Checked;
        }        
    }
}
