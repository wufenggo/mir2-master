﻿using System;
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
    public partial class NPCToolPanel : UserControl
    {
        MirDataTool MotherParent;
        public List<NPCInfo> NPCInfoList = new List<NPCInfo>();
        public List<NPCInfo> _SelectedItems = new List<NPCInfo>();
        public NPCToolPanel()
        {
            InitializeComponent();
        }

        public void SetChild(MirDataTool parent)
        {
            MotherParent = parent;
        }

        public void UpdateList()
        {
            NPCInfoListBox.Items.Clear();
            for (int i = 0; i < NPCInfoList.Count; i++)
                NPCInfoListBox.Items.Add(NPCInfoList[i]);
            UpdateInterface();
        }


        private void AddButton_Click(object sender, EventArgs e)
        {
            NPCInfoList.Add(new NPCInfo { Index = ++MotherParent.NPCIndex });
            UpdateInterface();
        }
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (_SelectedItems.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to remove the selected NPCs?", "Remove NPCs?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            for (int i = 0; i < _SelectedItems.Count; i++) NPCInfoList.Remove(_SelectedItems[i]);

            if (NPCInfoList.Count == 0) MotherParent.NPCIndex = 0;

            UpdateInterface();
        }

        private void UpdateInterface()
        {
            
            _SelectedItems = NPCInfoListBox.SelectedItems.Cast<NPCInfo>().ToList();

            if (_SelectedItems.Count == 0)
            {
                tabPage1.Enabled = false;
                tabPage2.Enabled = false;
                NPCIndexTextBox.Text = string.Empty;
                NFileNameTextBox.Text = string.Empty;
                NNameTextBox.Text = string.Empty;
                NXTextBox.Text = string.Empty;
                NYTextBox.Text = string.Empty;
                NImageTextBox.Text = string.Empty;
                NRateTextBox.Text = string.Empty;
                MapComboBox.SelectedItem = null;
                MinLev_textbox.Text = string.Empty;
                MaxLev_textbox.Text = string.Empty;
                Class_combo.Text = string.Empty;
                ConquestHidden_combo.SelectedIndex = -1;
                Day_combo.Text = string.Empty;
                TimeVisible_checkbox.Checked = false;
                StartHour_combo.Text = string.Empty;
                EndHour_combo.Text = string.Empty;
                StartMin_num.Value = 0;
                EndMin_num.Value = 1;
                Flag_textbox.Text = string.Empty;
                return;
            }

            NPCInfo info = _SelectedItems[0];

            tabPage1.Enabled = true;
            tabPage2.Enabled = true;

            NPCIndexTextBox.Text = info.Index.ToString();
            NFileNameTextBox.Text = info.FileName;
            NNameTextBox.Text = info.Name;
            NXTextBox.Text = info.Location.X.ToString();
            NYTextBox.Text = info.Location.Y.ToString();
            NImageTextBox.Text = info.Image.ToString();
            NRateTextBox.Text = info.Rate.ToString();
            MapComboBox.SelectedItem = MotherParent.MapPanel.MapInfoList.FirstOrDefault(x => x.Index == info.MapIndex);
            MinLev_textbox.Text = info.MinLev.ToString();
            MaxLev_textbox.Text = info.MaxLev.ToString();
            Class_combo.Text = info.ClassRequired;
            ConquestHidden_combo.SelectedItem = MotherParent.ConquestPanel.ConquestInfos.FirstOrDefault(x => x.Index == info.Conquest);
            Day_combo.Text = info.DayofWeek;
            TimeVisible_checkbox.Checked = info.TimeVisible;
            StartHour_combo.Text = info.HourStart.ToString();
            EndHour_combo.Text = info.HourEnd.ToString();
            StartMin_num.Value = info.MinuteStart;
            EndMin_num.Value = info.MinuteEnd;
            Flag_textbox.Text = info.FlagNeeded.ToString();


            for (int i = 1; i < _SelectedItems.Count; i++)
            {
                info = _SelectedItems[i];

                if (NFileNameTextBox.Text != info.FileName) NFileNameTextBox.Text = string.Empty;
                if (NNameTextBox.Text != info.Name) NNameTextBox.Text = string.Empty;
                if (NXTextBox.Text != info.Location.X.ToString()) NXTextBox.Text = string.Empty;

                if (NYTextBox.Text != info.Location.Y.ToString()) NYTextBox.Text = string.Empty;
                if (NImageTextBox.Text != info.Image.ToString()) NImageTextBox.Text = string.Empty;
                if (NRateTextBox.Text != info.Rate.ToString()) NRateTextBox.Text = string.Empty;
            }
        }

        private void RefreshNPCList()
        {
            NPCInfoListBox.SelectedIndexChanged -= NPCInfoListBox_SelectedIndexChanged;

            List<bool> selected = new List<bool>();

            for (int i = 0; i < NPCInfoListBox.Items.Count; i++) selected.Add(NPCInfoListBox.GetSelected(i));
            NPCInfoListBox.Items.Clear();

            for (int i = 0; i < NPCInfoList.Count; i++) NPCInfoListBox.Items.Add(NPCInfoList[i]);
            for (int i = 0; i < selected.Count; i++) NPCInfoListBox.SetSelected(i, selected[i]);

            NPCInfoListBox.SelectedIndexChanged += NPCInfoListBox_SelectedIndexChanged;
        }

        private void NPCInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInterface();
        }

        private void NFileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].FileName = ActiveControl.Text;

            RefreshNPCList();
        }
        private void NNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Name = ActiveControl.Text;
        }
        private void NXTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Location.X = temp;

            RefreshNPCList();
        }
        private void NYTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Location.Y = temp;

            RefreshNPCList();
        }
        private void NImageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Image = temp;

        }
        private void NRateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Rate = temp;
        }

        private void MapComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
            {
                MapInfo temp = (MapInfo)MapComboBox.SelectedItem;
                _SelectedItems[i].MapIndex = temp.Index;
            }

        }

        private void NPCInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }




        private void PasteMButton_Click(object sender, EventArgs e)
        {
            string data = Clipboard.GetText();

            if (!data.StartsWith("NPC", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Cannot Paste, Copied data is not NPC Information.");
                return;
            }


            string[] npcs = data.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);


            //for (int i = 1; i < npcs.Length; i++)
            //    NPCInfo.FromText(npcs[i]);

            UpdateInterface();
        }

        private void ExportAllButton_Click(object sender, EventArgs e)
        {
            ExportNPCs(NPCInfoList);
        }

        private void ExportSelected_Click(object sender, EventArgs e)
        {
            var list = NPCInfoListBox.SelectedItems.Cast<NPCInfo>().ToList();

            ExportNPCs(list);
        }

        public void ExportNPCs(List<NPCInfo> NPCs)
        {
            if (NPCs.Count == 0) return;
            /*
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.StartupPath + @"\Exports";
            sfd.Filter = "Binary File|*.bat";
            sfd.ShowDialog();

            if (sfd.FileName == string.Empty) return;

            using (StreamWriter sw = File.AppendText(sfd.FileNames[0]))
            {
                for (int j = 0; j < NPCs.Count; j++)
                {
                    sw.WriteLine(NPCs[j].ToText());
                }
            }
            */
            using (FileStream stream = File.Create(Settings.ExportPath + string.Format("NPCInfo-{0:dd-MM-yyyy_hh-mm-ss-tt}.dat", DateTime.Now)))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(NPCs.Count);
                    for (int i = 0; i < NPCs.Count; i++)
                        NPCs[i].Save(writer);
                }
            }
            MessageBox.Show("NPC Export complete");
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            string Path = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Binary File|*.dat";
            ofd.ShowDialog();

            if (ofd.FileName == string.Empty) return;

            Path = ofd.FileName;
            if (!File.Exists(Path))
            {
                MessageBox.Show("File could not be found!");
                return;
            }

            List<NPCInfo> list = new List<NPCInfo>();
            int added = 0;
            using (FileStream stream = File.OpenRead(Path))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        list.Add(new NPCInfo(reader));
                        added++;
                    }
                }
            }

            /*
            string data;
            using (var sr = new StreamReader(Path))
            {
                data = sr.ReadToEnd();
            }

            var npcs = data.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var m in npcs)
            {
                try
                {
                    NPCInfo.FromText(m);
                }
                catch { }
            }
            */
            UpdateInterface();
            MessageBox.Show(string.Format("{0} NPC's added", added));
        }

        private void OpenNButton_Click(object sender, EventArgs e)
        {
            if (NFileNameTextBox.Text == string.Empty) return;

            var scriptPath = Settings.NPCPath + NFileNameTextBox.Text + ".txt";

            if (File.Exists(scriptPath))
                Process.Start(scriptPath);
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(scriptPath));
                File.Create(scriptPath).Close();
                Process.Start(scriptPath);
            }
        }

        private void MinLev_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!short.TryParse(ActiveControl.Text, out short temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinLev = temp;
        }

        private void HourShow_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HourStart = temp;
        }

        private void MinutesShow_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinuteStart = temp;
        }

        private void Class_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].ClassRequired = ActiveControl.Text;
        }

        private void CopyMButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DateTime.Now.DayOfWeek.ToString());
        }

        private void MaxLev_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!short.TryParse(ActiveControl.Text, out short temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MaxLev = temp;
        }

        private void Class_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            string temp = ActiveControl.Text;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].ClassRequired = temp;
        }

        private void Day_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            string temp = ActiveControl.Text;


            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].DayofWeek = temp;
        }

        private void TimeVisible_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].TimeVisible = TimeVisible_checkbox.Checked;
        }

        private void StartHour_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HourStart = temp;
        }

        private void EndHour_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].HourEnd = temp;
        }

        private void StartMin_num_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinuteStart = (byte)StartMin_num.Value;
        }

        private void EndMin_num_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].MinuteEnd = (byte)EndMin_num.Value;
        }

        private void Flag_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].FlagNeeded = temp;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show(DateTime.Now.TimeOfDay.ToString());
        }

        private void NPCInfoForm_Load(object sender, EventArgs e)
        {

        }

        private void ConquestHidden_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            ConquestInfo temp = (ConquestInfo)ConquestHidden_combo.SelectedItem;

            for (int i = 0; i < _SelectedItems.Count; i++)
                _SelectedItems[i].Conquest = temp.Index;
        }

        private void MaxLev_textbox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Flag_textbox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void MinLev_textbox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void EndHour_combo_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void Day_combo_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void StartHour_combo_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void Class_combo_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void EndMin_num_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void StartMin_num_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void TimeVisible_checkbox_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void OpenNButton_Click_1(object sender, EventArgs e)
        {

        }

        private void NRateTextBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void NNameTextBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void NXTextBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void NImageTextBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void NYTextBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void NFileNameTextBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void ClearHButton_Click(object sender, EventArgs e)
        {

        }

        private void MapComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void ConquestHidden_combo_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void NPCInfoListBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void ExportButton_Click(object sender, EventArgs e)
        {

        }

        private void ExportSelectedButton_Click(object sender, EventArgs e)
        {

        }

        private void ImportButton_Click_1(object sender, EventArgs e)
        {

        }

        private void PasteMButton_Click_1(object sender, EventArgs e)
        {

        }

        private void CopyMButton_Click_1(object sender, EventArgs e)
        {

        }

        private void RemoveButton_Click_1(object sender, EventArgs e)
        {

        }

        private void AddButton_Click_1(object sender, EventArgs e)
        {

        }
    }
}
