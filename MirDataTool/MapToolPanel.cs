using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using System.IO;

namespace MirDataTool
{
    public partial class MapToolPanel : UserControl
    {
        public List<MapInfo> MapInfoList = new List<MapInfo>();
        public List<MapInfo> _selectedMapInfos = new List<MapInfo>();
        public List<SafeZoneInfo> _selectedSafeZoneInfos = new List<SafeZoneInfo>();
        public List<RespawnInfo> _selectedRespawnInfos = new List<RespawnInfo>();
        public List<MovementInfo> _selectedMovementInfos = new List<MovementInfo>();
        public List<MineZone> _selectedMineZones = new List<MineZone>();
        public MapInfo _info;

        MirDataTool MotherParent;
        public void SetChild(MirDataTool parent)
        {
            MotherParent = parent;
            MineComboBox.Items.Add(new ListItem { Text = "Disabled", Value = "0" });
            for (int i = 0; i < Settings.MineSetList.Count; i++) MineComboBox.Items.Add(new ListItem(Settings.MineSetList[i].Name, (i + 1).ToString()));

            MineZoneComboBox.Items.Add(new ListItem("Disabled", "0"));
            for (int i = 0; i < Settings.MineSetList.Count; i++) MineZoneComboBox.Items.Add(new ListItem(Settings.MineSetList[i].Name, (i + 1).ToString()));

            LightsComboBox.Items.AddRange(Enum.GetValues(typeof(LightSetting)).Cast<object>().ToArray());

            for (int i = 0; i < MotherParent.MonsterPanel.MonsterInfoList.Count; i++) MonsterInfoComboBox.Items.Add(MotherParent.MonsterPanel.MonsterInfoList[i]);

            ConquestComboBox.Items.Add(new ListItem("None", "0"));
            for (int i = 0; i < MotherParent.ConquestPanel.ConquestInfos.Count; i++) ConquestComboBox.Items.Add(MotherParent.ConquestPanel.ConquestInfos[i]);

            UpdateInterface();
        }

        public MapToolPanel()
        {
            InitializeComponent();
        }


