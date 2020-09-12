using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MirDataTool
{
    public partial class ConquestToolPanel : UserControl
    {
        #region Fields
        MirDataTool MotherParent;
        public ConquestArcherInfo selectedArcher;
        public ConquestGateInfo selectedGate;
        public ConquestWallInfo selectedWall;
        public ConquestSiegeInfo selectedSiege;
        public ConquestFlagInfo selectedFlag;
        public ConquestFlagInfo selectedControlPoint;
        public List<ConquestInfo> ConquestInfos = new List<ConquestInfo>();
        public ConquestInfo _SelectedItem = new ConquestInfo();
        #endregion
        public ConquestToolPanel()
        {
            InitializeComponent();
        }
        #region Constructor so to speak, this is called once and will assign the default values of the options
        public void SetChild(MirDataTool parent)
        {
            MotherParent = parent;
            for (int i = 0; i < MotherParent.MapPanel.MapInfoList.Count; i++)
            {
                ConquestMap_combo.Items.Add(MotherParent.MapPanel.MapInfoList[i]);
                PalaceMap_combo.Items.Add(MotherParent.MapPanel.MapInfoList[i]);
                ExtraMaps_combo.Items.Add(MotherParent.MapPanel.MapInfoList[i]);
            }

            WarType_combo.Items.AddRange(Enum.GetValues(typeof(ConquestType)).Cast<object>().ToArray());
            WarMode_combo.Items.AddRange(Enum.GetValues(typeof(ConquestGame)).Cast<object>().ToArray());
            WarType_combo.Items.Remove(ConquestType.Forced);
            for (int i = 0; i < MotherParent.MonsterPanel.MonsterInfoList.Count; i++)
            {
                if (MotherParent.MonsterPanel.MonsterInfoList[i].AI == 80)
                    ArcherIndex_combo.Items.Add(MotherParent.MonsterPanel.MonsterInfoList[i]);

                if (MotherParent.MonsterPanel.MonsterInfoList[i].AI == 81)
                    GateIndex_combo.Items.Add(MotherParent.MonsterPanel.MonsterInfoList[i]);

                if (MotherParent.MonsterPanel.MonsterInfoList[i].AI == 82)
                    WallIndex_combo.Items.Add(MotherParent.MonsterPanel.MonsterInfoList[i]);

                if (MotherParent.MonsterPanel.MonsterInfoList[i].AI == 74)
                    SiegeIndex_combo.Items.Add(MotherParent.MonsterPanel.MonsterInfoList[i]);
            }
        }
        #endregion
        #region Update the Interface
        private void UpdateArchers()
        {
            ArcherIndex_combo.SelectedItem = null;
            ArcherIndex_combo.SelectedIndex = -1;

            if (selectedArcher != null)
            {
                Archer_gb.Enabled = true;
                ArcherIndex_combo.SelectedItem = MotherParent.MonsterPanel.MonsterInfoList.FirstOrDefault(x => x.Index == selectedArcher.MobIndex);
                ArchXLoc_textbox.Text = selectedArcher.Location.X.ToString();
                ArchYLoc_textbox.Text = selectedArcher.Location.Y.ToString();
                ArcherName_textbox.Text = selectedArcher.Name;
                ArcherCost_textbox.Text = selectedArcher.RepairCost.ToString();
            }
            else
            {
                Archer_gb.Enabled = false;
                ArchXLoc_textbox.Text = string.Empty;
                ArchYLoc_textbox.Text = string.Empty;
                ArcherName_textbox.Text = string.Empty;
                ArcherCost_textbox.Text = string.Empty;
            }
        }

        private void UpdateFlags()
        {
            if (selectedFlag != null)
            {
                Flag_gb.Enabled = true;
                FlagXLoc_textbox.Text = selectedFlag.Location.X.ToString();
                FlagYLoc_textbox.Text = selectedFlag.Location.Y.ToString();
                FlagName_textbox.Text = selectedFlag.Name;
                FlagFilename_textbox.Text = selectedFlag.FileName;
            }
            else
            {
                Flag_gb.Enabled = false;
                FlagXLoc_textbox.Text = string.Empty;
                FlagYLoc_textbox.Text = string.Empty;
                FlagName_textbox.Text = string.Empty;
                FlagFilename_textbox.Text = string.Empty;
            }
        }

        private void UpdateGates()
        {
            if (selectedGate != null)
            {
                Gates_gb.Enabled = true;
                GateIndex_combo.SelectedItem = MotherParent.MonsterPanel.MonsterInfoList.FirstOrDefault(x => x.Index == selectedGate.MobIndex);
                GateXLoc_textbox.Text = selectedGate.Location.X.ToString();
                GateYLoc_textbox.Text = selectedGate.Location.Y.ToString();
                GateName_textbox.Text = selectedGate.Name;
                GateCost_textbox.Text = selectedGate.RepairCost.ToString();

            }
            else
            {
                Gates_gb.Enabled = false;
                GateIndex_combo.SelectedItem = -1;
                GateXLoc_textbox.Text = string.Empty;
                GateYLoc_textbox.Text = string.Empty;
                GateName_textbox.Text = string.Empty;
                GateCost_textbox.Text = string.Empty;
            }

        }

        private void UpdateWalls()
        {
            if (selectedWall != null)
            {
                Walls_gb.Enabled = true;
                WallIndex_combo.SelectedItem = MotherParent.MonsterPanel.MonsterInfoList.FirstOrDefault(x => x.Index == selectedWall.MobIndex);
                WallXLoc_textbox.Text = selectedWall.Location.X.ToString();
                WallYLoc_textbox.Text = selectedWall.Location.Y.ToString();
                WallName_textbox.Text = selectedWall.Name;
                WallCost_textbox.Text = selectedWall.RepairCost.ToString();

            }
            else
            {
                Walls_gb.Enabled = false;
                WallIndex_combo.SelectedItem = -1;
                WallXLoc_textbox.Text = string.Empty;
                WallYLoc_textbox.Text = string.Empty;
                WallName_textbox.Text = string.Empty;
                WallCost_textbox.Text = string.Empty;
            }

        }

        private void UpdateSiege()
        {
            if (selectedSiege != null)
            {
                Siege_gb.Enabled = true;
                SiegeIndex_combo.SelectedItem = MotherParent.MonsterPanel.MonsterInfoList.FirstOrDefault(x => x.Index == selectedSiege.MobIndex);
                SiegeXLoc_textbox.Text = selectedSiege.Location.X.ToString();
                SiegeYLoc_textbox.Text = selectedSiege.Location.Y.ToString();
                SiegeName_textbox.Text = selectedSiege.Name;
                SiegeCost_textbox.Text = selectedSiege.RepairCost.ToString();

            }
            else
            {
                Siege_gb.Enabled = false;
                SiegeIndex_combo.SelectedIndex = -1;
                SiegeXLoc_textbox.Text = string.Empty;
                SiegeYLoc_textbox.Text = string.Empty;
                SiegeName_textbox.Text = string.Empty;
                SiegeCost_textbox.Text = string.Empty;
            }
        }

        private void UpdateControlPoints()
        {
            if (selectedControlPoint != null)
            {
                Control_gb.Enabled = true;
                ControlXLoc_textbox.Text = selectedControlPoint.Location.X.ToString();
                ControlYLoc_textbox.Text = selectedControlPoint.Location.Y.ToString();
                ControlName_textbox.Text = selectedControlPoint.Name;
                ControlFilename_textbox.Text = selectedControlPoint.FileName;
            }
            else
            {
                Control_gb.Enabled = false;
                ControlXLoc_textbox.Text = string.Empty;
                ControlYLoc_textbox.Text = string.Empty;
                ControlName_textbox.Text = string.Empty;
                ControlFilename_textbox.Text = string.Empty;
            }
        }

        public void UpdateList()
        {
            ConquestInfoListBox.Items.Clear();
            for (int i = 0; i < ConquestInfos.Count; i++)
                ConquestInfoListBox.Items.Add(ConquestInfos[i]);
            UpdateInterface();
        }

        private void UpdateInterface()
        {
            _SelectedItem = (ConquestInfo)ConquestInfoListBox.SelectedItem;


            Maps_listbox.Items.Clear();
            Guards_listbox.Items.Clear();
            Gates_listbox.Items.Clear();
            Walls_listbox.Items.Clear();
            Siege_listbox.Items.Clear();
            Flags_listbox.Items.Clear();
            Index_textbox.Text = string.Empty;
            Name_textbox.Text = string.Empty;
            FullMap_checkbox.Checked = false;
            LocX_textbox.Text = string.Empty;
            LocY_textbox.Text = string.Empty;
            Size_textbox.Text = string.Empty;
            ObLocX_textbox.Text = string.Empty;
            ObLocY_textbox.Text = string.Empty;
            ObSize_textbox.Text = string.Empty;
            Controls_listbox.Items.Clear();
            ConquestMap_combo.SelectedIndex = -1;
            PalaceMap_combo.SelectedIndex = -1;
            ExtraMaps_combo.SelectedIndex = -1;
            WarType_combo.SelectedIndex = -1;
            WarMode_combo.SelectedIndex = -1;
            ArcherIndex_combo.SelectedIndex = -1;
            ArchXLoc_textbox.Text = string.Empty;
            ArchYLoc_textbox.Text = string.Empty;
            Archer_gb.Enabled = false;
            SiegeIndex_combo.SelectedIndex = -1;
            SiegeXLoc_textbox.Text = string.Empty;
            SiegeYLoc_textbox.Text = string.Empty;
            Siege_gb.Enabled = false;
            SiegeName_textbox.Text = string.Empty;
            Main_tabs.Enabled = false;
            WarLength_num.Value = 60;
            StartHour_num.Value = 1;
            Mon_checkbox.Checked = false;
            Tue_checkbox.Checked = false;
            Wed_checkbox.Checked = false;
            Thu_checkbox.Checked = false;
            Fri_checkbox.Checked = false;
            Sat_checkbox.Checked = false;
            Sun_checkbox.Checked = false;

            if (_SelectedItem != null)
            {
                Main_tabs.Enabled = true;

                Index_textbox.Text = _SelectedItem.Index.ToString();
                Name_textbox.Text = _SelectedItem.Name.ToString();
                FullMap_checkbox.Checked = _SelectedItem.FullMap;
                LocX_textbox.Text = _SelectedItem.Location.X.ToString();
                LocY_textbox.Text = _SelectedItem.Location.Y.ToString();
                Size_textbox.Text = _SelectedItem.Size.ToString();
                ObLocX_textbox.Text = _SelectedItem.KingLocation.X.ToString();
                ObLocY_textbox.Text = _SelectedItem.KingLocation.Y.ToString();
                ObSize_textbox.Text = _SelectedItem.KingSize.ToString();
                ConquestMap_combo.SelectedItem = MotherParent.MapPanel.MapInfoList.FirstOrDefault(x => x.Index == _SelectedItem.MapIndex);
                PalaceMap_combo.SelectedItem = MotherParent.MapPanel.MapInfoList.FirstOrDefault(x => x.Index == _SelectedItem.PalaceIndex);
                WarMode_combo.SelectedItem = _SelectedItem.Game;
                WarType_combo.SelectedItem = _SelectedItem.Type;
                WarLength_num.Value = _SelectedItem.WarLength;
                StartHour_num.Value = _SelectedItem.StartHour;
                Mon_checkbox.Checked = _SelectedItem.Monday;
                Tue_checkbox.Checked = _SelectedItem.Tuesday;
                Wed_checkbox.Checked = _SelectedItem.Wednesday;
                Thu_checkbox.Checked = _SelectedItem.Thursday;
                Fri_checkbox.Checked = _SelectedItem.Friday;
                Sat_checkbox.Checked = _SelectedItem.Saturday;
                Sun_checkbox.Checked = _SelectedItem.Sunday;
                for (int i = 0; i < _SelectedItem.ConquestGuards.Count; i++)
                {
                    Guards_listbox.Items.Add(_SelectedItem.ConquestGuards[i]);
                }

                for (int i = 0; i < _SelectedItem.ExtraMaps.Count; i++)
                {
                    Maps_listbox.Items.Add(MotherParent.MapPanel.MapInfoList.FirstOrDefault(x => x.Index == _SelectedItem.ExtraMaps[i]));
                }

                for (int i = 0; i < _SelectedItem.ConquestGates.Count; i++)
                {
                    Gates_listbox.Items.Add(_SelectedItem.ConquestGates[i]);
                }
                for (int i = 0; i < _SelectedItem.ConquestWalls.Count; i++)
                {
                    Walls_listbox.Items.Add(_SelectedItem.ConquestWalls[i]);
                }
                for (int i = 0; i < _SelectedItem.ConquestSieges.Count; i++)
                {
                    Siege_listbox.Items.Add(_SelectedItem.ConquestSieges[i]);
                }

                for (int i = 0; i < _SelectedItem.ConquestFlags.Count; i++)
                {
                    Flags_listbox.Items.Add(_SelectedItem.ConquestFlags[i]);
                }

                for (int i = 0; i < _SelectedItem.ControlPoints.Count; i++)
                {
                    Controls_listbox.Items.Add(_SelectedItem.ControlPoints[i]);
                }

            }

        }
        #endregion
        #region Controls
        private void ConquestInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            UpdateInterface();
        }

        private void AddConq_button_Click(object sender, EventArgs e)
        {
            ConquestInfos.Add(new ConquestInfo { Index = ++MotherParent.ConquestIndex, Location = new Point(0, 0), Size = 10, Name = "Conquest Wall", MapIndex = 1, PalaceIndex = 2 });
            UpdateList();
            UpdateInterface();
        }

        private void AddGuard_button_Click(object sender, EventArgs e)
        {

            if (_SelectedItem != null)
            {
                _SelectedItem.ConquestGuards.Add(new ConquestArcherInfo { Location = new Point(0, 0), Name = "Guard", Index = ++_SelectedItem.GuardIndex, MobIndex = 1, RepairCost = 1000 });
                UpdateInterface();
            }


        }

        private void Guards_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (Guards_listbox.SelectedIndex != -1)
            {
                selectedArcher = (ConquestArcherInfo)Guards_listbox.SelectedItem;
                UpdateArchers();
            }
            else
                selectedArcher = null;

        }

        private void AddExtraMap_button_Click(object sender, EventArgs e)
        {
            if (_SelectedItem != null && ExtraMaps_combo.SelectedIndex != -1)
            {
                MapInfo temp = (MapInfo)ExtraMaps_combo.SelectedItem;
                _SelectedItem.ExtraMaps.Add(temp.Index);
                UpdateInterface();
            }

        }

        private void ConquestInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Name_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            _SelectedItem.Name = ActiveControl.Text;
        }

        private void LocX_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            _SelectedItem.Location.X = temp;
        }

        private void LocY_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            _SelectedItem.Location.Y = temp;
        }

        private void Size_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            _SelectedItem.Size = temp;
        }

        private void ArchXLoc_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedArcher.Location.X = temp;
        }

        private void ArchYLoc_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedArcher.Location.Y = temp;
        }

        private void ConquestMap_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            MapInfo temp = (MapInfo)ConquestMap_combo.SelectedItem;
            _SelectedItem.MapIndex = temp.Index;

        }

        private void PalaceMap_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            MapInfo temp = (MapInfo)PalaceMap_combo.SelectedItem;
            _SelectedItem.PalaceIndex = temp.Index;

        }

        private void ArcherIndex_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            MonsterInfo temp = (MonsterInfo)ArcherIndex_combo.SelectedItem;
            selectedArcher.MobIndex = temp.Index;
        }

        private void ArcherName_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            selectedArcher.Name = ActiveControl.Text;
        }

        private void AddGate_button_Click(object sender, EventArgs e)
        {
            if (_SelectedItem != null)
            {
                _SelectedItem.ConquestGates.Add(new ConquestGateInfo { Location = new Point(0, 0), Name = "Gate", Index = ++_SelectedItem.GateIndex, MobIndex = 1, RepairCost = 1000 });
                UpdateInterface();
            }
        }

        private void Gates_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ActiveControl != sender) return;
            if (Gates_listbox.SelectedIndex != -1)
            {
                selectedGate = (ConquestGateInfo)Gates_listbox.SelectedItem;
                UpdateGates();
            }
            else
                selectedGate = null;
        }

        private void GateXLoc_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedGate.Location.X = temp;
        }

        private void GateIndex_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            MonsterInfo temp = (MonsterInfo)GateIndex_combo.SelectedItem;
            selectedGate.MobIndex = temp.Index;
        }

        private void GateYLoc_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedGate.Location.Y = temp;
        }

        private void AddWall_button_Click(object sender, EventArgs e)
        {
            if (_SelectedItem != null)
            {
                _SelectedItem.ConquestWalls.Add(new ConquestWallInfo { Location = new Point(0, 0), Name = "Wall", Index = ++_SelectedItem.WallIndex, MobIndex = 1, RepairCost = 1000 });
                UpdateInterface();
            }
        }

        private void Walls_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (Walls_listbox.SelectedIndex != -1)
            {
                selectedWall = (ConquestWallInfo)Walls_listbox.SelectedItem;
                UpdateWalls();
            }
            else
                selectedWall = null;
        }

        private void WallIndex_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            MonsterInfo temp = (MonsterInfo)WallIndex_combo.SelectedItem;
            selectedWall.MobIndex = temp.Index;
        }

        private void WallXLoc_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedWall.Location.X = temp;
        }

        private void WallYLoc_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedWall.Location.Y = temp;
        }

        private void ArcherCost_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!uint.TryParse(ActiveControl.Text, out uint temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedArcher.RepairCost = temp;
        }

        private void GateCost_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!uint.TryParse(ActiveControl.Text, out uint temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedGate.RepairCost = temp;
        }

        private void WallCost_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!uint.TryParse(ActiveControl.Text, out uint temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedWall.RepairCost = temp;
        }

        private void GateName_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            selectedGate.Name = ActiveControl.Text;
        }

        private void Walls_gb_Enter(object sender, EventArgs e)
        {

        }

        private void WallName_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            selectedWall.Name = ActiveControl.Text;
        }


        private void WarType_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            _SelectedItem.Type = (ConquestType)WarType_combo.SelectedItem;
        }

        private void WarMode_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            _SelectedItem.Game = (ConquestGame)WarMode_combo.SelectedItem;
        }

        private void WarTimes_gb_Enter(object sender, EventArgs e)
        {

        }

        private void StartHour_num_ValueChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            _SelectedItem.StartHour = (byte)StartHour_num.Value;
        }

        private void WarLength_num_ValueChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            _SelectedItem.WarLength = (int)WarLength_num.Value;
        }

        private void Mon_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            _SelectedItem.Monday = Mon_checkbox.Checked;
        }

        private void Tue_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            _SelectedItem.Tuesday = Tue_checkbox.Checked;
        }

        private void Wed_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            _SelectedItem.Wednesday = Wed_checkbox.Checked;
        }

        private void Thu_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            _SelectedItem.Thursday = Thu_checkbox.Checked;
        }

        private void Fri_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            _SelectedItem.Friday = Fri_checkbox.Checked;
        }

        private void Sat_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            _SelectedItem.Saturday = Sat_checkbox.Checked;
        }

        private void Sun_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            _SelectedItem.Sunday = Sun_checkbox.Checked;
        }

        private void RemoveConq_button_Click(object sender, EventArgs e)
        {
            if (_SelectedItem == null) return;

            if (MessageBox.Show("Are you sure you want to remove the selected Conquest?", "Remove Items?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            ConquestInfos.Remove(_SelectedItem);

            if (ConquestInfos.Count == 0) MotherParent.ConquestIndex = 0;

            UpdateInterface();

        }

        private void RemoveMap_button_Click(object sender, EventArgs e)
        {
            if (Maps_listbox.SelectedItem != null)
                _SelectedItem.ExtraMaps.Remove(((MapInfo)Maps_listbox.SelectedItem).Index);

            UpdateInterface();
        }

        private void Maps_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RemoveGuard_button_Click(object sender, EventArgs e)
        {
            if (Guards_listbox.SelectedItem != null)
                _SelectedItem.ConquestGuards.Remove((ConquestArcherInfo)Guards_listbox.SelectedItem);

            UpdateInterface();
        }

        private void RemoveGate_button_Click(object sender, EventArgs e)
        {
            if (Gates_listbox.SelectedItem != null)
                _SelectedItem.ConquestGates.Remove((ConquestGateInfo)Gates_listbox.SelectedItem);

            UpdateInterface();
        }

        private void RemoveWall_button_Click(object sender, EventArgs e)
        {
            if (Walls_listbox.SelectedItem != null)
                _SelectedItem.ConquestWalls.Remove((ConquestWallInfo)Walls_listbox.SelectedItem);

            UpdateInterface();
        }

        private void ObLocX_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            _SelectedItem.KingLocation.X = temp;
        }

        private void ObLocY_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            _SelectedItem.KingLocation.Y = temp;
        }

        private void ObSize_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;

            _SelectedItem.KingSize = temp;
        }

        private void Siege_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (Siege_listbox.SelectedIndex != -1)
            {
                selectedSiege = (ConquestSiegeInfo)Siege_listbox.SelectedItem;
                UpdateSiege();
            }
            else
                selectedSiege = null;
        }

        private void AddSiege_button_Click(object sender, EventArgs e)
        {
            if (_SelectedItem != null)
            {
                _SelectedItem.ConquestSieges.Add(new ConquestSiegeInfo { Location = new Point(0, 0), Name = "Siege", Index = ++_SelectedItem.SiegeIndex, MobIndex = 1, RepairCost = 1000 });
                UpdateInterface();
            }
        }

        private void RemoveSiege_button_Click(object sender, EventArgs e)
        {
            if (Siege_listbox.SelectedItem != null)
                _SelectedItem.ConquestSieges.Remove((ConquestSiegeInfo)Siege_listbox.SelectedItem);

            UpdateInterface();
        }

        private void SiegeXLoc_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedSiege.Location.X = temp;
        }

        private void SiegeYLoc_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedSiege.Location.Y = temp;
        }

        private void SiegeName_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            selectedSiege.Name = ActiveControl.Text;
        }

        private void SiegeCost_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!uint.TryParse(ActiveControl.Text, out uint temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedSiege.RepairCost = temp;
        }

        private void SiegeIndex_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            MonsterInfo temp = (MonsterInfo)SiegeIndex_combo.SelectedItem;
            selectedSiege.MobIndex = temp.Index;
        }

        private void RemoveFlag_button_Click(object sender, EventArgs e)
        {
            if (Flags_listbox.SelectedItem != null)
                _SelectedItem.ConquestFlags.Remove((ConquestFlagInfo)Flags_listbox.SelectedItem);

            UpdateInterface();
        }

        private void AddFlag_button_Click(object sender, EventArgs e)
        {
            if (_SelectedItem != null)
            {
                _SelectedItem.ConquestFlags.Add(new ConquestFlagInfo { Location = new Point(0, 0), Name = "Flag", Index = ++_SelectedItem.FlagIndex });
                UpdateInterface();
            }
        }

        private void FlagXLoc_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedFlag.Location.X = temp;
        }

        private void FlagYLoc_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedFlag.Location.Y = temp;
        }

        private void FlagName_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            selectedFlag.Name = ActiveControl.Text;
        }

        private void Flags_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (Flags_listbox.SelectedIndex != -1)
            {
                selectedFlag = (ConquestFlagInfo)Flags_listbox.SelectedItem;
                UpdateFlags();
            }
            else
                selectedFlag = null;
        }

        private void FlagFilename_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            selectedFlag.FileName = ActiveControl.Text;
        }

        private void FullMap_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            _SelectedItem.FullMap = FullMap_checkbox.Checked;
        }



        private void Control_Listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (Controls_listbox.SelectedIndex != -1)
            {
                selectedControlPoint = (ConquestFlagInfo)Controls_listbox.SelectedItem;
                UpdateControlPoints();
            }
            else
                selectedControlPoint = null;
        }

        private void AddControl_button_Click(object sender, EventArgs e)
        {
            if (_SelectedItem != null)
            {
                _SelectedItem.ControlPoints.Add(new ConquestFlagInfo { Location = new Point(0, 0), Name = "Control Point", Index = ++_SelectedItem.ControlPointIndex });
                UpdateInterface();
            }
        }

        private void RemoveControl_button_Click(object sender, EventArgs e)
        {
            if (Controls_listbox.SelectedItem != null)
                _SelectedItem.ControlPoints.Remove((ConquestFlagInfo)Controls_listbox.SelectedItem);

            UpdateInterface();
        }

        private void ControlXLoc_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedControlPoint.Location.X = temp;
        }

        private void ControlYLoc_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            selectedControlPoint.Location.Y = temp;
        }

        private void ControlName_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            selectedControlPoint.Name = ActiveControl.Text;
        }

        private void ControlFilename_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            selectedControlPoint.FileName = ActiveControl.Text;
        }
        #endregion
    }
}
