using Server.MirEnvir;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.Custom
{
    public partial class RaidDialog : Form
    {
        public Envir Envir
        {
            get { return SMain.EditEnvir; }
        }
        public RaidMap_Info _selectedRaid;
        public RaidItem_Info _selectedRaidReward;
        public ItemInfo _selectedItem;
        public RaidDialog()
        {
            InitializeComponent();
            for (int i = 0; i < Envir.ItemInfoList.Count; i++)
                pickItemCBox.Items.Add(Envir.ItemInfoList[i]);
            for (int i = 0; i < Envir.Raids.Count; i++)
                raidListBox.Items.Add(Envir.Raids[i]);
        }

        private void duration_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
           if (!byte.TryParse(ActiveControl.Text, out byte tmpB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.Duration = tmpB;
        }

        private void locY_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.StartLocation.Y = tmpI;
        }

        private void locX_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.StartLocation.X = tmpI;
        }

        private void lobbyMap_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.LobbyIndex = tmpI;
        }

        private void raidMap_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.MapIndex = tmpI;
        }

        private void oneLife_CheckedChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            _selectedRaid.OneLife = oneLife.Checked;
        }

        private void startMinute_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tmpB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.StartMinute = tmpB;
        }

        private void startHour_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tmpB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.StartHour = tmpB;
        }

        private void startDay_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tmpB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.StartDay = tmpB;
        }

        private void description_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (ActiveControl.Text.Length > 120)
                return;
            _selectedRaid.Description = ActiveControl.Text;
        }

        private void title_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (ActiveControl.Text.Contains(" "))
                return;
            if (ActiveControl.Text.Length > 28)
                return;
            _selectedRaid.Title = ActiveControl.Text;
            UpdateInterface(true);
        }

        public void UpdateInterface(bool refresh = false)
        {
            if (raidListBox.Items.Count != Envir.Raids.Count || refresh)
            {
                raidListBox.Items.Clear();
                for (int i = 0; i < Envir.Raids.Count; i++)
                    raidListBox.Items.Add(Envir.Raids[i]);
            }
            rewardListBox.Items.Clear();
            if (_selectedRaid != null)
            {                
                title.Text = _selectedRaid.Title;
                description.Text = _selectedRaid.Description;
                locX.Text = _selectedRaid.StartLocation.X.ToString();
                locY.Text = _selectedRaid.StartLocation.Y.ToString();
                startDay.Text = _selectedRaid.StartDay.ToString();
                startHour.Text = _selectedRaid.StartHour.ToString();
                startMinute.Text = _selectedRaid.StartMinute.ToString();
                duration.Text = _selectedRaid.Duration.ToString();
                raidMap.Text = _selectedRaid.MapIndex.ToString();
                lobbyMap.Text = _selectedRaid.LobbyIndex.ToString();
                oneLife.Checked = _selectedRaid.OneLife;
                sub0Index.Text = _selectedRaid.Sub0Index.ToString();
                sub1LocX.Text = _selectedRaid.BossAreas[0].X.ToString();
                sub1LocY.Text = _selectedRaid.BossAreas[0].Y.ToString();
                sub2Index.Text = _selectedRaid.Sub1Index.ToString();
                sub2LocX.Text = _selectedRaid.BossAreas[1].X.ToString();
                sub2LocY.Text = _selectedRaid.BossAreas[1].Y.ToString();
                bossIndex.Text = _selectedRaid.BossIndex.ToString();
                bossLocX.Text = _selectedRaid.BossAreas[2].X.ToString();
                bossLocY.Text = _selectedRaid.BossAreas[2].Y.ToString();
                if (_selectedRaid.ItemRewards != null &&
                    _selectedRaid.ItemRewards.Count > 0)
                {
                    for (int i = 0; i < _selectedRaid.ItemRewards.Count; i++)
                        rewardListBox.Items.Add(_selectedRaid.ItemRewards[i]);
                }
            }
            else
            {
                title.Text = string.Empty;
                description.Text = string.Empty;
                locX.Text = string.Empty;
                locY.Text = string.Empty;
                startDay.Text = string.Empty;
                startHour.Text = string.Empty;
                startMinute.Text = string.Empty;
                duration.Text = string.Empty;
                raidMap.Text = string.Empty;
                lobbyMap.Text = string.Empty;
                oneLife.Checked = false;
                sub0Index.Text = string.Empty;
                sub1LocX.Text = string.Empty;
                sub1LocY.Text = string.Empty;
                sub2Index.Text = string.Empty;
                sub2LocX.Text = string.Empty;
                sub2LocY.Text = string.Empty;
                bossIndex.Text = string.Empty;
                bossLocX.Text = string.Empty;
                bossLocY.Text = string.Empty;
            }
        }

        public void UpdateRewardInterface()
        {
            if (rewardListBox.Items.Count != _selectedRaid.ItemRewards.Count)
            {
                rewardListBox.Items.Clear();
                for (int i = 0; i < _selectedRaid.ItemRewards.Count; i++)
                    rewardListBox.Items.Add(_selectedRaid.ItemRewards[i]);
            }
            if (_selectedRaidReward != null)
            {
                minAC.Text = _selectedRaidReward.MinAC.ToString();
                maxAC.Text = _selectedRaidReward.MaxAC.ToString();
                minMAC.Text = _selectedRaidReward.MinMAC.ToString();
                maxMAC.Text = _selectedRaidReward.MaxMAC.ToString();
                minDC.Text = _selectedRaidReward.MinDC.ToString();
                maxDC.Text = _selectedRaidReward.MaxDC.ToString();
                minMC.Text = _selectedRaidReward.MinMC.ToString();
                maxMC.Text = _selectedRaidReward.MaxMC.ToString();
                minSC.Text = _selectedRaidReward.MinSC.ToString();
                maxSC.Text = _selectedRaidReward.MaxSC.ToString();
            }
            else
            {
                minAC.Text = string.Empty;
                maxAC.Text = string.Empty;
                minMAC.Text = string.Empty;
                maxMAC.Text = string.Empty;
                minDC.Text = string.Empty;
                maxDC.Text = string.Empty;
                minMC.Text = string.Empty;
                maxMC.Text = string.Empty;
                minSC.Text = string.Empty;
                maxSC.Text = string.Empty;
            }
        }

        private void addRaid_Click(object sender, EventArgs e)
        {
            Envir.CreateRaidInfo();
            UpdateInterface(true);
        }

        private void removeRaid_Click(object sender, EventArgs e)
        {
            Envir.Raids.Remove(_selectedRaid);
            UpdateInterface(true);
        }

        private void raidListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (raidListBox.SelectedItem != null)
                _selectedRaid = (RaidMap_Info)raidListBox.SelectedItem;
            UpdateInterface();
        }

        private void sub0Index_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.Sub0Index = tmpI;
        }

        private void sub1LocX_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.BossAreas[0].X = tmpI;
        }

        private void sub1LocY_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.BossAreas[0].Y = tmpI;
        }

        private void sub2Index_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.Sub1Index = tmpI;
        }

        private void sub2LocX_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.BossAreas[1].X = tmpI;
        }

        private void sub2LocY_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.BossAreas[1].Y = tmpI;
        }

        private void bossIndex_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.BossIndex = tmpI;
        }

        private void bossLocX_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.BossAreas[2].X = tmpI;
        }

        private void bossLocY_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!int.TryParse(ActiveControl.Text, out int tmpI))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaid.BossAreas[2].Y = tmpI;
        }

        private void removeReward_Click(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (_selectedRaidReward == null)
                return;
            _selectedRaid.ItemRewards.Remove(_selectedRaidReward);
        }

        private void addReward_Click(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (!selectResult.Visible)
                selectResult.Show();
        }

        private void rewardListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null)
                return;
            if (rewardListBox.SelectedItem != null)
            {
                _selectedRaidReward = (RaidItem_Info)rewardListBox.SelectedItem;
            }
            UpdateRewardInterface();
        }

        private void minAC_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null ||
                _selectedRaidReward == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort tmpUS))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaidReward.MinAC = tmpUS;
        }

        private void maxAC_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null ||
                _selectedRaidReward == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort tmpUS))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaidReward.MaxAC = tmpUS;
        }

        private void minMAC_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null ||
                _selectedRaidReward == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort tmpUS))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaidReward.MinMAC = tmpUS;
        }

        private void maxMAC_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null ||
                _selectedRaidReward == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort tmpUS))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaidReward.MaxMAC = tmpUS;
        }

        private void minDC_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null ||
                _selectedRaidReward == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort tmpUS))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaidReward.MinDC = tmpUS;
        }

        private void maxDC_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null ||
                _selectedRaidReward == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort tmpUS))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaidReward.MaxDC = tmpUS;
        }

        private void minMC_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null ||
                _selectedRaidReward == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort tmpUS))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaidReward.MinMC = tmpUS;
        }

        private void maxMC_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null ||
                _selectedRaidReward == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort tmpUS))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaidReward.MaxMC = tmpUS;
        }

        private void minSC_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null ||
                _selectedRaidReward == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort tmpUS))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaidReward.MinSC = tmpUS;
        }

        private void maxSC_TextChanged(object sender, EventArgs e)
        {
            if (_selectedRaid == null ||
                _selectedRaidReward == null)
                return;
            if (ActiveControl != sender)
                return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort tmpUS))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            _selectedRaidReward.MaxSC = tmpUS;
        }

        private void RaidDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Envir.SaveDB();
        }

        private void pickItemCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pickItemCBox.SelectedItem == null)
                return;
            _selectedItem = (ItemInfo)pickItemCBox.SelectedItem;
        }

        private void selectResultItem_Click(object sender, EventArgs e)
        {
            if (selectResult.Visible)
                selectResult.Hide();
            _selectedRaid.ItemRewards.Add(new RaidItem_Info { Info = _selectedItem });
            _selectedRaidReward = _selectedRaid.ItemRewards[_selectedRaid.ItemRewards.Count - 1];
            UpdateRewardInterface();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectResult.Visible)
                selectResult.Hide();
        }
    }
}