        private void UpdateInterface()
        {
            //Group<MapInfo> orderedMapInfoList = Envir.MapInfoList.OrderBy(m => m.Title).ToList();

            if (MapInfoListBox.Items.Count != MapInfoList.Count)
            {
                MapInfoListBox.Items.Clear();
                for (int i = 0; i < MapInfoList.Count; i++)
                    MapInfoListBox.Items.Add(MapInfoList[i]);
            }

            DestMapComboBox.Items.Clear();
            for(int i = 0; i < MapInfoList.Count; i++)
                DestMapComboBox.Items.Add(MapInfoList[i]);
            _selectedMapInfos = MapInfoListBox.SelectedItems.Cast<MapInfo>().ToList();

            if (_selectedMapInfos == null || _selectedMapInfos.Count == 0)
            {
                tabPage1.Show();
                MapTabs.Enabled = false;
                MapIndexTextBox.Text = string.Empty;
                FileNameTextBox.Text = string.Empty;
                MapNameTextBox.Text = string.Empty;
                MiniMapTextBox.Text = string.Empty;
                BigMapTextBox.Text = string.Empty;
                LightsComboBox.SelectedItem = null;
                MineComboBox.SelectedItem = null;
                MusicTextBox.Text = string.Empty;

                NoTeleportCheckbox.Checked = false;
                NoReconnectCheckbox.Checked = false;
                NoRandomCheckbox.Checked = false;
                NoEscapeCheckbox.Checked = false;
                NoRecallCheckbox.Checked = false;
                NoDrugCheckbox.Checked = false;

                NoPositionCheckbox.Checked = false;
                NoThrowItemCheckbox.Checked = false;
                NoDropPlayerCheckbox.Checked = false;
                NoDropMonsterCheckbox.Checked = false;
                NoNamesCheckbox.Checked = false;

                NoHeroCheckBox.Checked = false;

                FightCheckbox.Checked = false;
                FireCheckbox.Checked = false;
                LightningCheckbox.Checked = false;
                NoReconnectTextbox.Text = string.Empty;
                FireTextbox.Text = string.Empty;
                LightningTextbox.Text = string.Empty;
                MapDarkLighttextBox.Text = string.Empty;
                //MineIndextextBox.Text = string.Empty;
                return;
            }


            MapInfo mi = _selectedMapInfos[0];

            MapTabs.Enabled = true;

            MapIndexTextBox.Text = mi.Index.ToString();
            FileNameTextBox.Text = mi.FileName;
            MapNameTextBox.Text = mi.Title;
            MiniMapTextBox.Text = mi.MiniMap.ToString();
            BigMapTextBox.Text = mi.BigMap.ToString();
            LightsComboBox.SelectedItem = mi.Light;
            MineComboBox.SelectedIndex = mi.MineIndex;
            MusicTextBox.Text = mi.Music.ToString();

            //map attributes
            NoTeleportCheckbox.Checked = mi.NoTeleport;
            NoReconnectCheckbox.Checked = mi.NoReconnect;
            NoReconnectTextbox.Text = mi.NoReconnectMap;
            NoRandomCheckbox.Checked = mi.NoRandom;
            NoEscapeCheckbox.Checked = mi.NoEscape;
            NoRecallCheckbox.Checked = mi.NoRecall;
            NoDrugCheckbox.Checked = mi.NoDrug;
            NoPositionCheckbox.Checked = mi.NoPosition;
            NoHeroCheckBox.Checked = mi.NoHero;
            GTBox.Checked = mi.GT;
            NoThrowItemCheckbox.Checked = mi.NoThrowItem;
            NoDropPlayerCheckbox.Checked = mi.NoDropPlayer;
            NoDropMonsterCheckbox.Checked = mi.NoDropMonster;
            NoNamesCheckbox.Checked = mi.NoNames;
            FightCheckbox.Checked = mi.Fight;
            NoFightCheckbox.Checked = mi.NoFight;
            SafeZoneCheckbox.Checked = mi.SafeZone;
            FireCheckbox.Checked = mi.Fire;
            FireTextbox.Text = mi.FireDamage.ToString();
            LightningCheckbox.Checked = mi.Lightning;
            LightningTextbox.Text = mi.LightningDamage.ToString();
            MapDarkLighttextBox.Text = mi.MapDarkLight.ToString();

            NoMountCheckbox.Checked = mi.NoMount;
            NeedBridleCheckbox.Checked = mi.NeedBridle;
            //MineIndextextBox.Text = mi.MineIndex.ToString();

            for (int i = 1; i < _selectedMapInfos.Count; i++)
            {
                mi = _selectedMapInfos[i];

                if (MapIndexTextBox.Text != mi.Index.ToString()) MapIndexTextBox.Text = string.Empty;
                if (FileNameTextBox.Text != mi.FileName) FileNameTextBox.Text = string.Empty;
                if (MapNameTextBox.Text != mi.Title) MapNameTextBox.Text = string.Empty;
                if (MiniMapTextBox.Text != mi.MiniMap.ToString()) MiniMapTextBox.Text = string.Empty;
                if (BigMapTextBox.Text != mi.BigMap.ToString()) BigMapTextBox.Text = string.Empty;
                if (LightsComboBox.SelectedItem == null || (LightSetting)LightsComboBox.SelectedItem != mi.Light) LightsComboBox.SelectedItem = null;
                if (MineComboBox.SelectedItem == null || MineComboBox.SelectedIndex != mi.MineIndex) MineComboBox.SelectedIndex = 1;
                if (MusicTextBox.Text != mi.Music.ToString()) MusicTextBox.Text = string.Empty;

                //map attributes
                if (NoTeleportCheckbox.Checked != mi.NoTeleport) NoTeleportCheckbox.Checked = false;
                if (NoReconnectCheckbox.Checked != mi.NoReconnect) NoReconnectCheckbox.Checked = false;
                if (NoReconnectTextbox.Text != mi.NoReconnectMap) NoReconnectTextbox.Text = string.Empty;
                if (NoRandomCheckbox.Checked != mi.NoRandom) NoRandomCheckbox.Checked = false;
                if (NoEscapeCheckbox.Checked != mi.NoEscape) NoEscapeCheckbox.Checked = false;
                if (NoRecallCheckbox.Checked != mi.NoRecall) NoRecallCheckbox.Checked = false;
                if (NoDrugCheckbox.Checked != mi.NoDrug) NoDrugCheckbox.Checked = false;
                if (NoPositionCheckbox.Checked != mi.NoPosition) NoPositionCheckbox.Checked = false;
                if (NoHeroCheckBox.Checked != mi.NoHero) NoHeroCheckBox.Checked = false;
                if (GTBox.Checked != mi.GT) GTBox.Checked = false;
                if (NoThrowItemCheckbox.Checked != mi.NoThrowItem) NoThrowItemCheckbox.Checked = false;
                if (NoDropPlayerCheckbox.Checked != mi.NoDropPlayer) NoDropPlayerCheckbox.Checked = false;
                if (NoDropMonsterCheckbox.Checked != mi.NoDropMonster) NoDropMonsterCheckbox.Checked = false;
                if (NoNamesCheckbox.Checked != mi.NoNames) NoNamesCheckbox.Checked = false;
                if (FightCheckbox.Checked != mi.Fight) FightCheckbox.Checked = false;
                if (NoFightCheckbox.Checked != mi.NoFight) NoFightCheckbox.Checked = false;
                if (SafeZoneCheckbox.Checked != mi.SafeZone) SafeZoneCheckbox.Checked = false;
                if (FireCheckbox.Checked != mi.Fire) FireCheckbox.Checked = false;
                if (FireTextbox.Text != mi.FireDamage.ToString()) FireTextbox.Text = string.Empty;
                if (LightningCheckbox.Checked != mi.Lightning) LightningCheckbox.Checked = false;
                if (LightningTextbox.Text != mi.LightningDamage.ToString()) LightningTextbox.Text = string.Empty;
                if (MapDarkLighttextBox.Text != mi.MapDarkLight.ToString()) MapDarkLighttextBox.Text = string.Empty;

                if (NoMountCheckbox.Checked != mi.NoMount) NoMountCheckbox.Checked = false;
                if (NeedBridleCheckbox.Checked != mi.NeedBridle) NeedBridleCheckbox.Checked = false;
            }

            UpdateSafeZoneInterface();
            UpdateRespawnInterface();
            UpdateMovementInterface();
            UpdateMineZoneInterface();
        }
        private void UpdateSafeZoneInterface()
        {
            if (_selectedMapInfos.Count != 1)
            {
                SafeZoneInfoListBox.Items.Clear();
                if (_selectedSafeZoneInfos != null && _selectedSafeZoneInfos.Count > 0)
                    _selectedSafeZoneInfos.Clear();
                _info = null;

                SafeZoneInfoPanel.Enabled = false;

                SZXTextBox.Text = string.Empty;
                SZYTextBox.Text = string.Empty;
                StartPointCheckBox.CheckState = CheckState.Unchecked;
                return;
            }

            if (_info != _selectedMapInfos[0])
            {
                SafeZoneInfoListBox.Items.Clear();
                _info = _selectedMapInfos[0];
            }

            if (SafeZoneInfoListBox.Items.Count != _info.SafeZones.Count)
            {
                SafeZoneInfoListBox.Items.Clear();
                for (int i = 0; i < _info.SafeZones.Count; i++) SafeZoneInfoListBox.Items.Add(_info.SafeZones[i]);
            }
            _selectedSafeZoneInfos = SafeZoneInfoListBox.SelectedItems.Cast<SafeZoneInfo>().ToList();

            if (_selectedSafeZoneInfos.Count == 0)
            {
                SafeZoneInfoPanel.Enabled = false;

                SZXTextBox.Text = string.Empty;
                SZYTextBox.Text = string.Empty;
                StartPointCheckBox.CheckState = CheckState.Unchecked;
                return;
            }

            SafeZoneInfo info = _selectedSafeZoneInfos[0];
            SafeZoneInfoPanel.Enabled = true;

            SZXTextBox.Text = info.Location.X.ToString();
            SZYTextBox.Text = info.Location.Y.ToString();
            StartPointCheckBox.CheckState = info.StartPoint ? CheckState.Checked : CheckState.Unchecked;
            SizeTextBox.Text = info.Size.ToString();


            for (int i = 1; i < _selectedSafeZoneInfos.Count; i++)
            {
                info = _selectedSafeZoneInfos[i];

                if (SZXTextBox.Text != info.Location.X.ToString()) SZXTextBox.Text = string.Empty;
                if (SZYTextBox.Text != info.Location.Y.ToString()) SZYTextBox.Text = string.Empty;
                if (StartPointCheckBox.Checked != info.StartPoint) StartPointCheckBox.CheckState = CheckState.Indeterminate;
                if (SizeTextBox.Text != info.Size.ToString()) SizeTextBox.Text = string.Empty;
            }
        }
        private void UpdateRespawnInterface()
        {
            if (_selectedMapInfos.Count != 1)
            {
                RespawnInfoListBox.Items.Clear();
                if (_selectedRespawnInfos != null && _selectedRespawnInfos.Count > 0)
                    _selectedRespawnInfos.Clear();
                _info = null;

                RespawnInfoPanel.Enabled = false;

                MonsterInfoComboBox.SelectedItem = null;
                RXTextBox.Text = string.Empty;
                RYTextBox.Text = string.Empty;
                CountTextBox.Text = string.Empty;
                SpreadTextBox.Text = string.Empty;
                DelayTextBox.Text = string.Empty;
                chkrespawnsave.Enabled = false;
                chkRespawnEnableTick.Checked = false;
                chkrespawnsave.Checked = false;
                DirectionTextBox.Text = string.Empty;
                RoutePathTextBox.Text = string.Empty;
                Randomtextbox.Text = string.Empty;
                return;
            }

            if (_info != _selectedMapInfos[0])
            {
                RespawnInfoListBox.Items.Clear();
                _info = _selectedMapInfos[0];
            }

            if (RespawnInfoListBox.Items.Count != _info.Respawns.Count)
            {
                RespawnInfoListBox.Items.Clear();
                for (int i = 0; i < _info.Respawns.Count; i++) RespawnInfoListBox.Items.Add(_info.Respawns[i]);
            }
            _selectedRespawnInfos = RespawnInfoListBox.SelectedItems.Cast<RespawnInfo>().ToList();

            if (_selectedRespawnInfos.Count == 0)
            {
                RespawnInfoPanel.Enabled = false;

                MonsterInfoComboBox.SelectedItem = null;
                RXTextBox.Text = string.Empty;
                RYTextBox.Text = string.Empty;
                CountTextBox.Text = string.Empty;
                SpreadTextBox.Text = string.Empty;
                DelayTextBox.Text = string.Empty;
                chkrespawnsave.Enabled = false;
                chkRespawnEnableTick.Checked = false;
                chkrespawnsave.Checked = false;
                DirectionTextBox.Text = string.Empty;
                RoutePathTextBox.Text = string.Empty;
                Randomtextbox.Text = string.Empty;
                return;
            }

            RespawnInfo info = _selectedRespawnInfos[0];
            RespawnInfoPanel.Enabled = true;

            MonsterInfoComboBox.SelectedItem = MotherParent.MonsterPanel.MonsterInfoList.FirstOrDefault(x => x.Index == info.MonsterIndex);
            RXTextBox.Text = info.Location.X.ToString();
            RYTextBox.Text = info.Location.Y.ToString();
            CountTextBox.Text = info.Count.ToString();
            SpreadTextBox.Text = info.Spread.ToString();
            DelayTextBox.Text = info.RespawnTicks == 0 ? info.Delay.ToString() : info.RespawnTicks.ToString();
            chkrespawnsave.Enabled = info.RespawnTicks != 0;
            chkRespawnEnableTick.Checked = info.RespawnTicks != 0;
            chkrespawnsave.Checked = ((info.RespawnTicks != 0) && (info.SaveRespawnTime)) ? true : false;
            DirectionTextBox.Text = info.Direction.ToString();
            RoutePathTextBox.Text = info.RoutePath;
            Randomtextbox.Enabled = info.RespawnTicks == 0;
            Randomtextbox.Text = info.RandomDelay.ToString();

            for (int i = 1; i < _selectedRespawnInfos.Count; i++)
            {
                info = _selectedRespawnInfos[i];

                if (MonsterInfoComboBox.SelectedItem != MotherParent.MonsterPanel.MonsterInfoList.FirstOrDefault(x => x.Index == info.MonsterIndex)) MonsterInfoComboBox.SelectedItem = null;
                if (RXTextBox.Text != info.Location.X.ToString()) RXTextBox.Text = string.Empty;
                if (RYTextBox.Text != info.Location.Y.ToString()) RYTextBox.Text = string.Empty;
                if (CountTextBox.Text != info.Count.ToString()) CountTextBox.Text = string.Empty;
                if (SpreadTextBox.Text != info.Spread.ToString()) SpreadTextBox.Text = string.Empty;
                if (chkRespawnEnableTick.Checked != (info.RespawnTicks == 0))
                {
                    DelayTextBox.Text = string.Empty;
                    chkrespawnsave.Enabled = false;
                    chkrespawnsave.Checked = false;
                    Randomtextbox.Text = string.Empty;
                }
                else
                {
                    if (chkRespawnEnableTick.Checked)
                    {
                        if (DelayTextBox.Text != info.RespawnTicks.ToString()) DelayTextBox.Text = string.Empty;
                        if (chkrespawnsave.Checked != info.SaveRespawnTime)
                        {
                            chkrespawnsave.Enabled = false;
                            chkrespawnsave.Checked = false;
                        }
                        Randomtextbox.Enabled = false;
                        Randomtextbox.Text = string.Empty;
                    }
                    else
                    {
                        if (DelayTextBox.Text != info.Delay.ToString()) DelayTextBox.Text = string.Empty;
                        chkrespawnsave.Enabled = false;
                        chkrespawnsave.Checked = false;
                        if (Randomtextbox.Text != info.RandomDelay.ToString()) Randomtextbox.Text = string.Empty;
                    }
                }
                if (DirectionTextBox.Text != info.Direction.ToString()) DirectionTextBox.Text = string.Empty;
                if (RoutePathTextBox.Text != info.RoutePath) RoutePathTextBox.Text = string.Empty;
            }
        }
        private void UpdateMovementInterface()
        {
            if (_selectedMapInfos.Count != 1)
            {
                MovementInfoListBox.Items.Clear();
                if (_selectedMovementInfos != null && _selectedMovementInfos.Count > 0)
                    _selectedMovementInfos.Clear();
                _info = null;

                MovementInfoPanel.Enabled = false;

                SourceXTextBox.Text = string.Empty;
                SourceYTextBox.Text = string.Empty;
                DestMapComboBox.SelectedItem = null;
                ConquestComboBox.SelectedIndex = 0;
                DestXTextBox.Text = string.Empty;
                DestYTextBox.Text = string.Empty;
                return;
            }

            if (_info != _selectedMapInfos[0])
            {
                MovementInfoListBox.Items.Clear();
                _info = _selectedMapInfos[0];
            }

            if (MovementInfoListBox.Items.Count != _info.Movements.Count)
            {
                MovementInfoListBox.Items.Clear();
                for (int i = 0; i < _info.Movements.Count; i++) MovementInfoListBox.Items.Add(_info.Movements[i]);
            }
            _selectedMovementInfos = MovementInfoListBox.SelectedItems.Cast<MovementInfo>().ToList();

            if (_selectedMovementInfos.Count == 0)
            {
                MovementInfoPanel.Enabled = false;

                SourceXTextBox.Text = string.Empty;
                SourceYTextBox.Text = string.Empty;
                NeedHoleMCheckBox.Checked = false;
                NeedMoveMCheckBox.Checked = false;
                DestMapComboBox.SelectedItem = null;
                ConquestComboBox.SelectedIndex = 0;
                DestXTextBox.Text = string.Empty;
                DestYTextBox.Text = string.Empty;
                return;
            }

            MovementInfo info = _selectedMovementInfos[0];

            MovementInfoPanel.Enabled = true;

            SourceXTextBox.Text = info.Source.X.ToString();
            SourceYTextBox.Text = info.Source.Y.ToString();
            NeedHoleMCheckBox.Checked = info.NeedHole;
            NeedMoveMCheckBox.Checked = info.NeedMove;
            DestMapComboBox.SelectedItem = MapInfoList.FirstOrDefault(x => x.Index == info.MapIndex);
            DestXTextBox.Text = info.Destination.X.ToString();
            DestYTextBox.Text = info.Destination.Y.ToString();

            ConquestComboBox.SelectedItem = MotherParent.ConquestPanel.ConquestInfos.FirstOrDefault(x => x.Index == info.ConquestIndex);
            if (ConquestComboBox.SelectedItem == null) ConquestComboBox.SelectedIndex = 0;

            for (int i = 1; i < _selectedMovementInfos.Count; i++)
            {
                info = _selectedMovementInfos[i];

                SourceXTextBox.Text = info.Source.X.ToString();
                SourceYTextBox.Text = info.Source.Y.ToString();
                DestMapComboBox.SelectedItem = MapInfoList.FirstOrDefault(x => x.Index == info.MapIndex);
                DestXTextBox.Text = info.Destination.X.ToString();
                DestYTextBox.Text = info.Destination.Y.ToString();
                ConquestComboBox.SelectedItem = MotherParent.ConquestPanel.ConquestInfos.FirstOrDefault(x => x.Index == info.ConquestIndex);

                if (SourceXTextBox.Text != info.Source.X.ToString()) SourceXTextBox.Text = string.Empty;
                if (SourceYTextBox.Text != info.Source.Y.ToString()) SourceYTextBox.Text = string.Empty;

                if (DestMapComboBox.SelectedItem != MapInfoList.FirstOrDefault(x => x.Index == info.MapIndex)) DestMapComboBox.SelectedItem = null;

                if (DestXTextBox.Text != info.Destination.X.ToString()) DestXTextBox.Text = string.Empty;
                if (DestYTextBox.Text != info.Destination.Y.ToString()) DestYTextBox.Text = string.Empty;

                if (ConquestComboBox.SelectedItem != MotherParent.ConquestPanel.ConquestInfos.FirstOrDefault(x => x.Index == info.ConquestIndex)) ConquestComboBox.SelectedItem = null;
            }

        }

