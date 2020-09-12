using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace Server
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();

            VPathTextBox.Text = Settings.VersionPath;
            VersionCheckBox.Checked = Settings.CheckVersion;
            RelogDelayTextBox.Text = Settings.RelogDelay.ToString();

            IPAddressTextBox.Text = Settings.IPAddress;
            PortTextBox.Text = Settings.Port.ToString();
            TimeOutTextBox.Text = Settings.TimeOut.ToString();
            MaxUserTextBox.Text = Settings.MaxUser.ToString();
            statusportBox.Text = Settings.StatusConPort.ToString();

            AccountCheckBox.Checked = Settings.AllowNewAccount;
            PasswordCheckBox.Checked = Settings.AllowChangePassword;
            LoginCheckBox.Checked = Settings.AllowLogin;
            NCharacterCheckBox.Checked = Settings.AllowNewCharacter;
            DCharacterCheckBox.Checked = Settings.AllowDeleteCharacter;
            StartGameCheckBox.Checked = Settings.AllowStartGame;
            AllowAssassinCheckBox.Checked = Settings.AllowCreateAssassin;
            AllowArcherCheckBox.Checked = Settings.AllowCreateArcher;
            Resolution_textbox.Text = Settings.AllowedResolution.ToString();

            SafeZoneBorderCheckBox.Checked = Settings.SafeZoneBorder;
            ShowGMEffectBox.Checked = Settings.ShowGMEffect;
            StartLvlBox.Text = Settings.StartLevel.ToString();
            StartGoldBox.Text = Settings.StartGold.ToString();
            PotPerTickBox.Text = Settings.PerTickRegen.ToString();

            QuestDropBox.Text = Settings.QuestDropRate.ToString();
            QuestExpBox.Text = Settings.QuestExpRate.ToString();
            QuestGoldBox.Text = Settings.QuestGoldRate.ToString();

            NewbieNameBox.Text = Settings.NewbieName;
            NewbieExpBox.Text = Settings.NewbieExpBuff.ToString();
            NewbieLevelBox.Text = Settings.NewbieMaxLevel.ToString();

            SaveDelayTextBox.Text = Settings.SaveDelay.ToString();

            ServerVersionLabel.Text = Application.ProductVersion;
            DBVersionLabel.Text = MirEnvir.Envir.LoadVersion.ToString() + ((MirEnvir.Envir.LoadVersion < MirEnvir.Envir.Version) ? " (Update needed)" : "");
        }

        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Save();
            Settings.LoadVersion();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }

        public void Save()
        {
            Settings.VersionPath = VPathTextBox.Text;
            Settings.CheckVersion = VersionCheckBox.Checked;

            if (IPAddress.TryParse(IPAddressTextBox.Text, out IPAddress tempIP))
                Settings.IPAddress = tempIP.ToString();

            if (ushort.TryParse(PortTextBox.Text, out ushort tempshort))
                Settings.Port = tempshort;

            if (ushort.TryParse(TimeOutTextBox.Text, out tempshort))
                Settings.TimeOut = tempshort;

            if (ushort.TryParse(MaxUserTextBox.Text, out tempshort))
                Settings.MaxUser = tempshort;

            if (ushort.TryParse(RelogDelayTextBox.Text, out tempshort))
                Settings.RelogDelay = tempshort;

            if (ushort.TryParse(SaveDelayTextBox.Text, out tempshort))
                Settings.SaveDelay = tempshort;
            if (ushort.TryParse(statusportBox.Text, out tempshort))
                Settings.StatusConPort = tempshort;

            Settings.AllowNewAccount = AccountCheckBox.Checked;
            Settings.AllowChangePassword = PasswordCheckBox.Checked;
            Settings.AllowLogin = LoginCheckBox.Checked;
            Settings.AllowNewCharacter = NCharacterCheckBox.Checked;
            Settings.AllowDeleteCharacter = DCharacterCheckBox.Checked;
            Settings.AllowStartGame = StartGameCheckBox.Checked;
            Settings.AllowCreateAssassin = AllowAssassinCheckBox.Checked;
            Settings.AllowCreateArcher = AllowArcherCheckBox.Checked;

            if (int.TryParse(Resolution_textbox.Text, out int tempint))
                Settings.AllowedResolution = tempint;

            Settings.SafeZoneBorder = SafeZoneBorderCheckBox.Checked;
            Settings.SafeZoneHealing = SafeZoneHealingCheckBox.Checked;
            Settings.ShowGMEffect = ShowGMEffectBox.Checked;


            if (Int32.TryParse(StartLvlBox.Text, out tempint))
                Settings.StartLevel = tempint;

            if (Int32.TryParse(StartGoldBox.Text, out tempint))
                Settings.StartGold = tempint;

            if (Byte.TryParse(PotPerTickBox.Text, out byte tempByte))
                Settings.PerTickRegen = tempByte;

            if (float.TryParse(QuestDropBox.Text, out float tempFloat))
                Settings.QuestDropRate = tempFloat;

            if (float.TryParse(QuestGoldBox.Text, out tempFloat))
                Settings.QuestGoldRate = tempFloat;

            if (float.TryParse(QuestExpBox.Text, out tempFloat))
                Settings.QuestExpRate = tempFloat;

            if (int.TryParse(NewbieExpBox.Text, out tempint))
                Settings.NewbieExpBuff = tempint;

            if (int.TryParse(NewbieLevelBox.Text, out tempint))
                Settings.NewbieMaxLevel = tempint;

            Settings.NewbieName = NewbieNameBox.Text;
        }


        private void IPAddressCheck(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            ActiveControl.BackColor = !IPAddress.TryParse(ActiveControl.Text, out IPAddress temp) ? Color.Red : SystemColors.Window;
        }

        private void CheckUShort(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            ActiveControl.BackColor = !ushort.TryParse(ActiveControl.Text, out ushort temp) ? Color.Red : SystemColors.Window;
        }

        private void VPathBrowseButton_Click(object sender, EventArgs e)
        {
            if (VPathDialog.ShowDialog() == DialogResult.OK)
                VPathTextBox.Text = VPathDialog.FileName;
        }

        private void Resolution_textbox_TextChanged(object sender, EventArgs e)
        {
            if (ActiveControl != sender) return;


            ActiveControl.BackColor = !int.TryParse(ActiveControl.Text, out int temp) ? Color.Red : SystemColors.Window;

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void SafeZoneBorderCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SafeZoneHealingCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ShowGMEffectBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void StartLvlBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void QuestExpBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void StartGoldBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
