using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.MirForms.Systems
{
    public partial class ConquestBuffForm : Form
    {

        ClassBuffInfo _selectedClass;
        public ConquestBuffForm()
        {
            InitializeComponent();
            classCBox.Items.Add(MirClass.Warrior);
            classCBox.Items.Add(MirClass.Wizard);
            classCBox.Items.Add(MirClass.Taoist);
            classCBox.Items.Add(MirClass.Assassin);
            classCBox.Items.Add(MirClass.Archer);
            classCBox.SelectedIndex = 0;
            comboBox1.SelectedIndex = Settings.SWBuffInfo.Type;
            expTBox.Text = Settings.SWBuffInfo.EXPBoost.ToString();
            dropTBox.Text = Settings.SWBuffInfo.DropBoost.ToString();
        }

        private void minACBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.MinAC = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void minMACBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.MinMAC = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void minDCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.MinDC = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void minMCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.MinMC = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void minSCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.MinSC = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void accuracyBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.Accuracy = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void hpBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort tempUS))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.HP = tempUS;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void aspeedBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.ASpeed = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void mpBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!ushort.TryParse(ActiveControl.Text, out ushort tempUS))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.MP = tempUS;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void agilityBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.Agility = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void maxSCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.MaxSC = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void maxMCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.MaxMC = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void maxDCBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.MaxDC = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void maxMACBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.MaxMAC = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
        }

        private void maxACBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            for (int i = 0; i < Settings.SWBuffInfo.BuffInfo.Count; i++)
            {
                if (Settings.SWBuffInfo.BuffInfo[i].ClassInfo == _selectedClass.ClassInfo)
                {
                    _selectedClass.MaxAC = tempB;
                    Settings.SWBuffInfo.BuffInfo[i] = _selectedClass;
                }
            }
            
        }

        private void classCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((MirClass)classCBox.SelectedItem == MirClass.Warrior)
                _selectedClass = Settings.SWBuffInfo.BuffInfo[0];
            else if ((MirClass)classCBox.SelectedItem == MirClass.Wizard)
                _selectedClass = Settings.SWBuffInfo.BuffInfo[1];
            else if ((MirClass)classCBox.SelectedItem == MirClass.Taoist)
                _selectedClass = Settings.SWBuffInfo.BuffInfo[2];
            else if ((MirClass)classCBox.SelectedItem == MirClass.Assassin)
                _selectedClass = Settings.SWBuffInfo.BuffInfo[3];
            else if ((MirClass)classCBox.SelectedItem == MirClass.Archer)
                _selectedClass = Settings.SWBuffInfo.BuffInfo[4];

            if (_selectedClass != null)
            {
                minACBox.Text = _selectedClass.MinAC.ToString();
                maxACBox.Text = _selectedClass.MaxAC.ToString();
                minMACBox.Text = _selectedClass.MinMAC.ToString();
                maxMACBox.Text = _selectedClass.MaxMAC.ToString();
                minDCBox.Text = _selectedClass.MinDC.ToString();
                maxDCBox.Text = _selectedClass.MaxDC.ToString();
                minMCBox.Text = _selectedClass.MinMC.ToString();
                maxMCBox.Text = _selectedClass.MaxMC.ToString();
                minSCBox.Text = _selectedClass.MinSC.ToString();
                maxSCBox.Text = _selectedClass.MaxSC.ToString();
                agilityBox.Text = _selectedClass.Agility.ToString();
                accuracyBox.Text = _selectedClass.Accuracy.ToString();
                hpBox.Text = _selectedClass.HP.ToString();
                mpBox.Text = _selectedClass.MP.ToString();
                aspeedBox.Text = _selectedClass.ASpeed.ToString();
            }
            else
            {
                minACBox.Text = string.Empty;
                maxACBox.Text = string.Empty;
                minMACBox.Text = string.Empty;
                maxMACBox.Text = string.Empty;
                minDCBox.Text = string.Empty;
                maxDCBox.Text = string.Empty;
                minMCBox.Text = string.Empty;
                maxMCBox.Text = string.Empty;
                minSCBox.Text = string.Empty;
                maxSCBox.Text = string.Empty;
                accuracyBox.Text = string.Empty;
                agilityBox.Text = string.Empty;
                hpBox.Text = string.Empty;
                mpBox.Text = string.Empty;
                aspeedBox.Text = string.Empty;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
                Settings.SWBuffInfo.Type = (byte)comboBox1.SelectedIndex;
        }

        private void expTBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            Settings.SWBuffInfo.EXPBoost = tempB;
        }

        private void dropTBox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender)
                return;
            if (!byte.TryParse(ActiveControl.Text, out byte tempB))
            {
                ActiveControl.BackColor = Color.Red;
                return;
            }
            ActiveControl.BackColor = SystemColors.Window;
            Settings.SWBuffInfo.DropBoost = tempB;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Settings.SaveSWBuffInfo();
        }
    }
}