        private void RefreshSafeZoneList()
        {
            SafeZoneInfoListBox.SelectedIndexChanged -= SafeZoneInfoListBox_SelectedIndexChanged;

            List<bool> selected = new List<bool>();

            for (int i = 0; i < SafeZoneInfoListBox.Items.Count; i++) selected.Add(SafeZoneInfoListBox.GetSelected(i));
            SafeZoneInfoListBox.Items.Clear();
            for (int i = 0; i < _info.SafeZones.Count; i++) SafeZoneInfoListBox.Items.Add(_info.SafeZones[i]);
            for (int i = 0; i < selected.Count; i++) SafeZoneInfoListBox.SetSelected(i, selected[i]);

            SafeZoneInfoListBox.SelectedIndexChanged += SafeZoneInfoListBox_SelectedIndexChanged;
        }
        private void RefreshRespawnList()
        {
            RespawnInfoListBox.SelectedIndexChanged -= RespawnInfoListBox_SelectedIndexChanged;

            List<bool> selected = new List<bool>();

            for (int i = 0; i < RespawnInfoListBox.Items.Count; i++) selected.Add(RespawnInfoListBox.GetSelected(i));
            RespawnInfoListBox.Items.Clear();
            for (int i = 0; i < _info.Respawns.Count; i++) RespawnInfoListBox.Items.Add(_info.Respawns[i]);
            for (int i = 0; i < selected.Count; i++) RespawnInfoListBox.SetSelected(i, selected[i]);

            RespawnInfoListBox.SelectedIndexChanged += RespawnInfoListBox_SelectedIndexChanged;
        }
        private void RefreshMovementList()
        {
            MovementInfoListBox.SelectedIndexChanged -= MovementInfoListBox_SelectedIndexChanged;

            List<bool> selected = new List<bool>();

            for (int i = 0; i < MovementInfoListBox.Items.Count; i++) selected.Add(MovementInfoListBox.GetSelected(i));
            MovementInfoListBox.Items.Clear();
            for (int i = 0; i < _info.Movements.Count; i++) MovementInfoListBox.Items.Add(_info.Movements[i]);
            for (int i = 0; i < selected.Count; i++) MovementInfoListBox.SetSelected(i, selected[i]);

            MovementInfoListBox.SelectedIndexChanged += MovementInfoListBox_SelectedIndexChanged;
        }

        private void RefreshMineZoneList()
        {

            MZListlistBox.SelectedIndexChanged -= MZListlistBox_SelectedIndexChanged;

            List<bool> selected = new List<bool>();

            for (int i = 0; i < MZListlistBox.Items.Count; i++) selected.Add(MZListlistBox.GetSelected(i));
            MZListlistBox.Items.Clear();
            for (int i = 0; i < _info.MineZones.Count; i++) MZListlistBox.Items.Add(_info.MineZones[i]);
            for (int i = 0; i < selected.Count; i++) MZListlistBox.SetSelected(i, selected[i]);
            MZListlistBox.SelectedIndexChanged += MZListlistBox_SelectedIndexChanged;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            MapInfoList.Add(new MapInfo { Index = ++MotherParent.MapIndex });
            UpdateInterface();
        }
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (_selectedMapInfos.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to remove the selected maps?", "Remove Maps?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++) MapInfoList.Remove(_selectedMapInfos[i]);

            if (MapInfoList.Count == 0) MotherParent.MapIndex = 0;

            MapTabs.SelectTab(0);

            UpdateInterface();
        }
        private void MapInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SafeZoneInfoListBox.Items.Clear();
            RespawnInfoListBox.Items.Clear();
            MovementInfoListBox.Items.Clear();
            MZListlistBox.Items.Clear();
            UpdateInterface();
        }
        private void FileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].FileName = ActiveControl.Text;
        }
        private void MapNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].Title = ActiveControl.Text;

            UpdateList();
        }
        private void MiniMapTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].MiniMap = temp;
        }
        private void LightsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].Light = (LightSetting)LightsComboBox.SelectedItem;
        }


        private void AddSZButton_Click(object sender, EventArgs e)
        {
            if (_info == null) return;

            _info.CreateSafeZone();
            UpdateSafeZoneInterface();
        }
        private void RemoveSZButton_Click(object sender, EventArgs e)
        {
            if (_selectedSafeZoneInfos.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to remove the selected SafeZones?", "Remove SafeZones?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            for (int i = 0; i < _selectedSafeZoneInfos.Count; i++) _info.SafeZones.Remove(_selectedSafeZoneInfos[i]);

            UpdateSafeZoneInterface();
        }
        private void SafeZoneInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSafeZoneInterface();
        }
        private void SZXTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedSafeZoneInfos.Count; i++)
                _selectedSafeZoneInfos[i].Location.X = temp;

            RefreshSafeZoneList();
        }
        private void SZYTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedSafeZoneInfos.Count; i++)
                _selectedSafeZoneInfos[i].Location.Y = temp;

            RefreshSafeZoneList();
        }
        private void SizeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedSafeZoneInfos.Count; i++)
                _selectedSafeZoneInfos[i].Size = temp;
        }
        private void StartPointCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedSafeZoneInfos.Count; i++)
                _selectedSafeZoneInfos[i].StartPoint = StartPointCheckBox.Checked;

            RefreshSafeZoneList();
        }



        private void AddRButton_Click(object sender, EventArgs e)
        {
            if (_info == null) return;

            _info.Respawns.Add(new RespawnInfo { RespawnIndex = ++MotherParent.RespawnIndex });
            UpdateRespawnInterface();
        }
        private void RemoveRButton_Click(object sender, EventArgs e)
        {
            if (_selectedRespawnInfos.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to remove the selected Respawns?", "Remove Respawns?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            for (int i = 0; i < _selectedRespawnInfos.Count; i++) _info.Respawns.Remove(_selectedRespawnInfos[i]);

            UpdateRespawnInterface();
        }
        private void RespawnInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRespawnInterface();
        }
        private void MonsterInfoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            MonsterInfo info = MonsterInfoComboBox.SelectedItem as MonsterInfo;

            if (info == null) return;

            for (int i = 0; i < _selectedRespawnInfos.Count; i++)
                _selectedRespawnInfos[i].MonsterIndex = info.Index;

            RefreshRespawnList();

        }
        private void RXTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedRespawnInfos.Count; i++)
                _selectedRespawnInfos[i].Location.X = temp;

            RefreshRespawnList();
        }
        private void RYTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedRespawnInfos.Count; i++)
                _selectedRespawnInfos[i].Location.Y = temp;

            RefreshRespawnList();
        }
        private void CountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedRespawnInfos.Count; i++)
                _selectedRespawnInfos[i].Count = temp;

            RefreshRespawnList();
        }
        private void SpreadTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedRespawnInfos.Count; i++)
                _selectedRespawnInfos[i].Spread = temp;

            RefreshRespawnList();
        }
        private void DelayTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedRespawnInfos.Count; i++)
            {
                if (chkRespawnEnableTick.Checked)
                {
                    _selectedRespawnInfos[i].RespawnTicks = Math.Max((ushort)1, temp);//you can never have respawnticks set to 0 or it would bug the entire thing really
                    _selectedRespawnInfos[i].Delay = 0;
                }
                else
                {
                    _selectedRespawnInfos[i].Delay = temp;
                    _selectedRespawnInfos[i].RespawnTicks = 0;
                }
            }

            RefreshRespawnList();
        }
        private void DirectionTextBox_TextChanged(object sender, EventArgs e)
        {

            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedRespawnInfos.Count; i++)
                _selectedRespawnInfos[i].Direction = temp;

            RefreshRespawnList();
        }

        private void RoutePathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedRespawnInfos.Count; i++)
                _selectedRespawnInfos[i].RoutePath = ActiveControl.Text;

            RefreshRespawnList();
        }

        private void RPasteButton_Click(object sender, EventArgs e)
        {

        }

        private void AddMButton_Click(object sender, EventArgs e)
        {
            if (_info == null) return;

            _info.CreateMovementInfo();
            UpdateMovementInterface();
        }
        private void RemoveMButton_Click(object sender, EventArgs e)
        {
            if (_selectedMovementInfos.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to remove the selected Movements?", "Remove Movements?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            for (int i = 0; i < _selectedMovementInfos.Count; i++) _info.Movements.Remove(_selectedMovementInfos[i]);

            UpdateMovementInterface();
        }
        private void SourceXTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMovementInfos.Count; i++)
                _selectedMovementInfos[i].Source.X = temp;

            RefreshMovementList();
        }
        private void SourceYTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMovementInfos.Count; i++)
                _selectedMovementInfos[i].Source.Y = temp;

            RefreshMovementList();
        }
        private void DestXTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMovementInfos.Count; i++)
                _selectedMovementInfos[i].Destination.X = temp;

            RefreshMovementList();
        }
        private void DestYTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMovementInfos.Count; i++)
                _selectedMovementInfos[i].Destination.Y = temp;

            RefreshMovementList();
        }

        private void NeedHoleMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMovementInfos.Count; i++)
                _selectedMovementInfos[i].NeedHole = NeedHoleMCheckBox.Checked;
        }

        private void NeedScriptMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMovementInfos.Count; i++)
                _selectedMovementInfos[i].NeedMove = NeedMoveMCheckBox.Checked;
        }

        private void MovementInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void PasteMapButton_Click(object sender, EventArgs e)
        {

        }

        private void BigMapTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].BigMap = temp;
        }



        private void NoTeleportCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoTeleport = NoTeleportCheckbox.Checked;
        }
        private void NoReconnectCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoReconnect = NoReconnectCheckbox.Checked;
        }
        private void NoReconnectTextbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoReconnectMap = ActiveControl.Text;
        }
        private void NoRandomCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoRandom = NoRandomCheckbox.Checked;
        }
        private void NoEscapeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoEscape = NoEscapeCheckbox.Checked;
        }
        private void NoRecallCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoRecall = NoRecallCheckbox.Checked;
        }
        private void NoDrugCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoDrug = NoDrugCheckbox.Checked;
        }
        private void NoPositionCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoPosition = NoPositionCheckbox.Checked;
        }
        private void NoThrowItemCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoThrowItem = NoThrowItemCheckbox.Checked;
        }
        private void NoDropPlayerCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoDropPlayer = NoDropPlayerCheckbox.Checked;
        }
        private void NoDropMonsterCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoDropMonster = NoDropMonsterCheckbox.Checked;
        }
        private void NoNamesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoNames = NoNamesCheckbox.Checked;
        }
        private void FightCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].Fight = FightCheckbox.Checked;
        }
        private void FireCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].Fire = FireCheckbox.Checked;
        }
        private void FireTextbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].FireDamage = temp;
        }
        private void LightningCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].Lightning = LightningCheckbox.Checked;
        }
        private void LightningTextbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].LightningDamage = temp;
        }

        private void ClearHButton_Click(object sender, EventArgs e)
        {

        }

        private void MapDarkLighttextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!byte.TryParse(ActiveControl.Text, out byte temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].MapDarkLight = temp;
        }

        private void MZDeletebutton_Click(object sender, EventArgs e)
        {
            if (_selectedMineZones.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to remove the selected MineZones?", "Remove MineZones?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            for (int i = 0; i < _selectedMineZones.Count; i++) _info.MineZones.Remove(_selectedMineZones[i]);
            UpdateMineZoneInterface();
        }

        private void MZAddbutton_Click(object sender, EventArgs e)
        {
            if (_info == null) return;

            _info.MineZones.Add(new MineZone());
            UpdateMineZoneInterface();
        }

        private void MZListlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMineZoneInterface();
        }

        private void MZMineIndextextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if ((!byte.TryParse(ActiveControl.Text, out byte temp)) || (Settings.MineSetList.Count < temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMineZones.Count; i++)
                _selectedMineZones[i].Mine = temp;
            RefreshMineZoneList();
        }

        private void MZXtextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMineZones.Count; i++)
                _selectedMineZones[i].Location.X = temp;
            RefreshMineZoneList();
        }

        private void MZYtextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!int.TryParse(ActiveControl.Text, out int temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMineZones.Count; i++)
                _selectedMineZones[i].Location.Y = temp;
            RefreshMineZoneList();
        }

        private void MZSizetextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMineZones.Count; i++)
                _selectedMineZones[i].Size = temp;
            RefreshMineZoneList();
        }
        private void ImportMapInfoButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Binary File|*.dat";
            ofd.ShowDialog();

            if (ofd.FileName == string.Empty) return;

            string Path = ofd.FileName;
            if (!File.Exists(Path))
            {
                MessageBox.Show("File could not be found!");
                return;
            }
            List<MapInfo> list = new List<MapInfo>();
            using (FileStream stream = File.OpenRead(Path))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        list.Add(new MapInfo(reader));
                    }
                }
            }
            //  No function to update MapInfo due to no unique values will be held by the exports.
            //  Indexes will mis-match and duplicate names will create instances?
            int added = 0;
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Index = ++MotherParent.MapIndex;
                    MapInfoList.Add(list[i]);
                    added++;
                }
            }
            MessageBox.Show(string.Format("{0} Maps added.", added));
            UpdateInterface();

            MessageBox.Show(string.Format("{0} Maps imported", added));
        }

        private void ImportMonGenButton_Click(object sender, EventArgs e)
        {
            
        }
        private void ExportMapInfoButton_Click(object sender, EventArgs e)
        {
            using (FileStream stream = File.Create(Settings.ExportPath + string.Format("MapInfo-{0:dd-MM-yyyy_hh-mm-ss-tt}.dat", DateTime.Now)))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    //  As there's no export all button we'll make it so when you have nothing selected it'll export all MapInfos.
                    if (_selectedMapInfos.Count == 0)
                    {
                        writer.Write(MapInfoList.Count);
                        for (int i = 0; i < MapInfoList.Count; i++)
                            MapInfoList[i].Save(writer);
                    }
                    else
                    {

                        writer.Write(_selectedMapInfos.Count);
                        for (int i = 0; i < _selectedMapInfos.Count; i++)
                            _selectedMapInfos[i].Save(writer);
                    }
                }
            }
            MessageBox.Show(string.Format("{0} Map Info's exported", _selectedMapInfos.Count == 0 ? MapInfoList.Count : _selectedMapInfos.Count));
        }
        private void ExportMonGenButton_Click(object sender, EventArgs e)
        {
            if (_selectedMapInfos.Count == 0) return;

            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Settings.ExportPath,
                Filter = "Text File|*.txt"
            };
            sfd.ShowDialog();

            if (sfd.FileName == string.Empty) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
            {
                using (StreamWriter sw = File.AppendText(sfd.FileNames[0]))
                {
                    for (int j = 0; j < _selectedMapInfos[i].Respawns.Count; j++)
                    {
                        MonsterInfo mob = MotherParent.GetMonsterInfo(_selectedMapInfos[i].Respawns[j].MonsterIndex);

                        if (mob == null) continue;

                        string Output = string.Format("{0} {1} {2} {3} {4} {5} {6} {7}",
                            _selectedMapInfos[i].FileName,
                            _selectedMapInfos[i].Respawns[j].Location.X,
                            _selectedMapInfos[i].Respawns[j].Location.Y,
                            mob.Name.Replace(' ', '*'),
                           _selectedMapInfos[i].Respawns[j].Spread,
                           _selectedMapInfos[i].Respawns[j].Count,
                           _selectedMapInfos[i].Respawns[j].Delay,
                           _selectedMapInfos[i].Respawns[j].Direction);

                        sw.WriteLine(Output);
                    }
                }
            }
            MessageBox.Show("MonGen Export complete");
        }

        private void MineComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].MineIndex = Convert.ToByte(MineComboBox.SelectedIndex);
        }

        private void MineZoneComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMineZones.Count; i++)
                _selectedMineZones[i].Mine = Convert.ToByte(MineZoneComboBox.SelectedIndex);

            RefreshMineZoneList();
        }

        private void NoMountCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoMount = NoMountCheckbox.Checked;
        }

        private void NeedBridleCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NeedBridle = NeedBridleCheckbox.Checked;
        }

        private void NoFightCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoFight = NoFightCheckbox.Checked;
        }

        private void MusicTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }


            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].Music = temp;
        }

        private void RandomTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            if (!ushort.TryParse(ActiveControl.Text, out ushort temp))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;


            for (int i = 0; i < _selectedRespawnInfos.Count; i++)
                _selectedRespawnInfos[i].RandomDelay = temp;

            RefreshRespawnList();
        }

        private void chkRespawnEnableTick_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (chkRespawnEnableTick.Checked)
            {
                chkrespawnsave.Enabled = true;
                for (int i = 0; i < _selectedRespawnInfos.Count; i++)
                {
                    _selectedRespawnInfos[i].RespawnTicks = _selectedRespawnInfos[i].Delay;
                    _selectedRespawnInfos[i].Delay = 0;
                }
            }
            else
            {
                chkrespawnsave.Enabled = false;
                for (int i = 0; i < _selectedRespawnInfos.Count; i++)
                {
                    _selectedRespawnInfos[i].Delay = _selectedRespawnInfos[i].RespawnTicks;
                    _selectedRespawnInfos[i].RespawnTicks = 0;
                }
            }
            RefreshRespawnList();

        }

        private void chkrespawnsave_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;
            if (chkRespawnEnableTick.Checked)
            {
                for (int i = 0; i < _selectedRespawnInfos.Count; i++)
                {
                    _selectedRespawnInfos[i].SaveRespawnTime = chkrespawnsave.Checked;
                }
            }
            RefreshRespawnList();
        }

        private void ConquestComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            ConquestInfo info = ConquestComboBox.SelectedItem as ConquestInfo;

            if (info == null) return;

            for (int i = 0; i < _selectedMovementInfos.Count; i++)
                _selectedMovementInfos[i].ConquestIndex = info.Index;

            RefreshMovementList();
        }

        private void NoHeroCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].NoHero = NoHeroCheckBox.Checked;
        }

        private void GTBox_CheckedChanged(object sender, EventArgs e)
        {
            {
                if (ActiveControl != sender) return;

                for (int i = 0; i < _selectedMapInfos.Count; i++)
                    _selectedMapInfos[i].GT = GTBox.Checked;
            }
        }

        private void SafeZoneCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            for (int i = 0; i < _selectedMapInfos.Count; i++)
                _selectedMapInfos[i].SafeZone = SafeZoneCheckbox.Checked;
        }

        private void UpdateMineZoneInterface()
        {
            if (_selectedMapInfos.Count != 1)
            {
                MZListlistBox.Items.Clear();

                if (_selectedMineZones != null && _selectedMineZones.Count > 0)
                    _selectedMineZones.Clear();
                _info = null;

                MineZonepanel.Enabled = false;
                MZXtextBox.Text = string.Empty;
                MZYtextBox.Text = string.Empty;
                MineZoneComboBox.SelectedItem = null;
                MZSizetextBox.Text = string.Empty;
                return;
            }

            if (_info != _selectedMapInfos[0])
            {
                MZListlistBox.Items.Clear();
                _info = _selectedMapInfos[0];
            }
            if (MZListlistBox.Items.Count != _info.MineZones.Count)
            {
                MZListlistBox.Items.Clear();
                for (int i = 0; i < _info.MineZones.Count; i++) MZListlistBox.Items.Add(_info.MineZones[i]);
            }
            _selectedMineZones = MZListlistBox.SelectedItems.Cast<MineZone>().ToList();
            if (_selectedMineZones.Count == 0)
            {
                MineZonepanel.Enabled = false;
                MZXtextBox.Text = string.Empty;
                MZYtextBox.Text = string.Empty;
                MineZoneComboBox.SelectedItem = null;
                MZSizetextBox.Text = string.Empty;
                return;
            }
            MineZone info = _selectedMineZones[0];
            MineZonepanel.Enabled = true;

            MZXtextBox.Text = info.Location.X.ToString();
            MZYtextBox.Text = info.Location.Y.ToString();
            MineZoneComboBox.SelectedIndex = info.Mine;
            MZSizetextBox.Text = info.Size.ToString();

            for (int i = 1; i < _selectedMineZones.Count; i++)
            {
                info = _selectedMineZones[i];

                if (MZXtextBox.Text != info.Location.X.ToString()) MZXtextBox.Text = string.Empty;
                if (MZYtextBox.Text != info.Location.Y.ToString()) MZYtextBox.Text = string.Empty;
                if (MineComboBox.SelectedIndex != info.Mine) MineComboBox.SelectedIndex = 1;
                if (MZSizetextBox.Text != info.Size.ToString()) MZSizetextBox.Text = string.Empty;
            }
        }
        public void UpdateList()
        {
            MapInfoListBox.Items.Clear();
            for (int i = 0; i < MapInfoList.Count; i++)
                MapInfoListBox.Items.Add(MapInfoList[i]);
        }

        private void MovementInfoListBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            UpdateMovementInterface();
        }

        private void DestMapComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;

            MapInfo info = DestMapComboBox.SelectedItem as MapInfo;

            if (info == null) return;

            for (int i = 0; i < _selectedMovementInfos.Count; i++)
                _selectedMovementInfos[i].MapIndex = info.Index;

            RefreshMovementList();
        }
    }
}
